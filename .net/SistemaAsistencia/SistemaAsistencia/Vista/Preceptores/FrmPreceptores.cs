using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SistemaAsistencia.Controlador;
using SistemaAsistencia.Modelo.DAO;
using SistemaAsistencia.Modelo.Entidades;
using SistemaAsistencia.Vista.Comun;

namespace SistemaAsistencia.Vista.Preceptores
{
    public partial class FrmPreceptores : Form
    {
        private readonly PreceptorController preceptorController;
        private List<Preceptor> cachePreceptores = new List<Preceptor>();
        private int idPreceptorSeleccionado = 0;

        public FrmPreceptores()
        {
            InitializeComponent();

            preceptorController = new PreceptorController();
        }

        private void FrmPreceptores_Load(object sender, EventArgs e)
        {
            try
            {
                CargarPreceptores();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo cargar preceptores.\n" + ex.Message,
                    "Error de conexión",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void CargarPreceptores()
        {
            cachePreceptores =
                preceptorController.ObtenerPreceptores() ?? new List<Preceptor>();
            MostrarEnGrilla(cachePreceptores);
        }

        private void MostrarEnGrilla(List<Preceptor> lista)
        {
            dgvPreceptores.DataSource = null;
            dgvPreceptores.DataSource = lista;

            if (dgvPreceptores.Columns.Count > 0)
            {
                if (dgvPreceptores.Columns.Contains("IdPreceptor"))
                    dgvPreceptores.Columns["IdPreceptor"].Visible = false;
                if (dgvPreceptores.Columns.Contains("NombrePreceptor"))
                    dgvPreceptores.Columns["NombrePreceptor"].HeaderText = "Nombre";
                if (dgvPreceptores.Columns.Contains("ApellidoPreceptor"))
                    dgvPreceptores.Columns["ApellidoPreceptor"].HeaderText = "Apellido";
                if (dgvPreceptores.Columns.Contains("LegajoPreceptor"))
                    dgvPreceptores.Columns["LegajoPreceptor"].HeaderText = "Legajo";
                if (dgvPreceptores.Columns.Contains("CorreoPreceptor"))
                    dgvPreceptores.Columns["CorreoPreceptor"].HeaderText = "Correo";
                if (dgvPreceptores.Columns.Contains("TelefonoPreceptor"))
                    dgvPreceptores.Columns["TelefonoPreceptor"].HeaderText = "Teléfono";
            }
        }

        // Formato argentino: 54 + área (3) + número (7).
        // Vacío se permite; a medio completar se exige completar.
        private static bool TelefonoValido(MaskedTextBox txt)
        {
            string digitos = new string(txt.Text.Where(char.IsDigit).ToArray());
            return digitos.Length <= 2 || txt.MaskCompleted;
        }

        private static string TelefonoLimpio(MaskedTextBox txt)
        {
            string digitos = new string(txt.Text.Where(char.IsDigit).ToArray());
            return digitos.Length <= 2 ? string.Empty : txt.Text;
        }

        // ---------------- 1. Crear ----------------

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!TelefonoValido(txtTelefono))
            {
                MessageBox.Show(
                    "El teléfono debe estar completo (54 + área de 3 + número) o vacío.");
                txtTelefono.Focus();
                return;
            }

            if (!DniValido(txtDni.Text))
            {
                MessageBox.Show("El DNI debe contener solo números.");
                txtDni.Focus();
                return;
            }

            try
            {
                Preceptor preceptor = new Preceptor
                {
                    NombrePreceptor = txtNombre.Text,
                    ApellidoPreceptor = txtApellido.Text,
                    LegajoPreceptor = txtLegajo.Text,
                    Dni = txtDni.Text.Trim(),
                    CorreoPreceptor = txtCorreo.Text,
                    TelefonoPreceptor = TelefonoLimpio(txtTelefono),
                    Contrasena = txtContrasena.Text
                };

                if (preceptorController.AgregarPreceptor(preceptor))
                {
                    MessageBox.Show("Preceptor agregado correctamente.");

                    CargarPreceptores();
                    LimpiarCreacion();
                }
                else
                {
                    MessageBox.Show("No se pudo agregar el preceptor.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo agregar el preceptor.\n" + ex.Message);
            }
        }

        private void btnLimpiarCrear_Click(object sender, EventArgs e)
        {
            LimpiarCreacion();
        }

        private void LimpiarCreacion()
        {
            txtNombre.Clear();
            txtApellido.Clear();
            txtLegajo.Clear();
            txtDni.Clear();
            txtCorreo.Clear();
            txtTelefono.Clear();
            txtContrasena.Clear();
            txtNombre.Focus();
        }

        // El DNI es obligatorio y numérico para poder loguearse en la app móvil.
        private static bool DniValido(string dni)
        {
            return !string.IsNullOrWhiteSpace(dni) &&
                dni.Trim().All(char.IsDigit);
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
                MostrarEnGrilla(cachePreceptores);
                return;
            }

            List<Preceptor> resultados = cachePreceptores.FindAll(p =>
                (p.NombrePreceptor != null &&
                    p.NombrePreceptor.IndexOf(texto, StringComparison.OrdinalIgnoreCase) >= 0) ||
                (p.ApellidoPreceptor != null &&
                    p.ApellidoPreceptor.IndexOf(texto, StringComparison.OrdinalIgnoreCase) >= 0));

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

        private void dgvPreceptores_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            Preceptor preceptor =
                dgvPreceptores.Rows[e.RowIndex].DataBoundItem as Preceptor;
            if (preceptor != null)
                CargarEnEdicion(preceptor);
        }

