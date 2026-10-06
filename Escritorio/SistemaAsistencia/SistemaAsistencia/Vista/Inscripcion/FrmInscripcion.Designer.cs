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
            this.lblAnio = new MaterialSkin.Controls.MaterialLabel();
            this.nudAnio = new System.Windows.Forms.NumericUpDown();
            this.lblDivision = new MaterialSkin.Controls.MaterialLabel();
            this.nudDivision = new System.Windows.Forms.NumericUpDown();
            this.lblGrupo = new MaterialSkin.Controls.MaterialLabel();
            this.nudGrupo = new System.Windows.Forms.NumericUpDown();
            this.panelBotones = new System.Windows.Forms.Panel();
            this.btnGuardar = new MaterialSkin.Controls.MaterialButton();
            this.btnCancelar = new MaterialSkin.Controls.MaterialButton();
            this.dgvInscripciones = new System.Windows.Forms.DataGridView();
            this.dgvDictados = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cmbEspecialidad = new MaterialSkin.Controls.MaterialComboBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.Materia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Anio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Division = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Grupo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdDictado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdAlumno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ApellidoAlumno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NombreAlumno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LegajoAlumno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Inscripto = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.nudAnio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDivision)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudGrupo)).BeginInit();
            this.panelBotones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInscripciones)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDictados)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblAnio
            // 
            this.lblAnio.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblAnio.AutoSize = true;
            this.lblAnio.BackColor = System.Drawing.Color.White;
            this.lblAnio.Depth = 0;
            this.lblAnio.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblAnio.Location = new System.Drawing.Point(61, 13);
            this.lblAnio.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblAnio.Name = "lblAnio";
            this.lblAnio.Size = new System.Drawing.Size(29, 19);
            this.lblAnio.TabIndex = 1;
            this.lblAnio.Text = "Año";
            // 
            // nudAnio
            // 
            this.nudAnio.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.nudAnio.Location = new System.Drawing.Point(209, 12);
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
            this.nudAnio.Size = new System.Drawing.Size(35, 20);
            this.nudAnio.TabIndex = 1;
            this.nudAnio.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblDivision
            // 
            this.lblDivision.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblDivision.AutoSize = true;
            this.lblDivision.BackColor = System.Drawing.Color.White;
            this.lblDivision.Depth = 0;
            this.lblDivision.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblDivision.Location = new System.Drawing.Point(46, 13);
            this.lblDivision.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblDivision.Name = "lblDivision";
            this.lblDivision.Size = new System.Drawing.Size(58, 19);
            this.lblDivision.TabIndex = 2;
            this.lblDivision.Text = "División";
            // 
            // nudDivision
            // 
            this.nudDivision.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.nudDivision.Location = new System.Drawing.Point(207, 12);
            this.nudDivision.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.nudDivision.Name = "nudDivision";
            this.nudDivision.Size = new System.Drawing.Size(39, 20);
            this.nudDivision.TabIndex = 2;
            this.nudDivision.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblGrupo
            // 
            this.lblGrupo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblGrupo.AutoSize = true;
            this.lblGrupo.BackColor = System.Drawing.Color.White;
            this.lblGrupo.Depth = 0;
            this.lblGrupo.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblGrupo.Location = new System.Drawing.Point(12, 13);
            this.lblGrupo.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblGrupo.Name = "lblGrupo";
            this.lblGrupo.Size = new System.Drawing.Size(126, 19);
            this.lblGrupo.TabIndex = 3;
            this.lblGrupo.Text = "Grupo (0=ambos)";
            // 
            // nudGrupo
            // 
            this.nudGrupo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.nudGrupo.Location = new System.Drawing.Point(203, 13);
            this.nudGrupo.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudGrupo.Name = "nudGrupo";
            this.nudGrupo.Size = new System.Drawing.Size(47, 20);
            this.nudGrupo.TabIndex = 3;
            // 
            // panelBotones
            // 
            this.panelBotones.BackColor = System.Drawing.Color.White;
            this.panelBotones.Controls.Add(this.btnGuardar);
            this.panelBotones.Controls.Add(this.btnCancelar);
            this.panelBotones.Location = new System.Drawing.Point(0, 423);
            this.panelBotones.Name = "panelBotones";
            this.panelBotones.Size = new System.Drawing.Size(854, 52);
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
            this.dgvInscripciones.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dgvInscripciones.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvInscripciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInscripciones.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IdAlumno,
            this.ApellidoAlumno,
            this.NombreAlumno,
            this.LegajoAlumno,
            this.Inscripto});
            this.dgvInscripciones.Location = new System.Drawing.Point(321, 5);
            this.dgvInscripciones.Name = "dgvInscripciones";
            this.dgvInscripciones.RowHeadersVisible = false;
            this.dgvInscripciones.Size = new System.Drawing.Size(533, 412);
            this.dgvInscripciones.TabIndex = 5;
            // 
            // dgvDictados
            // 
            this.dgvDictados.AllowUserToAddRows = false;
            this.dgvDictados.AllowUserToDeleteRows = false;
            this.dgvDictados.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDictados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDictados.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Materia,
            this.Anio,
            this.Division,
            this.Grupo,
            this.IdDictado});
            this.dgvDictados.Location = new System.Drawing.Point(6, 213);
            this.dgvDictados.MultiSelect = false;
            this.dgvDictados.Name = "dgvDictados";
            this.dgvDictados.ReadOnly = true;
            this.dgvDictados.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDictados.Size = new System.Drawing.Size(309, 204);
            this.dgvDictados.TabIndex = 6;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.cmbEspecialidad, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 5);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(309, 205);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // cmbEspecialidad
            // 
            this.cmbEspecialidad.Anchor = System.Windows.Forms.AnchorStyles.None;
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
            this.cmbEspecialidad.Location = new System.Drawing.Point(36, 3);
            this.cmbEspecialidad.MaxDropDownItems = 4;
            this.cmbEspecialidad.MouseState = MaterialSkin.MouseState.OUT;
            this.cmbEspecialidad.Name = "cmbEspecialidad";
            this.cmbEspecialidad.Size = new System.Drawing.Size(236, 49);
            this.cmbEspecialidad.StartIndex = -1;
            this.cmbEspecialidad.TabIndex = 0;
            this.cmbEspecialidad.UseAccent = false;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.nudAnio, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblAnio, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 54);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(303, 45);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.nudDivision, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.lblDivision, 0, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 105);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(303, 45);
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.nudGrupo, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.lblGrupo, 0, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 156);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(303, 46);
            this.tableLayoutPanel4.TabIndex = 3;
            // 
            // Materia
            // 
            this.Materia.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Materia.HeaderText = "Materia";
            this.Materia.Name = "Materia";
            this.Materia.ReadOnly = true;
            // 
            // Anio
            // 
            this.Anio.HeaderText = "Año";
            this.Anio.Name = "Anio";
            this.Anio.ReadOnly = true;
            // 
            // Division
            // 
            this.Division.HeaderText = "División";
            this.Division.Name = "Division";
            this.Division.ReadOnly = true;
            // 
            // Grupo
            // 
            this.Grupo.HeaderText = "Grupo";
            this.Grupo.Name = "Grupo";
            this.Grupo.ReadOnly = true;
            // 
            // IdDictado
            // 
            this.IdDictado.HeaderText = "Dictado";
            this.IdDictado.Name = "IdDictado";
            this.IdDictado.ReadOnly = true;
            this.IdDictado.Visible = false;
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
            this.ApellidoAlumno.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ApellidoAlumno.HeaderText = "Apellido";
            this.ApellidoAlumno.Name = "ApellidoAlumno";
            this.ApellidoAlumno.ReadOnly = true;
            // 
            // NombreAlumno
            // 
            this.NombreAlumno.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.NombreAlumno.HeaderText = "Nombre";
            this.NombreAlumno.Name = "NombreAlumno";
            this.NombreAlumno.ReadOnly = true;
            // 
            // LegajoAlumno
            // 
            this.LegajoAlumno.HeaderText = "Legajo";
            this.LegajoAlumno.Name = "LegajoAlumno";
            this.LegajoAlumno.ReadOnly = true;
            // 
            // Inscripto
            // 
            this.Inscripto.HeaderText = "Inscripto";
            this.Inscripto.Name = "Inscripto";
            // 
            // FrmInscripcion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(860, 600);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.dgvDictados);
            this.Controls.Add(this.dgvInscripciones);
            this.Controls.Add(this.panelBotones);
            this.MinimumSize = new System.Drawing.Size(860, 600);
            this.Name = "FrmInscripcion";
            this.Text = "Inscripciones";
            this.Load += new System.EventHandler(this.FrmInscripcion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudAnio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDivision)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudGrupo)).EndInit();
            this.panelBotones.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInscripciones)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDictados)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private MaterialSkin.Controls.MaterialLabel lblAnio;
        private System.Windows.Forms.NumericUpDown nudAnio;
        private MaterialSkin.Controls.MaterialLabel lblDivision;
        private System.Windows.Forms.NumericUpDown nudDivision;
        private MaterialSkin.Controls.MaterialLabel lblGrupo;
        private System.Windows.Forms.NumericUpDown nudGrupo;
        private System.Windows.Forms.Panel panelBotones;
        private MaterialSkin.Controls.MaterialButton btnGuardar;
        private MaterialSkin.Controls.MaterialButton btnCancelar;
        private System.Windows.Forms.DataGridView dgvInscripciones;
        private System.Windows.Forms.DataGridView dgvDictados;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private MaterialSkin.Controls.MaterialComboBox cmbEspecialidad;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Materia;
        private System.Windows.Forms.DataGridViewTextBoxColumn Anio;
        private System.Windows.Forms.DataGridViewTextBoxColumn Division;
        private System.Windows.Forms.DataGridViewTextBoxColumn Grupo;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdDictado;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdAlumno;
        private System.Windows.Forms.DataGridViewTextBoxColumn ApellidoAlumno;
        private System.Windows.Forms.DataGridViewTextBoxColumn NombreAlumno;
        private System.Windows.Forms.DataGridViewTextBoxColumn LegajoAlumno;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Inscripto;
    }
}
