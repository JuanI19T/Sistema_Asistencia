using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using SistemaAsistencia.Modelo.Conexion;
using SistemaAsistencia.Modelo.Entidades;

namespace SistemaAsistencia.Modelo.DAO
{
    /// <summary>
    /// Clase encargada del acceso a datos de la entidad Materia.
    /// Contiene las operaciones CRUD.
    /// </summary>
    public class MateriaDAO
    {
        // Instancia de la conexión a la base de datos.
        private readonly ConexionBD conexionBD = new ConexionBD();

        /// <summary>
        /// Convierte un registro obtenido desde la base de datos
        /// en un objeto Materia.
        /// </summary>
        /// <param name="dr">DataReader con el registro actual.</param>
        /// <returns>Objeto Materia.</returns>
        private Materia MapearMateria(MySqlDataReader dr)
        {
            bool activo = true;
            try
            {
                int o = dr.GetOrdinal("activo");
                if (!dr.IsDBNull(o)) activo = Convert.ToBoolean(dr["activo"]);
            }
            catch (IndexOutOfRangeException) { }
            return new Materia
            {
                IdMateria = Convert.ToInt32(dr["id_materia"]),
                IdEspecialidad = Convert.ToInt32(dr["id_especialidad"]),
                NombreMateria = Convert.ToString(dr["nombre_materia"]),
                CargaHoraria = Convert.ToInt32(dr["carga_horaria"]),
                AnioMateria = Convert.ToInt32(dr["anio_materia"]),
                NombreEspecialidad = Convert.ToString(dr["nombre_especialidad"]),
                Activo = activo
            };
        }

        /// <summary>
        /// Obtiene todas las materias registradas.
        /// </summary>
        public List<Materia> ObtenerTodos()
        {
            List<Materia> lista = new List<Materia>();

            using (MySqlConnection cn = conexionBD.ObtenerConexion())
            {
                cn.Open();

                string sql = @"SELECT M.id_materia, M.id_especialidad, M.nombre_materia,
                                      M.carga_horaria, M.anio_materia, M.activo,
                                      E.nombre_especialidad
                                FROM materia M
                                INNER JOIN especialidad E
                                    ON E.id_especialidad = M.id_especialidad
                                WHERE M.activo = 1
                                ORDER BY M.nombre_materia";

                var cmd = new MySqlCommand(sql, cn);

                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(MapearMateria(dr));
                    }
                }
            }

