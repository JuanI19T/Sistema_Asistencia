using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using SistemaAsistencia.Modelo.Conexion;
using SistemaAsistencia.Modelo.Entidades;

namespace SistemaAsistencia.Modelo.DAO
{
    /// <summary>
    /// Clase encargada del acceso a datos de la entidad Dictado.
    /// Contiene las operaciones CRUD.
    /// </summary>
    public class DictadoDAO
    {
        // Instancia de la conexión a la base de datos.
        private readonly ConexionBD conexionBD = new ConexionBD();

        /// <summary>
        /// Convierte un registro obtenido desde la base de datos
        /// en un objeto Dictado.
        /// </summary>
        /// <param name="dr">DataReader con el registro actual.</param>
        /// <returns>Objeto Dictado.</returns>
        private Dictado MapearDictado(MySqlDataReader dr)
        {
            return new Dictado
            {
                IdDictado = Convert.ToInt32(dr["id_dictado"]),
                IdMateria = Convert.ToInt32(dr["id_materia"]),
                IdProfesor = Convert.ToInt32(dr["id_profesor"]),
                Dia = Convert.ToString(dr["dia"]),
                Horario = Convert.ToString(dr["horario"]),
                Grupo = Convert.ToString(dr["grupo"]),
                AnioLectivo = Convert.ToInt32(dr["anio_lectivo"]),
                NombreMateria = Convert.ToString(dr["nombre_materia"]),
                ApellidoProfesor = Convert.ToString(dr["apellido_profesor"])
            };
        }

        /// <summary>
        /// Obtiene todos los dictados registrados.
        /// </summary>
        public List<Dictado> ObtenerTodos()
        {
            List<Dictado> lista = new List<Dictado>();

            using (MySqlConnection cn = conexionBD.ObtenerConexion())
            {
                cn.Open();

                string sql = @"SELECT D.id_dictado, D.id_materia, D.id_profesor,
                                      D.dia, D.horario, D.grupo, D.anio_lectivo,
                                      M.nombre_materia, P.apellido_profesor
                               FROM DICTADO D
                               INNER JOIN MATERIA M
                                   ON M.id_materia = D.id_materia
INNER JOIN PROFESOR P
                                    ON P.id_profesor = D.id_profesor
                               WHERE D.activo = 1
                               ORDER BY D.anio_lectivo, D.grupo,
                                        M.nombre_materia, D.dia, D.horario";

                var cmd = new MySqlCommand(sql, cn);

                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(MapearDictado(dr));
                    }
                }
            }

            return lista;
        }

        /// <summary>
        /// Agrega un nuevo dictado a la base de datos.
        /// </summary>
        public bool Agregar(Dictado dictado)
        {
            using (MySqlConnection cn = conexionBD.ObtenerConexion())
            {
                cn.Open();

                string sql = @"INSERT INTO DICTADO
                               (id_materia, id_profesor, dia, horario, grupo, anio_lectivo)
                               VALUES
                               (@id_materia, @id_profesor, @dia, @horario, @grupo, @anio_lectivo)";

                var cmd = new MySqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@id_materia", dictado.IdMateria);
                cmd.Parameters.AddWithValue("@id_profesor", dictado.IdProfesor);
                cmd.Parameters.AddWithValue("@dia", dictado.Dia);
                cmd.Parameters.AddWithValue("@horario", dictado.Horario);
                cmd.Parameters.AddWithValue("@grupo", dictado.Grupo);
                cmd.Parameters.AddWithValue("@anio_lectivo", dictado.AnioLectivo);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        /// <summary>
        /// Modifica los datos de un dictado existente.
        /// </summary>
        public bool Modificar(Dictado dictado)
        {
            using (MySqlConnection cn = conexionBD.ObtenerConexion())
            {
                cn.Open();

                string sql = @"UPDATE DICTADO
                               SET id_materia = @id_materia,
                                   id_profesor = @id_profesor,
                                   dia = @dia,
                                   horario = @horario,
                                   grupo = @grupo,
                                   anio_lectivo = @anio_lectivo
                               WHERE id_dictado = @id";

                var cmd = new MySqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@id_materia", dictado.IdMateria);
                cmd.Parameters.AddWithValue("@id_profesor", dictado.IdProfesor);
                cmd.Parameters.AddWithValue("@dia", dictado.Dia);
                cmd.Parameters.AddWithValue("@horario", dictado.Horario);
                cmd.Parameters.AddWithValue("@grupo", dictado.Grupo);
                cmd.Parameters.AddWithValue("@anio_lectivo", dictado.AnioLectivo);
                cmd.Parameters.AddWithValue("@id", dictado.IdDictado);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        /// <summary>
        /// Da de baja lógica a un dictado (activo = 0). No borra su histórico.
        /// </summary>
        public bool DarDeBaja(int idDictado)
        {
            using (MySqlConnection cn = conexionBD.ObtenerConexion())
            {
                cn.Open();

                string sql = @"UPDATE DICTADO
                               SET activo = 0
                               WHERE id_dictado = @id";

                var cmd = new MySqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@id", idDictado);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        /// <summary>
        /// Cuenta los alumnos inscriptos en el dictado que se borrarían
        /// con una eliminación definitiva.
        /// </summary>
        public List<Dependencia> ObtenerDependencias(int idDictado)
        {
            List<Dependencia> dependencias = new List<Dependencia>();

            using (MySqlConnection cn = conexionBD.ObtenerConexion())
            {
                cn.Open();

                using (MySqlCommand cmd = new MySqlCommand(
                    "SELECT COUNT(*) FROM INSCRIBE WHERE id_dictado = @id", cn))
                {
                    cmd.Parameters.AddWithValue("@id", idDictado);

                    int alumnos = Convert.ToInt32(cmd.ExecuteScalar());

                    dependencias.Add(new Dependencia("Alumnos inscriptos", alumnos));
                }
            }

            return dependencias;
        }

        /// <summary>
        /// Elimina definitivamente el dictado y sus inscripciones vinculadas,
        /// dentro de una transacción (evita dejar datos huérfanos).
        /// </summary>
        public bool EliminarDefinitivo(int idDictado)
        {
            using (MySqlConnection cn = conexionBD.ObtenerConexion())
            {
                cn.Open();

                using (MySqlTransaction tx = cn.BeginTransaction())
                {
                    try
                    {
                        using (MySqlCommand cmdInscripciones = new MySqlCommand(
                            "DELETE FROM INSCRIBE WHERE id_dictado = @id", cn, tx))
                        {
                            cmdInscripciones.Parameters.AddWithValue("@id", idDictado);
                            cmdInscripciones.ExecuteNonQuery();
                        }

                        using (MySqlCommand cmd = new MySqlCommand(
                            "DELETE FROM DICTADO WHERE id_dictado = @id", cn, tx))
                        {
                            cmd.Parameters.AddWithValue("@id", idDictado);

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