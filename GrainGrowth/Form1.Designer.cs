namespace GrainGrowth
{
    partial class Form1
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.step_button = new System.Windows.Forms.Button();
            this.clear_button = new System.Windows.Forms.Button();
            this.stop_button = new System.Windows.Forms.Button();
            this.start_button = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.moore_button = new System.Windows.Forms.RadioButton();
            this.neumann_button = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.periodic_button = new System.Windows.Forms.RadioButton();
            this.nonperiodic_button = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.random_button = new System.Windows.Forms.Button();
            this.radial_button = new System.Windows.Forms.Button();
            this.homogeneus_button = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.numberRandomUpDown = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numberRadialUpDown = new System.Windows.Forms.NumericUpDown();
            this.radiusUpDown = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.colUpDown = new System.Windows.Forms.NumericUpDown();
            this.rowUpDown = new System.Windows.Forms.NumericUpDown();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.gridCheckBox = new System.Windows.Forms.CheckBox();
            this.speedTracBar = new System.Windows.Forms.TrackBar();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cellSizeTracBar = new System.Windows.Forms.TrackBar();
            this.applySettings = new System.Windows.Forms.Button();
            this.heightBox = new System.Windows.Forms.TextBox();
            this.widthBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numberRandomUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numberRadialUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radiusUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.colUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rowUpDown)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.speedTracBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cellSizeTracBar)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(720, 541);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.Picturebox1_Paint);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.step_button);
            this.groupBox1.Controls.Add(this.clear_button);
            this.groupBox1.Controls.Add(this.stop_button);
            this.groupBox1.Controls.Add(this.start_button);
            this.groupBox1.Location = new System.Drawing.Point(3, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(186, 100);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Controls";
            // 
            // step_button
            // 
            this.step_button.Location = new System.Drawing.Point(88, 59);
            this.step_button.Name = "step_button";
            this.step_button.Size = new System.Drawing.Size(75, 23);
            this.step_button.TabIndex = 3;
            this.step_button.Text = "Step";
            this.step_button.UseVisualStyleBackColor = true;
            this.step_button.Click += new System.EventHandler(this.step_button_Click);
            // 
            // clear_button
            // 
            this.clear_button.Location = new System.Drawing.Point(7, 59);
            this.clear_button.Name = "clear_button";
            this.clear_button.Size = new System.Drawing.Size(75, 23);
            this.clear_button.TabIndex = 2;
            this.clear_button.Text = "Clear";
            this.clear_button.UseVisualStyleBackColor = true;
            this.clear_button.Click += new System.EventHandler(this.clear_button_Click);
            // 
            // stop_button
            // 
            this.stop_button.Location = new System.Drawing.Point(88, 30);
            this.stop_button.Name = "stop_button";
            this.stop_button.Size = new System.Drawing.Size(75, 23);
            this.stop_button.TabIndex = 1;
            this.stop_button.Text = "Stop";
            this.stop_button.UseVisualStyleBackColor = true;
            this.stop_button.Click += new System.EventHandler(this.stop_button_Click);
            // 
            // start_button
            // 
            this.start_button.Location = new System.Drawing.Point(7, 30);
            this.start_button.Name = "start_button";
            this.start_button.Size = new System.Drawing.Size(75, 23);
            this.start_button.TabIndex = 0;
            this.start_button.Text = "Start";
            this.start_button.UseVisualStyleBackColor = true;
            this.start_button.Click += new System.EventHandler(this.start_button_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.moore_button);
            this.groupBox2.Controls.Add(this.neumann_button);
            this.groupBox2.Location = new System.Drawing.Point(3, 112);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(186, 100);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Neighbourhood";
            // 
            // moore_button
            // 
            this.moore_button.AutoSize = true;
            this.moore_button.Location = new System.Drawing.Point(7, 45);
            this.moore_button.Name = "moore_button";
            this.moore_button.Size = new System.Drawing.Size(55, 17);
            this.moore_button.TabIndex = 3;
            this.moore_button.TabStop = true;
            this.moore_button.Text = "Moore";
            this.moore_button.UseVisualStyleBackColor = true;
            this.moore_button.CheckedChanged += new System.EventHandler(this.moore_button_CheckedChanged);
            // 
            // neumann_button
            // 
            this.neumann_button.AutoSize = true;
            this.neumann_button.Checked = true;
            this.neumann_button.Location = new System.Drawing.Point(7, 22);
            this.neumann_button.Name = "neumann_button";
            this.neumann_button.Size = new System.Drawing.Size(92, 17);
            this.neumann_button.TabIndex = 2;
            this.neumann_button.TabStop = true;
            this.neumann_button.Text = "von Neumann";
            this.neumann_button.UseVisualStyleBackColor = true;
            this.neumann_button.CheckedChanged += new System.EventHandler(this.neumann_button_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.periodic_button);
            this.groupBox3.Controls.Add(this.nonperiodic_button);
            this.groupBox3.Location = new System.Drawing.Point(3, 218);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(186, 75);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Boundary condition";
            // 
            // periodic_button
            // 
            this.periodic_button.AutoSize = true;
            this.periodic_button.Checked = true;
            this.periodic_button.Location = new System.Drawing.Point(7, 44);
            this.periodic_button.Name = "periodic_button";
            this.periodic_button.Size = new System.Drawing.Size(63, 17);
            this.periodic_button.TabIndex = 1;
            this.periodic_button.TabStop = true;
            this.periodic_button.Text = "Periodic";
            this.periodic_button.UseVisualStyleBackColor = true;
            this.periodic_button.CheckedChanged += new System.EventHandler(this.periodic_button_CheckedChanged);
            // 
            // nonperiodic_button
            // 
            this.nonperiodic_button.AutoSize = true;
            this.nonperiodic_button.Location = new System.Drawing.Point(7, 20);
            this.nonperiodic_button.Name = "nonperiodic_button";
            this.nonperiodic_button.Size = new System.Drawing.Size(82, 17);
            this.nonperiodic_button.TabIndex = 0;
            this.nonperiodic_button.TabStop = true;
            this.nonperiodic_button.Text = "Nonperiodic";
            this.nonperiodic_button.UseVisualStyleBackColor = true;
            this.nonperiodic_button.CheckedChanged += new System.EventHandler(this.nonperiodic_button_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.random_button);
            this.groupBox4.Controls.Add(this.radial_button);
            this.groupBox4.Controls.Add(this.homogeneus_button);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.numberRandomUpDown);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.numberRadialUpDown);
            this.groupBox4.Controls.Add(this.radiusUpDown);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.colUpDown);
            this.groupBox4.Controls.Add(this.rowUpDown);
            this.groupBox4.Location = new System.Drawing.Point(3, 299);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(186, 178);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Nucleation";
            // 
            // random_button
            // 
            this.random_button.Location = new System.Drawing.Point(7, 129);
            this.random_button.Name = "random_button";
            this.random_button.Size = new System.Drawing.Size(82, 23);
            this.random_button.TabIndex = 14;
            this.random_button.Text = "Random";
            this.random_button.UseVisualStyleBackColor = true;
            this.random_button.Click += new System.EventHandler(this.random_button_Click);
            // 
            // radial_button
            // 
            this.radial_button.Location = new System.Drawing.Point(7, 75);
            this.radial_button.Name = "radial_button";
            this.radial_button.Size = new System.Drawing.Size(82, 23);
            this.radial_button.TabIndex = 13;
            this.radial_button.Text = "Radial";
            this.radial_button.UseVisualStyleBackColor = true;
            this.radial_button.Click += new System.EventHandler(this.radial_button_Click);
            // 
            // homogeneus_button
            // 
            this.homogeneus_button.Location = new System.Drawing.Point(7, 19);
            this.homogeneus_button.Name = "homogeneus_button";
            this.homogeneus_button.Size = new System.Drawing.Size(82, 23);
            this.homogeneus_button.TabIndex = 7;
            this.homogeneus_button.Text = "Homogeneus";
            this.homogeneus_button.UseVisualStyleBackColor = true;
            this.homogeneus_button.Click += new System.EventHandler(this.homogeneus_button_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(95, 156);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Num";
            // 
            // numberRandomUpDown
            // 
            this.numberRandomUpDown.Location = new System.Drawing.Point(95, 133);
            this.numberRandomUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numberRandomUpDown.Name = "numberRandomUpDown";
            this.numberRandomUpDown.Size = new System.Drawing.Size(39, 20);
            this.numberRandomUpDown.TabIndex = 11;
            this.numberRandomUpDown.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(141, 98);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Num";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(95, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(10, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "r";
            // 
            // numberRadialUpDown
            // 
            this.numberRadialUpDown.Location = new System.Drawing.Point(140, 75);
            this.numberRadialUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numberRadialUpDown.Name = "numberRadialUpDown";
            this.numberRadialUpDown.Size = new System.Drawing.Size(39, 20);
            this.numberRadialUpDown.TabIndex = 5;
            this.numberRadialUpDown.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // radiusUpDown
            // 
            this.radiusUpDown.Location = new System.Drawing.Point(95, 75);
            this.radiusUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.radiusUpDown.Name = "radiusUpDown";
            this.radiusUpDown.Size = new System.Drawing.Size(39, 20);
            this.radiusUpDown.TabIndex = 8;
            this.radiusUpDown.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(141, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Col";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(95, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Row";
            // 
            // colUpDown
            // 
            this.colUpDown.Location = new System.Drawing.Point(140, 19);
            this.colUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.colUpDown.Name = "colUpDown";
            this.colUpDown.Size = new System.Drawing.Size(39, 20);
            this.colUpDown.TabIndex = 5;
            this.colUpDown.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // rowUpDown
            // 
            this.rowUpDown.Location = new System.Drawing.Point(95, 19);
            this.rowUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.rowUpDown.Name = "rowUpDown";
            this.rowUpDown.Size = new System.Drawing.Size(39, 20);
            this.rowUpDown.TabIndex = 4;
            this.rowUpDown.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(738, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(200, 541);
            this.tabControl1.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.groupBox4);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(192, 515);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Controls";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.groupBox5);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(192, 515);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Settings";
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.gridCheckBox);
            this.groupBox5.Controls.Add(this.speedTracBar);
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Controls.Add(this.cellSizeTracBar);
            this.groupBox5.Controls.Add(this.applySettings);
            this.groupBox5.Controls.Add(this.heightBox);
            this.groupBox5.Controls.Add(this.widthBox);
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Controls.Add(this.label10);
            this.groupBox5.Location = new System.Drawing.Point(6, 6);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(180, 244);
            this.groupBox5.TabIndex = 3;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Data";
            // 
            // gridCheckBox
            // 
            this.gridCheckBox.AutoSize = true;
            this.gridCheckBox.Location = new System.Drawing.Point(18, 212);
            this.gridCheckBox.Name = "gridCheckBox";
            this.gridCheckBox.Size = new System.Drawing.Size(45, 17);
            this.gridCheckBox.TabIndex = 9;
            this.gridCheckBox.Text = "Grid";
            this.gridCheckBox.UseVisualStyleBackColor = true;
            this.gridCheckBox.CheckedChanged += new System.EventHandler(this.gridCheckBox_CheckedChanged);
            // 
            // speedTracBar
            // 
            this.speedTracBar.Location = new System.Drawing.Point(62, 169);
            this.speedTracBar.Maximum = 200;
            this.speedTracBar.Name = "speedTracBar";
            this.speedTracBar.Size = new System.Drawing.Size(112, 45);
            this.speedTracBar.TabIndex = 8;
            this.speedTracBar.Value = 10;
            this.speedTracBar.Scroll += new System.EventHandler(this.speedTracBar_Scroll);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 169);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Speed";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(18, 118);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "Cell size";
            // 
            // cellSizeTracBar
            // 
            this.cellSizeTracBar.LargeChange = 0;
            this.cellSizeTracBar.Location = new System.Drawing.Point(62, 118);
            this.cellSizeTracBar.Minimum = 1;
            this.cellSizeTracBar.Name = "cellSizeTracBar";
            this.cellSizeTracBar.Size = new System.Drawing.Size(112, 45);
            this.cellSizeTracBar.TabIndex = 5;
            this.cellSizeTracBar.Value = 5;
            this.cellSizeTracBar.Scroll += new System.EventHandler(this.cellSizeTracBar_Scroll);
            // 
            // applySettings
            // 
            this.applySettings.Location = new System.Drawing.Point(59, 76);
            this.applySettings.Name = "applySettings";
            this.applySettings.Size = new System.Drawing.Size(75, 23);
            this.applySettings.TabIndex = 4;
            this.applySettings.Text = "Apply";
            this.applySettings.UseVisualStyleBackColor = true;
            this.applySettings.Click += new System.EventHandler(this.applySettings_Click);
            // 
            // heightBox
            // 
            this.heightBox.Location = new System.Drawing.Point(59, 49);
            this.heightBox.Name = "heightBox";
            this.heightBox.Size = new System.Drawing.Size(100, 20);
            this.heightBox.TabIndex = 3;
            // 
            // widthBox
            // 
            this.widthBox.Location = new System.Drawing.Point(59, 21);
            this.widthBox.Name = "widthBox";
            this.widthBox.Size = new System.Drawing.Size(100, 20);
            this.widthBox.TabIndex = 2;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(18, 24);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 13);
            this.label9.TabIndex = 1;
            this.label9.Text = "Width";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(15, 52);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(38, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "Height";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 562);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_Closing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResizeBegin += new System.EventHandler(this.Form1_ResizeBegin);
            this.ResizeEnd += new System.EventHandler(this.Form1_ResizeEnd);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numberRandomUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numberRadialUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radiusUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.colUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rowUpDown)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.speedTracBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cellSizeTracBar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button step_button;
        private System.Windows.Forms.Button clear_button;
        private System.Windows.Forms.Button stop_button;
        private System.Windows.Forms.Button start_button;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton moore_button;
        private System.Windows.Forms.RadioButton neumann_button;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton periodic_button;
        private System.Windows.Forms.RadioButton nonperiodic_button;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numberRadialUpDown;
        private System.Windows.Forms.NumericUpDown radiusUpDown;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown colUpDown;
        private System.Windows.Forms.NumericUpDown rowUpDown;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numberRandomUpDown;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TrackBar speedTracBar;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TrackBar cellSizeTracBar;
        private System.Windows.Forms.Button applySettings;
        private System.Windows.Forms.TextBox heightBox;
        private System.Windows.Forms.TextBox widthBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button homogeneus_button;
        private System.Windows.Forms.Button radial_button;
        private System.Windows.Forms.Button random_button;
        private System.Windows.Forms.CheckBox gridCheckBox;
    }
}

