namespace TCPserver
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.label1 = new System.Windows.Forms.Label();
            this.tbHostAddress = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.nUDPort = new System.Windows.Forms.NumericUpDown();
            this.lbMessage = new System.Windows.Forms.ListBox();
            this.bStart = new System.Windows.Forms.Button();
            this.bStop = new System.Windows.Forms.Button();
            this.bwServer = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.bSend = new System.Windows.Forms.Button();
            this.tbMessage = new System.Windows.Forms.TextBox();
            this.tbPass = new System.Windows.Forms.TextBox();
            this.lbPass = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tvUsers = new System.Windows.Forms.TreeView();
            this.label4 = new System.Windows.Forms.Label();
            this.lCountUsers = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nUDPort)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(13, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Address";
            // 
            // tbHostAddress
            // 
            this.tbHostAddress.Location = new System.Drawing.Point(78, 16);
            this.tbHostAddress.Name = "tbHostAddress";
            this.tbHostAddress.Size = new System.Drawing.Size(72, 20);
            this.tbHostAddress.TabIndex = 1;
            this.tbHostAddress.Text = "192.168.0.13";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(156, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Port";
            // 
            // nUDPort
            // 
            this.nUDPort.Location = new System.Drawing.Point(194, 17);
            this.nUDPort.Name = "nUDPort";
            this.nUDPort.Size = new System.Drawing.Size(38, 20);
            this.nUDPort.TabIndex = 4;
            this.nUDPort.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // lbMessage
            // 
            this.lbMessage.BackColor = System.Drawing.Color.Beige;
            this.lbMessage.FormattingEnabled = true;
            this.lbMessage.HorizontalScrollbar = true;
            this.lbMessage.Location = new System.Drawing.Point(16, 53);
            this.lbMessage.Name = "lbMessage";
            this.lbMessage.Size = new System.Drawing.Size(544, 303);
            this.lbMessage.TabIndex = 5;
            this.lbMessage.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lbMessage_KeyUp);
            // 
            // bStart
            // 
            this.bStart.Location = new System.Drawing.Point(404, 14);
            this.bStart.Name = "bStart";
            this.bStart.Size = new System.Drawing.Size(75, 23);
            this.bStart.TabIndex = 6;
            this.bStart.Text = "Start";
            this.bStart.UseVisualStyleBackColor = true;
            this.bStart.Click += new System.EventHandler(this.bStart_Click);
            // 
            // bStop
            // 
            this.bStop.Enabled = false;
            this.bStop.Location = new System.Drawing.Point(485, 14);
            this.bStop.Name = "bStop";
            this.bStop.Size = new System.Drawing.Size(75, 23);
            this.bStop.TabIndex = 7;
            this.bStop.Text = "Stop";
            this.bStop.UseVisualStyleBackColor = true;
            this.bStop.Click += new System.EventHandler(this.bStop_Click);
            // 
            // bwServer
            // 
            this.bwServer.WorkerSupportsCancellation = true;
            this.bwServer.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwServer_DoWork);
            // 
            // backgroundWorker2
            // 
            this.backgroundWorker2.WorkerSupportsCancellation = true;
            this.backgroundWorker2.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker2_DoWork);
            // 
            // bSend
            // 
            this.bSend.Location = new System.Drawing.Point(485, 367);
            this.bSend.Name = "bSend";
            this.bSend.Size = new System.Drawing.Size(75, 23);
            this.bSend.TabIndex = 8;
            this.bSend.Text = "Send";
            this.bSend.UseVisualStyleBackColor = true;
            this.bSend.Click += new System.EventHandler(this.bSend_Click);
            // 
            // tbMessage
            // 
            this.tbMessage.Location = new System.Drawing.Point(16, 367);
            this.tbMessage.Name = "tbMessage";
            this.tbMessage.Size = new System.Drawing.Size(463, 20);
            this.tbMessage.TabIndex = 9;
            // 
            // tbPass
            // 
            this.tbPass.Location = new System.Drawing.Point(291, 16);
            this.tbPass.Name = "tbPass";
            this.tbPass.Size = new System.Drawing.Size(100, 20);
            this.tbPass.TabIndex = 10;
            this.tbPass.Text = "password";
            // 
            // lbPass
            // 
            this.lbPass.AutoSize = true;
            this.lbPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lbPass.Location = new System.Drawing.Point(238, 17);
            this.lbPass.Name = "lbPass";
            this.lbPass.Size = new System.Drawing.Size(47, 16);
            this.lbPass.TabIndex = 11;
            this.lbPass.Text = "Hasło";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(566, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 16);
            this.label3.TabIndex = 12;
            this.label3.Text = "Użytkownicy";
            // 
            // tvUsers
            // 
            this.tvUsers.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tvUsers.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tvUsers.Location = new System.Drawing.Point(569, 53);
            this.tvUsers.Name = "tvUsers";
            this.tvUsers.ShowNodeToolTips = true;
            this.tvUsers.Size = new System.Drawing.Size(167, 303);
            this.tvUsers.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(13, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 16);
            this.label4.TabIndex = 14;
            this.label4.Text = "Logi";
            // 
            // lCountUsers
            // 
            this.lCountUsers.AutoSize = true;
            this.lCountUsers.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lCountUsers.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lCountUsers.Location = new System.Drawing.Point(721, 34);
            this.lCountUsers.Name = "lCountUsers";
            this.lCountUsers.Size = new System.Drawing.Size(15, 16);
            this.lCountUsers.TabIndex = 15;
            this.lCountUsers.Text = "0";
            this.lCountUsers.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkKhaki;
            this.ClientSize = new System.Drawing.Size(748, 414);
            this.Controls.Add(this.lCountUsers);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tvUsers);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbPass);
            this.Controls.Add(this.tbPass);
            this.Controls.Add(this.tbMessage);
            this.Controls.Add(this.bSend);
            this.Controls.Add(this.bStop);
            this.Controls.Add(this.bStart);
            this.Controls.Add(this.lbMessage);
            this.Controls.Add(this.nUDPort);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbHostAddress);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainWindow";
            this.Text = "SzymCZAT [serwer] ";
            ((System.ComponentModel.ISupportInitialize)(this.nUDPort)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbHostAddress;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nUDPort;
        private System.Windows.Forms.ListBox lbMessage;
        private System.Windows.Forms.Button bStart;
        private System.Windows.Forms.Button bStop;
        private System.ComponentModel.BackgroundWorker bwServer;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.Windows.Forms.Button bSend;
        private System.Windows.Forms.TextBox tbMessage;
        private System.Windows.Forms.TextBox tbPass;
        private System.Windows.Forms.Label lbPass;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TreeView tvUsers;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lCountUsers;
    }
}

