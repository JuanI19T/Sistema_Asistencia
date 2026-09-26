using System;
using System.Windows.Forms;
using SistemaAsistencia.Controlador;
using SistemaAsistencia.Modelo.Entidades;

namespace SistemaAsistencia.Vista.PrimerUsuario
{
    public partial class FrmPrimerUsuario : Form
    {
        private readonly UsuarioController usuarioController;

        public FrmPrimerUsuario()
        {
            InitializeComponent();
            usuarioController = new UsuarioController();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            // Se aplican los placeholders una vez que la ventana está renderizada en pantalla
            UIHelper.EstablecerPlaceholder(txtUsuario, "USUARIO");
            UIHelper.EstablecerPlaceholder(txtPassword, "CONTRASEÑA");
            UIHelper.EstablecerPlaceholder(txtConfirmarPassword, "CONFIRMAR CONTRASEÑA");
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            DialogResult resp = MessageBox.Show(
                "Cerrar sistema, ¿confirma?",
                "Sistema Asistencia",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question
            );

            if (resp == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnCrearAdmin_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string contraseña = txtPassword.Text;
            string confirmarContraseña = txtConfirmarPassword.Text;

            if (string.IsNullOrWhiteSpace(usuario))
            {
                MessageBox.Show(
                    "Debe ingresar un nombre de usuario.",
                    "Datos incompletos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtUsuario.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(contraseña))
            {
                MessageBox.Show(
                    "Debe ingresar una contraseña.",
                    "Datos incompletos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtPassword.Focus();
                return;
            }

            if (contraseña != confirmarContraseña)
            {
                MessageBox.Show(
                    "Las contraseñas no coinciden.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtConfirmarPassword.Clear();
                txtConfirmarPassword.Focus();
                return;
            }

            DialogResult resultado = MessageBox.Show(
                "Se creará el primer usuario del sistema con el rol de Administrador.\n\n¿Desea continuar?",
                "Crear administrador",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (resultado != DialogResult.Yes)
            {
                return;
            }

            Usuario nuevoUsuario = new Usuario
            {
                NombreUsuario = usuario,
                Contrasena = contraseña,
                Rol = "Administrador",
                Activo = true
            };

            bool creado = usuarioController.AgregarUsuario(nuevoUsuario);

            if (creado)
            {
                MessageBox.Show(
                    "El usuario administrador fue creado correctamente.",
                    "Configuración inicial",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show(
                    "No se pudo crear el usuario administrador.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}