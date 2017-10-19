namespace Controller
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbAirportName = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.tb_command = new System.Windows.Forms.TextBox();
            this.btnEnter = new System.Windows.Forms.Button();
            this.btn_taxi = new System.Windows.Forms.Button();
            this.btn_takeoff = new System.Windows.Forms.Button();
            this.btn_land = new System.Windows.Forms.Button();
            this.btn_abort = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblR4 = new System.Windows.Forms.Label();
            this.lblR3 = new System.Windows.Forms.Label();
            this.planeR3 = new System.Windows.Forms.PictureBox();
            this.planeR2 = new System.Windows.Forms.PictureBox();
            this.R3_red = new System.Windows.Forms.PictureBox();
            this.R3_green = new System.Windows.Forms.PictureBox();
            this.R2_red = new System.Windows.Forms.PictureBox();
            this.pictureBox10 = new System.Windows.Forms.PictureBox();
            this.pictureBox15 = new System.Windows.Forms.PictureBox();
            this.pictureBox14 = new System.Windows.Forms.PictureBox();
            this.lblR2 = new System.Windows.Forms.Label();
            this.lblR1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.planeR1 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.R1_red = new System.Windows.Forms.PictureBox();
            this.R1_green = new System.Windows.Forms.PictureBox();
            this.R2_green = new System.Windows.Forms.PictureBox();
            this.lb_message = new System.Windows.Forms.Label();
            this.listView_flight = new System.Windows.Forms.ListView();
            this.Flight = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Fuel = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_serverStatus = new System.Windows.Forms.Label();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker3 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker4 = new System.ComponentModel.BackgroundWorker();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.bwAbort = new System.ComponentModel.BackgroundWorker();
            this.timerEmergency = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.planeR3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.planeR2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.R3_red)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.R3_green)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.R2_red)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.planeR1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.R1_red)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.R1_green)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.R2_green)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DodgerBlue;
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.lbAirportName);
            this.panel2.Controls.Add(this.pictureBox2);
            this.panel2.Location = new System.Drawing.Point(0, 1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1013, 39);
            this.panel2.TabIndex = 1;
            // 
            // lbAirportName
            // 
            this.lbAirportName.AutoSize = true;
            this.lbAirportName.Font = new System.Drawing.Font("Arial Rounded MT Bold", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAirportName.ForeColor = System.Drawing.Color.White;
            this.lbAirportName.Location = new System.Drawing.Point(379, 6);
            this.lbAirportName.Name = "lbAirportName";
            this.lbAirportName.Size = new System.Drawing.Size(177, 29);
            this.lbAirportName.TabIndex = 3;
            this.lbAirportName.Text = "Airport Name";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(1, -4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(57, 48);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            // 
            // tb_command
            // 
            this.tb_command.BackColor = System.Drawing.SystemColors.ControlText;
            this.tb_command.Font = new System.Drawing.Font("Consolas", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_command.ForeColor = System.Drawing.Color.Green;
            this.tb_command.Location = new System.Drawing.Point(255, 559);
            this.tb_command.Name = "tb_command";
            this.tb_command.Size = new System.Drawing.Size(619, 39);
            this.tb_command.TabIndex = 2;
            this.tb_command.TextChanged += new System.EventHandler(this.tb_command_TextChanged);
            // 
            // btnEnter
            // 
            this.btnEnter.BackColor = System.Drawing.Color.SteelBlue;
            this.btnEnter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEnter.Location = new System.Drawing.Point(881, 519);
            this.btnEnter.Name = "btnEnter";
            this.btnEnter.Size = new System.Drawing.Size(108, 76);
            this.btnEnter.TabIndex = 3;
            this.btnEnter.Text = "Enter";
            this.btnEnter.UseVisualStyleBackColor = false;
            this.btnEnter.Visible = false;
            this.btnEnter.Click += new System.EventHandler(this.btnEnter_Click);
            // 
            // btn_taxi
            // 
            this.btn_taxi.Location = new System.Drawing.Point(255, 519);
            this.btn_taxi.Name = "btn_taxi";
            this.btn_taxi.Size = new System.Drawing.Size(111, 25);
            this.btn_taxi.TabIndex = 4;
            this.btn_taxi.Text = "taxi";
            this.btn_taxi.UseVisualStyleBackColor = true;
            this.btn_taxi.Click += new System.EventHandler(this.btn_taxi_Click);
            // 
            // btn_takeoff
            // 
            this.btn_takeoff.Location = new System.Drawing.Point(375, 519);
            this.btn_takeoff.Name = "btn_takeoff";
            this.btn_takeoff.Size = new System.Drawing.Size(111, 25);
            this.btn_takeoff.TabIndex = 5;
            this.btn_takeoff.Text = "takeoff";
            this.btn_takeoff.UseVisualStyleBackColor = true;
            this.btn_takeoff.Click += new System.EventHandler(this.btn_takeoff_Click);
            // 
            // btn_land
            // 
            this.btn_land.Location = new System.Drawing.Point(499, 519);
            this.btn_land.Name = "btn_land";
            this.btn_land.Size = new System.Drawing.Size(111, 25);
            this.btn_land.TabIndex = 6;
            this.btn_land.Text = "land";
            this.btn_land.UseVisualStyleBackColor = true;
            this.btn_land.Click += new System.EventHandler(this.btn_land_Click);
            // 
            // btn_abort
            // 
            this.btn_abort.Location = new System.Drawing.Point(625, 519);
            this.btn_abort.Name = "btn_abort";
            this.btn_abort.Size = new System.Drawing.Size(111, 25);
            this.btn_abort.TabIndex = 7;
            this.btn_abort.Text = "abort";
            this.btn_abort.UseVisualStyleBackColor = true;
            this.btn_abort.Click += new System.EventHandler(this.btn_abort_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(751, 519);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(124, 25);
            this.button6.TabIndex = 8;
            this.button6.Text = "clear command";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.SteelBlue;
            this.panel3.Controls.Add(this.lblR4);
            this.panel3.Controls.Add(this.lblR3);
            this.panel3.Controls.Add(this.planeR3);
            this.panel3.Controls.Add(this.planeR2);
            this.panel3.Controls.Add(this.R3_red);
            this.panel3.Controls.Add(this.R3_green);
            this.panel3.Controls.Add(this.R2_red);
            this.panel3.Controls.Add(this.pictureBox10);
            this.panel3.Controls.Add(this.pictureBox15);
            this.panel3.Controls.Add(this.pictureBox14);
            this.panel3.Controls.Add(this.lblR2);
            this.panel3.Controls.Add(this.lblR1);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.planeR1);
            this.panel3.Controls.Add(this.pictureBox3);
            this.panel3.Controls.Add(this.R1_red);
            this.panel3.Controls.Add(this.R1_green);
            this.panel3.Controls.Add(this.R2_green);
            this.panel3.Location = new System.Drawing.Point(255, 39);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(749, 460);
            this.panel3.TabIndex = 3;
            this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint);
            // 
            // lblR4
            // 
            this.lblR4.AutoSize = true;
            this.lblR4.Location = new System.Drawing.Point(64, 421);
            this.lblR4.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.lblR4.Name = "lblR4";
            this.lblR4.Size = new System.Drawing.Size(40, 17);
            this.lblR4.TabIndex = 31;
            this.lblR4.Text = "lblR4";
            // 
            // lblR3
            // 
            this.lblR3.AutoSize = true;
            this.lblR3.Location = new System.Drawing.Point(53, 271);
            this.lblR3.Name = "lblR3";
            this.lblR3.Size = new System.Drawing.Size(40, 17);
            this.lblR3.TabIndex = 19;
            this.lblR3.Text = "lblR3";
            // 
            // planeR3
            // 
            this.planeR3.Image = ((System.Drawing.Image)(resources.GetObject("planeR3.Image")));
            this.planeR3.Location = new System.Drawing.Point(51, 230);
            this.planeR3.Name = "planeR3";
            this.planeR3.Size = new System.Drawing.Size(33, 32);
            this.planeR3.TabIndex = 30;
            this.planeR3.TabStop = false;
            // 
            // planeR2
            // 
            this.planeR2.Image = ((System.Drawing.Image)(resources.GetObject("planeR2.Image")));
            this.planeR2.Location = new System.Drawing.Point(51, 122);
            this.planeR2.Name = "planeR2";
            this.planeR2.Size = new System.Drawing.Size(33, 32);
            this.planeR2.TabIndex = 29;
            this.planeR2.TabStop = false;
            // 
            // R3_red
            // 
            this.R3_red.Image = ((System.Drawing.Image)(resources.GetObject("R3_red.Image")));
            this.R3_red.Location = new System.Drawing.Point(48, 230);
            this.R3_red.Name = "R3_red";
            this.R3_red.Size = new System.Drawing.Size(707, 39);
            this.R3_red.TabIndex = 28;
            this.R3_red.TabStop = false;
            // 
            // R3_green
            // 
            this.R3_green.ErrorImage = null;
            this.R3_green.Image = ((System.Drawing.Image)(resources.GetObject("R3_green.Image")));
            this.R3_green.InitialImage = ((System.Drawing.Image)(resources.GetObject("R3_green.InitialImage")));
            this.R3_green.Location = new System.Drawing.Point(51, 230);
            this.R3_green.Name = "R3_green";
            this.R3_green.Size = new System.Drawing.Size(707, 39);
            this.R3_green.TabIndex = 27;
            this.R3_green.TabStop = false;
            this.R3_green.Click += new System.EventHandler(this.R3_green_Click);
            // 
            // R2_red
            // 
            this.R2_red.Image = ((System.Drawing.Image)(resources.GetObject("R2_red.Image")));
            this.R2_red.Location = new System.Drawing.Point(52, 122);
            this.R2_red.Name = "R2_red";
            this.R2_red.Size = new System.Drawing.Size(707, 39);
            this.R2_red.TabIndex = 25;
            this.R2_red.TabStop = false;
            // 
            // pictureBox10
            // 
            this.pictureBox10.Image = global::Controller.Properties.Resources.Plane_icon_RIGHT;
            this.pictureBox10.Location = new System.Drawing.Point(67, 388);
            this.pictureBox10.Margin = new System.Windows.Forms.Padding(1);
            this.pictureBox10.Name = "pictureBox10";
            this.pictureBox10.Size = new System.Drawing.Size(33, 32);
            this.pictureBox10.TabIndex = 12;
            this.pictureBox10.TabStop = false;
            this.pictureBox10.Click += new System.EventHandler(this.pictureBox10_Click);
            // 
            // pictureBox15
            // 
            this.pictureBox15.Image = global::Controller.Properties.Resources.Lane_horzontal_green;
            this.pictureBox15.Location = new System.Drawing.Point(67, 388);
            this.pictureBox15.Name = "pictureBox15";
            this.pictureBox15.Size = new System.Drawing.Size(707, 39);
            this.pictureBox15.TabIndex = 24;
            this.pictureBox15.TabStop = false;
            this.pictureBox15.Visible = false;
            this.pictureBox15.Click += new System.EventHandler(this.pictureBox15_Click);
            // 
            // pictureBox14
            // 
            this.pictureBox14.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox14.Image")));
            this.pictureBox14.Location = new System.Drawing.Point(67, 388);
            this.pictureBox14.Name = "pictureBox14";
            this.pictureBox14.Size = new System.Drawing.Size(707, 39);
            this.pictureBox14.TabIndex = 23;
            this.pictureBox14.TabStop = false;
            this.pictureBox14.Visible = false;
            // 
            // lblR2
            // 
            this.lblR2.AutoSize = true;
            this.lblR2.Location = new System.Drawing.Point(45, 163);
            this.lblR2.Name = "lblR2";
            this.lblR2.Size = new System.Drawing.Size(40, 17);
            this.lblR2.TabIndex = 18;
            this.lblR2.Text = "lblR2";
            // 
            // lblR1
            // 
            this.lblR1.AutoSize = true;
            this.lblR1.Location = new System.Drawing.Point(83, 84);
            this.lblR1.Name = "lblR1";
            this.lblR1.Size = new System.Drawing.Size(40, 17);
            this.lblR1.TabIndex = 17;
            this.lblR1.Text = "lblR1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Cyan;
            this.label5.Location = new System.Drawing.Point(308, 352);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(106, 25);
            this.label5.TabIndex = 16;
            this.label5.Text = "SERVICE";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Cyan;
            this.label4.Location = new System.Drawing.Point(5, 230);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 25);
            this.label4.TabIndex = 15;
            this.label4.Text = "R3";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Cyan;
            this.label3.Location = new System.Drawing.Point(7, 122);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 25);
            this.label3.TabIndex = 14;
            this.label3.Text = "R2";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Cyan;
            this.label2.Location = new System.Drawing.Point(8, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 25);
            this.label2.TabIndex = 13;
            this.label2.Text = "R1";
            // 
            // planeR1
            // 
            this.planeR1.Image = ((System.Drawing.Image)(resources.GetObject("planeR1.Image")));
            this.planeR1.Location = new System.Drawing.Point(67, 51);
            this.planeR1.Name = "planeR1";
            this.planeR1.Size = new System.Drawing.Size(33, 32);
            this.planeR1.TabIndex = 10;
            this.planeR1.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(67, 388);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(692, 39);
            this.pictureBox3.TabIndex = 4;
            this.pictureBox3.TabStop = false;
            // 
            // R1_red
            // 
            this.R1_red.Image = ((System.Drawing.Image)(resources.GetObject("R1_red.Image")));
            this.R1_red.Location = new System.Drawing.Point(51, 51);
            this.R1_red.Name = "R1_red";
            this.R1_red.Size = new System.Drawing.Size(707, 39);
            this.R1_red.TabIndex = 7;
            this.R1_red.TabStop = false;
            this.R1_red.Click += new System.EventHandler(this.pictureBox6_Click);
            // 
            // R1_green
            // 
            this.R1_green.ErrorImage = null;
            this.R1_green.Image = ((System.Drawing.Image)(resources.GetObject("R1_green.Image")));
            this.R1_green.InitialImage = ((System.Drawing.Image)(resources.GetObject("R1_green.InitialImage")));
            this.R1_green.Location = new System.Drawing.Point(51, 51);
            this.R1_green.Name = "R1_green";
            this.R1_green.Size = new System.Drawing.Size(707, 39);
            this.R1_green.TabIndex = 20;
            this.R1_green.TabStop = false;
            this.R1_green.Click += new System.EventHandler(this.pictureBox11_Click);
            // 
            // R2_green
            // 
            this.R2_green.ErrorImage = null;
            this.R2_green.Image = ((System.Drawing.Image)(resources.GetObject("R2_green.Image")));
            this.R2_green.InitialImage = ((System.Drawing.Image)(resources.GetObject("R2_green.InitialImage")));
            this.R2_green.Location = new System.Drawing.Point(51, 122);
            this.R2_green.Name = "R2_green";
            this.R2_green.Size = new System.Drawing.Size(707, 39);
            this.R2_green.TabIndex = 26;
            this.R2_green.TabStop = false;
            this.R2_green.Click += new System.EventHandler(this.R2_green_Click);
            // 
            // lb_message
            // 
            this.lb_message.AutoSize = true;
            this.lb_message.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_message.Location = new System.Drawing.Point(299, 596);
            this.lb_message.Name = "lb_message";
            this.lb_message.Size = new System.Drawing.Size(18, 20);
            this.lb_message.TabIndex = 17;
            this.lb_message.Text = "|";
            // 
            // listView_flight
            // 
            this.listView_flight.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Flight,
            this.Fuel,
            this.Status});
            this.listView_flight.Location = new System.Drawing.Point(8, 83);
            this.listView_flight.Name = "listView_flight";
            this.listView_flight.Size = new System.Drawing.Size(263, 419);
            this.listView_flight.TabIndex = 4;
            this.listView_flight.UseCompatibleStateImageBehavior = false;
            this.listView_flight.View = System.Windows.Forms.View.Details;
            this.listView_flight.SelectedIndexChanged += new System.EventHandler(this.listView_flight_SelectedIndexChanged);
            // 
            // Flight
            // 
            this.Flight.Text = "Flight";
            this.Flight.Width = 62;
            // 
            // Fuel
            // 
            this.Fuel.Text = "Fuel";
            this.Fuel.Width = 46;
            // 
            // Status
            // 
            this.Status.Text = "Status";
            this.Status.Width = 114;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 580);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 17);
            this.label1.TabIndex = 18;
            this.label1.Text = "Server Status: ";
            // 
            // lbl_serverStatus
            // 
            this.lbl_serverStatus.AutoSize = true;
            this.lbl_serverStatus.BackColor = System.Drawing.Color.Red;
            this.lbl_serverStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lbl_serverStatus.ForeColor = System.Drawing.Color.White;
            this.lbl_serverStatus.Location = new System.Drawing.Point(109, 580);
            this.lbl_serverStatus.Name = "lbl_serverStatus";
            this.lbl_serverStatus.Size = new System.Drawing.Size(23, 20);
            this.lbl_serverStatus.TabIndex = 18;
            this.lbl_serverStatus.Text = "O";
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // backgroundWorker4
            // 
            this.backgroundWorker4.WorkerReportsProgress = true;
            this.backgroundWorker4.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker4_DoWork);
            this.backgroundWorker4.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker4_ProgressChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(125, 49);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(107, 29);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // timerEmergency
            // 
            this.timerEmergency.Interval = 1000;
            this.timerEmergency.Tick += new System.EventHandler(this.timerEmergency_Tick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(70, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(162, 30);
            this.button1.TabIndex = 4;
            this.button1.Text = "Save To File";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(1003, 620);
            this.Controls.Add(this.lbl_serverStatus);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.listView_flight);
            this.Controls.Add(this.lb_message);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.btnEnter);
            this.Controls.Add(this.btn_abort);
            this.Controls.Add(this.btn_taxi);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.tb_command);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.btn_land);
            this.Controls.Add(this.btn_takeoff);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Air Traffic Controller";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.planeR3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.planeR2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.R3_red)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.R3_green)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.R2_red)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.planeR1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.R1_red)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.R1_green)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.R2_green)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.TextBox tb_command;
        private System.Windows.Forms.Button btnEnter;
        private System.Windows.Forms.Button btn_taxi;
        private System.Windows.Forms.Button btn_takeoff;
        private System.Windows.Forms.Button btn_land;
        private System.Windows.Forms.Button btn_abort;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lbAirportName;
        private System.Windows.Forms.Label lb_message;
        private System.Windows.Forms.ListView listView_flight;
        private System.Windows.Forms.ColumnHeader Flight;
        private System.Windows.Forms.ColumnHeader Fuel;
        private System.Windows.Forms.ColumnHeader Status;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox R1_red;
        private System.Windows.Forms.PictureBox planeR1;
        private System.Windows.Forms.PictureBox pictureBox10;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_serverStatus;
        private System.Windows.Forms.Timer timer2;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.ComponentModel.BackgroundWorker backgroundWorker3;
        private System.ComponentModel.BackgroundWorker backgroundWorker4;
        private System.Windows.Forms.Label lblR3;
        private System.Windows.Forms.Label lblR2;
        private System.Windows.Forms.Label lblR1;
        private System.Windows.Forms.PictureBox R1_green;
        private System.Windows.Forms.PictureBox pictureBox15;
        private System.Windows.Forms.PictureBox pictureBox14;
        private System.ComponentModel.BackgroundWorker bwAbort;
        private System.Windows.Forms.PictureBox planeR2;
        private System.Windows.Forms.PictureBox R3_red;
        private System.Windows.Forms.PictureBox R3_green;
        private System.Windows.Forms.PictureBox R2_red;
        private System.Windows.Forms.PictureBox R2_green;
        private System.Windows.Forms.PictureBox planeR3;
        private System.Windows.Forms.Label lblR4;
        private System.Windows.Forms.Timer timerEmergency;
        private System.Windows.Forms.Button button1;
    }
}

