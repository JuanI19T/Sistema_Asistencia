using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using SistemaAsistencia.Modelo.Conexion;
using SistemaAsistencia.Modelo.Entidades;

namespace SistemaAsistencia.Modelo.DAO
{
    /// <summary>
    /// Clase encargada del acceso a datos de la entidad Especialidad.
    /// Por ahora solo se necesita consultar las especialidades existentes.
    /// </summary>
    public class EspecialidadDAO
    {
        // Instancia de la conexión a la base de datos.
        private readonly ConexionBD conexionBD = new ConexionBD();

        /// <summary>
        /// Obtiene todas las especialidades registradas.
        /// </summary>
        public List<Especialidad> ObtenerTodas()
        {
            List<Especialidad> lista = new List<Especialidad>();

            using (MySqlConnection cn = conexionBD.ObtenerConexion())
            {
                cn.Open();

                string sql = @"SELECT *
                               FROM ESPECIALIDAD
                               WHERE activo = 1
                               ORDER BY nombre_especialidad";

                var cmd = new MySqlCommand(sql, cn);

                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new Especialidad
                        {
                            IdEspecialidad = Convert.ToInt32(dr["id_especialidad"]),
                            NombreEspecialidad = Convert.ToString(dr["nombre_especialidad"])
                        });
                    }
                }
            }

            return lista;
        }

        /// <summary>
        /// Agrega una nueva especialidad a la base de datos.
        /// </summary>
        public bool Agregar(Especialidad especialidad)
        {
            using (MySqlConnection cn = conexionBD.ObtenerConexion())
            {
                cn.Open();

                string sql = @"INSERT INTO ESPECIALIDAD
                               (nombre_especialidad)
                               VALUES
                               (@nombre)";

                var cmd = new MySqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@nombre", especialidad.NombreEspecialidad);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        /// <summary>
        /// Modifica los datos de una especialidad existente.
        /// </summary>
        public bool Modificar(Especialidad especialidad)
        {
            using (MySqlConnection cn = conexionBD.ObtenerConexion())
            {
                cn.Open();

                string sql = @"UPDATE ESPECIALIDAD
                               SET nombre_especialidad = @nombre
                               WHERE id_especialidad = @id";

                var cmd = new MySqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@nombre", especialidad.NombreEspecialidad);
                cmd.Parameters.AddWithValue("@id", especialidad.IdEspecialidad);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        /// <summary>
        /// Da de baja lógica a una especialidad (activo = 0). No borra su histórico.
        /// </summary>
        public bool DarDeBaja(int idEspecialidad)
        {
            using (MySqlConnection cn = conexionBD.ObtenerConexion())
            {
                cn.Open();

                string sql = @"UPDATE ESPECIALIDAD
                               SET activo = 0
                               WHERE id_especialidad = @id";

                var cmd = new MySqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@id", idEspecialidad);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        /// <summary>
        /// Cuenta las materias de la especialidad que se borrarían
        /// con una eliminación definitiva.
        /// </summary>
        public List<Dependencia> ObtenerDependencias(int idEspecialidad)
        {
            List<Dependencia> dependencias = new List<Dependencia>();

            using (MySqlConnection cn = conexionBD.ObtenerConexion())
            {
                cn.Open();

                using (MySqlCommand cmd = new MySqlCommand(
                    "SELECT COUNT(*) FROM MATERIA WHERE id_especialidad = @id", cn))
                {
                    cmd.Parameters.AddWithValue("@id", idEspecialidad);

                    int materias = Convert.ToInt32(cmd.ExecuteScalar());

                    dependencias.Add(new Dependencia("Materias de la especialidad", materias));
                }
            }

            return dependencias;
        }

        /// <summary>
        /// Elimina definitivamente la especialidad y sus materias vinculadas,
        /// dentro de una transacción (evita dejar datos huérfanos).
        /// </summary>
        public bool EliminarDefinitivo(int idEspecialidad)
        {
            using (MySqlConnection cn = conexionBD.ObtenerConexion())
            {
                cn.Open();

                using (MySqlTransaction tx = cn.BeginTransaction())
                {
                    try
                    {
                        using (MySqlCommand cmdMaterias = new MySqlCommand(
                            "DELETE FROM MATERIA WHERE id_especialidad = @id", cn, tx))
                        {
                            cmdMaterias.Parameters.AddWithValue("@id", idEspecialidad);
                            cmdMaterias.ExecuteNonQuery();
                        }

                        using (MySqlCommand cmd = new MySqlCommand(
                            "DELETE FROM ESPECIALIDAD WHERE id_especialidad = @id", cn, tx))
                        {
                            cmd.Parameters.AddWithValue("@id", idEspecialidad);

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