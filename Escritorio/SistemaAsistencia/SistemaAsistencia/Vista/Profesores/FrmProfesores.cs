using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SistemaAsistencia.Controlador;
using SistemaAsistencia.Modelo.DAO;
using SistemaAsistencia.Modelo.Entidades;
using SistemaAsistencia.Vista.Comun;
using SistemaAsistencia.Utilidades;

namespace SistemaAsistencia.Vista.Profesores
{
    public partial class FrmProfesores : FrmBaseHijo
    {
        private readonly ProfesorController profesorController;
        private List<Profesor> cacheProfesores = new List<Profesor>();
        private int idProfesorSeleccionado = 0;

        public FrmProfesores()
        {
            InitializeComponent();

            profesorController = new ProfesorController();

            Tema.ConfigurarFondo(this);
            Tema.EstilizarGrilla(dgvProfesores);

            // Regla EEST: legajo = DNI (autocompletado, no editable).
            txtLegajo.ReadOnly = true;
            txtLegajo.TabStop = false;
            txtEditLegajo.ReadOnly = true;
            txtEditLegajo.TabStop = false;
            txtDni.TextChanged += (s, e) => txtLegajo.Text = txtDni.Text.Trim();
            txtEditDni.TextChanged += (s, e) => txtEditLegajo.Text = txtEditDni.Text.Trim();
        }

        private void FrmProfesores_Load(object sender, EventArgs e)
        {
            CargaVista.IntentarCarga(CargarProfesores, "profesores");
            EstadoActivoHelper.AplicarPermiso(btnToggleActivo);
            EstadoActivoHelper.ActualizarTexto(btnToggleActivo, null);
        }

        private void CargarProfesores()
        {
            cacheProfesores =
                CargaVista.ObtenerLista(() => profesorController.ObtenerProfesoresIncluyendoInactivos());
            MostrarEnGrilla(cacheProfesores);
        }

        private void MostrarEnGrilla(List<Profesor> lista)
        {
            CargaVista.MostrarEnGrilla(dgvProfesores, lista, ConfigurarColumnasProfesores);
        }

