using System;
using System.Windows.Forms;

namespace SistemaAsistencia.Utilidades
{
    /// <summary>
    /// Boton Activar/Desactivar generico (mismo comportamiento que el
    /// toggle de inscripciones en FrmInscripcion): lee si lo seleccionado
    /// esta activo y muestra "Desactivar" si lo esta, "Activar" si no.
    /// Trabaja contra Control para servir a Button y MaterialButton.
    /// No toca BD: la accion llega por delegados del controlador.
    /// </summary>
    public static class EstadoActivoHelper
    {
        public const string TextoActivar = "Activar";
        public const string TextoDesactivar = "Desactivar";
        public const string TextoNeutro = "Seleccione";

        /// <summary>
        /// Texto segun estado: activo muestra "Desactivar", inactivo "Activar".
        /// </summary>
        public static string TextoPara(bool activo)
        {
            return activo ? TextoDesactivar : TextoActivar;
        }

        /// <summary>
        /// Actualiza el texto del boton segun el seleccionado.
        /// Sin seleccion (null) deja el texto neutro.
        /// </summary>
        public static void ActualizarTexto(Control boton, bool? activo, string neutro = TextoNeutro)
        {
            if (boton == null || boton.IsDisposed) return;
            if (!activo.HasValue)
                boton.Text = neutro;
            else
                boton.Text = TextoPara(activo.Value);
        }

        /// <summary>
        /// Oculta el boton a quienes no pueden dar de alta/baja
        /// (solo Administrador y Directivo lo ven).
        /// </summary>
        public static void AplicarPermiso(Control boton)
        {
            if (boton == null || boton.IsDisposed) return;
            boton.Visible = Sesion.PuedeGestionarActivo();
        }

        /// <summary>
        /// Ejecuta el toggle: si esta activo da de baja, si no da de alta.
        /// Muestra los mensajes y retorna true si la operacion tuvo exito.
        /// </summary>
        public static bool EjecutarToggle(
            int id,
            bool activoActual,
            Func<int, bool> darDeBaja,
            Func<int, bool> darDeAlta,
            string nombreEntidad)
        {
            if (id == 0)
            {
                MessageBox.Show(
                    "Busque y seleccione un registro primero.",
                    nombreEntidad,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }

            if (!Sesion.PuedeGestionarActivo())
            {
                MessageBox.Show(
                    "Solo el administrador o directivo puede activar o desactivar.",
                    nombreEntidad,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }

            try
            {
                bool ok;
                if (activoActual)
                    ok = darDeBaja != null ? darDeBaja(id) : false;
                else
                    ok = darDeAlta != null ? darDeAlta(id) : false;

                if (ok)
                {
                    MessageBox.Show(
                        activoActual ? "Registro desactivado." : "Registro activado.",
                        nombreEntidad,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(
                        "No se pudo actualizar el estado.",
                        nombreEntidad,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
                return ok;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo actualizar el estado.\n" + ex.Message,
                    nombreEntidad,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
