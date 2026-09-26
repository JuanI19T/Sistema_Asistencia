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
            this.grpCrear = new System.Windows.Forms.GroupBox();
            this.btnLimpiarCrear = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.nudAnioLectivo = new System.Windows.Forms.NumericUpDown();
            this.lblCAnio = new System.Windows.Forms.Label();
            this.txtGrupo = new System.Windows.Forms.TextBox();
            this.lblCGrupo = new System.Windows.Forms.Label();
            this.txtHorario = new System.Windows.Forms.TextBox();
            this.lblCHorario = new System.Windows.Forms.Label();
            this.cmbDia = new System.Windows.Forms.ComboBox();
            this.lblCDia = new System.Windows.Forms.Label();
            this.cmbProfesor = new System.Windows.Forms.ComboBox();
            this.lblCProfesor = new System.Windows.Forms.Label();
            this.cmbMateria = new System.Windows.Forms.ComboBox();
            this.lblCMateria = new System.Windows.Forms.Label();
            this.grpModificar = new System.Windows.Forms.GroupBox();
            this.btnLimpiarEditar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.nudEditAnio = new System.Windows.Forms.NumericUpDown();
            this.lblEditAnio = new System.Windows.Forms.Label();
            this.txtEditGrupo = new System.Windows.Forms.TextBox();
            this.lblEditGrupo = new System.Windows.Forms.Label();
            this.txtEditHorario = new System.Windows.Forms.TextBox();
            this.lblEditHorario = new System.Windows.Forms.Label();
            this.cmbEditDia = new System.Windows.Forms.ComboBox();
            this.lblEditDia = new System.Windows.Forms.Label();
            this.cmbEditProfesor = new System.Windows.Forms.ComboBox();
            this.lblEditProfesor = new System.Windows.Forms.Label();
            this.cmbEditMateria = new System.Windows.Forms.ComboBox();
            this.lblEditMateria = new System.Windows.Forms.Label();
            this.lblEditando = new System.Windows.Forms.Label();
            this.pnlBusqueda = new System.Windows.Forms.Panel();
            this.dgvDictados = new System.Windows.Forms.DataGridView();
            this.pnlTopBusqueda = new System.Windows.Forms.Panel();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.lblTBuscar = new System.Windows.Forms.Label();
            this.tlpBase = new System.Windows.Forms.TableLayoutPanel();
            this.pnlIzquierda = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.nudAnioLectivo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEditAnio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDictados)).BeginInit();
            this.grpCrear.SuspendLayout();
            this.grpModificar.SuspendLayout();
            this.pnlBusqueda.SuspendLayout();
            this.pnlTopBusqueda.SuspendLayout();
            this.tlpBase.SuspendLayout();
            this.pnlIzquierda.SuspendLayout();
            this.SuspendLayout();
            //
            // grpCrear
            //
            this.grpCrear.Controls.Add(this.btnLimpiarCrear);
            this.grpCrear.Controls.Add(this.btnGuardar);
            this.grpCrear.Controls.Add(this.nudAnioLectivo);
            this.grpCrear.Controls.Add(this.lblCAnio);
            this.grpCrear.Controls.Add(this.txtGrupo);
            this.grpCrear.Controls.Add(this.lblCGrupo);
            this.grpCrear.Controls.Add(this.txtHorario);
            this.grpCrear.Controls.Add(this.lblCHorario);
            this.grpCrear.Controls.Add(this.cmbDia);
            this.grpCrear.Controls.Add(this.lblCDia);
            this.grpCrear.Controls.Add(this.cmbProfesor);
            this.grpCrear.Controls.Add(this.lblCProfesor);
            this.grpCrear.Controls.Add(this.cmbMateria);
            this.grpCrear.Controls.Add(this.lblCMateria);
            this.grpCrear.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpCrear.Location = new System.Drawing.Point(0, 0);
            this.grpCrear.Name = "grpCrear";
            this.grpCrear.Size = new System.Drawing.Size(560, 190);
            this.grpCrear.TabIndex = 0;
            this.grpCrear.TabStop = false;
            this.grpCrear.Text = "1. Crear dictado";
            //
            // lblCMateria
            //
            this.lblCMateria.AutoSize = true;
            this.lblCMateria.Location = new System.Drawing.Point(16, 30);
            this.lblCMateria.Name = "lblCMateria";
            this.lblCMateria.Size = new System.Drawing.Size(44, 13);
            this.lblCMateria.TabIndex = 0;
            this.lblCMateria.Text = "Materia";
            //
            // cmbMateria
            //
            this.cmbMateria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMateria.FormattingEnabled = true;
            this.cmbMateria.Location = new System.Drawing.Point(110, 27);
            this.cmbMateria.Name = "cmbMateria";
            this.cmbMateria.Size = new System.Drawing.Size(170, 21);
            this.cmbMateria.TabIndex = 1;
            //
            // lblCProfesor
            //
            this.lblCProfesor.AutoSize = true;
            this.lblCProfesor.Location = new System.Drawing.Point(16, 60);
            this.lblCProfesor.Name = "lblCProfesor";
            this.lblCProfesor.Size = new System.Drawing.Size(46, 13);
            this.lblCProfesor.TabIndex = 2;
            this.lblCProfesor.Text = "Profesor";
            //
            // cmbProfesor
            //
            this.cmbProfesor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProfesor.FormattingEnabled = true;
            this.cmbProfesor.Location = new System.Drawing.Point(110, 57);
            this.cmbProfesor.Name = "cmbProfesor";
            this.cmbProfesor.Size = new System.Drawing.Size(170, 21);
            this.cmbProfesor.TabIndex = 3;
            //
            // lblCDia
            //
            this.lblCDia.AutoSize = true;
            this.lblCDia.Location = new System.Drawing.Point(16, 90);
            this.lblCDia.Name = "lblCDia";
            this.lblCDia.Size = new System.Drawing.Size(30, 13);
            this.lblCDia.TabIndex = 4;
            this.lblCDia.Text = "Día";
            //
            // cmbDia
            //
            this.cmbDia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDia.FormattingEnabled = true;
            this.cmbDia.Items.AddRange(new object[] {
            "LUNES",
            "MARTES",
            "MIÉRCOLES",
            "JUEVES",
            "VIERNES"});
            this.cmbDia.Location = new System.Drawing.Point(110, 87);
            this.cmbDia.Name = "cmbDia";
            this.cmbDia.Size = new System.Drawing.Size(170, 21);
            this.cmbDia.TabIndex = 5;
            //
            // lblCHorario
            //
            this.lblCHorario.AutoSize = true;
            this.lblCHorario.Location = new System.Drawing.Point(300, 30);
            this.lblCHorario.Name = "lblCHorario";
            this.lblCHorario.Size = new System.Drawing.Size(46, 13);
            this.lblCHorario.TabIndex = 6;
            this.lblCHorario.Text = "Horario";
            //
            // txtHorario
            //
            this.txtHorario.Location = new System.Drawing.Point(380, 27);
            this.txtHorario.Name = "txtHorario";
            this.txtHorario.Size = new System.Drawing.Size(140, 20);
            this.txtHorario.TabIndex = 7;
            //
            // lblCGrupo
            //
            this.lblCGrupo.AutoSize = true;
            this.lblCGrupo.Location = new System.Drawing.Point(300, 60);
            this.lblCGrupo.Name = "lblCGrupo";
            this.lblCGrupo.Size = new System.Drawing.Size(36, 13);
            this.lblCGrupo.TabIndex = 8;
            this.lblCGrupo.Text = "Grupo";
            //
            // txtGrupo
            //
            this.txtGrupo.Location = new System.Drawing.Point(380, 57);
            this.txtGrupo.Name = "txtGrupo";
            this.txtGrupo.Size = new System.Drawing.Size(140, 20);
            this.txtGrupo.TabIndex = 9;
            //
            // lblCAnio
            //
            this.lblCAnio.AutoSize = true;
            this.lblCAnio.Location = new System.Drawing.Point(300, 90);
            this.lblCAnio.Name = "lblCAnio";
            this.lblCAnio.Size = new System.Drawing.Size(26, 13);
            this.lblCAnio.TabIndex = 10;
            this.lblCAnio.Text = "Año";
            //
            // nudAnioLectivo
            //
            this.nudAnioLectivo.Location = new System.Drawing.Point(380, 87);
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
            this.nudAnioLectivo.Size = new System.Drawing.Size(120, 20);
            this.nudAnioLectivo.TabIndex = 11;
            this.nudAnioLectivo.Value = new decimal(new int[] {
            2026,
            0,
            0,
            0});
            //
            // btnGuardar
            //
            this.btnGuardar.Location = new System.Drawing.Point(140, 140);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(105, 32);
            this.btnGuardar.TabIndex = 12;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            //
            // btnLimpiarCrear
            //
            this.btnLimpiarCrear.Location = new System.Drawing.Point(260, 140);
            this.btnLimpiarCrear.Name = "btnLimpiarCrear";
            this.btnLimpiarCrear.Size = new System.Drawing.Size(105, 32);
            this.btnLimpiarCrear.TabIndex = 13;
            this.btnLimpiarCrear.Text = "Limpiar";
            this.btnLimpiarCrear.UseVisualStyleBackColor = true;
            this.btnLimpiarCrear.Click += new System.EventHandler(this.btnLimpiarCrear_Click);
            //
            // grpModificar
            //
            this.grpModificar.Controls.Add(this.btnLimpiarEditar);
            this.grpModificar.Controls.Add(this.btnEliminar);
            this.grpModificar.Controls.Add(this.btnModificar);
            this.grpModificar.Controls.Add(this.nudEditAnio);
            this.grpModificar.Controls.Add(this.lblEditAnio);
            this.grpModificar.Controls.Add(this.txtEditGrupo);
            this.grpModificar.Controls.Add(this.lblEditGrupo);
            this.grpModificar.Controls.Add(this.txtEditHorario);
            this.grpModificar.Controls.Add(this.lblEditHorario);
            this.grpModificar.Controls.Add(this.cmbEditDia);
            this.grpModificar.Controls.Add(this.lblEditDia);
            this.grpModificar.Controls.Add(this.cmbEditProfesor);
            this.grpModificar.Controls.Add(this.lblEditProfesor);
            this.grpModificar.Controls.Add(this.cmbEditMateria);
            this.grpModificar.Controls.Add(this.lblEditMateria);
            this.grpModificar.Controls.Add(this.lblEditando);
            this.grpModificar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpModificar.Location = new System.Drawing.Point(0, 190);
            this.grpModificar.Name = "grpModificar";
            this.grpModificar.Size = new System.Drawing.Size(560, 410);
            this.grpModificar.TabIndex = 1;
            this.grpModificar.TabStop = false;
            this.grpModificar.Text = "2. Modificar dictado";
            //
            // lblEditando
            //
            this.lblEditando.AutoSize = true;
            this.lblEditando.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEditando.Location = new System.Drawing.Point(16, 26);
            this.lblEditando.Name = "lblEditando";
            this.lblEditando.Size = new System.Drawing.Size(242, 17);
            this.lblEditando.TabIndex = 0;
            this.lblEditando.Text = "Editando: (seleccione de la lista)";
            //
            // lblEditMateria
            //
            this.lblEditMateria.AutoSize = true;
            this.lblEditMateria.Location = new System.Drawing.Point(16, 56);
            this.lblEditMateria.Name = "lblEditMateria";
            this.lblEditMateria.Size = new System.Drawing.Size(44, 13);
            this.lblEditMateria.TabIndex = 1;
            this.lblEditMateria.Text = "Materia";
            //
            // cmbEditMateria
            //
            this.cmbEditMateria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEditMateria.FormattingEnabled = true;
            this.cmbEditMateria.Location = new System.Drawing.Point(110, 53);
            this.cmbEditMateria.Name = "cmbEditMateria";
            this.cmbEditMateria.Size = new System.Drawing.Size(170, 21);
            this.cmbEditMateria.TabIndex = 2;
            //
            // lblEditProfesor
            //
            this.lblEditProfesor.AutoSize = true;
            this.lblEditProfesor.Location = new System.Drawing.Point(16, 86);
            this.lblEditProfesor.Name = "lblEditProfesor";
            this.lblEditProfesor.Size = new System.Drawing.Size(46, 13);
            this.lblEditProfesor.TabIndex = 3;
            this.lblEditProfesor.Text = "Profesor";
            //
            // cmbEditProfesor
            //
            this.cmbEditProfesor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEditProfesor.FormattingEnabled = true;
            this.cmbEditProfesor.Location = new System.Drawing.Point(110, 83);
            this.cmbEditProfesor.Name = "cmbEditProfesor";
            this.cmbEditProfesor.Size = new System.Drawing.Size(170, 21);
            this.cmbEditProfesor.TabIndex = 4;
            //
            // lblEditDia
            //
            this.lblEditDia.AutoSize = true;
            this.lblEditDia.Location = new System.Drawing.Point(16, 116);
            this.lblEditDia.Name = "lblEditDia";
            this.lblEditDia.Size = new System.Drawing.Size(30, 13);
            this.lblEditDia.TabIndex = 5;
            this.lblEditDia.Text = "Día";
            //
            // cmbEditDia
            //
            this.cmbEditDia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEditDia.FormattingEnabled = true;
            this.cmbEditDia.Items.AddRange(new object[] {
            "LUNES",
            "MARTES",
            "MIÉRCOLES",
            "JUEVES",
            "VIERNES"});
            this.cmbEditDia.Location = new System.Drawing.Point(110, 113);
            this.cmbEditDia.Name = "cmbEditDia";
            this.cmbEditDia.Size = new System.Drawing.Size(170, 21);
            this.cmbEditDia.TabIndex = 6;
            //
            // lblEditHorario
            //
            this.lblEditHorario.AutoSize = true;
            this.lblEditHorario.Location = new System.Drawing.Point(300, 56);
            this.lblEditHorario.Name = "lblEditHorario";
            this.lblEditHorario.Size = new System.Drawing.Size(46, 13);
            this.lblEditHorario.TabIndex = 7;
            this.lblEditHorario.Text = "Horario";
            //
            // txtEditHorario
            //
            this.txtEditHorario.Location = new System.Drawing.Point(380, 53);
            this.txtEditHorario.Name = "txtEditHorario";
            this.txtEditHorario.Size = new System.Drawing.Size(140, 20);
            this.txtEditHorario.TabIndex = 8;
            //
            // lblEditGrupo
            //
            this.lblEditGrupo.AutoSize = true;
            this.lblEditGrupo.Location = new System.Drawing.Point(300, 86);
            this.lblEditGrupo.Name = "lblEditGrupo";
            this.lblEditGrupo.Size = new System.Drawing.Size(36, 13);
            this.lblEditGrupo.TabIndex = 9;
            this.lblEditGrupo.Text = "Grupo";
            //
            // txtEditGrupo
            //
            this.txtEditGrupo.Location = new System.Drawing.Point(380, 83);
            this.txtEditGrupo.Name = "txtEditGrupo";
            this.txtEditGrupo.Size = new System.Drawing.Size(140, 20);
            this.txtEditGrupo.TabIndex = 10;
            //
            // lblEditAnio
            //
            this.lblEditAnio.AutoSize = true;
            this.lblEditAnio.Location = new System.Drawing.Point(300, 116);
            this.lblEditAnio.Name = "lblEditAnio";
            this.lblEditAnio.Size = new System.Drawing.Size(26, 13);
            this.lblEditAnio.TabIndex = 11;
            this.lblEditAnio.Text = "Año";
            //
            // nudEditAnio
            //
            this.nudEditAnio.Location = new System.Drawing.Point(380, 113);
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
            this.nudEditAnio.Size = new System.Drawing.Size(120, 20);
            this.nudEditAnio.TabIndex = 12;
            this.nudEditAnio.Value = new decimal(new int[] {
            2026,
            0,
            0,
            0});
            //
            // btnModificar
            //
            this.btnModificar.Location = new System.Drawing.Point(40, 166);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(105, 32);
            this.btnModificar.TabIndex = 13;
            this.btnModificar.Text = "Modificar";
            this.btnModificar.UseVisualStyleBackColor = true;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            //
            // btnEliminar
            //
            this.btnEliminar.Location = new System.Drawing.Point(160, 166);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(105, 32);
            this.btnEliminar.TabIndex = 14;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            //
            // btnLimpiarEditar
            //
            this.btnLimpiarEditar.Location = new System.Drawing.Point(280, 166);
            this.btnLimpiarEditar.Name = "btnLimpiarEditar";
            this.btnLimpiarEditar.Size = new System.Drawing.Size(105, 32);
            this.btnLimpiarEditar.TabIndex = 15;
            this.btnLimpiarEditar.Text = "Limpiar";
            this.btnLimpiarEditar.UseVisualStyleBackColor = true;
            this.btnLimpiarEditar.Click += new System.EventHandler(this.btnLimpiarEditar_Click);
            //
            // pnlBusqueda
            //
            this.pnlBusqueda.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlBusqueda.Controls.Add(this.dgvDictados);
            this.pnlBusqueda.Controls.Add(this.pnlTopBusqueda);
            this.pnlBusqueda.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBusqueda.Name = "pnlBusqueda";
            this.pnlBusqueda.TabIndex = 2;
            //
            // pnlTopBusqueda
            //
            this.pnlTopBusqueda.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlTopBusqueda.Controls.Add(this.lblTBuscar);
            this.pnlTopBusqueda.Controls.Add(this.txtBuscar);
            this.pnlTopBusqueda.Controls.Add(this.btnBuscar);
            this.pnlTopBusqueda.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopBusqueda.Location = new System.Drawing.Point(0, 0);
            this.pnlTopBusqueda.Name = "pnlTopBusqueda";
            this.pnlTopBusqueda.Size = new System.Drawing.Size(300, 78);
            this.pnlTopBusqueda.TabIndex = 0;
            //
            // lblTBuscar
            //
            this.lblTBuscar.AutoSize = true;
            this.lblTBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTBuscar.Location = new System.Drawing.Point(12, 10);
            this.lblTBuscar.Name = "lblTBuscar";
            this.lblTBuscar.Size = new System.Drawing.Size(107, 15);
            this.lblTBuscar.TabIndex = 0;
            this.lblTBuscar.Text = "Buscar dictado";
            //
            // txtBuscar
            //
            this.txtBuscar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBuscar.Location = new System.Drawing.Point(12, 34);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(182, 20);
            this.txtBuscar.TabIndex = 1;
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);
            //
            // btnBuscar
            //
            this.btnBuscar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBuscar.Location = new System.Drawing.Point(200, 32);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(76, 24);
            this.btnBuscar.TabIndex = 2;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            //
            // dgvDictados
            //
            this.dgvDictados.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvDictados.BackgroundColor = System.Drawing.Color.White;
            this.dgvDictados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDictados.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDictados.Location = new System.Drawing.Point(0, 78);
            this.dgvDictados.Name = "dgvDictados";
            this.dgvDictados.Size = new System.Drawing.Size(300, 522);
            this.dgvDictados.TabIndex = 1;
            this.dgvDictados.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDictados_CellClick);
            //
            // tlpBase
            //
            this.tlpBase.ColumnCount = 2;
            this.tlpBase.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 68F));
            this.tlpBase.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32F));
            this.tlpBase.Controls.Add(this.pnlIzquierda, 0, 0);
            this.tlpBase.Controls.Add(this.pnlBusqueda, 1, 0);
            this.tlpBase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpBase.Location = new System.Drawing.Point(0, 0);
            this.tlpBase.Name = "tlpBase";
            this.tlpBase.RowCount = 1;
            this.tlpBase.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpBase.Size = new System.Drawing.Size(860, 600);
            this.tlpBase.TabIndex = 3;
            //
            // pnlIzquierda
            //
            this.pnlIzquierda.Controls.Add(this.grpModificar);
            this.pnlIzquierda.Controls.Add(this.grpCrear);
            this.pnlIzquierda.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlIzquierda.Location = new System.Drawing.Point(3, 3);
            this.pnlIzquierda.Name = "pnlIzquierda";
            this.pnlIzquierda.Size = new System.Drawing.Size(578, 594);
            this.pnlIzquierda.TabIndex = 0;
            //
            // FrmDictados
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(860, 600);
            this.MinimumSize = new System.Drawing.Size(800, 520);
            this.Controls.Add(this.tlpBase);
            this.Name = "FrmDictados";
            this.Text = "Dictados";
            this.Load += new System.EventHandler(this.FrmDictados_Load);
            this.grpCrear.ResumeLayout(false);
            this.grpCrear.PerformLayout();
            this.grpModificar.ResumeLayout(false);
            this.grpModificar.PerformLayout();
            this.pnlBusqueda.ResumeLayout(false);
            this.pnlTopBusqueda.ResumeLayout(false);
            this.pnlTopBusqueda.PerformLayout();
            this.tlpBase.ResumeLayout(false);
            this.pnlIzquierda.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDictados)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAnioLectivo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEditAnio)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpCrear;
        private System.Windows.Forms.Label lblCMateria;
        private System.Windows.Forms.ComboBox cmbMateria;
        private System.Windows.Forms.Label lblCProfesor;
        private System.Windows.Forms.ComboBox cmbProfesor;
        private System.Windows.Forms.Label lblCDia;
        private System.Windows.Forms.ComboBox cmbDia;
        private System.Windows.Forms.Label lblCHorario;
        private System.Windows.Forms.TextBox txtHorario;
        private System.Windows.Forms.Label lblCGrupo;
        private System.Windows.Forms.TextBox txtGrupo;
        private System.Windows.Forms.Label lblCAnio;
        private System.Windows.Forms.NumericUpDown nudAnioLectivo;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnLimpiarCrear;
        private System.Windows.Forms.GroupBox grpModificar;
        private System.Windows.Forms.Label lblEditando;
        private System.Windows.Forms.Label lblEditMateria;
        private System.Windows.Forms.ComboBox cmbEditMateria;
        private System.Windows.Forms.Label lblEditProfesor;
        private System.Windows.Forms.ComboBox cmbEditProfesor;
        private System.Windows.Forms.Label lblEditDia;
        private System.Windows.Forms.ComboBox cmbEditDia;
        private System.Windows.Forms.Label lblEditHorario;
        private System.Windows.Forms.TextBox txtEditHorario;
        private System.Windows.Forms.Label lblEditGrupo;
        private System.Windows.Forms.TextBox txtEditGrupo;
        private System.Windows.Forms.Label lblEditAnio;
        private System.Windows.Forms.NumericUpDown nudEditAnio;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnLimpiarEditar;
        private System.Windows.Forms.Panel pnlBusqueda;
        private System.Windows.Forms.Panel pnlTopBusqueda;
        private System.Windows.Forms.Label lblTBuscar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.DataGridView dgvDictados;
        private System.Windows.Forms.TableLayoutPanel tlpBase;
        private System.Windows.Forms.Panel pnlIzquierda;
    }
}
