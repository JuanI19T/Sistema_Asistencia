using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SistemaAsistencia.Controlador;
using SistemaAsistencia.Modelo.DAO;
using SistemaAsistencia.Modelo.Entidades;
using SistemaAsistencia.Vista.Comun;
using SistemaAsistencia.Utilidades;

namespace SistemaAsistencia.Vista.Dictados
{
    public partial class FrmDictados : FrmBaseHijo
    {
        private readonly DictadoController dictadoController;
        private readonly MateriaController materiaController;
        private readonly ProfesorController profesorController;
        private readonly EspecialidadController especialidadController;
        private List<Dictado> cacheDictados = new List<Dictado>();
        private List<Materia> cacheMaterias = new List<Materia>();
        private List<Especialidad> cacheEspecialidades = new List<Especialidad>();
        private int idDictadoSeleccionado = 0;
        private int idMateriaCrear = 0;
        private int idMateriaEditar = 0;

        // Contador (no flag) para distinguir cambios programáticos
        // (carga, limpiar, edición) de los del usuario. Anidado seguro.
        private int _bloqueo = 0;
        private bool Programando => _bloqueo > 0;

        public FrmDictados()
        {
            InitializeComponent();

            dictadoController = new DictadoController();
            materiaController = new MateriaController();
            profesorController = new ProfesorController();

            Tema.ConfigurarFondo(this);
            Tema.EstilizarGrilla(dgvDictados);
            Tema.EstilizarGrilla(dgvMateriaSel);
            Tema.EstilizarGrilla(dgvEditMateriaSel);

            especialidadController = new EspecialidadController();

            // Cascada por pasos: Especialidad -> Año -> División.
            // No se puede elegir el siguiente sin el anterior.
            // El clic en una fila elige la materia y habilita inicio/fin.
            cmbFiltroEspecialidad.SelectedIndexChanged += (s, e) => EspecialidadCrear_Cambiada();
            nudAnioMateria.ValueChanged += (s, e) => AnioCrear_Cambiado();
            nudDivision.ValueChanged += (s, e) => DivisionCrear_Cambiada();
            dgvMateriaSel.CellClick += (s, e) => SeleccionMateriaCrear();
            dgvMateriaSel.SelectionChanged += (s, e) => SeleccionMateriaCrear();
            cmbEditFiltroEspecialidad.SelectedIndexChanged += (s, e) => EspecialidadEditar_Cambiada();
            nudEditAnioMateria.ValueChanged += (s, e) => AnioEditar_Cambiado();
            nudEditDivision.ValueChanged += (s, e) => DivisionEditar_Cambiada();
            dgvEditMateriaSel.CellClick += (s, e) => SeleccionMateriaEditar();
            dgvEditMateriaSel.SelectionChanged += (s, e) => SeleccionMateriaEditar();
            // Arranque bloqueado: se habilita por pasos al elegir.
            nudAnioMateria.Enabled = false;
            nudDivision.Enabled = false;
            nudGrupo.Enabled = false;
            nudEditAnioMateria.Enabled = false;
            nudEditDivision.Enabled = false;
            nudEditGrupo.Enabled = false;
            dtpHorario.Enabled = false;
            dtpHorarioFin.Enabled = false;
            dtpEditHorario.Enabled = false;
            dtpEditHorarioFin.Enabled = false;
        }

        private void FrmDictados_Load(object sender, EventArgs e)
        {
            CargaVista.IntentarCarga(() =>
            {
                CargarFiltrosEspecialidad();
                CargarMaterias();
                CargarProfesores();
                CargarDictados();
            }, "dictados");
            EstadoActivoHelper.AplicarPermiso(btnToggleActivo);
            EstadoActivoHelper.ActualizarTexto(btnToggleActivo, null);
        }

        private void CargarFiltrosEspecialidad()
        {
            cacheEspecialidades = CargaVista.ObtenerLista(
                () => especialidadController.ObtenerEspecialidades());

            // Sin "Todas": la cascada exige elegir especialidad (paso 1).
            CargaVista.CargarCombo(cmbFiltroEspecialidad, cacheEspecialidades, "NombreEspecialidad", "IdEspecialidad");
            CargaVista.CargarCombo(cmbEditFiltroEspecialidad, cacheEspecialidades, "NombreEspecialidad", "IdEspecialidad");
        }

        private void CargarMaterias()
        {
            cacheMaterias = CargaVista.ObtenerLista(() => materiaController.ObtenerMaterias());
            FiltrarMateriasCrear();
            FiltrarMateriasEditar();
        }

        // Año + Especialidad -> grilla embebida.
        // Columnas separadas porque el nombre se repite entre tecnicaturas.
        private void FiltrarMateriasCrear()
        {
            FiltrarMaterias(nudAnioMateria, cmbFiltroEspecialidad,
                dgvMateriaSel, ref idMateriaCrear, dtpHorario, dtpHorarioFin);
        }

