namespace SistemaAsistencia.Vista.PrimerUsuario
{
    partial class FrmPrimerUsuario
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the content of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtPassword = new MaterialSkin.Controls.MaterialTextBox();
            this.txtUsuario = new MaterialSkin.Controls.MaterialTextBox();
            this.txtConfirmarPassword = new MaterialSkin.Controls.MaterialTextBox();
            this.btnCrearAdmin = new MaterialSkin.Controls.MaterialButton();
            this.btnSalir = new MaterialSkin.Controls.MaterialButton();
            this.SuspendLayout();
            //
            // txtUsuario
            //
            this.txtUsuario.Hint = "Usuario";
            this.txtUsuario.Location = new System.Drawing.Point(24, 84);
            this.txtUsuario.MaxLength = 50;
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(352, 48);
            this.txtUsuario.TabIndex = 0;
            this.txtUsuario.Hint = "Usuario";
            //
            // txtPassword
            //
            this.txtPassword.Hint = "Contraseña";
            this.txtPassword.Location = new System.Drawing.Point(24, 144);
            this.txtPassword.MaxLength = 50;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Password = true;
            this.txtPassword.Size = new System.Drawing.Size(352, 48);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.Hint = "Contraseña";
            //
            // txtConfirmarPassword
            //
            this.txtConfirmarPassword.Hint = "Confirmar Contraseña";
            this.txtConfirmarPassword.Location = new System.Drawing.Point(24, 204);
            this.txtConfirmarPassword.MaxLength = 50;
            this.txtConfirmarPassword.Name = "txtConfirmarPassword";
            this.txtConfirmarPassword.Size = new System.Drawing.Size(352, 48);
            this.txtConfirmarPassword.TabIndex = 2;
            this.txtConfirmarPassword.Hint = "Confirmar Contraseña";
            //
            // btnCrearAdmin
            //
            this.btnCrearAdmin.AutoSize = false;
            this.btnCrearAdmin.Location = new System.Drawing.Point(60, 274);
            this.btnCrearAdmin.Name = "btnCrearAdmin";
            this.btnCrearAdmin.Size = new System.Drawing.Size(110, 36);
            this.btnCrearAdmin.TabIndex = 3;
            this.btnCrearAdmin.Text = "Crear";
            this.btnCrearAdmin.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnCrearAdmin.UseAccentColor = false;
            this.btnCrearAdmin.Click += new System.EventHandler(this.btnCrearAdmin_Click);
            //
            // btnSalir
            //
            this.btnSalir.AutoSize = false;
            this.btnSalir.Location = new System.Drawing.Point(230, 274);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(110, 36);
            this.btnSalir.TabIndex = 4;
            this.btnSalir.Text = "Salir";
            this.btnSalir.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Text;
            this.btnSalir.UseAccentColor = false;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            //
            // FrmPrimerUsuario
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 330);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUsuario);
            this.Controls.Add(this.txtConfirmarPassword);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.btnCrearAdmin);
            this.MaximizeBox = false;
            this.Name = "FrmPrimerUsuario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Crear el primer usuario";
            this.ResumeLayout(false);
        }

        #endregion

        private MaterialSkin.Controls.MaterialTextBox txtPassword;
        private MaterialSkin.Controls.MaterialTextBox txtUsuario;
        private MaterialSkin.Controls.MaterialTextBox txtConfirmarPassword;
        private MaterialSkin.Controls.MaterialButton btnCrearAdmin;
        private MaterialSkin.Controls.MaterialButton btnSalir;
    }
}