using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SistemaAsistencia.Controlador;
using SistemaAsistencia.Modelo.Entidades;
using SistemaAsistencia.Utilidades;
using SistemaAsistencia.Vista.Comun;

namespace SistemaAsistencia.Vista.Inscripcion
{
    public partial class FrmInscripcion : FrmBaseHijo
    {
        private readonly InscripcionController inscripcionController;
        private readonly DictadoController dictadoController;
        private readonly AlumnoController alumnoController;
        private readonly MateriaController materiaController;
        private readonly EspecialidadController especialidadController;

        private List<Dictado> cacheDictados = new List<Dictado>();
        private List<Materia> cacheMaterias = new List<Materia>();

        // Contador (no flag) para distinguir cambios programáticos
        // (carga, reseteos) de los del usuario. Anidado seguro.
        private int _bloqueo = 0;
        private bool Programando => _bloqueo > 0;

        public FrmInscripcion()
        {
            InitializeComponent();

            inscripcionController = new InscripcionController();
            dictadoController = new DictadoController();
            alumnoController = new AlumnoController();
            materiaController = new MateriaController();
            especialidadController = new EspecialidadController();

            Tema.ConfigurarFondo(this);
            Tema.EstilizarGrilla(dgvInscripciones);

            // Cascada por pasos: Especialidad -> Año -> División -> Grupo -> Dictado.
            // No se puede elegir el siguiente sin el anterior.
            cmbEspecialidad.SelectedIndexChanged += (s, e) => Especialidad_Cambiada();
            nudAnio.ValueChanged += (s, e) => Anio_Cambiado();
            nudDivision.ValueChanged += (s, e) => Division_Cambiada();
            nudGrupo.ValueChanged += (s, e) => Grupo_Cambiado();

            // Arranque bloqueado: se habilita por pasos al elegir.
            nudAnio.Enabled = false;
            nudDivision.Enabled = false;
            nudGrupo.Enabled = false;
            cmbDictado.Enabled = false;
        }

        private void FrmInscripcion_Load(object sender, EventArgs e)
        {
            try
            {
                CargarEspecialidades();
                CargarDatos();
                dgvInscripciones.Rows.Clear();
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

        private void CargarEspecialidades()
        {
            List<Especialidad> lista = especialidadController.ObtenerEspecialidades()
                ?? new List<Especialidad>();

            // Sin "Todas": la cascada exige elegir especialidad (paso 1).
            cmbEspecialidad.DataSource = new List<Especialidad>(lista);
            cmbEspecialidad.DisplayMember = "NombreEspecialidad";
            cmbEspecialidad.ValueMember = "IdEspecialidad";
            cmbEspecialidad.SelectedIndex = -1;
        }

        private void CargarDatos()
        {
            cacheMaterias = materiaController.ObtenerMaterias() ?? new List<Materia>();
            cacheDictados = dictadoController.ObtenerDictados() ?? new List<Dictado>();
        }

        // ---- Cascada por pasos ----

        private void Especialidad_Cambiada()
        {
            bool hayEsp = cmbEspecialidad.SelectedIndex >= 0;

            _bloqueo++;
            try
            {
                if (hayEsp)
                {
                    bool basico = CicloHelper.EsCicloBasico(
                        CicloHelper.NombreEspecialidadDe(cmbEspecialidad));
                    CicloHelper.AplicarRangoAnio(nudAnio,
                        CicloHelper.NombreEspecialidadDe(cmbEspecialidad));
                    nudDivision.Value = nudDivision.Minimum;
                    nudDivision.Maximum = CicloHelper.DivisionMaxima(basico);
                }
                else
                {
                    nudAnio.Maximum = 7;
                    nudAnio.Minimum = 1;
                    nudDivision.Maximum = 7;
                }
                nudAnio.Value = nudAnio.Minimum;
                nudDivision.Value = nudDivision.Minimum;
                nudGrupo.Value = nudGrupo.Minimum;
                cmbDictado.DataSource = null;
                dgvInscripciones.Rows.Clear();
            }
            finally { _bloqueo--; }

            nudAnio.Enabled = hayEsp;
            nudDivision.Enabled = false;
            nudGrupo.Enabled = false;
            cmbDictado.Enabled = false;
        }

        private void Anio_Cambiado()
        {
            if (Programando) return;

            // El usuario eligió Año: se resetea lo de abajo y se abre División.
            _bloqueo++;
            try
            {
                nudDivision.Value = nudDivision.Minimum;
                nudGrupo.Value = nudGrupo.Minimum;
                cmbDictado.DataSource = null;
                dgvInscripciones.Rows.Clear();
            }
            finally { _bloqueo--; }

            nudDivision.Enabled = true;
            nudGrupo.Enabled = false;
            cmbDictado.Enabled = false;
        }

        private void Division_Cambiada()
        {
            if (Programando) return;

            _bloqueo++;
            try
            {
                nudGrupo.Value = nudGrupo.Minimum;
                cmbDictado.DataSource = null;
                dgvInscripciones.Rows.Clear();
            }
            finally { _bloqueo--; }

            nudGrupo.Enabled = true;
            cmbDictado.Enabled = false;
        }

        private void Grupo_Cambiado()
        {
            if (Programando) return;

            _bloqueo++;
            try
            {
                cmbDictado.DataSource = null;
                dgvInscripciones.Rows.Clear();
            }
            finally { _bloqueo--; }

            cmbDictado.Enabled = true;
            CargarDictadosFiltrados();
        }

        private void CargarDictadosFiltrados()
        {
            var esp = cmbEspecialidad.SelectedItem as Especialidad;
            if (esp == null) return;

            int anio = Convert.ToInt32(nudAnio.Value);
            int division = Convert.ToInt32(nudDivision.Value);
            int grupo = Convert.ToInt32(nudGrupo.Value);

            var lista = new List<Dictado>();
            foreach (Dictado d in cacheDictados)
            {
                Materia m = cacheMaterias.Find(x => x.IdMateria == d.IdMateria);
                if (m == null) continue;
                if (m.IdEspecialidad != esp.IdEspecialidad) continue;
                if (m.AnioMateria != anio) continue;
                // Alcance total (NULL) cubre cualquier división/grupo.
                if (d.Division.HasValue && d.Division.Value != division) continue;
                if (d.Grupo.HasValue && d.Grupo.Value != grupo) continue;
                lista.Add(d);
            }

            _bloqueo++;
            try
            {
                cmbDictado.DataSource = null;
                cmbDictado.DisplayMember = "Descripcion";
                cmbDictado.ValueMember = "IdDictado";
                cmbDictado.DataSource = lista;
                cmbDictado.SelectedIndex = -1;
            }
            finally { _bloqueo--; }

            if (lista.Count == 0)
            {
                MessageBox.Show(
                    "Sin dictados para esos filtros.",
                    "Inscripciones",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
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
                MessageBox.Show(
                    "Seleccione un dictado.",
                    "Inscripciones",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                cmbDictado.Focus();
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
                MessageBox.Show(
                    "No hubo cambios en las inscripciones.",
                    "Inscripciones",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(
                    $"Inscripciones actualizadas: {agregados} agregadas, {bajas} bajas.",
                    "Inscripciones",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }

            CargarTabla();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            CargarTabla();
        }
    }
}
