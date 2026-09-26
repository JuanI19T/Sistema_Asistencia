using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;

namespace SistemaAsistencia.Utilidades
{
    /// <summary>
    /// Estilos generales en un solo lugar.
    /// No toca BD, no pide secrets, no agrega NuGets.
    /// </summary>
    public static class Tema
    {
        // ---- Paleta (misma que ya usás en FrmPrincipal) ----
        public static readonly Color Primario = Color.FromArgb(63, 81, 181);      // Indigo600
        public static readonly Color PrimarioOscuro = Color.FromArgb(48, 63, 159); // Indigo700
        public static readonly Color FondoClaro = Color.FromArgb(245, 246, 250);
        public static readonly Color Texto = Color.FromArgb(33, 33, 33);
        public static readonly Color Gris = Color.FromArgb(117, 117, 117);
        public static readonly Color FilaAlterna = Color.FromArgb(245, 247, 250);
        public static readonly Color Seleccion = Color.FromArgb(197, 202, 233); // Indigo100

        public const string Fuente = "Segoe UI";

        // ---- Tema MaterialSkin (reemplaza las 6 líneas duplicadas) ----
        // Hoy lo tenés copiado en FrmPrincipal.cs:44-52 y FrmLogin.cs:24-27
        public static void AplicarTema(MaterialForm form)
        {
            MaterialSkinManager skin = MaterialSkinManager.Instance;
            skin.AddFormToManage(form);
            skin.Theme = MaterialSkinManager.Themes.LIGHT;
            skin.ColorScheme = new ColorScheme(
                Primary.Indigo600,
                Primary.Indigo700,
                Primary.Indigo200,
                Accent.LightBlue200,
                TextShade.WHITE);
        }

        // ---- Fondo base para Forms clásicos (ABM) ----
        public static void ConfigurarFondo(Form form)
        {
            if (form == null || form.IsDisposed) return;
            form.BackColor = Color.White;
            form.Font = new Font(Fuente, 9F, FontStyle.Regular, GraphicsUnit.Point);
        }

        // ---- Grillas: el cambio visual más grande con menos riesgo ----
        public static void EstilizarGrilla(DataGridView dgv)
        {
            if (dgv == null || dgv.IsDisposed) return;

            dgv.EnableHeadersVisualStyles = false;
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.RowHeadersVisible = false;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.ReadOnly = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgv.ScrollBars = ScrollBars.Both;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv.BackgroundColor = Color.White;

            // Cabecera
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Primario;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font(Fuente, 9F, FontStyle.Bold, GraphicsUnit.Point);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.ColumnHeadersHeight = 32;

            // Filas
            dgv.DefaultCellStyle.BackColor = Color.White;
            dgv.DefaultCellStyle.ForeColor = Texto;
            dgv.DefaultCellStyle.Font = new Font(Fuente, 9F, FontStyle.Regular, GraphicsUnit.Point);
            dgv.DefaultCellStyle.SelectionBackColor = Seleccion;
            dgv.DefaultCellStyle.SelectionForeColor = Texto;

            dgv.AlternatingRowsDefaultCellStyle.BackColor = FilaAlterna;
            dgv.AlternatingRowsDefaultCellStyle.ForeColor = Texto;
            dgv.AlternatingRowsDefaultCellStyle.SelectionBackColor = Seleccion;
            dgv.AlternatingRowsDefaultCellStyle.SelectionForeColor = Texto;

            dgv.RowTemplate.Height = 28;
        }

        // ---- Botones clásicos (para ABM sin reescribir Designer) ----
        public static void EstilizarBoton(Button btn, bool primario)
        {
            if (btn == null || btn.IsDisposed) return;
            btn.FlatStyle = FlatStyle.Flat;
            btn.Font = new Font(Fuente, 9F, FontStyle.Bold, GraphicsUnit.Point);
            btn.Cursor = Cursors.Hand;
            if (primario)
            {
                btn.BackColor = Primario;
                btn.ForeColor = Color.White;
                btn.FlatAppearance.BorderSize = 0;
            }
            else
            {
                btn.BackColor = Color.White;
                btn.ForeColor = Primario;
                btn.FlatAppearance.BorderColor = Primario;
                btn.FlatAppearance.BorderSize = 1;
            }
        }

        // ---- Versión (para sacar el literal "v1.0" de FrmPrincipal.cs:185) ----
        public static string ObtenerVersionCorta()
        {
            try
            {
                var v = Assembly.GetExecutingAssembly().GetName().Version;
                if (v == null) return "v1.0";
                return $"v{v.Major}.{v.Minor}";
            }
            catch
            {
                return "v1.0";
            }
        }
    }
}