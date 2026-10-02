using System;
using System.Drawing;
using System.Windows.Forms;
using SistemaAsistencia.Controlador;
using SistemaAsistencia.Modelo.Entidades;
using SistemaAsistencia.Utilidades;
using SistemaAsistencia.Vista.Principal;
using MaterialSkin;
using MaterialSkin.Controls;

namespace SistemaAsistencia.Vista.Login
{
    public partial class FrmLogin : MaterialForm
    {
        private readonly UsuarioController usuarioController;
        // True cuando abrimos el principal: no matar el proceso en FormClosing.
        private bool navegando = false;


        public FrmLogin()
        {
            InitializeComponent();

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Blue600, Primary.Blue700, Primary.Blue200, Accent.LightBlue200, TextShade.WHITE);

            usuarioController = new UsuarioController();

            // Configuración inicial del formulario
            txtPassword.Password = true;
            txtUsuario.Focus();

            // Permite usar Enter para iniciar sesión
            this.AcceptButton = btnLogin;

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
            this.FormClosing += FrmLogin_FormClosing;
        }


        private void FrmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Cerrar este form por la vía normal (Close/Dispose) se cuelga al
            // destruir sus MaterialTextBox. Se sale del proceso directamente,
            // salvo que sea por navegación al principal (segundo login).
            if (navegando) return;
            Environment.Exit(0);
        }


        private void btnLogin_Click(object sender, EventArgs e)
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


            // Solicita al controlador validar las credenciales
            Usuario usuario;
            try
            {
                usuario = usuarioController.IniciarSesion(
                    txtUsuario.Text,
                    txtPassword.Text);
            }
            catch (Exception ex)
            {
                MaterialMessageBox.Show(
                    this,
                    "No se pudo conectar a la base de datos.\n" + ex.Message,
                    "Error de conexión",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1,
                    false,
                    FlexibleMaterialForm.ButtonsPosition.Center);
                return;
            }


            // Si existe el usuario, se inicia sesión
            if (usuario != null)
            {
                Sesion.UsuarioActual = usuario;


                MaterialMessageBox.Show(
                    this,
                    $"Bienvenido {usuario.NombreUsuario}",
                    "Inicio correcto",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1,
                    false,
                    FlexibleMaterialForm.ButtonsPosition.Center);


                // Abrir formulario principal
                FrmPrincipal principal = new FrmPrincipal();

                navegando = true;
                this.Hide();
                principal.ShowDialog();

                this.Close();
            }
            else
            {
                MaterialMessageBox.Show(
                    this,
                    "Usuario o contraseña incorrectos.",
                    "Acceso denegado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1,
                    false,
                    FlexibleMaterialForm.ButtonsPosition.Center);

                txtPassword.Clear();
                txtPassword.Focus();
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