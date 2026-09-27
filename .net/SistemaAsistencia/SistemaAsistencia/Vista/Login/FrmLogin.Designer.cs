namespace SistemaAsistencia.Vista.Login
{
    partial class FrmLogin
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
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtPassword = new MaterialSkin.Controls.MaterialTextBox();
            this.txtUsuario = new MaterialSkin.Controls.MaterialTextBox();
            this.btnSalir = new MaterialSkin.Controls.MaterialButton();
            this.btnLogin = new MaterialSkin.Controls.MaterialButton();
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
            this.txtUsuario.TextChanged += new System.EventHandler(this.txtUsuario_TextChanged);
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
            //
            // btnLogin
            //
            this.btnLogin.AutoSize = false;
            this.btnLogin.Location = new System.Drawing.Point(60, 214);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(110, 36);
            this.btnLogin.TabIndex = 2;
            this.btnLogin.Text = "Login";
            this.btnLogin.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnLogin.UseAccentColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            //
            // btnSalir
            //
            this.btnSalir.AutoSize = false;
            this.btnSalir.Location = new System.Drawing.Point(230, 214);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(110, 36);
            this.btnSalir.TabIndex = 4;
            this.btnSalir.Text = "Salir";
            this.btnSalir.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Text;
            this.btnSalir.UseAccentColor = false;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            //
            // FrmLogin
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 270);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUsuario);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.btnLogin);
            this.MaximizeBox = false;
            this.Name = "FrmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sistema de Asistencia Escolar";
            this.ResumeLayout(false);
        }

        #endregion

        private MaterialSkin.Controls.MaterialTextBox txtPassword;
        private MaterialSkin.Controls.MaterialTextBox txtUsuario;
        private MaterialSkin.Controls.MaterialButton btnSalir;
        private MaterialSkin.Controls.MaterialButton btnLogin;
    }
}