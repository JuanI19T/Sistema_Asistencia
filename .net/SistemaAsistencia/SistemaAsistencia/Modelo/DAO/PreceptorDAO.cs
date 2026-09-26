using System;
using System.Collections.Generic;
using CryptSharp;
using MySql.Data.MySqlClient;
using SistemaAsistencia.Modelo.Conexion;
using SistemaAsistencia.Modelo.Entidades;
using SistemaAsistencia.Utilidades;

namespace SistemaAsistencia.Modelo.DAO
{
    /// <summary>
    /// Clase encargada del acceso a datos de la entidad Preceptor.
    /// Contiene las operaciones CRUD.
    /// </summary>
    public class PreceptorDAO
    {
        // Instancia de la conexión a la base de datos.
        private readonly ConexionBD conexionBD = new ConexionBD();

        /// <summary>
        /// Convierte un registro obtenido desde la base de datos
        /// en un objeto Preceptor.
        /// </summary>
        /// <param name="dr">DataReader con el registro actual.</param>
        /// <returns>Objeto Preceptor.</returns>
        private Preceptor MapearPreceptor(MySqlDataReader dr)
        {
            return new Preceptor
            {
                IdPreceptor = Convert.ToInt32(dr["id_preceptor"]),
                NombrePreceptor = Convert.ToString(dr["nombre_preceptor"]),
                ApellidoPreceptor = Convert.ToString(dr["apellido_preceptor"]),
                LegajoPreceptor = Convert.ToString(dr["legajo_preceptor"]),
                Dni = Convert.ToString(dr["dni"]),
                CorreoPreceptor = Convert.ToString(dr["correo_preceptor"]),
                TelefonoPreceptor = Convert.ToString(dr["telefono_preceptor"])
            };
        }

        /// <summary>
        /// Obtiene todos los preceptores registrados.
        /// </summary>
        public List<Preceptor> ObtenerTodos()
        {
            List<Preceptor> lista = new List<Preceptor>();

            using (MySqlConnection cn = conexionBD.ObtenerConexion())
            {
                cn.Open();

                string sql = @"SELECT *
                               FROM PRECEPTOR
                               WHERE activo = 1
                               ORDER BY apellido_preceptor, nombre_preceptor";

                var cmd = new MySqlCommand(sql, cn);

                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(MapearPreceptor(dr));
                    }
                }
            }

