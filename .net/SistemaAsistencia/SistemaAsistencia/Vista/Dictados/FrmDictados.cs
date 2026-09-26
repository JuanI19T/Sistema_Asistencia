using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SistemaAsistencia.Controlador;
using SistemaAsistencia.Modelo.DAO;
using SistemaAsistencia.Modelo.Entidades;
using SistemaAsistencia.Vista.Comun;

namespace SistemaAsistencia.Vista.Dictados
{
    public partial class FrmDictados : Form
    {
        private readonly DictadoController dictadoController;
        private readonly MateriaController materiaController;
        private readonly ProfesorController profesorController;
        private List<Dictado> cacheDictados = new List<Dictado>();
        private int idDictadoSeleccionado = 0;

        public FrmDictados()
        {
            InitializeComponent();

            dictadoController = new DictadoController();
            materiaController = new MateriaController();
            profesorController = new ProfesorController();
        }

        private void FrmDictados_Load(object sender, EventArgs e)
        {
            try
            {
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

        private void CargarMaterias()
        {
            List<Materia> lista = materiaController.ObtenerMaterias();

            cmbMateria.DataSource = new List<Materia>(lista);
            cmbMateria.DisplayMember = "NombreMateria";
            cmbMateria.ValueMember = "IdMateria";
            cmbMateria.SelectedIndex = -1;

            cmbEditMateria.DataSource = new List<Materia>(lista);
            cmbEditMateria.DisplayMember = "NombreMateria";
            cmbEditMateria.ValueMember = "IdMateria";
            cmbEditMateria.SelectedIndex = -1;
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
            if (!ValidarCampos(cmbMateria, cmbProfesor, cmbDia, txtHorario, txtGrupo))
                return;

            try
            {
                Dictado dictado = new Dictado
                {
                    IdMateria = Convert.ToInt32(cmbMateria.SelectedValue),
                    IdProfesor = Convert.ToInt32(cmbProfesor.SelectedValue),
                    Dia = Convert.ToString(cmbDia.SelectedItem),
                    Horario = ObtenerHorario(txtHorario.Text),
                    Grupo = txtGrupo.Text.Trim().ToUpper(),
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
            cmbMateria.SelectedIndex = -1;
            cmbProfesor.SelectedIndex = -1;
            cmbDia.SelectedIndex = -1;
            txtHorario.Clear();
            txtGrupo.Clear();
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

            SeleccionarValor(cmbEditMateria, dictado.IdMateria);
            SeleccionarValor(cmbEditProfesor, dictado.IdProfesor);
            cmbEditDia.SelectedItem = dictado.Dia;
            txtEditHorario.Text = dictado.Horario;
            txtEditGrupo.Text = dictado.Grupo;
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

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (idDictadoSeleccionado == 0)
            {
                MessageBox.Show("Busque y seleccione un dictado primero.");
                return;
            }

            if (!ValidarCampos(cmbEditMateria, cmbEditProfesor, cmbEditDia, txtEditHorario, txtEditGrupo))
                return;

            try
            {
                Dictado dictado = new Dictado
                {
                    IdDictado = idDictadoSeleccionado,
                    IdMateria = Convert.ToInt32(cmbEditMateria.SelectedValue),
                    IdProfesor = Convert.ToInt32(cmbEditProfesor.SelectedValue),
                    Dia = Convert.ToString(cmbEditDia.SelectedItem),
                    Horario = ObtenerHorario(txtEditHorario.Text),
                    Grupo = txtEditGrupo.Text.Trim().ToUpper(),
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
            cmbEditMateria.SelectedIndex = -1;
            cmbEditProfesor.SelectedIndex = -1;
            cmbEditDia.SelectedIndex = -1;
            txtEditHorario.Clear();
            txtEditGrupo.Clear();
            nudEditAnio.Value = LimitarAnio(nudEditAnio, DateTime.Now.Year);
        }

        private bool ValidarCampos(
            ComboBox materia, ComboBox profesor, ComboBox dia,
            TextBox horario, TextBox grupo)
        {
            if (materia.SelectedIndex < 0)
            {
                MessageBox.Show("Seleccione una materia.");
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

            if (!TimeSpan.TryParse(horario.Text, out _))
            {
                MessageBox.Show("Horario inválido. Ejemplo: 08:00");
                return false;
            }

            if (string.IsNullOrWhiteSpace(grupo.Text))
            {
                MessageBox.Show("Ingrese el grupo.");
                return false;
            }

            return true;
        }

        private string ObtenerHorario(string texto)
        {
            TimeSpan hora = TimeSpan.Parse(texto.Trim());

            return hora.ToString(@"hh\:mm\:ss");
        }
    }
}
