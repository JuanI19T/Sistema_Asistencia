using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SistemaAsistencia.Controlador;
using SistemaAsistencia.Modelo.DAO;
using SistemaAsistencia.Modelo.Entidades;
using SistemaAsistencia.Vista.Comun;
using SistemaAsistencia.Utilidades;

namespace SistemaAsistencia.Vista.Especialidades
{
    public partial class FrmEspecialidades : FrmBaseHijo
    {
        private readonly EspecialidadController especialidadController;
        private List<Especialidad> cacheEspecialidades = new List<Especialidad>();
        private int idEspecialidadSeleccionada = 0;

        public FrmEspecialidades()
        {
            InitializeComponent();

            especialidadController = new EspecialidadController();

            Tema.ConfigurarFondo(this);
            Tema.EstilizarGrilla(dgvEspecialidades);

            // Catálogo fijo (Ciclo Básico + 5 tecnicaturas): solo consulta.
            // La división se muestra pero no se edita (evita corromper dictados).
            dgvEspecialidades.CellFormatting += DgvEspecialidades_CellFormatting;

            // tabPage1 se conserva en el Designer para uso futuro;
            // solo se oculta en runtime.
            materialTabControl1.TabPages.Remove(tabPage1);
            materialTabControl1.SelectedIndex = 0;
            materialTabSelector1.Refresh();
        }

        private void FrmEspecialidades_Load(object sender, EventArgs e)
        {
            CargaVista.IntentarCarga(CargarEspecialidades, "especialidades");
            EstadoActivoHelper.AplicarPermiso(btnToggleActivo);
            EstadoActivoHelper.ActualizarTexto(btnToggleActivo, null);
        }

        private void CargarEspecialidades()
        {
            cacheEspecialidades =
                CargaVista.ObtenerLista(() => especialidadController.ObtenerEspecialidadesIncluyendoInactivos());
            MostrarEnGrilla(cacheEspecialidades);
        }

        private void MostrarEnGrilla(List<Especialidad> lista)
        {
            CargaVista.MostrarEnGrilla(dgvEspecialidades, lista, ConfigurarColumnasEspecialidades);
        }

