namespace _2021LY.Forms
{
    partial class MES
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
            this.SnTextBox = new Sunny.UI.UITextBox();
            this.uiListBox1 = new Sunny.UI.UIListBox();
            this.uiGroupBox1 = new Sunny.UI.UIGroupBox();
            this.uiGroupBox2 = new Sunny.UI.UIGroupBox();
            this.MesCheck = new Sunny.UI.UICheckBox();
            this.Is保存Log = new Sunny.UI.UICheckBox();
            this.uiGroupBox3 = new Sunny.UI.UIGroupBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.uiButton4 = new Sunny.UI.UIButton();
            this.uiButton3 = new Sunny.UI.UIButton();
            this.运行日志 = new Sunny.UI.UIGroupBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.PagePanel.SuspendLayout();
            this.uiGroupBox1.SuspendLayout();
            this.uiGroupBox2.SuspendLayout();
            this.uiGroupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.运行日志.SuspendLayout();
            this.SuspendLayout();
            // 
            // PagePanel
            // 
            this.PagePanel.Controls.Add(this.groupBox1);
            this.PagePanel.Controls.Add(this.uiGroupBox2);
            this.PagePanel.Controls.Add(this.uiGroupBox3);
            this.PagePanel.Controls.Add(this.uiGroupBox1);
            this.PagePanel.Controls.Add(this.SnTextBox);
            this.PagePanel.Size = new System.Drawing.Size(1016, 719);
            // 
            // SnTextBox
            // 
            this.SnTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.SnTextBox.FillColor = System.Drawing.Color.White;
            this.SnTextBox.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.SnTextBox.Location = new System.Drawing.Point(48, 8);
            this.SnTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.SnTextBox.Maximum = 2147483647D;
            this.SnTextBox.Minimum = -2147483648D;
            this.SnTextBox.MinimumSize = new System.Drawing.Size(1, 1);
            this.SnTextBox.Name = "SnTextBox";
            this.SnTextBox.Padding = new System.Windows.Forms.Padding(5);
            this.SnTextBox.Size = new System.Drawing.Size(252, 29);
            this.SnTextBox.TabIndex = 1;
            this.SnTextBox.Text = "SN";
            // 
            // uiListBox1
            // 
            this.uiListBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiListBox1.FillColor = System.Drawing.Color.White;
            this.uiListBox1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiListBox1.FormatString = "";
            this.uiListBox1.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.uiListBox1.Location = new System.Drawing.Point(0, 32);
            this.uiListBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiListBox1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiListBox1.Name = "uiListBox1";
            this.uiListBox1.Padding = new System.Windows.Forms.Padding(2);
            this.uiListBox1.Size = new System.Drawing.Size(299, 576);
            this.uiListBox1.TabIndex = 2;
            this.uiListBox1.Text = "uiListBox1";
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.uiListBox1);
            this.uiGroupBox1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiGroupBox1.Location = new System.Drawing.Point(13, 47);
            this.uiGroupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiGroupBox1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            this.uiGroupBox1.Size = new System.Drawing.Size(299, 608);
            this.uiGroupBox1.TabIndex = 3;
            this.uiGroupBox1.Text = "小板码";
            // 
            // uiGroupBox2
            // 
            this.uiGroupBox2.Controls.Add(this.MesCheck);
            this.uiGroupBox2.Controls.Add(this.Is保存Log);
            this.uiGroupBox2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiGroupBox2.Location = new System.Drawing.Point(308, 5);
            this.uiGroupBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiGroupBox2.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiGroupBox2.Name = "uiGroupBox2";
            this.uiGroupBox2.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            this.uiGroupBox2.Size = new System.Drawing.Size(424, 69);
            this.uiGroupBox2.TabIndex = 4;
            this.uiGroupBox2.Text = "MES";
            // 
            // MesCheck
            // 
            this.MesCheck.Cursor = System.Windows.Forms.Cursors.Hand;
            this.MesCheck.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.MesCheck.Location = new System.Drawing.Point(89, 30);
            this.MesCheck.MinimumSize = new System.Drawing.Size(1, 1);
            this.MesCheck.Name = "MesCheck";
            this.MesCheck.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.MesCheck.Size = new System.Drawing.Size(114, 29);
            this.MesCheck.TabIndex = 7;
            this.MesCheck.Text = "MES";
            // 
            // Is保存Log
            // 
            this.Is保存Log.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Is保存Log.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.Is保存Log.Location = new System.Drawing.Point(249, 30);
            this.Is保存Log.MinimumSize = new System.Drawing.Size(1, 1);
            this.Is保存Log.Name = "Is保存Log";
            this.Is保存Log.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.Is保存Log.Size = new System.Drawing.Size(114, 29);
            this.Is保存Log.TabIndex = 6;
            this.Is保存Log.Text = "保存Log";
            // 
            // uiGroupBox3
            // 
            this.uiGroupBox3.Controls.Add(this.label16);
            this.uiGroupBox3.Controls.Add(this.label15);
            this.uiGroupBox3.Controls.Add(this.label14);
            this.uiGroupBox3.Controls.Add(this.label13);
            this.uiGroupBox3.Controls.Add(this.label12);
            this.uiGroupBox3.Controls.Add(this.label11);
            this.uiGroupBox3.Controls.Add(this.label10);
            this.uiGroupBox3.Controls.Add(this.label9);
            this.uiGroupBox3.Controls.Add(this.label8);
            this.uiGroupBox3.Controls.Add(this.label7);
            this.uiGroupBox3.Controls.Add(this.label6);
            this.uiGroupBox3.Controls.Add(this.label5);
            this.uiGroupBox3.Controls.Add(this.label4);
            this.uiGroupBox3.Controls.Add(this.label3);
            this.uiGroupBox3.Controls.Add(this.label2);
            this.uiGroupBox3.Controls.Add(this.label1);
            this.uiGroupBox3.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiGroupBox3.Location = new System.Drawing.Point(320, 76);
            this.uiGroupBox3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiGroupBox3.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiGroupBox3.Name = "uiGroupBox3";
            this.uiGroupBox3.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            this.uiGroupBox3.Size = new System.Drawing.Size(105, 527);
            this.uiGroupBox3.TabIndex = 5;
            this.uiGroupBox3.Text = "校验结果";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(27, 495);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(64, 21);
            this.label16.TabIndex = 15;
            this.label16.Text = "label16";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(27, 474);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(64, 21);
            this.label15.TabIndex = 14;
            this.label15.Text = "label15";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(27, 444);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(64, 21);
            this.label14.TabIndex = 13;
            this.label14.Text = "label14";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(27, 413);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(64, 21);
            this.label13.TabIndex = 12;
            this.label13.Text = "label13";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(27, 383);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(64, 21);
            this.label12.TabIndex = 11;
            this.label12.Text = "label12";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(27, 346);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(64, 21);
            this.label11.TabIndex = 10;
            this.label11.Text = "label11";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(27, 313);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(64, 21);
            this.label10.TabIndex = 9;
            this.label10.Text = "label10";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(27, 282);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(55, 21);
            this.label9.TabIndex = 8;
            this.label9.Text = "label9";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(27, 250);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 21);
            this.label8.TabIndex = 7;
            this.label8.Text = "label8";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(27, 220);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 21);
            this.label7.TabIndex = 6;
            this.label7.Text = "label7";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(27, 190);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 21);
            this.label6.TabIndex = 5;
            this.label6.Text = "label6";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 160);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 21);
            this.label5.TabIndex = 4;
            this.label5.Text = "label5";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 127);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 21);
            this.label4.TabIndex = 3;
            this.label4.Text = "label4";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 21);
            this.label3.TabIndex = 2;
            this.label3.Text = "label3";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 21);
            this.label2.TabIndex = 1;
            this.label2.Text = "label2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.uiButton4);
            this.groupBox1.Controls.Add(this.uiButton3);
            this.groupBox1.Location = new System.Drawing.Point(449, 91);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(283, 512);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Log路径";
            // 
            // uiButton4
            // 
            this.uiButton4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiButton4.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiButton4.Location = new System.Drawing.Point(165, 51);
            this.uiButton4.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton4.Name = "uiButton4";
            this.uiButton4.Size = new System.Drawing.Size(100, 35);
            this.uiButton4.TabIndex = 11;
            this.uiButton4.Text = "停止读码";
            this.uiButton4.Click += new System.EventHandler(this.停止读码);
            // 
            // uiButton3
            // 
            this.uiButton3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiButton3.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiButton3.Location = new System.Drawing.Point(18, 51);
            this.uiButton3.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton3.Name = "uiButton3";
            this.uiButton3.Size = new System.Drawing.Size(100, 35);
            this.uiButton3.TabIndex = 10;
            this.uiButton3.Text = "手动读码";
            this.uiButton3.Click += new System.EventHandler(this.手动读码);
            // 
            // 运行日志
            // 
            this.运行日志.Controls.Add(this.richTextBox1);
            this.运行日志.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.运行日志.Location = new System.Drawing.Point(754, 32);
            this.运行日志.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.运行日志.MinimumSize = new System.Drawing.Size(1, 1);
            this.运行日志.Name = "运行日志";
            this.运行日志.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            this.运行日志.Size = new System.Drawing.Size(240, 606);
            this.运行日志.TabIndex = 9;
            this.运行日志.Text = "运行日志";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(0, 32);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(240, 574);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // serialPort1
            // 
            this.serialPort1.BaudRate = 115200;
            // 
            // MES
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 754);
            this.Controls.Add(this.运行日志);
            this.Name = "MES";
            this.Text = "MES";
            this.Controls.SetChildIndex(this.PagePanel, 0);
            this.Controls.SetChildIndex(this.运行日志, 0);
            this.PagePanel.ResumeLayout(false);
            this.uiGroupBox1.ResumeLayout(false);
            this.uiGroupBox2.ResumeLayout(false);
            this.uiGroupBox3.ResumeLayout(false);
            this.uiGroupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.运行日志.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UIGroupBox uiGroupBox1;
        private Sunny.UI.UIGroupBox uiGroupBox2;
        private Sunny.UI.UIGroupBox uiGroupBox3;
        private System.Windows.Forms.GroupBox groupBox1;
        private Sunny.UI.UIGroupBox 运行日志;
        private Sunny.UI.UICheckBox Is保存Log;
        public Sunny.UI.UIListBox uiListBox1;
        public Sunny.UI.UITextBox SnTextBox;
        public System.Windows.Forms.RichTextBox richTextBox1;
        public System.IO.Ports.SerialPort serialPort1;
        private Sunny.UI.UICheckBox MesCheck;
        private Sunny.UI.UIButton uiButton4;
        private Sunny.UI.UIButton uiButton3;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}