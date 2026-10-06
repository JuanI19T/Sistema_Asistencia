using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SistemaAsistencia.Controlador;
using SistemaAsistencia.Modelo.DAO;
using SistemaAsistencia.Modelo.Entidades;
using SistemaAsistencia.Vista.Comun;
using SistemaAsistencia.Utilidades;

namespace SistemaAsistencia.Vista.Alumnos
{
    public partial class FrmAlumnos : FrmBaseHijo
    {
        private readonly AlumnoController alumnoController;
        private List<Alumno> cacheAlumnos = new List<Alumno>();
        private int idAlumnoSeleccionado = 0;

        public FrmAlumnos()
        {
            InitializeComponent();

            alumnoController = new AlumnoController();

            Tema.ConfigurarFondo(this);
            Tema.EstilizarGrilla(dgvAlumnos);
        }

        private void FrmAlumnos_Load(object sender, EventArgs e)
        {
            CargaVista.IntentarCarga(CargarAlumnos, "alumnos");
        }

        private void CargarAlumnos()
        {
            cacheAlumnos = CargaVista.ObtenerLista(() => alumnoController.ObtenerAlumnos());
            MostrarEnGrilla(cacheAlumnos);
        }

        private void MostrarEnGrilla(List<Alumno> lista)
        {
            CargaVista.MostrarEnGrilla(dgvAlumnos, lista, ConfigurarColumnasAlumnos);
        }

        private void ConfigurarColumnasAlumnos(DataGridView dgv)
        {
                // Solo campos relevantes: Nombre, Apellido, DNI, Legajo, Correo, Teléfono, Activo.
                // Los teléfonos extra siguen en BD pero no se muestran (UI limpia).
                string[] soloRelevantes = { "NombreAlumno", "ApellidoAlumno", "DniAlumno", "LegajoAlumno", "CorreoAlumno", "TelefonoAlumno", "Activo" };
                foreach (DataGridViewColumn col in dgvAlumnos.Columns)
                    col.Visible = System.Array.IndexOf(soloRelevantes, col.Name) >= 0;

            if (dgv.Columns.Count == 0) return;

            if (dgv.Columns.Contains("NombreAlumno"))
            {
                dgv.Columns["NombreAlumno"].HeaderText = "Nombre";
                dgv.Columns["NombreAlumno"].DisplayIndex = 0;
            }
            if (dgv.Columns.Contains("ApellidoAlumno"))
            {
                dgv.Columns["ApellidoAlumno"].HeaderText = "Apellido";
                dgv.Columns["ApellidoAlumno"].DisplayIndex = 1;
            }
            if (dgv.Columns.Contains("DniAlumno"))
            {
                dgv.Columns["DniAlumno"].HeaderText = "DNI";
                dgv.Columns["DniAlumno"].DisplayIndex = 2;
            }
            if (dgv.Columns.Contains("LegajoAlumno"))
            {
                dgv.Columns["LegajoAlumno"].HeaderText = "Legajo";
                dgv.Columns["LegajoAlumno"].DisplayIndex = 3;
            }
            if (dgv.Columns.Contains("CorreoAlumno"))
            {
                dgv.Columns["CorreoAlumno"].HeaderText = "Correo";
                dgv.Columns["CorreoAlumno"].DisplayIndex = 4;
            }
            if (dgv.Columns.Contains("TelefonoAlumno"))
            {
                dgv.Columns["TelefonoAlumno"].HeaderText = "Teléfono";
                dgv.Columns["TelefonoAlumno"].DisplayIndex = 5;
            }
            if (dgv.Columns.Contains("Activo"))
            {
                dgv.Columns["Activo"].HeaderText = "Activo";
                dgv.Columns["Activo"].DisplayIndex = 6;
                dgv.Columns["Activo"].ReadOnly = true;
            }
        }

