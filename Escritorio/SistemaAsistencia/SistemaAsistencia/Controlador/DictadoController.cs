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
        /// Rangos globales de división y grupo. Son el sobre máximo: el
        /// rango fino de división depende del ciclo (Ciclo Básico 1-7,
        /// Tecnicaturas 1-6) y ese lo valida la vista, que conoce la
        /// materia seleccionada. Grupo: 1-2 en todos los casos
        /// (0/NULL = todo el curso o toda la división). Acá se defienden
        /// los límites que MySQL impone y la coherencia entre los dos campos.
        /// </summary>
        private const int DivisionMaxima = 7;
        private const int GrupoMaximo = 2;

        // La hora de fin es obligatoria y posterior al inicio.
        private static void Validar(Dictado dictado)
        {
            if (dictado == null)
                throw new DatosException("Datos de dictado inválidos.");
            if (!System.TimeSpan.TryParse(dictado.Horario, out System.TimeSpan inicio))
                throw new DatosException("Horario de inicio inválido. Ejemplo: 08:00");
            if (!System.TimeSpan.TryParse(dictado.HorarioFin, out System.TimeSpan fin))
                throw new DatosException("Horario de fin inválido. Ejemplo: 10:00");
            if (fin <= inicio)
                throw new DatosException("La hora de fin debe ser posterior a la de inicio.");

            // Sin división y sin grupo el dictado es de todo el curso: válido.
            // Con división, el grupo es opcional (toda la división).
            // Con grupo, la división es obligatoria: un grupo suelto no existe.
            if (dictado.Grupo.HasValue && !dictado.Division.HasValue)
                throw new DatosException("Si indicás un grupo, tenés que indicar también la división.");
            if (dictado.Division.HasValue
                && (dictado.Division.Value < 1 || dictado.Division.Value > DivisionMaxima))
                throw new DatosException("La división debe estar entre 1 y " + DivisionMaxima + ".");
            if (dictado.Grupo.HasValue
                && (dictado.Grupo.Value < 1 || dictado.Grupo.Value > GrupoMaximo))
                throw new DatosException("El grupo debe estar entre 1 y " + GrupoMaximo + ".");
        }

        /// <summary>
        /// Agrega un nuevo dictado.
        /// </summary>
        public bool AgregarDictado(Dictado dictado)
        {
            Validar(dictado);
            return Ejecutor.Ejecutar("Dictado.AgregarDictado",
                () => dictadoDAO.Agregar(dictado));
        }

        /// <summary>
        /// Modifica los datos de un dictado existente.
        /// </summary>
        public bool ModificarDictado(Dictado dictado)
        {
            Validar(dictado);
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