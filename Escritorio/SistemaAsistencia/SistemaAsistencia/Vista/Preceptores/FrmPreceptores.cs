using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SistemaAsistencia.Controlador;
using SistemaAsistencia.Modelo.DAO;
using SistemaAsistencia.Modelo.Entidades;
using SistemaAsistencia.Vista.Comun;
using SistemaAsistencia.Utilidades;

namespace SistemaAsistencia.Vista.Preceptores
{
    public partial class FrmPreceptores : FrmBaseHijo
    {
        private readonly PreceptorController preceptorController;
        private List<Preceptor> cachePreceptores = new List<Preceptor>();
        private int idPreceptorSeleccionado = 0;

        public FrmPreceptores()
        {
            InitializeComponent();

            preceptorController = new PreceptorController();

            Tema.ConfigurarFondo(this);
            Tema.EstilizarGrilla(dgvPreceptores);

            // Regla EEST: legajo = DNI (autocompletado, no editable).
            txtLegajo.ReadOnly = true;
            txtLegajo.TabStop = false;
            txtEditLegajo.ReadOnly = true;
            txtEditLegajo.TabStop = false;
            txtDni.TextChanged += (s, e) => txtLegajo.Text = txtDni.Text.Trim();
            txtEditDni.TextChanged += (s, e) => txtEditLegajo.Text = txtEditDni.Text.Trim();
        }

        private void FrmPreceptores_Load(object sender, EventArgs e)
        {
            CargaVista.IntentarCarga(CargarPreceptores, "preceptores");
            EstadoActivoHelper.AplicarPermiso(btnToggleActivo);
            EstadoActivoHelper.ActualizarTexto(btnToggleActivo, null);
        }

        private void CargarPreceptores()
        {
            cachePreceptores =
                CargaVista.ObtenerLista(() => preceptorController.ObtenerPreceptoresIncluyendoInactivos());
            MostrarEnGrilla(cachePreceptores);
        }

        private void MostrarEnGrilla(List<Preceptor> lista)
        {
            CargaVista.MostrarEnGrilla(dgvPreceptores, lista, ConfigurarColumnasPreceptores);
        }