            return lista;
        }

        /// <summary>
        /// Agrega un nuevo preceptor a la base de datos.
        /// La contraseña se guarda con hash bcrypt (formato compatible con
        /// el login de la API). Si no se indica contraseña, se usa el DNI.
        /// </summary>
        public bool Agregar(Preceptor preceptor)
        {
            if (string.IsNullOrWhiteSpace(preceptor.Dni))
            {
                throw new DatosException("El DNI es obligatorio.");
            }

            string plana = string.IsNullOrEmpty(preceptor.Contrasena)
                ? preceptor.Dni
                : preceptor.Contrasena;

            using (MySqlConnection cn = conexionBD.ObtenerConexion())
            {
                cn.Open();

                string sql = @"INSERT INTO PRECEPTOR
                               (nombre_preceptor, apellido_preceptor, legajo_preceptor,
                                dni, correo_preceptor, telefono_preceptor, contrasena)
                               VALUES
                               (@nombre, @apellido, @legajo, @dni, @correo, @telefono, @contrasena)";

                var cmd = new MySqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@nombre", preceptor.NombrePreceptor);
                cmd.Parameters.AddWithValue("@apellido", preceptor.ApellidoPreceptor);
                cmd.Parameters.AddWithValue("@legajo", preceptor.LegajoPreceptor);
                cmd.Parameters.AddWithValue("@dni", preceptor.Dni);
                cmd.Parameters.AddWithValue("@correo", preceptor.CorreoPreceptor);
                cmd.Parameters.AddWithValue("@telefono", preceptor.TelefonoPreceptor);
                cmd.Parameters.AddWithValue("@contrasena",
                    Crypter.Blowfish.Crypt(plana, Crypter.Blowfish.GenerateSalt(10)));

                try
                {
                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (MySqlException ex) when (ex.Number == 1062)
                {
                    throw new DatosException("Ya existe un preceptor con ese DNI o legajo.");
                }
            }
        }

        /// <summary>
        /// Modifica los datos de un preceptor existente.
        /// La contraseña solo se actualiza si viene indicada (en blanco
        /// conserva la actual).
        /// </summary>
        public bool Modificar(Preceptor preceptor)
        {
            if (string.IsNullOrWhiteSpace(preceptor.Dni))
            {
                throw new DatosException("El DNI es obligatorio.");
            }

            string nuevoHash = string.IsNullOrEmpty(preceptor.Contrasena)
                ? null
                : Crypter.Blowfish.Crypt(preceptor.Contrasena, Crypter.Blowfish.GenerateSalt(10));

            using (MySqlConnection cn = conexionBD.ObtenerConexion())
            {
                cn.Open();

                string sql = @"UPDATE PRECEPTOR
                               SET nombre_preceptor = @nombre,
                                   apellido_preceptor = @apellido,
                                   legajo_preceptor = @legajo,
                                   dni = @dni,
                                   correo_preceptor = @correo,
                                   telefono_preceptor = @telefono,
                                   contrasena = IFNULL(@contrasena, contrasena)
                               WHERE id_preceptor = @id";

                var cmd = new MySqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@nombre", preceptor.NombrePreceptor);
                cmd.Parameters.AddWithValue("@apellido", preceptor.ApellidoPreceptor);
                cmd.Parameters.AddWithValue("@legajo", preceptor.LegajoPreceptor);
                cmd.Parameters.AddWithValue("@dni", preceptor.Dni);
                cmd.Parameters.AddWithValue("@correo", preceptor.CorreoPreceptor);
                cmd.Parameters.AddWithValue("@telefono", preceptor.TelefonoPreceptor);
                cmd.Parameters.AddWithValue("@contrasena", (object)nuevoHash ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@id", preceptor.IdPreceptor);

                try
                {
                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (MySqlException ex) when (ex.Number == 1062)
                {
                    throw new DatosException("Ya existe un preceptor con ese DNI o legajo.");
                }
            }
        }

        /// <summary>
        /// Da de baja lógica a un preceptor (activo = 0). No borra su histórico.
        /// </summary>
        public bool DarDeBaja(int idPreceptor)
        {
            using (MySqlConnection cn = conexionBD.ObtenerConexion())
            {
                cn.Open();

                string sql = @"UPDATE PRECEPTOR
                               SET activo = 0
                               WHERE id_preceptor = @id";

                var cmd = new MySqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@id", idPreceptor);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        /// <summary>
        /// Cuenta los dictados a cargo del preceptor que se borrarían
        /// con una eliminación definitiva.
        /// </summary>
        public List<Dependencia> ObtenerDependencias(int idPreceptor)
        {
            List<Dependencia> dependencias = new List<Dependencia>();

            using (MySqlConnection cn = conexionBD.ObtenerConexion())
            {
                cn.Open();

                using (MySqlCommand cmd = new MySqlCommand(
                    "SELECT COUNT(*) FROM DICTADO WHERE id_preceptor = @id", cn))
                {
                    cmd.Parameters.AddWithValue("@id", idPreceptor);

                    int dictados = Convert.ToInt32(cmd.ExecuteScalar());

                    dependencias.Add(new Dependencia("Dictados a cargo", dictados));
                }
            }

            return dependencias;
        }

        /// <summary>
        /// Elimina definitivamente al preceptor y sus dictados vinculados,
        /// dentro de una transacción (evita dejar datos huérfanos).
        /// </summary>
        public bool EliminarDefinitivo(int idPreceptor)
        {
            using (MySqlConnection cn = conexionBD.ObtenerConexion())
            {
                cn.Open();

                using (MySqlTransaction tx = cn.BeginTransaction())
                {
                    try
                    {
                        using (MySqlCommand cmdDictados = new MySqlCommand(
                            "DELETE FROM DICTADO WHERE id_preceptor = @id", cn, tx))
                        {
                            cmdDictados.Parameters.AddWithValue("@id", idPreceptor);
                            cmdDictados.ExecuteNonQuery();
                        }

                        using (MySqlCommand cmd = new MySqlCommand(
                            "DELETE FROM PRECEPTOR WHERE id_preceptor = @id", cn, tx))
                        {
                            cmd.Parameters.AddWithValue("@id", idPreceptor);

                            bool eliminado = cmd.ExecuteNonQuery() > 0;

                            tx.Commit();

                            return eliminado;
                        }
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}