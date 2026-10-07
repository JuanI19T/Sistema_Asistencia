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

        /// <summary>
        /// Obtiene todas las especialidades, incluyendo inactivas (solo ABM).
        /// </summary>
        public List<Especialidad> ObtenerEspecialidadesIncluyendoInactivos()
        {
            return Ejecutor.Ejecutar("Especialidad.ObtenerEspecialidadesIncluyendoInactivos",
                () => especialidadDAO.ObtenerTodasIncluyendoInactivos());
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
        /// Cambia la división fija de una especialidad (única edición
        /// permitida sobre el catálogo fijo). NULL = libre.
        /// </summary>
        public bool ModificarDivision(int idEspecialidad, int? division)
        {
            if (division.HasValue && (division.Value < 1 || division.Value > 6))
                throw new DatosException("La división debe estar entre 1 y 6 (o libre).");
            return Ejecutor.Ejecutar("Especialidad.ModificarDivision",
                () => especialidadDAO.ModificarDivision(idEspecialidad, division));
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
        /// Solo Administrador y Directivo. Crear y renombrar siguen
        /// bloqueados por BloquearEdicion (catalogo fijo).
        /// </summary>
        public bool DarDeBaja(int idEspecialidad)
        {
            if (!Sesion.PuedeGestionarActivo())
                throw new DatosException("Solo el administrador o directivo puede dar de baja.");
            return Ejecutor.Ejecutar("Especialidad.DarDeBaja",
                () => especialidadDAO.DarDeBaja(idEspecialidad));
        }

        /// <summary>
        /// Da de alta a una especialidad (reverso de la baja logica).
        /// Solo Administrador y Directivo.
        /// </summary>
        public bool DarDeAlta(int idEspecialidad)
        {
            if (!Sesion.PuedeGestionarActivo())
                throw new DatosException("Solo el administrador o directivo puede dar de alta.");
            return Ejecutor.Ejecutar("Especialidad.DarDeAlta",
                () => especialidadDAO.DarDeAlta(idEspecialidad));
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