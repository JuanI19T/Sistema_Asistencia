using System;
using System.Drawing;
using System.Windows.Forms;
using MaterialSkin.Controls;

namespace SistemaAsistencia.Vista.Comun
{
    public partial class FrmBaseHijo : MaterialForm
    {
        public FrmBaseHijo()
        {
            // Hijo embebido en FrmPrincipal: sin botones de ventana.
            // ControlBox=false quita la X pero conserva el título propio
            // (MaterialForm dibuja su header aunque vaya con Border None).
            // El ciclo de vida lo maneja FrmPrincipal.MostrarForm.
            this.Sizable = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ControlBox = false;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;

            // Red de contención para monitores chicos: si el contenido
            // interno desborda (ej. tras el re-escalado por fuente/DPI),
            // el form muestra scrollbars en vez de recortar botones.
            // El form va Dock.Fill en panelContenido, así que su tamaño
            // lo manda el panel; esto cubre el desborde interno.
            this.AutoScroll = true;

            this.Load += FrmBaseHijo_Load;
        }

        private void FrmBaseHijo_Load(object sender, EventArgs e)
        {
            // Fuente única de los selectores de pestañas.
            AplicarFuenteUniforme(this);
        }

        private void AplicarFuenteUniforme(Control contenedor)
        {
            foreach (Control ctrl in contenedor.Controls)
            {
                if (ctrl is MaterialTabSelector)
                {
                    ctrl.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
                }

                if (ctrl.HasChildren)
                {
                    AplicarFuenteUniforme(ctrl);
                }
            }
        }
    }
}