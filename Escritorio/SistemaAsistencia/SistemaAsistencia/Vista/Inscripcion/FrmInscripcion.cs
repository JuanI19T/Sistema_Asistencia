using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SistemaAsistencia.Controlador;
using SistemaAsistencia.Modelo.Entidades;

namespace SistemaAsistencia.Vista.Inscripcion
{
    public partial class FrmInscripcion : Form
    {
        private readonly InscripcionController inscripcionController;
        private readonly DictadoController dictadoController;
        private readonly AlumnoController alumnoController;

        public FrmInscripcion()
        {
            InitializeComponent();

            inscripcionController = new InscripcionController();
            dictadoController = new DictadoController();
            alumnoController = new AlumnoController();
        }

        private void FrmInscripcion_Load(object sender, EventArgs e)
        {
            try
            {
                CargarDictados();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo cargar inscripciones.\n" + ex.Message,
                    "Error de conexión",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void CargarDictados()
        {
            cmbDictado.DataSource = null;
            cmbDictado.DisplayMember = "Descripcion";
            cmbDictado.ValueMember = "IdDictado";
            cmbDictado.DataSource = dictadoController.ObtenerDictados();
        }

        private void cmbDictado_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CargarTabla();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo cargar la tabla.\n" + ex.Message,
                    "Error de conexión",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void CargarTabla()
        {
            dgvInscripciones.Rows.Clear();

            if (cmbDictado.SelectedIndex < 0 ||
                cmbDictado.SelectedValue == null)
            {
                return;
            }

            int idDictado = Convert.ToInt32(cmbDictado.SelectedValue);

            List<int> inscriptos =
                inscripcionController.ObtenerAlumnosInscriptos(idDictado);

            foreach (Alumno alumno in alumnoController.ObtenerAlumnos())
            {
                int fila = dgvInscripciones.Rows.Add();

                dgvInscripciones.Rows[fila].Cells["IdAlumno"].Value = alumno.IdAlumno;
                dgvInscripciones.Rows[fila].Cells["ApellidoAlumno"].Value = alumno.ApellidoAlumno;
                dgvInscripciones.Rows[fila].Cells["NombreAlumno"].Value = alumno.NombreAlumno;
                dgvInscripciones.Rows[fila].Cells["LegajoAlumno"].Value = alumno.LegajoAlumno;
                dgvInscripciones.Rows[fila].Cells["Inscripto"].Value =
                    inscriptos.Contains(alumno.IdAlumno);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (cmbDictado.SelectedIndex < 0)
            {
                MessageBox.Show("Seleccione un dictado.");
                return;
            }

            int idDictado = Convert.ToInt32(cmbDictado.SelectedValue);
            int anioInicio = DateTime.Now.Year;

            List<int> inscriptosActuales =
                inscripcionController.ObtenerAlumnosInscriptos(idDictado);

            int agregados = 0;
            int bajas = 0;

            foreach (DataGridViewRow fila in dgvInscripciones.Rows)
            {
                int idAlumno = Convert.ToInt32(fila.Cells["IdAlumno"].Value);
                bool marcado = Convert.ToBoolean(fila.Cells["Inscripto"].Value);

                if (marcado && !inscriptosActuales.Contains(idAlumno))
                {
                    if (inscripcionController.AgregarInscripcion(
                            idAlumno, idDictado, anioInicio))
                    {
                        agregados++;
                    }
                }
                else if (!marcado && inscriptosActuales.Contains(idAlumno))
                {
                    if (inscripcionController.EliminarInscripcion(idAlumno, idDictado))
                    {
                        bajas++;
                    }
                }
            }

            if (agregados == 0 && bajas == 0)
            {
                MessageBox.Show("No hubo cambios en las inscripciones.");
            }
            else
            {
                MessageBox.Show(
                    $"Inscripciones actualizadas: {agregados} agregadas, {bajas} bajas.");
            }

            CargarTabla();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            CargarTabla();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            DialogResult resp = MessageBox.Show(
                "Cerrar sistema, ¿confirma?",
                "Sistema Asistencia",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);

            if (resp == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}