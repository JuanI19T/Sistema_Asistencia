using System.Collections.Generic;
using SistemaAsistencia.Modelo.DAO;
using SistemaAsistencia.Modelo.Entidades;
using SistemaAsistencia.Utilidades;

namespace SistemaAsistencia.Controlador
{
    /// <summary>
    /// Controlador encargado de gestionar las inscripciones
    /// de alumnos en los dictados.
    /// </summary>
    public class InscripcionController
    {
        private readonly InscripcionDAO inscripcionDAO;

        /// <summary>
        /// Constructor del controlador.
        /// Inicializa la conexión con la capa de datos.
        /// </summary>
        public InscripcionController()
        {
            inscripcionDAO = new InscripcionDAO();
        }

        /// <summary>
        /// Obtiene los ids de los alumnos inscriptos en un dictado.
        /// </summary>
        public List<int> ObtenerAlumnosInscriptos(int idDictado)
        {
            return Ejecutor.Ejecutar("Inscripcion.ObtenerAlumnosInscriptos",
                () => inscripcionDAO.ObtenerAlumnosInscriptos(idDictado));
        }

        /// <summary>
        /// Inscribe a un alumno en un dictado.
        /// </summary>
        public bool AgregarInscripcion(int idAlumno, int idDictado, int anioInicio)
        {
            return Ejecutor.Ejecutar("Inscripcion.AgregarInscripcion", () =>
            {
                Inscripcion inscripcion = new Inscripcion();

                inscripcion.IdAlumno = idAlumno;
                inscripcion.IdDictado = idDictado;
                inscripcion.AnioInicio = anioInicio;

                return inscripcionDAO.Agregar(inscripcion);
            });
        }

        /// <summary>
        /// Baja a un alumno de un dictado.
        /// </summary>
        public bool EliminarInscripcion(int idAlumno, int idDictado)
        {
            return Ejecutor.Ejecutar("Inscripcion.EliminarInscripcion",
                () => inscripcionDAO.Eliminar(idAlumno, idDictado));
        }
    }
}