        private void ConfigurarColumnasEspecialidades(DataGridView dgv)
        {
            if (dgv.Columns.Count == 0) return;

            if (dgv.Columns.Contains("IdEspecialidad"))
                dgv.Columns["IdEspecialidad"].Visible = false;
            if (dgv.Columns.Contains("NombreEspecialidad"))
            {
                dgv.Columns["NombreEspecialidad"].HeaderText = "Especialidad";
                dgv.Columns["NombreEspecialidad"].DisplayIndex = 0;
                dgv.Columns["NombreEspecialidad"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            }
            if (dgv.Columns.Contains("Division"))
            {
                dgv.Columns["Division"].HeaderText = "División";
                dgv.Columns["Division"].DisplayIndex = 1;
                dgv.Columns["Division"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            if (dgv.Columns.Contains("Activo"))
            {
                dgv.Columns["Activo"].HeaderText = "Activo";
                dgv.Columns["Activo"].DisplayIndex = 2;
                dgv.Columns["Activo"].ReadOnly = true;
            }
        }

        // NULL (sin división fija) se muestra como "Libre".
        private void DgvEspecialidades_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvEspecialidades.Columns[e.ColumnIndex].Name == "Division" && e.Value == null)
            {
                e.Value = "Libre";
                e.FormattingApplied = true;
            }
        }

        // ---------------- 1. Crear ----------------

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show(
                    "Ingrese el nombre de la especialidad.",
                    "Especialidades",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtNombre.Focus();
                return;
            }

            EjecutarConLayout(() =>
            {
                try
                {
                    Especialidad especialidad = new Especialidad
                    {
                        NombreEspecialidad = txtNombre.Text.Trim().ToUpper()
                    };

                    if (especialidadController.AgregarEspecialidad(especialidad))
                    {
                        MessageBox.Show(
                            "Especialidad agregada correctamente.",
                            "Especialidades",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        CargarEspecialidades();
                        LimpiarCreacion();
                    }
                    else
                    {
                        MessageBox.Show(
                            "No se pudo agregar la especialidad.",
                            "Especialidades",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "No se pudo agregar la especialidad.\n" + ex.Message,
                        "Especialidades",
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
            txtNombre.Clear();
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

            CargaVista.FiltrarCache(
                cacheEspecialidades,
                texto,
                item => item.NombreEspecialidad != null &&
                        item.NombreEspecialidad.IndexOf(texto, StringComparison.OrdinalIgnoreCase) >= 0,
                MostrarEnGrilla,
                CargarEnEdicion,
                LimpiarEdicion,
                "Especialidades",
                interactivo);
        }

        private void dgvEspecialidades_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            Especialidad especialidad =
                dgvEspecialidades.Rows[e.RowIndex].DataBoundItem as Especialidad;
            if (especialidad != null)
                CargarEnEdicion(especialidad);
        }

        private void CargarEnEdicion(Especialidad especialidad)
        {
            EjecutarConLayout(() =>
            {
                idEspecialidadSeleccionada = especialidad.IdEspecialidad;
                lblEditando.Text = "Editando: " + especialidad.NombreEspecialidad;
                txtEditNombre.Text = especialidad.NombreEspecialidad;
                EstadoActivoHelper.ActualizarTexto(btnToggleActivo, especialidad.Activo);
            });
        }

        

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (idEspecialidadSeleccionada == 0)
            {
                MessageBox.Show(
                    "Busque y seleccione una especialidad primero.",
                    "Especialidades",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtEditNombre.Text))
            {
                MessageBox.Show(
                    "Ingrese el nombre de la especialidad.",
                    "Especialidades",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtEditNombre.Focus();
                return;
            }

            EjecutarConLayout(() =>
            {
                try
                {
                    Especialidad especialidad = new Especialidad
                    {
                        IdEspecialidad = idEspecialidadSeleccionada,
                        NombreEspecialidad = txtEditNombre.Text.Trim().ToUpper()
                    };

                    if (especialidadController.ModificarEspecialidad(especialidad))
                    {
                        MessageBox.Show(
                            "Especialidad modificada correctamente.",
                            "Especialidades",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        CargarEspecialidades();
                        LimpiarEdicion();
                    }
                    else
                    {
                        MessageBox.Show(
                            "No se pudo modificar la especialidad.",
                            "Especialidades",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "No se pudo modificar la especialidad.\n" + ex.Message,
                        "Especialidades",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            });
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idEspecialidadSeleccionada == 0)
            {
                MessageBox.Show(
                    "Busque y seleccione una especialidad primero.",
                    "Especialidades",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            Especialidad especialidad =
                cacheEspecialidades.Find(es => es.IdEspecialidad == idEspecialidadSeleccionada);

            if (especialidad == null) return;

            EjecutarConLayout(() =>
            {
                try
                {
                    string descripcion = "Especialidad: " + especialidad.NombreEspecialidad;

                    List<Dependencia> dependencias =
                        especialidadController.ObtenerDependencias(idEspecialidadSeleccionada);

                    using (var confirmar = new FrmConfirmarEliminar(descripcion, dependencias))
                    {
                        if (confirmar.ShowDialog(this) != DialogResult.OK) return;

                        bool ok;

                        if (confirmar.Resultado == ResultadoEliminacion.Definitiva)
                        {
                            ok = especialidadController.EliminarDefinitivo(idEspecialidadSeleccionada);
                            if (ok)
                                MessageBox.Show(
                                    "Especialidad eliminada definitivamente.",
                                    "Especialidades",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                        }
                        else
                        {
                            ok = especialidadController.DarDeBaja(idEspecialidadSeleccionada);
                            if (ok)
                                MessageBox.Show(
                                    "Especialidad dada de baja.",
                                    "Especialidades",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                        }

                        if (ok)
                        {
                            CargarEspecialidades();
                            LimpiarEdicion();
                        }
                        else
                        {
                            MessageBox.Show(
                                "No se pudo eliminar.",
                                "Especialidades",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "No se pudo eliminar.\n" + ex.Message,
                        "Especialidades",
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
            idEspecialidadSeleccionada = 0;
            lblEditando.Text = "Editando: (seleccione de la lista)";
            txtEditNombre.Clear();
            EstadoActivoHelper.ActualizarTexto(btnToggleActivo, null);
        }

        private void btnToggleActivo_Click(object sender, EventArgs e)
        {
            if (idEspecialidadSeleccionada == 0)
            {
                MessageBox.Show(
                    "Busque y seleccione una especialidad primero.",
                    "Especialidades",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            Especialidad especialidad =
                cacheEspecialidades.Find(es => es.IdEspecialidad == idEspecialidadSeleccionada);
            if (especialidad == null) return;

            bool ok = EstadoActivoHelper.EjecutarToggle(
                idEspecialidadSeleccionada,
                especialidad.Activo,
                especialidadController.DarDeBaja,
                especialidadController.DarDeAlta,
                "Especialidades");

            if (ok)
            {
                CargarEspecialidades();
                LimpiarEdicion();
            }
        }

        private void materialTabSelector1_Click(object sender, EventArgs e)
        {

        }
    }
}
