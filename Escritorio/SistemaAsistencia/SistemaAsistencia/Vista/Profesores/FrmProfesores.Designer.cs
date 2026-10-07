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
            this.txtNombre = new MaterialSkin.Controls.MaterialTextBox2();
            this.txtApellido = new MaterialSkin.Controls.MaterialTextBox2();
            this.txtDni = new MaterialSkin.Controls.MaterialTextBox2();
            this.txtLegajo = new MaterialSkin.Controls.MaterialTextBox2();
            this.txtCorreo = new MaterialSkin.Controls.MaterialTextBox2();
            this.lblTelCrear = new MaterialSkin.Controls.MaterialLabel();
            this.ctrlTelCrear = new SistemaAsistencia.Vista.Comun.CtrlTelefono();
            this.btnGuardar = new MaterialSkin.Controls.MaterialButton();
            this.btnLimpiarCrear = new MaterialSkin.Controls.MaterialButton();
            this.txtEditNombre = new MaterialSkin.Controls.MaterialTextBox2();
            this.txtEditApellido = new MaterialSkin.Controls.MaterialTextBox2();
            this.txtEditDni = new MaterialSkin.Controls.MaterialTextBox2();
            this.txtEditLegajo = new MaterialSkin.Controls.MaterialTextBox2();
            this.txtEditCorreo = new MaterialSkin.Controls.MaterialTextBox2();
            this.lblTelEdit = new MaterialSkin.Controls.MaterialLabel();
            this.ctrlTelEdit = new SistemaAsistencia.Vista.Comun.CtrlTelefono();
            this.btnModificar = new MaterialSkin.Controls.MaterialButton();
            this.btnEliminar = new MaterialSkin.Controls.MaterialButton();
            this.btnLimpiarEditar = new MaterialSkin.Controls.MaterialButton();
            this.lblEditando = new MaterialSkin.Controls.MaterialLabel();
            this.txtBuscar = new MaterialSkin.Controls.MaterialTextBox();
            this.btnBuscar = new MaterialSkin.Controls.MaterialButton();
            this.dgvProfesores = new System.Windows.Forms.DataGridView();
            this.materialTabControl1 = new MaterialSkin.Controls.MaterialTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableTelCrear = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.materialTabSelector1 = new MaterialSkin.Controls.MaterialTabSelector();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProfesores)).BeginInit();
            this.materialTabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableTelCrear.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtNombre
            // 
            this.txtNombre.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtNombre.AnimateReadOnly = false;
            this.txtNombre.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.txtNombre.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtNombre.Depth = 0;
            this.txtNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtNombre.HideSelection = true;
            this.txtNombre.Hint = "Nombre";
            this.txtNombre.LeadingIcon = null;
            this.txtNombre.Location = new System.Drawing.Point(291, 3);
            this.txtNombre.MaxLength = 32767;
            this.txtNombre.MouseState = MaterialSkin.MouseState.OUT;
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.PasswordChar = '\0';
            this.txtNombre.PrefixSuffixText = null;
            this.txtNombre.ReadOnly = false;
            this.txtNombre.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtNombre.SelectedText = "";
            this.txtNombre.SelectionLength = 0;
            this.txtNombre.SelectionStart = 0;
            this.txtNombre.ShortcutsEnabled = true;
            this.txtNombre.Size = new System.Drawing.Size(250, 48);
            this.txtNombre.TabIndex = 0;
            this.txtNombre.TabStop = false;
            this.txtNombre.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtNombre.TrailingIcon = null;
            this.txtNombre.UseSystemPasswordChar = false;
            // 
            // txtApellido
            // 
            this.txtApellido.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtApellido.AnimateReadOnly = false;
            this.txtApellido.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.txtApellido.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtApellido.Depth = 0;
            this.txtApellido.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtApellido.HideSelection = true;
            this.txtApellido.Hint = "Apellido";
            this.txtApellido.LeadingIcon = null;
            this.txtApellido.Location = new System.Drawing.Point(291, 57);
            this.txtApellido.MaxLength = 32767;
            this.txtApellido.MouseState = MaterialSkin.MouseState.OUT;
            this.txtApellido.Name = "txtApellido";
            this.txtApellido.PasswordChar = '\0';
            this.txtApellido.PrefixSuffixText = null;
            this.txtApellido.ReadOnly = false;
            this.txtApellido.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtApellido.SelectedText = "";
            this.txtApellido.SelectionLength = 0;
            this.txtApellido.SelectionStart = 0;
            this.txtApellido.ShortcutsEnabled = true;
            this.txtApellido.Size = new System.Drawing.Size(250, 48);
            this.txtApellido.TabIndex = 1;
            this.txtApellido.TabStop = false;
            this.txtApellido.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtApellido.TrailingIcon = null;
            this.txtApellido.UseSystemPasswordChar = false;
            // 
            // txtDni
            // 
            this.txtDni.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtDni.AnimateReadOnly = false;
            this.txtDni.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.txtDni.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtDni.Depth = 0;
            this.txtDni.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtDni.HideSelection = true;
            this.txtDni.Hint = "DNI";
            this.txtDni.LeadingIcon = null;
            this.txtDni.Location = new System.Drawing.Point(291, 111);
            this.txtDni.MaxLength = 8;
            this.txtDni.MouseState = MaterialSkin.MouseState.OUT;
            this.txtDni.Name = "txtDni";
            this.txtDni.PasswordChar = '\0';
            this.txtDni.PrefixSuffixText = null;
            this.txtDni.ReadOnly = false;
            this.txtDni.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtDni.SelectedText = "";
            this.txtDni.SelectionLength = 0;
            this.txtDni.SelectionStart = 0;
            this.txtDni.ShortcutsEnabled = true;
            this.txtDni.Size = new System.Drawing.Size(250, 48);
            this.txtDni.TabIndex = 2;
            this.txtDni.TabStop = false;
            this.txtDni.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtDni.TrailingIcon = null;
            this.txtDni.UseSystemPasswordChar = false;
            // 
            // txtLegajo
            // 
            this.txtLegajo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtLegajo.AnimateReadOnly = false;
            this.txtLegajo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.txtLegajo.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtLegajo.Depth = 0;
            this.txtLegajo.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtLegajo.HideSelection = true;
            this.txtLegajo.Hint = "Legajo (= DNI)";
            this.txtLegajo.LeadingIcon = null;
            this.txtLegajo.Location = new System.Drawing.Point(291, 165);
            this.txtLegajo.MaxLength = 10;
            this.txtLegajo.MouseState = MaterialSkin.MouseState.OUT;
            this.txtLegajo.Name = "txtLegajo";
            this.txtLegajo.PasswordChar = '\0';
            this.txtLegajo.PrefixSuffixText = null;
            this.txtLegajo.ReadOnly = true;
            this.txtLegajo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtLegajo.SelectedText = "";
            this.txtLegajo.SelectionLength = 0;
            this.txtLegajo.SelectionStart = 0;
            this.txtLegajo.ShortcutsEnabled = true;
            this.txtLegajo.Size = new System.Drawing.Size(250, 48);
            this.txtLegajo.TabIndex = 3;
            this.txtLegajo.TabStop = false;
            this.txtLegajo.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtLegajo.TrailingIcon = null;
            this.txtLegajo.UseSystemPasswordChar = false;
            // 
            // txtCorreo
            // 
            this.txtCorreo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtCorreo.AnimateReadOnly = false;
            this.txtCorreo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.txtCorreo.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtCorreo.Depth = 0;
            this.txtCorreo.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtCorreo.HideSelection = true;
            this.txtCorreo.Hint = "Correo";
            this.txtCorreo.LeadingIcon = null;
            this.txtCorreo.Location = new System.Drawing.Point(291, 219);
            this.txtCorreo.MaxLength = 32767;
            this.txtCorreo.MouseState = MaterialSkin.MouseState.OUT;
            this.txtCorreo.Name = "txtCorreo";
            this.txtCorreo.PasswordChar = '\0';
            this.txtCorreo.PrefixSuffixText = null;
            this.txtCorreo.ReadOnly = false;
            this.txtCorreo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtCorreo.SelectedText = "";
            this.txtCorreo.SelectionLength = 0;
            this.txtCorreo.SelectionStart = 0;
            this.txtCorreo.ShortcutsEnabled = true;
            this.txtCorreo.Size = new System.Drawing.Size(250, 48);
            this.txtCorreo.TabIndex = 4;
            this.txtCorreo.TabStop = false;
            this.txtCorreo.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtCorreo.TrailingIcon = null;
            this.txtCorreo.UseSystemPasswordChar = false;
            // 
            // lblTelCrear
            // 
            this.lblTelCrear.AutoSize = true;
            this.lblTelCrear.Depth = 0;
            this.lblTelCrear.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTelCrear.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblTelCrear.Location = new System.Drawing.Point(3, 0);
            this.lblTelCrear.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblTelCrear.Name = "lblTelCrear";
            this.lblTelCrear.Size = new System.Drawing.Size(74, 66);
            this.lblTelCrear.TabIndex = 0;
            this.lblTelCrear.Text = "Teléfono";
            this.lblTelCrear.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ctrlTelCrear
            // 
            this.ctrlTelCrear.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrlTelCrear.Location = new System.Drawing.Point(83, 3);
            this.ctrlTelCrear.MinimumSize = new System.Drawing.Size(220, 48);
            this.ctrlTelCrear.Name = "ctrlTelCrear";
            this.ctrlTelCrear.Size = new System.Drawing.Size(325, 60);
            this.ctrlTelCrear.TabIndex = 5;
            this.ctrlTelCrear.Telefono = "";
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
            this.btnGuardar.Location = new System.Drawing.Point(193, 13);
            this.btnGuardar.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnGuardar.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnGuardar.Size = new System.Drawing.Size(181, 36);
            this.btnGuardar.TabIndex = 6;
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
            this.btnLimpiarCrear.Location = new System.Drawing.Point(44, 13);
            this.btnLimpiarCrear.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnLimpiarCrear.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnLimpiarCrear.Name = "btnLimpiarCrear";
            this.btnLimpiarCrear.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnLimpiarCrear.Size = new System.Drawing.Size(100, 36);
            this.btnLimpiarCrear.TabIndex = 7;
            this.btnLimpiarCrear.Text = "Limpiar";
            this.btnLimpiarCrear.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Text;
            this.btnLimpiarCrear.UseAccentColor = false;
            this.btnLimpiarCrear.UseVisualStyleBackColor = true;
            this.btnLimpiarCrear.Click += new System.EventHandler(this.btnLimpiarCrear_Click);
            // 
            // txtEditNombre
            // 
            this.txtEditNombre.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtEditNombre.AnimateReadOnly = false;
            this.txtEditNombre.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.txtEditNombre.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtEditNombre.Depth = 0;
            this.txtEditNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtEditNombre.HideSelection = true;
            this.txtEditNombre.Hint = "Nombre";
            this.txtEditNombre.LeadingIcon = null;
            this.txtEditNombre.Location = new System.Drawing.Point(118, 33);
            this.txtEditNombre.MaxLength = 32767;
            this.txtEditNombre.MouseState = MaterialSkin.MouseState.OUT;
            this.txtEditNombre.Name = "txtEditNombre";
            this.txtEditNombre.PasswordChar = '\0';
            this.txtEditNombre.PrefixSuffixText = null;
            this.txtEditNombre.ReadOnly = false;
            this.txtEditNombre.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtEditNombre.SelectedText = "";
            this.txtEditNombre.SelectionLength = 0;
            this.txtEditNombre.SelectionStart = 0;
            this.txtEditNombre.ShortcutsEnabled = true;
            this.txtEditNombre.Size = new System.Drawing.Size(174, 48);
            this.txtEditNombre.TabIndex = 2;
            this.txtEditNombre.TabStop = false;
            this.txtEditNombre.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtEditNombre.TrailingIcon = null;
            this.txtEditNombre.UseSystemPasswordChar = false;
            // 
            // txtEditApellido
            // 
            this.txtEditApellido.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtEditApellido.AnimateReadOnly = false;
            this.txtEditApellido.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.txtEditApellido.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtEditApellido.Depth = 0;
            this.txtEditApellido.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtEditApellido.HideSelection = true;
            this.txtEditApellido.Hint = "Apellido";
            this.txtEditApellido.LeadingIcon = null;
            this.txtEditApellido.Location = new System.Drawing.Point(118, 88);
            this.txtEditApellido.MaxLength = 32767;
            this.txtEditApellido.MouseState = MaterialSkin.MouseState.OUT;
            this.txtEditApellido.Name = "txtEditApellido";
            this.txtEditApellido.PasswordChar = '\0';
            this.txtEditApellido.PrefixSuffixText = null;
            this.txtEditApellido.ReadOnly = false;
            this.txtEditApellido.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtEditApellido.SelectedText = "";
            this.txtEditApellido.SelectionLength = 0;
            this.txtEditApellido.SelectionStart = 0;
            this.txtEditApellido.ShortcutsEnabled = true;
            this.txtEditApellido.Size = new System.Drawing.Size(174, 48);
            this.txtEditApellido.TabIndex = 3;
            this.txtEditApellido.TabStop = false;
            this.txtEditApellido.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtEditApellido.TrailingIcon = null;
            this.txtEditApellido.UseSystemPasswordChar = false;
            // 
            // txtEditDni
            // 
            this.txtEditDni.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtEditDni.AnimateReadOnly = false;
            this.txtEditDni.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.txtEditDni.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtEditDni.Depth = 0;
            this.txtEditDni.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtEditDni.HideSelection = true;
            this.txtEditDni.Hint = "DNI";
            this.txtEditDni.LeadingIcon = null;
            this.txtEditDni.Location = new System.Drawing.Point(118, 143);
            this.txtEditDni.MaxLength = 8;
            this.txtEditDni.MouseState = MaterialSkin.MouseState.OUT;
            this.txtEditDni.Name = "txtEditDni";
            this.txtEditDni.PasswordChar = '\0';
            this.txtEditDni.PrefixSuffixText = null;
            this.txtEditDni.ReadOnly = false;
            this.txtEditDni.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtEditDni.SelectedText = "";
            this.txtEditDni.SelectionLength = 0;
            this.txtEditDni.SelectionStart = 0;
            this.txtEditDni.ShortcutsEnabled = true;
            this.txtEditDni.Size = new System.Drawing.Size(174, 48);
            this.txtEditDni.TabIndex = 4;
            this.txtEditDni.TabStop = false;
            this.txtEditDni.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtEditDni.TrailingIcon = null;
            this.txtEditDni.UseSystemPasswordChar = false;
            // 
            // txtEditLegajo
            // 
            this.txtEditLegajo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtEditLegajo.AnimateReadOnly = false;
            this.txtEditLegajo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.txtEditLegajo.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtEditLegajo.Depth = 0;
            this.txtEditLegajo.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtEditLegajo.HideSelection = true;
            this.txtEditLegajo.Hint = "Legajo (= DNI)";
            this.txtEditLegajo.LeadingIcon = null;
            this.txtEditLegajo.Location = new System.Drawing.Point(118, 198);
            this.txtEditLegajo.MaxLength = 10;
            this.txtEditLegajo.MouseState = MaterialSkin.MouseState.OUT;
            this.txtEditLegajo.Name = "txtEditLegajo";
            this.txtEditLegajo.PasswordChar = '\0';
            this.txtEditLegajo.PrefixSuffixText = null;
            this.txtEditLegajo.ReadOnly = true;
            this.txtEditLegajo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtEditLegajo.SelectedText = "";
            this.txtEditLegajo.SelectionLength = 0;
            this.txtEditLegajo.SelectionStart = 0;
            this.txtEditLegajo.ShortcutsEnabled = true;
            this.txtEditLegajo.Size = new System.Drawing.Size(174, 48);
            this.txtEditLegajo.TabIndex = 5;
            this.txtEditLegajo.TabStop = false;
            this.txtEditLegajo.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtEditLegajo.TrailingIcon = null;
            this.txtEditLegajo.UseSystemPasswordChar = false;
            // 
            // txtEditCorreo
            // 
            this.txtEditCorreo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtEditCorreo.AnimateReadOnly = false;
            this.txtEditCorreo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.txtEditCorreo.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtEditCorreo.Depth = 0;
            this.txtEditCorreo.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtEditCorreo.HideSelection = true;
            this.txtEditCorreo.Hint = "Correo";
            this.txtEditCorreo.LeadingIcon = null;
            this.txtEditCorreo.Location = new System.Drawing.Point(118, 253);
            this.txtEditCorreo.MaxLength = 32767;
            this.txtEditCorreo.MouseState = MaterialSkin.MouseState.OUT;
            this.txtEditCorreo.Name = "txtEditCorreo";
            this.txtEditCorreo.PasswordChar = '\0';
            this.txtEditCorreo.PrefixSuffixText = null;
            this.txtEditCorreo.ReadOnly = false;
            this.txtEditCorreo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtEditCorreo.SelectedText = "";
            this.txtEditCorreo.SelectionLength = 0;
            this.txtEditCorreo.SelectionStart = 0;
            this.txtEditCorreo.ShortcutsEnabled = true;
            this.txtEditCorreo.Size = new System.Drawing.Size(174, 48);
            this.txtEditCorreo.TabIndex = 6;
            this.txtEditCorreo.TabStop = false;
            this.txtEditCorreo.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtEditCorreo.TrailingIcon = null;
            this.txtEditCorreo.UseSystemPasswordChar = false;
            // 
            // lblTelEdit
            // 
            this.lblTelEdit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTelEdit.AutoSize = true;
            this.lblTelEdit.Depth = 0;
            this.lblTelEdit.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblTelEdit.Location = new System.Drawing.Point(69, 21);
            this.lblTelEdit.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblTelEdit.Name = "lblTelEdit";
            this.lblTelEdit.Size = new System.Drawing.Size(64, 19);
            this.lblTelEdit.TabIndex = 11;
            this.lblTelEdit.Text = "Teléfono";
            // 
            // ctrlTelEdit
            // 
            this.ctrlTelEdit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ctrlTelEdit.Location = new System.Drawing.Point(205, 6);
            this.ctrlTelEdit.MinimumSize = new System.Drawing.Size(220, 48);
            this.ctrlTelEdit.Name = "ctrlTelEdit";
            this.ctrlTelEdit.Size = new System.Drawing.Size(220, 48);
            this.ctrlTelEdit.TabIndex = 7;
            this.ctrlTelEdit.Telefono = "";
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
            this.btnModificar.Location = new System.Drawing.Point(17, 14);
            this.btnModificar.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnModificar.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnModificar.Size = new System.Drawing.Size(100, 36);
            this.btnModificar.TabIndex = 8;
            this.btnModificar.Text = "Modificar";
            this.btnModificar.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnModificar.UseAccentColor = false;
            this.btnModificar.UseVisualStyleBackColor = true;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
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
            this.btnEliminar.Location = new System.Drawing.Point(152, 14);
            this.btnEliminar.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnEliminar.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnEliminar.Size = new System.Drawing.Size(100, 36);
            this.btnEliminar.TabIndex = 9;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Outlined;
            this.btnEliminar.UseAccentColor = false;
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
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
            this.btnLimpiarEditar.Location = new System.Drawing.Point(287, 14);
            this.btnLimpiarEditar.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnLimpiarEditar.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnLimpiarEditar.Name = "btnLimpiarEditar";
            this.btnLimpiarEditar.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnLimpiarEditar.Size = new System.Drawing.Size(100, 36);
            this.btnLimpiarEditar.TabIndex = 10;
            this.btnLimpiarEditar.Text = "Limpiar";
            this.btnLimpiarEditar.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Text;
            this.btnLimpiarEditar.UseAccentColor = false;
            this.btnLimpiarEditar.UseVisualStyleBackColor = true;
            this.btnLimpiarEditar.Click += new System.EventHandler(this.btnLimpiarEditar_Click);
            // 
            // lblEditando
            // 
            this.lblEditando.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblEditando.AutoSize = true;
            this.lblEditando.Depth = 0;
            this.lblEditando.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblEditando.Location = new System.Drawing.Point(91, 5);
            this.lblEditando.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblEditando.Name = "lblEditando";
            this.lblEditando.Size = new System.Drawing.Size(229, 19);
            this.lblEditando.TabIndex = 12;
            this.lblEditando.Text = "Editando: (seleccione de la lista)";
            // 
            // txtBuscar
            // 
            this.txtBuscar.AnimateReadOnly = false;
            this.txtBuscar.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBuscar.Depth = 0;
            this.txtBuscar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBuscar.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtBuscar.Hint = "Buscar profesor";
            this.txtBuscar.LeadingIcon = null;
            this.txtBuscar.Location = new System.Drawing.Point(3, 3);
            this.txtBuscar.MaxLength = 50;
            this.txtBuscar.MouseState = MaterialSkin.MouseState.OUT;
            this.txtBuscar.Multiline = false;
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(299, 50);
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
            this.btnBuscar.Location = new System.Drawing.Point(310, 8);
            this.btnBuscar.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnBuscar.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnBuscar.Size = new System.Drawing.Size(90, 31);
            this.btnBuscar.TabIndex = 1;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnBuscar.UseAccentColor = false;
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // dgvProfesores
            // 
            this.dgvProfesores.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProfesores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProfesores.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvProfesores.Location = new System.Drawing.Point(3, 56);
            this.dgvProfesores.Name = "dgvProfesores";
            this.dgvProfesores.Size = new System.Drawing.Size(405, 384);
            this.dgvProfesores.TabIndex = 1;
            this.dgvProfesores.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProfesores_CellClick);
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
            this.tabPage1.Size = new System.Drawing.Size(840, 512);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Crear un Profesor";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.txtNombre, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtApellido, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtDni, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtLegajo, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtCorreo, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.tableTelCrear, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 6);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(834, 410);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableTelCrear
            // 
            this.tableTelCrear.ColumnCount = 2;
            this.tableTelCrear.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableTelCrear.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableTelCrear.Controls.Add(this.lblTelCrear, 0, 0);
            this.tableTelCrear.Controls.Add(this.ctrlTelCrear, 1, 0);
            this.tableTelCrear.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableTelCrear.Location = new System.Drawing.Point(211, 273);
            this.tableTelCrear.Name = "tableTelCrear";
            this.tableTelCrear.RowCount = 1;
            this.tableTelCrear.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableTelCrear.Size = new System.Drawing.Size(411, 66);
            this.tableTelCrear.TabIndex = 5;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.btnGuardar, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnLimpiarCrear, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(227, 345);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(378, 62);
            this.tableLayoutPanel2.TabIndex = 6;
            // 
            // tabPage2
            // 
            this.tabPage2.AutoScroll = true;
            this.tabPage2.BackColor = System.Drawing.Color.White;
            this.tabPage2.Controls.Add(this.tableLayoutPanel8);
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(840, 512);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Modificar un Profesor";
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 2;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.Controls.Add(this.tableLayoutPanel5, 0, 0);
            this.tableLayoutPanel8.Controls.Add(this.tableLayoutPanel3, 1, 0);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 1;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(834, 449);
            this.tableLayoutPanel8.TabIndex = 15;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Controls.Add(this.lblEditando, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.txtEditNombre, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.txtEditCorreo, 0, 5);
            this.tableLayoutPanel5.Controls.Add(this.txtEditLegajo, 0, 4);
            this.tableLayoutPanel5.Controls.Add(this.txtEditDni, 0, 3);
            this.tableLayoutPanel5.Controls.Add(this.txtEditApellido, 0, 2);
            this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel6, 0, 6);
            this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel7, 0, 7);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 8;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 67F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 68F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(411, 443);
            this.tableLayoutPanel5.TabIndex = 14;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 2;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Controls.Add(this.lblTelEdit, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.ctrlTelEdit, 1, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 308);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(405, 61);
            this.tableLayoutPanel6.TabIndex = 13;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 3;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel7.Controls.Add(this.btnModificar, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.btnEliminar, 1, 0);
            this.tableLayoutPanel7.Controls.Add(this.btnLimpiarEditar, 2, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 375);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 1;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(405, 65);
            this.tableLayoutPanel7.TabIndex = 14;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.AutoScroll = true;
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.dgvProfesores, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(420, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 88F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(411, 443);
            this.tableLayoutPanel3.TabIndex = 13;
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
            this.tableLayoutPanel4.Size = new System.Drawing.Size(405, 47);
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
            this.materialTabSelector1.Location = new System.Drawing.Point(6, 5);
            this.materialTabSelector1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTabSelector1.Name = "materialTabSelector1";
            this.materialTabSelector1.Size = new System.Drawing.Size(848, 41);
            this.materialTabSelector1.TabIndex = 15;
            // 
            // FrmProfesores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(860, 600);
            this.Controls.Add(this.materialTabSelector1);
            this.Controls.Add(this.materialTabControl1);
            this.MinimumSize = new System.Drawing.Size(860, 600);
            this.Name = "FrmProfesores";
            this.Text = "Profesores";
            this.Load += new System.EventHandler(this.FrmProfesores_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProfesores)).EndInit();
            this.materialTabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableTelCrear.ResumeLayout(false);
            this.tableTelCrear.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MaterialSkin.Controls.MaterialTextBox2 txtNombre;
        private MaterialSkin.Controls.MaterialTextBox2 txtApellido;
        private MaterialSkin.Controls.MaterialTextBox2 txtDni;
        private MaterialSkin.Controls.MaterialTextBox2 txtLegajo;
        private MaterialSkin.Controls.MaterialTextBox2 txtCorreo;
        private MaterialSkin.Controls.MaterialLabel lblTelCrear;
        private SistemaAsistencia.Vista.Comun.CtrlTelefono ctrlTelCrear;
        private MaterialSkin.Controls.MaterialButton btnGuardar;
        private MaterialSkin.Controls.MaterialButton btnLimpiarCrear;
        private MaterialSkin.Controls.MaterialTextBox2 txtEditNombre;
        private MaterialSkin.Controls.MaterialTextBox2 txtEditApellido;
        private MaterialSkin.Controls.MaterialTextBox2 txtEditDni;
        private MaterialSkin.Controls.MaterialTextBox2 txtEditLegajo;
        private MaterialSkin.Controls.MaterialTextBox2 txtEditCorreo;
        private MaterialSkin.Controls.MaterialLabel lblTelEdit;
        private SistemaAsistencia.Vista.Comun.CtrlTelefono ctrlTelEdit;
        private MaterialSkin.Controls.MaterialButton btnModificar;
        private MaterialSkin.Controls.MaterialButton btnEliminar;
        private MaterialSkin.Controls.MaterialButton btnLimpiarEditar;
        private MaterialSkin.Controls.MaterialLabel lblEditando;
        private MaterialSkin.Controls.MaterialTextBox txtBuscar;
        private MaterialSkin.Controls.MaterialButton btnBuscar;
        private System.Windows.Forms.DataGridView dgvProfesores;
        private MaterialSkin.Controls.MaterialTabControl materialTabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableTelCrear;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private MaterialSkin.Controls.MaterialTabSelector materialTabSelector1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
    }
}
