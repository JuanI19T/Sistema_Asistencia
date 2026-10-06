using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SistemaAsistencia.Controlador;
using SistemaAsistencia.Modelo.Entidades;
using SistemaAsistencia.Utilidades;
using SistemaAsistencia.Vista.Comun;

namespace SistemaAsistencia.Vista.Usuarios
{
    public partial class FrmUsuarios : FrmBaseHijo
    {
        private readonly UsuarioController usuarioController;
        private List<Usuario> cacheUsuarios = new List<Usuario>();
        private int? idUsuarioSeleccionado = null;

        public FrmUsuarios()
        {
            InitializeComponent();

            usuarioController = new UsuarioController();

            Tema.ConfigurarFondo(this);
            Tema.EstilizarGrilla(dgvUsuarios);

            InicializarRolesEstaticos();
        }


        private void FrmUsuarios_Load(object sender, EventArgs e)
        {
            // La fuente del selector la unifica FrmBaseHijo.
            // El evento Load se concentra exclusivamente en los datos dinámicos.
            CargaVista.IntentarCarga(CargarUsuarios, "usuarios");
        }

        private void InicializarRolesEstaticos()
        {
            string[] roles = { "Administrador", "Directivo", "Preceptor", "Profesor" };

            CargaVista.CargarItems(cmbRol, roles);
            CargaVista.CargarItems(cmbEditRol, roles);
        }

        private void CargarUsuarios()
        {
            cacheUsuarios = CargaVista.ObtenerLista(() => usuarioController.ObtenerUsuarios());
            MostrarEnGrilla(cacheUsuarios);
        }

        private void MostrarEnGrilla(List<Usuario> lista)
        {
            CargaVista.MostrarEnGrilla(dgvUsuarios, lista, ConfigurarColumnasUsuarios);
        }

        private void ConfigurarColumnasUsuarios(DataGridView dgv)
        {
            if (dgv.Columns.Count == 0) return;

            if (dgv.Columns.Contains("IdUsuario"))
                dgv.Columns["IdUsuario"].Visible = false;
            if (dgv.Columns.Contains("Contrasena"))
                dgv.Columns["Contrasena"].Visible = false;
            if (dgv.Columns.Contains("NombreUsuario"))
                dgv.Columns["NombreUsuario"].HeaderText = "Usuario";
            if (dgv.Columns.Contains("Activo"))
                dgv.Columns["Activo"].ReadOnly = true;
        }

