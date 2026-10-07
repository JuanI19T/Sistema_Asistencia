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
        /// Obtiene todos los alumnos, incluyendo inactivos (solo ABM).
        /// </summary>
        public List<Alumno> ObtenerAlumnosIncluyendoInactivos()
        {
            return Ejecutor.Ejecutar("Alumno.ObtenerAlumnosIncluyendoInactivos",
                () => alumnoDAO.ObtenerTodosIncluyendoInactivos());
        }

        private static void Validar(Alumno alumno)
        {
            if (alumno == null)
                throw new DatosException("Datos de alumno inválidos.");
            if (string.IsNullOrWhiteSpace(alumno.NombreAlumno) || alumno.NombreAlumno.Trim().Length < 2)
                throw new DatosException("Ingrese el nombre del alumno.");
            if (string.IsNullOrWhiteSpace(alumno.ApellidoAlumno) || alumno.ApellidoAlumno.Trim().Length < 2)
                throw new DatosException("Ingrese el apellido del alumno.");
            alumno.DniAlumno = SeguridadAcceso.ValidarDni(alumno.DniAlumno);
            string legajo = (alumno.LegajoAlumno ?? string.Empty).Trim();
            if (legajo.Length == 0)
                throw new DatosException("Ingrese el legajo del alumno.");
            if (!SeguridadAcceso.SoloDigitos(legajo) || legajo.Length > 10)
                throw new DatosException("El legajo debe ser numérico de hasta 10 dígitos (lo escriben ustedes).");
            alumno.LegajoAlumno = legajo;
            string correo = (alumno.CorreoAlumno ?? string.Empty).Trim();
            if (correo.Length > 0 && (!correo.Contains("@") || !correo.Contains(".")))
                throw new DatosException("El correo no tiene un formato válido.");
            alumno.NombreAlumno = alumno.NombreAlumno.Trim();
            alumno.ApellidoAlumno = alumno.ApellidoAlumno.Trim();
            alumno.CorreoAlumno = correo;
        }

        /// <summary>
        /// Agrega un nuevo alumno.
        /// </summary>
        public bool AgregarAlumno(Alumno alumno)
        {
            Validar(alumno);
            try
            {
                return Ejecutor.Ejecutar("Alumno.AgregarAlumno",
                    () => alumnoDAO.Agregar(alumno));
            }
            catch (DatosException ex) when (ex.InnerException != null &&
                ex.InnerException.Message.Contains("Duplicate"))
            {
                throw new DatosException("Ya existe un alumno con ese DNI o legajo (deben ser únicos).");
            }
        }

        /// <summary>
        /// Modifica los datos de un alumno existente.
        /// </summary>
        public bool ModificarAlumno(Alumno alumno)
        {
            Validar(alumno);
            try
            {
                return Ejecutor.Ejecutar("Alumno.ModificarAlumno",
                    () => alumnoDAO.Modificar(alumno));
            }
            catch (DatosException ex) when (ex.InnerException != null &&
                ex.InnerException.Message.Contains("Duplicate"))
            {
                throw new DatosException("Ya existe otro alumno con ese DNI o legajo (deben ser únicos).");
            }
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
        /// Solo Administrador y Directivo.
        /// </summary>
        public bool DarDeBaja(int idAlumno)
        {
            if (!Sesion.PuedeGestionarActivo())
                throw new DatosException("Solo el administrador o directivo puede dar de baja.");
            return Ejecutor.Ejecutar("Alumno.DarDeBaja",
                () => alumnoDAO.DarDeBaja(idAlumno));
        }

        /// <summary>
        /// Da de alta a un alumno (reverso de la baja logica).
        /// Solo Administrador y Directivo.
        /// </summary>
        public bool DarDeAlta(int idAlumno)
        {
            if (!Sesion.PuedeGestionarActivo())
                throw new DatosException("Solo el administrador o directivo puede dar de alta.");
            return Ejecutor.Ejecutar("Alumno.DarDeAlta",
                () => alumnoDAO.DarDeAlta(idAlumno));
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