using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SistemaAsistencia.Controlador;
using SistemaAsistencia.Modelo.DAO;
using SistemaAsistencia.Modelo.Entidades;
using SistemaAsistencia.Vista.Comun;
using SistemaAsistencia.Utilidades;

namespace SistemaAsistencia.Vista.Materias
{
    public partial class FrmMaterias : FrmBaseHijo
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

            Tema.ConfigurarFondo(this);
            Tema.EstilizarGrilla(dgvMaterias);

            // La especialidad condiciona el año: Ciclo Básico 1-3, tecnicaturas 4-7.
            cmbEspecialidad.SelectedIndexChanged += (s, e) =>
                AjustarRangoAnio(cmbEspecialidad, nudAnioMateria);
            cmbEditEspecialidad.SelectedIndexChanged += (s, e) =>
                AjustarRangoAnio(cmbEditEspecialidad, nudEditAnio);
        }

        private static void AjustarRangoAnio(ComboBox combo, NumericUpDown nud)
        {
            int min = 1, max = 7;
            if (combo.SelectedIndex >= 0)
            {
                if (EsCicloBasico(NombreEspecialidadDe(combo))) { min = 1; max = 3; }
                else { min = 4; max = 7; }
            }

            // Orden seguro: nunca dejar Value fuera de [Minimum, Maximum].
            decimal v = nud.Value;
            if (v < min) { nud.Maximum = max; nud.Minimum = min; nud.Value = min; }
            else if (v > max) { nud.Minimum = min; nud.Maximum = max; nud.Value = max; }
            else { nud.Minimum = min; nud.Maximum = max; }
        }

        private void FrmMaterias_Load(object sender, EventArgs e)
        {
            CargaVista.IntentarCarga(() =>
            {
                CargarEspecialidades();
                CargarMaterias();
            }, "materias");
            EstadoActivoHelper.AplicarPermiso(btnToggleActivo);
            EstadoActivoHelper.ActualizarTexto(btnToggleActivo, null);
        }

        private void CargarEspecialidades()
        {
            List<Especialidad> lista =
                CargaVista.ObtenerLista(() => especialidadController.ObtenerEspecialidades());

            CargaVista.CargarCombo(cmbEspecialidad, lista, "NombreEspecialidad", "IdEspecialidad");
            CargaVista.CargarCombo(cmbEditEspecialidad, lista, "NombreEspecialidad", "IdEspecialidad");
        }

        private void CargarMaterias()
        {
            cacheMaterias = CargaVista.ObtenerLista(() => materiaController.ObtenerMateriasIncluyendoInactivos());
            MostrarEnGrilla(cacheMaterias);
        }

        private void MostrarEnGrilla(List<Materia> lista)
        {
            CargaVista.MostrarEnGrilla(dgvMaterias, lista, ConfigurarColumnasMaterias);
        }

        private void ConfigurarColumnasMaterias(DataGridView dgv)
        {
            if (dgv.Columns.Count == 0) return;

            if (dgv.Columns.Contains("IdMateria"))
                dgv.Columns["IdMateria"].Visible = false;
            if (dgv.Columns.Contains("IdEspecialidad"))
                dgv.Columns["IdEspecialidad"].Visible = false;
            if (dgv.Columns.Contains("NombreMateria"))
                dgv.Columns["NombreMateria"].HeaderText = "Materia";
                dgv.Columns["NombreMateria"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            if (dgv.Columns.Contains("NombreEspecialidad"))
                dgv.Columns["NombreEspecialidad"].HeaderText = "Especialidad";
                dgv.Columns["NombreEspecialidad"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            if (dgv.Columns.Contains("CargaHoraria"))
                dgv.Columns["CargaHoraria"].HeaderText = "Carga Horaria";
            if (dgv.Columns.Contains("AnioMateria"))
                dgv.Columns["AnioMateria"].HeaderText = "Año";
            if (dgv.Columns.Contains("Ciclo"))
                dgv.Columns["Ciclo"].HeaderText = "Ciclo";
            if (dgv.Columns.Contains("Activo"))
            {
                dgv.Columns["Activo"].HeaderText = "Activo";
                dgv.Columns["Activo"].ReadOnly = true;
            }
        }

        // Regla EEST: años 1-3 van con "Ciclo Básico"; años 4-7 con su tecnicatura.
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

        private static bool CicloValido(int anio, string nombreEspecialidad)
        {
            bool basico = EsCicloBasico(nombreEspecialidad);
            if (anio >= 1 && anio <= 3 && !basico)
            {
                MessageBox.Show(
                    "Las materias de 1° a 3° año deben registrarse con la especialidad 'Ciclo Básico'.",
                    "Materias",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }
            if (anio >= 4 && anio <= 7 && basico)
            {
                MessageBox.Show(
                    "Las materias de 4° a 7° año deben registrarse con su tecnicatura (no 'Ciclo Básico').",
                    "Materias",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        // ---------------- 1. Crear ----------------

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (cmbEspecialidad.SelectedIndex < 0)
            {
                MessageBox.Show(
                    "Seleccione una especialidad.",
                    "Materias",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                cmbEspecialidad.Focus();
                return;
            }

            if (!CicloValido(Convert.ToInt32(nudAnioMateria.Value), NombreEspecialidadDe(cmbEspecialidad)))
            {
                cmbEspecialidad.Focus();
                return;
            }

            EjecutarConLayout(() =>
            {
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
                        MessageBox.Show(
                            "Materia agregada correctamente.",
                            "Materias",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        CargarMaterias();
                        LimpiarCreacion();
                    }
                    else
                    {
                        MessageBox.Show(
                            "No se pudo agregar la materia.",
                            "Materias",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "No se pudo agregar la materia.\n" + ex.Message,
                        "Materias",
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
            CargaVista.ReiniciarCombo(cmbEspecialidad);
            txtNombre.Clear();
            nudCargaHoraria.Value = 0;
            nudAnioMateria.Value = nudAnioMateria.Minimum;
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
                cacheMaterias,
                texto,
                m => m.NombreMateria != null &&
                     m.NombreMateria.IndexOf(texto, StringComparison.OrdinalIgnoreCase) >= 0,
                MostrarEnGrilla,
                CargarEnEdicion,
                LimpiarEdicion,
                "Materias",
                interactivo);
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
            EjecutarConLayout(() =>
            {
                idMateriaSeleccionada = materia.IdMateria;
                lblEditando.Text = "Editando: " + materia.NombreMateria;
                txtEditNombre.Text = materia.NombreMateria;
                SeleccionarEspecialidad(cmbEditEspecialidad, materia.IdEspecialidad);
                nudEditCarga.Value = Limitar(nudEditCarga, materia.CargaHoraria);
                nudEditAnio.Value = Limitar(nudEditAnio, materia.AnioMateria);
                EstadoActivoHelper.ActualizarTexto(btnToggleActivo, materia.Activo);
            });
        }

        private static void SeleccionarEspecialidad(ComboBox combo, int idEspecialidad)
        {
            try { combo.SelectedValue = idEspecialidad; }
            catch { combo.SelectedIndex = -1; }
            if (combo.SelectedIndex < 0)
                combo.SelectedIndex = -1;
            CargaVista.Refrescar(combo);
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
                MessageBox.Show(
                    "Busque y seleccione una materia primero.",
                    "Materias",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (cmbEditEspecialidad.SelectedIndex < 0)
            {
                MessageBox.Show(
                    "Seleccione una especialidad.",
                    "Materias",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                cmbEditEspecialidad.Focus();
                return;
            }

            if (!CicloValido(Convert.ToInt32(nudEditAnio.Value), NombreEspecialidadDe(cmbEditEspecialidad)))
                return;

            EjecutarConLayout(() =>
            {
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
                        MessageBox.Show(
                            "Materia modificada correctamente.",
                            "Materias",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        CargarMaterias();
                        LimpiarEdicion();
                    }
                    else
                    {
                        MessageBox.Show(
                            "No se pudo modificar la materia.",
                            "Materias",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "No se pudo modificar la materia.\n" + ex.Message,
                        "Materias",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            });
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idMateriaSeleccionada == 0)
            {
                MessageBox.Show(
                    "Busque y seleccione una materia primero.",
                    "Materias",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            Materia materia = cacheMaterias.Find(m => m.IdMateria == idMateriaSeleccionada);
            if (materia == null) return;

            EjecutarConLayout(() =>
            {
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
                            if (ok)
                                MessageBox.Show(
                                    "Materia eliminada definitivamente.",
                                    "Materias",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                        }
                        else
                        {
                            ok = materiaController.DarDeBaja(idMateriaSeleccionada);
                            if (ok)
                                MessageBox.Show(
                                    "Materia dada de baja.",
                                    "Materias",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                        }

                        if (ok)
                        {
                            CargarMaterias();
                            LimpiarEdicion();
                        }
                        else
                        {
                            MessageBox.Show(
                                "No se pudo eliminar.",
                                "Materias",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "No se pudo eliminar.\n" + ex.Message,
                        "Materias",
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
            idMateriaSeleccionada = 0;
            lblEditando.Text = "Editando: (seleccione de la lista)";
            txtEditNombre.Clear();
            CargaVista.ReiniciarCombo(cmbEditEspecialidad);
            nudEditCarga.Value = 0;
            nudEditAnio.Value = nudEditAnio.Minimum;
            EstadoActivoHelper.ActualizarTexto(btnToggleActivo, null);
        }

        private void btnToggleActivo_Click(object sender, EventArgs e)
        {
            if (idMateriaSeleccionada == 0)
            {
                MessageBox.Show(
                    "Busque y seleccione una materia primero.",
                    "Materias",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            Materia materia = cacheMaterias.Find(m => m.IdMateria == idMateriaSeleccionada);
            if (materia == null) return;

            bool ok = EstadoActivoHelper.EjecutarToggle(
                idMateriaSeleccionada,
                materia.Activo,
                materiaController.DarDeBaja,
                materiaController.DarDeAlta,
                "Materias");

            if (ok)
            {
                CargarMaterias();
                LimpiarEdicion();
            }
        }
    }
}