        private void ConfigurarColumnasProfesores(DataGridView dgv)
        {
            if (dgv.Columns.Count == 0) return;

            string[] soloRelevantes = { "NombreProfesor", "ApellidoProfesor", "DniProfesor", "LegajoProfesor", "CorreoProfesor", "TelefonoProfesor", "Activo" };
            foreach (DataGridViewColumn col in dgv.Columns)
                col.Visible = System.Array.IndexOf(soloRelevantes, col.Name) >= 0;

            if (dgv.Columns.Contains("NombreProfesor"))
            {
                dgv.Columns["NombreProfesor"].HeaderText = "Nombre";
                dgv.Columns["NombreProfesor"].DisplayIndex = 0;
            }
            if (dgv.Columns.Contains("ApellidoProfesor"))
            {
                dgv.Columns["ApellidoProfesor"].HeaderText = "Apellido";
                dgv.Columns["ApellidoProfesor"].DisplayIndex = 1;
            }
            if (dgv.Columns.Contains("DniProfesor"))
            {
                dgv.Columns["DniProfesor"].HeaderText = "DNI";
                dgv.Columns["DniProfesor"].DisplayIndex = 2;
            }
            if (dgv.Columns.Contains("LegajoProfesor"))
            {
                dgv.Columns["LegajoProfesor"].HeaderText = "Legajo";
                dgv.Columns["LegajoProfesor"].DisplayIndex = 3;
            }
            if (dgv.Columns.Contains("CorreoProfesor"))
            {
                dgv.Columns["CorreoProfesor"].HeaderText = "Correo";
                dgv.Columns["CorreoProfesor"].DisplayIndex = 4;
                dgv.Columns["CorreoProfesor"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            if (dgv.Columns.Contains("TelefonoProfesor"))
            {
                dgv.Columns["TelefonoProfesor"].HeaderText = "Teléfono";
                dgv.Columns["TelefonoProfesor"].DisplayIndex = 5;
            }
            if (dgv.Columns.Contains("Activo"))
            {
                dgv.Columns["Activo"].HeaderText = "Activo";
                dgv.Columns["Activo"].DisplayIndex = 6;
                dgv.Columns["Activo"].ReadOnly = true;
            }
        }

        // Teléfono en 3 cajas (CtrlTelefono): | 54 | área máx 3 | número |.
        private bool TelefonoValidoSimple(SistemaAsistencia.Vista.Comun.CtrlTelefono ctrl)
        {
            if (!ctrl.EsValido(out string error))
            {
                MessageBox.Show(
                    error,
                    "Profesores",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                ctrl.Enfocar();
                return false;
            }
            return true;
        }

        // ---------------- 1. Crear ----------------

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!TelefonoValidoSimple(ctrlTelCrear))
                return;

            EjecutarConLayout(() =>
            {
                try
                {
                    Profesor profesor = new Profesor
                    {
                        NombreProfesor = txtNombre.Text,
                        ApellidoProfesor = txtApellido.Text,
                        DniProfesor = txtDni.Text,
                        LegajoProfesor = txtDni.Text.Trim(),
                        CorreoProfesor = txtCorreo.Text,
                        TelefonoProfesor = ctrlTelCrear.Telefono
                    };

                    if (profesorController.AgregarProfesor(profesor))
                    {
                        MessageBox.Show(
                            "Profesor agregado correctamente.",
                            "Profesores",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        CargarProfesores();
                        LimpiarCreacion();
                    }
                    else
                    {
                        MessageBox.Show(
                            "No se pudo agregar el profesor.",
                            "Profesores",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "No se pudo agregar el profesor.\n" + ex.Message,
                        "Profesores",
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
            txtApellido.Clear();
            txtDni.Clear();
            txtLegajo.Clear();
            txtCorreo.Clear();
            ctrlTelCrear.Limpiar();
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
                cacheProfesores,
                texto,
                p => (p.NombreProfesor != null &&
                        p.NombreProfesor.IndexOf(texto, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (p.ApellidoProfesor != null &&
                        p.ApellidoProfesor.IndexOf(texto, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (p.DniProfesor != null &&
                        p.DniProfesor.IndexOf(texto, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (p.LegajoProfesor != null &&
                        p.LegajoProfesor.IndexOf(texto, StringComparison.OrdinalIgnoreCase) >= 0),
                MostrarEnGrilla,
                CargarEnEdicion,
                LimpiarEdicion,
                "Profesores",
                interactivo);
        }

        private void dgvProfesores_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            Profesor profesor =
                dgvProfesores.Rows[e.RowIndex].DataBoundItem as Profesor;
            if (profesor != null)
                CargarEnEdicion(profesor);
        }

        private void CargarEnEdicion(Profesor profesor)
        {
            EjecutarConLayout(() =>
            {
                idProfesorSeleccionado = profesor.IdProfesor;
                lblEditando.Text =
                    "Editando: " + profesor.ApellidoProfesor + ", " + profesor.NombreProfesor;
                txtEditNombre.Text = profesor.NombreProfesor;
                txtEditApellido.Text = profesor.ApellidoProfesor;
                txtEditDni.Text = profesor.DniProfesor;
                txtEditLegajo.Text = profesor.LegajoProfesor;
                txtEditCorreo.Text = profesor.CorreoProfesor;
                ctrlTelEdit.Telefono = profesor.TelefonoProfesor;
                EstadoActivoHelper.ActualizarTexto(btnToggleActivo, profesor.Activo);
            });
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (idProfesorSeleccionado == 0)
            {
                MessageBox.Show(
                    "Busque y seleccione un profesor primero.",
                    "Profesores",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (!TelefonoValidoSimple(ctrlTelEdit))
                return;

            EjecutarConLayout(() =>
            {
                try
                {
                    Profesor profesor = new Profesor
                    {
                        IdProfesor = idProfesorSeleccionado,
                        NombreProfesor = txtEditNombre.Text,
                        ApellidoProfesor = txtEditApellido.Text,
                        DniProfesor = txtEditDni.Text,
                        LegajoProfesor = txtEditDni.Text.Trim(),
                        CorreoProfesor = txtEditCorreo.Text,
                        TelefonoProfesor = ctrlTelEdit.Telefono
                    };

                    if (profesorController.ModificarProfesor(profesor))
                    {
                        MessageBox.Show(
                            "Profesor modificado correctamente.",
                            "Profesores",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        CargarProfesores();
                        LimpiarEdicion();
                    }
                    else
                    {
                        MessageBox.Show(
                            "No se pudo modificar el profesor.",
                            "Profesores",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "No se pudo modificar el profesor.\n" + ex.Message,
                        "Profesores",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            });
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idProfesorSeleccionado == 0)
            {
                MessageBox.Show(
                    "Busque y seleccione un profesor primero.",
                    "Profesores",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            Profesor profesor = cacheProfesores.Find(p => p.IdProfesor == idProfesorSeleccionado);
            if (profesor == null) return;

            EjecutarConLayout(() =>
            {
                try
                {
                    string descripcion = "Profesor: " + profesor.NombreCompleto +
                        " (Legajo " + profesor.LegajoProfesor + ")";

                    List<Dependencia> dependencias =
                        profesorController.ObtenerDependencias(idProfesorSeleccionado);

                    using (var confirmar = new FrmConfirmarEliminar(descripcion, dependencias))
                    {
                        if (confirmar.ShowDialog(this) != DialogResult.OK) return;

                        bool ok;

                        if (confirmar.Resultado == ResultadoEliminacion.Definitiva)
                        {
                            ok = profesorController.EliminarDefinitivo(idProfesorSeleccionado);
                            if (ok)
                                MessageBox.Show(
                                    "Profesor eliminado definitivamente.",
                                    "Profesores",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                        }
                        else
                        {
                            ok = profesorController.DarDeBaja(idProfesorSeleccionado);
                            if (ok)
                                MessageBox.Show(
                                    "Profesor dado de baja.",
                                    "Profesores",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                        }

                        if (ok)
                        {
                            CargarProfesores();
                            LimpiarEdicion();
                        }
                        else
                        {
                            MessageBox.Show(
                                "No se pudo eliminar.",
                                "Profesores",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "No se pudo eliminar.\n" + ex.Message,
                        "Profesores",
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
            idProfesorSeleccionado = 0;
            lblEditando.Text = "Editando: (seleccione de la lista)";
            txtEditNombre.Clear();
            txtEditApellido.Clear();
            txtEditDni.Clear();
            txtEditLegajo.Clear();
            txtEditCorreo.Clear();
            ctrlTelEdit.Limpiar();
            EstadoActivoHelper.ActualizarTexto(btnToggleActivo, null);
        }

        private void btnToggleActivo_Click(object sender, EventArgs e)
        {
            if (idProfesorSeleccionado == 0)
            {
                MessageBox.Show(
                    "Busque y seleccione un profesor primero.",
                    "Profesores",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            Profesor profesor = cacheProfesores.Find(p => p.IdProfesor == idProfesorSeleccionado);
            if (profesor == null) return;

            bool ok = EstadoActivoHelper.EjecutarToggle(
                idProfesorSeleccionado,
                profesor.Activo,
                profesorController.DarDeBaja,
                profesorController.DarDeAlta,
                "Profesores");

            if (ok)
            {
                CargarProfesores();
                LimpiarEdicion();
            }
        }
    }
}
