namespace SistemaAsistencia.Vista.Inscripcion
{
    partial class FrmInscripcion
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
            this.panelCampos = new System.Windows.Forms.Panel();
            this.panelBotones = new System.Windows.Forms.Panel();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.cmbDictado = new System.Windows.Forms.ComboBox();
            this.lblDictado = new System.Windows.Forms.Label();
            this.dgvInscripciones = new System.Windows.Forms.DataGridView();
            this.IdAlumno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ApellidoAlumno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NombreAlumno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LegajoAlumno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Inscripto = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInscripciones)).BeginInit();
            this.panelCampos.SuspendLayout();
            this.panelBotones.SuspendLayout();
            this.SuspendLayout();
            //
            // panelCampos
            //
            this.panelCampos.BackColor = System.Drawing.Color.White;
            this.panelCampos.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCampos.Location = new System.Drawing.Point(0, 0);
            this.panelCampos.Name = "panelCampos";
            this.panelCampos.Size = new System.Drawing.Size(503, 50);
            this.panelCampos.TabIndex = 0;
            //
            // panelBotones
            //
            this.panelBotones.BackColor = System.Drawing.Color.White;
            this.panelBotones.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBotones.Location = new System.Drawing.Point(0, 348);
            this.panelBotones.Name = "panelBotones";
            this.panelBotones.Size = new System.Drawing.Size(503, 52);
            this.panelBotones.TabIndex = 2;
            //
            // btnGuardar
            //
            this.btnGuardar.Location = new System.Drawing.Point(93, 11);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(75, 30);
            this.btnGuardar.TabIndex = 58;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            //
            // btnCancelar
            //
            this.btnCancelar.Location = new System.Drawing.Point(12, 11);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 30);
            this.btnCancelar.TabIndex = 57;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            //
            // cmbDictado
            //
            this.cmbDictado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDictado.FormattingEnabled = true;
            this.cmbDictado.Location = new System.Drawing.Point(108, 12);
            this.cmbDictado.Name = "cmbDictado";
            this.cmbDictado.Size = new System.Drawing.Size(380, 21);
            this.cmbDictado.TabIndex = 40;
            this.cmbDictado.SelectedIndexChanged += new System.EventHandler(this.cmbDictado_SelectedIndexChanged);
            //
            // lblDictado
            //
            this.lblDictado.AutoSize = true;
            this.lblDictado.Location = new System.Drawing.Point(12, 15);
            this.lblDictado.Name = "lblDictado";
            this.lblDictado.Size = new System.Drawing.Size(45, 13);
            this.lblDictado.TabIndex = 39;
            this.lblDictado.Text = "Dictado";
            //
            // dgvInscripciones
            //
            this.dgvInscripciones.AllowUserToAddRows = false;
            this.dgvInscripciones.AllowUserToDeleteRows = false;
            this.dgvInscripciones.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvInscripciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInscripciones.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IdAlumno,
            this.ApellidoAlumno,
            this.NombreAlumno,
            this.LegajoAlumno,
            this.Inscripto});
            this.dgvInscripciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvInscripciones.Location = new System.Drawing.Point(12, 50);
            this.dgvInscripciones.Name = "dgvInscripciones";
            this.dgvInscripciones.RowHeadersVisible = false;
            this.dgvInscripciones.Size = new System.Drawing.Size(476, 290);
            this.dgvInscripciones.TabIndex = 1;
            //
            // IdAlumno
            //
            this.IdAlumno.HeaderText = "IdAlumno";
            this.IdAlumno.Name = "IdAlumno";
            this.IdAlumno.ReadOnly = true;
            this.IdAlumno.Visible = false;
            //
            // ApellidoAlumno
            //
            this.ApellidoAlumno.HeaderText = "Apellido";
            this.ApellidoAlumno.Name = "ApellidoAlumno";
            this.ApellidoAlumno.ReadOnly = true;
            this.ApellidoAlumno.Width = 120;
            //
            // NombreAlumno
            //
            this.NombreAlumno.HeaderText = "Nombre";
            this.NombreAlumno.Name = "NombreAlumno";
            this.NombreAlumno.ReadOnly = true;
            this.NombreAlumno.Width = 120;
            //
            // LegajoAlumno
            //
            this.LegajoAlumno.HeaderText = "Legajo";
            this.LegajoAlumno.Name = "LegajoAlumno";
            this.LegajoAlumno.ReadOnly = true;
            this.LegajoAlumno.Width = 100;
            //
            // Inscripto
            //
            this.Inscripto.HeaderText = "Inscripto";
            this.Inscripto.Name = "Inscripto";
            this.Inscripto.Width = 80;
            //
            // FrmInscripcion
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(503, 400);
            this.panelCampos.Controls.Add(this.cmbDictado);
            this.panelCampos.Controls.Add(this.lblDictado);
            this.panelBotones.Controls.Add(this.btnGuardar);
            this.panelBotones.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.dgvInscripciones);
            this.Controls.Add(this.panelBotones);
            this.Controls.Add(this.panelCampos);
            this.Name = "FrmInscripcion";
            this.Text = "FrmInscripcion";
            this.Load += new System.EventHandler(this.FrmInscripcion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInscripciones)).EndInit();
            this.panelCampos.ResumeLayout(false);
            this.panelCampos.PerformLayout();
            this.panelBotones.ResumeLayout(false);
            this.panelBotones.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelCampos;
        private System.Windows.Forms.Panel panelBotones;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.ComboBox cmbDictado;
        private System.Windows.Forms.Label lblDictado;
        private System.Windows.Forms.DataGridView dgvInscripciones;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdAlumno;
        private System.Windows.Forms.DataGridViewTextBoxColumn ApellidoAlumno;
        private System.Windows.Forms.DataGridViewTextBoxColumn NombreAlumno;
        private System.Windows.Forms.DataGridViewTextBoxColumn LegajoAlumno;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Inscripto;
    }
}
