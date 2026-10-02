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
    public partial class FrmEspecialidades : Form
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

            Tema.EstilizarBoton(btnBuscar, false);
            Tema.EstilizarBoton(btnLimpiarEditar, false);

            // Catálogo fijo (Ciclo Básico + 5 tecnicaturas): solo consulta.
            // El ABM existe por normalización pero no se edita a mano.
            grpCrear.Visible = false;
            btnModificar.Visible = false;
            btnEliminar.Visible = false;
            txtEditNombre.ReadOnly = true;
            grpModificar.Text = "Catálogo (solo consulta)";
            var aviso = new Label
            {
                AutoSize = true,
                Location = new System.Drawing.Point(16, 150),
                Text = "Catálogo fijo: Ciclo Básico + 5 tecnicaturas (se eligen desde Materias).",
                ForeColor = System.Drawing.Color.Gray
            };
            grpModificar.Controls.Add(aviso);
            txtEditNombre.Location = new System.Drawing.Point(90, 57);
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
                MessageBox.Show("Ingrese el nombre de la especialidad.");
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
                    MessageBox.Show("Especialidad agregada correctamente.");

                    CargarEspecialidades();
                    LimpiarCreacion();
                }
                else
                {
                    MessageBox.Show("No se pudo agregar la especialidad.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo agregar la especialidad.\n" + ex.Message);
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
                MessageBox.Show("Sin resultados para esa búsqueda.");
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
                MessageBox.Show("Busque y seleccione una especialidad primero.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtEditNombre.Text))
            {
                MessageBox.Show("Ingrese el nombre de la especialidad.");
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
                    MessageBox.Show("Especialidad modificada correctamente.");

                    CargarEspecialidades();
                    LimpiarEdicion();
                }
                else
                {
                    MessageBox.Show("No se pudo modificar la especialidad.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo modificar la especialidad.\n" + ex.Message);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idEspecialidadSeleccionada == 0)
            {
                MessageBox.Show("Busque y seleccione una especialidad primero.");
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
                        if (ok) MessageBox.Show("Especialidad eliminada definitivamente.");
                    }
                    else
                    {
                        ok = especialidadController.DarDeBaja(idEspecialidadSeleccionada);
                        if (ok) MessageBox.Show("Especialidad dada de baja.");
                    }

                    if (ok)
                    {
                        CargarEspecialidades();
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
            idEspecialidadSeleccionada = 0;
            lblEditando.Text = "Editando: (seleccione de la lista)";
            txtEditNombre.Clear();
        }
    }
}
