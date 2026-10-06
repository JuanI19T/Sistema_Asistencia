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

        private bool _cargando = false;

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
            Tema.EstilizarGrilla(dgvDictados);

            // Vinculación de eventos
            cmbEspecialidad.SelectedIndexChanged += CmbEspecialidad_SelectedIndexChanged;
            nudAnio.ValueChanged += NudAnio_ValueChanged;
            nudDivision.ValueChanged += NudFiltro_ValueChanged;
            nudGrupo.ValueChanged += NudFiltro_ValueChanged;
            dgvDictados.SelectionChanged += DgvDictados_SelectionChanged;

            // Estado inicial
            nudAnio.Enabled = false;
            nudDivision.Enabled = false;
            nudGrupo.Enabled = false;
            dgvDictados.Enabled = false;
        }

        private void FrmInscripcion_Load(object sender, EventArgs e)
        {
            CargaVista.IntentarCarga(() =>
            {
                ConfigurarGrillaDictados();
                CargarEspecialidades();
                CargarDatos();
                dgvInscripciones.Rows.Clear();
            }, "inscripciones");
        }

        private void ConfigurarGrillaDictados()
        {
            if (dgvDictados.Columns.Count == 0)
            {
                dgvDictados.Columns.Add("IdDictado", "IdDictado");
                dgvDictados.Columns["IdDictado"].Visible = false;
                dgvDictados.Columns.Add("Materia", "Materia");
                dgvDictados.Columns.Add("Anio", "Año");
                dgvDictados.Columns.Add("Division", "División");
                dgvDictados.Columns.Add("Grupo", "Grupo");
            }
        }

        private void CargarEspecialidades()
        {
            _cargando = true;
            try
            {
                List<Especialidad> lista = CargaVista.ObtenerLista(
                    () => especialidadController.ObtenerEspecialidades());
                CargaVista.CargarCombo(cmbEspecialidad, lista, "NombreEspecialidad", "IdEspecialidad");
            }
            finally
            {
                _cargando = false;
            }
        }

        private void CargarDatos()
        {
            cacheMaterias = CargaVista.ObtenerLista(() => materiaController.ObtenerMaterias()) ?? new List<Materia>();
            cacheDictados = CargaVista.ObtenerLista(() => dictadoController.ObtenerDictados()) ?? new List<Dictado>();
        }

        private void CmbEspecialidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_cargando) return;

            _cargando = true;
            try
            {
                bool hayEsp = cmbEspecialidad.SelectedIndex >= 0;
                if (hayEsp)
                {
                    string nomEsp = CicloHelper.NombreEspecialidadDe(cmbEspecialidad);
                    bool basico = CicloHelper.EsCicloBasico(nomEsp);

                    CicloHelper.AplicarRangoAnio(nudAnio, nomEsp);
                    nudDivision.Maximum = CicloHelper.DivisionMaxima(basico);
                }

                nudAnio.Value = nudAnio.Minimum;
                nudDivision.Value = nudDivision.Minimum;
                nudGrupo.Value = nudGrupo.Minimum;

                dgvDictados.Rows.Clear();
                dgvInscripciones.Rows.Clear();

                nudAnio.Enabled = hayEsp;
                nudDivision.Enabled = false;
                nudGrupo.Enabled = false;
                dgvDictados.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cambiar especialidad: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _cargando = false;
            }
        }

        private void NudAnio_ValueChanged(object sender, EventArgs e)
        {
            if (_cargando) return;

            _cargando = true;
            try
            {
                nudDivision.Value = nudDivision.Minimum;
                nudGrupo.Value = nudGrupo.Minimum;

                nudDivision.Enabled = true;
                nudGrupo.Enabled = true;
                dgvDictados.Enabled = true;
            }
            finally
            {
                _cargando = false;
            }

            FiltrarDictados();
        }

        private void NudFiltro_ValueChanged(object sender, EventArgs e)
        {
            if (_cargando) return;
            FiltrarDictados();
        }

        private void FiltrarDictados()
        {
            if (_cargando) return;

            _cargando = true;
            try
            {
                dgvDictados.Rows.Clear();
                dgvInscripciones.Rows.Clear();

                var esp = cmbEspecialidad.SelectedItem as Especialidad;
                if (esp == null || !nudAnio.Enabled) return;

                int anio = Convert.ToInt32(nudAnio.Value);
                int division = Convert.ToInt32(nudDivision.Value);
                int grupo = Convert.ToInt32(nudGrupo.Value);

                foreach (Dictado d in cacheDictados)
                {
                    Materia m = cacheMaterias.Find(x => x.IdMateria == d.IdMateria);
                    if (m == null) continue;
                    if (m.IdEspecialidad != esp.IdEspecialidad) continue;
                    if (m.AnioMateria != anio) continue;

                    // Filtro por División (0 = Muestra solo las dictadas para "Todas", o el número exacto)
                    if (division > (int)nudDivision.Minimum)
                    {
                        int divDictado = d.Division.HasValue ? d.Division.Value : 0;
                        if (divDictado != division)
                            continue;
                    }

                    // Filtro estricto por Grupo:
                    // grupo = 0 -> muestra solo dictados asignados a 0/null ("Todos")
                    // grupo = 1 -> muestra solo dictados asignados a 1
                    // grupo = 2 -> muestra solo dictados asignados a 2
                    int grupoDictado = d.Grupo.HasValue ? d.Grupo.Value : 0;
                    if (grupoDictado != grupo)
                    {
                        continue;
                    }

                    int fila = dgvDictados.Rows.Add();
                    dgvDictados.Rows[fila].Cells["IdDictado"].Value = d.IdDictado;
                    dgvDictados.Rows[fila].Cells["Materia"].Value = m.NombreMateria ?? "Sin nombre";
                    dgvDictados.Rows[fila].Cells["Anio"].Value = m.AnioMateria;
                    dgvDictados.Rows[fila].Cells["Division"].Value = (!d.Division.HasValue || d.Division.Value == 0) ? "Todas" : d.Division.Value.ToString();
                    dgvDictados.Rows[fila].Cells["Grupo"].Value = (!d.Grupo.HasValue || d.Grupo.Value == 0) ? "Todos" : d.Grupo.Value.ToString();
                }

                dgvDictados.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al filtrar dictados: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _cargando = false;
            }
        }

        private void DgvDictados_SelectionChanged(object sender, EventArgs e)
        {
            if (_cargando) return;

            if (dgvDictados.SelectedRows.Count == 0 && dgvDictados.CurrentRow == null)
            {
                dgvInscripciones.Rows.Clear();
                return;
            }

            CargarTablaAlumnos();
        }

        private void CargarTablaAlumnos()
        {
            dgvInscripciones.Rows.Clear();

            if (dgvDictados.CurrentRow == null || dgvDictados.CurrentRow.Cells["IdDictado"].Value == null)
                return;

            if (!int.TryParse(dgvDictados.CurrentRow.Cells["IdDictado"].Value.ToString(), out int idDictado))
                return;

            try
            {
                List<int> inscriptos = inscripcionController.ObtenerAlumnosInscriptos(idDictado) ?? new List<int>();

                foreach (Alumno alumno in alumnoController.ObtenerAlumnos())
                {
                    int fila = dgvInscripciones.Rows.Add();
                    dgvInscripciones.Rows[fila].Cells["IdAlumno"].Value = alumno.IdAlumno;
                    dgvInscripciones.Rows[fila].Cells["ApellidoAlumno"].Value = alumno.ApellidoAlumno;
                    dgvInscripciones.Rows[fila].Cells["NombreAlumno"].Value = alumno.NombreAlumno;
                    dgvInscripciones.Rows[fila].Cells["LegajoAlumno"].Value = alumno.LegajoAlumno;
                    dgvInscripciones.Rows[fila].Cells["Inscripto"].Value = inscriptos.Contains(alumno.IdAlumno);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar alumnos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (dgvDictados.CurrentRow == null || dgvDictados.CurrentRow.Cells["IdDictado"].Value == null)
            {
                MessageBox.Show("Seleccione un dictado de la lista.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idDictadoVal = Convert.ToInt32(dgvDictados.CurrentRow.Cells["IdDictado"].Value);
            int anioInicio = DateTime.Now.Year;

            List<int> inscriptosActuales = inscripcionController.ObtenerAlumnosInscriptos(idDictadoVal) ?? new List<int>();

            int agregados = 0;
            int bajas = 0;

            foreach (DataGridViewRow fila in dgvInscripciones.Rows)
            {
                if (fila.IsNewRow) continue;

                int idAlumno = Convert.ToInt32(fila.Cells["IdAlumno"].Value);
                bool marcado = Convert.ToBoolean(fila.Cells["Inscripto"].Value);

                if (marcado && !inscriptosActuales.Contains(idAlumno))
                {
                    if (inscripcionController.AgregarInscripcion(idAlumno, idDictadoVal, anioInicio))
                        agregados++;
                }
                else if (!marcado && inscriptosActuales.Contains(idAlumno))
                {
                    if (inscripcionController.EliminarInscripcion(idAlumno, idDictadoVal))
                        bajas++;
                }
            }

            MessageBox.Show($"Inscripciones actualizadas: {agregados} agregadas, {bajas} bajas.", "Inscripciones", MessageBoxButtons.OK, MessageBoxIcon.Information);
            CargarTablaAlumnos();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            CargarTablaAlumnos();
        }
    }
}