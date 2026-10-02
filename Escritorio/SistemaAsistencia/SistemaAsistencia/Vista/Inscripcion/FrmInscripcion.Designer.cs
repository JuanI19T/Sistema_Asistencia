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
            this.panelFiltros = new System.Windows.Forms.Panel();
            this.lblEspecialidad = new MaterialSkin.Controls.MaterialLabel();
            this.cmbEspecialidad = new MaterialSkin.Controls.MaterialComboBox();
            this.lblAnio = new MaterialSkin.Controls.MaterialLabel();
            this.nudAnio = new System.Windows.Forms.NumericUpDown();
            this.lblDivision = new MaterialSkin.Controls.MaterialLabel();
            this.nudDivision = new System.Windows.Forms.NumericUpDown();
            this.lblGrupo = new MaterialSkin.Controls.MaterialLabel();
            this.nudGrupo = new System.Windows.Forms.NumericUpDown();
            this.lblDictado = new MaterialSkin.Controls.MaterialLabel();
            this.cmbDictado = new MaterialSkin.Controls.MaterialComboBox();
            this.panelBotones = new System.Windows.Forms.Panel();
            this.btnGuardar = new MaterialSkin.Controls.MaterialButton();
            this.btnCancelar = new MaterialSkin.Controls.MaterialButton();
            this.dgvInscripciones = new System.Windows.Forms.DataGridView();
            this.IdAlumno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ApellidoAlumno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NombreAlumno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LegajoAlumno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Inscripto = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.nudAnio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDivision)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudGrupo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInscripciones)).BeginInit();
            this.panelFiltros.SuspendLayout();
            this.panelBotones.SuspendLayout();
            this.SuspendLayout();
            //
            // panelFiltros
            //
            this.panelFiltros.BackColor = System.Drawing.Color.White;
            this.panelFiltros.Controls.Add(this.lblEspecialidad);
            this.panelFiltros.Controls.Add(this.cmbEspecialidad);
            this.panelFiltros.Controls.Add(this.lblAnio);
            this.panelFiltros.Controls.Add(this.nudAnio);
            this.panelFiltros.Controls.Add(this.lblDivision);
            this.panelFiltros.Controls.Add(this.nudDivision);
            this.panelFiltros.Controls.Add(this.lblGrupo);
            this.panelFiltros.Controls.Add(this.nudGrupo);
            this.panelFiltros.Controls.Add(this.lblDictado);
            this.panelFiltros.Controls.Add(this.cmbDictado);
            this.panelFiltros.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFiltros.Location = new System.Drawing.Point(0, 0);
            this.panelFiltros.Name = "panelFiltros";
            this.panelFiltros.Size = new System.Drawing.Size(860, 140);
            this.panelFiltros.TabIndex = 0;
            //
            // lblEspecialidad
            //
            this.lblEspecialidad.AutoSize = true;
            this.lblEspecialidad.BackColor = System.Drawing.Color.White;
            this.lblEspecialidad.Depth = 0;
            this.lblEspecialidad.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblEspecialidad.Location = new System.Drawing.Point(16, 28);
            this.lblEspecialidad.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblEspecialidad.Name = "lblEspecialidad";
            this.lblEspecialidad.Size = new System.Drawing.Size(101, 19);
            this.lblEspecialidad.TabIndex = 0;
            this.lblEspecialidad.Text = "Especialidad";
            //
            // cmbEspecialidad
            //
            this.cmbEspecialidad.AutoResize = false;
            this.cmbEspecialidad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbEspecialidad.Depth = 0;
            this.cmbEspecialidad.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbEspecialidad.DropDownHeight = 174;
            this.cmbEspecialidad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEspecialidad.DropDownWidth = 121;
            this.cmbEspecialidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.cmbEspecialidad.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cmbEspecialidad.FormattingEnabled = true;
            this.cmbEspecialidad.Hint = "Especialidad";
            this.cmbEspecialidad.IntegralHeight = false;
            this.cmbEspecialidad.ItemHeight = 43;
            this.cmbEspecialidad.Location = new System.Drawing.Point(130, 12);
            this.cmbEspecialidad.MaxDropDownItems = 4;
            this.cmbEspecialidad.MouseState = MaterialSkin.MouseState.OUT;
            this.cmbEspecialidad.Name = "cmbEspecialidad";
            this.cmbEspecialidad.Size = new System.Drawing.Size(240, 49);
            this.cmbEspecialidad.StartIndex = -1;
            this.cmbEspecialidad.TabIndex = 0;
            this.cmbEspecialidad.UseAccent = false;
            //
            // lblAnio
            //
            this.lblAnio.AutoSize = true;
            this.lblAnio.BackColor = System.Drawing.Color.White;
            this.lblAnio.Depth = 0;
            this.lblAnio.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblAnio.Location = new System.Drawing.Point(390, 28);
            this.lblAnio.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblAnio.Name = "lblAnio";
            this.lblAnio.Size = new System.Drawing.Size(32, 19);
            this.lblAnio.TabIndex = 1;
            this.lblAnio.Text = "Año";
            //
            // nudAnio
            //
            this.nudAnio.Location = new System.Drawing.Point(435, 25);
            this.nudAnio.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.nudAnio.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudAnio.Name = "nudAnio";
            this.nudAnio.Size = new System.Drawing.Size(70, 20);
            this.nudAnio.TabIndex = 1;
            this.nudAnio.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            //
            // lblDivision
            //
            this.lblDivision.AutoSize = true;
            this.lblDivision.BackColor = System.Drawing.Color.White;
            this.lblDivision.Depth = 0;
            this.lblDivision.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblDivision.Location = new System.Drawing.Point(525, 28);
            this.lblDivision.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblDivision.Name = "lblDivision";
            this.lblDivision.Size = new System.Drawing.Size(62, 19);
            this.lblDivision.TabIndex = 2;
            this.lblDivision.Text = "División";
            //
            // nudDivision
            //
            this.nudDivision.Location = new System.Drawing.Point(600, 25);
            this.nudDivision.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.nudDivision.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudDivision.Name = "nudDivision";
            this.nudDivision.Size = new System.Drawing.Size(70, 20);
            this.nudDivision.TabIndex = 2;
            this.nudDivision.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            //
            // lblGrupo
            //
            this.lblGrupo.AutoSize = true;
            this.lblGrupo.BackColor = System.Drawing.Color.White;
            this.lblGrupo.Depth = 0;
            this.lblGrupo.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblGrupo.Location = new System.Drawing.Point(690, 28);
            this.lblGrupo.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblGrupo.Name = "lblGrupo";
            this.lblGrupo.Size = new System.Drawing.Size(110, 19);
            this.lblGrupo.TabIndex = 3;
            this.lblGrupo.Text = "Grupo (0=ambos)";
            //
            // nudGrupo
            //
            this.nudGrupo.Location = new System.Drawing.Point(745, 25);
            this.nudGrupo.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudGrupo.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudGrupo.Name = "nudGrupo";
            this.nudGrupo.Size = new System.Drawing.Size(70, 20);
            this.nudGrupo.TabIndex = 3;
            this.nudGrupo.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            //
            // lblDictado
            //
            this.lblDictado.AutoSize = true;
            this.lblDictado.BackColor = System.Drawing.Color.White;
            this.lblDictado.Depth = 0;
            this.lblDictado.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblDictado.Location = new System.Drawing.Point(16, 98);
            this.lblDictado.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblDictado.Name = "lblDictado";
            this.lblDictado.Size = new System.Drawing.Size(60, 19);
            this.lblDictado.TabIndex = 4;
            this.lblDictado.Text = "Dictado";
            //
            // cmbDictado
            //
            this.cmbDictado.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbDictado.AutoResize = false;
            this.cmbDictado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbDictado.Depth = 0;
            this.cmbDictado.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbDictado.DropDownHeight = 174;
            this.cmbDictado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDictado.DropDownWidth = 121;
            this.cmbDictado.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.cmbDictado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cmbDictado.FormattingEnabled = true;
            this.cmbDictado.Hint = "Dictado (elegí especialidad, año, división y grupo)";
            this.cmbDictado.IntegralHeight = false;
            this.cmbDictado.ItemHeight = 43;
            this.cmbDictado.Location = new System.Drawing.Point(130, 82);
            this.cmbDictado.MaxDropDownItems = 4;
            this.cmbDictado.MouseState = MaterialSkin.MouseState.OUT;
            this.cmbDictado.Name = "cmbDictado";
            this.cmbDictado.Size = new System.Drawing.Size(700, 49);
            this.cmbDictado.StartIndex = -1;
            this.cmbDictado.TabIndex = 4;
            this.cmbDictado.UseAccent = false;
            this.cmbDictado.SelectedIndexChanged += new System.EventHandler(this.cmbDictado_SelectedIndexChanged);
            //
            // panelBotones
            //
            this.panelBotones.BackColor = System.Drawing.Color.White;
            this.panelBotones.Controls.Add(this.btnGuardar);
            this.panelBotones.Controls.Add(this.btnCancelar);
            this.panelBotones.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBotones.Location = new System.Drawing.Point(0, 548);
            this.panelBotones.Name = "panelBotones";
            this.panelBotones.Size = new System.Drawing.Size(860, 52);
            this.panelBotones.TabIndex = 2;
            //
            // btnGuardar
            //
            this.btnGuardar.AutoSize = false;
            this.btnGuardar.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnGuardar.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnGuardar.Depth = 0;
            this.btnGuardar.HighEmphasis = true;
            this.btnGuardar.Icon = null;
            this.btnGuardar.Location = new System.Drawing.Point(122, 8);
            this.btnGuardar.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnGuardar.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnGuardar.Size = new System.Drawing.Size(150, 36);
            this.btnGuardar.TabIndex = 6;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnGuardar.UseAccentColor = false;
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            //
            // btnCancelar
            //
            this.btnCancelar.AutoSize = false;
            this.btnCancelar.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnCancelar.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnCancelar.Depth = 0;
            this.btnCancelar.HighEmphasis = true;
            this.btnCancelar.Icon = null;
            this.btnCancelar.Location = new System.Drawing.Point(12, 8);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnCancelar.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnCancelar.Size = new System.Drawing.Size(100, 36);
            this.btnCancelar.TabIndex = 7;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Text;
            this.btnCancelar.UseAccentColor = false;
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
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
            this.dgvInscripciones.Location = new System.Drawing.Point(0, 140);
            this.dgvInscripciones.Name = "dgvInscripciones";
            this.dgvInscripciones.RowHeadersVisible = false;
            this.dgvInscripciones.Size = new System.Drawing.Size(860, 408);
            this.dgvInscripciones.TabIndex = 5;
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
            this.ClientSize = new System.Drawing.Size(860, 600);
            this.Controls.Add(this.dgvInscripciones);
            this.Controls.Add(this.panelBotones);
            this.Controls.Add(this.panelFiltros);
            this.MinimumSize = new System.Drawing.Size(860, 600);
            this.Name = "FrmInscripcion";
            this.Text = "Inscripciones";
            this.Load += new System.EventHandler(this.FrmInscripcion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudAnio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDivision)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudGrupo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInscripciones)).EndInit();
            this.panelFiltros.ResumeLayout(false);
            this.panelFiltros.PerformLayout();
            this.panelBotones.ResumeLayout(false);
            this.panelBotones.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelFiltros;
        private MaterialSkin.Controls.MaterialLabel lblEspecialidad;
        private MaterialSkin.Controls.MaterialComboBox cmbEspecialidad;
        private MaterialSkin.Controls.MaterialLabel lblAnio;
        private System.Windows.Forms.NumericUpDown nudAnio;
        private MaterialSkin.Controls.MaterialLabel lblDivision;
        private System.Windows.Forms.NumericUpDown nudDivision;
        private MaterialSkin.Controls.MaterialLabel lblGrupo;
        private System.Windows.Forms.NumericUpDown nudGrupo;
        private MaterialSkin.Controls.MaterialLabel lblDictado;
        private MaterialSkin.Controls.MaterialComboBox cmbDictado;
        private System.Windows.Forms.Panel panelBotones;
        private MaterialSkin.Controls.MaterialButton btnGuardar;
        private MaterialSkin.Controls.MaterialButton btnCancelar;
        private System.Windows.Forms.DataGridView dgvInscripciones;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdAlumno;
        private System.Windows.Forms.DataGridViewTextBoxColumn ApellidoAlumno;
        private System.Windows.Forms.DataGridViewTextBoxColumn NombreAlumno;
        private System.Windows.Forms.DataGridViewTextBoxColumn LegajoAlumno;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Inscripto;
    }
}
