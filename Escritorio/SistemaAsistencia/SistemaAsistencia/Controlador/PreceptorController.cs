using System.Collections.Generic;
using SistemaAsistencia.Modelo.DAO;
using SistemaAsistencia.Modelo.Entidades;
using SistemaAsistencia.Utilidades;

namespace SistemaAsistencia.Controlador
{
    /// <summary>
    /// Controlador encargado de gestionar las operaciones relacionadas
    /// con los preceptores del sistema.
    /// </summary>
    public class PreceptorController
    {
        private readonly PreceptorDAO preceptorDAO;

        /// <summary>
        /// Constructor del controlador.
        /// Inicializa la conexión con la capa de datos.
        /// </summary>
        public PreceptorController()
        {
            preceptorDAO = new PreceptorDAO();
        }

        /// <summary>
        /// Obtiene todos los preceptores registrados.
        /// </summary>
        public List<Preceptor> ObtenerPreceptores()
        {
            return Ejecutor.Ejecutar("Preceptor.ObtenerPreceptores",
                () => preceptorDAO.ObtenerTodos());
        }

        // Regla EEST: el legajo del personal se autocompleta con el DNI.
        private static void NormalizarLegajo(Preceptor preceptor)
        {
            if (preceptor == null)
                throw new DatosException("Datos de preceptor inválidos.");
            preceptor.Dni = (preceptor.Dni ?? string.Empty).Trim();
            preceptor.LegajoPreceptor = preceptor.Dni;
        }

        /// <summary>
        /// Agrega un nuevo preceptor.
        /// </summary>
        public bool AgregarPreceptor(Preceptor preceptor)
        {
            NormalizarLegajo(preceptor);
            return Ejecutor.Ejecutar("Preceptor.AgregarPreceptor",
                () => preceptorDAO.Agregar(preceptor));
        }

        /// <summary>
        /// Modifica los datos de un preceptor existente.
        /// </summary>
        public bool ModificarPreceptor(Preceptor preceptor)
        {
            NormalizarLegajo(preceptor);
            return Ejecutor.Ejecutar("Preceptor.ModificarPreceptor",
                () => preceptorDAO.Modificar(preceptor));
        }

        /// <summary>
        /// Devuelve los registros vinculados al preceptor (para informar
        /// antes de una eliminación definitiva).
        /// </summary>
        public List<Dependencia> ObtenerDependencias(int idPreceptor)
        {
            return Ejecutor.Ejecutar("Preceptor.ObtenerDependencias",
                () => preceptorDAO.ObtenerDependencias(idPreceptor));
        }

        /// <summary>
        /// Da de baja lógica a un preceptor (conserva su histórico).
        /// </summary>
        public bool DarDeBaja(int idPreceptor)
        {
            return Ejecutor.Ejecutar("Preceptor.DarDeBaja",
                () => preceptorDAO.DarDeBaja(idPreceptor));
        }

        /// <summary>
        /// Elimina definitivamente al preceptor y sus registros vinculados.
        /// Solo disponible para el rol Administrador.
        /// </summary>
        public bool EliminarDefinitivo(int idPreceptor)
        {
            if (!Sesion.UsuarioActualRolAdministrador())
            {
                throw new DatosException("Solo el administrador puede eliminar registros definitivamente.");
            }

            return Ejecutor.Ejecutar("Preceptor.EliminarDefinitivo",
                () => preceptorDAO.EliminarDefinitivo(idPreceptor));
        }
    }
}