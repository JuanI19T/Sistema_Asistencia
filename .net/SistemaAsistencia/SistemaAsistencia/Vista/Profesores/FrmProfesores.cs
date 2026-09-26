using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SistemaAsistencia.Controlador;
using SistemaAsistencia.Modelo.DAO;
using SistemaAsistencia.Modelo.Entidades;
using SistemaAsistencia.Vista.Comun;

namespace SistemaAsistencia.Vista.Profesores
{
    public partial class FrmProfesores : Form
    {
        private readonly ProfesorController profesorController;
        private List<Profesor> cacheProfesores = new List<Profesor>();
        private int idProfesorSeleccionado = 0;

        public FrmProfesores()
        {
            InitializeComponent();

            profesorController = new ProfesorController();
        }

        private void FrmProfesores_Load(object sender, EventArgs e)
        {
            try
            {
                CargarProfesores();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo cargar profesores.\n" + ex.Message,
                    "Error de conexión",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void CargarProfesores()
        {
            cacheProfesores =
                profesorController.ObtenerProfesores() ?? new List<Profesor>();
            MostrarEnGrilla(cacheProfesores);
        }

        private void MostrarEnGrilla(List<Profesor> lista)
        {
            dgvProfesores.DataSource = null;
            dgvProfesores.DataSource = lista;

            if (dgvProfesores.Columns.Count > 0)
            {
                if (dgvProfesores.Columns.Contains("IdProfesor"))
                    dgvProfesores.Columns["IdProfesor"].Visible = false;
                if (dgvProfesores.Columns.Contains("NombreProfesor"))
                    dgvProfesores.Columns["NombreProfesor"].HeaderText = "Nombre";
                if (dgvProfesores.Columns.Contains("ApellidoProfesor"))
                    dgvProfesores.Columns["ApellidoProfesor"].HeaderText = "Apellido";
                if (dgvProfesores.Columns.Contains("LegajoProfesor"))
                    dgvProfesores.Columns["LegajoProfesor"].HeaderText = "Legajo";
                if (dgvProfesores.Columns.Contains("CorreoProfesor"))
                    dgvProfesores.Columns["CorreoProfesor"].HeaderText = "Correo";
                if (dgvProfesores.Columns.Contains("TelefonoProfesor"))
                    dgvProfesores.Columns["TelefonoProfesor"].HeaderText = "Teléfono";
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

            try
            {
                Profesor profesor = new Profesor
                {
                    NombreProfesor = txtNombre.Text,
                    ApellidoProfesor = txtApellido.Text,
                    LegajoProfesor = txtLegajo.Text,
                    CorreoProfesor = txtCorreo.Text,
                    TelefonoProfesor = TelefonoLimpio(txtTelefono)
                };

                if (profesorController.AgregarProfesor(profesor))
                {
                    MessageBox.Show("Profesor agregado correctamente.");

                    CargarProfesores();
                    LimpiarCreacion();
                }
                else
                {
                    MessageBox.Show("No se pudo agregar el profesor.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo agregar el profesor.\n" + ex.Message);
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
            txtCorreo.Clear();
            txtTelefono.Clear();
            txtNombre.Focus();
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
                MostrarEnGrilla(cacheProfesores);
                return;
            }

            List<Profesor> resultados = cacheProfesores.FindAll(p =>
                (p.NombreProfesor != null &&
                    p.NombreProfesor.IndexOf(texto, StringComparison.OrdinalIgnoreCase) >= 0) ||
                (p.ApellidoProfesor != null &&
                    p.ApellidoProfesor.IndexOf(texto, StringComparison.OrdinalIgnoreCase) >= 0));

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

        private void dgvProfesores_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            Profesor profesor =
                dgvProfesores.Rows[e.RowIndex].DataBoundItem as Profesor;
            if (profesor != null)
                CargarEnEdicion(profesor);
        }

        private void CargarEnEdicion(Profesor profesor)
        {
            idProfesorSeleccionado = profesor.IdProfesor;
            lblEditando.Text =
                "Editando: " + profesor.ApellidoProfesor + ", " + profesor.NombreProfesor;
            txtEditNombre.Text = profesor.NombreProfesor;
            txtEditApellido.Text = profesor.ApellidoProfesor;
            txtEditLegajo.Text = profesor.LegajoProfesor;
            txtEditCorreo.Text = profesor.CorreoProfesor;
            txtEditTelefono.Text = profesor.TelefonoProfesor;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (idProfesorSeleccionado == 0)
            {
                MessageBox.Show("Busque y seleccione un profesor primero.");
                return;
            }

            if (!TelefonoValido(txtEditTelefono))
            {
                MessageBox.Show(
                    "El teléfono debe estar completo (54 + área de 3 + número) o vacío.");
                txtEditTelefono.Focus();
                return;
            }

            try
            {
                Profesor profesor = new Profesor
                {
                    IdProfesor = idProfesorSeleccionado,
                    NombreProfesor = txtEditNombre.Text,
                    ApellidoProfesor = txtEditApellido.Text,
                    LegajoProfesor = txtEditLegajo.Text,
                    CorreoProfesor = txtEditCorreo.Text,
                    TelefonoProfesor = TelefonoLimpio(txtEditTelefono)
                };

                if (profesorController.ModificarProfesor(profesor))
                {
                    MessageBox.Show("Profesor modificado correctamente.");

                    CargarProfesores();
                    LimpiarEdicion();
                }
                else
                {
                    MessageBox.Show("No se pudo modificar el profesor.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo modificar el profesor.\n" + ex.Message);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idProfesorSeleccionado == 0)
            {
                MessageBox.Show("Busque y seleccione un profesor primero.");
                return;
            }

            Profesor profesor = cacheProfesores.Find(p => p.IdProfesor == idProfesorSeleccionado);
            if (profesor == null) return;

            try
            {
                string descripcion = "Profesor: " + profesor.NombreCompleto +
                    " (Legajo " + profesor.LegajoProfesor + ")";

                List<Dependencia> dependencias =
                    profesorController.ObtenerDependencias(idProfesorSeleccionado);

                using (var confirmar = new FrmConfirmarEliminar(descripcion, dependencias))
                {
                    if (confirmar.ShowDialog(this) != DialogResult.OK) return;

                    bool ok;

                    if (confirmar.Resultado == ResultadoEliminacion.Definitiva)
                    {
                        ok = profesorController.EliminarDefinitivo(idProfesorSeleccionado);
                        if (ok) MessageBox.Show("Profesor eliminado definitivamente.");
                    }
                    else
                    {
                        ok = profesorController.DarDeBaja(idProfesorSeleccionado);
                        if (ok) MessageBox.Show("Profesor dado de baja.");
                    }

                    if (ok)
                    {
                        CargarProfesores();
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
            idProfesorSeleccionado = 0;
            lblEditando.Text = "Editando: (seleccione de la lista)";
            txtEditNombre.Clear();
            txtEditApellido.Clear();
            txtEditLegajo.Clear();
            txtEditCorreo.Clear();
            txtEditTelefono.Clear();
        }
    }
}
