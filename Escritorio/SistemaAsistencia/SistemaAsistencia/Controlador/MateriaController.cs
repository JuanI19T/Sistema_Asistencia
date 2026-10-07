using System.Collections.Generic;
using SistemaAsistencia.Modelo.DAO;
using SistemaAsistencia.Modelo.Entidades;
using SistemaAsistencia.Utilidades;

namespace SistemaAsistencia.Controlador
{
    /// <summary>
    /// Controlador encargado de gestionar las operaciones relacionadas
    /// con las materias del sistema.
    /// </summary>
    public class MateriaController
    {
        private readonly MateriaDAO materiaDAO;

        /// <summary>
        /// Constructor del controlador.
        /// Inicializa la conexión con la capa de datos.
        /// </summary>
        public MateriaController()
        {
            materiaDAO = new MateriaDAO();
        }

        /// <summary>
        /// Obtiene todas las materias registradas.
        /// </summary>
        public List<Materia> ObtenerMaterias()
        {
            return Ejecutor.Ejecutar("Materia.ObtenerMaterias",
                () => materiaDAO.ObtenerTodos());
        }

        /// <summary>
        /// Obtiene todas las materias, incluyendo inactivas (solo ABM).
        /// </summary>
        public List<Materia> ObtenerMateriasIncluyendoInactivos()
        {
            return Ejecutor.Ejecutar("Materia.ObtenerMateriasIncluyendoInactivos",
                () => materiaDAO.ObtenerTodosIncluyendoInactivos());
        }

        // La EEST tiene 7 años: el NumericUpDown limita a 1-7,
        // esto es defensa por si se escribe el valor a mano.
        private static void Validar(Materia materia)
        {
            if (materia == null)
                throw new DatosException("Datos de materia inválidos.");
            if (materia.AnioMateria < 1 || materia.AnioMateria > 7)
                throw new DatosException("El año debe estar entre 1 y 7.");
        }

        /// <summary>
        /// Agrega una nueva materia.
        /// </summary>
        public bool AgregarMateria(Materia materia)
        {
            Validar(materia);
            return Ejecutor.Ejecutar("Materia.AgregarMateria",
                () => materiaDAO.Agregar(materia));
        }

        /// <summary>
        /// Modifica los datos de una materia existente.
        /// </summary>
        public bool ModificarMateria(Materia materia)
        {
            Validar(materia);
            return Ejecutor.Ejecutar("Materia.ModificarMateria",
                () => materiaDAO.Modificar(materia));
        }

        /// <summary>
        /// Devuelve los registros vinculados a la materia (para informar
        /// antes de una eliminación definitiva).
        /// </summary>
        public List<Dependencia> ObtenerDependencias(int idMateria)
        {
            return Ejecutor.Ejecutar("Materia.ObtenerDependencias",
                () => materiaDAO.ObtenerDependencias(idMateria));
        }

        /// <summary>
        /// Da de baja lógica a una materia (conserva su histórico).
        /// Solo Administrador y Directivo.
        /// </summary>
        public bool DarDeBaja(int idMateria)
        {
            if (!Sesion.PuedeGestionarActivo())
                throw new DatosException("Solo el administrador o directivo puede dar de baja.");
            return Ejecutor.Ejecutar("Materia.DarDeBaja",
                () => materiaDAO.DarDeBaja(idMateria));
        }

        /// <summary>
        /// Da de alta a una materia (reverso de la baja logica).
        /// Solo Administrador y Directivo.
        /// </summary>
        public bool DarDeAlta(int idMateria)
        {
            if (!Sesion.PuedeGestionarActivo())
                throw new DatosException("Solo el administrador o directivo puede dar de alta.");
            return Ejecutor.Ejecutar("Materia.DarDeAlta",
                () => materiaDAO.DarDeAlta(idMateria));
        }

        /// <summary>
        /// Elimina definitivamente la materia y sus registros vinculados.
        /// Solo disponible para el rol Administrador.
        /// </summary>
        public bool EliminarDefinitivo(int idMateria)
        {
            if (!Sesion.UsuarioActualRolAdministrador())
            {
                throw new DatosException("Solo el administrador puede eliminar registros definitivamente.");
            }

            return Ejecutor.Ejecutar("Materia.EliminarDefinitivo",
                () => materiaDAO.EliminarDefinitivo(idMateria));
        }
    }
}