        // ---------------- 1. Crear ----------------

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsuario.Text))
            {
                MessageBox.Show(
                    "Ingrese un nombre de usuario.",
                    "Usuarios",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtUsuario.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show(
                    "Ingrese una contraseña.",
                    "Usuarios",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            if (cmbRol.SelectedIndex < 0)
            {
                MessageBox.Show(
                    "Seleccione un rol.",
                    "Usuarios",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                cmbRol.Focus();
                return;
            }

            EjecutarConLayout(() =>
            {
                try
                {
                    Usuario usuario = new Usuario
                    {
                        NombreUsuario = txtUsuario.Text.Trim(),
                        Contrasena = txtPassword.Text,
                        Rol = cmbRol.SelectedItem.ToString(),
                        Activo = true
                    };

                    if (usuarioController.AgregarUsuario(usuario))
                    {
                        MessageBox.Show(
                            "Usuario agregado correctamente.",
                            "Usuarios",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        CargarUsuarios();
                        LimpiarCreacion();
                    }
                    else
                    {
                        MessageBox.Show(
                            "No se pudo agregar el usuario (¿nombre duplicado?).",
                            "Usuarios",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "No se pudo agregar el usuario.\n" + ex.Message,
                        "Usuarios",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            });
        }

        private void btnLimpiarCrear_Click(object sender, EventArgs e)
        {
            LimpiarCreacion();
        }

        private void LimpiarCreacion()
        {
            txtUsuario.Clear();
            txtPassword.Clear();
            CargaVista.ReiniciarCombo(cmbRol);
        }

        private void LimpiarEdicion()
        {
            idUsuarioSeleccionado = null;
            lblEditando.Text = "Editando: (seleccione de la lista)";
            txtEditUsuario.Clear();
            txtEditPassword.Clear();
            CargaVista.ReiniciarCombo(cmbEditRol);
            chkEditActivo.Checked = false;
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

            CargaVista.FiltrarCache(
                cacheUsuarios,
                texto,
                u => u.NombreUsuario != null &&
                     u.NombreUsuario.IndexOf(texto, StringComparison.OrdinalIgnoreCase) >= 0,
                MostrarEnGrilla,
                CargarEnEdicion,
                LimpiarEdicion,
                "Usuarios",
                interactivo);
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
            EjecutarConLayout(() =>
            {
                idUsuarioSeleccionado = usuario.IdUsuario;
                lblEditando.Text = "Editando: " + usuario.NombreUsuario;
                txtEditUsuario.Text = usuario.NombreUsuario;
                txtEditPassword.Clear();
                SeleccionarRol(cmbEditRol, usuario.Rol);
                chkEditActivo.Checked = usuario.Activo;
            });
        }

        private static void SeleccionarRol(ComboBox combo, string rol)
        {
            if (string.IsNullOrWhiteSpace(rol))
            {
                combo.SelectedIndex = -1;
            }
            else
            {
                string rolNormalizado = rol.Trim();

                // "Docente" viene de la BD legacy y equivale a "Profesor".
                if (string.Equals(rolNormalizado, "Docente", StringComparison.OrdinalIgnoreCase))
                    rolNormalizado = "Profesor";

                int indice = combo.FindStringExact(rolNormalizado);
                combo.SelectedIndex = indice >= 0 ? indice : -1;
            }

            CargaVista.Refrescar(combo);
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (idUsuarioSeleccionado == null)
            {
                MessageBox.Show(
                    "Busque y seleccione un usuario primero.",
                    "Usuarios",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtEditUsuario.Text))
            {
                MessageBox.Show(
                    "Ingrese un nombre de usuario.",
                    "Usuarios",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtEditUsuario.Focus();
                return;
            }

            if (cmbEditRol.SelectedIndex < 0)
            {
                MessageBox.Show(
                    "Seleccione un rol.",
                    "Usuarios",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                cmbEditRol.Focus();
                return;
            }

            EjecutarConLayout(() =>
            {
                try
                {
                    Usuario usuario = new Usuario
                    {
                        IdUsuario = idUsuarioSeleccionado.Value,
                        NombreUsuario = txtEditUsuario.Text.Trim(),
                        Contrasena = txtEditPassword.Text,
                        Rol = cmbEditRol.SelectedItem.ToString(),
                        Activo = chkEditActivo.Checked
                    };

                    if (usuarioController.ModificarUsuario(usuario))
                    {
                        MessageBox.Show(
                            "Usuario modificado correctamente.",
                            "Usuarios",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        CargarUsuarios();
                        LimpiarEdicion();
                    }
                    else
                    {
                        MessageBox.Show(
                            "No se pudo modificar el usuario.",
                            "Usuarios",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "No se pudo modificar el usuario.\n" + ex.Message,
                        "Usuarios",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            });
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idUsuarioSeleccionado == null)
            {
                MessageBox.Show(
                    "Busque y seleccione un usuario primero.",
                    "Usuarios",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            DialogResult resultado = MessageBox.Show(
                "¿Desea eliminar este usuario?",
                "Usuarios",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado != DialogResult.Yes) return;

            EjecutarConLayout(() =>
            {
                try
                {
                    if (usuarioController.EliminarUsuario(idUsuarioSeleccionado.Value))
                    {
                        MessageBox.Show(
                            "Usuario eliminado.",
                            "Usuarios",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        CargarUsuarios();
                        LimpiarEdicion();
                    }
                    else
                    {
                        MessageBox.Show(
                            "No se pudo eliminar.",
                            "Usuarios",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "No se pudo eliminar.\n" + ex.Message,
                        "Usuarios",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            });
        }

        private void btnLimpiarEditar_Click(object sender, EventArgs e)
        {
            LimpiarEdicion();
        }
    }
}