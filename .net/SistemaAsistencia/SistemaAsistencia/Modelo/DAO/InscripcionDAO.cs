using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using SistemaAsistencia.Modelo.Conexion;
using SistemaAsistencia.Modelo.Entidades;

namespace SistemaAsistencia.Modelo.DAO
{
    /// <summary>
    /// Clase encargada del acceso a datos de la entidad Inscripcion.
    /// Gestiona los alumnos inscriptos en cada dictado (tabla INSCRIBE).
    /// </summary>
    public class InscripcionDAO
    {
        // Instancia de la conexión a la base de datos.
        private readonly ConexionBD conexionBD = new ConexionBD();

        /// <summary>
        /// Obtiene los ids de los alumnos inscriptos en un dictado.
        /// </summary>
        public List<int> ObtenerAlumnosInscriptos(int idDictado)
        {
            List<int> lista = new List<int>();

            using (MySqlConnection cn = conexionBD.ObtenerConexion())
            {
                cn.Open();

                string sql = @"SELECT id_alumno
                               FROM INSCRIBE
                               WHERE id_dictado = @id";

                var cmd = new MySqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@id", idDictado);

                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(Convert.ToInt32(dr["id_alumno"]));
                    }
                }
            }

            return lista;
        }

        /// <summary>
        /// Inscribe a un alumno en un dictado exclusivamente si no estava
        /// inscripto previamente (evita duplicados).
        /// </summary>
        public bool Agregar(Inscripcion inscripcion)
        {
            using (MySqlConnection cn = conexionBD.ObtenerConexion())
            {
                cn.Open();

                string sql = @"INSERT INTO INSCRIBE (id_alumno, id_dictado, anio_inicio)
                               SELECT @alumno, @dictado, @anio
                               FROM DUAL
                               WHERE NOT EXISTS (SELECT 1 FROM INSCRIBE
                                                 WHERE id_alumno = @alumno
                                                   AND id_dictado = @dictado)";

                var cmd = new MySqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@alumno", inscripcion.IdAlumno);
                cmd.Parameters.AddWithValue("@dictado", inscripcion.IdDictado);
                cmd.Parameters.AddWithValue("@anio", inscripcion.AnioInicio);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        /// <summary>
        /// Baja a un alumno de un dictado.
        /// </summary>
        public bool Eliminar(int idAlumno, int idDictado)
        {
            using (MySqlConnection cn = conexionBD.ObtenerConexion())
            {
                cn.Open();

                string sql = @"DELETE FROM INSCRIBE
                               WHERE id_alumno = @alumno
                                 AND id_dictado = @dictado";

                var cmd = new MySqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@alumno", idAlumno);
                cmd.Parameters.AddWithValue("@dictado", idDictado);

                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}