using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SistemaAsistencia.Controlador;
using SistemaAsistencia.Modelo.Entidades;

namespace SistemaAsistencia.Vista.Usuarios
{
    public partial class FrmUsuarios : Form
    {
        private readonly UsuarioController usuarioController;
        private List<Usuario> cacheUsuarios = new List<Usuario>();
        private int? idUsuarioSeleccionado = null;

        public FrmUsuarios()
        {
            InitializeComponent();

            usuarioController = new UsuarioController();
        }

        private void FrmUsuarios_Load(object sender, EventArgs e)
        {
            try
            {
                CargarRoles();
                CargarUsuarios();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo cargar usuarios.\n" + ex.Message,
                    "Error de conexión",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void CargarRoles()
        {
            string[] roles = { "Administrador", "Directivo", "Preceptor", "Docente" };

            cmbRol.Items.Clear();
            cmbRol.Items.AddRange(roles);
            cmbRol.SelectedIndex = -1;

            cmbEditRol.Items.Clear();
            cmbEditRol.Items.AddRange(roles);
            cmbEditRol.SelectedIndex = -1;
        }

        private void CargarUsuarios()
        {
            cacheUsuarios = usuarioController.ObtenerUsuarios() ?? new List<Usuario>();
            MostrarEnGrilla(cacheUsuarios);
        }

        private void MostrarEnGrilla(List<Usuario> lista)
        {
            dgvUsuarios.DataSource = null;
            dgvUsuarios.DataSource = lista;

            if (dgvUsuarios.Columns.Count > 0)
            {
                if (dgvUsuarios.Columns.Contains("IdUsuario"))
                    dgvUsuarios.Columns["IdUsuario"].Visible = false;
                if (dgvUsuarios.Columns.Contains("Contrasena"))
                    dgvUsuarios.Columns["Contrasena"].Visible = false;
                if (dgvUsuarios.Columns.Contains("NombreUsuario"))
                    dgvUsuarios.Columns["NombreUsuario"].HeaderText = "Usuario";
                if (dgvUsuarios.Columns.Contains("Activo"))
                    dgvUsuarios.Columns["Activo"].ReadOnly = true;
            }
        }

        // ---------------- 1. Crear ----------------

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsuario.Text))
            {
                MessageBox.Show("Ingrese un nombre de usuario.");
                txtUsuario.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Ingrese una contraseña.");
                txtPassword.Focus();
                return;
            }

            if (cmbRol.SelectedIndex < 0)
            {
                MessageBox.Show("Seleccione un rol.");
                cmbRol.Focus();
                return;
            }

            try
            {
                Usuario usuario = new Usuario
                {
                    NombreUsuario = txtUsuario.Text.Trim(),
                    Contrasena = txtPassword.Text,
                    Rol = cmbRol.SelectedItem.ToString(),
                    Activo = chkActivo.Checked
                };

                if (usuarioController.AgregarUsuario(usuario))
                {
                    MessageBox.Show("Usuario agregado correctamente.");

                    CargarUsuarios();
                    LimpiarCreacion();
                }
                else
                {
                    MessageBox.Show("No se pudo agregar el usuario (¿nombre duplicado?).");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo agregar el usuario.\n" + ex.Message);
            }
        }

        private void btnLimpiarCrear_Click(object sender, EventArgs e)
        {
            LimpiarCreacion();
        }

        private void LimpiarCreacion()
        {
            txtUsuario.Clear();
            txtPassword.Clear();
            cmbRol.SelectedIndex = -1;
            chkActivo.Checked = true;
        }

        // ---------------- 2. Buscar y modificar ----------------

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            AplicarFiltro(interactivo: true);
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            AplicarFiltro(interactivo: false);
        }

        private void AplicarFiltro(bool interactivo)
        {
            string texto = txtBuscar.Text.Trim();

            if (string.IsNullOrEmpty(texto))
            {
                MostrarEnGrilla(cacheUsuarios);
                return;
            }

            List<Usuario> resultados = cacheUsuarios.FindAll(u =>
                u.NombreUsuario != null &&
                u.NombreUsuario.IndexOf(texto, StringComparison.OrdinalIgnoreCase) >= 0);

            MostrarEnGrilla(resultados);

            if (!interactivo) return;

            if (resultados.Count == 0)
            {
                MessageBox.Show("Sin resultados para esa búsqueda.");
                LimpiarEdicion();
            }
            else if (resultados.Count == 1)
            {
                CargarEnEdicion(resultados[0]);
            }
        }