        private void FiltrarMateriasEditar()
        {
            FiltrarMaterias(nudEditAnioMateria, cmbEditFiltroEspecialidad,
                dgvEditMateriaSel, ref idMateriaEditar, dtpEditHorario, dtpEditHorarioFin);
        }

        // ---- Cascada por pasos (Crear): Especialidad -> Año -> División ----

        // División fija de la especialidad elegida (NULL = libre).
        private int? DivisionFijaCrear()
        {
            return CicloHelper.DivisionDeEspecialidad(
                cmbFiltroEspecialidad.SelectedItem as Especialidad);
        }

        private int? DivisionFijaEditar()
        {
            return CicloHelper.DivisionDeEspecialidad(
                cmbEditFiltroEspecialidad.SelectedItem as Especialidad);
        }

        private void EspecialidadCrear_Cambiada()
        {
            bool hayEsp = cmbFiltroEspecialidad.SelectedIndex >= 0;
            int? divFija = hayEsp ? DivisionFijaCrear() : (int?)null;

            _bloqueo++;
            try
            {
                if (hayEsp)
                    AplicarRangoAnio(nudAnioMateria, NombreEspecialidadDe(cmbFiltroEspecialidad));
                else
                {
                    nudAnioMateria.Maximum = 7;
                    nudAnioMateria.Minimum = 1;
                }
                nudAnioMateria.Value = nudAnioMateria.Minimum;
                nudDivision.Value = divFija ?? 1;
                nudGrupo.Value = 0;
                idMateriaCrear = 0;
                dgvMateriaSel.DataSource = null;
                dtpHorario.Enabled = false;
                dtpHorarioFin.Enabled = false;
            }
            finally { _bloqueo--; }

            nudAnioMateria.Enabled = hayEsp;
            nudDivision.Enabled = hayEsp && !divFija.HasValue;
            nudGrupo.Enabled = false;
            if (hayEsp) FiltrarMateriasCrear();
        }

        private void AnioCrear_Cambiado()
        {
            FiltrarMateriasCrear();
            if (Programando) return;

            // El usuario eligió Año: se resetea lo dependiente y se abre División,
            // salvo división fija (sigue bloqueada con su valor).
            int? divFija = DivisionFijaCrear();
            idMateriaCrear = 0;
            dtpHorario.Enabled = false;
            dtpHorarioFin.Enabled = false;
            _bloqueo++;
            try
            {
                nudDivision.Value = divFija ?? 1;
                nudGrupo.Value = 0;
            }
            finally { _bloqueo--; }
            nudDivision.Enabled = !divFija.HasValue;
            nudGrupo.Enabled = false;
        }

        private void DivisionCrear_Cambiada()
        {
            bool conDivision = nudDivision.Enabled && nudDivision.Value > 0;
            nudGrupo.Enabled = conDivision;
            if (!conDivision && !Programando) nudGrupo.Value = 0;
        }

        // ---- Cascada por pasos (Modificar): igual que Crear ----

        private void EspecialidadEditar_Cambiada()
        {
            bool hayEsp = cmbEditFiltroEspecialidad.SelectedIndex >= 0;
            int? divFija = hayEsp ? DivisionFijaEditar() : (int?)null;

            _bloqueo++;
            try
            {
                if (hayEsp)
                    AplicarRangoAnio(nudEditAnioMateria, NombreEspecialidadDe(cmbEditFiltroEspecialidad));
                else
                {
                    nudEditAnioMateria.Maximum = 7;
                    nudEditAnioMateria.Minimum = 1;
                }
                nudEditAnioMateria.Value = nudEditAnioMateria.Minimum;
                nudEditDivision.Value = divFija ?? 0;
                nudEditGrupo.Value = 0;
                idMateriaEditar = 0;
                dgvEditMateriaSel.DataSource = null;
                dtpEditHorario.Enabled = false;
                dtpEditHorarioFin.Enabled = false;
            }
            finally { _bloqueo--; }

            nudEditAnioMateria.Enabled = hayEsp;
            nudEditDivision.Enabled = hayEsp && !divFija.HasValue;
            nudEditGrupo.Enabled = false;
            if (hayEsp) FiltrarMateriasEditar();
        }

        private void AnioEditar_Cambiado()
        {
            FiltrarMateriasEditar();
            if (Programando) return;

            int? divFija = DivisionFijaEditar();
            idMateriaEditar = 0;
            dtpEditHorario.Enabled = false;
            dtpEditHorarioFin.Enabled = false;
            _bloqueo++;
            try
            {
                nudEditDivision.Value = divFija ?? 0;
                nudEditGrupo.Value = 0;
            }
            finally { _bloqueo--; }
            nudEditDivision.Enabled = !divFija.HasValue;
            nudEditGrupo.Enabled = false;
        }

