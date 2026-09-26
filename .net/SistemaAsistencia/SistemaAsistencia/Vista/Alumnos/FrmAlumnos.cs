using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SistemaAsistencia.Controlador;
using SistemaAsistencia.Modelo.DAO;
using SistemaAsistencia.Modelo.Entidades;
using SistemaAsistencia.Vista.Comun;

namespace SistemaAsistencia.Vista.Alumnos
{
    public partial class FrmAlumnos : Form
    {
        private readonly AlumnoController alumnoController;
        private List<Alumno> cacheAlumnos = new List<Alumno>();
        private int idAlumnoSeleccionado = 0;

        public FrmAlumnos()
        {
            InitializeComponent();

            alumnoController = new AlumnoController();
        }

        private void FrmAlumnos_Load(object sender, EventArgs e)
        {
            try
            {
                CargarAlumnos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo cargar alumnos.\n" + ex.Message,
                    "Error de conexión",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void CargarAlumnos()
        {
            cacheAlumnos = alumnoController.ObtenerAlumnos() ?? new List<Alumno>();
            MostrarEnGrilla(cacheAlumnos);
        }

        private void MostrarEnGrilla(List<Alumno> lista)
        {
            dgvAlumnos.DataSource = null;
            dgvAlumnos.DataSource = lista;

            if (dgvAlumnos.Columns.Count > 0)
            {
                if (dgvAlumnos.Columns.Contains("IdAlumno"))
                    dgvAlumnos.Columns["IdAlumno"].Visible = false;
                if (dgvAlumnos.Columns.Contains("NombreAlumno"))
                    dgvAlumnos.Columns["NombreAlumno"].HeaderText = "Nombre";
                if (dgvAlumnos.Columns.Contains("ApellidoAlumno"))
                    dgvAlumnos.Columns["ApellidoAlumno"].HeaderText = "Apellido";
                if (dgvAlumnos.Columns.Contains("LegajoAlumno"))
                    dgvAlumnos.Columns["LegajoAlumno"].HeaderText = "Legajo";
                if (dgvAlumnos.Columns.Contains("CorreoAlumno"))
                    dgvAlumnos.Columns["CorreoAlumno"].HeaderText = "Correo";
                if (dgvAlumnos.Columns.Contains("TelefonoAlumno"))
                    dgvAlumnos.Columns["TelefonoAlumno"].HeaderText = "Teléfono";
                if (dgvAlumnos.Columns.Contains("TelefonoEmergencia"))
                    dgvAlumnos.Columns["TelefonoEmergencia"].HeaderText = "Tel. Emergencia";
                if (dgvAlumnos.Columns.Contains("TelefonoPadre"))
                    dgvAlumnos.Columns["TelefonoPadre"].HeaderText = "Tel. Padre";
                if (dgvAlumnos.Columns.Contains("TelefonoMadre"))
                    dgvAlumnos.Columns["TelefonoMadre"].HeaderText = "Tel. Madre";
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

        private bool TelefonosValidos(
            MaskedTextBox tel, MaskedTextBox emg, MaskedTextBox padre, MaskedTextBox madre)
        {
            if (!TelefonoValido(tel) || !TelefonoValido(emg) ||
                !TelefonoValido(padre) || !TelefonoValido(madre))
            {
                MessageBox.Show(
                    "Los teléfonos deben estar completos (54 + área de 3 + número) o vacíos.");
                return false;
            }
            return true;
        }

        // ---------------- 1. Crear ----------------

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!TelefonosValidos(txtTelefono, txtTelEmergencia, txtTelPadre, txtTelMadre))
                return;

            try
            {
                Alumno alumno = new Alumno
                {
                    NombreAlumno = txtNombre.Text,
                    ApellidoAlumno = txtApellido.Text,
                    LegajoAlumno = txtLegajo.Text,
                    CorreoAlumno = txtCorreo.Text,
                    TelefonoAlumno = TelefonoLimpio(txtTelefono),
                    TelefonoEmergencia = TelefonoLimpio(txtTelEmergencia),
                    TelefonoPadre = TelefonoLimpio(txtTelPadre),
                    TelefonoMadre = TelefonoLimpio(txtTelMadre)
                };

                if (alumnoController.AgregarAlumno(alumno))
                {
                    MessageBox.Show("Alumno agregado correctamente.");

                    CargarAlumnos();
                    LimpiarCreacion();
                }
                else
                {
                    MessageBox.Show("No se pudo agregar el alumno.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo agregar el alumno.\n" + ex.Message);
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
            txtTelEmergencia.Clear();
            txtTelPadre.Clear();
            txtTelMadre.Clear();
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
                MostrarEnGrilla(cacheAlumnos);
                return;
            }

            List<Alumno> resultados = cacheAlumnos.FindAll(a =>
                (a.NombreAlumno != null &&
                    a.NombreAlumno.IndexOf(texto, StringComparison.OrdinalIgnoreCase) >= 0) ||
                (a.ApellidoAlumno != null &&
                    a.ApellidoAlumno.IndexOf(texto, StringComparison.OrdinalIgnoreCase) >= 0) ||
                (a.LegajoAlumno != null &&
                    a.LegajoAlumno.IndexOf(texto, StringComparison.OrdinalIgnoreCase) >= 0));

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

        private void dgvAlumnos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            Alumno alumno = dgvAlumnos.Rows[e.RowIndex].DataBoundItem as Alumno;
            if (alumno != null)
                CargarEnEdicion(alumno);
        }

        private void CargarEnEdicion(Alumno alumno)
        {
            idAlumnoSeleccionado = alumno.IdAlumno;
            lblEditando.Text =
                "Editando: " + alumno.ApellidoAlumno + ", " + alumno.NombreAlumno;
            txtEditNombre.Text = alumno.NombreAlumno;
            txtEditApellido.Text = alumno.ApellidoAlumno;
            txtEditLegajo.Text = alumno.LegajoAlumno;
            txtEditCorreo.Text = alumno.CorreoAlumno;
            txtEditTelefono.Text = alumno.TelefonoAlumno;
            txtEditTelEmergencia.Text = alumno.TelefonoEmergencia;
            txtEditTelPadre.Text = alumno.TelefonoPadre;
            txtEditTelMadre.Text = alumno.TelefonoMadre;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (idAlumnoSeleccionado == 0)
            {
                MessageBox.Show("Busque y seleccione un alumno primero.");
                return;
            }

            if (!TelefonosValidos(
                    txtEditTelefono, txtEditTelEmergencia, txtEditTelPadre, txtEditTelMadre))
                return;

            try
            {
                Alumno alumno = new Alumno
                {
                    IdAlumno = idAlumnoSeleccionado,
                    NombreAlumno = txtEditNombre.Text,
                    ApellidoAlumno = txtEditApellido.Text,
                    LegajoAlumno = txtEditLegajo.Text,
                    CorreoAlumno = txtEditCorreo.Text,
                    TelefonoAlumno = TelefonoLimpio(txtEditTelefono),
                    TelefonoEmergencia = TelefonoLimpio(txtEditTelEmergencia),
                    TelefonoPadre = TelefonoLimpio(txtEditTelPadre),
                    TelefonoMadre = TelefonoLimpio(txtEditTelMadre)
                };

                if (alumnoController.ModificarAlumno(alumno))
                {
                    MessageBox.Show("Alumno modificado correctamente.");

                    CargarAlumnos();
                    LimpiarEdicion();
                }
                else
                {
                    MessageBox.Show("No se pudo modificar el alumno.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo modificar el alumno.\n" + ex.Message);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idAlumnoSeleccionado == 0)
            {
                MessageBox.Show("Busque y seleccione un alumno primero.");
                return;
            }

            Alumno alumno = cacheAlumnos.Find(a => a.IdAlumno == idAlumnoSeleccionado);
            if (alumno == null) return;

            try
            {
                string descripcion = "Alumno: " + alumno.ApellidoAlumno + ", " +
                    alumno.NombreAlumno + " (Legajo " + alumno.LegajoAlumno + ")";

                List<Dependencia> dependencias =
                    alumnoController.ObtenerDependencias(idAlumnoSeleccionado);

                using (var confirmar = new FrmConfirmarEliminar(descripcion, dependencias))
                {
                    if (confirmar.ShowDialog(this) != DialogResult.OK) return;

                    bool ok;

                    if (confirmar.Resultado == ResultadoEliminacion.Definitiva)
                    {
                        ok = alumnoController.EliminarDefinitivo(idAlumnoSeleccionado);
                        if (ok) MessageBox.Show("Alumno eliminado definitivamente.");
                    }
                    else
                    {
                        ok = alumnoController.DarDeBaja(idAlumnoSeleccionado);
                        if (ok) MessageBox.Show("Alumno dado de baja.");
                    }

                    if (ok)
                    {
                        CargarAlumnos();
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
            idAlumnoSeleccionado = 0;
            lblEditando.Text = "Editando: (seleccione de la lista)";
            txtEditNombre.Clear();
            txtEditApellido.Clear();
            txtEditLegajo.Clear();
            txtEditCorreo.Clear();
            txtEditTelefono.Clear();
            txtEditTelEmergencia.Clear();
            txtEditTelPadre.Clear();
            txtEditTelMadre.Clear();
        }
    }
}
