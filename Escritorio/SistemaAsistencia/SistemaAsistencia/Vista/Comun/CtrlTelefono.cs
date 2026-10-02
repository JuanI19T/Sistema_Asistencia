using System;
using System.Drawing;
using System.Windows.Forms;
using MaterialSkin.Controls;
using SistemaAsistencia.Utilidades;

namespace SistemaAsistencia.Vista.Comun
{
    /// <summary>
    /// Teléfono en 3 cajas Material: | 54 | área (máx 3) | número |.
    /// Reutilizable en Alumnos, Profesores, Preceptores, etc.
    /// La BD sigue recibiendo un solo string vía <see cref="Telefono"/>.
    /// API pública intacta: Telefono, EstaVacio, EsValido, Limpiar, Enfocar.
    /// </summary>
    public class CtrlTelefono : UserControl
    {
        private readonly MaterialLabel lblPais;
        private readonly MaterialTextBox2 txtArea;
        private readonly MaterialTextBox2 txtNumero;

        public CtrlTelefono()
        {
            var tlp = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3,
                RowCount = 1
            };
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 44F));
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32F));
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 68F));
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            // Etiqueta en vez de caja: no recibe foco ni cursor.
            lblPais = new MaterialLabel
            {
                Text = TelefonoHelper.PaisFijo,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };

            txtArea = new MaterialTextBox2
            {
                Hint = "Área",
                MaxLength = TelefonoHelper.AreaMax,
                Dock = DockStyle.Fill,
                TabIndex = 0
            };

            txtNumero = new MaterialTextBox2
            {
                Hint = "Número",
                MaxLength = TelefonoHelper.NumeroMax,
                Dock = DockStyle.Fill,
                TabIndex = 1
            };

            // MaterialTextBox2 no expone su TextBox interno: el filtro
            // solo-números se aplica saneando en TextChanged.
            txtArea.TextChanged += SoloNumeros;
            txtNumero.TextChanged += SoloNumeros;

            var tip = new ToolTip();
            tip.SetToolTip(lblPais, "Código país");
            tip.SetToolTip(txtArea, $"Código de área ({TelefonoHelper.AreaMin} a {TelefonoHelper.AreaMax} dígitos)");
            tip.SetToolTip(txtNumero, $"Número ({TelefonoHelper.NumeroMin} a {TelefonoHelper.NumeroMax} dígitos)");

            tlp.Controls.Add(lblPais, 0, 0);
            tlp.Controls.Add(txtArea, 1, 0);
            tlp.Controls.Add(txtNumero, 2, 0);
            Controls.Add(tlp);

            Size = new Size(300, 48);
            MinimumSize = new Size(220, 48);
        }

        private static void SoloNumeros(object sender, EventArgs e)
        {
            var box = sender as MaterialTextBox2;
            if (box == null) return;

            string texto = box.Text ?? string.Empty;
            bool limpio = true;
            foreach (char c in texto)
            {
                if (!char.IsDigit(c))
                {
                    limpio = false;
                    break;
                }
            }
            if (limpio) return;

            var digitos = new System.Text.StringBuilder(texto.Length);
            foreach (char c in texto)
            {
                if (char.IsDigit(c)) digitos.Append(c);
            }
            box.Text = digitos.ToString();
            try { box.SelectionStart = box.Text.Length; }
            catch { /* Nunca romper la edición por el caret. */ }
        }

        /// <summary>
        /// Formato guardado "54 AAA NNNNNN" o vacío.
        /// </summary>
        public string Telefono
        {
            get { return TelefonoHelper.Unir(lblPais.Text, txtArea.Text, txtNumero.Text); }
            set
            {
                TelefonoHelper.Separar(value, out string pais, out string area, out string numero);
                lblPais.Text = string.IsNullOrEmpty(pais) ? TelefonoHelper.PaisFijo : pais;
                txtArea.Text = area;
                txtNumero.Text = numero;
            }
        }

        public bool EstaVacio
        {
            get { return Telefono.Length == 0; }
        }

        public bool EsValido(out string error)
        {
            return TelefonoHelper.EsValido(txtArea.Text, txtNumero.Text, out error);
        }

        public void Limpiar()
        {
            lblPais.Text = TelefonoHelper.PaisFijo;
            txtArea.Clear();
            txtNumero.Clear();
        }

        public void Enfocar()
        {
            txtArea.Focus();
        }
    }
}
