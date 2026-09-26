namespace SistemaAsistencia.Vista.Materias
{
    partial class FrmMaterias
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
            this.nudCargaHoraria = new System.Windows.Forms.NumericUpDown();
            this.nudAnioMateria = new System.Windows.Forms.NumericUpDown();
            this.cmbEspecialidad = new System.Windows.Forms.ComboBox();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblCargaHoraria = new System.Windows.Forms.Label();
            this.lblAnio = new System.Windows.Forms.Label();
            this.lblEspecialidad = new System.Windows.Forms.Label();
            this.lblNombre = new System.Windows.Forms.Label();
            this.grpModificar = new System.Windows.Forms.GroupBox();
            this.btnLimpiarEditar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.nudEditCarga = new System.Windows.Forms.NumericUpDown();
            this.nudEditAnio = new System.Windows.Forms.NumericUpDown();
            this.cmbEditEspecialidad = new System.Windows.Forms.ComboBox();
            this.txtEditNombre = new System.Windows.Forms.TextBox();
            this.lblEditCarga = new System.Windows.Forms.Label();
            this.lblEditAnio = new System.Windows.Forms.Label();
            this.lblEditEspecialidad = new System.Windows.Forms.Label();
            this.lblEditNombre = new System.Windows.Forms.Label();
            this.lblEditando = new System.Windows.Forms.Label();
            this.pnlBusqueda = new System.Windows.Forms.Panel();
            this.dgvMaterias = new System.Windows.Forms.DataGridView();
            this.pnlTopBusqueda = new System.Windows.Forms.Panel();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.lblTBuscar = new System.Windows.Forms.Label();
            this.tlpBase = new System.Windows.Forms.TableLayoutPanel();
            this.pnlIzquierda = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.nudCargaHoraria)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAnioMateria)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEditCarga)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEditAnio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaterias)).BeginInit();
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
            this.grpCrear.Controls.Add(this.nudCargaHoraria);
            this.grpCrear.Controls.Add(this.nudAnioMateria);
            this.grpCrear.Controls.Add(this.cmbEspecialidad);
            this.grpCrear.Controls.Add(this.txtNombre);
            this.grpCrear.Controls.Add(this.lblCargaHoraria);
            this.grpCrear.Controls.Add(this.lblAnio);
            this.grpCrear.Controls.Add(this.lblEspecialidad);
            this.grpCrear.Controls.Add(this.lblNombre);
            this.grpCrear.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpCrear.Location = new System.Drawing.Point(0, 0);
            this.grpCrear.Name = "grpCrear";
            this.grpCrear.Size = new System.Drawing.Size(560, 205);
            this.grpCrear.TabIndex = 0;
            this.grpCrear.TabStop = false;
            this.grpCrear.Text = "1. Crear materia";
            //
            // lblNombre
            //
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(16, 60);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(44, 13);
            this.lblNombre.TabIndex = 0;
            this.lblNombre.Text = "Nombre";
            //
            // txtNombre
            //
            this.txtNombre.Location = new System.Drawing.Point(130, 57);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(180, 20);
            this.txtNombre.TabIndex = 1;
            //
            // lblEspecialidad
            //
            this.lblEspecialidad.AutoSize = true;
            this.lblEspecialidad.Location = new System.Drawing.Point(16, 30);
            this.lblEspecialidad.Name = "lblEspecialidad";
            this.lblEspecialidad.Size = new System.Drawing.Size(67, 13);
            this.lblEspecialidad.TabIndex = 2;
            this.lblEspecialidad.Text = "Especialidad";
            //
            // cmbEspecialidad
            //
            this.cmbEspecialidad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEspecialidad.FormattingEnabled = true;
            this.cmbEspecialidad.Location = new System.Drawing.Point(130, 27);
            this.cmbEspecialidad.Name = "cmbEspecialidad";
            this.cmbEspecialidad.Size = new System.Drawing.Size(180, 21);
            this.cmbEspecialidad.TabIndex = 3;
            //
            // lblAnio
            //
            this.lblAnio.AutoSize = true;
            this.lblAnio.Location = new System.Drawing.Point(16, 120);
            this.lblAnio.Name = "lblAnio";
            this.lblAnio.Size = new System.Drawing.Size(26, 13);
            this.lblAnio.TabIndex = 4;
            this.lblAnio.Text = "Año";
            //
            // nudAnioMateria
            //
            this.nudAnioMateria.Location = new System.Drawing.Point(130, 117);
            this.nudAnioMateria.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudAnioMateria.Name = "nudAnioMateria";
            this.nudAnioMateria.Size = new System.Drawing.Size(120, 20);
            this.nudAnioMateria.TabIndex = 5;
            //
            // lblCargaHoraria
            //
            this.lblCargaHoraria.AutoSize = true;
            this.lblCargaHoraria.Location = new System.Drawing.Point(16, 90);
            this.lblCargaHoraria.Name = "lblCargaHoraria";
            this.lblCargaHoraria.Size = new System.Drawing.Size(73, 13);
            this.lblCargaHoraria.TabIndex = 6;
            this.lblCargaHoraria.Text = "Carga Horaria";
            //
            // nudCargaHoraria
            //
            this.nudCargaHoraria.Location = new System.Drawing.Point(130, 87);
            this.nudCargaHoraria.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudCargaHoraria.Name = "nudCargaHoraria";
            this.nudCargaHoraria.Size = new System.Drawing.Size(120, 20);
            this.nudCargaHoraria.TabIndex = 7;
            //
            // btnGuardar
            //
            this.btnGuardar.Location = new System.Drawing.Point(130, 158);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(105, 32);
            this.btnGuardar.TabIndex = 8;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            //
            // btnLimpiarCrear
            //
            this.btnLimpiarCrear.Location = new System.Drawing.Point(250, 158);
            this.btnLimpiarCrear.Name = "btnLimpiarCrear";
            this.btnLimpiarCrear.Size = new System.Drawing.Size(105, 32);
            this.btnLimpiarCrear.TabIndex = 9;
            this.btnLimpiarCrear.Text = "Limpiar";
            this.btnLimpiarCrear.UseVisualStyleBackColor = true;
            this.btnLimpiarCrear.Click += new System.EventHandler(this.btnLimpiarCrear_Click);
            //
            // grpModificar
            //
            this.grpModificar.Controls.Add(this.btnLimpiarEditar);
            this.grpModificar.Controls.Add(this.btnEliminar);
            this.grpModificar.Controls.Add(this.btnModificar);
            this.grpModificar.Controls.Add(this.nudEditCarga);
            this.grpModificar.Controls.Add(this.nudEditAnio);
            this.grpModificar.Controls.Add(this.cmbEditEspecialidad);
            this.grpModificar.Controls.Add(this.txtEditNombre);
            this.grpModificar.Controls.Add(this.lblEditCarga);
            this.grpModificar.Controls.Add(this.lblEditAnio);
            this.grpModificar.Controls.Add(this.lblEditEspecialidad);
            this.grpModificar.Controls.Add(this.lblEditNombre);
            this.grpModificar.Controls.Add(this.lblEditando);
            this.grpModificar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpModificar.Location = new System.Drawing.Point(0, 205);
            this.grpModificar.Name = "grpModificar";
            this.grpModificar.Size = new System.Drawing.Size(560, 395);
            this.grpModificar.TabIndex = 1;
            this.grpModificar.TabStop = false;
            this.grpModificar.Text = "2. Modificar materia";
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
            this.lblEditNombre.Location = new System.Drawing.Point(16, 88);
            this.lblEditNombre.Name = "lblEditNombre";
            this.lblEditNombre.Size = new System.Drawing.Size(44, 13);
            this.lblEditNombre.TabIndex = 1;
            this.lblEditNombre.Text = "Nombre";
            //
            // txtEditNombre
            //
            this.txtEditNombre.Location = new System.Drawing.Point(130, 85);
            this.txtEditNombre.Name = "txtEditNombre";
            this.txtEditNombre.Size = new System.Drawing.Size(180, 20);
            this.txtEditNombre.TabIndex = 2;
            //
            // lblEditEspecialidad
            //
            this.lblEditEspecialidad.AutoSize = true;
            this.lblEditEspecialidad.Location = new System.Drawing.Point(16, 58);
            this.lblEditEspecialidad.Name = "lblEditEspecialidad";
            this.lblEditEspecialidad.Size = new System.Drawing.Size(67, 13);
            this.lblEditEspecialidad.TabIndex = 3;
            this.lblEditEspecialidad.Text = "Especialidad";
            //
            // cmbEditEspecialidad
            //
            this.cmbEditEspecialidad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEditEspecialidad.FormattingEnabled = true;
            this.cmbEditEspecialidad.Location = new System.Drawing.Point(130, 55);
            this.cmbEditEspecialidad.Name = "cmbEditEspecialidad";
            this.cmbEditEspecialidad.Size = new System.Drawing.Size(180, 21);
            this.cmbEditEspecialidad.TabIndex = 4;
            //
            // lblEditAnio
            //
            this.lblEditAnio.AutoSize = true;
            this.lblEditAnio.Location = new System.Drawing.Point(16, 148);
            this.lblEditAnio.Name = "lblEditAnio";
            this.lblEditAnio.Size = new System.Drawing.Size(26, 13);
            this.lblEditAnio.TabIndex = 5;
            this.lblEditAnio.Text = "Año";
            //
            // nudEditAnio
            //
            this.nudEditAnio.Location = new System.Drawing.Point(130, 145);
            this.nudEditAnio.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudEditAnio.Name = "nudEditAnio";
            this.nudEditAnio.Size = new System.Drawing.Size(120, 20);
            this.nudEditAnio.TabIndex = 6;
            //
            // lblEditCarga
            //
            this.lblEditCarga.AutoSize = true;
            this.lblEditCarga.Location = new System.Drawing.Point(16, 118);
            this.lblEditCarga.Name = "lblEditCarga";
            this.lblEditCarga.Size = new System.Drawing.Size(73, 13);
            this.lblEditCarga.TabIndex = 7;
            this.lblEditCarga.Text = "Carga Horaria";
            //
            // nudEditCarga
            //
            this.nudEditCarga.Location = new System.Drawing.Point(130, 115);
            this.nudEditCarga.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudEditCarga.Name = "nudEditCarga";
            this.nudEditCarga.Size = new System.Drawing.Size(120, 20);
            this.nudEditCarga.TabIndex = 8;
            //
            // btnModificar
            //
            this.btnModificar.Location = new System.Drawing.Point(40, 186);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(105, 32);
            this.btnModificar.TabIndex = 9;
            this.btnModificar.Text = "Modificar";
            this.btnModificar.UseVisualStyleBackColor = true;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            //
            // btnEliminar
            //
            this.btnEliminar.Location = new System.Drawing.Point(160, 186);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(105, 32);
            this.btnEliminar.TabIndex = 10;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            //
            // btnLimpiarEditar
            //
            this.btnLimpiarEditar.Location = new System.Drawing.Point(280, 186);
            this.btnLimpiarEditar.Name = "btnLimpiarEditar";
            this.btnLimpiarEditar.Size = new System.Drawing.Size(105, 32);
            this.btnLimpiarEditar.TabIndex = 11;
            this.btnLimpiarEditar.Text = "Limpiar";
            this.btnLimpiarEditar.UseVisualStyleBackColor = true;
            this.btnLimpiarEditar.Click += new System.EventHandler(this.btnLimpiarEditar_Click);
            //
            // pnlBusqueda
            //
            this.pnlBusqueda.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlBusqueda.Controls.Add(this.dgvMaterias);
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
            this.lblTBuscar.Size = new System.Drawing.Size(106, 15);
            this.lblTBuscar.TabIndex = 0;
            this.lblTBuscar.Text = "Buscar materia";
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
            // dgvMaterias
            //
            this.dgvMaterias.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMaterias.BackgroundColor = System.Drawing.Color.White;
            this.dgvMaterias.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMaterias.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMaterias.Location = new System.Drawing.Point(0, 78);
            this.dgvMaterias.Name = "dgvMaterias";
            this.dgvMaterias.Size = new System.Drawing.Size(300, 522);
            this.dgvMaterias.TabIndex = 1;
            this.dgvMaterias.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMaterias_CellClick);
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
            // FrmMaterias
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(860, 600);
            this.MinimumSize = new System.Drawing.Size(800, 520);
            this.Controls.Add(this.tlpBase);
            this.Name = "FrmMaterias";
            this.Text = "Materias";
            this.Load += new System.EventHandler(this.FrmMaterias_Load);
            this.grpCrear.ResumeLayout(false);
            this.grpCrear.PerformLayout();
            this.grpModificar.ResumeLayout(false);
            this.grpModificar.PerformLayout();
            this.pnlBusqueda.ResumeLayout(false);
            this.pnlTopBusqueda.ResumeLayout(false);
            this.pnlTopBusqueda.PerformLayout();
            this.tlpBase.ResumeLayout(false);
            this.pnlIzquierda.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaterias)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCargaHoraria)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAnioMateria)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEditCarga)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEditAnio)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpCrear;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblEspecialidad;
        private System.Windows.Forms.ComboBox cmbEspecialidad;
        private System.Windows.Forms.Label lblAnio;
        private System.Windows.Forms.NumericUpDown nudAnioMateria;
        private System.Windows.Forms.Label lblCargaHoraria;
        private System.Windows.Forms.NumericUpDown nudCargaHoraria;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnLimpiarCrear;
        private System.Windows.Forms.GroupBox grpModificar;
        private System.Windows.Forms.Label lblEditando;
        private System.Windows.Forms.Label lblEditNombre;
        private System.Windows.Forms.TextBox txtEditNombre;
        private System.Windows.Forms.Label lblEditEspecialidad;
        private System.Windows.Forms.ComboBox cmbEditEspecialidad;
        private System.Windows.Forms.Label lblEditAnio;
        private System.Windows.Forms.NumericUpDown nudEditAnio;
        private System.Windows.Forms.Label lblEditCarga;
        private System.Windows.Forms.NumericUpDown nudEditCarga;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnLimpiarEditar;
        private System.Windows.Forms.Panel pnlBusqueda;
        private System.Windows.Forms.Panel pnlTopBusqueda;
        private System.Windows.Forms.Label lblTBuscar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.DataGridView dgvMaterias;
        private System.Windows.Forms.TableLayoutPanel tlpBase;
        private System.Windows.Forms.Panel pnlIzquierda;
    }
}