        private void DivisionEditar_Cambiada()
        {
            bool conDivision = nudEditDivision.Enabled && nudEditDivision.Value > 0;
            nudEditGrupo.Enabled = conDivision;
            if (!conDivision && !Programando) nudEditGrupo.Value = 0;
        }

        // ---- La especialidad condiciona el año (igual que en Materias) ----
        // Ciclo Básico: 1-3. Cualquier tecnicatura: 4-7.

        private static bool EsCicloBasico(string nombreEspecialidad)
        {
            string n = (nombreEspecialidad ?? string.Empty).Trim();
            return n.Equals("Ciclo Básico", StringComparison.OrdinalIgnoreCase)
                || n.Equals("Ciclo Basico", StringComparison.OrdinalIgnoreCase);
        }

        private static string NombreEspecialidadDe(ComboBox combo)
        {
            var esp = combo.SelectedItem as Especialidad;
            if (esp != null && !string.IsNullOrWhiteSpace(esp.NombreEspecialidad))
                return esp.NombreEspecialidad;
            return combo.Text;
        }

        private static int IdEspecialidadDe(ComboBox combo)
        {
            var esp = combo.SelectedItem as Especialidad;
            if (esp != null && esp.IdEspecialidad > 0)
                return esp.IdEspecialidad;
            try { return Convert.ToInt32(combo.SelectedValue); }
            catch { return 0; }
        }

        private static void AplicarRangoAnio(NumericUpDown nud, string nombreEspecialidad)
        {
            bool basico = EsCicloBasico(nombreEspecialidad);
            int min = basico ? 1 : 4;
            int max = basico ? 3 : 7;

            // Orden seguro: nunca dejar Value fuera de [Minimum, Maximum].
            decimal v = nud.Value;
            if (v < min) { nud.Maximum = max; nud.Minimum = min; nud.Value = min; }
            else if (v > max) { nud.Minimum = min; nud.Maximum = max; nud.Value = max; }
            else { nud.Minimum = min; nud.Maximum = max; }
        }

        private void FiltrarMaterias(NumericUpDown nudAnio, ComboBox cmbFiltroEsp,
            DataGridView dgv, ref int idMateria, params DateTimePicker[] relojes)
        {
            if (nudAnio == null || cmbFiltroEsp == null || dgv == null) return;

            // Paso 1 pendiente: sin especialidad no hay materias.
            if (cmbFiltroEsp.SelectedIndex < 0)
            {
                dgv.DataSource = null;
                idMateria = 0;
                foreach (DateTimePicker dtp in relojes)
                    dtp.Enabled = false;
                return;
            }

            int anio = Convert.ToInt32(nudAnio.Value);
            // SelectedValue del MaterialComboBox puede llegar con lag al
            // dispararse SelectedIndexChanged: se lee primero el SelectedItem
            // (igual que NombreEspecialidadDe) y solo de fallback el Value.
            int idEsp = IdEspecialidadDe(cmbFiltroEsp);

            List<Materia> filtradas = cacheMaterias.FindAll(m =>
                m.AnioMateria == anio && (idEsp == 0 || m.IdEspecialidad == idEsp));

            dgv.DataSource = null;
            dgv.DataSource = filtradas;
            ConfigurarGrillaMaterias(dgv);

            idMateria = 0;
            foreach (DateTimePicker dtp in relojes)
                dtp.Enabled = false;
        }

