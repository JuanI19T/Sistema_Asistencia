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
    public partial class FrmDictados : Form
    {
        private readonly DictadoController dictadoController;
        private readonly MateriaController materiaController;
        private readonly ProfesorController profesorController;
        private readonly EspecialidadController especialidadController;
        private List<Dictado> cacheDictados = new List<Dictado>();
        private List<Materia> cacheMaterias = new List<Materia>();
        private int idDictadoSeleccionado = 0;
        private int idMateriaCrear = 0;
        private int idMateriaEditar = 0;

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

            Tema.EstilizarBoton(btnGuardar, true);
            Tema.EstilizarBoton(btnModificar, true);
            Tema.EstilizarBoton(btnBuscar, false);
            Tema.EstilizarBoton(btnEliminar, false);
            Tema.EstilizarBoton(btnLimpiarCrear, false);
            Tema.EstilizarBoton(btnLimpiarEditar, false);
            especialidadController = new EspecialidadController();

            // Cascada Año -> Especialidad (filtro) -> grilla + buscador.
            // El clic en una fila elige la materia y habilita inicio/fin.
            nudAnioMateria.ValueChanged += (s, e) => FiltrarMateriasCrear();
            cmbFiltroEspecialidad.SelectedIndexChanged += (s, e) => FiltrarMateriasCrear();
            dgvMateriaSel.CellClick += (s, e) => SeleccionMateriaCrear();
            dgvMateriaSel.SelectionChanged += (s, e) => SeleccionMateriaCrear();
            nudEditAnioMateria.ValueChanged += (s, e) => FiltrarMateriasEditar();
            cmbEditFiltroEspecialidad.SelectedIndexChanged += (s, e) => FiltrarMateriasEditar();
            dgvEditMateriaSel.CellClick += (s, e) => SeleccionMateriaEditar();
            dgvEditMateriaSel.SelectionChanged += (s, e) => SeleccionMateriaEditar();
            dtpHorario.Enabled = false;
            dtpHorarioFin.Enabled = false;
            dtpEditHorario.Enabled = false;
            dtpEditHorarioFin.Enabled = false;
        }

        private void FrmDictados_Load(object sender, EventArgs e)
        {
            try
            {
                CargarFiltrosEspecialidad();
                CargarMaterias();
                CargarProfesores();
                CargarDictados();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo cargar dictados.\n" + ex.Message,
                    "Error de conexión",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void CargarFiltrosEspecialidad()
        {
            List<Especialidad> lista = especialidadController.ObtenerEspecialidades()
                ?? new List<Especialidad>();
            lista.Insert(0, new Especialidad { IdEspecialidad = 0, NombreEspecialidad = "Todas" });

            cmbFiltroEspecialidad.DataSource = new List<Especialidad>(lista);
            cmbFiltroEspecialidad.DisplayMember = "NombreEspecialidad";
            cmbFiltroEspecialidad.ValueMember = "IdEspecialidad";
            cmbFiltroEspecialidad.SelectedIndex = 0;

            cmbEditFiltroEspecialidad.DataSource = new List<Especialidad>(lista);
            cmbEditFiltroEspecialidad.DisplayMember = "NombreEspecialidad";
            cmbEditFiltroEspecialidad.ValueMember = "IdEspecialidad";
            cmbEditFiltroEspecialidad.SelectedIndex = 0;
        }

        private void CargarMaterias()
        {
            cacheMaterias = materiaController.ObtenerMaterias() ?? new List<Materia>();
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

        private void FiltrarMaterias(NumericUpDown nudAnio, ComboBox cmbFiltroEsp,
            DataGridView dgv, ref int idMateria, params DateTimePicker[] relojes)
        {
            if (nudAnio == null || cmbFiltroEsp == null || dgv == null) return;
            int anio = Convert.ToInt32(nudAnio.Value);
            int idEsp = 0;
            try { idEsp = Convert.ToInt32(cmbFiltroEsp.SelectedValue); }
            catch { idEsp = 0; }

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
                dgv.Columns["NombreMateria"].DisplayIndex = 0;
            }
            if (dgv.Columns.Contains("NombreEspecialidad"))
            {
                dgv.Columns["NombreEspecialidad"].HeaderText = "Especialidad";
                dgv.Columns["NombreEspecialidad"].DisplayIndex = 1;
            }
            if (dgv.Columns.Contains("AnioMateria"))
            {
                dgv.Columns["AnioMateria"].HeaderText = "Año";
                dgv.Columns["AnioMateria"].DisplayIndex = 2;
            }
            if (dgv.Columns.Contains("Ciclo"))
            {
                dgv.Columns["Ciclo"].HeaderText = "Ciclo";
                dgv.Columns["Ciclo"].DisplayIndex = 3;
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
                    dgv.CurrentCell = fila.Cells[0];
                    fila.Selected = true;
                    return;
                }
            }
        }

        private void CargarProfesores()
        {
            List<Profesor> lista = profesorController.ObtenerProfesores();

            cmbProfesor.DataSource = new List<Profesor>(lista);
            cmbProfesor.DisplayMember = "NombreCompleto";
            cmbProfesor.ValueMember = "IdProfesor";
            cmbProfesor.SelectedIndex = -1;

            cmbEditProfesor.DataSource = new List<Profesor>(lista);
            cmbEditProfesor.DisplayMember = "NombreCompleto";
            cmbEditProfesor.ValueMember = "IdProfesor";
            cmbEditProfesor.SelectedIndex = -1;
        }

        private void CargarDictados()
        {
            cacheDictados = dictadoController.ObtenerDictados() ?? new List<Dictado>();
            MostrarEnGrilla(cacheDictados);
        }

        private void MostrarEnGrilla(List<Dictado> lista)
        {
            dgvDictados.DataSource = null;
            dgvDictados.DataSource = lista;

            if (dgvDictados.Columns.Count > 0)
            {
                if (dgvDictados.Columns.Contains("IdDictado"))
                    dgvDictados.Columns["IdDictado"].Visible = false;
                if (dgvDictados.Columns.Contains("IdMateria"))
                    dgvDictados.Columns["IdMateria"].Visible = false;
                if (dgvDictados.Columns.Contains("IdProfesor"))
                    dgvDictados.Columns["IdProfesor"].Visible = false;
                if (dgvDictados.Columns.Contains("NombreMateria"))
                    dgvDictados.Columns["NombreMateria"].HeaderText = "Materia";
                if (dgvDictados.Columns.Contains("ApellidoProfesor"))
                    dgvDictados.Columns["ApellidoProfesor"].HeaderText = "Profesor";
                if (dgvDictados.Columns.Contains("AnioLectivo"))
                    dgvDictados.Columns["AnioLectivo"].HeaderText = "Año";
            }
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

            try
            {
                Dictado dictado = new Dictado
                {
                    IdMateria = idMateriaCrear,
                    IdProfesor = Convert.ToInt32(cmbProfesor.SelectedValue),
                    Dia = Convert.ToString(cmbDia.SelectedItem),
                    Horario = FormatearHora(dtpHorario.Value),
                    HorarioFin = FormatearHora(dtpHorarioFin.Value),
                    Grupo = Convert.ToInt32(nudGrupo.Value).ToString(),
                    AnioLectivo = Convert.ToInt32(nudAnioLectivo.Value)
                };

                if (dictadoController.AgregarDictado(dictado))
                {
                    MessageBox.Show("Dictado agregado correctamente.");

                    CargarDictados();
                    LimpiarCreacion();
                }
                else
                {
                    MessageBox.Show("No se pudo agregar el dictado.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo agregar el dictado.\n" + ex.Message);
            }
        }

        private void btnLimpiarCrear_Click(object sender, EventArgs e)
        {
            LimpiarCreacion();
        }

        private void LimpiarCreacion()
        {
            nudAnioMateria.Value = nudAnioMateria.Minimum;
            cmbFiltroEspecialidad.SelectedIndex = 0;
            idMateriaCrear = 0;
            cmbProfesor.SelectedIndex = -1;
            cmbDia.SelectedIndex = -1;
            FijarRango(dtpHorario, TimeSpan.Zero, new TimeSpan(23, 59, 0), new TimeSpan(8, 0, 0));
            FijarRango(dtpHorarioFin, TimeSpan.Zero, new TimeSpan(23, 59, 0), new TimeSpan(10, 0, 0));
            nudGrupo.Value = nudGrupo.Minimum;
            nudAnioLectivo.Value = LimitarAnio(nudAnioLectivo, DateTime.Now.Year);
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
                MostrarEnGrilla(cacheDictados);
                return;
            }

            List<Dictado> resultados = cacheDictados.FindAll(d =>
                d.Descripcion != null &&
                d.Descripcion.IndexOf(texto, StringComparison.OrdinalIgnoreCase) >= 0);

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

        private void dgvDictados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            Dictado dictado = dgvDictados.Rows[e.RowIndex].DataBoundItem as Dictado;
            if (dictado != null)
                CargarEnEdicion(dictado);
        }

        private void CargarEnEdicion(Dictado dictado)
        {
            idDictadoSeleccionado = dictado.IdDictado;
            lblEditando.Text = "Editando: " + dictado.Descripcion;

            // Filtros primero (disparan la cascada) y recién la fila.
            Materia mat = cacheMaterias.Find(m => m.IdMateria == dictado.IdMateria);
            if (mat != null)
            {
                nudEditAnioMateria.Value = LimitarAnioMateria(nudEditAnioMateria, mat.AnioMateria);
                SeleccionarValor(cmbEditFiltroEspecialidad, mat.IdEspecialidad);
            }
            SeleccionarFilaMateria(dgvEditMateriaSel, dictado.IdMateria);
            idMateriaEditar = IdMateriaDeFila(dgvEditMateriaSel);
            SeleccionarValor(cmbEditProfesor, dictado.IdProfesor);
            cmbEditDia.SelectedItem = dictado.Dia;
            // Rango amplio primero (el select de materia lo recorta después).
            FijarRango(dtpEditHorario, TimeSpan.Zero, new TimeSpan(23, 59, 0),
                HoraDesdeTexto(dictado.Horario, new TimeSpan(8, 0, 0)));
            FijarRango(dtpEditHorarioFin, TimeSpan.Zero, new TimeSpan(23, 59, 0),
                HoraDesdeTexto(dictado.HorarioFin, new TimeSpan(10, 0, 0)));
            // Recorta a la franja de la materia (normaliza horarios viejos).
            AplicarFranjaPorMateria(idMateriaEditar, dtpEditHorario, dtpEditHorarioFin);
            // Grupos viejos eran texto ("4to B"): si no es 1-3, cae al mínimo.
            if (!int.TryParse(dictado.Grupo, out int gEdit) || gEdit < 1 || gEdit > 3)
                nudEditGrupo.Value = nudEditGrupo.Minimum;
            else
                nudEditGrupo.Value = gEdit;
            nudEditAnio.Value = LimitarAnio(nudEditAnio, dictado.AnioLectivo);
        }

        private static void SeleccionarValor(ComboBox combo, int id)
        {
            try { combo.SelectedValue = id; }
            catch { combo.SelectedIndex = -1; }
        }

        private static decimal LimitarAnio(NumericUpDown nud, int valor)
        {
            if (valor < nud.Minimum) return nud.Minimum;
            if (valor > nud.Maximum) return nud.Maximum;
            return valor;
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
                MessageBox.Show("Busque y seleccione un dictado primero.");
                return;
            }

            if (!ValidarCampos(idMateriaEditar, dgvEditMateriaSel.Rows.Count > 0,
                    cmbEditProfesor, cmbEditDia, dtpEditHorario, dtpEditHorarioFin))
                return;

            if (!FranjaValida(
                    idMateriaEditar,
                    dtpEditHorario.Value.TimeOfDay, dtpEditHorarioFin.Value.TimeOfDay))
                return;

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
                    Grupo = Convert.ToInt32(nudEditGrupo.Value).ToString(),
                    AnioLectivo = Convert.ToInt32(nudEditAnio.Value)
                };

                if (dictadoController.ModificarDictado(dictado))
                {
                    MessageBox.Show("Dictado modificado correctamente.");

                    CargarDictados();
                    LimpiarEdicion();
                }
                else
                {
                    MessageBox.Show("No se pudo modificar el dictado.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo modificar el dictado.\n" + ex.Message);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idDictadoSeleccionado == 0)
            {
                MessageBox.Show("Busque y seleccione un dictado primero.");
                return;
            }

            Dictado dictado = cacheDictados.Find(d => d.IdDictado == idDictadoSeleccionado);
            if (dictado == null) return;

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
                        if (ok) MessageBox.Show("Dictado eliminado definitivamente.");
                    }
                    else
                    {
                        ok = dictadoController.DarDeBaja(idDictadoSeleccionado);
                        if (ok) MessageBox.Show("Dictado dado de baja.");
                    }

                    if (ok)
                    {
                        CargarDictados();
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
            idDictadoSeleccionado = 0;
            lblEditando.Text = "Editando: (seleccione de la lista)";
            nudEditAnioMateria.Value = nudEditAnioMateria.Minimum;
            cmbEditFiltroEspecialidad.SelectedIndex = 0;
            idMateriaEditar = 0;
            cmbEditProfesor.SelectedIndex = -1;
            cmbEditDia.SelectedIndex = -1;
            FijarRango(dtpEditHorario, TimeSpan.Zero, new TimeSpan(23, 59, 0), new TimeSpan(8, 0, 0));
            FijarRango(dtpEditHorarioFin, TimeSpan.Zero, new TimeSpan(23, 59, 0), new TimeSpan(10, 0, 0));
            nudEditGrupo.Value = nudEditGrupo.Minimum;
            nudEditAnio.Value = LimitarAnio(nudEditAnio, DateTime.Now.Year);
        }

        private bool ValidarCampos(
            int idMateria, bool hayFilas,
            ComboBox profesor, ComboBox dia,
            DateTimePicker horario, DateTimePicker horarioFin)
        {
            if (!hayFilas)
            {
                MessageBox.Show("No hay materias para ese año y especialidad. Ajuste los filtros.");
                return false;
            }

            if (idMateria == 0)
            {
                MessageBox.Show("Seleccione una materia de la lista.");
                return false;
            }

            if (profesor.SelectedIndex < 0)
            {
                MessageBox.Show("Seleccione un profesor.");
                return false;
            }

            if (dia.SelectedIndex < 0)
            {
                MessageBox.Show("Seleccione un día.");
                return false;
            }

            // DateTimePicker siempre da hora válida: solo comparar.
            if (horarioFin.Value.TimeOfDay <= horario.Value.TimeOfDay)
            {
                MessageBox.Show("La hora de fin debe ser posterior a la de inicio.");
                horarioFin.Focus();
                return false;
            }

            // Grupo es NumericUpDown 1-3: siempre válido.
            return true;
        }

        private static string FormatearHora(DateTime valor)
        {
            return valor.TimeOfDay.ToString(@"hh\:mm\:ss");
        }

        // Recorta el selector de hora a [min, max] (solo-hora: la fecha es hoy).
        // Orden seguro: primero Value (siempre dentro del rango amplio
        // por defecto), después Min/Max.
        private static void FijarRango(
            DateTimePicker dtp, TimeSpan min, TimeSpan max, TimeSpan valor)
        {
            TimeSpan t = valor;
            if (t < min) t = min;
            else if (t > max) t = max;
            DateTime hoy = DateTime.Today;
            dtp.Value = hoy + t;
            dtp.MinDate = hoy + min;
            dtp.MaxDate = hoy + max;
        }

        private static void FranjaDe(int? anioMateria, out TimeSpan min, out TimeSpan max)
        {
            if (anioMateria >= 1 && anioMateria <= 3)
            {
                min = new TimeSpan(7, 30, 0); max = new TimeSpan(17, 0, 0);
            }
            else if (anioMateria >= 4 && anioMateria <= 7)
            {
                min = new TimeSpan(13, 0, 0); max = new TimeSpan(21, 0, 0);
            }
            else
            {
                min = TimeSpan.Zero; max = new TimeSpan(23, 59, 0);
            }
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

        // Franjas EEST según el ciclo de la materia elegida:
        // Ciclo Básico (años 1-3) 07:30-17:00, Superior (4-7) 13:00-21:00.
        private bool FranjaValida(int idMateria, TimeSpan inicio, TimeSpan fin)
        {
            Materia mat = cacheMaterias.Find(m => m.IdMateria == idMateria);
            if (mat == null) return true;

            TimeSpan min, max;
            string ciclo;
            if (mat.AnioMateria >= 1 && mat.AnioMateria <= 3)
            {
                min = new TimeSpan(7, 30, 0); max = new TimeSpan(17, 0, 0);
                ciclo = "Ciclo Básico (07:30 a 17:00)";
            }
            else
            {
                min = new TimeSpan(13, 0, 0); max = new TimeSpan(21, 0, 0);
                ciclo = "Ciclo Superior (13:00 a 21:00)";
            }

            if (inicio < min || fin > max)
            {
                MessageBox.Show(
                    $"La materia es de {ciclo}: el dictado debe estar entre {min:hh\\:mm} y {max:hh\\:mm}.");
                return false;
            }
            return true;
        }

    }
}
