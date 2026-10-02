using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
using Microsoft.Win32;
using SistemaAsistencia.Utilidades;
using SistemaAsistencia.Vista.Alumnos;
using SistemaAsistencia.Vista.Dictados;
using SistemaAsistencia.Vista.Especialidades;
using SistemaAsistencia.Vista.Inscripcion;
using SistemaAsistencia.Vista.Login;
using SistemaAsistencia.Vista.Materias;
using SistemaAsistencia.Vista.Preceptores;
using SistemaAsistencia.Vista.Profesores;
using SistemaAsistencia.Vista.Usuarios;

namespace SistemaAsistencia.Vista.Principal
{
    public partial class FrmPrincipal : MaterialForm
    {
        private Form formActivo;
        private Font fuenteMenuNormal;
        private readonly Dictionary<string, OpcionMenu> opciones =
            new Dictionary<string, OpcionMenu>();

        private class OpcionMenu
        {
            public Panel Fila;
            public Panel Indicador;
            public MaterialButton Boton;
            public Func<Form> Fabrica;
            public Color Acento;
        }
        // True solo cuando Cerramos por "Cerrar sesión" para NO matar
        // el proceso en FormClosing (vamos a abrir otro login).
        private bool navegando = false;

        public FrmPrincipal()
        {
            InitializeComponent();

            MaterialSkinManager skin = MaterialSkinManager.Instance;
            skin.AddFormToManage(this);
            skin.Theme = MaterialSkinManager.Themes.LIGHT;
            skin.ColorScheme = new ColorScheme(
                Primary.Indigo600,
                Primary.Indigo700,
                Primary.Indigo200,
                Accent.LightBlue200,
                TextShade.WHITE);

            CrearNavegacion();
            CargarDatosUsuario();
            AplicarPermisos();

            // La cruz / Alt+F4 deben cerrar la app sin pasar por el cierre del
            // FrmLogin oculto (sus MaterialTextBox se cuelgan en EM_STREAMOUT).
            this.FormClosing += FrmPrincipal_FormClosing;

            // Multi-monitor (ej. HDMI a la TV): MaterialForm dibuja su propia
            // barra y emula el maximizado, lo que falla con pantallas
            // extendidas de distinta resolución. En vez de depender del
            // maximizado, la ventana se dimensiona a la pantalla donde está
            // el cursor y, si cambia la configuración, se reubica.
            this.Shown += (s, e) => AjusteInicialPantalla();
            this.Shown += (s, e) => AsegurarVisibleEnPantalla();
            SystemEvents.DisplaySettingsChanged += AlCambiarPantallas;
            this.FormClosed += (s, e) =>
                SystemEvents.DisplaySettingsChanged -= AlCambiarPantallas;
        }

        private void AlCambiarPantallas(object sender, EventArgs e)
        {
            if (IsDisposed) return;
            AsegurarVisibleEnPantalla();
        }

        private void AjusteInicialPantalla()
        {
            try
            {
                // Pantalla completa "manual": ocupar el área de trabajo del
                // monitor donde está el cursor. Se evita WindowState.Maximized
                // porque el maximizado emulado del MaterialForm calcula mal
                // la geometría con escritorio extendido (TV con otra
                // resolución) y corta la parte de abajo.
                Rectangle area = Screen.FromPoint(Cursor.Position).WorkingArea;

                WindowState = FormWindowState.Normal;
                Bounds = area;
            }
            catch
            {
                // Nunca romper el arranque por un ajuste de posición.
            }
        }

        private void AsegurarVisibleEnPantalla()
        {
            try
            {
                Screen pantalla = Screen.FromControl(this);
                Rectangle area = pantalla.WorkingArea;

                // Si está maximizada, Windows ya la ajusta al monitor:
                // solo verificar que ese monitor siga existiendo.
                if (WindowState == FormWindowState.Maximized)
                {
                    if (!pantalla.Bounds.IntersectsWith(this.Bounds))
                    {
                        WindowState = FormWindowState.Normal;
                        Location = area.Location;
                        WindowState = FormWindowState.Maximized;
                    }
                    return;
                }

                if (Width > area.Width) Width = area.Width;
                if (Height > area.Height) Height = area.Height;

                int x = Math.Max(area.Left, Math.Min(Left, area.Right - Width));
                int y = Math.Max(area.Top, Math.Min(Top, area.Bottom - Height));
                Location = new Point(x, y);
            }
            catch
            {
                // Nunca romper el arranque por un ajuste de posición.
            }
        }

        private void FrmPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Si es por "Cerrar sesión" no matar el proceso:
            // se va a mostrar el login de nuevo.
            if (navegando) return;
            Environment.Exit(0);
        }

