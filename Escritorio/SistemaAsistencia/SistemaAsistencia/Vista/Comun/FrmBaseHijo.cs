using System;
using System.Drawing;
using System.Windows.Forms;
using MaterialSkin.Controls;
using SistemaAsistencia.Utilidades;

namespace SistemaAsistencia.Vista.Comun
{
    public partial class FrmBaseHijo : MaterialForm
    {
        public FrmBaseHijo()
        {
            // Hijo embebido en FrmPrincipal: sin botones de ventana.
            this.Sizable = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ControlBox = false;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;

            // Red de contención
            this.AutoScroll = true;

            // Obliga al hijo a calzar con el panelContenido
            this.MinimumSize = System.Drawing.Size.Empty;
            this.MaximumSize = System.Drawing.Size.Empty;

            this.Load += FrmBaseHijo_Load;
        }

        private void FrmBaseHijo_ParentChanged(object sender, EventArgs e)
        {
            AjustarAlPadre();
        }

        private void FrmBaseHijo_SizeChanged(object sender, EventArgs e)
        {
            AjustarAlPadre();
        }

        private void AjustarAlPadre()
        {
            if (this.Parent == null) return;

            // Forzamos a que ocupe exactamente el espacio del contenedor
            this.Dock = DockStyle.Fill;
            this.MinimumSize = new Size(0, 0);
            this.MaximumSize = new Size(0, 0);
            this.AutoSize = false;

            // Aseguramos que tome el ClientSize del panel
            if (this.Parent is Control contenedor)
            {
                this.ClientSize = contenedor.ClientSize;
            }
        }

        /// <summary>
        /// Ejecuta una acción con el layout del form suspendido (ver
        /// CargaVista): altas, modificaciones, bajas y cargas que tocan
        /// varios controles a la vez, sin parpadeos ni estados intermedios.
        /// Todos los formularios la heredan.
        /// </summary>
        protected void EjecutarConLayout(Action accion)
        {
            CargaVista.EjecutarConLayout(this, accion);
        }

        /// <summary>
        /// Fuerza el repintado inmediato de un control Material.
        /// Todos los formularios la heredan.
        /// </summary>
        protected void Refrescar(Control control)
        {
            CargaVista.Refrescar(control);
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