        private void ConfigurarColumnasPreceptores(DataGridView dgv)
        {
            if (dgv.Columns.Count == 0) return;

            string[] soloRelevantes = { "NombrePreceptor", "ApellidoPreceptor", "Dni", "LegajoPreceptor", "CorreoPreceptor", "TelefonoPreceptor", "Activo" };
            foreach (DataGridViewColumn col in dgv.Columns)
                col.Visible = System.Array.IndexOf(soloRelevantes, col.Name) >= 0;

            if (dgv.Columns.Contains("NombrePreceptor"))
            {
                dgv.Columns["NombrePreceptor"].HeaderText = "Nombre";
                dgv.Columns["NombrePreceptor"].DisplayIndex = 0;
            }
            if (dgv.Columns.Contains("ApellidoPreceptor"))
            {
                dgv.Columns["ApellidoPreceptor"].HeaderText = "Apellido";
                dgv.Columns["ApellidoPreceptor"].DisplayIndex = 1;
            }
            if (dgv.Columns.Contains("Dni"))
            {
                dgv.Columns["Dni"].HeaderText = "DNI";
                dgv.Columns["Dni"].DisplayIndex = 2;
            }
            if (dgv.Columns.Contains("LegajoPreceptor"))
            {
                dgv.Columns["LegajoPreceptor"].HeaderText = "Legajo";
                dgv.Columns["LegajoPreceptor"].DisplayIndex = 3;
            }
            if (dgv.Columns.Contains("CorreoPreceptor"))
            {
                dgv.Columns["CorreoPreceptor"].HeaderText = "Correo";
                dgv.Columns["CorreoPreceptor"].DisplayIndex = 4;
                dgv.Columns["CorreoPreceptor"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            if (dgv.Columns.Contains("TelefonoPreceptor"))
            {
                dgv.Columns["TelefonoPreceptor"].HeaderText = "Teléfono";
                dgv.Columns["TelefonoPreceptor"].DisplayIndex = 5;
            }
            if (dgv.Columns.Contains("Activo"))
            {
                dgv.Columns["Activo"].HeaderText = "Activo";
                dgv.Columns["Activo"].DisplayIndex = 6;
                dgv.Columns["Activo"].ReadOnly = true;
            }
            if (dgv.Columns.Contains("Contrasena"))
                dgv.Columns["Contrasena"].Visible = false;
        }

        // Teléfono en 3 cajas (CtrlTelefono): | 54 | área máx 3 | número |.
        private bool TelefonoValidoSimple(CtrlTelefono ctrl)
        {
            if (!ctrl.EsValido(out string error))
            {
                MessageBox.Show(
                    error,
                    "Preceptores",
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
                    Preceptor preceptor = new Preceptor
                    {
                        NombrePreceptor = txtNombre.Text,
                        ApellidoPreceptor = txtApellido.Text,
                        LegajoPreceptor = txtDni.Text.Trim(),
                        Dni = txtDni.Text.Trim(),
                        CorreoPreceptor = txtCorreo.Text,
                        TelefonoPreceptor = ctrlTelCrear.Telefono,
                        Contrasena = txtDni.Text.Trim()
                    };

                    if (preceptorController.AgregarPreceptor(preceptor))
                    {
                        MessageBox.Show(
                            "Preceptor agregado correctamente.",
                            "Preceptores",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        CargarPreceptores();
                        LimpiarCreacion();
                    }
                    else
                    {
                        MessageBox.Show(
                            "No se pudo agregar el preceptor.",
                            "Preceptores",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "No se pudo agregar el preceptor.\n" + ex.Message,
                        "Preceptores",
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
            txtLegajo.Clear();
            txtDni.Clear();
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
                cachePreceptores,
                texto,
                p => (p.NombrePreceptor != null &&
                        p.NombrePreceptor.IndexOf(texto, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (p.ApellidoPreceptor != null &&
                        p.ApellidoPreceptor.IndexOf(texto, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (p.Dni != null &&
                        p.Dni.IndexOf(texto, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (p.LegajoPreceptor != null &&
                        p.LegajoPreceptor.IndexOf(texto, StringComparison.OrdinalIgnoreCase) >= 0),
                MostrarEnGrilla,
                CargarEnEdicion,
                LimpiarEdicion,
                "Preceptores",
                interactivo);
        }

        private void dgvPreceptores_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            Preceptor preceptor =
                dgvPreceptores.Rows[e.RowIndex].DataBoundItem as Preceptor;
            if (preceptor != null)
                CargarEnEdicion(preceptor);
        }

        private void CargarEnEdicion(Preceptor preceptor)
        {
            EjecutarConLayout(() =>
            {
                idPreceptorSeleccionado = preceptor.IdPreceptor;
                lblEditando.Text =
                    "Editando: " + preceptor.ApellidoPreceptor + ", " + preceptor.NombrePreceptor;
                txtEditNombre.Text = preceptor.NombrePreceptor;
                txtEditApellido.Text = preceptor.ApellidoPreceptor;
                txtEditLegajo.Text = preceptor.LegajoPreceptor;
                txtEditDni.Text = preceptor.Dni;
                txtEditCorreo.Text = preceptor.CorreoPreceptor;
                ctrlTelEdit.Telefono = preceptor.TelefonoPreceptor;
                EstadoActivoHelper.ActualizarTexto(btnToggleActivo, preceptor.Activo);
            });
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (idPreceptorSeleccionado == 0)
            {
                MessageBox.Show(
                    "Busque y seleccione un preceptor primero.",
                    "Preceptores",
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
                    Preceptor preceptor = new Preceptor
                    {
                        IdPreceptor = idPreceptorSeleccionado,
                        NombrePreceptor = txtEditNombre.Text,
                        ApellidoPreceptor = txtEditApellido.Text,
                        LegajoPreceptor = txtEditDni.Text.Trim(),
                        Dni = txtEditDni.Text.Trim(),
                        CorreoPreceptor = txtEditCorreo.Text,
                        TelefonoPreceptor = ctrlTelEdit.Telefono,
                    };

                    if (preceptorController.ModificarPreceptor(preceptor))
                    {
                        MessageBox.Show(
                            "Preceptor modificado correctamente.",
                            "Preceptores",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        CargarPreceptores();
                        LimpiarEdicion();
                    }
                    else
                    {
                        MessageBox.Show(
                            "No se pudo modificar el preceptor.",
                            "Preceptores",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "No se pudo modificar el preceptor.\n" + ex.Message,
                        "Preceptores",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            });
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idPreceptorSeleccionado == 0)
            {
                MessageBox.Show(
                    "Busque y seleccione un preceptor primero.",
                    "Preceptores",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            Preceptor preceptor = cachePreceptores.Find(p => p.IdPreceptor == idPreceptorSeleccionado);
            if (preceptor == null) return;

            EjecutarConLayout(() =>
            {
                try
                {
                    string descripcion = "Preceptor: " + preceptor.NombreCompleto +
                        " (Legajo " + preceptor.LegajoPreceptor + ")";

                    List<Dependencia> dependencias =
                        preceptorController.ObtenerDependencias(idPreceptorSeleccionado);

                    using (var confirmar = new FrmConfirmarEliminar(descripcion, dependencias))
                    {
                        if (confirmar.ShowDialog(this) != DialogResult.OK) return;

                        bool ok;

                        if (confirmar.Resultado == ResultadoEliminacion.Definitiva)
                        {
                            ok = preceptorController.EliminarDefinitivo(idPreceptorSeleccionado);
                            if (ok)
                                MessageBox.Show(
                                    "Preceptor eliminado definitivamente.",
                                    "Preceptores",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                        }
                        else
                        {
                            ok = preceptorController.DarDeBaja(idPreceptorSeleccionado);
                            if (ok)
                                MessageBox.Show(
                                    "Preceptor dado de baja.",
                                    "Preceptores",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                        }

                        if (ok)
                        {
                            CargarPreceptores();
                            LimpiarEdicion();
                        }
                        else
                        {
                            MessageBox.Show(
                                "No se pudo eliminar.",
                                "Preceptores",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "No se pudo eliminar.\n" + ex.Message,
                        "Preceptores",
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
            idPreceptorSeleccionado = 0;
            lblEditando.Text = "Editando: (seleccione de la lista)";
            txtEditNombre.Clear();
            txtEditApellido.Clear();
            txtEditLegajo.Clear();
            txtEditDni.Clear();
            txtEditCorreo.Clear();
            ctrlTelEdit.Limpiar();
            EstadoActivoHelper.ActualizarTexto(btnToggleActivo, null);
        }

        private void btnToggleActivo_Click(object sender, EventArgs e)
        {
            if (idPreceptorSeleccionado == 0)
            {
                MessageBox.Show(
                    "Busque y seleccione un preceptor primero.",
                    "Preceptores",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            Preceptor preceptor = cachePreceptores.Find(p => p.IdPreceptor == idPreceptorSeleccionado);
            if (preceptor == null) return;

            bool ok = EstadoActivoHelper.EjecutarToggle(
                idPreceptorSeleccionado,
                preceptor.Activo,
                preceptorController.DarDeBaja,
                preceptorController.DarDeAlta,
                "Preceptores");

            if (ok)
            {
                CargarPreceptores();
                LimpiarEdicion();
            }
        }
    }
}