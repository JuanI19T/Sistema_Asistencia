using System.Collections.Generic;
using SistemaAsistencia.Modelo.DAO;
using SistemaAsistencia.Modelo.Entidades;
using SistemaAsistencia.Utilidades;

namespace SistemaAsistencia.Controlador
{
    /// <summary>
    /// Controlador encargado de las consultas de especialidades.
    /// </summary>
    public class EspecialidadController
    {
        private readonly EspecialidadDAO especialidadDAO;

        /// <summary>
        /// Constructor del controlador.
        /// Inicializa la conexión con la capa de datos.
        /// </summary>
        public EspecialidadController()
        {
            especialidadDAO = new EspecialidadDAO();
        }

        /// <summary>
        /// Obtiene todas las especialidades registradas.
        /// </summary>
        public List<Especialidad> ObtenerEspecialidades()
        {
            return Ejecutor.Ejecutar("Especialidad.ObtenerEspecialidades",
                () => especialidadDAO.ObtenerTodas());
        }

        // Catálogo fijo (Ciclo Básico + 5 tecnicaturas): no se crea ni se renombra a mano.
        // El ABM existe por normalización pero opera en solo-lectura.
        private static void BloquearEdicion()
        {
            throw new DatosException(
                "Catálogo fijo (Ciclo Básico + 5 tecnicaturas): no se puede crear ni modificar a mano.");
        }

        /// <summary>
        /// Agrega una nueva especialidad.
        /// </summary>
        public bool AgregarEspecialidad(Especialidad especialidad)
        {
            BloquearEdicion();
            return false;
        }

        /// <summary>
        /// Modifica los datos de una especialidad existente.
        /// </summary>
        public bool ModificarEspecialidad(Especialidad especialidad)
        {
            BloquearEdicion();
            return false;
        }

        /// <summary>
        /// Devuelve los registros vinculados a la especialidad (para informar
        /// antes de una eliminación definitiva).
        /// </summary>
        public List<Dependencia> ObtenerDependencias(int idEspecialidad)
        {
            return Ejecutor.Ejecutar("Especialidad.ObtenerDependencias",
                () => especialidadDAO.ObtenerDependencias(idEspecialidad));
        }

        /// <summary>
        /// Da de baja lógica a una especialidad (conserva su histórico).
        /// </summary>
        public bool DarDeBaja(int idEspecialidad)
        {
            BloquearEdicion();
            return false;
        }

        /// <summary>
        /// Elimina definitivamente la especialidad y sus registros vinculados.
        /// Solo disponible para el rol Administrador.
        /// </summary>
        public bool EliminarDefinitivo(int idEspecialidad)
        {
            BloquearEdicion();
            return false;
        }
    }
}