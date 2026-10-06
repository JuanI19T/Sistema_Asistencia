using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SistemaAsistencia.Utilidades
{
    /// <summary>
    /// Mecánica común de carga de datos en las vistas (ABM).
    /// Centraliza el refresco de controles Material (que no siempre repintan
    /// en tiempo real), la suspensión de layout en operaciones por lotes y
    /// los diálogos estándar. Las reglas de negocio (validaciones,
    /// normalizaciones, columnas de grilla, mensajes propios) quedan en cada
    /// formulario; acá solo vive la mecánica compartida.
    /// </summary>
    public static class CargaVista
    {
        /// <summary>
        /// Ejecuta una acción con el layout suspendido para evitar parpadeos
        /// y estados intermedios. Siempre reanuda, incluso si falla.
        /// </summary>
        /// <param name="contenedor">Control o formulario a suspender.</param>
        /// <param name="accion">Trabajo a ejecutar.</param>
        public static void EjecutarConLayout(Control contenedor, Action accion)
        {
            if (accion == null) return;
            if (contenedor == null || contenedor.IsDisposed)
            {
                accion();
                return;
            }
            contenedor.SuspendLayout();
            try
            {
                accion();
            }
            finally
            {
                if (!contenedor.IsDisposed)
                    contenedor.ResumeLayout(true);
            }
        }

        /// <summary>
        /// Fuerza el repintado inmediato de un control. Los controles Material
        /// no siempre se actualizan en tiempo real ante cambios de datos;
        /// este refresco es lo que los pone al día.
        /// </summary>
        /// <param name="control">Control a refrescar.</param>
        public static void Refrescar(Control control)
        {
            if (control == null || control.IsDisposed) return;
            control.Invalidate();
            control.Refresh();
        }

        /// <summary>
        /// Pide una lista al controlador sin riesgo de null.
        /// </summary>
        /// <param name="obtener">Llamada al controlador.</param>
        /// <returns>La lista, o una vacía si vino null.</returns>
        public static List<T> ObtenerLista<T>(Func<List<T>> obtener)
        {
            if (obtener == null) return new List<T>();
            return obtener() ?? new List<T>();
        }

        /// <summary>
        /// Muestra una lista en la grilla y aplica la configuración de
        /// columnas propia de cada formulario. Refresca al final.
        /// </summary>
        /// <param name="dgv">Grilla destino.</param>
        /// <param name="datos">Datos a mostrar.</param>
        /// <param name="configurarColumnas">Ajuste de columnas del formulario (puede ser null).</param>
        public static void MostrarEnGrilla<T>(DataGridView dgv, IList<T> datos, Action<DataGridView> configurarColumnas)
        {
            if (dgv == null || dgv.IsDisposed) return;
            dgv.DataSource = null;
            dgv.DataSource = datos;
            if (configurarColumnas != null)
                configurarColumnas(dgv);
            Refrescar(dgv);
        }

        /// <summary>
        /// Carga un combo desde una lista de BD (DisplayMember/ValueMember),
        /// sin selección inicial y refrescado. Cada combo recibe su propia
        /// copia: dos combos con la misma instancia sincronizarían selección.
        /// </summary>
        public static void CargarCombo<T>(ComboBox combo, IList<T> datos, string displayMember, string valueMember)
        {
            if (combo == null || combo.IsDisposed) return;
            combo.DataSource = null;
            combo.DataSource = datos == null ? null : new List<T>(datos);
            if (!string.IsNullOrEmpty(displayMember))
                combo.DisplayMember = displayMember;
            if (!string.IsNullOrEmpty(valueMember))
                combo.ValueMember = valueMember;
            combo.SelectedIndex = -1;
            Refrescar(combo);
        }

        /// <summary>
        /// Carga un combo con items fijos (catálogos estáticos),
        /// sin selección inicial y refrescado.
        /// </summary>
        public static void CargarItems(ComboBox combo, params string[] items)
        {
            if (combo == null || combo.IsDisposed) return;
            combo.Items.Clear();
            if (items != null)
                combo.Items.AddRange(items);
            combo.SelectedIndex = -1;
            Refrescar(combo);
        }

        /// <summary>
        /// Selecciona en el combo el primer item que cumpla el predicado
        /// (cada formulario pasa su propia normalización). Refresca siempre.
        /// </summary>
        /// <returns>True si encontró y seleccionó un item.</returns>
        public static bool ElegirEnCombo(ComboBox combo, Predicate<object> coincide)
        {
            if (combo == null || combo.IsDisposed) return false;
            int indice = -1;
            if (coincide != null)
            {
                for (int i = 0; i < combo.Items.Count; i++)
                {
                    if (coincide(combo.Items[i]))
                    {
                        indice = i;
                        break;
                    }
                }
            }
            combo.SelectedIndex = indice;
            Refrescar(combo);
            return indice >= 0;
        }

        /// <summary>
        /// Deselecciona el combo y lo refresca (típico de los Limpiar).
        /// </summary>
        public static void ReiniciarCombo(ComboBox combo)
        {
            if (combo == null || combo.IsDisposed) return;
            combo.SelectedIndex = -1;
            Refrescar(combo);
        }

        /// <summary>
        /// Filtra la caché en memoria: sin texto muestra todo; con texto
        /// muestra coincidencias; si es interactivo, sin resultados avisa y
        /// limpia la edición, y con un único resultado lo carga directo.
        /// </summary>
        public static void FiltrarCache<T>(
            List<T> cache,
            string texto,
            Func<T, bool> coincide,
            Action<List<T>> mostrar,
            Action<T> cargarUnico,
            Action alVaciar,
            string titulo,
            bool interactivo)
        {
            if (mostrar == null) return;
            if (cache == null)
                cache = new List<T>();

            if (string.IsNullOrWhiteSpace(texto))
            {
                mostrar(cache);
                return;
            }

            List<T> resultados = coincide == null
                ? new List<T>(cache)
                : cache.Where(coincide).ToList();
            mostrar(resultados);

            if (!interactivo) return;

            if (resultados.Count == 0)
            {
                MessageBox.Show(
                    "Sin resultados para esa búsqueda.",
                    titulo,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                if (alVaciar != null)
                    alVaciar();
            }
            else if (resultados.Count == 1 && cargarUnico != null)
            {
                cargarUnico(resultados[0]);
            }
        }

        /// <summary>
        /// Ejecuta una carga inicial mostrando el error estándar de conexión.
        /// </summary>
        /// <returns>True si la carga terminó sin errores.</returns>
        public static bool IntentarCarga(Action cargar, string entidad)
        {
            try
            {
                if (cargar != null)
                    cargar();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo cargar " + entidad + ".\n" + ex.Message,
                    "Error de conexión",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
