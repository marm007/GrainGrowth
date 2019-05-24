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
            this.radialNeighbourhood_UpDown = new System.Windows.Forms.NumericUpDown();
            this.radialNeighbourhood_RadioButton = new System.Windows.Forms.RadioButton();
            this.pentagonalLabel = new System.Windows.Forms.Label();
            this.hexagonalComboBox = new System.Windows.Forms.ComboBox();
            this.pentagonalRadioButton = new System.Windows.Forms.RadioButton();
            this.hexagonalRadioButton = new System.Windows.Forms.RadioButton();
            this.moore_button = new System.Windows.Forms.RadioButton();
            this.neumann_button = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.periodic_button = new System.Windows.Forms.RadioButton();
            this.nonperiodic_button = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.alertTextBox = new System.Windows.Forms.TextBox();
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
            this.energyCheckBox = new System.Windows.Forms.CheckBox();
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
            this.hexagonalToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.monteCarloStopButton = new System.Windows.Forms.Button();
            this.monteCarloEnergyButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.monteCarloIterationsUpDown = new System.Windows.Forms.NumericUpDown();
            this.monteCarlo_Button = new System.Windows.Forms.Button();
            this.iterationLabel = new System.Windows.Forms.Label();
            this.kTNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.JNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radialNeighbourhood_UpDown)).BeginInit();
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
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.monteCarloIterationsUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kTNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.JNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(720, 481);
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
            this.groupBox2.Controls.Add(this.radialNeighbourhood_UpDown);
            this.groupBox2.Controls.Add(this.radialNeighbourhood_RadioButton);
            this.groupBox2.Controls.Add(this.pentagonalLabel);
            this.groupBox2.Controls.Add(this.hexagonalComboBox);
            this.groupBox2.Controls.Add(this.pentagonalRadioButton);
            this.groupBox2.Controls.Add(this.hexagonalRadioButton);
            this.groupBox2.Controls.Add(this.moore_button);
            this.groupBox2.Controls.Add(this.neumann_button);
            this.groupBox2.Location = new System.Drawing.Point(3, 112);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(186, 149);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Neighbourhood";
            // 
            // radialNeighbourhood_UpDown
            // 
            this.radialNeighbourhood_UpDown.DecimalPlaces = 1;
            this.radialNeighbourhood_UpDown.Enabled = false;
            this.radialNeighbourhood_UpDown.Location = new System.Drawing.Point(107, 106);
            this.radialNeighbourhood_UpDown.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.radialNeighbourhood_UpDown.Name = "radialNeighbourhood_UpDown";
            this.radialNeighbourhood_UpDown.Size = new System.Drawing.Size(64, 20);
            this.radialNeighbourhood_UpDown.TabIndex = 10;
            this.radialNeighbourhood_UpDown.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.radialNeighbourhood_UpDown.ValueChanged += new System.EventHandler(this.radialNegihbourhoodUpDown_ValueChanged);
            // 
            // radialNeighbourhood_RadioButton
            // 
            this.radialNeighbourhood_RadioButton.AutoSize = true;
            this.radialNeighbourhood_RadioButton.Location = new System.Drawing.Point(6, 109);
            this.radialNeighbourhood_RadioButton.Name = "radialNeighbourhood_RadioButton";
            this.radialNeighbourhood_RadioButton.Size = new System.Drawing.Size(55, 17);
            this.radialNeighbourhood_RadioButton.TabIndex = 9;
            this.radialNeighbourhood_RadioButton.TabStop = true;
            this.radialNeighbourhood_RadioButton.Text = "Radial";
            this.radialNeighbourhood_RadioButton.UseVisualStyleBackColor = true;
            this.radialNeighbourhood_RadioButton.CheckedChanged += new System.EventHandler(this.radialNeighbourhood_RadioButton_CheckedChanged);
            // 
            // pentagonalLabel
            // 
            this.pentagonalLabel.AutoSize = true;
            this.pentagonalLabel.Location = new System.Drawing.Point(24, 68);
            this.pentagonalLabel.Name = "pentagonalLabel";
            this.pentagonalLabel.Size = new System.Drawing.Size(0, 13);
            this.pentagonalLabel.TabIndex = 7;
            this.pentagonalLabel.Visible = false;
            // 
            // hexagonalComboBox
            // 
            this.hexagonalComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.hexagonalComboBox.Enabled = false;
            this.hexagonalComboBox.FormattingEnabled = true;
            this.hexagonalComboBox.Location = new System.Drawing.Point(107, 68);
            this.hexagonalComboBox.Name = "hexagonalComboBox";
            this.hexagonalComboBox.Size = new System.Drawing.Size(72, 21);
            this.hexagonalComboBox.TabIndex = 6;
            this.hexagonalComboBox.SelectedIndexChanged += new System.EventHandler(this.hexagonalComboBox_SelectedItemChanged);
            // 
            // pentagonalRadioButton
            // 
            this.pentagonalRadioButton.AutoSize = true;
            this.pentagonalRadioButton.Location = new System.Drawing.Point(7, 45);
            this.pentagonalRadioButton.Name = "pentagonalRadioButton";
            this.pentagonalRadioButton.Size = new System.Drawing.Size(79, 17);
            this.pentagonalRadioButton.TabIndex = 5;
            this.pentagonalRadioButton.Text = "Pentagonal";
            this.pentagonalRadioButton.UseVisualStyleBackColor = true;
            this.pentagonalRadioButton.CheckedChanged += new System.EventHandler(this.pentagonalRadioButton_CheckedChanged);
            this.pentagonalRadioButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pentagonalRadioButton_Click);
            // 
            // hexagonalRadioButton
            // 
            this.hexagonalRadioButton.AutoSize = true;
            this.hexagonalRadioButton.Location = new System.Drawing.Point(107, 45);
            this.hexagonalRadioButton.Name = "hexagonalRadioButton";
            this.hexagonalRadioButton.Size = new System.Drawing.Size(76, 17);
            this.hexagonalRadioButton.TabIndex = 4;
            this.hexagonalRadioButton.TabStop = true;
            this.hexagonalRadioButton.Text = "Hexagonal";
            this.hexagonalRadioButton.UseVisualStyleBackColor = true;
            this.hexagonalRadioButton.CheckedChanged += new System.EventHandler(this.hexagonalRadioButton_CheckedChanged);
            // 
            // moore_button
            // 
            this.moore_button.AutoSize = true;
            this.moore_button.Location = new System.Drawing.Point(107, 22);
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
            this.groupBox3.Location = new System.Drawing.Point(3, 267);
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
            this.groupBox4.Controls.Add(this.alertTextBox);
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
            this.groupBox4.Location = new System.Drawing.Point(3, 348);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(186, 210);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Nucleation";
            // 
            // alertTextBox
            // 
            this.alertTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.alertTextBox.Location = new System.Drawing.Point(3, 184);
            this.alertTextBox.Name = "alertTextBox";
            this.alertTextBox.Size = new System.Drawing.Size(180, 20);
            this.alertTextBox.TabIndex = 15;
            this.alertTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.alertTextBox.Visible = false;
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
            this.tabControl1.Size = new System.Drawing.Size(200, 590);
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
            this.tabPage1.Size = new System.Drawing.Size(192, 564);
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
            this.tabPage2.Size = new System.Drawing.Size(192, 564);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Settings";
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.energyCheckBox);
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
            this.groupBox5.Size = new System.Drawing.Size(180, 238);
            this.groupBox5.TabIndex = 3;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Data";
            // 
            // energyCheckBox
            // 
            this.energyCheckBox.AutoSize = true;
            this.energyCheckBox.Location = new System.Drawing.Point(75, 212);
            this.energyCheckBox.Name = "energyCheckBox";
            this.energyCheckBox.Size = new System.Drawing.Size(59, 17);
            this.energyCheckBox.TabIndex = 10;
            this.energyCheckBox.Text = "Energy";
            this.energyCheckBox.UseVisualStyleBackColor = true;
            this.energyCheckBox.CheckedChanged += new System.EventHandler(this.energyCheckBox_CheckedChanged);
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
            // groupBox6
            // 
            this.groupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox6.Controls.Add(this.label13);
            this.groupBox6.Controls.Add(this.JNumericUpDown);
            this.groupBox6.Controls.Add(this.label11);
            this.groupBox6.Controls.Add(this.kTNumericUpDown);
            this.groupBox6.Controls.Add(this.iterationLabel);
            this.groupBox6.Controls.Add(this.monteCarloStopButton);
            this.groupBox6.Controls.Add(this.monteCarloEnergyButton);
            this.groupBox6.Controls.Add(this.label1);
            this.groupBox6.Controls.Add(this.monteCarloIterationsUpDown);
            this.groupBox6.Controls.Add(this.monteCarlo_Button);
            this.groupBox6.Location = new System.Drawing.Point(12, 499);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(263, 104);
            this.groupBox6.TabIndex = 7;
            this.groupBox6.TabStop = false;
            // 
            // monteCarloStopButton
            // 
            this.monteCarloStopButton.Location = new System.Drawing.Point(87, 12);
            this.monteCarloStopButton.Name = "monteCarloStopButton";
            this.monteCarloStopButton.Size = new System.Drawing.Size(75, 23);
            this.monteCarloStopButton.TabIndex = 5;
            this.monteCarloStopButton.Text = "Stop";
            this.monteCarloStopButton.UseVisualStyleBackColor = true;
            this.monteCarloStopButton.Click += new System.EventHandler(this.monteCarloStopButton_Click);
            // 
            // monteCarloEnergyButton
            // 
            this.monteCarloEnergyButton.Location = new System.Drawing.Point(168, 12);
            this.monteCarloEnergyButton.Name = "monteCarloEnergyButton";
            this.monteCarloEnergyButton.Size = new System.Drawing.Size(89, 23);
            this.monteCarloEnergyButton.TabIndex = 4;
            this.monteCarloEnergyButton.Text = "Energy / ON";
            this.monteCarloEnergyButton.UseVisualStyleBackColor = true;
            this.monteCarloEnergyButton.Click += new System.EventHandler(this.monteCarloEnergyButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(87, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "iterations";
            // 
            // monteCarloIterationsUpDown
            // 
            this.monteCarloIterationsUpDown.Location = new System.Drawing.Point(6, 44);
            this.monteCarloIterationsUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.monteCarloIterationsUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.monteCarloIterationsUpDown.Name = "monteCarloIterationsUpDown";
            this.monteCarloIterationsUpDown.Size = new System.Drawing.Size(75, 20);
            this.monteCarloIterationsUpDown.TabIndex = 1;
            this.monteCarloIterationsUpDown.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.monteCarloIterationsUpDown.ValueChanged += new System.EventHandler(this.monteCarloIterationsUpDown_ValueChanged);
            // 
            // monteCarlo_Button
            // 
            this.monteCarlo_Button.Location = new System.Drawing.Point(6, 12);
            this.monteCarlo_Button.Name = "monteCarlo_Button";
            this.monteCarlo_Button.Size = new System.Drawing.Size(75, 23);
            this.monteCarlo_Button.TabIndex = 0;
            this.monteCarlo_Button.Text = "MonteCarlo";
            this.monteCarlo_Button.UseVisualStyleBackColor = true;
            this.monteCarlo_Button.Click += new System.EventHandler(this.monteCarlo_Button_Click);
            // 
            // iterationLabel
            // 
            this.iterationLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.iterationLabel.AutoSize = true;
            this.iterationLabel.Location = new System.Drawing.Point(165, 46);
            this.iterationLabel.Name = "iterationLabel";
            this.iterationLabel.Size = new System.Drawing.Size(70, 13);
            this.iterationLabel.TabIndex = 7;
            this.iterationLabel.Text = "0 / 2 iteration";
            // 
            // kTNumericUpDown
            // 
            this.kTNumericUpDown.DecimalPlaces = 1;
            this.kTNumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.kTNumericUpDown.Location = new System.Drawing.Point(6, 71);
            this.kTNumericUpDown.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            65536});
            this.kTNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.kTNumericUpDown.Name = "kTNumericUpDown";
            this.kTNumericUpDown.Size = new System.Drawing.Size(75, 20);
            this.kTNumericUpDown.TabIndex = 8;
            this.kTNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.kTNumericUpDown.ValueChanged += new System.EventHandler(this.kTNumericUpDown_ValueChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(87, 73);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(20, 13);
            this.label11.TabIndex = 9;
            this.label11.Text = "kT";
            // 
            // JNumericUpDown
            // 
            this.JNumericUpDown.DecimalPlaces = 1;
            this.JNumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.JNumericUpDown.Location = new System.Drawing.Point(168, 73);
            this.JNumericUpDown.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.JNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.JNumericUpDown.Name = "JNumericUpDown";
            this.JNumericUpDown.Size = new System.Drawing.Size(75, 20);
            this.JNumericUpDown.TabIndex = 10;
            this.JNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.JNumericUpDown.ValueChanged += new System.EventHandler(this.JNumericUpDown_ValueChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(249, 75);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(12, 13);
            this.label13.TabIndex = 11;
            this.label13.Text = "J";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 611);
            this.Controls.Add(this.groupBox6);
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
            ((System.ComponentModel.ISupportInitialize)(this.radialNeighbourhood_UpDown)).EndInit();
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
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.monteCarloIterationsUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kTNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.JNumericUpDown)).EndInit();
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
        private System.Windows.Forms.TextBox alertTextBox;
        private System.Windows.Forms.RadioButton pentagonalRadioButton;
        private System.Windows.Forms.RadioButton hexagonalRadioButton;
        private System.Windows.Forms.Label pentagonalLabel;
        private System.Windows.Forms.ComboBox hexagonalComboBox;
        private System.Windows.Forms.ToolTip hexagonalToolTip;
        private System.Windows.Forms.CheckBox energyCheckBox;
        private System.Windows.Forms.NumericUpDown radialNeighbourhood_UpDown;
        private System.Windows.Forms.RadioButton radialNeighbourhood_RadioButton;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown monteCarloIterationsUpDown;
        private System.Windows.Forms.Button monteCarlo_Button;
        private System.Windows.Forms.Button monteCarloStopButton;
        private System.Windows.Forms.Button monteCarloEnergyButton;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown kTNumericUpDown;
        private System.Windows.Forms.Label iterationLabel;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.NumericUpDown JNumericUpDown;
    }
}

