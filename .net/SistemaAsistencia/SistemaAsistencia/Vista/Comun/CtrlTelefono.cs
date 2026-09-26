using System;
using System.Drawing;
using System.Windows.Forms;
using SistemaAsistencia.Utilidades;

namespace SistemaAsistencia.Vista.Comun
{
    /// <summary>
    /// Teléfono en 3 cajas: | 54 | área (máx 3) | número |.
    /// Reutilizable en Alumnos, Profesores, Preceptores, etc.
    /// La BD sigue recibiendo un solo string vía <see cref="Telefono"/>.
    /// </summary>
    public class CtrlTelefono : UserControl
    {
        private readonly Label lblPais;
        private readonly TextBox txtArea;
        private readonly TextBox txtNumero;

        public CtrlTelefono()
        {
            // Etiqueta en vez de TextBox: no recibe foco ni cursor.
            lblPais = new Label
            {
                Location = new Point(0, 0),
                Size = new Size(32, 20),
                Text = TelefonoHelper.PaisFijo,
                BackColor = SystemColors.Control,
                ForeColor = Color.DimGray,
                BorderStyle = BorderStyle.FixedSingle,
                TextAlign = ContentAlignment.MiddleCenter
            };
            txtArea = new TextBox
            {
                Location = new Point(37, 0),
                Size = new Size(42, 20),
                MaxLength = TelefonoHelper.AreaMax
            };
            txtNumero = new TextBox
            {
                Location = new Point(84, 0),
                Size = new Size(96, 20),
                MaxLength = TelefonoHelper.NumeroMax
            };

            txtArea.KeyPress += SoloNumeros;
            txtNumero.KeyPress += SoloNumeros;

            var tip = new ToolTip();
            tip.SetToolTip(lblPais, "Código país");
            tip.SetToolTip(txtArea, $"Código de área ({TelefonoHelper.AreaMin} a {TelefonoHelper.AreaMax} dígitos)");
            tip.SetToolTip(txtNumero, $"Número ({TelefonoHelper.NumeroMin} a {TelefonoHelper.NumeroMax} dígitos)");

            Controls.Add(lblPais);
            Controls.Add(txtArea);
            Controls.Add(txtNumero);

            Size = new Size(180, 22);
            MinimumSize = Size;
        }

        private static void SoloNumeros(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;
            if (!char.IsDigit(e.KeyChar)) e.Handled = true;
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
