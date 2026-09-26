using System;
using System.Drawing;
using System.Windows.Forms;

namespace SistemaAsistencia.Vista.Configuracion
{
    public class FrmConfiguracion : Form
    {
        private readonly TextBox txtMongo = new TextBox();

        public FrmConfiguracion()
        {
            Text = "Configuración de conexión";
            ClientSize = new Size(520, 130);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;

            var lblMongo = new Label
            {
                Text = "URI de MongoDB (MongoAtlas):",
                Location = new Point(16, 18),
                AutoSize = true
            };
            txtMongo.Location = new Point(16, 40);
            txtMongo.Width = 488;

            var btnGuardar = new Button
            {
                Text = "Guardar",
                Location = new Point(420, 82),
                Size = new Size(84, 30)
            };
            btnGuardar.Click += (s, e) => GuardarYAceptar();

            Controls.Add(lblMongo);
            Controls.Add(txtMongo);
            Controls.Add(btnGuardar);
        }

        private void GuardarYAceptar()
        {
            if (string.IsNullOrWhiteSpace(txtMongo.Text))
            {
                MessageBox.Show("Ingresá el URI de MongoDB.",
                    "Configuración",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            SistemaAsistencia.Utilidades.Configuracion.GuardarCadena(
                "MongoAtlas", txtMongo.Text.Trim());

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}