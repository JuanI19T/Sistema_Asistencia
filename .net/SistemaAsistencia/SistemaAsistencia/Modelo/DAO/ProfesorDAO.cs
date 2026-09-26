using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using SistemaAsistencia.Modelo.Conexion;
using SistemaAsistencia.Modelo.Entidades;

namespace SistemaAsistencia.Modelo.DAO
{
    /// <summary>
    /// Clase encargada del acceso a datos de la entidad Profesor.
    /// Contiene las operaciones CRUD.
    /// </summary>
    public class ProfesorDAO
    {
        // Instancia de la conexión a la base de datos.
        private readonly ConexionBD conexionBD = new ConexionBD();

        /// <summary>
        /// Convierte un registro obtenido desde la base de datos
        /// en un objeto Profesor.
        /// </summary>
        /// <param name="dr">DataReader con el registro actual.</param>
        /// <returns>Objeto Profesor.</returns>
        private Profesor MapearProfesor(MySqlDataReader dr)
        {
            return new Profesor
            {
                IdProfesor = Convert.ToInt32(dr["id_profesor"]),
                NombreProfesor = Convert.ToString(dr["nombre_profesor"]),
                ApellidoProfesor = Convert.ToString(dr["apellido_profesor"]),
                LegajoProfesor = Convert.ToString(dr["legajo_profesor"]),
                CorreoProfesor = Convert.ToString(dr["correo_profesor"]),
                TelefonoProfesor = Convert.ToString(dr["telefono_profesor"])
            };
        }

        /// <summary>
        /// Obtiene todos los profesores registrados.
        /// </summary>
        public List<Profesor> ObtenerTodos()
        {
            List<Profesor> lista = new List<Profesor>();

            using (MySqlConnection cn = conexionBD.ObtenerConexion())
            {
                cn.Open();

                string sql = @"SELECT *
                               FROM PROFESOR
                               WHERE activo = 1
                               ORDER BY apellido_profesor, nombre_profesor";

                var cmd = new MySqlCommand(sql, cn);

                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(MapearProfesor(dr));
                    }
                }
            }

            return lista;
        }

        /// <summary>
        /// Agrega un nuevo profesor a la base de datos.
        /// </summary>
        public bool Agregar(Profesor profesor)
        {
            using (MySqlConnection cn = conexionBD.ObtenerConexion())
            {
                cn.Open();

                string sql = @"INSERT INTO PROFESOR
                               (nombre_profesor, apellido_profesor, legajo_profesor,
                                correo_profesor, telefono_profesor)
                               VALUES
                               (@nombre, @apellido, @legajo, @correo, @telefono)";

                var cmd = new MySqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@nombre", profesor.NombreProfesor);
                cmd.Parameters.AddWithValue("@apellido", profesor.ApellidoProfesor);
                cmd.Parameters.AddWithValue("@legajo", profesor.LegajoProfesor);
                cmd.Parameters.AddWithValue("@correo", profesor.CorreoProfesor);
                cmd.Parameters.AddWithValue("@telefono", profesor.TelefonoProfesor);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        /// <summary>
        /// Modifica los datos de un profesor existente.
        /// </summary>
        public bool Modificar(Profesor profesor)
        {
            using (MySqlConnection cn = conexionBD.ObtenerConexion())
            {
                cn.Open();

                string sql = @"UPDATE PROFESOR
                               SET nombre_profesor = @nombre,
                                   apellido_profesor = @apellido,
                                   legajo_profesor = @legajo,
                                   correo_profesor = @correo,
                                   telefono_profesor = @telefono
                               WHERE id_profesor = @id";

                var cmd = new MySqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@nombre", profesor.NombreProfesor);
                cmd.Parameters.AddWithValue("@apellido", profesor.ApellidoProfesor);
                cmd.Parameters.AddWithValue("@legajo", profesor.LegajoProfesor);
                cmd.Parameters.AddWithValue("@correo", profesor.CorreoProfesor);
                cmd.Parameters.AddWithValue("@telefono", profesor.TelefonoProfesor);
                cmd.Parameters.AddWithValue("@id", profesor.IdProfesor);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        /// <summary>
        /// Da de baja lógica a un profesor (activo = 0). No borra su histórico.
        /// </summary>
        public bool DarDeBaja(int idProfesor)
        {
            using (MySqlConnection cn = conexionBD.ObtenerConexion())
            {
                cn.Open();

                string sql = @"UPDATE PROFESOR
                               SET activo = 0
                               WHERE id_profesor = @id";

                var cmd = new MySqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@id", idProfesor);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        /// <summary>
        /// Cuenta los dictados a cargo del profesor que se borrarían
        /// con una eliminación definitiva.
        /// </summary>
        public List<Dependencia> ObtenerDependencias(int idProfesor)
        {
            List<Dependencia> dependencias = new List<Dependencia>();

            using (MySqlConnection cn = conexionBD.ObtenerConexion())
            {
                cn.Open();

                using (MySqlCommand cmd = new MySqlCommand(
                    "SELECT COUNT(*) FROM DICTADO WHERE id_profesor = @id", cn))
                {
                    cmd.Parameters.AddWithValue("@id", idProfesor);

                    int dictados = Convert.ToInt32(cmd.ExecuteScalar());

                    dependencias.Add(new Dependencia("Dictados a cargo", dictados));
                }
            }

            return dependencias;
        }

        /// <summary>
        /// Elimina definitivamente al profesor y sus dictados vinculados,
        /// dentro de una transacción (evita dejar datos huérfanos).
        /// </summary>
        public bool EliminarDefinitivo(int idProfesor)
        {
            using (MySqlConnection cn = conexionBD.ObtenerConexion())
            {
                cn.Open();

                using (MySqlTransaction tx = cn.BeginTransaction())
                {
                    try
                    {
                        using (MySqlCommand cmdDictados = new MySqlCommand(
                            "DELETE FROM DICTADO WHERE id_profesor = @id", cn, tx))
                        {
                            cmdDictados.Parameters.AddWithValue("@id", idProfesor);
                            cmdDictados.ExecuteNonQuery();
                        }

                        using (MySqlCommand cmd = new MySqlCommand(
                            "DELETE FROM PROFESOR WHERE id_profesor = @id", cn, tx))
                        {
                            cmd.Parameters.AddWithValue("@id", idProfesor);

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