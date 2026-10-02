using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SistemaAsistencia.Controlador;
using SistemaAsistencia.Modelo.DAO;
using SistemaAsistencia.Modelo.Entidades;
using SistemaAsistencia.Vista.Comun;
using SistemaAsistencia.Utilidades;

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

            Tema.ConfigurarFondo(this);
            Tema.EstilizarGrilla(dgvProfesores);

            Tema.EstilizarBoton(btnGuardar, true);
            Tema.EstilizarBoton(btnModificar, true);
            Tema.EstilizarBoton(btnBuscar, false);
            Tema.EstilizarBoton(btnEliminar, false);
            Tema.EstilizarBoton(btnLimpiarCrear, false);
            Tema.EstilizarBoton(btnLimpiarEditar, false);

            // Regla EEST: legajo = DNI (autocompletado, no editable).
            txtLegajo.ReadOnly = true;
            txtLegajo.TabStop = false;
            txtLegajo.BackColor = System.Drawing.SystemColors.Control;
            txtEditLegajo.ReadOnly = true;
            txtEditLegajo.TabStop = false;
            txtEditLegajo.BackColor = System.Drawing.SystemColors.Control;
            txtDni.TextChanged += (s, e) => txtLegajo.Text = txtDni.Text.Trim();
            txtEditDni.TextChanged += (s, e) => txtEditLegajo.Text = txtEditDni.Text.Trim();
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
                string[] soloRelevantes = { "NombreProfesor", "ApellidoProfesor", "DniProfesor", "LegajoProfesor", "CorreoProfesor", "TelefonoProfesor", "Activo" };
                foreach (DataGridViewColumn col in dgvProfesores.Columns)
                    col.Visible = System.Array.IndexOf(soloRelevantes, col.Name) >= 0;

                if (dgvProfesores.Columns.Contains("NombreProfesor"))
                {
                    dgvProfesores.Columns["NombreProfesor"].HeaderText = "Nombre";
                    dgvProfesores.Columns["NombreProfesor"].DisplayIndex = 0;
                }
                if (dgvProfesores.Columns.Contains("ApellidoProfesor"))
                {
                    dgvProfesores.Columns["ApellidoProfesor"].HeaderText = "Apellido";
                    dgvProfesores.Columns["ApellidoProfesor"].DisplayIndex = 1;
                }
                if (dgvProfesores.Columns.Contains("DniProfesor"))
                {
                    dgvProfesores.Columns["DniProfesor"].HeaderText = "DNI";
                    dgvProfesores.Columns["DniProfesor"].DisplayIndex = 2;
                }
                if (dgvProfesores.Columns.Contains("LegajoProfesor"))
                {
                    dgvProfesores.Columns["LegajoProfesor"].HeaderText = "Legajo";
                    dgvProfesores.Columns["LegajoProfesor"].DisplayIndex = 3;
                }
                if (dgvProfesores.Columns.Contains("CorreoProfesor"))
                {
                    dgvProfesores.Columns["CorreoProfesor"].HeaderText = "Correo";
                    dgvProfesores.Columns["CorreoProfesor"].DisplayIndex = 4;
                }
                if (dgvProfesores.Columns.Contains("TelefonoProfesor"))
                {
                    dgvProfesores.Columns["TelefonoProfesor"].HeaderText = "Teléfono";
                    dgvProfesores.Columns["TelefonoProfesor"].DisplayIndex = 5;
                }
                if (dgvProfesores.Columns.Contains("Activo"))
                {
                    dgvProfesores.Columns["Activo"].HeaderText = "Activo";
                    dgvProfesores.Columns["Activo"].DisplayIndex = 6;
                    dgvProfesores.Columns["Activo"].ReadOnly = true;
                }
            }
        }

        // Teléfono en 3 cajas (CtrlTelefono): | 54 | área máx 3 | número |.
        private bool TelefonoValidoSimple(SistemaAsistencia.Vista.Comun.CtrlTelefono ctrl)
        {
            if (!ctrl.EsValido(out string error))
            {
                MessageBox.Show(error);
                ctrl.Enfocar();
                return false;
            }
            return true;
        }

        // ---------------- 1. Crear ----------------

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!TelefonoValidoSimple(ctrlTelCrear))
                return;

            try
            {
                Profesor profesor = new Profesor
                {
                    NombreProfesor = txtNombre.Text,
                    ApellidoProfesor = txtApellido.Text,
                    DniProfesor = txtDni.Text,
                    LegajoProfesor = txtDni.Text.Trim(),
                    CorreoProfesor = txtCorreo.Text,
                    TelefonoProfesor = ctrlTelCrear.Telefono
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
            txtDni.Clear();
            txtLegajo.Clear();
            txtCorreo.Clear();
            ctrlTelCrear.Limpiar();
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
                    p.ApellidoProfesor.IndexOf(texto, StringComparison.OrdinalIgnoreCase) >= 0) ||
                (p.DniProfesor != null &&
                    p.DniProfesor.IndexOf(texto, StringComparison.OrdinalIgnoreCase) >= 0) ||
                (p.LegajoProfesor != null &&
                    p.LegajoProfesor.IndexOf(texto, StringComparison.OrdinalIgnoreCase) >= 0));

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
            txtEditDni.Text = profesor.DniProfesor;
            txtEditLegajo.Text = profesor.LegajoProfesor;
            txtEditCorreo.Text = profesor.CorreoProfesor;
            ctrlTelEdit.Telefono = profesor.TelefonoProfesor;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (idProfesorSeleccionado == 0)
            {
                MessageBox.Show("Busque y seleccione un profesor primero.");
                return;
            }

            if (!TelefonoValidoSimple(ctrlTelEdit))
                return;

            try
            {
                Profesor profesor = new Profesor
                {
                    IdProfesor = idProfesorSeleccionado,
                    NombreProfesor = txtEditNombre.Text,
                    ApellidoProfesor = txtEditApellido.Text,
                    DniProfesor = txtEditDni.Text,
                    LegajoProfesor = txtEditDni.Text.Trim(),
                    CorreoProfesor = txtEditCorreo.Text,
                    TelefonoProfesor = ctrlTelEdit.Telefono
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
            txtEditDni.Clear();
            txtEditLegajo.Clear();
            txtEditCorreo.Clear();
            ctrlTelEdit.Limpiar();
        }
    }
}
