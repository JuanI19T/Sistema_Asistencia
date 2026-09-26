using System.Collections.Generic;
using SistemaAsistencia.Modelo.DAO;
using SistemaAsistencia.Modelo.Entidades;
using SistemaAsistencia.Utilidades;

namespace SistemaAsistencia.Controlador
{
    /// <summary>
    /// Controlador encargado de gestionar las operaciones relacionadas
    /// con los dictados del sistema.
    /// </summary>
    public class DictadoController
    {
        private readonly DictadoDAO dictadoDAO;

        /// <summary>
        /// Constructor del controlador.
        /// Inicializa la conexión con la capa de datos.
        /// </summary>
        public DictadoController()
        {
            dictadoDAO = new DictadoDAO();
        }

        /// <summary>
        /// Obtiene todos los dictados registrados.
        /// </summary>
        public List<Dictado> ObtenerDictados()
        {
            return Ejecutor.Ejecutar("Dictado.ObtenerDictados",
                () => dictadoDAO.ObtenerTodos());
        }

        /// <summary>
        /// Agrega un nuevo dictado.
        /// </summary>
        public bool AgregarDictado(Dictado dictado)
        {
            return Ejecutor.Ejecutar("Dictado.AgregarDictado",
                () => dictadoDAO.Agregar(dictado));
        }

        /// <summary>
        /// Modifica los datos de un dictado existente.
        /// </summary>
        public bool ModificarDictado(Dictado dictado)
        {
            return Ejecutor.Ejecutar("Dictado.ModificarDictado",
                () => dictadoDAO.Modificar(dictado));
        }

        /// <summary>
        /// Devuelve los registros vinculados al dictado (para informar
        /// antes de una eliminación definitiva).
        /// </summary>
        public List<Dependencia> ObtenerDependencias(int idDictado)
        {
            return Ejecutor.Ejecutar("Dictado.ObtenerDependencias",
                () => dictadoDAO.ObtenerDependencias(idDictado));
        }

        /// <summary>
        /// Da de baja lógica a un dictado (conserva su histórico).
        /// </summary>
        public bool DarDeBaja(int idDictado)
        {
            return Ejecutor.Ejecutar("Dictado.DarDeBaja",
                () => dictadoDAO.DarDeBaja(idDictado));
        }

        /// <summary>
        /// Elimina definitivamente el dictado y sus registros vinculados.
        /// Solo disponible para el rol Administrador.
        /// </summary>
        public bool EliminarDefinitivo(int idDictado)
        {
            if (!Sesion.UsuarioActualRolAdministrador())
            {
                throw new DatosException("Solo el administrador puede eliminar registros definitivamente.");
            }

            return Ejecutor.Ejecutar("Dictado.EliminarDefinitivo",
                () => dictadoDAO.EliminarDefinitivo(idDictado));
        }
    }
}