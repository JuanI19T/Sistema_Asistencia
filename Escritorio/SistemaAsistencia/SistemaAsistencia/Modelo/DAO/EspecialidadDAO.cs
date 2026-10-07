using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using SistemaAsistencia.Modelo.Conexion;
using SistemaAsistencia.Modelo.Entidades;
using SistemaAsistencia.Utilidades;

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
                                FROM especialidad
                                WHERE activo = 1
                                ORDER BY nombre_especialidad";

                var cmd = new MySqlCommand(sql, cn);

                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        int ordDiv = dr.GetOrdinal("division");
                        int ordAct = dr.GetOrdinal("activo");
                        lista.Add(new Especialidad
                        {
                            IdEspecialidad = Convert.ToInt32(dr["id_especialidad"]),
                            NombreEspecialidad = Convert.ToString(dr["nombre_especialidad"]),
                            Division = dr.IsDBNull(ordDiv) ? (int?)null : Convert.ToInt32(dr["division"]),
                            Activo = !dr.IsDBNull(ordAct) && Convert.ToBoolean(dr["activo"])
                        });
                    }
                }
            }

            return lista;
        }

        /// <summary>
        /// Obtiene todas las especialidades, incluyendo inactivas.
        /// Solo para los ABM (el toggle Activar necesita ver las inactivas).
        /// </summary>
        public List<Especialidad> ObtenerTodasIncluyendoInactivos()
        {
            List<Especialidad> lista = new List<Especialidad>();

            using (MySqlConnection cn = conexionBD.ObtenerConexion())
            {
                cn.Open();

                string sql = @"SELECT *
                                 FROM especialidad
                                 ORDER BY nombre_especialidad";

                var cmd = new MySqlCommand(sql, cn);

                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        int ordDiv = dr.GetOrdinal("division");
                        int ordAct = dr.GetOrdinal("activo");
                        lista.Add(new Especialidad
                        {
                            IdEspecialidad = Convert.ToInt32(dr["id_especialidad"]),
                            NombreEspecialidad = Convert.ToString(dr["nombre_especialidad"]),
                            Division = dr.IsDBNull(ordDiv) ? (int?)null : Convert.ToInt32(dr["division"]),
                            Activo = !dr.IsDBNull(ordAct) && Convert.ToBoolean(dr["activo"])
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

                string sql = @"INSERT INTO especialidad
                                (nombre_especialidad, activo)
                                VALUES
                                (@nombre, @activo)";

                var cmd = new MySqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@nombre", especialidad.NombreEspecialidad);
                // Todo lo creado desde el escritorio nace activo.
                cmd.Parameters.AddWithValue("@activo", 1);

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

                string sql = @"UPDATE especialidad
                               SET nombre_especialidad = @nombre
                               WHERE id_especialidad = @id";

                var cmd = new MySqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@nombre", especialidad.NombreEspecialidad);
                cmd.Parameters.AddWithValue("@id", especialidad.IdEspecialidad);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        /// <summary>
        /// Actualiza solo la división fija de una especialidad.
        /// NULL = libre (Ciclo Básico). Es el único cambio permitido
        /// sobre el catálogo fijo (el nombre no se toca).
        /// </summary>
        public bool ModificarDivision(int idEspecialidad, int? division)
        {
            using (MySqlConnection cn = conexionBD.ObtenerConexion())
            {
                cn.Open();

                string sql = @"UPDATE especialidad
                                SET division = @division
                                WHERE id_especialidad = @id";

                var cmd = new MySqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@division",
                    division.HasValue ? (object)division.Value : System.DBNull.Value);
                cmd.Parameters.AddWithValue("@id", idEspecialidad);

                try
                {
                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (MySqlException ex) when (ex.Number == 1062)
                {
                    throw new DatosException("Esa división ya corresponde a otra especialidad.");
                }
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

                string sql = @"UPDATE especialidad
                                SET activo = 0
                                WHERE id_especialidad = @id";

                var cmd = new MySqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@id", idEspecialidad);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        /// <summary>
        /// Da de alta a una especialidad (activo = 1). Reverso de la baja logica.
        /// </summary>
        public bool DarDeAlta(int idEspecialidad)
        {
            using (MySqlConnection cn = conexionBD.ObtenerConexion())
            {
                cn.Open();

                string sql = @"UPDATE especialidad
                                SET activo = 1
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
                    "SELECT COUNT(*) FROM materia WHERE id_especialidad = @id", cn))
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
                            "DELETE FROM materia WHERE id_especialidad = @id", cn, tx))
                        {
                            cmdMaterias.Parameters.AddWithValue("@id", idEspecialidad);
                            cmdMaterias.ExecuteNonQuery();
                        }

                        using (MySqlCommand cmd = new MySqlCommand(
                            "DELETE FROM especialidad WHERE id_especialidad = @id", cn, tx))
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