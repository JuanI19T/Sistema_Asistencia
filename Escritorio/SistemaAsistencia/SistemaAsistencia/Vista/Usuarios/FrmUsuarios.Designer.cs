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
            this.txtPassword = new MaterialSkin.Controls.MaterialTextBox2();
            this.txtUsuario = new MaterialSkin.Controls.MaterialTextBox2();
            this.btnLimpiarCrear = new MaterialSkin.Controls.MaterialButton();
            this.btnGuardar = new MaterialSkin.Controls.MaterialButton();
            this.cmbRol = new MaterialSkin.Controls.MaterialComboBox();
            this.btnModificar = new MaterialSkin.Controls.MaterialButton();
            this.txtBuscar = new MaterialSkin.Controls.MaterialTextBox();
            this.btnToggleActivo = new MaterialSkin.Controls.MaterialButton();
            this.materialTabControl1 = new MaterialSkin.Controls.MaterialTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.lblEditando = new MaterialSkin.Controls.MaterialLabel();
            this.txtEditPassword = new MaterialSkin.Controls.MaterialTextBox2();
            this.cmbEditRol = new MaterialSkin.Controls.MaterialComboBox();
            this.chkEditActivo = new MaterialSkin.Controls.MaterialCheckbox();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.btnLimpiarEditar = new MaterialSkin.Controls.MaterialButton();
            this.btnEliminar = new MaterialSkin.Controls.MaterialButton();
            this.txtEditUsuario = new MaterialSkin.Controls.MaterialTextBox2();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.dgvUsuarios = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.materialTabSelector1 = new MaterialSkin.Controls.MaterialTabSelector();
            this.object_5c919b92_82d7_40f2_8000_877d301d889d = new System.Windows.Forms.TableLayoutPanel();
            this.object_75e25dcf_ce2a_4393_bee0_b4b261e27f50 = new System.Windows.Forms.TableLayoutPanel();
            this.materialTabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuarios)).BeginInit();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtPassword
            // 
            this.txtPassword.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtPassword.AnimateReadOnly = false;
            this.txtPassword.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.txtPassword.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtPassword.Depth = 0;
            this.txtPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtPassword.HideSelection = true;
            this.txtPassword.Hint = "Contraseña";
            this.txtPassword.LeadingIcon = null;
            this.txtPassword.Location = new System.Drawing.Point(83, 126);
            this.txtPassword.MaxLength = 32767;
            this.txtPassword.MouseState = MaterialSkin.MouseState.OUT;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '\0';
            this.txtPassword.PrefixSuffixText = null;
            this.txtPassword.ReadOnly = false;
            this.txtPassword.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtPassword.SelectedText = "";
            this.txtPassword.SelectionLength = 0;
            this.txtPassword.SelectionStart = 0;
            this.txtPassword.ShortcutsEnabled = true;
            this.txtPassword.Size = new System.Drawing.Size(250, 48);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.TabStop = false;
            this.txtPassword.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtPassword.TrailingIcon = null;
            this.txtPassword.UseSystemPasswordChar = false;
            // 
            // txtUsuario
            // 
            this.txtUsuario.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtUsuario.AnimateReadOnly = false;
            this.txtUsuario.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.txtUsuario.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtUsuario.Depth = 0;
            this.txtUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtUsuario.HideSelection = true;
            this.txtUsuario.Hint = "Usuario";
            this.txtUsuario.LeadingIcon = null;
            this.txtUsuario.Location = new System.Drawing.Point(83, 26);
            this.txtUsuario.MaxLength = 32767;
            this.txtUsuario.MouseState = MaterialSkin.MouseState.OUT;
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.PasswordChar = '\0';
            this.txtUsuario.PrefixSuffixText = null;
            this.txtUsuario.ReadOnly = false;
            this.txtUsuario.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtUsuario.SelectedText = "";
            this.txtUsuario.SelectionLength = 0;
            this.txtUsuario.SelectionStart = 0;
            this.txtUsuario.ShortcutsEnabled = true;
            this.txtUsuario.Size = new System.Drawing.Size(250, 48);
            this.txtUsuario.TabIndex = 0;
            this.txtUsuario.TabStop = false;
            this.txtUsuario.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtUsuario.TrailingIcon = null;
            this.txtUsuario.UseSystemPasswordChar = false;
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
            this.btnLimpiarCrear.Location = new System.Drawing.Point(39, 22);
            this.btnLimpiarCrear.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnLimpiarCrear.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnLimpiarCrear.Name = "btnLimpiarCrear";
            this.btnLimpiarCrear.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnLimpiarCrear.Size = new System.Drawing.Size(100, 36);
            this.btnLimpiarCrear.TabIndex = 4;
            this.btnLimpiarCrear.Text = "Limpiar";
            this.btnLimpiarCrear.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Outlined;
            this.btnLimpiarCrear.UseAccentColor = false;
            this.btnLimpiarCrear.UseVisualStyleBackColor = true;
            this.btnLimpiarCrear.Click += new System.EventHandler(this.btnLimpiarCrear_Click);
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
            this.btnGuardar.Location = new System.Drawing.Point(182, 22);
            this.btnGuardar.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnGuardar.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnGuardar.Size = new System.Drawing.Size(170, 36);
            this.btnGuardar.TabIndex = 3;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnGuardar.UseAccentColor = false;
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // cmbRol
            // 
            this.cmbRol.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cmbRol.AutoResize = false;
            this.cmbRol.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbRol.Depth = 0;
            this.cmbRol.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbRol.DropDownHeight = 174;
            this.cmbRol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRol.DropDownWidth = 121;
            this.cmbRol.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.cmbRol.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cmbRol.FormattingEnabled = true;
            this.cmbRol.Hint = "Selecciona un Rol";
            this.cmbRol.IntegralHeight = false;
            this.cmbRol.ItemHeight = 43;
            this.cmbRol.Location = new System.Drawing.Point(107, 225);
            this.cmbRol.MaxDropDownItems = 4;
            this.cmbRol.MouseState = MaterialSkin.MouseState.OUT;
            this.cmbRol.Name = "cmbRol";
            this.cmbRol.Size = new System.Drawing.Size(202, 49);
            this.cmbRol.StartIndex = -1;
            this.cmbRol.TabIndex = 2;
            this.cmbRol.UseAccent = false;
            // 
            // btnModificar
            // 
            this.btnModificar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnModificar.AutoSize = false;
            this.btnModificar.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnModificar.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnModificar.Depth = 0;
            this.btnModificar.HighEmphasis = true;
            this.btnModificar.Icon = null;
            this.btnModificar.Location = new System.Drawing.Point(19, 6);
            this.btnModificar.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnModificar.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnModificar.Size = new System.Drawing.Size(96, 42);
            this.btnModificar.TabIndex = 7;
            this.btnModificar.Text = "Modificar";
            this.btnModificar.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnModificar.UseAccentColor = false;
            this.btnModificar.UseVisualStyleBackColor = true;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // txtBuscar
            // 
            this.txtBuscar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtBuscar.AnimateReadOnly = false;
            this.txtBuscar.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBuscar.Depth = 0;
            this.txtBuscar.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtBuscar.Hint = "Buscar Usuarios";
            this.txtBuscar.LeadingIcon = null;
            this.txtBuscar.Location = new System.Drawing.Point(30, 3);
            this.txtBuscar.MaxLength = 50;
            this.txtBuscar.MouseState = MaterialSkin.MouseState.OUT;
            this.txtBuscar.Multiline = false;
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(142, 50);
            this.txtBuscar.TabIndex = 0;
            this.txtBuscar.Text = "";
            this.txtBuscar.TrailingIcon = null;
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);
            // 
            // btnToggleActivo
            // 
            this.btnToggleActivo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnToggleActivo.AutoSize = false;
            this.btnToggleActivo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnToggleActivo.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnToggleActivo.Depth = 0;
            this.btnToggleActivo.HighEmphasis = true;
            this.btnToggleActivo.Icon = null;
            this.btnToggleActivo.Location = new System.Drawing.Point(265, 9);
            this.btnToggleActivo.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnToggleActivo.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnToggleActivo.Name = "btnToggleActivo";
            this.btnToggleActivo.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnToggleActivo.Size = new System.Drawing.Size(76, 38);
            this.btnToggleActivo.TabIndex = 1;
            this.btnToggleActivo.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnToggleActivo.UseAccentColor = false;
            this.btnToggleActivo.UseVisualStyleBackColor = true;
            this.btnToggleActivo.Click += new System.EventHandler(this.btnToggleActivo_Click);
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
            this.materialTabControl1.Location = new System.Drawing.Point(6, 49);
            this.materialTabControl1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTabControl1.Multiline = true;
            this.materialTabControl1.Name = "materialTabControl1";
            this.materialTabControl1.SelectedIndex = 0;
            this.materialTabControl1.Size = new System.Drawing.Size(848, 545);
            this.materialTabControl1.TabIndex = 14;
            // 
            // tabPage1
            // 
            this.tabPage1.AutoScroll = true;
            this.tabPage1.BackColor = System.Drawing.Color.White;
            this.tabPage1.Controls.Add(this.tableLayoutPanel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(840, 515);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Crear un Usuario";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.txtUsuario, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtPassword, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.cmbRol, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(834, 400);
            this.tableLayoutPanel1.TabIndex = 14;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.btnGuardar, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnLimpiarCrear, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(30, 310);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(356, 80);
            this.tableLayoutPanel2.TabIndex = 14;
            // 
            // tabPage2
            // 
            this.tabPage2.AutoScroll = true;
            this.tabPage2.BackColor = System.Drawing.Color.White;
            this.tabPage2.Controls.Add(this.tableLayoutPanel7);
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(840, 515);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Modificar un Usuario";
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 2;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.Controls.Add(this.tableLayoutPanel5, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.tableLayoutPanel3, 1, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 1;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(834, 431);
            this.tableLayoutPanel7.TabIndex = 19;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Controls.Add(this.lblEditando, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.txtEditPassword, 0, 2);
            this.tableLayoutPanel5.Controls.Add(this.cmbEditRol, 0, 3);
            this.tableLayoutPanel5.Controls.Add(this.chkEditActivo, 0, 4);
            this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel6, 0, 5);
            this.tableLayoutPanel5.Controls.Add(this.txtEditUsuario, 0, 1);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 6;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(411, 425);
            this.tableLayoutPanel5.TabIndex = 18;
            // 
            // lblEditando
            // 
            this.lblEditando.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblEditando.AutoSize = true;
            this.lblEditando.Depth = 0;
            this.lblEditando.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblEditando.Location = new System.Drawing.Point(3, 10);
            this.lblEditando.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblEditando.Name = "lblEditando";
            this.lblEditando.Size = new System.Drawing.Size(229, 19);
            this.lblEditando.TabIndex = 16;
            this.lblEditando.Text = "Editando: (seleccione de la lista)";
            // 
            // txtEditPassword
            // 
            this.txtEditPassword.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtEditPassword.AnimateReadOnly = false;
            this.txtEditPassword.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.txtEditPassword.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtEditPassword.Depth = 0;
            this.txtEditPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtEditPassword.HideSelection = true;
            this.txtEditPassword.Hint = "Contraseña";
            this.txtEditPassword.LeadingIcon = null;
            this.txtEditPassword.Location = new System.Drawing.Point(94, 137);
            this.txtEditPassword.MaxLength = 32767;
            this.txtEditPassword.MouseState = MaterialSkin.MouseState.OUT;
            this.txtEditPassword.Name = "txtEditPassword";
            this.txtEditPassword.PasswordChar = '\0';
            this.txtEditPassword.PrefixSuffixText = null;
            this.txtEditPassword.ReadOnly = false;
            this.txtEditPassword.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtEditPassword.SelectedText = "";
            this.txtEditPassword.SelectionLength = 0;
            this.txtEditPassword.SelectionStart = 0;
            this.txtEditPassword.ShortcutsEnabled = true;
            this.txtEditPassword.Size = new System.Drawing.Size(223, 48);
            this.txtEditPassword.TabIndex = 3;
            this.txtEditPassword.TabStop = false;
            this.txtEditPassword.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtEditPassword.TrailingIcon = null;
            this.txtEditPassword.UseSystemPasswordChar = false;
            // 
            // cmbEditRol
            // 
            this.cmbEditRol.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cmbEditRol.AutoResize = false;
            this.cmbEditRol.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbEditRol.Depth = 0;
            this.cmbEditRol.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbEditRol.DropDownHeight = 174;
            this.cmbEditRol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEditRol.DropDownWidth = 121;
            this.cmbEditRol.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.cmbEditRol.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cmbEditRol.FormattingEnabled = true;
            this.cmbEditRol.Hint = "Selecciona un Rol";
            this.cmbEditRol.IntegralHeight = false;
            this.cmbEditRol.ItemHeight = 43;
            this.cmbEditRol.Location = new System.Drawing.Point(96, 218);
            this.cmbEditRol.MaxDropDownItems = 4;
            this.cmbEditRol.MouseState = MaterialSkin.MouseState.OUT;
            this.cmbEditRol.Name = "cmbEditRol";
            this.cmbEditRol.Size = new System.Drawing.Size(219, 49);
            this.cmbEditRol.StartIndex = -1;
            this.cmbEditRol.TabIndex = 4;
            this.cmbEditRol.UseAccent = false;
            // 
            // chkEditActivo
            // 
            this.chkEditActivo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.chkEditActivo.Depth = 0;
            this.chkEditActivo.Location = new System.Drawing.Point(154, 305);
            this.chkEditActivo.Margin = new System.Windows.Forms.Padding(0);
            this.chkEditActivo.MouseLocation = new System.Drawing.Point(-1, -1);
            this.chkEditActivo.MouseState = MaterialSkin.MouseState.HOVER;
            this.chkEditActivo.Name = "chkEditActivo";
            this.chkEditActivo.ReadOnly = false;
            this.chkEditActivo.Ripple = true;
            this.chkEditActivo.Size = new System.Drawing.Size(103, 37);
            this.chkEditActivo.TabIndex = 5;
            this.chkEditActivo.Text = "Activo";
            this.chkEditActivo.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 3;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel6.Controls.Add(this.btnModificar, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.btnLimpiarEditar, 1, 0);
            this.tableLayoutPanel6.Controls.Add(this.btnEliminar, 2, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 367);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(405, 55);
            this.tableLayoutPanel6.TabIndex = 17;
            // 
            // btnLimpiarEditar
            // 
            this.btnLimpiarEditar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnLimpiarEditar.AutoSize = false;
            this.btnLimpiarEditar.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnLimpiarEditar.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnLimpiarEditar.Depth = 0;
            this.btnLimpiarEditar.HighEmphasis = true;
            this.btnLimpiarEditar.Icon = null;
            this.btnLimpiarEditar.Location = new System.Drawing.Point(151, 6);
            this.btnLimpiarEditar.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnLimpiarEditar.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnLimpiarEditar.Name = "btnLimpiarEditar";
            this.btnLimpiarEditar.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnLimpiarEditar.Size = new System.Drawing.Size(103, 42);
            this.btnLimpiarEditar.TabIndex = 6;
            this.btnLimpiarEditar.Text = "Limpiar";
            this.btnLimpiarEditar.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Text;
            this.btnLimpiarEditar.UseAccentColor = false;
            this.btnLimpiarEditar.UseVisualStyleBackColor = true;
            this.btnLimpiarEditar.Click += new System.EventHandler(this.btnLimpiarEditar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnEliminar.AutoSize = false;
            this.btnEliminar.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnEliminar.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnEliminar.Depth = 0;
            this.btnEliminar.HighEmphasis = true;
            this.btnEliminar.Icon = null;
            this.btnEliminar.Location = new System.Drawing.Point(285, 6);
            this.btnEliminar.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnEliminar.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnEliminar.Size = new System.Drawing.Size(105, 42);
            this.btnEliminar.TabIndex = 8;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Outlined;
            this.btnEliminar.UseAccentColor = false;
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // txtEditUsuario
            // 
            this.txtEditUsuario.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtEditUsuario.AnimateReadOnly = false;
            this.txtEditUsuario.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.txtEditUsuario.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtEditUsuario.Depth = 0;
            this.txtEditUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtEditUsuario.HideSelection = true;
            this.txtEditUsuario.Hint = "Usuario";
            this.txtEditUsuario.LeadingIcon = null;
            this.txtEditUsuario.Location = new System.Drawing.Point(93, 56);
            this.txtEditUsuario.MaxLength = 32767;
            this.txtEditUsuario.MouseState = MaterialSkin.MouseState.OUT;
            this.txtEditUsuario.Name = "txtEditUsuario";
            this.txtEditUsuario.PasswordChar = '\0';
            this.txtEditUsuario.PrefixSuffixText = null;
            this.txtEditUsuario.ReadOnly = false;
            this.txtEditUsuario.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtEditUsuario.SelectedText = "";
            this.txtEditUsuario.SelectionLength = 0;
            this.txtEditUsuario.SelectionStart = 0;
            this.txtEditUsuario.ShortcutsEnabled = true;
            this.txtEditUsuario.Size = new System.Drawing.Size(225, 48);
            this.txtEditUsuario.TabIndex = 2;
            this.txtEditUsuario.TabStop = false;
            this.txtEditUsuario.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtEditUsuario.TrailingIcon = null;
            this.txtEditUsuario.UseSystemPasswordChar = false;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.dgvUsuarios, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(420, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.60905F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 85.39095F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(411, 425);
            this.tableLayoutPanel3.TabIndex = 17;
            // 
            // dgvUsuarios
            // 
            this.dgvUsuarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsuarios.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUsuarios.Location = new System.Drawing.Point(3, 65);
            this.dgvUsuarios.Name = "dgvUsuarios";
            this.dgvUsuarios.Size = new System.Drawing.Size(405, 357);
            this.dgvUsuarios.TabIndex = 0;
            this.dgvUsuarios.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUsuarios_CellClick);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.btnToggleActivo, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.txtBuscar, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(405, 56);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // materialTabSelector1
            // 
            this.materialTabSelector1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.materialTabSelector1.BaseTabControl = this.materialTabControl1;
            this.materialTabSelector1.CharacterCasing = MaterialSkin.Controls.MaterialTabSelector.CustomCharacterCasing.Normal;
            this.materialTabSelector1.Depth = 0;
            this.materialTabSelector1.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialTabSelector1.Location = new System.Drawing.Point(6, 2);
            this.materialTabSelector1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTabSelector1.Name = "materialTabSelector1";
            this.materialTabSelector1.Size = new System.Drawing.Size(848, 41);
            this.materialTabSelector1.TabIndex = 15;
            // 
            // object_5c919b92_82d7_40f2_8000_877d301d889d
            // 
            this.object_5c919b92_82d7_40f2_8000_877d301d889d.AutoSize = true;
            this.object_5c919b92_82d7_40f2_8000_877d301d889d.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.object_5c919b92_82d7_40f2_8000_877d301d889d.ColumnCount = 2;
            this.object_5c919b92_82d7_40f2_8000_877d301d889d.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.object_5c919b92_82d7_40f2_8000_877d301d889d.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.object_5c919b92_82d7_40f2_8000_877d301d889d.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.object_5c919b92_82d7_40f2_8000_877d301d889d.Dock = System.Windows.Forms.DockStyle.Top;
            this.object_5c919b92_82d7_40f2_8000_877d301d889d.Location = new System.Drawing.Point(3, 3);
            this.object_5c919b92_82d7_40f2_8000_877d301d889d.Name = "object_5c919b92_82d7_40f2_8000_877d301d889d";
            this.object_5c919b92_82d7_40f2_8000_877d301d889d.RowCount = 4;
            this.object_5c919b92_82d7_40f2_8000_877d301d889d.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.object_5c919b92_82d7_40f2_8000_877d301d889d.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.object_5c919b92_82d7_40f2_8000_877d301d889d.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.object_5c919b92_82d7_40f2_8000_877d301d889d.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.object_5c919b92_82d7_40f2_8000_877d301d889d.Size = new System.Drawing.Size(834, 86);
            this.object_5c919b92_82d7_40f2_8000_877d301d889d.TabIndex = 14;
            // 
            // object_75e25dcf_ce2a_4393_bee0_b4b261e27f50
            // 
            this.object_75e25dcf_ce2a_4393_bee0_b4b261e27f50.AutoSize = true;
            this.object_75e25dcf_ce2a_4393_bee0_b4b261e27f50.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.object_75e25dcf_ce2a_4393_bee0_b4b261e27f50.ColumnCount = 2;
            this.object_75e25dcf_ce2a_4393_bee0_b4b261e27f50.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.object_75e25dcf_ce2a_4393_bee0_b4b261e27f50.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.object_75e25dcf_ce2a_4393_bee0_b4b261e27f50.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.object_75e25dcf_ce2a_4393_bee0_b4b261e27f50.Dock = System.Windows.Forms.DockStyle.Top;
            this.object_75e25dcf_ce2a_4393_bee0_b4b261e27f50.Location = new System.Drawing.Point(3, 3);
            this.object_75e25dcf_ce2a_4393_bee0_b4b261e27f50.Name = "object_75e25dcf_ce2a_4393_bee0_b4b261e27f50";
            this.object_75e25dcf_ce2a_4393_bee0_b4b261e27f50.RowCount = 4;
            this.object_75e25dcf_ce2a_4393_bee0_b4b261e27f50.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.object_75e25dcf_ce2a_4393_bee0_b4b261e27f50.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.object_75e25dcf_ce2a_4393_bee0_b4b261e27f50.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.object_75e25dcf_ce2a_4393_bee0_b4b261e27f50.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.object_75e25dcf_ce2a_4393_bee0_b4b261e27f50.Size = new System.Drawing.Size(834, 86);
            this.object_75e25dcf_ce2a_4393_bee0_b4b261e27f50.TabIndex = 14;
            // 
            // FrmUsuarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(860, 600);
            this.Controls.Add(this.materialTabSelector1);
            this.Controls.Add(this.materialTabControl1);
            this.MinimumSize = new System.Drawing.Size(860, 600);
            this.Name = "FrmUsuarios";
            this.Text = "Usuarios";
            this.Load += new System.EventHandler(this.FrmUsuarios_Load);
            this.materialTabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuarios)).EndInit();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private MaterialSkin.Controls.MaterialComboBox cmbRol;

        private MaterialSkin.Controls.MaterialButton btnGuardar;
        private MaterialSkin.Controls.MaterialButton btnLimpiarCrear;
        private MaterialSkin.Controls.MaterialButton btnModificar;
        private MaterialSkin.Controls.MaterialTextBox txtBuscar;
        private MaterialSkin.Controls.MaterialButton btnToggleActivo;
        private MaterialSkin.Controls.MaterialTextBox2 txtUsuario;
        private MaterialSkin.Controls.MaterialTextBox2 txtPassword;
        private MaterialSkin.Controls.MaterialTabControl materialTabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dgvUsuarios;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private MaterialSkin.Controls.MaterialTabSelector materialTabSelector1;
        private MaterialSkin.Controls.MaterialCheckbox chkEditActivo;
        private MaterialSkin.Controls.MaterialComboBox cmbEditRol;
        private MaterialSkin.Controls.MaterialLabel lblEditando;
        private MaterialSkin.Controls.MaterialTextBox2 txtEditUsuario;
        private MaterialSkin.Controls.MaterialTextBox2 txtEditPassword;
        private MaterialSkin.Controls.MaterialButton btnEliminar;
        private MaterialSkin.Controls.MaterialButton btnLimpiarEditar;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.TableLayoutPanel object_5c919b92_82d7_40f2_8000_877d301d889d;
        private System.Windows.Forms.TableLayoutPanel object_75e25dcf_ce2a_4393_bee0_b4b261e27f50;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
    }
}