        // ---------------- Navegación lateral ----------------

        private void CrearNavegacion()
        {
            // ---- Marca superior (estilo referencia: logo + nombre + versión)
            var marca = new Panel
            {
                Dock = DockStyle.Top,
                Height = 72,
                BackColor = Color.White
            };
            panelLateral.Controls.Add(marca);

            var logo = new Panel
            {
                Location = new Point(14, 16),
                Size = new Size(38, 38),
                BackColor = Color.FromArgb(63, 81, 181)
            };
            var lblLogo = new Label
            {
                Dock = DockStyle.Fill,
                Text = "A",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 17, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };
            logo.Controls.Add(lblLogo);
            marca.Controls.Add(logo);

            var lblMarca = new Label
            {
                Location = new Point(60, 16),
                AutoSize = true,
                Text = "Asistencia",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(33, 33, 33)
            };
            marca.Controls.Add(lblMarca);

            var lblVersion = new Label
            {
                Location = new Point(60, 40),
                AutoSize = true,
                Text = "ESCUELA • v1.0",
                Font = new Font("Segoe UI", 8, FontStyle.Regular),
                ForeColor = Color.Gray
            };
            marca.Controls.Add(lblVersion);

            var lblMenu = new Label
            {
                Location = new Point(14, 80),
                AutoSize = true,
                Text = "MENÚ",
                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                ForeColor = Color.Gray
            };
            panelLateral.Controls.Add(lblMenu);
            lblMenu.BringToFront();

            // ---- Opciones (cada una con su color de acento) ----
            AgregarOpcion("Inicio", () => new FrmInicio(), Color.FromArgb(96, 125, 139));
            AgregarOpcion("Usuarios", () => new FrmUsuarios(), Color.FromArgb(63, 81, 181));
            AgregarOpcion("Alumnos", () => new FrmAlumnos(), Color.FromArgb(0, 150, 136));
            AgregarOpcion("Profesores", () => new FrmProfesores(), Color.FromArgb(156, 39, 176));
            AgregarOpcion("Preceptores", () => new FrmPreceptores(), Color.FromArgb(0, 172, 193));
            AgregarOpcion("Materias", () => new FrmMaterias(), Color.FromArgb(245, 124, 0));
            AgregarOpcion("Especialidades", () => new FrmEspecialidades(), Color.FromArgb(233, 30, 99));
            AgregarOpcion("Dictados", () => new FrmDictados(), Color.FromArgb(67, 160, 71));
            AgregarOpcion("Inscripciones", () => new FrmInscripcion(), Color.FromArgb(121, 85, 72));

            // ---- Panel inferior con Dock: Cerrar/Salir siempre visibles ----
            var panelInferior = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 100,
                BackColor = Color.White
            };
            panelLateral.Controls.Add(panelInferior);

            var btnSalir = new MaterialButton
            {
                Text = "Salir",
                AutoSize = false,
                Size = new Size(204, 40),
                Dock = DockStyle.Bottom,
                TextAlign = ContentAlignment.MiddleLeft,
                Type = MaterialButton.MaterialButtonType.Outlined,
                HighEmphasis = false,
                UseAccentColor = false
            };
            btnSalir.Click += (sender, e) => Salir();
            panelInferior.Controls.Add(btnSalir);

            var btnCerrar = new MaterialButton
            {
                Text = "Cerrar sesión",
                AutoSize = false,
                Size = new Size(204, 40),
                Dock = DockStyle.Bottom,
                TextAlign = ContentAlignment.MiddleLeft,
                Type = MaterialButton.MaterialButtonType.Outlined,
                HighEmphasis = false,
                UseAccentColor = false
            };
            btnCerrar.Click += (sender, e) => CerrarSesion();
            panelInferior.Controls.Add(btnCerrar);
            btnCerrar.BringToFront();

