using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using SistemaAsistencia.Modelo.Conexion;
using SistemaAsistencia.Modelo.Entidades;

namespace SistemaAsistencia.Modelo.DAO
{
    /// <summary>
    /// Clase encargada del acceso a datos de la entidad Alumno.
    /// Contiene las operaciones CRUD.
    /// </summary>
    public class AlumnoDAO
    {
        // Instancia de la conexión a la base de datos.
        private readonly ConexionBD conexionBD = new ConexionBD();

        /// <summary>
        /// Convierte un registro obtenido desde la base de datos
        /// en un objeto Alumno.
        /// </summary>
        /// <param name="dr">DataReader con el registro actual.</param>
        /// <returns>Objeto Alumno.</returns>
        private Alumno MapearAlumno(MySqlDataReader dr)
        {
            int ordDni = dr.GetOrdinal("dni");
            int ordActivo = dr.GetOrdinal("activo");
            return new Alumno
            {
                IdAlumno = Convert.ToInt32(dr["id_alumno"]),
                NombreAlumno = Convert.ToString(dr["nombre_alumno"]),
                ApellidoAlumno = Convert.ToString(dr["apellido_alumno"]),
                DniAlumno = dr.IsDBNull(ordDni) ? string.Empty : Convert.ToString(dr["dni"]),
                LegajoAlumno = Convert.ToString(dr["legajo_alumno"]),
                CorreoAlumno = Convert.ToString(dr["correo_alumno"]),
                TelefonoAlumno = Convert.ToString(dr["telefono_alumno"]),
                Activo = !dr.IsDBNull(ordActivo) && Convert.ToBoolean(dr["activo"])
            };
        }

        /// <summary>
        /// Obtiene todos los alumnos registrados.
        /// </summary>
        public List<Alumno> ObtenerTodos()
        {
            List<Alumno> lista = new List<Alumno>();

            using (MySqlConnection cn = conexionBD.ObtenerConexion())
            {
                cn.Open();

                string sql = @"SELECT *
                                FROM alumno
                                WHERE activo = 1
                                ORDER BY apellido_alumno, nombre_alumno";

                var cmd = new MySqlCommand(sql, cn);

                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(MapearAlumno(dr));
                    }
                }
            }

            return lista;
        }

        /// <summary>
        /// Agrega un nuevo alumno a la base de datos.
        /// </summary>
        public bool Agregar(Alumno alumno)
        {
            using (MySqlConnection cn = conexionBD.ObtenerConexion())
            {
                cn.Open();

                string sql = @"INSERT INTO ALUMNO
                                (nombre_alumno, apellido_alumno, dni, legajo_alumno,
                                 correo_alumno, telefono_alumno)
                                VALUES
                                (@nombre, @apellido, @dni, @legajo,
                                 @correo, @telefono)";

                var cmd = new MySqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@nombre", alumno.NombreAlumno);
                cmd.Parameters.AddWithValue("@apellido", alumno.ApellidoAlumno);
                cmd.Parameters.AddWithValue("@dni", alumno.DniAlumno.Trim());
                cmd.Parameters.AddWithValue("@legajo", alumno.LegajoAlumno);
                cmd.Parameters.AddWithValue("@correo", alumno.CorreoAlumno);
                cmd.Parameters.AddWithValue("@telefono", alumno.TelefonoAlumno);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        /// <summary>
        /// Modifica los datos de un alumno existente.
        /// </summary>
        public bool Modificar(Alumno alumno)
        {
            using (MySqlConnection cn = conexionBD.ObtenerConexion())
            {
                cn.Open();

                string sql = @"UPDATE ALUMNO
                                SET nombre_alumno = @nombre,
                                    apellido_alumno = @apellido,
                                    dni = @dni,
                                    legajo_alumno = @legajo,
                                    correo_alumno = @correo,
                                    telefono_alumno = @telefono
                                WHERE id_alumno = @id";

                var cmd = new MySqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@nombre", alumno.NombreAlumno);
                cmd.Parameters.AddWithValue("@apellido", alumno.ApellidoAlumno);
                cmd.Parameters.AddWithValue("@dni", alumno.DniAlumno.Trim());
                cmd.Parameters.AddWithValue("@legajo", alumno.LegajoAlumno);
                cmd.Parameters.AddWithValue("@correo", alumno.CorreoAlumno);
                cmd.Parameters.AddWithValue("@telefono", alumno.TelefonoAlumno);
                cmd.Parameters.AddWithValue("@id", alumno.IdAlumno);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        /// <summary>
        /// Da de baja lógica a un alumno (activo = 0). No borra su histórico.
        /// </summary>
        public bool DarDeBaja(int idAlumno)
        {
            using (MySqlConnection cn = conexionBD.ObtenerConexion())
            {
                cn.Open();

                string sql = @"UPDATE ALUMNO
                               SET activo = 0
                               WHERE id_alumno = @id";

                var cmd = new MySqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@id", idAlumno);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        /// <summary>
        /// Cuenta los registros vinculados al alumno (p. ej. inscripciones),
        /// para informar antes de una eliminación definitiva.
        /// </summary>
        public List<Dependencia> ObtenerDependencias(int idAlumno)
        {
            List<Dependencia> dependencias = new List<Dependencia>();

            using (MySqlConnection cn = conexionBD.ObtenerConexion())
            {
                cn.Open();

                using (MySqlCommand cmd = new MySqlCommand(
                    "SELECT COUNT(*) FROM INSCRIBE WHERE id_alumno = @id", cn))
                {
                    cmd.Parameters.AddWithValue("@id", idAlumno);

                    int inscripciones = Convert.ToInt32(cmd.ExecuteScalar());

                    dependencias.Add(new Dependencia("Inscripciones en dictados", inscripciones));
                }
            }

            return dependencias;
        }

        /// <summary>
        /// Elimina definitivamente al alumno y sus registros vinculados,
        /// dentro de una transacción (evita dejar datos huérfanos).
        /// </summary>
        public bool EliminarDefinitivo(int idAlumno)
        {
            using (MySqlConnection cn = conexionBD.ObtenerConexion())
            {
                cn.Open();

                using (MySqlTransaction tx = cn.BeginTransaction())
                {
                    try
                    {
                        using (MySqlCommand cmdInscripciones = new MySqlCommand(
                            "DELETE FROM INSCRIBE WHERE id_alumno = @id", cn, tx))
                        {
                            cmdInscripciones.Parameters.AddWithValue("@id", idAlumno);
                            cmdInscripciones.ExecuteNonQuery();
                        }

                        using (MySqlCommand cmd = new MySqlCommand(
                            "DELETE FROM ALUMNO WHERE id_alumno = @id", cn, tx))
                        {
                            cmd.Parameters.AddWithValue("@id", idAlumno);

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