using System.Collections.Generic;
using SistemaAsistencia.Modelo.DAO;
using SistemaAsistencia.Modelo.Entidades;
using SistemaAsistencia.Utilidades;

namespace SistemaAsistencia.Controlador
{
    /// <summary>
    /// Controlador encargado de gestionar las operaciones relacionadas
    /// con los alumnos del sistema.
    /// </summary>
    public class AlumnoController
    {
        private readonly AlumnoDAO alumnoDAO;

        /// <summary>
        /// Constructor del controlador.
        /// Inicializa la conexión con la capa de datos.
        /// </summary>
        public AlumnoController()
        {
            alumnoDAO = new AlumnoDAO();
        }

        /// <summary>
        /// Obtiene todos los alumnos registrados.
        /// </summary>
        public List<Alumno> ObtenerAlumnos()
        {
            return Ejecutor.Ejecutar("Alumno.ObtenerAlumnos",
                () => alumnoDAO.ObtenerTodos());
        }

        /// <summary>
        /// Agrega un nuevo alumno.
        /// </summary>
        public bool AgregarAlumno(Alumno alumno)
        {
            return Ejecutor.Ejecutar("Alumno.AgregarAlumno",
                () => alumnoDAO.Agregar(alumno));
        }

        /// <summary>
        /// Modifica los datos de un alumno existente.
        /// </summary>
        public bool ModificarAlumno(Alumno alumno)
        {
            return Ejecutor.Ejecutar("Alumno.ModificarAlumno",
                () => alumnoDAO.Modificar(alumno));
        }

        /// <summary>
        /// Devuelve los registros vinculados al alumno (para informar
        /// antes de una eliminación definitiva).
        /// </summary>
        public List<Dependencia> ObtenerDependencias(int idAlumno)
        {
            return Ejecutor.Ejecutar("Alumno.ObtenerDependencias",
                () => alumnoDAO.ObtenerDependencias(idAlumno));
        }

        /// <summary>
        /// Da de baja lógica a un alumno (conserva su histórico).
        /// </summary>
        public bool DarDeBaja(int idAlumno)
        {
            return Ejecutor.Ejecutar("Alumno.DarDeBaja",
                () => alumnoDAO.DarDeBaja(idAlumno));
        }

        /// <summary>
        /// Elimina definitivamente al alumno y sus registros vinculados.
        /// Solo disponible para el rol Administrador.
        /// </summary>
        public bool EliminarDefinitivo(int idAlumno)
        {
            if (!Sesion.UsuarioActualRolAdministrador())
            {
                throw new DatosException("Solo el administrador puede eliminar registros definitivamente.");
            }

            return Ejecutor.Ejecutar("Alumno.EliminarDefinitivo",
                () => alumnoDAO.EliminarDefinitivo(idAlumno));
        }
    }
}