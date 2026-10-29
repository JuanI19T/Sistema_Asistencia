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
        /// Lee una columna entera que admite NULL (division, grupo).
        /// Sin valor el dictado va a todo el curso; en el modelo eso es null,
        /// y la vista manda 0 en el NumericUpDown para el mismo significado.
        /// </summary>
        private static int? LeerEnteroOpcional(MySqlDataReader dr, string columna)
        {
            object valor = dr[columna];
            if (valor == null || DBNull.Value.Equals(valor)) return null;

            int numero = Convert.ToInt32(valor);

            return numero == 0 ? (int?)null : numero;
        }

        /// <summary>
        /// Convierte un registro obtenido desde la base de datos
        /// en un objeto Dictado.
        /// </summary>
        private Dictado MapearDictado(MySqlDataReader dr)
        {
            string fin = string.Empty;
            try
            {
                int o = dr.GetOrdinal("horario_fin");
                if (!dr.IsDBNull(o)) fin = Convert.ToString(dr["horario_fin"]);
            }
            catch (IndexOutOfRangeException) { }
            return new Dictado
            {
                IdDictado = Convert.ToInt32(dr["id_dictado"]),
                IdMateria = Convert.ToInt32(dr["id_materia"]),
                IdProfesor = Convert.ToInt32(dr["id_profesor"]),
                Dia = Convert.ToString(dr["dia"]),
                Horario = Convert.ToString(dr["horario"]),
                HorarioFin = fin,
                Division = LeerEnteroOpcional(dr, "division"),
                Grupo = LeerEnteroOpcional(dr, "grupo"),
                AnioLectivo = Convert.ToInt32(dr["anio_lectivo"]),
                IdPreceptor = LeerEnteroOpcional(dr, "id_preceptor"),
                AnioMateria = Convert.ToInt32(dr["anio_materia"]),
                NombreMateria = Convert.ToString(dr["nombre_materia"]),
                NombreEspecialidad = Convert.ToString(dr["nombre_especialidad"]),
                ApellidoProfesor = Convert.ToString(dr["apellido_profesor"]),
                Activo = LeerActivo(dr)
            };
        }

        private static bool LeerActivo(MySqlDataReader dr)
        {
            try
            {
                int o = dr.GetOrdinal("activo");
                if (!dr.IsDBNull(o)) return Convert.ToBoolean(dr["activo"]);
            }
            catch (IndexOutOfRangeException) { }
            return true;
        }

        public List<Dictado> ObtenerTodos()
        {
            List<Dictado> lista = new List<Dictado>();

            using (MySqlConnection cn = conexionBD.ObtenerConexion())
            {
                cn.Open();

                string sql = @"SELECT D.id_dictado, D.id_materia, D.id_profesor,
                                    D.dia, D.horario, D.horario_fin,
                                    D.division, D.grupo, D.anio_lectivo, D.id_preceptor, D.activo,
                                    M.nombre_materia, M.anio_materia, P.apellido_profesor,
                                    E.nombre_especialidad
                               FROM dictado D
                               INNER JOIN materia M
                                   ON M.id_materia = D.id_materia
                               INNER JOIN profesor P
                                    ON P.id_profesor = D.id_profesor
                               INNER JOIN especialidad E
                                   ON E.id_especialidad = M.id_especialidad
                               WHERE D.activo = 1
                               ORDER BY D.anio_lectivo, D.division, D.grupo,
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

        public List<Dictado> ObtenerTodosIncluyendoInactivos()
        {
            List<Dictado> lista = new List<Dictado>();

            using (MySqlConnection cn = conexionBD.ObtenerConexion())
            {
                cn.Open();

                string sql = @"SELECT D.id_dictado, D.id_materia, D.id_profesor,
                                    D.dia, D.horario, D.horario_fin,
                                    D.division, D.grupo, D.anio_lectivo, D.id_preceptor, D.activo,
                                    M.nombre_materia, M.anio_materia, P.apellido_profesor,
                                    E.nombre_especialidad
                               FROM dictado D
                               INNER JOIN materia M
                                   ON M.id_materia = D.id_materia
                               INNER JOIN profesor P
                                    ON P.id_profesor = D.id_profesor
                               INNER JOIN especialidad E
                                   ON E.id_especialidad = M.id_especialidad
                               ORDER BY D.anio_lectivo, D.division, D.grupo,
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

        public bool ExisteSolapamiento(int idProfesor, string dia, string inicio,
            string fin, int anioLectivo, int excluirIdDictado)
        {
            using (MySqlConnection cn = conexionBD.ObtenerConexion())
            {
                cn.Open();

                string sql = @"SELECT COUNT(*)
                                FROM dictado
                                WHERE id_profesor = @prof
                                  AND dia = @dia
                                  AND anio_lectivo = @anio
                                  AND activo = 1
                                  AND id_dictado <> @excluir
                                  AND TIME(horario) < @fin
                                  AND TIME(horario_fin) > @inicio";

                var cmd = new MySqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@prof", idProfesor);
                cmd.Parameters.AddWithValue("@dia", dia ?? string.Empty);
                cmd.Parameters.AddWithValue("@anio", anioLectivo);
                cmd.Parameters.AddWithValue("@excluir", excluirIdDictado);
                cmd.Parameters.AddWithValue("@inicio", inicio);
                cmd.Parameters.AddWithValue("@fin", fin);

                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }

        public bool Agregar(Dictado dictado)
        {
            using (MySqlConnection cn = conexionBD.ObtenerConexion())
            {
                cn.Open();

                string sql = @"INSERT INTO dictado
                                (id_materia, id_profesor, dia, horario, horario_fin, division, grupo, anio_lectivo, activo)
                                VALUES
                                (@id_materia, @id_profesor, @dia, @horario, @horario_fin, @division, @grupo, @anio_lectivo, @activo)";

                var cmd = new MySqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@id_materia", dictado.IdMateria);
                cmd.Parameters.AddWithValue("@id_profesor", dictado.IdProfesor);
                cmd.Parameters.AddWithValue("@dia", dictado.Dia);
                cmd.Parameters.AddWithValue("@horario", dictado.Horario);
                cmd.Parameters.AddWithValue("@horario_fin",
                    string.IsNullOrWhiteSpace(dictado.HorarioFin) ? (object)System.DBNull.Value : dictado.HorarioFin);
                cmd.Parameters.AddWithValue("@division",
                    dictado.Division.HasValue ? (object)dictado.Division.Value : System.DBNull.Value);
                cmd.Parameters.AddWithValue("@grupo", dictado.Grupo);
                cmd.Parameters.AddWithValue("@anio_lectivo", dictado.AnioLectivo);
                cmd.Parameters.AddWithValue("@activo", 1);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Modificar(Dictado dictado)
        {
            using (MySqlConnection cn = conexionBD.ObtenerConexion())
            {
                cn.Open();

                string sql = @"UPDATE dictado
                                SET id_materia = @id_materia,
                                    id_profesor = @id_profesor,
                                    dia = @dia,
                                    horario = @horario,
                                    horario_fin = @horario_fin,
                                    division = @division,
                                    grupo = @grupo,
                                    anio_lectivo = @anio_lectivo
                                WHERE id_dictado = @id";

                var cmd = new MySqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@id_materia", dictado.IdMateria);
                cmd.Parameters.AddWithValue("@id_profesor", dictado.IdProfesor);
                cmd.Parameters.AddWithValue("@dia", dictado.Dia);
                cmd.Parameters.AddWithValue("@horario", dictado.Horario);
                cmd.Parameters.AddWithValue("@horario_fin",
                    string.IsNullOrWhiteSpace(dictado.HorarioFin) ? (object)System.DBNull.Value : dictado.HorarioFin);
                cmd.Parameters.AddWithValue("@division",
                    dictado.Division.HasValue ? (object)dictado.Division.Value : System.DBNull.Value);
                cmd.Parameters.AddWithValue("@grupo", dictado.Grupo);
                cmd.Parameters.AddWithValue("@anio_lectivo", dictado.AnioLectivo);
                cmd.Parameters.AddWithValue("@id", dictado.IdDictado);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool DarDeBaja(int idDictado)
        {
            using (MySqlConnection cn = conexionBD.ObtenerConexion())
            {
                cn.Open();

                string sql = @"UPDATE dictado
                                SET activo = 0
                                WHERE id_dictado = @id";

                var cmd = new MySqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@id", idDictado);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool DarDeAlta(int idDictado)
        {
            using (MySqlConnection cn = conexionBD.ObtenerConexion())
            {
                cn.Open();

                string sql = @"UPDATE dictado
                                SET activo = 1
                                WHERE id_dictado = @id";

                var cmd = new MySqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@id", idDictado);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool AsignarPreceptor(int idDictado, int? idPreceptor)
        {
            using (MySqlConnection cn = conexionBD.ObtenerConexion())
            {
                cn.Open();

                string sql = @"UPDATE dictado
                                SET id_preceptor = @preceptor
                                WHERE id_dictado = @id";

                var cmd = new MySqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@preceptor",
                    idPreceptor.HasValue ? (object)idPreceptor.Value : System.DBNull.Value);
                cmd.Parameters.AddWithValue("@id", idDictado);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        /// <summary>
        /// Cuenta los elementos dependientes (alumnos inscriptos, clases y asistencias) 
        /// que se borrarían con una eliminación definitiva.
        /// </summary>
        public List<Dependencia> ObtenerDependencias(int idDictado)
        {
            List<Dependencia> dependencias = new List<Dependencia>();

            using (MySqlConnection cn = conexionBD.ObtenerConexion())
            {
                cn.Open();

                // 1. Alumnos inscriptos
                using (MySqlCommand cmd = new MySqlCommand(
                    "SELECT COUNT(*) FROM inscribe WHERE id_dictado = @id", cn))
                {
                    cmd.Parameters.AddWithValue("@id", idDictado);
                    int alumnos = Convert.ToInt32(cmd.ExecuteScalar());
                    dependencias.Add(new Dependencia("Alumnos inscriptos", alumnos));
                }

                // 2. Clases registradas
                using (MySqlCommand cmdClases = new MySqlCommand(
                    "SELECT COUNT(*) FROM clase WHERE id_dictado = @id", cn))
                {
                    cmdClases.Parameters.AddWithValue("@id", idDictado);
                    int clases = Convert.ToInt32(cmdClases.ExecuteScalar());
                    dependencias.Add(new Dependencia("Clases registradas", clases));
                }

                // 3. Asistencias asociadas a las clases del dictado
                using (MySqlCommand cmdAsistencias = new MySqlCommand(
                    @"SELECT COUNT(*) FROM asistencia A 
                      INNER JOIN clase C ON A.id_clase = C.id_clase 
                      WHERE C.id_dictado = @id", cn))
                {
                    cmdAsistencias.Parameters.AddWithValue("@id", idDictado);
                    int asistencias = Convert.ToInt32(cmdAsistencias.ExecuteScalar());
                    dependencias.Add(new Dependencia("Registros de asistencia", asistencias));
                }
            }

            return dependencias;
        }

        /// <summary>
        /// Elimina definitivamente el dictado, sus inscripciones, asistencias y clases vinculadas,
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
                        // 1. Borrar inscripciones de alumnos
                        using (MySqlCommand cmdInscripciones = new MySqlCommand(
                            "DELETE FROM inscribe WHERE id_dictado = @id", cn, tx))
                        {
                            cmdInscripciones.Parameters.AddWithValue("@id", idDictado);
                            cmdInscripciones.ExecuteNonQuery();
                        }

                        // 2. Borrar asistencias de las clases asociadas a este dictado
                        using (MySqlCommand cmdAsistencias = new MySqlCommand(
                            @"DELETE A FROM asistencia A 
                              INNER JOIN clase C ON A.id_clase = C.id_clase 
                              WHERE C.id_dictado = @id", cn, tx))
                        {
                            cmdAsistencias.Parameters.AddWithValue("@id", idDictado);
                            cmdAsistencias.ExecuteNonQuery();
                        }

                        // 3. Borrar clases asociadas al dictado
                        using (MySqlCommand cmdClases = new MySqlCommand(
                            "DELETE FROM clase WHERE id_dictado = @id", cn, tx))
                        {
                            cmdClases.Parameters.AddWithValue("@id", idDictado);
                            cmdClases.ExecuteNonQuery();
                        }

                        // 4. Borrar el dictado principal
                        using (MySqlCommand cmd = new MySqlCommand(
                            "DELETE FROM dictado WHERE id_dictado = @id", cn, tx))
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