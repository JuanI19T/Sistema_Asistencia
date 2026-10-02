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
            // La pestaña Crear queda bloqueada (aviso + campo deshabilitado)
            // y en Modificar no se edita a mano (el ABM existe por
            // normalización pero las filas no se tocan).
            btnGuardar.Visible = false;
            btnLimpiarCrear.Visible = false;
            txtNombre.ReadOnly = true;
            txtNombre.TabStop = false;
            txtEditNombre.ReadOnly = true;
            // Solo consulta: se esconde la pestaña Crear.
            materialTabControl1.TabPages.Remove(tabPage1);
            var aviso = new Label
            {
                AutoSize = true,
                Location = new System.Drawing.Point(20, 170),
                Text = "Catálogo fijo: Ciclo Básico + 5 tecnicaturas (se eligen desde Materias).",
                ForeColor = System.Drawing.Color.Gray
            };
            tabPage2.Controls.Add(aviso);
        }

        private void FrmEspecialidades_Load(object sender, EventArgs e)
        {
            try
            {
                CargarEspecialidades();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo cargar especialidades.\n" + ex.Message,
                    "Error de conexión",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void CargarEspecialidades()
        {
            cacheEspecialidades =
                especialidadController.ObtenerEspecialidades() ?? new List<Especialidad>();
            MostrarEnGrilla(cacheEspecialidades);
        }

        private void MostrarEnGrilla(List<Especialidad> lista)
        {
            dgvEspecialidades.DataSource = null;
            dgvEspecialidades.DataSource = lista;

            if (dgvEspecialidades.Columns.Count > 0)
            {
                if (dgvEspecialidades.Columns.Contains("IdEspecialidad"))
                    dgvEspecialidades.Columns["IdEspecialidad"].Visible = false;
                if (dgvEspecialidades.Columns.Contains("NombreEspecialidad"))
                    dgvEspecialidades.Columns["NombreEspecialidad"].HeaderText = "Especialidad";
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

            if (string.IsNullOrEmpty(texto))
            {
                MostrarEnGrilla(cacheEspecialidades);
                return;
            }

            List<Especialidad> resultados = cacheEspecialidades.FindAll(item =>
                item.NombreEspecialidad != null &&
                item.NombreEspecialidad.IndexOf(texto, StringComparison.OrdinalIgnoreCase) >= 0);

            MostrarEnGrilla(resultados);

            if (!interactivo) return;

            if (resultados.Count == 0)
            {
                MessageBox.Show(
                    "Sin resultados para esa búsqueda.",
                    "Especialidades",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                LimpiarEdicion();
            }
            else if (resultados.Count == 1)
            {
                CargarEnEdicion(resultados[0]);
            }
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
            idEspecialidadSeleccionada = especialidad.IdEspecialidad;
            lblEditando.Text = "Editando: " + especialidad.NombreEspecialidad;
            txtEditNombre.Text = especialidad.NombreEspecialidad;
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
        }

        private void materialTabSelector1_Click(object sender, EventArgs e)
        {

        }
    }
}