        private void CargarEnEdicion(Preceptor preceptor)
        {
            idPreceptorSeleccionado = preceptor.IdPreceptor;
            lblEditando.Text =
                "Editando: " + preceptor.ApellidoPreceptor + ", " + preceptor.NombrePreceptor;
            txtEditNombre.Text = preceptor.NombrePreceptor;
            txtEditApellido.Text = preceptor.ApellidoPreceptor;
            txtEditLegajo.Text = preceptor.LegajoPreceptor;
            txtEditDni.Text = preceptor.Dni;
            txtEditCorreo.Text = preceptor.CorreoPreceptor;
            txtEditTelefono.Text = preceptor.TelefonoPreceptor;
            txtEditContrasena.Clear();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (idPreceptorSeleccionado == 0)
            {
                MessageBox.Show("Busque y seleccione un preceptor primero.");
                return;
            }

            if (!TelefonoValido(txtEditTelefono))
            {
                MessageBox.Show(
                    "El teléfono debe estar completo (54 + área de 3 + número) o vacío.");
                txtEditTelefono.Focus();
                return;
            }

            if (!DniValido(txtEditDni.Text))
            {
                MessageBox.Show("El DNI debe contener solo números.");
                txtEditDni.Focus();
                return;
            }

            try
            {
                Preceptor preceptor = new Preceptor
                {
                    IdPreceptor = idPreceptorSeleccionado,
                    NombrePreceptor = txtEditNombre.Text,
                    ApellidoPreceptor = txtEditApellido.Text,
                    LegajoPreceptor = txtEditLegajo.Text,
                    Dni = txtEditDni.Text.Trim(),
                    CorreoPreceptor = txtEditCorreo.Text,
                    TelefonoPreceptor = TelefonoLimpio(txtEditTelefono),
                    Contrasena = txtEditContrasena.Text
                };

                if (preceptorController.ModificarPreceptor(preceptor))
                {
                    MessageBox.Show("Preceptor modificado correctamente.");

                    CargarPreceptores();
                    LimpiarEdicion();
                }
                else
                {
                    MessageBox.Show("No se pudo modificar el preceptor.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo modificar el preceptor.\n" + ex.Message);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idPreceptorSeleccionado == 0)
            {
                MessageBox.Show("Busque y seleccione un preceptor primero.");
                return;
            }

            Preceptor preceptor = cachePreceptores.Find(p => p.IdPreceptor == idPreceptorSeleccionado);
            if (preceptor == null) return;

            try
            {
                string descripcion = "Preceptor: " + preceptor.NombreCompleto +
                    " (Legajo " + preceptor.LegajoPreceptor + ")";

                List<Dependencia> dependencias =
                    preceptorController.ObtenerDependencias(idPreceptorSeleccionado);

                using (var confirmar = new FrmConfirmarEliminar(descripcion, dependencias))
                {
                    if (confirmar.ShowDialog(this) != DialogResult.OK) return;

                    bool ok;

                    if (confirmar.Resultado == ResultadoEliminacion.Definitiva)
                    {
                        ok = preceptorController.EliminarDefinitivo(idPreceptorSeleccionado);
                        if (ok) MessageBox.Show("Preceptor eliminado definitivamente.");
                    }
                    else
                    {
                        ok = preceptorController.DarDeBaja(idPreceptorSeleccionado);
                        if (ok) MessageBox.Show("Preceptor dado de baja.");
                    }

                    if (ok)
                    {
                        CargarPreceptores();
                        LimpiarEdicion();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo eliminar.\n" + ex.Message);
            }
        }

        private void btnLimpiarEditar_Click(object sender, EventArgs e)
        {
            LimpiarEdicion();
        }

        private void LimpiarEdicion()
        {
            idPreceptorSeleccionado = 0;
            lblEditando.Text = "Editando: (seleccione de la lista)";
            txtEditNombre.Clear();
            txtEditApellido.Clear();
            txtEditLegajo.Clear();
            txtEditDni.Clear();
            txtEditCorreo.Clear();
            txtEditTelefono.Clear();
            txtEditContrasena.Clear();
        }
    }
}