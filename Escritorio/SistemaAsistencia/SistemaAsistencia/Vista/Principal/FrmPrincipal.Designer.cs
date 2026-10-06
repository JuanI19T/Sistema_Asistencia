namespace SistemaAsistencia.Vista.Principal
{
    partial class FrmPrincipal
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.panelLateral = new System.Windows.Forms.Panel();
            this.panelContenido = new System.Windows.Forms.Panel();
            this.panelEstado = new System.Windows.Forms.Panel();
            this.lblUsuario = new MaterialSkin.Controls.MaterialLabel();
            this.lblRol = new MaterialSkin.Controls.MaterialLabel();
            this.SuspendLayout();
            //
            // panelLateral
            //
            this.panelLateral.BackColor = System.Drawing.Color.White;
            this.panelLateral.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelLateral.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLateral.Location = new System.Drawing.Point(0, 0);
            this.panelLateral.Name = "panelLateral";
            this.panelLateral.Size = new System.Drawing.Size(220, 560);
            this.panelLateral.TabIndex = 0;
            //
            // panelContenido
            //
            this.panelContenido.AutoScroll = true;
            this.panelContenido.BackColor = System.Drawing.Color.White;
            this.panelContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenido.Location = new System.Drawing.Point(220, 0);
            this.panelContenido.Name = "panelContenido";
            this.panelContenido.Size = new System.Drawing.Size(680, 528);
            this.panelContenido.TabIndex = 1;
            //
            // panelEstado
            //
            this.panelEstado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelEstado.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelEstado.Location = new System.Drawing.Point(0, 528);
            this.panelEstado.Name = "panelEstado";
            this.panelEstado.Size = new System.Drawing.Size(900, 32);
            this.panelEstado.TabIndex = 2;
            //
            // lblUsuario
            //
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Location = new System.Drawing.Point(12, 6);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(65, 19);
            this.lblUsuario.TabIndex = 0;
            this.lblUsuario.Text = "Usuario";
            //
            // lblRol
            //
            this.lblRol.AutoSize = true;
            this.lblRol.Location = new System.Drawing.Point(260, 6);
            this.lblRol.Name = "lblRol";
            this.lblRol.Size = new System.Drawing.Size(29, 19);
            this.lblRol.TabIndex = 1;
            this.lblRol.Text = "Rol";
            //
            // FrmPrincipal
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 560);
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            // Mínimo calibrado para monitores chicos (ej. 1366x768@100%):
            // la ventana entra cómoda y panelContenido conserva aire.
            // El scroll de seguridad lo aporta FrmBaseHijo.AutoScroll.
            this.MinimumSize = new System.Drawing.Size(960, 600);
            this.panelEstado.Controls.Add(this.lblRol);
            this.panelEstado.Controls.Add(this.lblUsuario);
            this.Controls.Add(this.panelContenido);
            this.Controls.Add(this.panelEstado);
            this.Controls.Add(this.panelLateral);
            this.Name = "FrmPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sistema de Asistencia Escolar";
            this.panelEstado.ResumeLayout(false);
            this.panelEstado.PerformLayout();
            this.ResumeLayout(false);
        }
        #endregion

        private System.Windows.Forms.Panel panelLateral;
        private System.Windows.Forms.Panel panelContenido;
        private System.Windows.Forms.Panel panelEstado;
        private MaterialSkin.Controls.MaterialLabel lblUsuario;
        private MaterialSkin.Controls.MaterialLabel lblRol;
    }
}