        private static void ConfigurarGrillaMaterias(DataGridView dgv)
        {
            if (dgv.Columns.Count == 0) return;
            string[] visibles = { "NombreMateria", "NombreEspecialidad", "AnioMateria", "Ciclo" };
            foreach (DataGridViewColumn col in dgv.Columns)
                col.Visible = System.Array.IndexOf(visibles, col.Name) >= 0;
            if (dgv.Columns.Contains("NombreMateria"))
            {
                dgv.Columns["NombreMateria"].HeaderText = "Materia";
                dgv.Columns["NombreMateria"].DisplayIndex = 2;
                dgv.Columns["NombreMateria"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            if (dgv.Columns.Contains("NombreEspecialidad"))
            {
                dgv.Columns["NombreEspecialidad"].HeaderText = "Especialidad";
                dgv.Columns["NombreEspecialidad"].DisplayIndex = 0;
                dgv.Columns["NombreEspecialidad"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            if (dgv.Columns.Contains("AnioMateria"))
            {
                dgv.Columns["AnioMateria"].HeaderText = "Año";
                dgv.Columns["AnioMateria"].DisplayIndex = 1;
                dgv.Columns["AnioMateria"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            if (dgv.Columns.Contains("Ciclo"))
            {
                dgv.Columns["Ciclo"].HeaderText = "Ciclo";
                dgv.Columns["Ciclo"].DisplayIndex = 3;
                dgv.Columns["Ciclo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            dgv.ClearSelection();
        }

        private void SeleccionMateriaCrear()
        {
            idMateriaCrear = IdMateriaDeFila(dgvMateriaSel);
            AplicarFranjaPorMateria(idMateriaCrear, dtpHorario, dtpHorarioFin);
        }

        private void SeleccionMateriaEditar()
        {
            idMateriaEditar = IdMateriaDeFila(dgvEditMateriaSel);
            AplicarFranjaPorMateria(idMateriaEditar, dtpEditHorario, dtpEditHorarioFin);
        }

        private static int IdMateriaDeFila(DataGridView dgv)
        {
            if (dgv == null || dgv.CurrentRow == null || dgv.CurrentRow.Index < 0) return 0;
            Materia mat = dgv.CurrentRow.DataBoundItem as Materia;
            return mat != null ? mat.IdMateria : 0;
        }

        private void SeleccionarFilaMateria(DataGridView dgv, int idMateria)
        {
            dgv.ClearSelection();
            if (idMateria == 0 || dgv.Rows.Count == 0) return;
            foreach (DataGridViewRow fila in dgv.Rows)
            {
                Materia mat = fila.DataBoundItem as Materia;
                if (mat != null && mat.IdMateria == idMateria)
                {
                    // Cells[0] suele ser el Id oculto: CurrentCell exige
                    // una celda visible o lanza InvalidOperationException.
                    foreach (DataGridViewCell celda in fila.Cells)
                    {
                        if (celda.Visible)
                        {
                            dgv.CurrentCell = celda;
                            break;
                        }
                    }
                    fila.Selected = true;
                    return;
                }
            }
        }

        private void CargarProfesores()
        {
            List<Profesor> lista = CargaVista.ObtenerLista(
                () => profesorController.ObtenerProfesores());

            CargaVista.CargarCombo(cmbProfesor, lista, "NombreCompleto", "IdProfesor");
            CargaVista.CargarCombo(cmbEditProfesor, lista, "NombreCompleto", "IdProfesor");
        }

        private void CargarDictados()
        {
            cacheDictados = CargaVista.ObtenerLista(() => dictadoController.ObtenerDictadosIncluyendoInactivos());
            MostrarEnGrilla(cacheDictados);
        }

        private void MostrarEnGrilla(List<Dictado> lista)
        {
            // 1. Desactivar autogeneración ANTES de asignar los datos
            dgvDictados.AutoGenerateColumns = false;

            // 2. Configurar y limpiar columnas
            ConfigurarColumnasDictados(dgvDictados);

            // 3. Asignar el origen de datos
            dgvDictados.DataSource = null;
            dgvDictados.DataSource = lista;
        }

        private void ConfigurarColumnasDictados(DataGridView dgv)
        {
            dgv.AutoGenerateColumns = false;
            dgv.AllowUserToOrderColumns = false;

            // Limpiar cualquier columna creada previamente
            dgv.Columns.Clear();

            // Agregar exactamente las 8 columnas en orden
            dgv.Columns.Add(NuevaColumnaTexto("NombreEspecialidad", "Especialidad"));
            dgv.Columns.Add(NuevaColumnaTexto("AnioMateria", "Año"));
            dgv.Columns.Add(NuevaColumnaTexto("Alcance", "Alcance"));
            dgv.Columns.Add(NuevaColumnaTexto("NombreMateria", "Materia"));
            dgv.Columns.Add(NuevaColumnaTexto("Dia", "Día"));
            dgv.Columns.Add(NuevaColumnaTexto("HorarioCompleto", "Horario"));
            dgv.Columns.Add(NuevaColumnaTexto("ApellidoProfesor", "Profesor"));

            var colActivo = new DataGridViewCheckBoxColumn
            {
                Name = "Activo",
                HeaderText = "Activo",
                DataPropertyName = "Activo",
                ReadOnly = true
            };
            dgv.Columns.Add(colActivo);
        }

        private static DataGridViewTextBoxColumn NuevaColumnaTexto(string propiedad, string encabezado)
        {
            var col = new DataGridViewTextBoxColumn();
            col.Name = propiedad;
            col.HeaderText = encabezado;
            col.DataPropertyName = propiedad;
            col.ReadOnly = true;
            return col;
        }

        // ---------------- 1. Crear ----------------

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos(idMateriaCrear, dgvMateriaSel.Rows.Count > 0,
                    cmbProfesor, cmbDia, dtpHorario, dtpHorarioFin))
                return;

            if (!FranjaValida(
                    idMateriaCrear,
                    dtpHorario.Value.TimeOfDay, dtpHorarioFin.Value.TimeOfDay))
                return;

            if (!AlcanceValido(idMateriaCrear, nudDivision, nudGrupo))
                return;

            EjecutarConLayout(() =>
            {
                try
                {
                    Dictado dictado = new Dictado
                    {
                        IdMateria = idMateriaCrear,
                        IdProfesor = Convert.ToInt32(cmbProfesor.SelectedValue),
                        Dia = Convert.ToString(cmbDia.SelectedItem),
                        Horario = FormatearHora(dtpHorario.Value),
                        HorarioFin = FormatearHora(dtpHorarioFin.Value),
                        Division = AlcanceOpcional(nudDivision),
                        Grupo = AlcanceOpcional(nudGrupo),
                        AnioLectivo = Convert.ToInt32(nudAnioLectivo.Value)
                    };

                    if (dictadoController.AgregarDictado(dictado))
                    {
                        MessageBox.Show(
                            "Dictado agregado correctamente.",
                            "Dictados",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        CargarDictados();
                        LimpiarCreacion();
                    }
                    else
                    {
                        MessageBox.Show(
                            "No se pudo agregar el dictado.",
                            "Dictados",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "No se pudo agregar el dictado.\n" + ex.Message,
                        "Dictados",
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
            _bloqueo++;
            try
            {
                CargaVista.ReiniciarCombo(cmbFiltroEspecialidad);
                nudAnioMateria.Value = nudAnioMateria.Minimum;
                nudDivision.Value = 1;
                nudGrupo.Value = 1;
                idMateriaCrear = 0;
                dgvMateriaSel.DataSource = null;
                CargaVista.ReiniciarCombo(cmbProfesor);
                CargaVista.ReiniciarCombo(cmbDia);
                FijarRango(dtpHorario, TimeSpan.Zero, new TimeSpan(23, 59, 0), new TimeSpan(8, 0, 0));
                FijarRango(dtpHorarioFin, TimeSpan.Zero, new TimeSpan(23, 59, 0), new TimeSpan(10, 0, 0));
                nudAnioLectivo.Value = LimitarAnio(nudAnioLectivo, DateTime.Now.Year);
            }
            finally { _bloqueo--; }

            // Vuelta al paso 1: todo bloqueado hasta elegir especialidad.
            nudAnioMateria.Enabled = false;
            nudDivision.Enabled = false;
            nudGrupo.Enabled = false;
            dtpHorario.Enabled = false;
            dtpHorarioFin.Enabled = false;
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
                cacheDictados,
                texto,
                d => d.Descripcion != null &&
                     d.Descripcion.IndexOf(texto, StringComparison.OrdinalIgnoreCase) >= 0,
                MostrarEnGrilla,
                CargarEnEdicion,
                LimpiarEdicion,
                "Dictados",
                interactivo);
        }

        private void dgvDictados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            Dictado dictado = dgvDictados.Rows[e.RowIndex].DataBoundItem as Dictado;
            if (dictado != null)
                CargarEnEdicion(dictado);
        }

        private void CargarEnEdicion(Dictado dictado)
        {
            EjecutarConLayout(() =>
            {
                _bloqueo++;
                try
                {
                    idDictadoSeleccionado = dictado.IdDictado;

                    // Orden de cascada: Especialidad -> Año -> fila -> División.
                    Materia mat = cacheMaterias.Find(m => m.IdMateria == dictado.IdMateria);
                    if (mat != null)
                    {
                        SeleccionarValor(cmbEditFiltroEspecialidad, mat.IdEspecialidad);
                        nudEditAnioMateria.Enabled = true;
                        nudEditAnioMateria.Value = LimitarAnioMateria(nudEditAnioMateria, mat.AnioMateria);
                    }

                    // Nombre de la materia
                    string nombreMateria = mat != null ? mat.NombreMateria : (dictado.NombreMateria ?? "Materia");

                    // Formato de División
                    string divTexto = dictado.Division.HasValue && dictado.Division > 0
                        ? $"Div. {dictado.Division}"
                        : "Sin Div.";

                    // Formato de Grupo
                    string grupoTexto = dictado.Grupo.HasValue && dictado.Grupo > 0
                        ? $"Grupo {dictado.Grupo}"
                        : "Grupo 0";

                    // Asignación con el nuevo formato: Materia + División + Grupo
                    lblEditando.Text = $"Editando: {nombreMateria} - {divTexto} - {grupoTexto}";

                    FiltrarMateriasEditar();
                    SeleccionarFilaMateria(dgvEditMateriaSel, dictado.IdMateria);
                    idMateriaEditar = IdMateriaDeFila(dgvEditMateriaSel);
                    nudEditDivision.Enabled = true;

                    // Si la materia tiene división fija, se impone y bloquea;
                    // si no, se carga la del dictado y queda libre.
                    int? divFijaEdit = mat != null
                        ? CicloHelper.DivisionDeEspecialidad(cacheEspecialidades.Find(e => e.IdEspecialidad == mat.IdEspecialidad))
                        : (int?)null;
                    nudEditDivision.Value = divFijaEdit ?? LimitarAlcance(nudEditDivision, dictado.Division);
                    nudEditDivision.Enabled = !divFijaEdit.HasValue;

                    nudEditGrupo.Value = LimitarAlcance(nudEditGrupo, dictado.Grupo);
                    nudEditGrupo.Enabled = nudEditDivision.Value > 0;

                    SeleccionarValor(cmbEditProfesor, dictado.IdProfesor);
                    cmbEditDia.SelectedItem = dictado.Dia;
                    CargaVista.Refrescar(cmbEditDia);

                    // Horarios sin restricciones
                    FijarRango(dtpEditHorario, TimeSpan.Zero, new TimeSpan(23, 59, 0),
                        HoraDesdeTexto(dictado.Horario, new TimeSpan(8, 0, 0)));
                    FijarRango(dtpEditHorarioFin, TimeSpan.Zero, new TimeSpan(23, 59, 0),
                        HoraDesdeTexto(dictado.HorarioFin, new TimeSpan(10, 0, 0)));

                    AplicarFranjaPorMateria(idMateriaEditar, dtpEditHorario, dtpEditHorarioFin);
                    nudEditAnio.Value = LimitarAnio(nudEditAnio, dictado.AnioLectivo);
                    EstadoActivoHelper.ActualizarTexto(btnToggleActivo, dictado.Activo);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "No se pudo cargar el dictado para editar.\n" + ex.Message,
                        "Dictados",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    LimpiarEdicion();
                }
                finally { _bloqueo--; }
            });
        }

        private static void SeleccionarValor(ComboBox combo, int id)
        {
            try { combo.SelectedValue = id; }
            catch { combo.SelectedIndex = -1; }
            CargaVista.Refrescar(combo);
        }

        private static decimal LimitarAnio(NumericUpDown nud, int valor)
        {
            if (valor < nud.Minimum) return nud.Minimum;
            if (valor > nud.Maximum) return nud.Maximum;
            return valor;
        }

        // NULL o fuera de rango -> 0, que la vista muestra como "todo el curso".
        private static decimal LimitarAlcance(NumericUpDown nud, int? valor)
        {
            if (!valor.HasValue || valor.Value < 1) return 0;
            if (valor.Value > nud.Maximum) return nud.Maximum;
            return valor.Value;
        }

        private static decimal LimitarAnioMateria(NumericUpDown nud, int valor)
        {
            if (valor < 1) return nud.Minimum;
            if (valor > 7) return nud.Maximum;
            if (valor < nud.Minimum) return nud.Minimum;
            if (valor > nud.Maximum) return nud.Maximum;
            return valor;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (idDictadoSeleccionado == 0)
            {
                MessageBox.Show(
                    "Busque y seleccione un dictado primero.",
                    "Dictados",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (!ValidarCampos(idMateriaEditar, dgvEditMateriaSel.Rows.Count > 0,
                    cmbEditProfesor, cmbEditDia, dtpEditHorario, dtpEditHorarioFin))
                return;

            if (!FranjaValida(
                    idMateriaEditar,
                    dtpEditHorario.Value.TimeOfDay, dtpEditHorarioFin.Value.TimeOfDay))
                return;

            if (!AlcanceValido(idMateriaEditar, nudEditDivision, nudEditGrupo))
                return;

            EjecutarConLayout(() =>
            {
                try
                {
                    Dictado dictado = new Dictado
                    {
                        IdDictado = idDictadoSeleccionado,
                        IdMateria = idMateriaEditar,
                        IdProfesor = Convert.ToInt32(cmbEditProfesor.SelectedValue),
                        Dia = Convert.ToString(cmbEditDia.SelectedItem),
                        Horario = FormatearHora(dtpEditHorario.Value),
                        HorarioFin = FormatearHora(dtpEditHorarioFin.Value),
                        Division = AlcanceOpcional(nudEditDivision),
                        Grupo = AlcanceOpcional(nudEditGrupo),
                        AnioLectivo = Convert.ToInt32(nudEditAnio.Value)
                    };

                    if (dictadoController.ModificarDictado(dictado))
                    {
                        MessageBox.Show(
                            "Dictado modificado correctamente.",
                            "Dictados",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        CargarDictados();
                        LimpiarEdicion();
                    }
                    else
                    {
                        MessageBox.Show(
                            "No se pudo modificar el dictado.",
                            "Dictados",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "No se pudo modificar el dictado.\n" + ex.Message,
                        "Dictados",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            });
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idDictadoSeleccionado == 0)
            {
                MessageBox.Show(
                    "Busque y seleccione un dictado primero.",
                    "Dictados",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            Dictado dictado = cacheDictados.Find(d => d.IdDictado == idDictadoSeleccionado);
            if (dictado == null) return;

            EjecutarConLayout(() =>
            {
                try
                {
                    string descripcion = "Dictado: " + dictado.Descripcion;

                    List<Dependencia> dependencias =
                        dictadoController.ObtenerDependencias(idDictadoSeleccionado);

                    using (var confirmar = new FrmConfirmarEliminar(descripcion, dependencias))
                    {
                        if (confirmar.ShowDialog(this) != DialogResult.OK) return;

                        bool ok;

                        if (confirmar.Resultado == ResultadoEliminacion.Definitiva)
                        {
                            ok = dictadoController.EliminarDefinitivo(idDictadoSeleccionado);
                            if (ok)
                                MessageBox.Show(
                                    "Dictado eliminado definitivamente.",
                                    "Dictados",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                        }
                        else
                        {
                            ok = dictadoController.DarDeBaja(idDictadoSeleccionado);
                            if (ok)
                                MessageBox.Show(
                                    "Dictado dado de baja.",
                                    "Dictados",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                        }

                        if (ok)
                        {
                            CargarDictados();
                            LimpiarEdicion();
                        }
                        else
                        {
                            MessageBox.Show(
                                "No se pudo eliminar.",
                                "Dictados",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "No se pudo eliminar.\n" + ex.Message,
                        "Dictados",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            });
        }

        private void btnLimpiarEditar_Click(object sender, EventArgs e)
        {
            LimpiarEdicion();
        }

        private void LimpiarEdicion()
        {
            _bloqueo++;
            try
            {
                idDictadoSeleccionado = 0;
                lblEditando.Text = "Editando: (seleccione de la lista)";
                CargaVista.ReiniciarCombo(cmbEditFiltroEspecialidad);
                nudEditAnioMateria.Value = nudEditAnioMateria.Minimum;
                nudEditDivision.Value = 0;
                nudEditGrupo.Value = 0;
                idMateriaEditar = 0;
                dgvEditMateriaSel.DataSource = null;
                CargaVista.ReiniciarCombo(cmbEditProfesor);
                CargaVista.ReiniciarCombo(cmbEditDia);
                FijarRango(dtpEditHorario, TimeSpan.Zero, new TimeSpan(23, 59, 0), new TimeSpan(8, 0, 0));
                FijarRango(dtpEditHorarioFin, TimeSpan.Zero, new TimeSpan(23, 59, 0), new TimeSpan(10, 0, 0));
                nudEditAnio.Value = LimitarAnio(nudEditAnio, DateTime.Now.Year);
            }
            finally { _bloqueo--; }

            nudEditAnioMateria.Enabled = false;
            nudEditDivision.Enabled = false;
            nudEditGrupo.Enabled = false;
            dtpEditHorario.Enabled = false;
            dtpEditHorarioFin.Enabled = false;
            EstadoActivoHelper.ActualizarTexto(btnToggleActivo, null);
        }

        private void btnToggleActivo_Click(object sender, EventArgs e)
        {
            if (idDictadoSeleccionado == 0)
            {
                MessageBox.Show(
                    "Busque y seleccione un dictado primero.",
                    "Dictados",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            Dictado dictado = cacheDictados.Find(d => d.IdDictado == idDictadoSeleccionado);
            if (dictado == null) return;

            bool ok = EstadoActivoHelper.EjecutarToggle(
                idDictadoSeleccionado,
                dictado.Activo,
                dictadoController.DarDeBaja,
                dictadoController.DarDeAlta,
                "Dictados");

            if (ok)
            {
                CargarDictados();
                LimpiarEdicion();
            }
        }

        private bool ValidarCampos(
            int idMateria, bool hayFilas,
            ComboBox profesor, ComboBox dia,
            DateTimePicker horario, DateTimePicker horarioFin)
        {
            if (!hayFilas)
            {
                MessageBox.Show(
                    "No hay materias para ese año y especialidad. Ajuste los filtros.",
                    "Dictados",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }

            if (idMateria == 0)
            {
                MessageBox.Show(
                    "Seleccione una materia de la lista.",
                    "Dictados",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }

            if (profesor.SelectedIndex < 0)
            {
                MessageBox.Show(
                    "Seleccione un profesor.",
                    "Dictados",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }

            if (dia.SelectedIndex < 0)
            {
                MessageBox.Show(
                    "Seleccione un día.",
                    "Dictados",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }

            // DateTimePicker siempre da hora válida: solo comparar.
            if (horarioFin.Value.TimeOfDay <= horario.Value.TimeOfDay)
            {
                MessageBox.Show(
                    "La hora de fin debe ser posterior a la de inicio.",
                    "Dictados",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                horarioFin.Focus();
                return false;
            }

            // El alcance (división/grupo) se valida aparte, en
            // AlcanceValido(), porque su rango depende del ciclo de la materia.
            return true;
        }

        private static string FormatearHora(DateTime valor)
        {
            return valor.TimeOfDay.ToString(@"hh\:mm\:ss");
        }

        // Recorta el selector de hora a [min, max] (solo-hora: la fecha es hoy).
        // Orden seguro: primero Max (siempre >= Min vigente), después Min
        // (siempre <= Max recién ampliado) y recién Value (ya dentro de
        // [min, max]). Al revés revienta: Value fuera del [MinDate, MaxDate]
        // vigente lanza ArgumentOutOfRangeException (ej. reloj angostado a
        // 13-21 y nuevo valor 08:00).
        private static void FijarRango(
            DateTimePicker dtp, TimeSpan min, TimeSpan max, TimeSpan valor)
        {
            TimeSpan t = valor;
            if (t < min) t = min;
            else if (t > max) t = max;
            DateTime hoy = DateTime.Today;
            dtp.MaxDate = hoy + max;
            dtp.MinDate = hoy + min;
            dtp.Value = hoy + t;
        }

        private static void FranjaDe(int? anioMateria, out TimeSpan min, out TimeSpan max)
        {
            min = TimeSpan.Zero;
            max = new TimeSpan(23, 59, 0);
        }

        // Sin fila elegida: no clickeables; con fila: franja de esa materia.
        private void AplicarFranjaPorMateria(int idMateria, DateTimePicker dtpIni, DateTimePicker dtpFin)
        {
            Materia mat = idMateria == 0
                ? null
                : cacheMaterias.Find(m => m.IdMateria == idMateria);
            dtpIni.Enabled = mat != null;
            dtpFin.Enabled = mat != null;
            if (mat == null) return;
            FranjaDe(mat.AnioMateria, out TimeSpan min, out TimeSpan max);
            FijarRango(dtpIni, min, max, dtpIni.Value.TimeOfDay);
            FijarRango(dtpFin, min, max, dtpFin.Value.TimeOfDay);
        }

        private static TimeSpan HoraDesdeTexto(string texto, TimeSpan defecto)
        {
            if (TimeSpan.TryParse(texto, out TimeSpan ts)) return ts;
            return defecto;
        }

        // 0 = todo el curso. Devuelve null para guardarlo como NULL en MySQL.
        private static int? AlcanceOpcional(NumericUpDown nud)
        {
            int valor = Convert.ToInt32(nud.Value);
            return valor > 0 ? (int?)valor : null;
        }

        // Rango de división según el ciclo de la materia elegida:
        // Ciclo Básico (años 1-3) división 1-7; Tecnicaturas (años 4-7) 1-6.
        // Grupo: 1-2 en todos los casos (0 = todo el curso dentro de la división).
        private bool AlcanceValido(int idMateria, NumericUpDown nudDivision, NumericUpDown nudGrupo)
        {
            Materia mat = cacheMaterias.Find(m => m.IdMateria == idMateria);
            if (mat == null) return true;

            bool cicloBasico = mat.AnioMateria >= 1 && mat.AnioMateria <= 3;
            int maxDivision = cicloBasico ? 7 : 6;
            const int maxGrupo = 2;
            string donde = cicloBasico ? "Ciclo Básico" : "las tecnicaturas";

            int division = Convert.ToInt32(nudDivision.Value);
            int grupo = Convert.ToInt32(nudGrupo.Value);

            // División fija por especialidad (ciclo superior): debe ser
            // exactamente la mapeada; el control ya viene bloqueado.
            Especialidad espMat = cacheEspecialidades.Find(e => e.IdEspecialidad == mat.IdEspecialidad);
            int? divFija = CicloHelper.DivisionDeEspecialidad(espMat);
            if (divFija.HasValue && division != divFija.Value)
            {
                MessageBox.Show(
                    $"Esa especialidad corresponde a la división {divFija.Value}.",
                    "Dictados",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                nudDivision.Focus();
                return false;
            }

            // Validación: división es obligatoria (no se permite 0)
            if (division <= 0)
            {
                MessageBox.Show(
                    "Debe seleccionar una división válida (no puede ser 0).",
                    "Dictados",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                nudDivision.Focus();
                return false;
            }

            if (division > maxDivision)
            {
                MessageBox.Show(
                    $"En {donde} la división va de 1 a {maxDivision}.",
                    "Dictados",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }

            if (grupo > maxGrupo)
            {
                MessageBox.Show(
                    $"En {donde} el grupo va de 1 a {maxGrupo}. " +
                    "Usá 0 si el dictado es para toda la división.",
                    "Dictados",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        // Sin restricción de franja horaria
        private bool FranjaValida(int idMateria, TimeSpan inicio, TimeSpan fin)
        {
            // Restricciones de horario removidas
            return true;
        }
    }
}
