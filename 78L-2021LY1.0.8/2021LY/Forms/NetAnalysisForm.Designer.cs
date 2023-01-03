namespace _2021LY.Forms
{
    partial class NetAnalysisForm
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
            this.server_ip = new Sunny.UI.UITextBox();
            this.uiLabel1 = new Sunny.UI.UILabel();
            this.server_port = new Sunny.UI.UITextBox();
            this.uiLabel2 = new Sunny.UI.UILabel();
            this.uiLabel3 = new Sunny.UI.UILabel();
            this.conn_state = new Sunny.UI.UILight();
            this.connect_server = new Sunny.UI.UIButton();
            this.uiLabel4 = new Sunny.UI.UILabel();
            this.heart_time = new Sunny.UI.UITextBox();
            this.uiGroupBox1 = new Sunny.UI.UIGroupBox();
            this.cs_msg = new Sunny.UI.UITextBox();
            this.cut_server = new Sunny.UI.UIButton();
            this.uiLabel5 = new Sunny.UI.UILabel();
            this.PagePanel.SuspendLayout();
            this.uiGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // PagePanel
            // 
            this.PagePanel.Controls.Add(this.uiLabel5);
            this.PagePanel.Controls.Add(this.cut_server);
            this.PagePanel.Controls.Add(this.uiGroupBox1);
            this.PagePanel.Controls.Add(this.heart_time);
            this.PagePanel.Controls.Add(this.uiLabel4);
            this.PagePanel.Controls.Add(this.connect_server);
            this.PagePanel.Controls.Add(this.conn_state);
            this.PagePanel.Controls.Add(this.uiLabel3);
            this.PagePanel.Controls.Add(this.server_port);
            this.PagePanel.Controls.Add(this.uiLabel2);
            this.PagePanel.Controls.Add(this.server_ip);
            this.PagePanel.Controls.Add(this.uiLabel1);
            this.PagePanel.Size = new System.Drawing.Size(1139, 683);
            // 
            // server_ip
            // 
            this.server_ip.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.server_ip.FillColor = System.Drawing.Color.White;
            this.server_ip.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.server_ip.Location = new System.Drawing.Point(166, 76);
            this.server_ip.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.server_ip.Maximum = 2147483647D;
            this.server_ip.Minimum = -2147483648D;
            this.server_ip.MinimumSize = new System.Drawing.Size(1, 1);
            this.server_ip.Name = "server_ip";
            this.server_ip.Padding = new System.Windows.Forms.Padding(5);
            this.server_ip.Size = new System.Drawing.Size(106, 29);
            this.server_ip.TabIndex = 7;
            this.server_ip.Text = "192.168.1.1";
            // 
            // uiLabel1
            // 
            this.uiLabel1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLabel1.Location = new System.Drawing.Point(48, 76);
            this.uiLabel1.Name = "uiLabel1";
            this.uiLabel1.Size = new System.Drawing.Size(107, 23);
            this.uiLabel1.TabIndex = 6;
            this.uiLabel1.Text = "服务器地址：";
            this.uiLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // server_port
            // 
            this.server_port.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.server_port.DoubleValue = 50000D;
            this.server_port.FillColor = System.Drawing.Color.White;
            this.server_port.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.server_port.IntValue = 50000;
            this.server_port.Location = new System.Drawing.Point(165, 129);
            this.server_port.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.server_port.Maximum = 2147483647D;
            this.server_port.Minimum = -2147483648D;
            this.server_port.MinimumSize = new System.Drawing.Size(1, 1);
            this.server_port.Name = "server_port";
            this.server_port.Padding = new System.Windows.Forms.Padding(5);
            this.server_port.Size = new System.Drawing.Size(106, 29);
            this.server_port.TabIndex = 9;
            this.server_port.Text = "50000";
            // 
            // uiLabel2
            // 
            this.uiLabel2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLabel2.Location = new System.Drawing.Point(96, 129);
            this.uiLabel2.Name = "uiLabel2";
            this.uiLabel2.Size = new System.Drawing.Size(59, 23);
            this.uiLabel2.TabIndex = 8;
            this.uiLabel2.Text = "端口：";
            this.uiLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiLabel3
            // 
            this.uiLabel3.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLabel3.Location = new System.Drawing.Point(64, 187);
            this.uiLabel3.Name = "uiLabel3";
            this.uiLabel3.Size = new System.Drawing.Size(91, 23);
            this.uiLabel3.TabIndex = 10;
            this.uiLabel3.Text = "连接状态：";
            this.uiLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // conn_state
            // 
            this.conn_state.CenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(232)))));
            this.conn_state.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.conn_state.Interval = 300;
            this.conn_state.Location = new System.Drawing.Point(169, 185);
            this.conn_state.MinimumSize = new System.Drawing.Size(1, 1);
            this.conn_state.Name = "conn_state";
            this.conn_state.OnColor = System.Drawing.Color.DarkGreen;
            this.conn_state.Radius = 35;
            this.conn_state.Size = new System.Drawing.Size(35, 35);
            this.conn_state.State = Sunny.UI.UILightState.Off;
            this.conn_state.TabIndex = 36;
            this.conn_state.Text = "conn_state";
            // 
            // connect_server
            // 
            this.connect_server.Cursor = System.Windows.Forms.Cursors.Hand;
            this.connect_server.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.connect_server.Location = new System.Drawing.Point(52, 263);
            this.connect_server.MinimumSize = new System.Drawing.Size(1, 1);
            this.connect_server.Name = "connect_server";
            this.connect_server.Size = new System.Drawing.Size(87, 33);
            this.connect_server.TabIndex = 37;
            this.connect_server.Text = "连接";
            this.connect_server.Click += new System.EventHandler(this.connect_server_Click);
            // 
            // uiLabel4
            // 
            this.uiLabel4.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLabel4.Location = new System.Drawing.Point(48, 350);
            this.uiLabel4.Name = "uiLabel4";
            this.uiLabel4.Size = new System.Drawing.Size(91, 23);
            this.uiLabel4.TabIndex = 38;
            this.uiLabel4.Text = "心跳间隔：";
            this.uiLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // heart_time
            // 
            this.heart_time.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.heart_time.DoubleValue = 6000D;
            this.heart_time.FillColor = System.Drawing.Color.White;
            this.heart_time.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.heart_time.IntValue = 6000;
            this.heart_time.Location = new System.Drawing.Point(146, 350);
            this.heart_time.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.heart_time.Maximum = 2147483647D;
            this.heart_time.Minimum = -2147483648D;
            this.heart_time.MinimumSize = new System.Drawing.Size(1, 1);
            this.heart_time.Name = "heart_time";
            this.heart_time.Padding = new System.Windows.Forms.Padding(5);
            this.heart_time.Size = new System.Drawing.Size(106, 29);
            this.heart_time.TabIndex = 10;
            this.heart_time.Text = "6000";
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.cs_msg);
            this.uiGroupBox1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiGroupBox1.Location = new System.Drawing.Point(375, 8);
            this.uiGroupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiGroupBox1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            this.uiGroupBox1.Size = new System.Drawing.Size(751, 661);
            this.uiGroupBox1.TabIndex = 39;
            this.uiGroupBox1.Text = "信息收发监控";
            // 
            // cs_msg
            // 
            this.cs_msg.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.cs_msg.FillColor = System.Drawing.Color.White;
            this.cs_msg.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.cs_msg.Location = new System.Drawing.Point(4, 27);
            this.cs_msg.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cs_msg.Maximum = 2147483647D;
            this.cs_msg.Minimum = -2147483648D;
            this.cs_msg.MinimumSize = new System.Drawing.Size(1, 1);
            this.cs_msg.Multiline = true;
            this.cs_msg.Name = "cs_msg";
            this.cs_msg.Padding = new System.Windows.Forms.Padding(5);
            this.cs_msg.Size = new System.Drawing.Size(743, 629);
            this.cs_msg.TabIndex = 0;
            // 
            // cut_server
            // 
            this.cut_server.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cut_server.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.cut_server.Location = new System.Drawing.Point(201, 263);
            this.cut_server.MinimumSize = new System.Drawing.Size(1, 1);
            this.cut_server.Name = "cut_server";
            this.cut_server.Size = new System.Drawing.Size(87, 33);
            this.cut_server.TabIndex = 40;
            this.cut_server.Text = "断开";
            this.cut_server.Click += new System.EventHandler(this.cut_server_Click);
            // 
            // uiLabel5
            // 
            this.uiLabel5.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLabel5.Location = new System.Drawing.Point(262, 354);
            this.uiLabel5.Name = "uiLabel5";
            this.uiLabel5.Size = new System.Drawing.Size(34, 23);
            this.uiLabel5.TabIndex = 41;
            this.uiLabel5.Text = "ms";
            this.uiLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // NetAnalysisForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1139, 718);
            this.Name = "NetAnalysisForm";
            this.Text = "NetAnalysisForm";
            this.PagePanel.ResumeLayout(false);
            this.uiGroupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UITextBox server_ip;
        private Sunny.UI.UILabel uiLabel1;
        private Sunny.UI.UITextBox server_port;
        private Sunny.UI.UILabel uiLabel2;
        private Sunny.UI.UILabel uiLabel3;
        private Sunny.UI.UIButton connect_server;
        private Sunny.UI.UILabel uiLabel4;
        private Sunny.UI.UITextBox heart_time;
        private Sunny.UI.UIGroupBox uiGroupBox1;
        private Sunny.UI.UITextBox cs_msg;
        private Sunny.UI.UIButton cut_server;
        public Sunny.UI.UILight conn_state;
        private Sunny.UI.UILabel uiLabel5;
    }
}