        private void dgvUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            Usuario usuario = dgvUsuarios.Rows[e.RowIndex].DataBoundItem as Usuario;
            if (usuario != null)
                CargarEnEdicion(usuario);
        }

        private void CargarEnEdicion(Usuario usuario)
        {
            idUsuarioSeleccionado = usuario.IdUsuario;
            lblEditando.Text = "Editando: " + usuario.NombreUsuario;
            txtEditUsuario.Text = usuario.NombreUsuario;
            txtEditPassword.Clear();
            SeleccionarRol(cmbEditRol, usuario.Rol);
            chkEditActivo.Checked = usuario.Activo;
        }

        private static void SeleccionarRol(ComboBox combo, string rol)
        {
            if (string.IsNullOrWhiteSpace(rol))
            {
                combo.SelectedIndex = -1;
                return;
            }

            int indice = combo.FindStringExact(rol.Trim());
            combo.SelectedIndex = indice >= 0 ? indice : -1;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (idUsuarioSeleccionado == null)
            {
                MessageBox.Show("Busque y seleccione un usuario primero.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtEditUsuario.Text))
            {
                MessageBox.Show("Ingrese un nombre de usuario.");
                return;
            }

            if (cmbEditRol.SelectedIndex < 0)
            {
                MessageBox.Show("Seleccione un rol.");
                return;
            }

            try
            {
                Usuario usuario = new Usuario
                {
                    IdUsuario = idUsuarioSeleccionado.Value,
                    NombreUsuario = txtEditUsuario.Text.Trim(),
                    // Vacío = no se cambia (el DAO solo la pisa si hay texto).
                    Contrasena = txtEditPassword.Text,
                    Rol = cmbEditRol.SelectedItem.ToString(),
                    Activo = chkEditActivo.Checked
                };

                if (usuarioController.ModificarUsuario(usuario))
                {
                    MessageBox.Show("Usuario modificado correctamente.");

                    CargarUsuarios();
                    LimpiarEdicion();
                }
                else
                {
                    MessageBox.Show("No se pudo modificar el usuario.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo modificar el usuario.\n" + ex.Message);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idUsuarioSeleccionado == null)
            {
                MessageBox.Show("Busque y seleccione un usuario primero.");
                return;
            }

            DialogResult resultado = MessageBox.Show(
                "¿Desea eliminar este usuario?",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado != DialogResult.Yes) return;

            try
            {
                if (usuarioController.EliminarUsuario(idUsuarioSeleccionado.Value))
                {
                    MessageBox.Show("Usuario eliminado.");

                    CargarUsuarios();
                    LimpiarEdicion();
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo eliminar.\n" + ex.Message);
            }
        }

        private void btnActivar_Click(object sender, EventArgs e)
        {
            if (idUsuarioSeleccionado == null)
            {
                MessageBox.Show("Busque y seleccione un usuario primero.");
                return;
            }

            try
            {
                if (usuarioController.ActivarUsuario(idUsuarioSeleccionado.Value))
                {
                    MessageBox.Show("Usuario activado correctamente.");

                    CargarUsuarios();
                    LimpiarEdicion();
                }
                else
                {
                    MessageBox.Show("No se pudo activar el usuario.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo activar.\n" + ex.Message);
            }
        }

        private void btnLimpiarEditar_Click(object sender, EventArgs e)
        {
            LimpiarEdicion();
        }

        private void LimpiarEdicion()
        {
            idUsuarioSeleccionado = null;
            lblEditando.Text = "Editando: (seleccione de la lista)";
            txtEditUsuario.Clear();
            txtEditPassword.Clear();
            cmbEditRol.SelectedIndex = -1;
            chkEditActivo.Checked = false;
        }
    }
}
