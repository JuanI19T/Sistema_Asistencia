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
        private readonly PreceptorController preceptorController;

        private List<Dictado> cacheDictados = new List<Dictado>();
        private List<Materia> cacheMaterias = new List<Materia>();
        private List<Alumno> cacheAlumnos = new List<Alumno>();
        private List<Preceptor> cachePreceptores = new List<Preceptor>();
        private List<int> inscriptosActuales = new List<int>();
        private int? preceptorAsignadoActual = null;

        private bool _cargando = false;

        public FrmInscripcion()
        {
            InitializeComponent();

            inscripcionController = new InscripcionController();
            dictadoController = new DictadoController();
            alumnoController = new AlumnoController();
            materiaController = new MateriaController();
            especialidadController = new EspecialidadController();
            preceptorController = new PreceptorController();

            Tema.ConfigurarFondo(this);
            Tema.EstilizarGrilla(dgvInscripciones);
            Tema.EstilizarGrilla(dgvDictados);
            Tema.EstilizarGrilla(dgvDictadosPrec);
            Tema.EstilizarGrilla(dgvPreceptores);

            // Vinculación de eventos
            cmbEspecialidad.SelectedIndexChanged += CmbEspecialidad_SelectedIndexChanged;
            nudAnio.ValueChanged += NudAnio_ValueChanged;
            nudDivision.ValueChanged += NudFiltro_ValueChanged;
            nudGrupo.ValueChanged += NudFiltro_ValueChanged;
            dgvDictados.SelectionChanged += DgvDictados_SelectionChanged;
            txtBuscarAlumno.TextChanged += txtBuscarAlumno_TextChanged;
            btnLimpiar.Click += btnLimpiar_Click;
            btnToggleInscripcion.Click += btnToggleInscripcion_Click;
            dgvInscripciones.SelectionChanged += DgvInscripciones_SelectionChanged;

            // Vinculación de eventos (pestaña preceptores)
            cmbEspecialidadPrec.SelectedIndexChanged += CmbEspecialidadPrec_SelectedIndexChanged;
            nudAnioPrec.ValueChanged += NudAnioPrec_ValueChanged;
            nudDivisionPrec.ValueChanged += NudFiltroPrec_ValueChanged;
            nudGrupoPrec.ValueChanged += NudFiltroPrec_ValueChanged;
            dgvDictadosPrec.SelectionChanged += DgvDictadosPrec_SelectionChanged;
            txtBuscarPreceptor.TextChanged += txtBuscarPreceptor_TextChanged;
            btnLimpiarPrec.Click += btnLimpiarPrec_Click;
            btnTogglePreceptor.Click += btnTogglePreceptor_Click;
            dgvPreceptores.SelectionChanged += DgvPreceptores_SelectionChanged;

            // Estado inicial
            nudAnio.Enabled = false;
            nudDivision.Enabled = false;
            nudGrupo.Enabled = false;
            dgvDictados.Enabled = false;

            // Estado inicial (pestaña preceptores)
            nudAnioPrec.Enabled = false;
            nudDivisionPrec.Enabled = false;
            nudGrupoPrec.Enabled = false;
            dgvDictadosPrec.Enabled = false;
        }

        private void FrmInscripcion_Load(object sender, EventArgs e)
        {
            CargaVista.IntentarCarga(() =>
            {
                ConfigurarGrillaDictados();
                ConfigurarGrillaPreceptores();
                CargarEspecialidades();
                CargarEspecialidadesPrec();
                CargarDatos();
                dgvInscripciones.Rows.Clear();
                dgvPreceptores.Rows.Clear();
            }, "inscripciones");
            EstadoActivoHelper.AplicarPermiso(btnTogglePreceptor);
            ActualizarTextoTogglePrec();
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
            cacheAlumnos = CargaVista.ObtenerLista(() => alumnoController.ObtenerAlumnos()) ?? new List<Alumno>();
            cachePreceptores = CargaVista.ObtenerLista(() => preceptorController.ObtenerPreceptores()) ?? new List<Preceptor>();
        }

        // División fija de la especialidad elegida (NULL = libre).
        private int? DivisionFija()
        {
            return CicloHelper.DivisionDeEspecialidad(
                cmbEspecialidad.SelectedItem as Especialidad);
        }

        private void CmbEspecialidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_cargando) return;

            bool hayEsp = false;
            int? divFija = null;
            _cargando = true;
            try
            {
                hayEsp = cmbEspecialidad.SelectedIndex >= 0;
                if (hayEsp)
                {
                    string nomEsp = CicloHelper.NombreEspecialidadDe(cmbEspecialidad);
                    bool basico = CicloHelper.EsCicloBasico(nomEsp);

                    CicloHelper.AplicarRangoAnio(nudAnio, nomEsp);
                    nudDivision.Maximum = CicloHelper.DivisionMaxima(basico);
                    divFija = DivisionFija();
                }

                nudAnio.Value = nudAnio.Minimum;
                nudDivision.Value = divFija ?? nudDivision.Minimum;
                nudGrupo.Value = nudGrupo.Minimum;

                dgvDictados.Rows.Clear();
                dgvInscripciones.Rows.Clear();
                inscriptosActuales = new List<int>();

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

            // El año ya quedó en un valor válido (mínimo del rango): se avanza
            // la cascada y se filtra sin exigir que el usuario mueva el nud.
            // Con división fija, División queda bloqueada con su valor.
            if (hayEsp)
            {
                nudDivision.Enabled = !divFija.HasValue;
                nudGrupo.Enabled = true;
                dgvDictados.Enabled = true;
                FiltrarDictados();
            }
        }

        private void NudAnio_ValueChanged(object sender, EventArgs e)
        {
            if (_cargando) return;

            int? divFijaAnio = DivisionFija();
            _cargando = true;
            try
            {
                nudDivision.Value = divFijaAnio ?? nudDivision.Minimum;
                nudGrupo.Value = nudGrupo.Minimum;

                nudDivision.Enabled = !divFijaAnio.HasValue;
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
                inscriptosActuales = new List<int>();

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
                inscriptosActuales = new List<int>();
                return;
            }

            CargarTablaAlumnos();
        }

        // ---------------- Alumnos del dictado + búsqueda ----------------

        private void CargarTablaAlumnos()
        {
            dgvInscripciones.Rows.Clear();

            if (dgvDictados.CurrentRow == null || dgvDictados.CurrentRow.Cells["IdDictado"].Value == null)
            {
                inscriptosActuales = new List<int>();
                return;
            }

            if (!int.TryParse(dgvDictados.CurrentRow.Cells["IdDictado"].Value.ToString(), out int idDictado))
            {
                inscriptosActuales = new List<int>();
                return;
            }

            try
            {
                inscriptosActuales = inscripcionController.ObtenerAlumnosInscriptos(idDictado) ?? new List<int>();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar alumnos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                inscriptosActuales = new List<int>();
                return;
            }

            // Respeta el texto de búsqueda vigente.
            AplicarFiltroAlumnos(interactivo: false);
        }

        private void MostrarAlumnos(List<Alumno> lista)
        {
            dgvInscripciones.Rows.Clear();
            if (lista == null) return;

            foreach (Alumno alumno in lista)
            {
                int fila = dgvInscripciones.Rows.Add();
                dgvInscripciones.Rows[fila].Cells["IdAlumno"].Value = alumno.IdAlumno;
                dgvInscripciones.Rows[fila].Cells["ApellidoAlumno"].Value = alumno.ApellidoAlumno;
                dgvInscripciones.Rows[fila].Cells["NombreAlumno"].Value = alumno.NombreAlumno;
                dgvInscripciones.Rows[fila].Cells["LegajoAlumno"].Value = alumno.LegajoAlumno;
                dgvInscripciones.Rows[fila].Cells["Inscripto"].Value = inscriptosActuales.Contains(alumno.IdAlumno);
            }

            dgvInscripciones.ClearSelection();
            ActualizarTextoToggle();
        }

        private void txtBuscarAlumno_TextChanged(object sender, EventArgs e)
        {
            if (_cargando) return;
            AplicarFiltroAlumnos(interactivo: false);
        }

        private void AplicarFiltroAlumnos(bool interactivo)
        {
            string texto = txtBuscarAlumno.Text.Trim();

            if (string.IsNullOrWhiteSpace(texto))
            {
                MostrarAlumnos(cacheAlumnos);
                return;
            }

            List<Alumno> resultados = cacheAlumnos.FindAll(a =>
                (a.NombreAlumno != null &&
                    a.NombreAlumno.IndexOf(texto, StringComparison.OrdinalIgnoreCase) >= 0) ||
                (a.ApellidoAlumno != null &&
                    a.ApellidoAlumno.IndexOf(texto, StringComparison.OrdinalIgnoreCase) >= 0) ||
                (a.DniAlumno != null &&
                    a.DniAlumno.IndexOf(texto, StringComparison.OrdinalIgnoreCase) >= 0) ||
                (a.LegajoAlumno != null &&
                    a.LegajoAlumno.IndexOf(texto, StringComparison.OrdinalIgnoreCase) >= 0));

            MostrarAlumnos(resultados);

            if (interactivo && resultados.Count == 0)
            {
                MessageBox.Show(
                    "Sin resultados para esa búsqueda.",
                    "Inscripciones",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void DgvInscripciones_SelectionChanged(object sender, EventArgs e)
        {
            if (_cargando) return;
            ActualizarTextoToggle();
        }

        private void ActualizarTextoToggle()
        {
            const string neutro = "Asignar/Quitar";

            if (dgvInscripciones.CurrentRow == null ||
                dgvInscripciones.CurrentRow.Cells["Inscripto"].Value == null)
            {
                btnToggleInscripcion.Text = neutro;
                return;
            }

            bool inscripto = Convert.ToBoolean(dgvInscripciones.CurrentRow.Cells["Inscripto"].Value);
            btnToggleInscripcion.Text = inscripto ? "Dar de baja" : "Inscribir";
        }

        // ---------------- Limpiar ----------------

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            _cargando = true;
            try
            {
                // ReiniciarCombo dispara SelectedIndexChanged, pero con
                // _cargando en true el handler vuelve temprano.
                CargaVista.ReiniciarCombo(cmbEspecialidad);
                nudAnio.Value = nudAnio.Minimum;
                nudDivision.Value = nudDivision.Minimum;
                nudGrupo.Value = nudGrupo.Minimum;
                txtBuscarAlumno.Clear();
                dgvDictados.Rows.Clear();
                dgvInscripciones.Rows.Clear();
                inscriptosActuales = new List<int>();
            }
            finally
            {
                _cargando = false;
            }

            nudAnio.Enabled = false;
            nudDivision.Enabled = false;
            nudGrupo.Enabled = false;
            dgvDictados.Enabled = false;
        }

        // ---------------- Asignar / Desasignar (inmediato, fila seleccionada) ----------------

        private void btnToggleInscripcion_Click(object sender, EventArgs e)
        {
            if (dgvDictados.CurrentRow == null || dgvDictados.CurrentRow.Cells["IdDictado"].Value == null)
            {
                MessageBox.Show("Seleccione un dictado de la lista.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgvInscripciones.CurrentRow == null || dgvInscripciones.CurrentRow.Cells["IdAlumno"].Value == null)
            {
                MessageBox.Show("Seleccione un alumno de la lista.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idDictado = Convert.ToInt32(dgvDictados.CurrentRow.Cells["IdDictado"].Value);
            int idAlumno = Convert.ToInt32(dgvInscripciones.CurrentRow.Cells["IdAlumno"].Value);
            bool inscripto = Convert.ToBoolean(dgvInscripciones.CurrentRow.Cells["Inscripto"].Value ?? false);
            string nombreAlumno = Convert.ToString(dgvInscripciones.CurrentRow.Cells["ApellidoAlumno"].Value)
                + ", " + Convert.ToString(dgvInscripciones.CurrentRow.Cells["NombreAlumno"].Value);

            bool ok;
            if (inscripto)
                ok = inscripcionController.EliminarInscripcion(idAlumno, idDictado);
            else
                ok = inscripcionController.AgregarInscripcion(idAlumno, idDictado, DateTime.Now.Year);

            if (!ok)
            {
                MessageBox.Show(
                    "No se pudo actualizar la inscripción.",
                    "Inscripciones",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show(
                nombreAlumno + (inscripto ? " dado de baja del dictado." : " inscripto correctamente."),
                "Inscripciones",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            // Recarga manteniendo la búsqueda vigente.
            CargarTablaAlumnos();
        }

        // ---------------- Pestaña preceptores ----------------

        private void ConfigurarGrillaPreceptores()
        {
            if (dgvPreceptores.Columns.Count == 0)
            {
                dgvPreceptores.Columns.Add("IdPreceptor", "IdPreceptor");
                dgvPreceptores.Columns["IdPreceptor"].Visible = false;
                dgvPreceptores.Columns.Add("ApellidoPreceptor", "Apellido");
                dgvPreceptores.Columns["ApellidoPreceptor"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvPreceptores.Columns.Add("NombrePreceptor", "Nombre");
                dgvPreceptores.Columns["NombrePreceptor"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvPreceptores.Columns.Add("LegajoPreceptor", "Legajo");
                var colAsignado = new DataGridViewCheckBoxColumn();
                colAsignado.Name = "Asignado";
                colAsignado.HeaderText = "Asignado";
                dgvPreceptores.Columns.Add(colAsignado);
            }
        }

        private void CargarEspecialidadesPrec()
        {
            _cargando = true;
            try
            {
                List<Especialidad> lista = CargaVista.ObtenerLista(
                    () => especialidadController.ObtenerEspecialidades());
                CargaVista.CargarCombo(cmbEspecialidadPrec, lista, "NombreEspecialidad", "IdEspecialidad");
            }
            finally
            {
                _cargando = false;
            }
        }

        // División fija de la especialidad elegida (NULL = libre).
        private int? DivisionFijaPrec()
        {
            return CicloHelper.DivisionDeEspecialidad(
                cmbEspecialidadPrec.SelectedItem as Especialidad);
        }

        private void CmbEspecialidadPrec_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_cargando) return;

            bool hayEsp = false;
            int? divFija = null;
            _cargando = true;
            try
            {
                hayEsp = cmbEspecialidadPrec.SelectedIndex >= 0;
                if (hayEsp)
                {
                    string nomEsp = CicloHelper.NombreEspecialidadDe(cmbEspecialidadPrec);
                    bool basico = CicloHelper.EsCicloBasico(nomEsp);

                    CicloHelper.AplicarRangoAnio(nudAnioPrec, nomEsp);
                    nudDivisionPrec.Maximum = CicloHelper.DivisionMaxima(basico);
                    divFija = DivisionFijaPrec();
                }

                nudAnioPrec.Value = nudAnioPrec.Minimum;
                nudDivisionPrec.Value = divFija ?? nudDivisionPrec.Minimum;
                nudGrupoPrec.Value = nudGrupoPrec.Minimum;

                dgvDictadosPrec.Rows.Clear();
                dgvPreceptores.Rows.Clear();
                preceptorAsignadoActual = null;

                nudAnioPrec.Enabled = hayEsp;
                nudDivisionPrec.Enabled = false;
                nudGrupoPrec.Enabled = false;
                dgvDictadosPrec.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cambiar especialidad: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _cargando = false;
            }

            if (hayEsp)
            {
                nudDivisionPrec.Enabled = !divFija.HasValue;
                nudGrupoPrec.Enabled = true;
                dgvDictadosPrec.Enabled = true;
                FiltrarDictadosPrec();
            }
        }

        private void NudAnioPrec_ValueChanged(object sender, EventArgs e)
        {
            if (_cargando) return;

            int? divFijaAnio = DivisionFijaPrec();
            _cargando = true;
            try
            {
                nudDivisionPrec.Value = divFijaAnio ?? nudDivisionPrec.Minimum;
                nudGrupoPrec.Value = nudGrupoPrec.Minimum;

                nudDivisionPrec.Enabled = !divFijaAnio.HasValue;
                nudGrupoPrec.Enabled = true;
                dgvDictadosPrec.Enabled = true;
            }
            finally
            {
                _cargando = false;
            }

            FiltrarDictadosPrec();
        }

        private void NudFiltroPrec_ValueChanged(object sender, EventArgs e)
        {
            if (_cargando) return;
            FiltrarDictadosPrec();
        }

        private void FiltrarDictadosPrec()
        {
            if (_cargando) return;

            _cargando = true;
            try
            {
                dgvDictadosPrec.Rows.Clear();
                dgvPreceptores.Rows.Clear();
                preceptorAsignadoActual = null;

                var esp = cmbEspecialidadPrec.SelectedItem as Especialidad;
                if (esp == null || !nudAnioPrec.Enabled) return;

                int anio = Convert.ToInt32(nudAnioPrec.Value);
                int division = Convert.ToInt32(nudDivisionPrec.Value);
                int grupo = Convert.ToInt32(nudGrupoPrec.Value);

                foreach (Dictado d in cacheDictados)
                {
                    Materia m = cacheMaterias.Find(x => x.IdMateria == d.IdMateria);
                    if (m == null) continue;
                    if (m.IdEspecialidad != esp.IdEspecialidad) continue;
                    if (m.AnioMateria != anio) continue;

                    if (division > (int)nudDivisionPrec.Minimum)
                    {
                        int divDictado = d.Division.HasValue ? d.Division.Value : 0;
                        if (divDictado != division)
                            continue;
                    }

                    int grupoDictado = d.Grupo.HasValue ? d.Grupo.Value : 0;
                    if (grupoDictado != grupo)
                    {
                        continue;
                    }

                    int fila = dgvDictadosPrec.Rows.Add();
                    dgvDictadosPrec.Rows[fila].Cells["colIdDictadoPrec"].Value = d.IdDictado;
                    dgvDictadosPrec.Rows[fila].Cells["colMateriaPrec"].Value = m.NombreMateria ?? "Sin nombre";
                    dgvDictadosPrec.Rows[fila].Cells["colAnioPrec"].Value = m.AnioMateria;
                    dgvDictadosPrec.Rows[fila].Cells["colDivisionPrec"].Value = (!d.Division.HasValue || d.Division.Value == 0) ? "Todas" : d.Division.Value.ToString();
                    dgvDictadosPrec.Rows[fila].Cells["colGrupoPrec"].Value = (!d.Grupo.HasValue || d.Grupo.Value == 0) ? "Todos" : d.Grupo.Value.ToString();
                }

                dgvDictadosPrec.ClearSelection();
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

        private void DgvDictadosPrec_SelectionChanged(object sender, EventArgs e)
        {
            if (_cargando) return;

            if (dgvDictadosPrec.SelectedRows.Count == 0 && dgvDictadosPrec.CurrentRow == null)
            {
                dgvPreceptores.Rows.Clear();
                preceptorAsignadoActual = null;
                return;
            }

            CargarTablaPreceptores();
        }

        // ---------------- Preceptores del dictado + búsqueda ----------------

        private void CargarTablaPreceptores()
        {
            dgvPreceptores.Rows.Clear();

            if (dgvDictadosPrec.CurrentRow == null || dgvDictadosPrec.CurrentRow.Cells["colIdDictadoPrec"].Value == null)
            {
                preceptorAsignadoActual = null;
                return;
            }

            if (!int.TryParse(dgvDictadosPrec.CurrentRow.Cells["colIdDictadoPrec"].Value.ToString(), out int idDictado))
            {
                preceptorAsignadoActual = null;
                return;
            }

            Dictado dictado = cacheDictados.Find(d => d.IdDictado == idDictado);
            preceptorAsignadoActual = dictado != null ? dictado.IdPreceptor : null;

            // Respeta el texto de búsqueda vigente.
            AplicarFiltroPreceptores(interactivo: false);
        }

        private void MostrarPreceptores(List<Preceptor> lista)
        {
            dgvPreceptores.Rows.Clear();
            if (lista == null) return;

            foreach (Preceptor preceptor in lista)
            {
                int fila = dgvPreceptores.Rows.Add();
                dgvPreceptores.Rows[fila].Cells["IdPreceptor"].Value = preceptor.IdPreceptor;
                dgvPreceptores.Rows[fila].Cells["ApellidoPreceptor"].Value = preceptor.ApellidoPreceptor;
                dgvPreceptores.Rows[fila].Cells["NombrePreceptor"].Value = preceptor.NombrePreceptor;
                dgvPreceptores.Rows[fila].Cells["LegajoPreceptor"].Value = preceptor.LegajoPreceptor;
                dgvPreceptores.Rows[fila].Cells["Asignado"].Value =
                    preceptorAsignadoActual.HasValue && preceptorAsignadoActual.Value == preceptor.IdPreceptor;
            }

            dgvPreceptores.ClearSelection();
            ActualizarTextoTogglePrec();
        }

        private void txtBuscarPreceptor_TextChanged(object sender, EventArgs e)
        {
            if (_cargando) return;
            AplicarFiltroPreceptores(interactivo: false);
        }

        private void AplicarFiltroPreceptores(bool interactivo)
        {
            string texto = txtBuscarPreceptor.Text.Trim();

            if (string.IsNullOrWhiteSpace(texto))
            {
                MostrarPreceptores(cachePreceptores);
                return;
            }

            List<Preceptor> resultados = cachePreceptores.FindAll(p =>
                (p.NombrePreceptor != null &&
                    p.NombrePreceptor.IndexOf(texto, StringComparison.OrdinalIgnoreCase) >= 0) ||
                (p.ApellidoPreceptor != null &&
                    p.ApellidoPreceptor.IndexOf(texto, StringComparison.OrdinalIgnoreCase) >= 0) ||
                (p.Dni != null &&
                    p.Dni.IndexOf(texto, StringComparison.OrdinalIgnoreCase) >= 0) ||
                (p.LegajoPreceptor != null &&
                    p.LegajoPreceptor.IndexOf(texto, StringComparison.OrdinalIgnoreCase) >= 0));

            MostrarPreceptores(resultados);

            if (interactivo && resultados.Count == 0)
            {
                MessageBox.Show(
                    "Sin resultados para esa búsqueda.",
                    "Inscripciones",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void DgvPreceptores_SelectionChanged(object sender, EventArgs e)
        {
            if (_cargando) return;
            ActualizarTextoTogglePrec();
        }

        private void ActualizarTextoTogglePrec()
        {
            const string neutro = "Asignar/Quitar";

            if (dgvPreceptores.CurrentRow == null ||
                dgvPreceptores.CurrentRow.Cells["Asignado"].Value == null)
            {
                btnTogglePreceptor.Text = neutro;
                return;
            }

            bool asignado = Convert.ToBoolean(dgvPreceptores.CurrentRow.Cells["Asignado"].Value);
            btnTogglePreceptor.Text = asignado ? "Quitar" : "Asignar";
        }

        // ---------------- Limpiar (pestaña preceptores) ----------------

        private void btnLimpiarPrec_Click(object sender, EventArgs e)
        {
            _cargando = true;
            try
            {
                CargaVista.ReiniciarCombo(cmbEspecialidadPrec);
                nudAnioPrec.Value = nudAnioPrec.Minimum;
                nudDivisionPrec.Value = nudDivisionPrec.Minimum;
                nudGrupoPrec.Value = nudGrupoPrec.Minimum;
                txtBuscarPreceptor.Clear();
                dgvDictadosPrec.Rows.Clear();
                dgvPreceptores.Rows.Clear();
                preceptorAsignadoActual = null;
            }
            finally
            {
                _cargando = false;
            }

            nudAnioPrec.Enabled = false;
            nudDivisionPrec.Enabled = false;
            nudGrupoPrec.Enabled = false;
            dgvDictadosPrec.Enabled = false;
            ActualizarTextoTogglePrec();
        }

        // ---------------- Asignar / Quitar (inmediato, fila seleccionada) ----------------

        private void btnTogglePreceptor_Click(object sender, EventArgs e)
        {
            if (dgvDictadosPrec.CurrentRow == null || dgvDictadosPrec.CurrentRow.Cells["colIdDictadoPrec"].Value == null)
            {
                MessageBox.Show("Seleccione un dictado de la lista.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgvPreceptores.CurrentRow == null || dgvPreceptores.CurrentRow.Cells["IdPreceptor"].Value == null)
            {
                MessageBox.Show("Seleccione un preceptor de la lista.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idDictado = Convert.ToInt32(dgvDictadosPrec.CurrentRow.Cells["colIdDictadoPrec"].Value);
            int idPreceptor = Convert.ToInt32(dgvPreceptores.CurrentRow.Cells["IdPreceptor"].Value);
            bool asignado = Convert.ToBoolean(dgvPreceptores.CurrentRow.Cells["Asignado"].Value ?? false);
            string nombrePreceptor = Convert.ToString(dgvPreceptores.CurrentRow.Cells["ApellidoPreceptor"].Value)
                + ", " + Convert.ToString(dgvPreceptores.CurrentRow.Cells["NombrePreceptor"].Value);

            bool ok;
            try
            {
                if (asignado)
                    ok = dictadoController.AsignarPreceptor(idDictado, null);
                else
                    ok = dictadoController.AsignarPreceptor(idDictado, idPreceptor);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo actualizar la asignación.\n" + ex.Message,
                    "Inscripciones",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (!ok)
            {
                MessageBox.Show(
                    "No se pudo actualizar la asignación.",
                    "Inscripciones",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show(
                nombrePreceptor + (asignado ? ": asignación quitada." : " asignado correctamente."),
                "Inscripciones",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            // Recarga el caché (cambió el preceptor del dictado)
            // manteniendo la búsqueda vigente.
            cacheDictados = CargaVista.ObtenerLista(() => dictadoController.ObtenerDictados()) ?? new List<Dictado>();
            CargarTablaPreceptores();
        }
    }
}