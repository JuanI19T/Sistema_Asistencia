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
        /// Agrega una nueva materia.
        /// </summary>
        public bool AgregarMateria(Materia materia)
        {
            return Ejecutor.Ejecutar("Materia.AgregarMateria",
                () => materiaDAO.Agregar(materia));
        }

        /// <summary>
        /// Modifica los datos de una materia existente.
        /// </summary>
        public bool ModificarMateria(Materia materia)
        {
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
        /// </summary>
        public bool DarDeBaja(int idMateria)
        {
            return Ejecutor.Ejecutar("Materia.DarDeBaja",
                () => materiaDAO.DarDeBaja(idMateria));
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