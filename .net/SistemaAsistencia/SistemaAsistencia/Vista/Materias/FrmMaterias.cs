using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SistemaAsistencia.Controlador;
using SistemaAsistencia.Modelo.DAO;
using SistemaAsistencia.Modelo.Entidades;
using SistemaAsistencia.Vista.Comun;

namespace SistemaAsistencia.Vista.Materias
{
    public partial class FrmMaterias : Form
    {
        private readonly MateriaController materiaController;
        private readonly EspecialidadController especialidadController;
        private List<Materia> cacheMaterias = new List<Materia>();
        private int idMateriaSeleccionada = 0;

        public FrmMaterias()
        {
            InitializeComponent();

            materiaController = new MateriaController();
            especialidadController = new EspecialidadController();
        }

        private void FrmMaterias_Load(object sender, EventArgs e)
        {
            try
            {
                CargarEspecialidades();
                CargarMaterias();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo cargar materias.\n" + ex.Message,
                    "Error de conexión",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void CargarEspecialidades()
        {
            List<Especialidad> lista = especialidadController.ObtenerEspecialidades();

            cmbEspecialidad.DataSource = new List<Especialidad>(lista);
            cmbEspecialidad.DisplayMember = "NombreEspecialidad";
            cmbEspecialidad.ValueMember = "IdEspecialidad";
            cmbEspecialidad.SelectedIndex = -1;

            cmbEditEspecialidad.DataSource = new List<Especialidad>(lista);
            cmbEditEspecialidad.DisplayMember = "NombreEspecialidad";
            cmbEditEspecialidad.ValueMember = "IdEspecialidad";
            cmbEditEspecialidad.SelectedIndex = -1;
        }

        private void CargarMaterias()
        {
            cacheMaterias = materiaController.ObtenerMaterias() ?? new List<Materia>();
            MostrarEnGrilla(cacheMaterias);
        }

        private void MostrarEnGrilla(List<Materia> lista)
        {
            dgvMaterias.DataSource = null;
            dgvMaterias.DataSource = lista;

            if (dgvMaterias.Columns.Count > 0)
            {
                if (dgvMaterias.Columns.Contains("IdMateria"))
                    dgvMaterias.Columns["IdMateria"].Visible = false;
                if (dgvMaterias.Columns.Contains("IdEspecialidad"))
                    dgvMaterias.Columns["IdEspecialidad"].Visible = false;
                if (dgvMaterias.Columns.Contains("NombreMateria"))
                    dgvMaterias.Columns["NombreMateria"].HeaderText = "Materia";
                if (dgvMaterias.Columns.Contains("NombreEspecialidad"))
                    dgvMaterias.Columns["NombreEspecialidad"].HeaderText = "Especialidad";
                if (dgvMaterias.Columns.Contains("CargaHoraria"))
                    dgvMaterias.Columns["CargaHoraria"].HeaderText = "Carga Horaria";
                if (dgvMaterias.Columns.Contains("AnioMateria"))
                    dgvMaterias.Columns["AnioMateria"].HeaderText = "Año";
            }
        }

        // ---------------- 1. Crear ----------------

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (cmbEspecialidad.SelectedIndex < 0)
            {
                MessageBox.Show("Seleccione una especialidad.");
                cmbEspecialidad.Focus();
                return;
            }

            try
            {
                Materia materia = new Materia
                {
                    IdEspecialidad = Convert.ToInt32(cmbEspecialidad.SelectedValue),
                    NombreMateria = txtNombre.Text,
                    CargaHoraria = Convert.ToInt32(nudCargaHoraria.Value),
                    AnioMateria = Convert.ToInt32(nudAnioMateria.Value)
                };

                if (materiaController.AgregarMateria(materia))
                {
                    MessageBox.Show("Materia agregada correctamente.");

                    CargarMaterias();
                    LimpiarCreacion();
                }
                else
                {
                    MessageBox.Show("No se pudo agregar la materia.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo agregar la materia.\n" + ex.Message);
            }
        }

        private void btnLimpiarCrear_Click(object sender, EventArgs e)
        {
            LimpiarCreacion();
        }

        private void LimpiarCreacion()
        {
            cmbEspecialidad.SelectedIndex = -1;
            txtNombre.Clear();
            nudCargaHoraria.Value = 0;
            nudAnioMateria.Value = 0;
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
                MostrarEnGrilla(cacheMaterias);
                return;
            }

            List<Materia> resultados = cacheMaterias.FindAll(m =>
                m.NombreMateria != null &&
                m.NombreMateria.IndexOf(texto, StringComparison.OrdinalIgnoreCase) >= 0);

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

        private void dgvMaterias_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            Materia materia = dgvMaterias.Rows[e.RowIndex].DataBoundItem as Materia;
            if (materia != null)
                CargarEnEdicion(materia);
        }

        private void CargarEnEdicion(Materia materia)
        {
            idMateriaSeleccionada = materia.IdMateria;
            lblEditando.Text = "Editando: " + materia.NombreMateria;
            txtEditNombre.Text = materia.NombreMateria;
            SeleccionarEspecialidad(cmbEditEspecialidad, materia.IdEspecialidad);
            nudEditCarga.Value = Limitar(nudEditCarga, materia.CargaHoraria);
            nudEditAnio.Value = Limitar(nudEditAnio, materia.AnioMateria);
        }

        private static void SeleccionarEspecialidad(ComboBox combo, int idEspecialidad)
        {
            try { combo.SelectedValue = idEspecialidad; }
            catch { combo.SelectedIndex = -1; }
            if (combo.SelectedIndex < 0)
                combo.SelectedIndex = -1;
        }

        private static decimal Limitar(NumericUpDown nud, int valor)
        {
            if (valor < nud.Minimum) return nud.Minimum;
            if (valor > nud.Maximum) return nud.Maximum;
            return valor;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (idMateriaSeleccionada == 0)
            {
                MessageBox.Show("Busque y seleccione una materia primero.");
                return;
            }

            if (cmbEditEspecialidad.SelectedIndex < 0)
            {
                MessageBox.Show("Seleccione una especialidad.");
                return;
            }

            try
            {
                Materia materia = new Materia
                {
                    IdMateria = idMateriaSeleccionada,
                    IdEspecialidad = Convert.ToInt32(cmbEditEspecialidad.SelectedValue),
                    NombreMateria = txtEditNombre.Text,
                    CargaHoraria = Convert.ToInt32(nudEditCarga.Value),
                    AnioMateria = Convert.ToInt32(nudEditAnio.Value)
                };

                if (materiaController.ModificarMateria(materia))
                {
                    MessageBox.Show("Materia modificada correctamente.");

                    CargarMaterias();
                    LimpiarEdicion();
                }
                else
                {
                    MessageBox.Show("No se pudo modificar la materia.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo modificar la materia.\n" + ex.Message);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idMateriaSeleccionada == 0)
            {
                MessageBox.Show("Busque y seleccione una materia primero.");
                return;
            }

            Materia materia = cacheMaterias.Find(m => m.IdMateria == idMateriaSeleccionada);
            if (materia == null) return;

            try
            {
                string descripcion = "Materia: " + materia.NombreMateria;

                List<Dependencia> dependencias =
                    materiaController.ObtenerDependencias(idMateriaSeleccionada);

                using (var confirmar = new FrmConfirmarEliminar(descripcion, dependencias))
                {
                    if (confirmar.ShowDialog(this) != DialogResult.OK) return;

                    bool ok;

                    if (confirmar.Resultado == ResultadoEliminacion.Definitiva)
                    {
                        ok = materiaController.EliminarDefinitivo(idMateriaSeleccionada);
                        if (ok) MessageBox.Show("Materia eliminada definitivamente.");
                    }
                    else
                    {
                        ok = materiaController.DarDeBaja(idMateriaSeleccionada);
                        if (ok) MessageBox.Show("Materia dada de baja.");
                    }

                    if (ok)
                    {
                        CargarMaterias();
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
            idMateriaSeleccionada = 0;
            lblEditando.Text = "Editando: (seleccione de la lista)";
            txtEditNombre.Clear();
            cmbEditEspecialidad.SelectedIndex = -1;
            nudEditCarga.Value = 0;
            nudEditAnio.Value = 0;
        }
    }
}