            return lista;
        }

        /// <summary>
        /// Obtiene todas las materias, incluyendo inactivas.
        /// Solo para los ABM (el toggle Activar necesita ver las inactivas).
        /// </summary>
        public List<Materia> ObtenerTodosIncluyendoInactivos()
        {
            List<Materia> lista = new List<Materia>();

            using (MySqlConnection cn = conexionBD.ObtenerConexion())
            {
                cn.Open();

                string sql = @"SELECT M.id_materia, M.id_especialidad, M.nombre_materia,
                                      M.carga_horaria, M.anio_materia, M.activo,
                                      E.nombre_especialidad
                                FROM materia M
                                INNER JOIN especialidad E
                                    ON E.id_especialidad = M.id_especialidad
                                ORDER BY M.nombre_materia";

                var cmd = new MySqlCommand(sql, cn);

                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(MapearMateria(dr));
                    }
                }
            }

            return lista;
        }

        /// <summary>
        /// Agrega una nueva materia a la base de datos.
        /// </summary>
        public bool Agregar(Materia materia)
        {
            using (MySqlConnection cn = conexionBD.ObtenerConexion())
            {
                cn.Open();

                string sql = @"INSERT INTO materia
                                (id_especialidad, nombre_materia, carga_horaria, anio_materia, activo)
                                VALUES
                                (@id_especialidad, @nombre, @carga_horaria, @anio, @activo)";

                var cmd = new MySqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@id_especialidad", materia.IdEspecialidad);
                cmd.Parameters.AddWithValue("@nombre", materia.NombreMateria);
                cmd.Parameters.AddWithValue("@carga_horaria", materia.CargaHoraria);
                cmd.Parameters.AddWithValue("@anio", materia.AnioMateria);
                // Todo lo creado desde el escritorio nace activo.
                cmd.Parameters.AddWithValue("@activo", 1);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        /// <summary>
        /// Modifica los datos de una materia existente.
        /// </summary>
        public bool Modificar(Materia materia)
        {
            using (MySqlConnection cn = conexionBD.ObtenerConexion())
            {
                cn.Open();

                string sql = @"UPDATE materia
                               SET id_especialidad = @id_especialidad,
                                   nombre_materia = @nombre,
                                   carga_horaria = @carga_horaria,
                                   anio_materia = @anio
                               WHERE id_materia = @id";

                var cmd = new MySqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@id_especialidad", materia.IdEspecialidad);
                cmd.Parameters.AddWithValue("@nombre", materia.NombreMateria);
                cmd.Parameters.AddWithValue("@carga_horaria", materia.CargaHoraria);
                cmd.Parameters.AddWithValue("@anio", materia.AnioMateria);
                cmd.Parameters.AddWithValue("@id", materia.IdMateria);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        /// <summary>
        /// Da de baja lógica a una materia (activo = 0). No borra su histórico.
        /// </summary>
        public bool DarDeBaja(int idMateria)
        {
            using (MySqlConnection cn = conexionBD.ObtenerConexion())
            {
                cn.Open();

                string sql = @"UPDATE materia
                                SET activo = 0
                                WHERE id_materia = @id";

                var cmd = new MySqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@id", idMateria);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        /// <summary>
        /// Da de alta a una materia (activo = 1). Reverso de la baja logica.
        /// </summary>
        public bool DarDeAlta(int idMateria)
        {
            using (MySqlConnection cn = conexionBD.ObtenerConexion())
            {
                cn.Open();

                string sql = @"UPDATE materia
                                SET activo = 1
                                WHERE id_materia = @id";

                var cmd = new MySqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@id", idMateria);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        /// <summary>
        /// Cuenta los dictados que usan la materia que se borrarían
        /// con una eliminación definitiva.
        /// </summary>
        public List<Dependencia> ObtenerDependencias(int idMateria)
        {
            List<Dependencia> dependencias = new List<Dependencia>();

            using (MySqlConnection cn = conexionBD.ObtenerConexion())
            {
                cn.Open();

                using (MySqlCommand cmd = new MySqlCommand(
                    "SELECT COUNT(*) FROM dictado WHERE id_materia = @id", cn))
                {
                    cmd.Parameters.AddWithValue("@id", idMateria);

                    int dictados = Convert.ToInt32(cmd.ExecuteScalar());

                    dependencias.Add(new Dependencia("Dictados que la usan", dictados));
                }
            }

            return dependencias;
        }

        /// <summary>
        /// Elimina definitivamente la materia y sus dictados vinculados,
        /// dentro de una transacción (evita dejar datos huérfanos).
        /// </summary>
        public bool EliminarDefinitivo(int idMateria)
        {
            using (MySqlConnection cn = conexionBD.ObtenerConexion())
            {
                cn.Open();

                using (MySqlTransaction tx = cn.BeginTransaction())
                {
                    try
                    {
                        using (MySqlCommand cmdDictados = new MySqlCommand(
                            "DELETE FROM dictado WHERE id_materia = @id", cn, tx))
                        {
                            cmdDictados.Parameters.AddWithValue("@id", idMateria);
                            cmdDictados.ExecuteNonQuery();
                        }

                        using (MySqlCommand cmd = new MySqlCommand(
                            "DELETE FROM materia WHERE id_materia = @id", cn, tx))
                        {
                            cmd.Parameters.AddWithValue("@id", idMateria);

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