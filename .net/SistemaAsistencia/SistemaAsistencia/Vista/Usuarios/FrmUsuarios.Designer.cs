namespace SistemaAsistencia.Vista.Usuarios
{
    partial class FrmUsuarios
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
            this.chkActivo = new System.Windows.Forms.CheckBox();
            this.cmbRol = new System.Windows.Forms.ComboBox();
            this.lblCRol = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblCPassword = new System.Windows.Forms.Label();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.lblCUsuario = new System.Windows.Forms.Label();
            this.grpModificar = new System.Windows.Forms.GroupBox();
            this.btnLimpiarEditar = new System.Windows.Forms.Button();
            this.btnActivar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.lblPassHint = new System.Windows.Forms.Label();
            this.chkEditActivo = new System.Windows.Forms.CheckBox();
            this.cmbEditRol = new System.Windows.Forms.ComboBox();
            this.lblEditRol = new System.Windows.Forms.Label();
            this.txtEditPassword = new System.Windows.Forms.TextBox();
            this.lblEditPassword = new System.Windows.Forms.Label();
            this.txtEditUsuario = new System.Windows.Forms.TextBox();
            this.lblEditUsuario = new System.Windows.Forms.Label();
            this.lblEditando = new System.Windows.Forms.Label();
            this.pnlBusqueda = new System.Windows.Forms.Panel();
            this.dgvUsuarios = new System.Windows.Forms.DataGridView();
            this.pnlTopBusqueda = new System.Windows.Forms.Panel();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.lblTBUSCAR = new System.Windows.Forms.Label();
            this.tlpBase = new System.Windows.Forms.TableLayoutPanel();
            this.pnlIzquierda = new System.Windows.Forms.Panel();
            this.grpCrear.SuspendLayout();
            this.grpModificar.SuspendLayout();
            this.pnlBusqueda.SuspendLayout();
            this.pnlTopBusqueda.SuspendLayout();
            this.tlpBase.SuspendLayout();
            this.pnlIzquierda.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuarios)).BeginInit();
            this.SuspendLayout();
            //
            // grpCrear
            //
            this.grpCrear.Controls.Add(this.btnLimpiarCrear);
            this.grpCrear.Controls.Add(this.btnGuardar);
            this.grpCrear.Controls.Add(this.chkActivo);
            this.grpCrear.Controls.Add(this.cmbRol);
            this.grpCrear.Controls.Add(this.lblCRol);
            this.grpCrear.Controls.Add(this.txtPassword);
            this.grpCrear.Controls.Add(this.lblCPassword);
            this.grpCrear.Controls.Add(this.txtUsuario);
            this.grpCrear.Controls.Add(this.lblCUsuario);
            this.grpCrear.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpCrear.Location = new System.Drawing.Point(0, 0);
            this.grpCrear.Name = "grpCrear";
            this.grpCrear.Size = new System.Drawing.Size(560, 165);
            this.grpCrear.TabIndex = 0;
            this.grpCrear.TabStop = false;
            this.grpCrear.Text = "1. Crear usuario";
            //
            // lblCUsuario
            //
            this.lblCUsuario.AutoSize = true;
            this.lblCUsuario.Location = new System.Drawing.Point(16, 30);
            this.lblCUsuario.Name = "lblCUsuario";
            this.lblCUsuario.Size = new System.Drawing.Size(43, 13);
            this.lblCUsuario.TabIndex = 0;
            this.lblCUsuario.Text = "Usuario";
            //
            // txtUsuario
            //
            this.txtUsuario.Location = new System.Drawing.Point(130, 27);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(170, 20);
            this.txtUsuario.TabIndex = 1;
            //
            // lblCPassword
            //
            this.lblCPassword.AutoSize = true;
            this.lblCPassword.Location = new System.Drawing.Point(16, 60);
            this.lblCPassword.Name = "lblCPassword";
            this.lblCPassword.Size = new System.Drawing.Size(61, 13);
            this.lblCPassword.TabIndex = 2;
            this.lblCPassword.Text = "Contraseña";
            //
            // txtPassword
            //
            this.txtPassword.Location = new System.Drawing.Point(130, 57);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(170, 20);
            this.txtPassword.TabIndex = 3;
            this.txtPassword.UseSystemPasswordChar = true;
            //
            // lblCRol
            //
            this.lblCRol.AutoSize = true;
            this.lblCRol.Location = new System.Drawing.Point(320, 30);
            this.lblCRol.Name = "lblCRol";
            this.lblCRol.Size = new System.Drawing.Size(23, 13);
            this.lblCRol.TabIndex = 4;
            this.lblCRol.Text = "Rol";
            //
            // cmbRol
            //
            this.cmbRol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRol.FormattingEnabled = true;
            this.cmbRol.Location = new System.Drawing.Point(385, 27);
            this.cmbRol.Name = "cmbRol";
            this.cmbRol.Size = new System.Drawing.Size(150, 21);
            this.cmbRol.TabIndex = 5;
            //
            // chkActivo
            //
            this.chkActivo.AutoSize = true;
            this.chkActivo.Checked = true;
            this.chkActivo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkActivo.Location = new System.Drawing.Point(320, 59);
            this.chkActivo.Name = "chkActivo";
            this.chkActivo.Size = new System.Drawing.Size(56, 17);
            this.chkActivo.TabIndex = 6;
            this.chkActivo.Text = "Activo";
            this.chkActivo.UseVisualStyleBackColor = true;
            //
            // btnGuardar
            //
            this.btnGuardar.Location = new System.Drawing.Point(320, 110);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(100, 32);
            this.btnGuardar.TabIndex = 7;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            //
            // btnLimpiarCrear
            //
            this.btnLimpiarCrear.Location = new System.Drawing.Point(435, 110);
            this.btnLimpiarCrear.Name = "btnLimpiarCrear";
            this.btnLimpiarCrear.Size = new System.Drawing.Size(100, 32);
            this.btnLimpiarCrear.TabIndex = 8;
            this.btnLimpiarCrear.Text = "Limpiar";
            this.btnLimpiarCrear.UseVisualStyleBackColor = true;
            this.btnLimpiarCrear.Click += new System.EventHandler(this.btnLimpiarCrear_Click);
            //
            // grpModificar
            //
            this.grpModificar.Controls.Add(this.lblEditando);
            this.grpModificar.Controls.Add(this.lblEditUsuario);
            this.grpModificar.Controls.Add(this.txtEditUsuario);
            this.grpModificar.Controls.Add(this.lblEditPassword);
            this.grpModificar.Controls.Add(this.txtEditPassword);
            this.grpModificar.Controls.Add(this.lblEditRol);
            this.grpModificar.Controls.Add(this.cmbEditRol);
            this.grpModificar.Controls.Add(this.chkEditActivo);
            this.grpModificar.Controls.Add(this.lblPassHint);
            this.grpModificar.Controls.Add(this.btnModificar);
            this.grpModificar.Controls.Add(this.btnEliminar);
            this.grpModificar.Controls.Add(this.btnActivar);
            this.grpModificar.Controls.Add(this.btnLimpiarEditar);
            this.grpModificar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpModificar.Location = new System.Drawing.Point(0, 165);
            this.grpModificar.Name = "grpModificar";
            this.grpModificar.Size = new System.Drawing.Size(560, 435);
            this.grpModificar.TabIndex = 1;
            this.grpModificar.TabStop = false;
            this.grpModificar.Text = "2. Modificar usuario";
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
            // lblEditUsuario
            //
            this.lblEditUsuario.AutoSize = true;
            this.lblEditUsuario.Location = new System.Drawing.Point(16, 58);
            this.lblEditUsuario.Name = "lblEditUsuario";
            this.lblEditUsuario.Size = new System.Drawing.Size(43, 13);
            this.lblEditUsuario.TabIndex = 1;
            this.lblEditUsuario.Text = "Usuario";
            //
            // txtEditUsuario
            //
            this.txtEditUsuario.Location = new System.Drawing.Point(130, 55);
            this.txtEditUsuario.Name = "txtEditUsuario";
            this.txtEditUsuario.Size = new System.Drawing.Size(170, 20);
            this.txtEditUsuario.TabIndex = 2;
            //
            // lblEditPassword
            //
            this.lblEditPassword.AutoSize = true;
            this.lblEditPassword.Location = new System.Drawing.Point(16, 88);
            this.lblEditPassword.Name = "lblEditPassword";
            this.lblEditPassword.Size = new System.Drawing.Size(61, 13);
            this.lblEditPassword.TabIndex = 3;
            this.lblEditPassword.Text = "Contraseña";
            //
            // txtEditPassword
            //
            this.txtEditPassword.Location = new System.Drawing.Point(130, 85);
            this.txtEditPassword.Name = "txtEditPassword";
            this.txtEditPassword.Size = new System.Drawing.Size(170, 20);
            this.txtEditPassword.TabIndex = 4;
            this.txtEditPassword.UseSystemPasswordChar = true;
            //
            // lblEditRol
            //
            this.lblEditRol.AutoSize = true;
            this.lblEditRol.Location = new System.Drawing.Point(320, 58);
            this.lblEditRol.Name = "lblEditRol";
            this.lblEditRol.Size = new System.Drawing.Size(23, 13);
            this.lblEditRol.TabIndex = 5;
            this.lblEditRol.Text = "Rol";
            //
            // cmbEditRol
            //
            this.cmbEditRol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEditRol.FormattingEnabled = true;
            this.cmbEditRol.Location = new System.Drawing.Point(385, 55);
            this.cmbEditRol.Name = "cmbEditRol";
            this.cmbEditRol.Size = new System.Drawing.Size(150, 21);
            this.cmbEditRol.TabIndex = 6;
            //
            // chkEditActivo
            //
            this.chkEditActivo.AutoSize = true;
            this.chkEditActivo.Location = new System.Drawing.Point(320, 87);
            this.chkEditActivo.Name = "chkEditActivo";
            this.chkEditActivo.Size = new System.Drawing.Size(56, 17);
            this.chkEditActivo.TabIndex = 7;
            this.chkEditActivo.Text = "Activo";
            this.chkEditActivo.UseVisualStyleBackColor = true;
            //
            // lblPassHint
            //
            this.lblPassHint.AutoSize = true;
            this.lblPassHint.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassHint.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblPassHint.Location = new System.Drawing.Point(130, 108);
            this.lblPassHint.Name = "lblPassHint";
            this.lblPassHint.Size = new System.Drawing.Size(132, 13);
            this.lblPassHint.TabIndex = 8;
            this.lblPassHint.Text = "(vacía = no se cambia)";
            //
            // btnModificar
            //
            this.btnModificar.Location = new System.Drawing.Point(40, 145);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(105, 32);
            this.btnModificar.TabIndex = 9;
            this.btnModificar.Text = "Modificar";
            this.btnModificar.UseVisualStyleBackColor = true;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            //
            // btnEliminar
            //
            this.btnEliminar.Location = new System.Drawing.Point(155, 145);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(105, 32);
            this.btnEliminar.TabIndex = 10;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            //
            // btnActivar
            //
            this.btnActivar.Location = new System.Drawing.Point(270, 145);
            this.btnActivar.Name = "btnActivar";
            this.btnActivar.Size = new System.Drawing.Size(105, 32);
            this.btnActivar.TabIndex = 11;
            this.btnActivar.Text = "Activar";
            this.btnActivar.UseVisualStyleBackColor = true;
            this.btnActivar.Click += new System.EventHandler(this.btnActivar_Click);
            //
            // btnLimpiarEditar
            //
            this.btnLimpiarEditar.Location = new System.Drawing.Point(385, 145);
            this.btnLimpiarEditar.Name = "btnLimpiarEditar";
            this.btnLimpiarEditar.Size = new System.Drawing.Size(105, 32);
            this.btnLimpiarEditar.TabIndex = 12;
            this.btnLimpiarEditar.Text = "Limpiar";
            this.btnLimpiarEditar.UseVisualStyleBackColor = true;
            this.btnLimpiarEditar.Click += new System.EventHandler(this.btnLimpiarEditar_Click);
            //
            // pnlBusqueda
            //
            this.pnlBusqueda.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlBusqueda.Controls.Add(this.dgvUsuarios);
            this.pnlBusqueda.Controls.Add(this.pnlTopBusqueda);
            this.pnlBusqueda.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBusqueda.Name = "pnlBusqueda";
            this.pnlBusqueda.TabIndex = 2;
            //
            // pnlTopBusqueda
            //
            this.pnlTopBusqueda.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlTopBusqueda.Controls.Add(this.lblTBUSCAR);
            this.pnlTopBusqueda.Controls.Add(this.txtBuscar);
            this.pnlTopBusqueda.Controls.Add(this.btnBuscar);
            this.pnlTopBusqueda.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopBusqueda.Location = new System.Drawing.Point(0, 0);
            this.pnlTopBusqueda.Name = "pnlTopBusqueda";
            this.pnlTopBusqueda.Size = new System.Drawing.Size(300, 78);
            this.pnlTopBusqueda.TabIndex = 0;
            //
            // lblTBUSCAR
            //
            this.lblTBUSCAR.AutoSize = true;
            this.lblTBUSCAR.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTBUSCAR.Location = new System.Drawing.Point(12, 10);
            this.lblTBUSCAR.Name = "lblTBUSCAR";
            this.lblTBUSCAR.Size = new System.Drawing.Size(102, 15);
            this.lblTBUSCAR.TabIndex = 0;
            this.lblTBUSCAR.Text = "Buscar usuario";
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
            // dgvUsuarios
            //
            this.dgvUsuarios.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvUsuarios.BackgroundColor = System.Drawing.Color.White;
            this.dgvUsuarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsuarios.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUsuarios.Location = new System.Drawing.Point(0, 78);
            this.dgvUsuarios.Name = "dgvUsuarios";
            this.dgvUsuarios.Size = new System.Drawing.Size(300, 522);
            this.dgvUsuarios.TabIndex = 1;
            this.dgvUsuarios.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUsuarios_CellClick);
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
            // FrmUsuarios
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(860, 600);
            this.MinimumSize = new System.Drawing.Size(800, 520);
            this.Controls.Add(this.tlpBase);
            this.Name = "FrmUsuarios";
            this.Text = "Usuarios";
            this.Load += new System.EventHandler(this.FrmUsuarios_Load);
            this.grpCrear.ResumeLayout(false);
            this.grpCrear.PerformLayout();
            this.grpModificar.ResumeLayout(false);
            this.grpModificar.PerformLayout();
            this.pnlBusqueda.ResumeLayout(false);
            this.pnlTopBusqueda.ResumeLayout(false);
            this.pnlTopBusqueda.PerformLayout();
            this.tlpBase.ResumeLayout(false);
            this.pnlIzquierda.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuarios)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpCrear;
        private System.Windows.Forms.Label lblCUsuario;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.Label lblCPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblCRol;
        private System.Windows.Forms.ComboBox cmbRol;
        private System.Windows.Forms.CheckBox chkActivo;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnLimpiarCrear;
        private System.Windows.Forms.GroupBox grpModificar;
        private System.Windows.Forms.Label lblEditando;
        private System.Windows.Forms.Label lblEditUsuario;
        private System.Windows.Forms.TextBox txtEditUsuario;
        private System.Windows.Forms.Label lblEditPassword;
        private System.Windows.Forms.TextBox txtEditPassword;
        private System.Windows.Forms.Label lblPassHint;
        private System.Windows.Forms.Label lblEditRol;
        private System.Windows.Forms.ComboBox cmbEditRol;
        private System.Windows.Forms.CheckBox chkEditActivo;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnActivar;
        private System.Windows.Forms.Button btnLimpiarEditar;
        private System.Windows.Forms.Panel pnlBusqueda;
        private System.Windows.Forms.TableLayoutPanel tlpBase;
        private System.Windows.Forms.Panel pnlIzquierda;
        private System.Windows.Forms.Panel pnlTopBusqueda;
        private System.Windows.Forms.Label lblTBUSCAR;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.DataGridView dgvUsuarios;
    }
}
