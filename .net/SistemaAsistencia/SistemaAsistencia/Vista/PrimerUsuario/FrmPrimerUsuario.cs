using System;
using System.Drawing;
using System.Windows.Forms;
using SistemaAsistencia.Controlador;
using SistemaAsistencia.Modelo.Entidades;
using MaterialSkin;
using MaterialSkin.Controls;

namespace SistemaAsistencia.Vista.PrimerUsuario
{
    public partial class FrmPrimerUsuario : MaterialForm
    {
        private readonly UsuarioController usuarioController;

        public FrmPrimerUsuario()
        {
            InitializeComponent();

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Blue600, Primary.Blue700, Primary.Blue200,
                Accent.LightBlue200, TextShade.WHITE);

            usuarioController = new UsuarioController();

            // Configuración inicial del formulario
            txtPassword.Password = true;
            txtUsuario.Focus();

            // Permite usar Enter para continuar
            this.AcceptButton = btnCrearAdmin;

            // Permite usar Escape para cancelar
            this.CancelButton = btnSalir;

            // Multi-monitor: abrir en la pantalla donde está el cursor,
            // no siempre en la primaria (puede ser la TV).
            this.StartPosition = FormStartPosition.Manual;
            try
            {
                Rectangle area = Screen.FromPoint(Cursor.Position).WorkingArea;
                Location = new System.Drawing.Point(
                    area.Left + (area.Width - Width) / 2,
                    area.Top + (area.Height - Height) / 2);
            }
            catch { }

            // La cruz / Alt+F4 deben cerrar la app sin pasar por Dispose
            // (los MaterialTextBox son RichTextBox y se cuelgan en EM_STREAMOUT).
            this.FormClosing += FrmPrimerUsuario_FormClosing;
        }


        private void FrmPrimerUsuario_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Cerrar este form por la vía normal (Close/Dispose) se cuelga al
            // destruir sus MaterialTextBox. Se sale del proceso directamente.
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Environment.Exit(0);
            }
        }


        private void btnCrearAdmin_Click(object sender, EventArgs e)
        {
            // Validación de usuario vacío
            if (string.IsNullOrWhiteSpace(txtUsuario.Text))
            {
                MaterialMessageBox.Show(
                    this,
                    "Ingrese un usuario.",
                    "Atención",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button1,
                    false,
                    FlexibleMaterialForm.ButtonsPosition.Center);

                txtUsuario.Focus();
                return;
            }

            // Validación de contraseña vacía
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MaterialMessageBox.Show(
                    this,
                    "Ingrese una contraseña.",
                    "Atención",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button1,
                    false,
                    FlexibleMaterialForm.ButtonsPosition.Center);

                txtPassword.Focus();
                return;
            }

            // Validación de confirmación de contraseña
            if (txtPassword.Text != txtConfirmarPassword.Text)
            {
                MaterialMessageBox.Show(
                    this,
                    "Las contraseñas no coinciden.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1,
                    false,
                    FlexibleMaterialForm.ButtonsPosition.Center);

                txtConfirmarPassword.Clear();
                txtConfirmarPassword.Focus();
                return;
            }

            DialogResult resultado = MaterialMessageBox.Show(
                this,
                "Se creará el primer usuario del sistema con el rol de Administrador.\n\n¿Desea continuar?",
                "Crear administrador",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado != DialogResult.Yes)
            {
                return;
            }

            Usuario nuevoUsuario = new Usuario
            {
                NombreUsuario = txtUsuario.Text.Trim(),
                Contrasena = txtPassword.Text,
                Rol = "Administrador",
                Activo = true
            };

            bool creado = usuarioController.AgregarUsuario(nuevoUsuario);

            if (creado)
            {
                MaterialMessageBox.Show(
                    this,
                    "El usuario administrador fue creado correctamente.",
                    "Configuración inicial",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1,
                    false,
                    FlexibleMaterialForm.ButtonsPosition.Center);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MaterialMessageBox.Show(
                    this,
                    "No se pudo crear el usuario administrador.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1,
                    false,
                    FlexibleMaterialForm.ButtonsPosition.Center);
            }
        }


        private void btnSalir_Click(object sender, EventArgs e)
        {
            DialogResult respuesta = MaterialMessageBox.Show(
                this,
                "¿Desea cerrar el sistema?",
                "Sistema Asistencia",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2,
                false,
                FlexibleMaterialForm.ButtonsPosition.Center);


            if (respuesta == DialogResult.Yes)
            {
                // Ver FrmPrincipal.Salir: Application.Exit() se cuelga
                // destruyendo los MaterialTextBox (RichTextBox).
                Environment.Exit(0);
            }
        }


        private void txtUsuario_TextChanged(object sender, EventArgs e)
        {

        }
    }
}