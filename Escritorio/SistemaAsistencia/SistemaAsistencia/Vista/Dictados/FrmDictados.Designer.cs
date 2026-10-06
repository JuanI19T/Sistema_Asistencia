namespace SistemaAsistencia.Vista.Dictados
{
    partial class FrmDictados
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
            this.cmbFiltroEspecialidad = new MaterialSkin.Controls.MaterialComboBox();
            this.lblCAnioMateria = new MaterialSkin.Controls.MaterialLabel();
            this.nudAnioMateria = new System.Windows.Forms.NumericUpDown();
            this.lblCDivision = new MaterialSkin.Controls.MaterialLabel();
            this.nudDivision = new System.Windows.Forms.NumericUpDown();
            this.dgvMateriaSel = new System.Windows.Forms.DataGridView();
            this.cmbProfesor = new MaterialSkin.Controls.MaterialComboBox();
            this.cmbDia = new MaterialSkin.Controls.MaterialComboBox();
            this.lblCHorario = new MaterialSkin.Controls.MaterialLabel();
            this.dtpHorario = new System.Windows.Forms.DateTimePicker();
            this.lblCFin = new MaterialSkin.Controls.MaterialLabel();
            this.dtpHorarioFin = new System.Windows.Forms.DateTimePicker();
            this.lblCGrupo = new MaterialSkin.Controls.MaterialLabel();
            this.nudGrupo = new System.Windows.Forms.NumericUpDown();
            this.lblCAnio = new MaterialSkin.Controls.MaterialLabel();
            this.nudAnioLectivo = new System.Windows.Forms.NumericUpDown();
            this.btnGuardar = new MaterialSkin.Controls.MaterialButton();
            this.btnLimpiarCrear = new MaterialSkin.Controls.MaterialButton();
            this.lblEditando = new MaterialSkin.Controls.MaterialLabel();
            this.cmbEditFiltroEspecialidad = new MaterialSkin.Controls.MaterialComboBox();
            this.lblEditAnioMateria = new MaterialSkin.Controls.MaterialLabel();
            this.nudEditAnioMateria = new System.Windows.Forms.NumericUpDown();
            this.lblEditDivision = new MaterialSkin.Controls.MaterialLabel();
            this.nudEditDivision = new System.Windows.Forms.NumericUpDown();
            this.dgvEditMateriaSel = new System.Windows.Forms.DataGridView();
            this.cmbEditProfesor = new MaterialSkin.Controls.MaterialComboBox();
            this.cmbEditDia = new MaterialSkin.Controls.MaterialComboBox();
            this.lblEditHorario = new MaterialSkin.Controls.MaterialLabel();
            this.dtpEditHorario = new System.Windows.Forms.DateTimePicker();
            this.lblEditFin = new MaterialSkin.Controls.MaterialLabel();
            this.dtpEditHorarioFin = new System.Windows.Forms.DateTimePicker();
            this.lblEditGrupo = new MaterialSkin.Controls.MaterialLabel();
            this.nudEditGrupo = new System.Windows.Forms.NumericUpDown();
            this.lblEditAnio = new MaterialSkin.Controls.MaterialLabel();
            this.nudEditAnio = new System.Windows.Forms.NumericUpDown();
            this.btnModificar = new MaterialSkin.Controls.MaterialButton();
            this.btnEliminar = new MaterialSkin.Controls.MaterialButton();
            this.btnLimpiarEditar = new MaterialSkin.Controls.MaterialButton();
            this.txtBuscar = new MaterialSkin.Controls.MaterialTextBox();
            this.btnBuscar = new MaterialSkin.Controls.MaterialButton();
            this.dgvDictados = new System.Windows.Forms.DataGridView();
            this.materialTabControl1 = new MaterialSkin.Controls.MaterialTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.pnlCrear = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.pnlEdit = new System.Windows.Forms.Panel();
            this.materialTabSelector1 = new MaterialSkin.Controls.MaterialTabSelector();
            ((System.ComponentModel.ISupportInitialize)(this.nudAnioMateria)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDivision)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMateriaSel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudGrupo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAnioLectivo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEditAnioMateria)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEditDivision)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEditMateriaSel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEditGrupo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEditAnio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDictados)).BeginInit();
            this.materialTabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.pnlCrear.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.pnlEdit.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbFiltroEspecialidad
            // 
            this.cmbFiltroEspecialidad.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cmbFiltroEspecialidad.AutoResize = false;
            this.cmbFiltroEspecialidad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbFiltroEspecialidad.Depth = 0;
            this.cmbFiltroEspecialidad.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbFiltroEspecialidad.DropDownHeight = 174;
            this.cmbFiltroEspecialidad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFiltroEspecialidad.DropDownWidth = 121;
            this.cmbFiltroEspecialidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.cmbFiltroEspecialidad.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cmbFiltroEspecialidad.FormattingEnabled = true;
            this.cmbFiltroEspecialidad.Hint = "Especialidad";
            this.cmbFiltroEspecialidad.IntegralHeight = false;
            this.cmbFiltroEspecialidad.ItemHeight = 43;
            this.cmbFiltroEspecialidad.Location = new System.Drawing.Point(90, 17);
            this.cmbFiltroEspecialidad.MaxDropDownItems = 4;
            this.cmbFiltroEspecialidad.MouseState = MaterialSkin.MouseState.OUT;
            this.cmbFiltroEspecialidad.Name = "cmbFiltroEspecialidad";
            this.cmbFiltroEspecialidad.Size = new System.Drawing.Size(234, 49);
            this.cmbFiltroEspecialidad.StartIndex = -1;
            this.cmbFiltroEspecialidad.TabIndex = 0;
            this.cmbFiltroEspecialidad.UseAccent = false;
            // 
            // lblCAnioMateria
            // 
            this.lblCAnioMateria.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblCAnioMateria.AutoSize = true;
            this.lblCAnioMateria.BackColor = System.Drawing.Color.White;
            this.lblCAnioMateria.Depth = 0;
            this.lblCAnioMateria.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblCAnioMateria.Location = new System.Drawing.Point(58, 29);
            this.lblCAnioMateria.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblCAnioMateria.Name = "lblCAnioMateria";
            this.lblCAnioMateria.Size = new System.Drawing.Size(29, 19);
            this.lblCAnioMateria.TabIndex = 1;
            this.lblCAnioMateria.Text = "Año";
            // 
            // nudAnioMateria
            // 
            this.nudAnioMateria.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.nudAnioMateria.Location = new System.Drawing.Point(174, 26);
            this.nudAnioMateria.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.nudAnioMateria.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudAnioMateria.Name = "nudAnioMateria";
            this.nudAnioMateria.Size = new System.Drawing.Size(90, 25);
            this.nudAnioMateria.TabIndex = 1;
            this.nudAnioMateria.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblCDivision
            // 
            this.lblCDivision.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblCDivision.AutoSize = true;
            this.lblCDivision.BackColor = System.Drawing.Color.White;
            this.lblCDivision.Depth = 0;
            this.lblCDivision.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblCDivision.Location = new System.Drawing.Point(39, 29);
            this.lblCDivision.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblCDivision.Name = "lblCDivision";
            this.lblCDivision.Size = new System.Drawing.Size(62, 19);
            this.lblCDivision.TabIndex = 2;
            this.lblCDivision.Text = "División ";
            // 
            // nudDivision
            // 
            this.nudDivision.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.nudDivision.Location = new System.Drawing.Point(165, 26);
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
            this.nudDivision.Size = new System.Drawing.Size(90, 25);
            this.nudDivision.TabIndex = 2;
            this.nudDivision.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // dgvMateriaSel
            // 
            this.dgvMateriaSel.AllowUserToAddRows = false;
            this.dgvMateriaSel.AllowUserToDeleteRows = false;
            this.dgvMateriaSel.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMateriaSel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMateriaSel.Location = new System.Drawing.Point(3, 335);
            this.dgvMateriaSel.MultiSelect = false;
            this.dgvMateriaSel.Name = "dgvMateriaSel";
            this.dgvMateriaSel.ReadOnly = true;
            this.dgvMateriaSel.RowHeadersVisible = false;
            this.dgvMateriaSel.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMateriaSel.Size = new System.Drawing.Size(408, 81);
            this.dgvMateriaSel.TabIndex = 4;
            // 
            // cmbProfesor
            // 
            this.cmbProfesor.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cmbProfesor.AutoResize = false;
            this.cmbProfesor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbProfesor.Depth = 0;
            this.cmbProfesor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbProfesor.DropDownHeight = 174;
            this.cmbProfesor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProfesor.DropDownWidth = 121;
            this.cmbProfesor.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.cmbProfesor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cmbProfesor.FormattingEnabled = true;
            this.cmbProfesor.Hint = "Profesor";
            this.cmbProfesor.IntegralHeight = false;
            this.cmbProfesor.ItemHeight = 43;
            this.cmbProfesor.Location = new System.Drawing.Point(500, 17);
            this.cmbProfesor.MaxDropDownItems = 4;
            this.cmbProfesor.MouseState = MaterialSkin.MouseState.OUT;
            this.cmbProfesor.Name = "cmbProfesor";
            this.cmbProfesor.Size = new System.Drawing.Size(241, 49);
            this.cmbProfesor.StartIndex = -1;
            this.cmbProfesor.TabIndex = 5;
            this.cmbProfesor.UseAccent = false;
            // 
            // cmbDia
            // 
            this.cmbDia.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cmbDia.AutoResize = false;
            this.cmbDia.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbDia.Depth = 0;
            this.cmbDia.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbDia.DropDownHeight = 174;
            this.cmbDia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDia.DropDownWidth = 121;
            this.cmbDia.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.cmbDia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cmbDia.FormattingEnabled = true;
            this.cmbDia.Hint = "Día";
            this.cmbDia.IntegralHeight = false;
            this.cmbDia.ItemHeight = 43;
            this.cmbDia.Items.AddRange(new object[] {
            "LUNES",
            "MARTES",
            "MIÉRCOLES",
            "JUEVES",
            "VIERNES"});
            this.cmbDia.Location = new System.Drawing.Point(500, 100);
            this.cmbDia.MaxDropDownItems = 4;
            this.cmbDia.MouseState = MaterialSkin.MouseState.OUT;
            this.cmbDia.Name = "cmbDia";
            this.cmbDia.Size = new System.Drawing.Size(241, 49);
            this.cmbDia.StartIndex = -1;
            this.cmbDia.TabIndex = 6;
            this.cmbDia.UseAccent = false;
            // 
            // lblCHorario
            // 
            this.lblCHorario.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblCHorario.AutoSize = true;
            this.lblCHorario.BackColor = System.Drawing.Color.White;
            this.lblCHorario.Depth = 0;
            this.lblCHorario.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblCHorario.Location = new System.Drawing.Point(31, 29);
            this.lblCHorario.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblCHorario.Name = "lblCHorario";
            this.lblCHorario.Size = new System.Drawing.Size(39, 19);
            this.lblCHorario.TabIndex = 6;
            this.lblCHorario.Text = "Inicio";
            // 
            // dtpHorario
            // 
            this.dtpHorario.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dtpHorario.CustomFormat = "HH:mm";
            this.dtpHorario.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpHorario.Location = new System.Drawing.Point(123, 26);
            this.dtpHorario.Name = "dtpHorario";
            this.dtpHorario.ShowUpDown = true;
            this.dtpHorario.Size = new System.Drawing.Size(60, 25);
            this.dtpHorario.TabIndex = 7;
            // 
            // lblCFin
            // 
            this.lblCFin.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblCFin.AutoSize = true;
            this.lblCFin.BackColor = System.Drawing.Color.White;
            this.lblCFin.Depth = 0;
            this.lblCFin.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblCFin.Location = new System.Drawing.Point(243, 29);
            this.lblCFin.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblCFin.Name = "lblCFin";
            this.lblCFin.Size = new System.Drawing.Size(23, 19);
            this.lblCFin.TabIndex = 7;
            this.lblCFin.Text = "Fin";
            // 
            // dtpHorarioFin
            // 
            this.dtpHorarioFin.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dtpHorarioFin.CustomFormat = "HH:mm";
            this.dtpHorarioFin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpHorarioFin.Location = new System.Drawing.Point(327, 26);
            this.dtpHorarioFin.Name = "dtpHorarioFin";
            this.dtpHorarioFin.ShowUpDown = true;
            this.dtpHorarioFin.Size = new System.Drawing.Size(60, 25);
            this.dtpHorarioFin.TabIndex = 8;
            // 
            // lblCGrupo
            // 
            this.lblCGrupo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblCGrupo.AutoSize = true;
            this.lblCGrupo.BackColor = System.Drawing.Color.White;
            this.lblCGrupo.Depth = 0;
            this.lblCGrupo.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblCGrupo.Location = new System.Drawing.Point(14, 29);
            this.lblCGrupo.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblCGrupo.Name = "lblCGrupo";
            this.lblCGrupo.Size = new System.Drawing.Size(109, 19);
            this.lblCGrupo.TabIndex = 8;
            this.lblCGrupo.Text = "Grupo (0=todo)";
            // 
            // nudGrupo
            // 
            this.nudGrupo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.nudGrupo.Location = new System.Drawing.Point(161, 26);
            this.nudGrupo.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudGrupo.Name = "nudGrupo";
            this.nudGrupo.Size = new System.Drawing.Size(90, 25);
            this.nudGrupo.TabIndex = 3;
            // 
            // lblCAnio
            // 
            this.lblCAnio.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblCAnio.AutoSize = true;
            this.lblCAnio.BackColor = System.Drawing.Color.White;
            this.lblCAnio.Depth = 0;
            this.lblCAnio.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblCAnio.Location = new System.Drawing.Point(62, 29);
            this.lblCAnio.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblCAnio.Name = "lblCAnio";
            this.lblCAnio.Size = new System.Drawing.Size(79, 19);
            this.lblCAnio.TabIndex = 9;
            this.lblCAnio.Text = "Año lectivo";
            // 
            // nudAnioLectivo
            // 
            this.nudAnioLectivo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.nudAnioLectivo.Location = new System.Drawing.Point(261, 26);
            this.nudAnioLectivo.Maximum = new decimal(new int[] {
            2100,
            0,
            0,
            0});
            this.nudAnioLectivo.Minimum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.nudAnioLectivo.Name = "nudAnioLectivo";
            this.nudAnioLectivo.Size = new System.Drawing.Size(90, 25);
            this.nudAnioLectivo.TabIndex = 9;
            this.nudAnioLectivo.Value = new decimal(new int[] {
            2026,
            0,
            0,
            0});
            // 
            // btnGuardar
            // 
            this.btnGuardar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnGuardar.AutoSize = false;
            this.btnGuardar.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnGuardar.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnGuardar.Depth = 0;
            this.btnGuardar.HighEmphasis = true;
            this.btnGuardar.Icon = null;
            this.btnGuardar.Location = new System.Drawing.Point(40, 22);
            this.btnGuardar.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnGuardar.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnGuardar.Size = new System.Drawing.Size(123, 36);
            this.btnGuardar.TabIndex = 10;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnGuardar.UseAccentColor = false;
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnLimpiarCrear
            // 
            this.btnLimpiarCrear.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnLimpiarCrear.AutoSize = false;
            this.btnLimpiarCrear.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnLimpiarCrear.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnLimpiarCrear.Depth = 0;
            this.btnLimpiarCrear.HighEmphasis = true;
            this.btnLimpiarCrear.Icon = null;
            this.btnLimpiarCrear.Location = new System.Drawing.Point(251, 22);
            this.btnLimpiarCrear.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnLimpiarCrear.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnLimpiarCrear.Name = "btnLimpiarCrear";
            this.btnLimpiarCrear.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnLimpiarCrear.Size = new System.Drawing.Size(110, 36);
            this.btnLimpiarCrear.TabIndex = 11;
            this.btnLimpiarCrear.Text = "Limpiar";
            this.btnLimpiarCrear.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Text;
            this.btnLimpiarCrear.UseAccentColor = false;
            this.btnLimpiarCrear.UseVisualStyleBackColor = true;
            this.btnLimpiarCrear.Click += new System.EventHandler(this.btnLimpiarCrear_Click);
            // 
            // lblEditando
            // 
            this.lblEditando.BackColor = System.Drawing.Color.White;
            this.lblEditando.Depth = 0;
            this.lblEditando.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblEditando.Location = new System.Drawing.Point(11, 10);
            this.lblEditando.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblEditando.Name = "lblEditando";
            this.lblEditando.Size = new System.Drawing.Size(403, 19);
            this.lblEditando.TabIndex = 15;
            this.lblEditando.Text = "Editando: (seleccione de la lista)";
            // 
            // cmbEditFiltroEspecialidad
            // 
            this.cmbEditFiltroEspecialidad.AutoResize = false;
            this.cmbEditFiltroEspecialidad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbEditFiltroEspecialidad.Depth = 0;
            this.cmbEditFiltroEspecialidad.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbEditFiltroEspecialidad.DropDownHeight = 174;
            this.cmbEditFiltroEspecialidad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEditFiltroEspecialidad.DropDownWidth = 121;
            this.cmbEditFiltroEspecialidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.cmbEditFiltroEspecialidad.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cmbEditFiltroEspecialidad.FormattingEnabled = true;
            this.cmbEditFiltroEspecialidad.Hint = "Especialidad";
            this.cmbEditFiltroEspecialidad.IntegralHeight = false;
            this.cmbEditFiltroEspecialidad.ItemHeight = 43;
            this.cmbEditFiltroEspecialidad.Location = new System.Drawing.Point(10, 32);
            this.cmbEditFiltroEspecialidad.MaxDropDownItems = 4;
            this.cmbEditFiltroEspecialidad.MouseState = MaterialSkin.MouseState.OUT;
            this.cmbEditFiltroEspecialidad.Name = "cmbEditFiltroEspecialidad";
            this.cmbEditFiltroEspecialidad.Size = new System.Drawing.Size(230, 49);
            this.cmbEditFiltroEspecialidad.StartIndex = -1;
            this.cmbEditFiltroEspecialidad.TabIndex = 2;
            this.cmbEditFiltroEspecialidad.UseAccent = false;
            // 
            // lblEditAnioMateria
            // 
            this.lblEditAnioMateria.AutoSize = true;
            this.lblEditAnioMateria.BackColor = System.Drawing.Color.White;
            this.lblEditAnioMateria.Depth = 0;
            this.lblEditAnioMateria.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblEditAnioMateria.Location = new System.Drawing.Point(10, 100);
            this.lblEditAnioMateria.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblEditAnioMateria.Name = "lblEditAnioMateria";
            this.lblEditAnioMateria.Size = new System.Drawing.Size(29, 19);
            this.lblEditAnioMateria.TabIndex = 3;
            this.lblEditAnioMateria.Text = "Año";
            // 
            // nudEditAnioMateria
            // 
            this.nudEditAnioMateria.Location = new System.Drawing.Point(150, 97);
            this.nudEditAnioMateria.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.nudEditAnioMateria.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudEditAnioMateria.Name = "nudEditAnioMateria";
            this.nudEditAnioMateria.Size = new System.Drawing.Size(90, 25);
            this.nudEditAnioMateria.TabIndex = 3;
            this.nudEditAnioMateria.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblEditDivision
            // 
            this.lblEditDivision.AutoSize = true;
            this.lblEditDivision.BackColor = System.Drawing.Color.White;
            this.lblEditDivision.Depth = 0;
            this.lblEditDivision.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblEditDivision.Location = new System.Drawing.Point(10, 130);
            this.lblEditDivision.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblEditDivision.Name = "lblEditDivision";
            this.lblEditDivision.Size = new System.Drawing.Size(123, 19);
            this.lblEditDivision.TabIndex = 4;
            this.lblEditDivision.Text = "División (0=todo)";
            // 
            // nudEditDivision
            // 
            this.nudEditDivision.Location = new System.Drawing.Point(150, 127);
            this.nudEditDivision.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.nudEditDivision.Name = "nudEditDivision";
            this.nudEditDivision.Size = new System.Drawing.Size(90, 25);
            this.nudEditDivision.TabIndex = 4;
            // 
            // dgvEditMateriaSel
            // 
            this.dgvEditMateriaSel.AllowUserToAddRows = false;
            this.dgvEditMateriaSel.AllowUserToDeleteRows = false;
            this.dgvEditMateriaSel.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEditMateriaSel.Location = new System.Drawing.Point(10, 185);
            this.dgvEditMateriaSel.MultiSelect = false;
            this.dgvEditMateriaSel.Name = "dgvEditMateriaSel";
            this.dgvEditMateriaSel.ReadOnly = true;
            this.dgvEditMateriaSel.RowHeadersVisible = false;
            this.dgvEditMateriaSel.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEditMateriaSel.Size = new System.Drawing.Size(274, 59);
            this.dgvEditMateriaSel.TabIndex = 6;
            // 
            // cmbEditProfesor
            // 
            this.cmbEditProfesor.AutoResize = false;
            this.cmbEditProfesor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbEditProfesor.Depth = 0;
            this.cmbEditProfesor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbEditProfesor.DropDownHeight = 174;
            this.cmbEditProfesor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEditProfesor.DropDownWidth = 121;
            this.cmbEditProfesor.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.cmbEditProfesor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cmbEditProfesor.FormattingEnabled = true;
            this.cmbEditProfesor.Hint = "Profesor";
            this.cmbEditProfesor.IntegralHeight = false;
            this.cmbEditProfesor.ItemHeight = 43;
            this.cmbEditProfesor.Location = new System.Drawing.Point(13, 250);
            this.cmbEditProfesor.MaxDropDownItems = 4;
            this.cmbEditProfesor.MouseState = MaterialSkin.MouseState.OUT;
            this.cmbEditProfesor.Name = "cmbEditProfesor";
            this.cmbEditProfesor.Size = new System.Drawing.Size(227, 49);
            this.cmbEditProfesor.StartIndex = -1;
            this.cmbEditProfesor.TabIndex = 7;
            this.cmbEditProfesor.UseAccent = false;
            // 
            // cmbEditDia
            // 
            this.cmbEditDia.AutoResize = false;
            this.cmbEditDia.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbEditDia.Depth = 0;
            this.cmbEditDia.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbEditDia.DropDownHeight = 174;
            this.cmbEditDia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEditDia.DropDownWidth = 121;
            this.cmbEditDia.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.cmbEditDia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cmbEditDia.FormattingEnabled = true;
            this.cmbEditDia.Hint = "Día";
            this.cmbEditDia.IntegralHeight = false;
            this.cmbEditDia.ItemHeight = 43;
            this.cmbEditDia.Items.AddRange(new object[] {
            "LUNES",
            "MARTES",
            "MIÉRCOLES",
            "JUEVES",
            "VIERNES"});
            this.cmbEditDia.Location = new System.Drawing.Point(13, 307);
            this.cmbEditDia.MaxDropDownItems = 4;
            this.cmbEditDia.MouseState = MaterialSkin.MouseState.OUT;
            this.cmbEditDia.Name = "cmbEditDia";
            this.cmbEditDia.Size = new System.Drawing.Size(227, 49);
            this.cmbEditDia.StartIndex = -1;
            this.cmbEditDia.TabIndex = 8;
            this.cmbEditDia.UseAccent = false;
            // 
            // lblEditHorario
            // 
            this.lblEditHorario.AutoSize = true;
            this.lblEditHorario.BackColor = System.Drawing.Color.White;
            this.lblEditHorario.Depth = 0;
            this.lblEditHorario.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblEditHorario.Location = new System.Drawing.Point(10, 366);
            this.lblEditHorario.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblEditHorario.Name = "lblEditHorario";
            this.lblEditHorario.Size = new System.Drawing.Size(39, 19);
            this.lblEditHorario.TabIndex = 8;
            this.lblEditHorario.Text = "Inicio";
            // 
            // dtpEditHorario
            // 
            this.dtpEditHorario.CustomFormat = "HH:mm";
            this.dtpEditHorario.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEditHorario.Location = new System.Drawing.Point(73, 362);
            this.dtpEditHorario.Name = "dtpEditHorario";
            this.dtpEditHorario.ShowUpDown = true;
            this.dtpEditHorario.Size = new System.Drawing.Size(60, 25);
            this.dtpEditHorario.TabIndex = 9;
            // 
            // lblEditFin
            // 
            this.lblEditFin.AutoSize = true;
            this.lblEditFin.BackColor = System.Drawing.Color.White;
            this.lblEditFin.Depth = 0;
            this.lblEditFin.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblEditFin.Location = new System.Drawing.Point(261, 366);
            this.lblEditFin.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblEditFin.Name = "lblEditFin";
            this.lblEditFin.Size = new System.Drawing.Size(23, 19);
            this.lblEditFin.TabIndex = 9;
            this.lblEditFin.Text = "Fin";
            // 
            // dtpEditHorarioFin
            // 
            this.dtpEditHorarioFin.CustomFormat = "HH:mm";
            this.dtpEditHorarioFin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEditHorarioFin.Location = new System.Drawing.Point(290, 362);
            this.dtpEditHorarioFin.Name = "dtpEditHorarioFin";
            this.dtpEditHorarioFin.ShowUpDown = true;
            this.dtpEditHorarioFin.Size = new System.Drawing.Size(60, 25);
            this.dtpEditHorarioFin.TabIndex = 10;
            // 
            // lblEditGrupo
            // 
            this.lblEditGrupo.AutoSize = true;
            this.lblEditGrupo.BackColor = System.Drawing.Color.White;
            this.lblEditGrupo.Depth = 0;
            this.lblEditGrupo.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblEditGrupo.Location = new System.Drawing.Point(10, 160);
            this.lblEditGrupo.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblEditGrupo.Name = "lblEditGrupo";
            this.lblEditGrupo.Size = new System.Drawing.Size(109, 19);
            this.lblEditGrupo.TabIndex = 10;
            this.lblEditGrupo.Text = "Grupo (0=todo)";
            // 
            // nudEditGrupo
            // 
            this.nudEditGrupo.Location = new System.Drawing.Point(150, 157);
            this.nudEditGrupo.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudEditGrupo.Name = "nudEditGrupo";
            this.nudEditGrupo.Size = new System.Drawing.Size(90, 25);
            this.nudEditGrupo.TabIndex = 5;
            // 
            // lblEditAnio
            // 
            this.lblEditAnio.AutoSize = true;
            this.lblEditAnio.BackColor = System.Drawing.Color.White;
            this.lblEditAnio.Depth = 0;
            this.lblEditAnio.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblEditAnio.Location = new System.Drawing.Point(10, 396);
            this.lblEditAnio.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblEditAnio.Name = "lblEditAnio";
            this.lblEditAnio.Size = new System.Drawing.Size(79, 19);
            this.lblEditAnio.TabIndex = 11;
            this.lblEditAnio.Text = "Año lectivo";
            // 
            // nudEditAnio
            // 
            this.nudEditAnio.Location = new System.Drawing.Point(150, 393);
            this.nudEditAnio.Maximum = new decimal(new int[] {
            2100,
            0,
            0,
            0});
            this.nudEditAnio.Minimum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.nudEditAnio.Name = "nudEditAnio";
            this.nudEditAnio.Size = new System.Drawing.Size(90, 25);
            this.nudEditAnio.TabIndex = 11;
            this.nudEditAnio.Value = new decimal(new int[] {
            2026,
            0,
            0,
            0});
            // 
            // btnModificar
            // 
            this.btnModificar.AutoSize = false;
            this.btnModificar.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnModificar.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnModificar.Depth = 0;
            this.btnModificar.HighEmphasis = true;
            this.btnModificar.Icon = null;
            this.btnModificar.Location = new System.Drawing.Point(4, 426);
            this.btnModificar.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnModificar.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnModificar.Size = new System.Drawing.Size(110, 36);
            this.btnModificar.TabIndex = 12;
            this.btnModificar.Text = "Modificar";
            this.btnModificar.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnModificar.UseAccentColor = false;
            this.btnModificar.UseVisualStyleBackColor = true;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.AutoSize = false;
            this.btnEliminar.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnEliminar.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnEliminar.Depth = 0;
            this.btnEliminar.HighEmphasis = true;
            this.btnEliminar.Icon = null;
            this.btnEliminar.Location = new System.Drawing.Point(122, 426);
            this.btnEliminar.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnEliminar.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnEliminar.Size = new System.Drawing.Size(110, 36);
            this.btnEliminar.TabIndex = 13;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Outlined;
            this.btnEliminar.UseAccentColor = false;
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnLimpiarEditar
            // 
            this.btnLimpiarEditar.AutoSize = false;
            this.btnLimpiarEditar.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnLimpiarEditar.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnLimpiarEditar.Depth = 0;
            this.btnLimpiarEditar.HighEmphasis = true;
            this.btnLimpiarEditar.Icon = null;
            this.btnLimpiarEditar.Location = new System.Drawing.Point(240, 426);
            this.btnLimpiarEditar.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnLimpiarEditar.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnLimpiarEditar.Name = "btnLimpiarEditar";
            this.btnLimpiarEditar.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnLimpiarEditar.Size = new System.Drawing.Size(110, 36);
            this.btnLimpiarEditar.TabIndex = 14;
            this.btnLimpiarEditar.Text = "Limpiar";
            this.btnLimpiarEditar.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Text;
            this.btnLimpiarEditar.UseAccentColor = false;
            this.btnLimpiarEditar.UseVisualStyleBackColor = true;
            this.btnLimpiarEditar.Click += new System.EventHandler(this.btnLimpiarEditar_Click);
            // 
            // txtBuscar
            // 
            this.txtBuscar.AnimateReadOnly = false;
            this.txtBuscar.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBuscar.Depth = 0;
            this.txtBuscar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBuscar.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtBuscar.Hint = "Buscar dictado";
            this.txtBuscar.LeadingIcon = null;
            this.txtBuscar.Location = new System.Drawing.Point(3, 3);
            this.txtBuscar.MaxLength = 50;
            this.txtBuscar.MouseState = MaterialSkin.MouseState.OUT;
            this.txtBuscar.Multiline = false;
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(276, 50);
            this.txtBuscar.TabIndex = 0;
            this.txtBuscar.Text = "";
            this.txtBuscar.TrailingIcon = null;
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnBuscar.AutoSize = false;
            this.btnBuscar.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnBuscar.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnBuscar.Depth = 0;
            this.btnBuscar.HighEmphasis = true;
            this.btnBuscar.Icon = null;
            this.btnBuscar.Location = new System.Drawing.Point(287, 6);
            this.btnBuscar.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnBuscar.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnBuscar.Size = new System.Drawing.Size(90, 34);
            this.btnBuscar.TabIndex = 1;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnBuscar.UseAccentColor = false;
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // dgvDictados
            // 
            this.dgvDictados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDictados.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDictados.Location = new System.Drawing.Point(3, 55);
            this.dgvDictados.Name = "dgvDictados";
            this.dgvDictados.Size = new System.Drawing.Size(382, 380);
            this.dgvDictados.TabIndex = 1;
            this.dgvDictados.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDictados_CellClick);
            // 
            // materialTabControl1
            // 
            this.materialTabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.materialTabControl1.Controls.Add(this.tabPage1);
            this.materialTabControl1.Controls.Add(this.tabPage2);
            this.materialTabControl1.Depth = 0;
            this.materialTabControl1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.materialTabControl1.Location = new System.Drawing.Point(6, 52);
            this.materialTabControl1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTabControl1.Multiline = true;
            this.materialTabControl1.Name = "materialTabControl1";
            this.materialTabControl1.SelectedIndex = 0;
            this.materialTabControl1.Size = new System.Drawing.Size(848, 542);
            this.materialTabControl1.TabIndex = 16;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.White;
            this.tabPage1.Controls.Add(this.pnlCrear);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(840, 512);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Crear un Dictado";
            // 
            // pnlCrear
            // 
            this.pnlCrear.AutoScroll = true;
            this.pnlCrear.Controls.Add(this.tableLayoutPanel1);
            this.pnlCrear.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCrear.Location = new System.Drawing.Point(3, 3);
            this.pnlCrear.Name = "pnlCrear";
            this.pnlCrear.Size = new System.Drawing.Size(834, 506);
            this.pnlCrear.TabIndex = 12;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel9, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel8, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.cmbDia, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.cmbProfesor, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.dgvMateriaSel, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.cmbFiltroEspecialidad, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel5, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel6, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel7, 1, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(828, 419);
            this.tableLayoutPanel1.TabIndex = 12;
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.ColumnCount = 2;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel9.Controls.Add(this.btnGuardar, 0, 0);
            this.tableLayoutPanel9.Controls.Add(this.btnLimpiarCrear, 1, 0);
            this.tableLayoutPanel9.Location = new System.Drawing.Point(417, 335);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 1;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(408, 81);
            this.tableLayoutPanel9.TabIndex = 7;
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 2;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.Controls.Add(this.lblCAnio, 0, 0);
            this.tableLayoutPanel8.Controls.Add(this.nudAnioLectivo, 1, 0);
            this.tableLayoutPanel8.Location = new System.Drawing.Point(417, 252);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 1;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(408, 77);
            this.tableLayoutPanel8.TabIndex = 4;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.nudAnioMateria, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblCAnioMateria, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(61, 86);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(292, 77);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Controls.Add(this.lblCDivision, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.nudDivision, 1, 0);
            this.tableLayoutPanel5.Location = new System.Drawing.Point(67, 169);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(280, 77);
            this.tableLayoutPanel5.TabIndex = 2;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tableLayoutPanel6.ColumnCount = 2;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Controls.Add(this.lblCGrupo, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.nudGrupo, 1, 0);
            this.tableLayoutPanel6.Location = new System.Drawing.Point(69, 252);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(275, 77);
            this.tableLayoutPanel6.TabIndex = 3;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 4;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel7.Controls.Add(this.lblCHorario, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.dtpHorarioFin, 3, 0);
            this.tableLayoutPanel7.Controls.Add(this.lblCFin, 2, 0);
            this.tableLayoutPanel7.Controls.Add(this.dtpHorario, 1, 0);
            this.tableLayoutPanel7.Location = new System.Drawing.Point(417, 169);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 1;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(408, 77);
            this.tableLayoutPanel7.TabIndex = 4;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.White;
            this.tabPage2.Controls.Add(this.tableLayoutPanel3);
            this.tabPage2.Controls.Add(this.pnlEdit);
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(840, 512);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Modificar un Dictado";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.dgvDictados, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 0, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(443, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 88F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(388, 438);
            this.tableLayoutPanel3.TabIndex = 3;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel4.Controls.Add(this.txtBuscar, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnBuscar, 1, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(382, 46);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // pnlEdit
            // 
            this.pnlEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlEdit.AutoScroll = true;
            this.pnlEdit.Controls.Add(this.lblEditando);
            this.pnlEdit.Controls.Add(this.cmbEditFiltroEspecialidad);
            this.pnlEdit.Controls.Add(this.lblEditAnioMateria);
            this.pnlEdit.Controls.Add(this.nudEditAnioMateria);
            this.pnlEdit.Controls.Add(this.lblEditDivision);
            this.pnlEdit.Controls.Add(this.nudEditDivision);
            this.pnlEdit.Controls.Add(this.dgvEditMateriaSel);
            this.pnlEdit.Controls.Add(this.cmbEditProfesor);
            this.pnlEdit.Controls.Add(this.cmbEditDia);
            this.pnlEdit.Controls.Add(this.lblEditHorario);
            this.pnlEdit.Controls.Add(this.dtpEditHorario);
            this.pnlEdit.Controls.Add(this.lblEditFin);
            this.pnlEdit.Controls.Add(this.dtpEditHorarioFin);
            this.pnlEdit.Controls.Add(this.lblEditGrupo);
            this.pnlEdit.Controls.Add(this.nudEditGrupo);
            this.pnlEdit.Controls.Add(this.lblEditAnio);
            this.pnlEdit.Controls.Add(this.nudEditAnio);
            this.pnlEdit.Controls.Add(this.btnModificar);
            this.pnlEdit.Controls.Add(this.btnEliminar);
            this.pnlEdit.Controls.Add(this.btnLimpiarEditar);
            this.pnlEdit.Location = new System.Drawing.Point(3, 3);
            this.pnlEdit.Name = "pnlEdit";
            this.pnlEdit.Size = new System.Drawing.Size(434, 438);
            this.pnlEdit.TabIndex = 2;
            // 
            // materialTabSelector1
            // 
            this.materialTabSelector1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.materialTabSelector1.BaseTabControl = this.materialTabControl1;
            this.materialTabSelector1.CharacterCasing = MaterialSkin.Controls.MaterialTabSelector.CustomCharacterCasing.Normal;
            this.materialTabSelector1.Depth = 0;
            this.materialTabSelector1.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialTabSelector1.Location = new System.Drawing.Point(6, 5);
            this.materialTabSelector1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTabSelector1.Name = "materialTabSelector1";
            this.materialTabSelector1.Size = new System.Drawing.Size(848, 41);
            this.materialTabSelector1.TabIndex = 17;
            // 
            // FrmDictados
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(860, 600);
            this.Controls.Add(this.materialTabSelector1);
            this.Controls.Add(this.materialTabControl1);
            this.MinimumSize = new System.Drawing.Size(860, 600);
            this.Name = "FrmDictados";
            this.Text = "Dictados";
            this.Load += new System.EventHandler(this.FrmDictados_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudAnioMateria)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDivision)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMateriaSel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudGrupo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAnioLectivo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEditAnioMateria)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEditDivision)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEditMateriaSel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEditGrupo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEditAnio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDictados)).EndInit();
            this.materialTabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.pnlCrear.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel9.ResumeLayout(false);
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel8.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.pnlEdit.ResumeLayout(false);
            this.pnlEdit.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private MaterialSkin.Controls.MaterialComboBox cmbFiltroEspecialidad;
        private MaterialSkin.Controls.MaterialLabel lblCAnioMateria;
        private System.Windows.Forms.NumericUpDown nudAnioMateria;
        private MaterialSkin.Controls.MaterialLabel lblCDivision;
        private System.Windows.Forms.NumericUpDown nudDivision;
        private System.Windows.Forms.DataGridView dgvMateriaSel;
        private MaterialSkin.Controls.MaterialComboBox cmbProfesor;
        private MaterialSkin.Controls.MaterialComboBox cmbDia;
        private MaterialSkin.Controls.MaterialLabel lblCHorario;
        private System.Windows.Forms.DateTimePicker dtpHorario;
        private MaterialSkin.Controls.MaterialLabel lblCFin;
        private System.Windows.Forms.DateTimePicker dtpHorarioFin;
        private MaterialSkin.Controls.MaterialLabel lblCGrupo;
        private System.Windows.Forms.NumericUpDown nudGrupo;
        private MaterialSkin.Controls.MaterialLabel lblCAnio;
        private System.Windows.Forms.NumericUpDown nudAnioLectivo;
        private MaterialSkin.Controls.MaterialButton btnGuardar;
        private MaterialSkin.Controls.MaterialButton btnLimpiarCrear;
        private MaterialSkin.Controls.MaterialLabel lblEditando;
        private MaterialSkin.Controls.MaterialComboBox cmbEditFiltroEspecialidad;
        private MaterialSkin.Controls.MaterialLabel lblEditAnioMateria;
        private System.Windows.Forms.NumericUpDown nudEditAnioMateria;
        private MaterialSkin.Controls.MaterialLabel lblEditDivision;
        private System.Windows.Forms.NumericUpDown nudEditDivision;
        private System.Windows.Forms.DataGridView dgvEditMateriaSel;
        private MaterialSkin.Controls.MaterialComboBox cmbEditProfesor;
        private MaterialSkin.Controls.MaterialComboBox cmbEditDia;
        private MaterialSkin.Controls.MaterialLabel lblEditHorario;
        private System.Windows.Forms.DateTimePicker dtpEditHorario;
        private MaterialSkin.Controls.MaterialLabel lblEditFin;
        private System.Windows.Forms.DateTimePicker dtpEditHorarioFin;
        private MaterialSkin.Controls.MaterialLabel lblEditGrupo;
        private System.Windows.Forms.NumericUpDown nudEditGrupo;
        private MaterialSkin.Controls.MaterialLabel lblEditAnio;
        private System.Windows.Forms.NumericUpDown nudEditAnio;
        private MaterialSkin.Controls.MaterialButton btnModificar;
        private MaterialSkin.Controls.MaterialButton btnEliminar;
        private MaterialSkin.Controls.MaterialButton btnLimpiarEditar;
        private MaterialSkin.Controls.MaterialTextBox txtBuscar;
        private MaterialSkin.Controls.MaterialButton btnBuscar;
        private System.Windows.Forms.DataGridView dgvDictados;
        private MaterialSkin.Controls.MaterialTabControl materialTabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Panel pnlCrear;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel pnlEdit;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private MaterialSkin.Controls.MaterialTabSelector materialTabSelector1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
    }
}