            // Pantalla de inicio por defecto: no toca la BD ni depende
            // del rol, así el arranque es instantáneo para todos.
            this.Shown += (s, e) => MostrarForm("Inicio");
        }

        private void AgregarOpcion(string titulo, Func<Form> fabrica, Color acento)
        {
            // Fila con indicador de color a la izquierda: el ítem activo
            // "se conecta" con el color del form (cinta superior).
            var fila = new Panel
            {
                Size = new Size(204, 40),
                Location = new Point(8, 102 + opciones.Count * 45),
                BackColor = Color.White
            };

            var indicador = new Panel
            {
                Dock = DockStyle.Left,
                Width = 5,
                BackColor = acento,
                Visible = false
            };
            fila.Controls.Add(indicador);

            var boton = new MaterialButton
            {
                Text = titulo,
                AutoSize = false,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Type = MaterialButton.MaterialButtonType.Text,
                HighEmphasis = false,
                UseAccentColor = false
            };
            boton.Click += (sender, e) => MostrarForm(titulo);
            fila.Controls.Add(boton);

            if (fuenteMenuNormal == null)
                fuenteMenuNormal = boton.Font;

            opciones[titulo] = new OpcionMenu
            {
                Fila = fila,
                Indicador = indicador,
                Boton = boton,
                Fabrica = fabrica,
                Acento = acento
            };
            panelLateral.Controls.Add(fila);
            fila.BringToFront();
        }

        private void MostrarForm(string titulo)
        {
            if (!opciones.TryGetValue(titulo, out OpcionMenu op))
                return;

            // Defensa extra: nunca abrir una sección oculta por permisos
            // (ej. Usuarios para un rol no administrador).
            if (!op.Fila.Visible)
            {
                MessageBox.Show(
                    "No tiene permiso para acceder a esta sección.",
                    "Acceso denegado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            Form form;
            try
            {
                form = op.Fabrica();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"No se pudo abrir {titulo}.\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            try
            {
                if (formActivo != null)
                {
                    formActivo.Close();
                    formActivo.Dispose();
                    formActivo = null;
                }

                // Ítem activo: indicador del color de la sección + negrita.
                // La cinta superior del contenido usa ese mismo color:
                // así el menú "se conecta" con el form.
                foreach (var kv in opciones)
                {
                    bool activa = kv.Key == titulo;
                    kv.Value.Indicador.Visible = activa;
                    kv.Value.Boton.HighEmphasis = activa;
                    kv.Value.Boton.Font = activa && fuenteMenuNormal != null
                        ? new Font(fuenteMenuNormal, FontStyle.Bold)
                        : fuenteMenuNormal;
                }

                // Cinta superior del color de la sección.
                var cinta = new Panel
                {
                    Dock = DockStyle.Top,
                    Height = 54,
                    BackColor = op.Acento
                };
                var lblCinta = new Label
                {
                    Dock = DockStyle.Fill,
                    Text = titulo.ToUpper(),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 15, FontStyle.Bold),
                    TextAlign = ContentAlignment.MiddleLeft,
                    Padding = new Padding(16, 0, 0, 0)
                };
                cinta.Controls.Add(lblCinta);

                form.TopLevel = false;
                form.FormBorderStyle = FormBorderStyle.None;
                form.Dock = DockStyle.Fill;
                form.Visible = true;
                formActivo = form;

                panelContenido.SuspendLayout();
                panelContenido.Controls.Clear();
                panelContenido.Controls.Add(form);
                panelContenido.Controls.Add(cinta);
                panelContenido.ResumeLayout(true);

                form.Show();
                form.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"No se pudo mostrar {titulo}.\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // ---------------- Información del usuario ----------------

        private void CargarDatosUsuario()
        {
            if (Sesion.UsuarioActual != null)
            {
                lblUsuario.Text = $"Usuario: {Sesion.UsuarioActual.NombreUsuario}";
                lblRol.Text = $"Rol: {Sesion.UsuarioActual.Rol}";
            }
        }

        private void AplicarPermisos()
        {
            bool esAdmin = Sesion.UsuarioActual != null &&
                Sesion.UsuarioActual.Rol.Trim()
                    .Equals("Administrador", StringComparison.OrdinalIgnoreCase);

            opciones["Usuarios"].Fila.Visible = esAdmin;
        }

        // ---------------- Sesión ----------------

        private void CerrarSesion()
        {
            DialogResult resultado = MaterialMessageBox.Show(
                this,
                "¿Desea cerrar la sesión actual?",
                "Cerrar sesión",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2,
                false,
                FlexibleMaterialForm.ButtonsPosition.Center);

            if (resultado == DialogResult.Yes)
            {
                Sesion.CerrarSesion();

                navegando = true;
                Hide();

                FrmLogin login = new FrmLogin();
                login.ShowDialog();

                Close();
            }
        }

        private void Salir()
        {
            DialogResult resp = MaterialMessageBox.Show(
                this,
                "Cerrar sistema, ¿confirma?",
                "Sistema Asistencia",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2,
                false,
                FlexibleMaterialForm.ButtonsPosition.Center);

            if (resp == DialogResult.Yes)
            {
                // Environment.Exit evita el deadlock de Application.Exit()
                // al destruir el FrmLogin oculto (sus MaterialTextBox son
                // RichTextBox y se cuelgan en EM_STREAMOUT).
                // Seguro acá: al salir no hay operaciones de BD en curso
                // (todo el acceso a datos usa using y ya terminó).
                Environment.Exit(0);
            }
        }
    }
}