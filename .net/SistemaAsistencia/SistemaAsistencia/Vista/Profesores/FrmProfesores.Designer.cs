namespace SistemaAsistencia.Vista.Profesores
{
    partial class FrmProfesores
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
            this.ctrlTelCrear = new SistemaAsistencia.Vista.Comun.CtrlTelefono();
            this.lblCTelefono = new System.Windows.Forms.Label();
            this.txtDni = new System.Windows.Forms.TextBox();
            this.lblCDni = new System.Windows.Forms.Label();
            this.txtCorreo = new System.Windows.Forms.TextBox();
            this.lblCCorreo = new System.Windows.Forms.Label();
            this.txtLegajo = new System.Windows.Forms.TextBox();
            this.lblCLegajo = new System.Windows.Forms.Label();
            this.txtApellido = new System.Windows.Forms.TextBox();
            this.lblCApellido = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblCNombre = new System.Windows.Forms.Label();
            this.grpModificar = new System.Windows.Forms.GroupBox();
            this.btnLimpiarEditar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.ctrlTelEdit = new SistemaAsistencia.Vista.Comun.CtrlTelefono();
            this.lblEditTelefono = new System.Windows.Forms.Label();
            this.txtEditDni = new System.Windows.Forms.TextBox();
            this.lblEditDni = new System.Windows.Forms.Label();
            this.txtEditCorreo = new System.Windows.Forms.TextBox();
            this.lblEditCorreo = new System.Windows.Forms.Label();
            this.txtEditLegajo = new System.Windows.Forms.TextBox();
            this.lblEditLegajo = new System.Windows.Forms.Label();
            this.txtEditApellido = new System.Windows.Forms.TextBox();
            this.lblEditApellido = new System.Windows.Forms.Label();
            this.txtEditNombre = new System.Windows.Forms.TextBox();
            this.lblEditNombre = new System.Windows.Forms.Label();
            this.lblEditando = new System.Windows.Forms.Label();
            this.pnlBusqueda = new System.Windows.Forms.Panel();
            this.dgvProfesores = new System.Windows.Forms.DataGridView();
            this.pnlTopBusqueda = new System.Windows.Forms.Panel();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.lblTBuscar = new System.Windows.Forms.Label();
            this.tlpBase = new System.Windows.Forms.TableLayoutPanel();
            this.pnlIzquierda = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProfesores)).BeginInit();
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
            this.grpCrear.Controls.Add(this.ctrlTelCrear);
            this.grpCrear.Controls.Add(this.lblCTelefono);
            this.grpCrear.Controls.Add(this.txtDni);
            this.grpCrear.Controls.Add(this.lblCDni);
            this.grpCrear.Controls.Add(this.txtCorreo);
            this.grpCrear.Controls.Add(this.lblCCorreo);
            this.grpCrear.Controls.Add(this.txtLegajo);
            this.grpCrear.Controls.Add(this.lblCLegajo);
            this.grpCrear.Controls.Add(this.txtApellido);
            this.grpCrear.Controls.Add(this.lblCApellido);
            this.grpCrear.Controls.Add(this.txtNombre);
            this.grpCrear.Controls.Add(this.lblCNombre);
            this.grpCrear.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpCrear.Location = new System.Drawing.Point(0, 0);
            this.grpCrear.Name = "grpCrear";
            this.grpCrear.Size = new System.Drawing.Size(560, 255);
            this.grpCrear.TabIndex = 0;
            this.grpCrear.TabStop = false;
            this.grpCrear.Text = "1. Crear profesor";
            //
            // lblCNombre
            //
            this.lblCNombre.AutoSize = true;
            this.lblCNombre.Location = new System.Drawing.Point(16, 28);
            this.lblCNombre.Name = "lblCNombre";
            this.lblCNombre.Size = new System.Drawing.Size(44, 13);
            this.lblCNombre.TabIndex = 0;
            this.lblCNombre.Text = "Nombre";
            //
            // txtNombre
            //
            this.txtNombre.Location = new System.Drawing.Point(90, 25);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(180, 20);
            this.txtNombre.TabIndex = 1;
            //
            // lblCApellido
            //
            this.lblCApellido.AutoSize = true;
            this.lblCApellido.Location = new System.Drawing.Point(16, 58);
            this.lblCApellido.Name = "lblCApellido";
            this.lblCApellido.Size = new System.Drawing.Size(44, 13);
            this.lblCApellido.TabIndex = 2;
            this.lblCApellido.Text = "Apellido";
            //
            // txtApellido
            //
            this.txtApellido.Location = new System.Drawing.Point(90, 55);
            this.txtApellido.Name = "txtApellido";
            this.txtApellido.Size = new System.Drawing.Size(180, 20);
            this.txtApellido.TabIndex = 3;
            //
            // lblCDni
            //
            this.lblCDni.AutoSize = true;
            this.lblCDni.Location = new System.Drawing.Point(16, 88);
            this.lblCDni.Name = "lblCDni";
            this.lblCDni.Size = new System.Drawing.Size(26, 13);
            this.lblCDni.TabIndex = 4;
            this.lblCDni.Text = "DNI";
            //
            // txtDni
            //
            this.txtDni.Location = new System.Drawing.Point(90, 85);
            this.txtDni.MaxLength = 8;
            this.txtDni.Name = "txtDni";
            this.txtDni.Size = new System.Drawing.Size(180, 20);
            this.txtDni.TabIndex = 5;
            //
            // lblCLegajo
            //
            this.lblCLegajo.AutoSize = true;
            this.lblCLegajo.Location = new System.Drawing.Point(16, 118);
            this.lblCLegajo.Name = "lblCLegajo";
            this.lblCLegajo.Size = new System.Drawing.Size(39, 13);
            this.lblCLegajo.TabIndex = 6;
            this.lblCLegajo.Text = "Legajo";
            //
            // txtLegajo
            //
            this.txtLegajo.Location = new System.Drawing.Point(90, 115);
            this.txtLegajo.MaxLength = 10;
            this.txtLegajo.Name = "txtLegajo";
            this.txtLegajo.Size = new System.Drawing.Size(180, 20);
            this.txtLegajo.TabIndex = 7;
            //
            // lblCCorreo
            //
            this.lblCCorreo.AutoSize = true;
            this.lblCCorreo.Location = new System.Drawing.Point(16, 148);
            this.lblCCorreo.Name = "lblCCorreo";
            this.lblCCorreo.Size = new System.Drawing.Size(38, 13);
            this.lblCCorreo.TabIndex = 8;
            this.lblCCorreo.Text = "Correo";
            //
            // txtCorreo
            //
            this.txtCorreo.Location = new System.Drawing.Point(90, 145);
            this.txtCorreo.Name = "txtCorreo";
            this.txtCorreo.Size = new System.Drawing.Size(180, 20);
            this.txtCorreo.TabIndex = 9;
            //
            // lblCTelefono
            //
            this.lblCTelefono.AutoSize = true;
            this.lblCTelefono.Location = new System.Drawing.Point(16, 178);
            this.lblCTelefono.Name = "lblCTelefono";
            this.lblCTelefono.Size = new System.Drawing.Size(49, 13);
            this.lblCTelefono.TabIndex = 10;
            this.lblCTelefono.Text = "Teléfono";
            //
            // ctrlTelCrear
            //
            this.ctrlTelCrear.Location = new System.Drawing.Point(90, 175);
            this.ctrlTelCrear.Name = "ctrlTelCrear";
            this.ctrlTelCrear.Size = new System.Drawing.Size(180, 22);
            this.ctrlTelCrear.TabIndex = 11;
            //
            // btnGuardar
            //
            this.btnGuardar.Location = new System.Drawing.Point(90, 210);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(105, 32);
            this.btnGuardar.TabIndex = 12;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            //
            // btnLimpiarCrear
            //
            this.btnLimpiarCrear.Location = new System.Drawing.Point(210, 210);
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
            this.grpModificar.Controls.Add(this.ctrlTelEdit);
            this.grpModificar.Controls.Add(this.lblEditTelefono);
            this.grpModificar.Controls.Add(this.txtEditDni);
            this.grpModificar.Controls.Add(this.lblEditDni);
            this.grpModificar.Controls.Add(this.txtEditCorreo);
            this.grpModificar.Controls.Add(this.lblEditCorreo);
            this.grpModificar.Controls.Add(this.txtEditLegajo);
            this.grpModificar.Controls.Add(this.lblEditLegajo);
            this.grpModificar.Controls.Add(this.txtEditApellido);
            this.grpModificar.Controls.Add(this.lblEditApellido);
            this.grpModificar.Controls.Add(this.txtEditNombre);
            this.grpModificar.Controls.Add(this.lblEditNombre);
            this.grpModificar.Controls.Add(this.lblEditando);
            this.grpModificar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpModificar.Location = new System.Drawing.Point(0, 255);
            this.grpModificar.Name = "grpModificar";
            this.grpModificar.Size = new System.Drawing.Size(560, 375);
            this.grpModificar.TabIndex = 1;
            this.grpModificar.TabStop = false;
            this.grpModificar.Text = "2. Modificar profesor";
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
            // lblEditNombre
            //
            this.lblEditNombre.AutoSize = true;
            this.lblEditNombre.Location = new System.Drawing.Point(16, 56);
            this.lblEditNombre.Name = "lblEditNombre";
            this.lblEditNombre.Size = new System.Drawing.Size(44, 13);
            this.lblEditNombre.TabIndex = 1;
            this.lblEditNombre.Text = "Nombre";
            //
            // txtEditNombre
            //
            this.txtEditNombre.Location = new System.Drawing.Point(90, 53);
            this.txtEditNombre.Name = "txtEditNombre";
            this.txtEditNombre.Size = new System.Drawing.Size(180, 20);
            this.txtEditNombre.TabIndex = 2;
            //
            // lblEditApellido
            //
            this.lblEditApellido.AutoSize = true;
            this.lblEditApellido.Location = new System.Drawing.Point(16, 86);
            this.lblEditApellido.Name = "lblEditApellido";
            this.lblEditApellido.Size = new System.Drawing.Size(44, 13);
            this.lblEditApellido.TabIndex = 3;
            this.lblEditApellido.Text = "Apellido";
            //
            // txtEditApellido
            //
            this.txtEditApellido.Location = new System.Drawing.Point(90, 83);
            this.txtEditApellido.Name = "txtEditApellido";
            this.txtEditApellido.Size = new System.Drawing.Size(180, 20);
            this.txtEditApellido.TabIndex = 4;
            //
            // lblEditDni
            //
            this.lblEditDni.AutoSize = true;
            this.lblEditDni.Location = new System.Drawing.Point(16, 116);
            this.lblEditDni.Name = "lblEditDni";
            this.lblEditDni.Size = new System.Drawing.Size(26, 13);
            this.lblEditDni.TabIndex = 5;
            this.lblEditDni.Text = "DNI";
            //
            // txtEditDni
            //
            this.txtEditDni.Location = new System.Drawing.Point(90, 113);
            this.txtEditDni.MaxLength = 8;
            this.txtEditDni.Name = "txtEditDni";
            this.txtEditDni.Size = new System.Drawing.Size(180, 20);
            this.txtEditDni.TabIndex = 6;
            //
            // lblEditLegajo
            //
            this.lblEditLegajo.AutoSize = true;
            this.lblEditLegajo.Location = new System.Drawing.Point(16, 146);
            this.lblEditLegajo.Name = "lblEditLegajo";
            this.lblEditLegajo.Size = new System.Drawing.Size(39, 13);
            this.lblEditLegajo.TabIndex = 7;
            this.lblEditLegajo.Text = "Legajo";
            //
            // txtEditLegajo
            //
            this.txtEditLegajo.Location = new System.Drawing.Point(90, 143);
            this.txtEditLegajo.MaxLength = 10;
            this.txtEditLegajo.Name = "txtEditLegajo";
            this.txtEditLegajo.Size = new System.Drawing.Size(180, 20);
            this.txtEditLegajo.TabIndex = 8;
            //
            // lblEditCorreo
            //
            this.lblEditCorreo.AutoSize = true;
            this.lblEditCorreo.Location = new System.Drawing.Point(16, 176);
            this.lblEditCorreo.Name = "lblEditCorreo";
            this.lblEditCorreo.Size = new System.Drawing.Size(38, 13);
            this.lblEditCorreo.TabIndex = 9;
            this.lblEditCorreo.Text = "Correo";
            //
            // txtEditCorreo
            //
            this.txtEditCorreo.Location = new System.Drawing.Point(90, 173);
            this.txtEditCorreo.Name = "txtEditCorreo";
            this.txtEditCorreo.Size = new System.Drawing.Size(180, 20);
            this.txtEditCorreo.TabIndex = 10;
            //
            // lblEditTelefono
            //
            this.lblEditTelefono.AutoSize = true;
            this.lblEditTelefono.Location = new System.Drawing.Point(16, 206);
            this.lblEditTelefono.Name = "lblEditTelefono";
            this.lblEditTelefono.Size = new System.Drawing.Size(49, 13);
            this.lblEditTelefono.TabIndex = 11;
            this.lblEditTelefono.Text = "Teléfono";
            //
            // ctrlTelEdit
            //
            this.ctrlTelEdit.Location = new System.Drawing.Point(90, 203);
            this.ctrlTelEdit.Name = "ctrlTelEdit";
            this.ctrlTelEdit.Size = new System.Drawing.Size(180, 22);
            this.ctrlTelEdit.TabIndex = 12;
            //
            // btnModificar
            //
            this.btnModificar.Location = new System.Drawing.Point(40, 238);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(105, 32);
            this.btnModificar.TabIndex = 13;
            this.btnModificar.Text = "Modificar";
            this.btnModificar.UseVisualStyleBackColor = true;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            //
            // btnEliminar
            //
            this.btnEliminar.Location = new System.Drawing.Point(160, 238);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(105, 32);
            this.btnEliminar.TabIndex = 14;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            //
            // btnLimpiarEditar
            //
            this.btnLimpiarEditar.Location = new System.Drawing.Point(280, 238);
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
            this.pnlBusqueda.Controls.Add(this.dgvProfesores);
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
            this.lblTBuscar.Size = new System.Drawing.Size(104, 15);
            this.lblTBuscar.TabIndex = 0;
            this.lblTBuscar.Text = "Buscar profesor";
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
            // dgvProfesores
            //
            this.dgvProfesores.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvProfesores.BackgroundColor = System.Drawing.Color.White;
            this.dgvProfesores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProfesores.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvProfesores.Location = new System.Drawing.Point(0, 78);
            this.dgvProfesores.Name = "dgvProfesores";
            this.dgvProfesores.Size = new System.Drawing.Size(300, 522);
            this.dgvProfesores.TabIndex = 1;
            this.dgvProfesores.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProfesores_CellClick);
            //
            // tlpBase
            //
            this.tlpBase.ColumnCount = 2;
            this.tlpBase.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tlpBase.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
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
            // FrmProfesores
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(860, 600);
            this.MinimumSize = new System.Drawing.Size(800, 520);
            this.Controls.Add(this.tlpBase);
            this.Name = "FrmProfesores";
            this.Text = "Profesores";
            this.Load += new System.EventHandler(this.FrmProfesores_Load);
            this.grpCrear.ResumeLayout(false);
            this.grpCrear.PerformLayout();
            this.grpModificar.ResumeLayout(false);
            this.grpModificar.PerformLayout();
            this.pnlBusqueda.ResumeLayout(false);
            this.pnlTopBusqueda.ResumeLayout(false);
            this.pnlTopBusqueda.PerformLayout();
            this.tlpBase.ResumeLayout(false);
            this.pnlIzquierda.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProfesores)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpCrear;
        private System.Windows.Forms.Label lblCNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblCApellido;
        private System.Windows.Forms.TextBox txtApellido;
        private System.Windows.Forms.Label lblCLegajo;
        private System.Windows.Forms.TextBox txtLegajo;
        private System.Windows.Forms.Label lblCCorreo;
        private System.Windows.Forms.TextBox txtCorreo;
        private System.Windows.Forms.Label lblCTelefono;
        private SistemaAsistencia.Vista.Comun.CtrlTelefono ctrlTelCrear;
        private System.Windows.Forms.Label lblCDni;
        private System.Windows.Forms.TextBox txtDni;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnLimpiarCrear;
        private System.Windows.Forms.GroupBox grpModificar;
        private System.Windows.Forms.Label lblEditando;
        private System.Windows.Forms.Label lblEditNombre;
        private System.Windows.Forms.TextBox txtEditNombre;
        private System.Windows.Forms.Label lblEditApellido;
        private System.Windows.Forms.TextBox txtEditApellido;
        private System.Windows.Forms.Label lblEditLegajo;
        private System.Windows.Forms.TextBox txtEditLegajo;
        private System.Windows.Forms.Label lblEditCorreo;
        private System.Windows.Forms.TextBox txtEditCorreo;
        private System.Windows.Forms.Label lblEditTelefono;
        private SistemaAsistencia.Vista.Comun.CtrlTelefono ctrlTelEdit;
        private System.Windows.Forms.Label lblEditDni;
        private System.Windows.Forms.TextBox txtEditDni;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnLimpiarEditar;
        private System.Windows.Forms.Panel pnlBusqueda;
        private System.Windows.Forms.Panel pnlTopBusqueda;
        private System.Windows.Forms.Label lblTBuscar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.DataGridView dgvProfesores;
        private System.Windows.Forms.TableLayoutPanel tlpBase;
        private System.Windows.Forms.Panel pnlIzquierda;
    }
}
