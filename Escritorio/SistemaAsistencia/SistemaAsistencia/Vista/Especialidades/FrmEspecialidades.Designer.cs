namespace SistemaAsistencia.Vista.Especialidades
{
    partial class FrmEspecialidades
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
            this.lblAvisoCrear = new MaterialSkin.Controls.MaterialLabel();
            this.txtNombre = new MaterialSkin.Controls.MaterialTextBox2();
            this.btnGuardar = new MaterialSkin.Controls.MaterialButton();
            this.btnLimpiarCrear = new MaterialSkin.Controls.MaterialButton();
            this.lblEditando = new MaterialSkin.Controls.MaterialLabel();
            this.txtEditNombre = new MaterialSkin.Controls.MaterialTextBox2();
            this.lblEditDivision = new MaterialSkin.Controls.MaterialLabel();
            this.txtBuscar = new MaterialSkin.Controls.MaterialTextBox();
            this.btnToggleActivo = new MaterialSkin.Controls.MaterialButton();
            this.dgvEspecialidades = new System.Windows.Forms.DataGridView();
            this.materialTabControl1 = new MaterialSkin.Controls.MaterialTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.btnLimpiarEditar = new MaterialSkin.Controls.MaterialButton();
            this.nudEditDivision = new System.Windows.Forms.NumericUpDown();
            this.btnGuardarDivision = new MaterialSkin.Controls.MaterialButton();
            this.materialTabSelector1 = new MaterialSkin.Controls.MaterialTabSelector();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEspecialidades)).BeginInit();
            this.materialTabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudEditDivision)).BeginInit();
            this.SuspendLayout();
            // 
            // lblAvisoCrear
            // 
            this.lblAvisoCrear.AutoSize = true;
            this.lblAvisoCrear.Depth = 0;
            this.lblAvisoCrear.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAvisoCrear.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblAvisoCrear.Location = new System.Drawing.Point(211, 0);
            this.lblAvisoCrear.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblAvisoCrear.Name = "lblAvisoCrear";
            this.lblAvisoCrear.Size = new System.Drawing.Size(411, 19);
            this.lblAvisoCrear.TabIndex = 0;
            this.lblAvisoCrear.Text = "Catálogo fijo: no se crean especialidades a mano.";
            this.lblAvisoCrear.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.txtNombre.Location = new System.Drawing.Point(291, 22);
            this.txtNombre.MaxLength = 32767;
            this.txtNombre.MouseState = MaterialSkin.MouseState.OUT;
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.PasswordChar = '\0';
            this.txtNombre.PrefixSuffixText = null;
            this.txtNombre.ReadOnly = true;
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
            // btnGuardar
            // 
            this.btnGuardar.AutoSize = false;
            this.btnGuardar.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnGuardar.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnGuardar.Depth = 0;
            this.btnGuardar.HighEmphasis = true;
            this.btnGuardar.Icon = null;
            this.btnGuardar.Location = new System.Drawing.Point(4, 79);
            this.btnGuardar.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnGuardar.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnGuardar.Size = new System.Drawing.Size(105, 32);
            this.btnGuardar.TabIndex = 1;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnGuardar.UseAccentColor = false;
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Visible = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnLimpiarCrear
            // 
            this.btnLimpiarCrear.AutoSize = false;
            this.btnLimpiarCrear.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnLimpiarCrear.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnLimpiarCrear.Depth = 0;
            this.btnLimpiarCrear.HighEmphasis = true;
            this.btnLimpiarCrear.Icon = null;
            this.btnLimpiarCrear.Location = new System.Drawing.Point(212, 79);
            this.btnLimpiarCrear.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnLimpiarCrear.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnLimpiarCrear.Name = "btnLimpiarCrear";
            this.btnLimpiarCrear.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnLimpiarCrear.Size = new System.Drawing.Size(105, 32);
            this.btnLimpiarCrear.TabIndex = 2;
            this.btnLimpiarCrear.Text = "Limpiar";
            this.btnLimpiarCrear.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Text;
            this.btnLimpiarCrear.UseAccentColor = false;
            this.btnLimpiarCrear.UseVisualStyleBackColor = true;
            this.btnLimpiarCrear.Visible = false;
            this.btnLimpiarCrear.Click += new System.EventHandler(this.btnLimpiarCrear_Click);
            // 
            // lblEditando
            // 
            this.lblEditando.AutoSize = true;
            this.lblEditando.BackColor = System.Drawing.Color.White;
            this.lblEditando.Depth = 0;
            this.lblEditando.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblEditando.Location = new System.Drawing.Point(20, 15);
            this.lblEditando.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblEditando.Name = "lblEditando";
            this.lblEditando.Size = new System.Drawing.Size(229, 19);
            this.lblEditando.TabIndex = 3;
            this.lblEditando.Text = "Editando: (seleccione de la lista)";
            this.lblEditando.Visible = false;
            // 
            // txtEditNombre
            // 
            this.txtEditNombre.AnimateReadOnly = false;
            this.txtEditNombre.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.txtEditNombre.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtEditNombre.Depth = 0;
            this.txtEditNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtEditNombre.HideSelection = true;
            this.txtEditNombre.Hint = "Nombre";
            this.txtEditNombre.LeadingIcon = null;
            this.txtEditNombre.Location = new System.Drawing.Point(95, 45);
            this.txtEditNombre.MaxLength = 32767;
            this.txtEditNombre.MouseState = MaterialSkin.MouseState.OUT;
            this.txtEditNombre.Name = "txtEditNombre";
            this.txtEditNombre.PasswordChar = '\0';
            this.txtEditNombre.PrefixSuffixText = null;
            this.txtEditNombre.ReadOnly = true;
            this.txtEditNombre.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtEditNombre.SelectedText = "";
            this.txtEditNombre.SelectionLength = 0;
            this.txtEditNombre.SelectionStart = 0;
            this.txtEditNombre.ShortcutsEnabled = true;
            this.txtEditNombre.Size = new System.Drawing.Size(180, 48);
            this.txtEditNombre.TabIndex = 2;
            this.txtEditNombre.TabStop = false;
            this.txtEditNombre.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtEditNombre.TrailingIcon = null;
            this.txtEditNombre.UseSystemPasswordChar = false;
            this.txtEditNombre.Visible = false;
            // 
            // lblEditDivision
            // 
            this.lblEditDivision.AutoSize = true;
            this.lblEditDivision.BackColor = System.Drawing.Color.White;
            this.lblEditDivision.Depth = 0;
            this.lblEditDivision.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblEditDivision.Location = new System.Drawing.Point(20, 160);
            this.lblEditDivision.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblEditDivision.Name = "lblEditDivision";
            this.lblEditDivision.Size = new System.Drawing.Size(129, 19);
            this.lblEditDivision.TabIndex = 5;
            this.lblEditDivision.Text = "División (0 = libre)";
            this.lblEditDivision.Visible = false;
            // 
            // txtBuscar
            // 
            this.txtBuscar.AnimateReadOnly = false;
            this.txtBuscar.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBuscar.Depth = 0;
            this.txtBuscar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBuscar.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtBuscar.Hint = "Buscar especialidad";
            this.txtBuscar.LeadingIcon = null;
            this.txtBuscar.Location = new System.Drawing.Point(3, 3);
            this.txtBuscar.MaxLength = 50;
            this.txtBuscar.MouseState = MaterialSkin.MouseState.OUT;
            this.txtBuscar.Multiline = false;
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(353, 50);
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
            this.btnToggleActivo.Location = new System.Drawing.Point(364, 10);
            this.btnToggleActivo.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnToggleActivo.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnToggleActivo.Name = "btnToggleActivo";
            this.btnToggleActivo.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnToggleActivo.Size = new System.Drawing.Size(90, 34);
            this.btnToggleActivo.TabIndex = 1;
            this.btnToggleActivo.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnToggleActivo.UseAccentColor = false;
            this.btnToggleActivo.UseVisualStyleBackColor = true;
            this.btnToggleActivo.Click += new System.EventHandler(this.btnToggleActivo_Click);
            // 
            // dgvEspecialidades
            // 
            this.dgvEspecialidades.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEspecialidades.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEspecialidades.Location = new System.Drawing.Point(3, 63);
            this.dgvEspecialidades.Name = "dgvEspecialidades";
            this.dgvEspecialidades.Size = new System.Drawing.Size(459, 437);
            this.dgvEspecialidades.TabIndex = 1;
            this.dgvEspecialidades.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEspecialidades_CellClick);
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
            this.materialTabControl1.Location = new System.Drawing.Point(6, 50);
            this.materialTabControl1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTabControl1.Multiline = true;
            this.materialTabControl1.Name = "materialTabControl1";
            this.materialTabControl1.SelectedIndex = 0;
            this.materialTabControl1.Size = new System.Drawing.Size(848, 544);
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
            this.tabPage1.Size = new System.Drawing.Size(840, 514);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Crear una Especialidad";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.btnGuardar, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnLimpiarCrear, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblAvisoCrear, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtNombre, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(834, 117);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.AutoScroll = true;
            this.tabPage2.BackColor = System.Drawing.Color.White;
            this.tabPage2.Controls.Add(this.tableLayoutPanel3);
            this.tabPage2.Controls.Add(this.lblEditando);
            this.tabPage2.Controls.Add(this.txtEditNombre);
            this.tabPage2.Controls.Add(this.btnLimpiarEditar);
            this.tabPage2.Controls.Add(this.lblEditDivision);
            this.tabPage2.Controls.Add(this.nudEditDivision);
            this.tabPage2.Controls.Add(this.btnGuardarDivision);
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(840, 514);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Consultar Especialidades";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.dgvEspecialidades, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 0, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(366, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 88F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(465, 503);
            this.tableLayoutPanel3.TabIndex = 4;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel4.Controls.Add(this.txtBuscar, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnToggleActivo, 1, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(459, 54);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // btnLimpiarEditar
            // 
            this.btnLimpiarEditar.AutoSize = false;
            this.btnLimpiarEditar.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnLimpiarEditar.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnLimpiarEditar.Depth = 0;
            this.btnLimpiarEditar.HighEmphasis = true;
            this.btnLimpiarEditar.Icon = null;
            this.btnLimpiarEditar.Location = new System.Drawing.Point(23, 210);
            this.btnLimpiarEditar.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnLimpiarEditar.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnLimpiarEditar.Name = "btnLimpiarEditar";
            this.btnLimpiarEditar.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnLimpiarEditar.Size = new System.Drawing.Size(100, 36);
            this.btnLimpiarEditar.TabIndex = 3;
            this.btnLimpiarEditar.Text = "Limpiar";
            this.btnLimpiarEditar.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Text;
            this.btnLimpiarEditar.UseAccentColor = false;
            this.btnLimpiarEditar.UseVisualStyleBackColor = true;
            this.btnLimpiarEditar.Visible = false;
            this.btnLimpiarEditar.Click += new System.EventHandler(this.btnLimpiarEditar_Click);
            // 
            // nudEditDivision
            // 
            this.nudEditDivision.Location = new System.Drawing.Point(155, 157);
            this.nudEditDivision.Maximum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.nudEditDivision.Name = "nudEditDivision";
            this.nudEditDivision.Size = new System.Drawing.Size(180, 25);
            this.nudEditDivision.TabIndex = 6;
            this.nudEditDivision.Visible = false;
            // 
            // btnGuardarDivision
            // 
            this.btnGuardarDivision.AutoSize = false;
            this.btnGuardarDivision.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnGuardarDivision.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnGuardarDivision.Depth = 0;
            this.btnGuardarDivision.HighEmphasis = true;
            this.btnGuardarDivision.Icon = null;
            this.btnGuardarDivision.Location = new System.Drawing.Point(155, 210);
            this.btnGuardarDivision.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnGuardarDivision.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnGuardarDivision.Name = "btnGuardarDivision";
            this.btnGuardarDivision.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnGuardarDivision.Size = new System.Drawing.Size(180, 36);
            this.btnGuardarDivision.TabIndex = 7;
            this.btnGuardarDivision.Text = "Guardar división";
            this.btnGuardarDivision.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnGuardarDivision.UseAccentColor = false;
            this.btnGuardarDivision.UseVisualStyleBackColor = true;
            this.btnGuardarDivision.Visible = false;
            // 
            // materialTabSelector1
            // 
            this.materialTabSelector1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.materialTabSelector1.BaseTabControl = this.materialTabControl1;
            this.materialTabSelector1.CharacterCasing = MaterialSkin.Controls.MaterialTabSelector.CustomCharacterCasing.Normal;
            this.materialTabSelector1.Depth = 0;
            this.materialTabSelector1.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialTabSelector1.Location = new System.Drawing.Point(10, 3);
            this.materialTabSelector1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTabSelector1.Name = "materialTabSelector1";
            this.materialTabSelector1.Size = new System.Drawing.Size(844, 41);
            this.materialTabSelector1.TabIndex = 15;
            this.materialTabSelector1.Click += new System.EventHandler(this.materialTabSelector1_Click);
            // 
            // FrmEspecialidades
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(860, 600);
            this.Controls.Add(this.materialTabSelector1);
            this.Controls.Add(this.materialTabControl1);
            this.MinimumSize = new System.Drawing.Size(860, 600);
            this.Name = "FrmEspecialidades";
            this.Text = "Especialidades";
            this.Load += new System.EventHandler(this.FrmEspecialidades_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEspecialidades)).EndInit();
            this.materialTabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudEditDivision)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MaterialSkin.Controls.MaterialLabel lblAvisoCrear;
        private MaterialSkin.Controls.MaterialTextBox2 txtNombre;
        private MaterialSkin.Controls.MaterialButton btnGuardar;
        private MaterialSkin.Controls.MaterialButton btnLimpiarCrear;
        private MaterialSkin.Controls.MaterialLabel lblEditando;
        private MaterialSkin.Controls.MaterialTextBox2 txtEditNombre;
        private MaterialSkin.Controls.MaterialLabel lblEditDivision;
        private MaterialSkin.Controls.MaterialTextBox txtBuscar;
        private MaterialSkin.Controls.MaterialButton btnToggleActivo;
        private System.Windows.Forms.DataGridView dgvEspecialidades;
        private MaterialSkin.Controls.MaterialTabControl materialTabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private MaterialSkin.Controls.MaterialTabSelector materialTabSelector1;
        private MaterialSkin.Controls.MaterialButton btnLimpiarEditar;
        private System.Windows.Forms.NumericUpDown nudEditDivision;
        private MaterialSkin.Controls.MaterialButton btnGuardarDivision;
    }
}
