using System.Collections.Generic;
using SistemaAsistencia.Modelo.DAO;
using SistemaAsistencia.Modelo.Entidades;
using SistemaAsistencia.Utilidades;

namespace SistemaAsistencia.Controlador
{
    /// <summary>
    /// Controlador encargado de gestionar las operaciones relacionadas
    /// con los profesores del sistema.
    /// </summary>
    public class ProfesorController
    {
        private readonly ProfesorDAO profesorDAO;

        /// <summary>
        /// Constructor del controlador.
        /// Inicializa la conexión con la capa de datos.
        /// </summary>
        public ProfesorController()
        {
            profesorDAO = new ProfesorDAO();
        }

        /// <summary>
        /// Obtiene todos los profesores registrados.
        /// </summary>
        public List<Profesor> ObtenerProfesores()
        {
            return Ejecutor.Ejecutar("Profesor.ObtenerProfesores",
                () => profesorDAO.ObtenerTodos());
        }

        /// <summary>
        /// Obtiene todos los profesores, incluyendo inactivos (solo ABM).
        /// </summary>
        public List<Profesor> ObtenerProfesoresIncluyendoInactivos()
        {
            return Ejecutor.Ejecutar("Profesor.ObtenerProfesoresIncluyendoInactivos",
                () => profesorDAO.ObtenerTodosIncluyendoInactivos());
        }

        private static void Validar(Profesor profesor)
        {
            if (profesor == null)
                throw new DatosException("Datos de profesor inválidos.");
            if (string.IsNullOrWhiteSpace(profesor.NombreProfesor) || profesor.NombreProfesor.Trim().Length < 2)
                throw new DatosException("Ingrese el nombre del profesor.");
            if (string.IsNullOrWhiteSpace(profesor.ApellidoProfesor) || profesor.ApellidoProfesor.Trim().Length < 2)
                throw new DatosException("Ingrese el apellido del profesor.");
            string dni = SeguridadAcceso.ValidarDni(profesor.DniProfesor);
            profesor.DniProfesor = dni;
            // Regla EEST: el legajo del personal se autocompleta con el DNI.
            profesor.LegajoProfesor = dni;
            string correo = (profesor.CorreoProfesor ?? string.Empty).Trim();
            if (correo.Length > 0 && (!correo.Contains("@") || !correo.Contains(".")))
                throw new DatosException("El correo no tiene un formato válido.");
            profesor.NombreProfesor = profesor.NombreProfesor.Trim();
            profesor.ApellidoProfesor = profesor.ApellidoProfesor.Trim();
            profesor.CorreoProfesor = correo;
        }

        /// <summary>
        /// Agrega un nuevo profesor.
        /// </summary>
        public bool AgregarProfesor(Profesor profesor)
        {
            Validar(profesor);
            try
            {
                return Ejecutor.Ejecutar("Profesor.AgregarProfesor",
                    () => profesorDAO.Agregar(profesor));
            }
            catch (DatosException ex) when (ex.InnerException != null &&
                ex.InnerException.Message.Contains("Duplicate"))
            {
                throw new DatosException("Ya existe un profesor con ese DNI o legajo (deben ser únicos).");
            }
        }

        /// <summary>
        /// Modifica los datos de un profesor existente.
        /// </summary>
        public bool ModificarProfesor(Profesor profesor)
        {
            Validar(profesor);
            try
            {
                return Ejecutor.Ejecutar("Profesor.ModificarProfesor",
                    () => profesorDAO.Modificar(profesor));
            }
            catch (DatosException ex) when (ex.InnerException != null &&
                ex.InnerException.Message.Contains("Duplicate"))
            {
                throw new DatosException("Ya existe otro profesor con ese DNI o legajo (deben ser únicos).");
            }
        }

        /// <summary>
        /// Devuelve los registros vinculados al profesor (para informar
        /// antes de una eliminación definitiva).
        /// </summary>
        public List<Dependencia> ObtenerDependencias(int idProfesor)
        {
            return Ejecutor.Ejecutar("Profesor.ObtenerDependencias",
                () => profesorDAO.ObtenerDependencias(idProfesor));
        }

        /// <summary>
        /// Da de baja lógica a un profesor (conserva su histórico).
        /// Solo Administrador y Directivo.
        /// </summary>
        public bool DarDeBaja(int idProfesor)
        {
            if (!Sesion.PuedeGestionarActivo())
                throw new DatosException("Solo el administrador o directivo puede dar de baja.");
            return Ejecutor.Ejecutar("Profesor.DarDeBaja",
                () => profesorDAO.DarDeBaja(idProfesor));
        }

        /// <summary>
        /// Da de alta a un profesor (reverso de la baja logica).
        /// Solo Administrador y Directivo.
        /// </summary>
        public bool DarDeAlta(int idProfesor)
        {
            if (!Sesion.PuedeGestionarActivo())
                throw new DatosException("Solo el administrador o directivo puede dar de alta.");
            return Ejecutor.Ejecutar("Profesor.DarDeAlta",
                () => profesorDAO.DarDeAlta(idProfesor));
        }

        /// <summary>
        /// Elimina definitivamente al profesor y sus registros vinculados.
        /// Solo disponible para el rol Administrador.
        /// </summary>
        public bool EliminarDefinitivo(int idProfesor)
        {
            if (!Sesion.UsuarioActualRolAdministrador())
            {
                throw new DatosException("Solo el administrador puede eliminar registros definitivamente.");
            }

            return Ejecutor.Ejecutar("Profesor.EliminarDefinitivo",
                () => profesorDAO.EliminarDefinitivo(idProfesor));
        }
    }
}