        // Teléfono en 3 cajas (CtrlTelefono): | 54 | área máx 3 | número |.
        // Vacío se permite; a medio completar se exige completar.
        private bool TelefonoValidoSimple(SistemaAsistencia.Vista.Comun.CtrlTelefono ctrl)
        {
            if (!ctrl.EsValido(out string error))
            {
                MessageBox.Show(
                    error,
                    "Alumnos",
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
                    Alumno alumno = new Alumno
                    {
                        NombreAlumno = txtNombre.Text,
                        ApellidoAlumno = txtApellido.Text,
                        DniAlumno = txtDni.Text,
                        LegajoAlumno = txtLegajo.Text,
                        CorreoAlumno = txtCorreo.Text,
                        TelefonoAlumno = ctrlTelCrear.Telefono
                    };

                    if (alumnoController.AgregarAlumno(alumno))
                    {
                        MessageBox.Show(
                            "Alumno agregado correctamente.",
                            "Alumnos",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        CargarAlumnos();
                        LimpiarCreacion();
                    }
                    else
                    {
                        MessageBox.Show(
                            "No se pudo agregar el alumno.",
                            "Alumnos",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "No se pudo agregar el alumno.\n" + ex.Message,
                        "Alumnos",
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
                cacheAlumnos,
                texto,
                a => (a.NombreAlumno != null &&
                        a.NombreAlumno.IndexOf(texto, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (a.ApellidoAlumno != null &&
                        a.ApellidoAlumno.IndexOf(texto, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (a.DniAlumno != null &&
                        a.DniAlumno.IndexOf(texto, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (a.LegajoAlumno != null &&
                        a.LegajoAlumno.IndexOf(texto, StringComparison.OrdinalIgnoreCase) >= 0),
                MostrarEnGrilla,
                CargarEnEdicion,
                LimpiarEdicion,
                "Alumnos",
                interactivo);
        }

        private void dgvAlumnos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            Alumno alumno = dgvAlumnos.Rows[e.RowIndex].DataBoundItem as Alumno;
            if (alumno != null)
                CargarEnEdicion(alumno);
        }

        private void CargarEnEdicion(Alumno alumno)
        {
            EjecutarConLayout(() =>
            {
                idAlumnoSeleccionado = alumno.IdAlumno;
                lblEditando.Text =
                    "Editando: " + alumno.ApellidoAlumno + ", " + alumno.NombreAlumno;
                txtEditNombre.Text = alumno.NombreAlumno;
                txtEditApellido.Text = alumno.ApellidoAlumno;
                txtEditDni.Text = alumno.DniAlumno;
                txtEditLegajo.Text = alumno.LegajoAlumno;
                txtEditCorreo.Text = alumno.CorreoAlumno;
                ctrlTelEdit.Telefono = alumno.TelefonoAlumno;
            });
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (idAlumnoSeleccionado == 0)
            {
                MessageBox.Show(
                    "Busque y seleccione un alumno primero.",
                    "Alumnos",
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
                    Alumno alumno = new Alumno
                    {
                        IdAlumno = idAlumnoSeleccionado,
                        NombreAlumno = txtEditNombre.Text,
                        ApellidoAlumno = txtEditApellido.Text,
                        DniAlumno = txtEditDni.Text,
                        LegajoAlumno = txtEditLegajo.Text,
                        CorreoAlumno = txtEditCorreo.Text,
                        TelefonoAlumno = ctrlTelEdit.Telefono
                    };

                    if (alumnoController.ModificarAlumno(alumno))
                    {
                        MessageBox.Show(
                            "Alumno modificado correctamente.",
                            "Alumnos",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        CargarAlumnos();
                        LimpiarEdicion();
                    }
                    else
                    {
                        MessageBox.Show(
                            "No se pudo modificar el alumno.",
                            "Alumnos",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "No se pudo modificar el alumno.\n" + ex.Message,
                        "Alumnos",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            });
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idAlumnoSeleccionado == 0)
            {
                MessageBox.Show(
                    "Busque y seleccione un alumno primero.",
                    "Alumnos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            Alumno alumno = cacheAlumnos.Find(a => a.IdAlumno == idAlumnoSeleccionado);
            if (alumno == null) return;

            EjecutarConLayout(() =>
            {
                try
                {
                    string descripcion = "Alumno: " + alumno.ApellidoAlumno + ", " +
                        alumno.NombreAlumno + " (Legajo " + alumno.LegajoAlumno + ")";

                    List<Dependencia> dependencias =
                        alumnoController.ObtenerDependencias(idAlumnoSeleccionado);

                    using (var confirmar = new FrmConfirmarEliminar(descripcion, dependencias))
                    {
                        if (confirmar.ShowDialog(this) != DialogResult.OK) return;

                        bool ok;

                        if (confirmar.Resultado == ResultadoEliminacion.Definitiva)
                        {
                            ok = alumnoController.EliminarDefinitivo(idAlumnoSeleccionado);
                            if (ok)
                                MessageBox.Show(
                                    "Alumno eliminado definitivamente.",
                                    "Alumnos",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                        }
                        else
                        {
                            ok = alumnoController.DarDeBaja(idAlumnoSeleccionado);
                            if (ok)
                                MessageBox.Show(
                                    "Alumno dado de baja.",
                                    "Alumnos",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                        }

                        if (ok)
                        {
                            CargarAlumnos();
                            LimpiarEdicion();
                        }
                        else
                        {
                            MessageBox.Show(
                                "No se pudo eliminar.",
                                "Alumnos",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "No se pudo eliminar.\n" + ex.Message,
                        "Alumnos",
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
            idAlumnoSeleccionado = 0;
            lblEditando.Text = "Editando: (seleccione de la lista)";
            txtEditNombre.Clear();
            txtEditApellido.Clear();
            txtEditDni.Clear();
            txtEditLegajo.Clear();
            txtEditCorreo.Clear();
            ctrlTelEdit.Limpiar();
        }
    }
}
