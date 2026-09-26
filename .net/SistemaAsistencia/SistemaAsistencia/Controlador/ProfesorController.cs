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
        /// Agrega un nuevo profesor.
        /// </summary>
        public bool AgregarProfesor(Profesor profesor)
        {
            return Ejecutor.Ejecutar("Profesor.AgregarProfesor",
                () => profesorDAO.Agregar(profesor));
        }

        /// <summary>
        /// Modifica los datos de un profesor existente.
        /// </summary>
        public bool ModificarProfesor(Profesor profesor)
        {
            return Ejecutor.Ejecutar("Profesor.ModificarProfesor",
                () => profesorDAO.Modificar(profesor));
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
        /// </summary>
        public bool DarDeBaja(int idProfesor)
        {
            return Ejecutor.Ejecutar("Profesor.DarDeBaja",
                () => profesorDAO.DarDeBaja(idProfesor));
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