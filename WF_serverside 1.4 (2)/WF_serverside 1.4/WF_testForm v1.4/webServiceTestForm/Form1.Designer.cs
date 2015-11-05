namespace webServiceTestForm
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
            this.btnTestService = new System.Windows.Forms.Button();
            this.btnTestDB = new System.Windows.Forms.Button();
            this.btnGetRooms = new System.Windows.Forms.Button();
            this.txtRoomCampus = new System.Windows.Forms.TextBox();
            this.lblRoomCampus = new System.Windows.Forms.Label();
            this.btnGetCampuses = new System.Windows.Forms.Button();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.txtPath_Waypoint = new System.Windows.Forms.TextBox();
            this.lblPath_Waypoint = new System.Windows.Forms.Label();
            this.rbPath_Dis = new System.Windows.Forms.RadioButton();
            this.btnResolvePath = new System.Windows.Forms.Button();
            this.btnTestImg = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.TestImgIndex = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.RoomName = new System.Windows.Forms.TextBox();
            this.dbImage = new System.Windows.Forms.PictureBox();
            this.Left = new System.Windows.Forms.Button();
            this.right = new System.Windows.Forms.Button();
            this.labelCampusVer = new System.Windows.Forms.Label();
            this.txtCampusVer = new System.Windows.Forms.TextBox();
            this.btnGetCampusVer = new System.Windows.Forms.Button();
            this.labelGetService = new System.Windows.Forms.Label();
            this.txtGetService = new System.Windows.Forms.TextBox();
            this.btnGetService = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dbImage)).BeginInit();
            this.SuspendLayout();
            // 
            // btnTestService
            // 
            this.btnTestService.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTestService.Location = new System.Drawing.Point(45, 33);
            this.btnTestService.Name = "btnTestService";
            this.btnTestService.Size = new System.Drawing.Size(106, 23);
            this.btnTestService.TabIndex = 0;
            this.btnTestService.Text = "Test Service Conn";
            this.btnTestService.UseVisualStyleBackColor = true;
            this.btnTestService.Click += new System.EventHandler(this.btnTestService_Click);
            // 
            // btnTestDB
            // 
            this.btnTestDB.Location = new System.Drawing.Point(45, 62);
            this.btnTestDB.Name = "btnTestDB";
            this.btnTestDB.Size = new System.Drawing.Size(106, 23);
            this.btnTestDB.TabIndex = 1;
            this.btnTestDB.Text = "Test DB Conn";
            this.btnTestDB.UseVisualStyleBackColor = true;
            this.btnTestDB.Click += new System.EventHandler(this.btnTestDB_Click);
            // 
            // btnGetRooms
            // 
            this.btnGetRooms.Location = new System.Drawing.Point(45, 234);
            this.btnGetRooms.Name = "btnGetRooms";
            this.btnGetRooms.Size = new System.Drawing.Size(106, 23);
            this.btnGetRooms.TabIndex = 5;
            this.btnGetRooms.Text = "Get Room Data";
            this.btnGetRooms.UseVisualStyleBackColor = true;
            this.btnGetRooms.Click += new System.EventHandler(this.btnGetRooms_Click);
            // 
            // txtRoomCampus
            // 
            this.txtRoomCampus.Location = new System.Drawing.Point(126, 263);
            this.txtRoomCampus.Name = "txtRoomCampus";
            this.txtRoomCampus.Size = new System.Drawing.Size(46, 20);
            this.txtRoomCampus.TabIndex = 7;
            this.txtRoomCampus.Text = "PE";
            this.txtRoomCampus.TextChanged += new System.EventHandler(this.txtRoom_Campus_TextChanged);
            // 
            // lblRoomCampus
            // 
            this.lblRoomCampus.AutoSize = true;
            this.lblRoomCampus.Location = new System.Drawing.Point(45, 265);
            this.lblRoomCampus.Name = "lblRoomCampus";
            this.lblRoomCampus.Size = new System.Drawing.Size(75, 13);
            this.lblRoomCampus.TabIndex = 8;
            this.lblRoomCampus.Text = "Campus code:";
            // 
            // btnGetCampuses
            // 
            this.btnGetCampuses.Location = new System.Drawing.Point(45, 119);
            this.btnGetCampuses.Name = "btnGetCampuses";
            this.btnGetCampuses.Size = new System.Drawing.Size(106, 23);
            this.btnGetCampuses.TabIndex = 11;
            this.btnGetCampuses.Text = "Get Campus Data";
            this.btnGetCampuses.UseVisualStyleBackColor = true;
            this.btnGetCampuses.Click += new System.EventHandler(this.btnGetCampuses_Click);
            // 
            // txtOutput
            // 
            this.txtOutput.AcceptsReturn = true;
            this.txtOutput.AcceptsTab = true;
            this.txtOutput.AllowDrop = true;
            this.txtOutput.Location = new System.Drawing.Point(275, 33);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ReadOnly = true;
            this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtOutput.Size = new System.Drawing.Size(608, 287);
            this.txtOutput.TabIndex = 13;
            this.txtOutput.TabStop = false;
            this.txtOutput.WordWrap = false;
            // 
            // txtPath_Waypoint
            // 
            this.txtPath_Waypoint.Location = new System.Drawing.Point(117, 408);
            this.txtPath_Waypoint.Name = "txtPath_Waypoint";
            this.txtPath_Waypoint.Size = new System.Drawing.Size(100, 20);
            this.txtPath_Waypoint.TabIndex = 21;
            this.txtPath_Waypoint.Text = "3";
            // 
            // lblPath_Waypoint
            // 
            this.lblPath_Waypoint.AutoSize = true;
            this.lblPath_Waypoint.Location = new System.Drawing.Point(48, 411);
            this.lblPath_Waypoint.Name = "lblPath_Waypoint";
            this.lblPath_Waypoint.Size = new System.Drawing.Size(63, 13);
            this.lblPath_Waypoint.TabIndex = 22;
            this.lblPath_Waypoint.Text = "WaypointID";
            // 
            // rbPath_Dis
            // 
            this.rbPath_Dis.AutoSize = true;
            this.rbPath_Dis.Location = new System.Drawing.Point(48, 434);
            this.rbPath_Dis.Name = "rbPath_Dis";
            this.rbPath_Dis.Size = new System.Drawing.Size(103, 17);
            this.rbPath_Dis.TabIndex = 23;
            this.rbPath_Dis.TabStop = true;
            this.rbPath_Dis.Text = "Disabled access";
            this.rbPath_Dis.UseVisualStyleBackColor = true;
            // 
            // btnResolvePath
            // 
            this.btnResolvePath.Location = new System.Drawing.Point(45, 379);
            this.btnResolvePath.Name = "btnResolvePath";
            this.btnResolvePath.Size = new System.Drawing.Size(106, 23);
            this.btnResolvePath.TabIndex = 16;
            this.btnResolvePath.Text = "ResolvePath()";
            this.btnResolvePath.UseVisualStyleBackColor = true;
            this.btnResolvePath.Click += new System.EventHandler(this.btnResolvePath_Click);
            // 
            // btnTestImg
            // 
            this.btnTestImg.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnTestImg.Location = new System.Drawing.Point(45, 494);
            this.btnTestImg.Name = "btnTestImg";
            this.btnTestImg.Size = new System.Drawing.Size(106, 23);
            this.btnTestImg.TabIndex = 25;
            this.btnTestImg.Text = "TestImg";
            this.btnTestImg.UseVisualStyleBackColor = true;
            this.btnTestImg.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(48, 526);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 27;
            this.label1.Text = "Test[]Index";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // TestImgIndex
            // 
            this.TestImgIndex.Location = new System.Drawing.Point(117, 523);
            this.TestImgIndex.Name = "TestImgIndex";
            this.TestImgIndex.Size = new System.Drawing.Size(100, 20);
            this.TestImgIndex.TabIndex = 26;
            this.TestImgIndex.Text = "0";
            this.TestImgIndex.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(48, 552);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 29;
            this.label2.Text = "RoomName";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // RoomName
            // 
            this.RoomName.Location = new System.Drawing.Point(117, 549);
            this.RoomName.Name = "RoomName";
            this.RoomName.Size = new System.Drawing.Size(100, 20);
            this.RoomName.TabIndex = 28;
            this.RoomName.Text = "R000";
            this.RoomName.TextChanged += new System.EventHandler(this.textBox1_TextChanged_1);
            // 
            // dbImage
            // 
            this.dbImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.dbImage.Location = new System.Drawing.Point(275, 341);
            this.dbImage.Name = "dbImage";
            this.dbImage.Size = new System.Drawing.Size(608, 232);
            this.dbImage.TabIndex = 24;
            this.dbImage.TabStop = false;
            // 
            // Left
            // 
            this.Left.Location = new System.Drawing.Point(246, 549);
            this.Left.Name = "Left";
            this.Left.Size = new System.Drawing.Size(23, 23);
            this.Left.TabIndex = 30;
            this.Left.Text = "<";
            this.Left.UseVisualStyleBackColor = true;
            this.Left.Click += new System.EventHandler(this.Left_Click);
            this.Left.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Left_MouseDown);
            // 
            // right
            // 
            this.right.Location = new System.Drawing.Point(889, 550);
            this.right.Name = "right";
            this.right.Size = new System.Drawing.Size(23, 23);
            this.right.TabIndex = 31;
            this.right.Text = ">";
            this.right.UseVisualStyleBackColor = true;
            this.right.MouseDown += new System.Windows.Forms.MouseEventHandler(this.right_MouseDown);
            // 
            // labelCampusVer
            // 
            this.labelCampusVer.AutoSize = true;
            this.labelCampusVer.Location = new System.Drawing.Point(45, 179);
            this.labelCampusVer.Name = "labelCampusVer";
            this.labelCampusVer.Size = new System.Drawing.Size(75, 13);
            this.labelCampusVer.TabIndex = 34;
            this.labelCampusVer.Text = "Campus code:";
            this.labelCampusVer.Click += new System.EventHandler(this.label3_Click);
            // 
            // txtCampusVer
            // 
            this.txtCampusVer.Location = new System.Drawing.Point(126, 177);
            this.txtCampusVer.Name = "txtCampusVer";
            this.txtCampusVer.Size = new System.Drawing.Size(46, 20);
            this.txtCampusVer.TabIndex = 33;
            this.txtCampusVer.Text = "PE";
            this.txtCampusVer.TextChanged += new System.EventHandler(this.textBox1_TextChanged_2);
            // 
            // btnGetCampusVer
            // 
            this.btnGetCampusVer.Location = new System.Drawing.Point(45, 148);
            this.btnGetCampusVer.Name = "btnGetCampusVer";
            this.btnGetCampusVer.Size = new System.Drawing.Size(106, 23);
            this.btnGetCampusVer.TabIndex = 32;
            this.btnGetCampusVer.Text = "Get Campus Ver";
            this.btnGetCampusVer.UseVisualStyleBackColor = true;
            this.btnGetCampusVer.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // labelGetService
            // 
            this.labelGetService.AutoSize = true;
            this.labelGetService.Location = new System.Drawing.Point(45, 320);
            this.labelGetService.Name = "labelGetService";
            this.labelGetService.Size = new System.Drawing.Size(75, 13);
            this.labelGetService.TabIndex = 37;
            this.labelGetService.Text = "Campus code:";
            // 
            // txtGetService
            // 
            this.txtGetService.Location = new System.Drawing.Point(126, 318);
            this.txtGetService.Name = "txtGetService";
            this.txtGetService.Size = new System.Drawing.Size(46, 20);
            this.txtGetService.TabIndex = 36;
            this.txtGetService.Text = "PE";
            this.txtGetService.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // btnGetService
            // 
            this.btnGetService.Location = new System.Drawing.Point(45, 289);
            this.btnGetService.Name = "btnGetService";
            this.btnGetService.Size = new System.Drawing.Size(106, 23);
            this.btnGetService.TabIndex = 35;
            this.btnGetService.Text = "Get Service";
            this.btnGetService.UseVisualStyleBackColor = true;
            this.btnGetService.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(923, 625);
            this.Controls.Add(this.labelGetService);
            this.Controls.Add(this.txtGetService);
            this.Controls.Add(this.btnGetService);
            this.Controls.Add(this.labelCampusVer);
            this.Controls.Add(this.txtCampusVer);
            this.Controls.Add(this.btnGetCampusVer);
            this.Controls.Add(this.right);
            this.Controls.Add(this.Left);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.RoomName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TestImgIndex);
            this.Controls.Add(this.btnTestImg);
            this.Controls.Add(this.dbImage);
            this.Controls.Add(this.rbPath_Dis);
            this.Controls.Add(this.lblPath_Waypoint);
            this.Controls.Add(this.txtPath_Waypoint);
            this.Controls.Add(this.btnResolvePath);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.btnGetCampuses);
            this.Controls.Add(this.lblRoomCampus);
            this.Controls.Add(this.txtRoomCampus);
            this.Controls.Add(this.btnGetRooms);
            this.Controls.Add(this.btnTestDB);
            this.Controls.Add(this.btnTestService);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dbImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTestService;
        private System.Windows.Forms.Button btnTestDB;
        private System.Windows.Forms.Button btnGetRooms;
        private System.Windows.Forms.TextBox txtRoomCampus;
        private System.Windows.Forms.Label lblRoomCampus;
        private System.Windows.Forms.Button btnGetCampuses;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.TextBox txtPath_Waypoint;
        private System.Windows.Forms.Label lblPath_Waypoint;
        private System.Windows.Forms.RadioButton rbPath_Dis;
        private System.Windows.Forms.Button btnResolvePath;
        private System.Windows.Forms.Button btnTestImg;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TestImgIndex;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox RoomName;
        private System.Windows.Forms.PictureBox dbImage;
        private System.Windows.Forms.Button Left;
        private System.Windows.Forms.Button right;
        private System.Windows.Forms.Label labelCampusVer;
        private System.Windows.Forms.TextBox txtCampusVer;
        private System.Windows.Forms.Button btnGetCampusVer;
        private System.Windows.Forms.Label labelGetService;
        private System.Windows.Forms.TextBox txtGetService;
        private System.Windows.Forms.Button btnGetService;
    }
}

