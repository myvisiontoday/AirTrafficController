namespace Pilot
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
            this.button1 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btn_LandingRequest = new System.Windows.Forms.Button();
            this.lb_flightNumber = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lb_status = new System.Windows.Forms.Label();
            this.lb_fuel = new System.Windows.Forms.Label();
            this.lb_message = new System.Windows.Forms.Label();
            this.pb_fuelLevel = new System.Windows.Forms.ProgressBar();
            this.label4 = new System.Windows.Forms.Label();
            this.lbl_serverStatus = new System.Windows.Forms.Label();
            this.Take_Off_Request = new System.Windows.Forms.Button();
            this.Cancel_Request = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.Undock = new System.Windows.Forms.Button();
            this.timerProgress = new System.Windows.Forms.Timer(this.components);
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Red;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.button1.Location = new System.Drawing.Point(553, 412);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(221, 45);
            this.button1.TabIndex = 4;
            this.button1.Text = "Emergency";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DodgerBlue;
            this.panel2.Controls.Add(this.pictureBox2);
            this.panel2.Location = new System.Drawing.Point(1, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(833, 39);
            this.panel2.TabIndex = 2;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Pilot.Properties.Resources.plane_symbol;
            this.pictureBox2.Location = new System.Drawing.Point(1, -4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(57, 48);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            // 
            // btn_LandingRequest
            // 
            this.btn_LandingRequest.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_LandingRequest.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btn_LandingRequest.Location = new System.Drawing.Point(244, 342);
            this.btn_LandingRequest.Name = "btn_LandingRequest";
            this.btn_LandingRequest.Size = new System.Drawing.Size(235, 77);
            this.btn_LandingRequest.TabIndex = 7;
            this.btn_LandingRequest.Text = "Land request";
            this.btn_LandingRequest.UseVisualStyleBackColor = true;
            this.btn_LandingRequest.Click += new System.EventHandler(this.btn_landingrequest_Click);
            // 
            // lb_flightNumber
            // 
            this.lb_flightNumber.AutoSize = true;
            this.lb_flightNumber.BackColor = System.Drawing.Color.White;
            this.lb_flightNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_flightNumber.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lb_flightNumber.Location = new System.Drawing.Point(55, 71);
            this.lb_flightNumber.Name = "lb_flightNumber";
            this.lb_flightNumber.Size = new System.Drawing.Size(133, 25);
            this.lb_flightNumber.TabIndex = 8;
            this.lb_flightNumber.Text = "flightNumber";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(29, 143);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 17);
            this.label2.TabIndex = 11;
            this.label2.Text = "Airport ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(109, 345);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 17);
            this.label3.TabIndex = 12;
            this.label3.Text = "Fuel Level";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Pilot.Properties.Resources.plane;
            this.pictureBox1.Location = new System.Drawing.Point(343, 57);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(465, 256);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.White;
            this.pictureBox3.Image = global::Pilot.Properties.Resources.ProCP_logo;
            this.pictureBox3.Location = new System.Drawing.Point(16, 412);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(108, 29);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 13;
            this.pictureBox3.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(29, 115);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 17);
            this.label1.TabIndex = 10;
            this.label1.Text = "Status : ";
            // 
            // lb_status
            // 
            this.lb_status.AutoSize = true;
            this.lb_status.ForeColor = System.Drawing.Color.White;
            this.lb_status.Location = new System.Drawing.Point(93, 115);
            this.lb_status.Name = "lb_status";
            this.lb_status.Size = new System.Drawing.Size(59, 17);
            this.lb_status.TabIndex = 14;
            this.lb_status.Text = "Landing";
            // 
            // lb_fuel
            // 
            this.lb_fuel.AutoSize = true;
            this.lb_fuel.Font = new System.Drawing.Font("Consolas", 13.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_fuel.Location = new System.Drawing.Point(116, 295);
            this.lb_fuel.Name = "lb_fuel";
            this.lb_fuel.Size = new System.Drawing.Size(64, 28);
            this.lb_fuel.TabIndex = 15;
            this.lb_fuel.Text = "00 %";
            // 
            // lb_message
            // 
            this.lb_message.AutoSize = true;
            this.lb_message.Font = new System.Drawing.Font("Consolas", 13.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_message.Location = new System.Drawing.Point(163, 433);
            this.lb_message.Name = "lb_message";
            this.lb_message.Size = new System.Drawing.Size(25, 28);
            this.lb_message.TabIndex = 16;
            this.lb_message.Text = "|";
            // 
            // pb_fuelLevel
            // 
            this.pb_fuelLevel.Location = new System.Drawing.Point(113, 326);
            this.pb_fuelLevel.Name = "pb_fuelLevel";
            this.pb_fuelLevel.Size = new System.Drawing.Size(67, 15);
            this.pb_fuelLevel.TabIndex = 17;
            this.pb_fuelLevel.Value = 50;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.Control;
            this.label4.Location = new System.Drawing.Point(19, 451);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 17);
            this.label4.TabIndex = 18;
            this.label4.Text = "Server Status: ";
            // 
            // lbl_serverStatus
            // 
            this.lbl_serverStatus.AutoSize = true;
            this.lbl_serverStatus.BackColor = System.Drawing.Color.Red;
            this.lbl_serverStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_serverStatus.ForeColor = System.Drawing.Color.White;
            this.lbl_serverStatus.Location = new System.Drawing.Point(119, 448);
            this.lbl_serverStatus.Name = "lbl_serverStatus";
            this.lbl_serverStatus.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lbl_serverStatus.Size = new System.Drawing.Size(23, 20);
            this.lbl_serverStatus.TabIndex = 19;
            this.lbl_serverStatus.Text = "O";
            // 
            // Take_Off_Request
            // 
            this.Take_Off_Request.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Take_Off_Request.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Take_Off_Request.Location = new System.Drawing.Point(244, 342);
            this.Take_Off_Request.Name = "Take_Off_Request";
            this.Take_Off_Request.Size = new System.Drawing.Size(235, 77);
            this.Take_Off_Request.TabIndex = 7;
            this.Take_Off_Request.Text = "Take off Request";
            this.Take_Off_Request.UseVisualStyleBackColor = true;
            this.Take_Off_Request.Click += new System.EventHandler(this.Take_Off_Request_Click);
            // 
            // Cancel_Request
            // 
            this.Cancel_Request.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cancel_Request.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Cancel_Request.Location = new System.Drawing.Point(244, 425);
            this.Cancel_Request.Name = "Cancel_Request";
            this.Cancel_Request.Size = new System.Drawing.Size(235, 49);
            this.Cancel_Request.TabIndex = 7;
            this.Cancel_Request.Text = "Cancel Request";
            this.Cancel_Request.UseVisualStyleBackColor = true;
            this.Cancel_Request.Click += new System.EventHandler(this.Cancel_Request_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // Undock
            // 
            this.Undock.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.Undock.Location = new System.Drawing.Point(553, 358);
            this.Undock.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Undock.Name = "Undock";
            this.Undock.Size = new System.Drawing.Size(221, 44);
            this.Undock.TabIndex = 20;
            this.Undock.Text = "Undock";
            this.Undock.UseVisualStyleBackColor = true;
            this.Undock.Click += new System.EventHandler(this.Undock_Click);
            // 
            // timerProgress
            // 
            this.timerProgress.Interval = 3000;
            this.timerProgress.Tick += new System.EventHandler(this.timerProgress_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.ClientSize = new System.Drawing.Size(832, 479);
            this.Controls.Add(this.Undock);
            this.Controls.Add(this.lbl_serverStatus);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pb_fuelLevel);
            this.Controls.Add(this.lb_message);
            this.Controls.Add(this.lb_fuel);
            this.Controls.Add(this.lb_status);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lb_flightNumber);
            this.Controls.Add(this.Cancel_Request);
            this.Controls.Add(this.Take_Off_Request);
            this.Controls.Add(this.btn_LandingRequest);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btn_LandingRequest;
        private System.Windows.Forms.Label lb_flightNumber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lb_status;
        private System.Windows.Forms.Label lb_fuel;
        private System.Windows.Forms.Label lb_message;
        private System.Windows.Forms.ProgressBar pb_fuelLevel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbl_serverStatus;
        private System.Windows.Forms.Button Take_Off_Request;
        private System.Windows.Forms.Button Cancel_Request;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Button Undock;
        private System.Windows.Forms.Timer timerProgress;
    }
}

