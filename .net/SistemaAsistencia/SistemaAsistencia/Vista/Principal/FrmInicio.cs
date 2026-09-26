using System;
using System.Windows.Forms;
using SistemaAsistencia.Utilidades;

namespace SistemaAsistencia.Vista.Principal
{
    public partial class FrmInicio : Form
    {
        public FrmInicio()
        {
            InitializeComponent();
        }

        private void FrmInicio_Load(object sender, EventArgs e)
        {
            // Sin acceso a BD: carga instantánea, sin demoras ni errores
            // de conexión para ningún rol.
            if (Sesion.UsuarioActual != null)
            {
                lblTitulo.Text = $"Bienvenido, {Sesion.UsuarioActual.NombreUsuario}";
                lblUsuario.Text = $"Usuario: {Sesion.UsuarioActual.NombreUsuario}";
                lblRol.Text = $"Rol: {Sesion.UsuarioActual.Rol}";
            }

            lblFecha.Text = $"Fecha: {DateTime.Now:dd/MM/yyyy}";
        }
    }
}
