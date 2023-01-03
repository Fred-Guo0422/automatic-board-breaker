namespace _2021LY.Forms
{
    partial class VisionCalibra
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VisionCalibra));
            this.九点标定 = new Sunny.UI.UIGroupBox();
            this.UIDataGriew = new Sunny.UI.UIDataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uiGroupBox1 = new Sunny.UI.UIGroupBox();
            this.CaliDisply = new Cognex.VisionPro.CogRecordDisplay();
            this.uiGroupBox2 = new Sunny.UI.UIGroupBox();
            this.uiButton4 = new Sunny.UI.UIButton();
            this.uiButton3 = new Sunny.UI.UIButton();
            this.uiButton2 = new Sunny.UI.UIButton();
            this.DiffPosY = new Sunny.UI.UITextBox();
            this.DiffPosX = new Sunny.UI.UITextBox();
            this.CutPosY = new Sunny.UI.UITextBox();
            this.CutPosX = new Sunny.UI.UITextBox();
            this.CamerPosY = new Sunny.UI.UITextBox();
            this.CamerPosX = new Sunny.UI.UITextBox();
            this.uiLabel13 = new Sunny.UI.UILabel();
            this.uiLabel12 = new Sunny.UI.UILabel();
            this.uiLabel9 = new Sunny.UI.UILabel();
            this.uiLabel8 = new Sunny.UI.UILabel();
            this.uiLabel7 = new Sunny.UI.UILabel();
            this.uiGroupBox3 = new Sunny.UI.UIGroupBox();
            this.uiButton5 = new Sunny.UI.UIButton();
            this.CurrentCutY = new Sunny.UI.UITextBox();
            this.CurrentCutX = new Sunny.UI.UITextBox();
            this.CurrentCamY = new Sunny.UI.UITextBox();
            this.CurrentCamX = new Sunny.UI.UITextBox();
            this.uiLabel11 = new Sunny.UI.UILabel();
            this.uiLabel10 = new Sunny.UI.UILabel();
            this.uiGroupBox4 = new Sunny.UI.UIGroupBox();
            this.uiButton6 = new Sunny.UI.UIButton();
            this.uiButton1 = new Sunny.UI.UIButton();
            this.uiGroupBox5 = new Sunny.UI.UIGroupBox();
            this.Rms = new Sunny.UI.UILabel();
            this.Roation = new Sunny.UI.UILabel();
            this.Skew = new Sunny.UI.UILabel();
            this.Scale = new Sunny.UI.UILabel();
            this.MoveY = new Sunny.UI.UILabel();
            this.MoveX = new Sunny.UI.UILabel();
            this.uiLabel6 = new Sunny.UI.UILabel();
            this.uiLabel5 = new Sunny.UI.UILabel();
            this.uiLabel4 = new Sunny.UI.UILabel();
            this.uiLabel3 = new Sunny.UI.UILabel();
            this.uiLabel2 = new Sunny.UI.UILabel();
            this.uiLabel1 = new Sunny.UI.UILabel();
            this.uiButton7 = new Sunny.UI.UIButton();
            this.PagePanel.SuspendLayout();
            this.九点标定.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UIDataGriew)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CaliDisply)).BeginInit();
            this.uiGroupBox2.SuspendLayout();
            this.uiGroupBox3.SuspendLayout();
            this.uiGroupBox4.SuspendLayout();
            this.uiGroupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // PagePanel
            // 
            this.PagePanel.Controls.Add(this.uiGroupBox5);
            this.PagePanel.Controls.Add(this.uiGroupBox4);
            this.PagePanel.Controls.Add(this.uiGroupBox3);
            this.PagePanel.Controls.Add(this.uiGroupBox2);
            this.PagePanel.Controls.Add(this.uiGroupBox1);
            this.PagePanel.Controls.Add(this.九点标定);
            this.PagePanel.Size = new System.Drawing.Size(1279, 729);
            // 
            // 九点标定
            // 
            this.九点标定.Controls.Add(this.UIDataGriew);
            this.九点标定.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.九点标定.Location = new System.Drawing.Point(13, 8);
            this.九点标定.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.九点标定.MinimumSize = new System.Drawing.Size(1, 1);
            this.九点标定.Name = "九点标定";
            this.九点标定.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            this.九点标定.Size = new System.Drawing.Size(369, 454);
            this.九点标定.TabIndex = 0;
            this.九点标定.Text = "九点标定";
            // 
            // UIDataGriew
            // 
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.UIDataGriew.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
            this.UIDataGriew.BackgroundColor = System.Drawing.Color.White;
            this.UIDataGriew.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("微软雅黑", 12F);
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.UIDataGriew.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.UIDataGriew.ColumnHeadersHeight = 32;
            this.UIDataGriew.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.UIDataGriew.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("微软雅黑", 12F);
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.UIDataGriew.DefaultCellStyle = dataGridViewCellStyle8;
            this.UIDataGriew.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UIDataGriew.EnableHeadersVisualStyles = false;
            this.UIDataGriew.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.UIDataGriew.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.UIDataGriew.Location = new System.Drawing.Point(0, 32);
            this.UIDataGriew.Name = "UIDataGriew";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle9.Font = new System.Drawing.Font("微软雅黑", 12F);
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.UIDataGriew.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.White;
            this.UIDataGriew.RowsDefaultCellStyle = dataGridViewCellStyle10;
            this.UIDataGriew.RowTemplate.Height = 29;
            this.UIDataGriew.SelectedIndex = -1;
            this.UIDataGriew.ShowGridLine = true;
            this.UIDataGriew.Size = new System.Drawing.Size(369, 422);
            this.UIDataGriew.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "序号";
            this.Column1.Name = "Column1";
            this.Column1.Width = 50;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Pix_X";
            this.Column2.Name = "Column2";
            this.Column2.Width = 70;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Pix_Y";
            this.Column3.Name = "Column3";
            this.Column3.Width = 70;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Word_X";
            this.Column4.Name = "Column4";
            this.Column4.Width = 70;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Word_Y";
            this.Column5.Name = "Column5";
            this.Column5.Width = 70;
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.CaliDisply);
            this.uiGroupBox1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiGroupBox1.Location = new System.Drawing.Point(389, 23);
            this.uiGroupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiGroupBox1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            this.uiGroupBox1.Size = new System.Drawing.Size(341, 422);
            this.uiGroupBox1.TabIndex = 1;
            this.uiGroupBox1.Text = "窗体";
            // 
            // CaliDisply
            // 
            this.CaliDisply.ColorMapLowerClipColor = System.Drawing.Color.Black;
            this.CaliDisply.ColorMapLowerRoiLimit = 0D;
            this.CaliDisply.ColorMapPredefined = Cognex.VisionPro.Display.CogDisplayColorMapPredefinedConstants.None;
            this.CaliDisply.ColorMapUpperClipColor = System.Drawing.Color.Black;
            this.CaliDisply.ColorMapUpperRoiLimit = 1D;
            this.CaliDisply.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CaliDisply.DoubleTapZoomCycleLength = 2;
            this.CaliDisply.DoubleTapZoomSensitivity = 2.5D;
            this.CaliDisply.Location = new System.Drawing.Point(0, 32);
            this.CaliDisply.MouseWheelMode = Cognex.VisionPro.Display.CogDisplayMouseWheelModeConstants.Zoom1;
            this.CaliDisply.MouseWheelSensitivity = 1D;
            this.CaliDisply.Name = "CaliDisply";
            this.CaliDisply.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("CaliDisply.OcxState")));
            this.CaliDisply.Size = new System.Drawing.Size(341, 390);
            this.CaliDisply.TabIndex = 0;
            // 
            // uiGroupBox2
            // 
            this.uiGroupBox2.Controls.Add(this.uiButton4);
            this.uiGroupBox2.Controls.Add(this.uiButton3);
            this.uiGroupBox2.Controls.Add(this.uiButton2);
            this.uiGroupBox2.Controls.Add(this.DiffPosY);
            this.uiGroupBox2.Controls.Add(this.DiffPosX);
            this.uiGroupBox2.Controls.Add(this.CutPosY);
            this.uiGroupBox2.Controls.Add(this.CutPosX);
            this.uiGroupBox2.Controls.Add(this.CamerPosY);
            this.uiGroupBox2.Controls.Add(this.CamerPosX);
            this.uiGroupBox2.Controls.Add(this.uiLabel13);
            this.uiGroupBox2.Controls.Add(this.uiLabel12);
            this.uiGroupBox2.Controls.Add(this.uiLabel9);
            this.uiGroupBox2.Controls.Add(this.uiLabel8);
            this.uiGroupBox2.Controls.Add(this.uiLabel7);
            this.uiGroupBox2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiGroupBox2.Location = new System.Drawing.Point(738, 23);
            this.uiGroupBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiGroupBox2.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiGroupBox2.Name = "uiGroupBox2";
            this.uiGroupBox2.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            this.uiGroupBox2.Size = new System.Drawing.Size(397, 198);
            this.uiGroupBox2.TabIndex = 2;
            this.uiGroupBox2.Text = "相机和切刀差";
            // 
            // uiButton4
            // 
            this.uiButton4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiButton4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiButton4.Location = new System.Drawing.Point(336, 146);
            this.uiButton4.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton4.Name = "uiButton4";
            this.uiButton4.Size = new System.Drawing.Size(55, 26);
            this.uiButton4.TabIndex = 13;
            this.uiButton4.Text = "计算";
            this.uiButton4.Click += new System.EventHandler(this.btnCal);
            // 
            // uiButton3
            // 
            this.uiButton3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiButton3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiButton3.Location = new System.Drawing.Point(333, 94);
            this.uiButton3.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton3.Name = "uiButton3";
            this.uiButton3.Size = new System.Drawing.Size(55, 26);
            this.uiButton3.TabIndex = 12;
            this.uiButton3.Text = "GetPos";
            this.uiButton3.Click += new System.EventHandler(this.btnGetPosCut);
            // 
            // uiButton2
            // 
            this.uiButton2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiButton2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiButton2.Location = new System.Drawing.Point(333, 45);
            this.uiButton2.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton2.Name = "uiButton2";
            this.uiButton2.Size = new System.Drawing.Size(55, 26);
            this.uiButton2.TabIndex = 11;
            this.uiButton2.Text = "GetPos";
            this.uiButton2.Click += new System.EventHandler(this.btnGetCamera);
            // 
            // DiffPosY
            // 
            this.DiffPosY.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.DiffPosY.FillColor = System.Drawing.Color.White;
            this.DiffPosY.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.DiffPosY.Location = new System.Drawing.Point(222, 143);
            this.DiffPosY.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.DiffPosY.Maximum = 2147483647D;
            this.DiffPosY.Minimum = -2147483648D;
            this.DiffPosY.MinimumSize = new System.Drawing.Size(1, 1);
            this.DiffPosY.Name = "DiffPosY";
            this.DiffPosY.Padding = new System.Windows.Forms.Padding(5);
            this.DiffPosY.Size = new System.Drawing.Size(107, 29);
            this.DiffPosY.TabIndex = 10;
            this.DiffPosY.Text = "DiffPosY";
            // 
            // DiffPosX
            // 
            this.DiffPosX.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.DiffPosX.FillColor = System.Drawing.Color.White;
            this.DiffPosX.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.DiffPosX.Location = new System.Drawing.Point(94, 143);
            this.DiffPosX.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.DiffPosX.Maximum = 2147483647D;
            this.DiffPosX.Minimum = -2147483648D;
            this.DiffPosX.MinimumSize = new System.Drawing.Size(1, 1);
            this.DiffPosX.Name = "DiffPosX";
            this.DiffPosX.Padding = new System.Windows.Forms.Padding(5);
            this.DiffPosX.Size = new System.Drawing.Size(107, 29);
            this.DiffPosX.TabIndex = 9;
            this.DiffPosX.Text = "DiffPosX";
            // 
            // CutPosY
            // 
            this.CutPosY.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.CutPosY.FillColor = System.Drawing.Color.White;
            this.CutPosY.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.CutPosY.Location = new System.Drawing.Point(222, 94);
            this.CutPosY.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CutPosY.Maximum = 2147483647D;
            this.CutPosY.Minimum = -2147483648D;
            this.CutPosY.MinimumSize = new System.Drawing.Size(1, 1);
            this.CutPosY.Name = "CutPosY";
            this.CutPosY.Padding = new System.Windows.Forms.Padding(5);
            this.CutPosY.Size = new System.Drawing.Size(107, 29);
            this.CutPosY.TabIndex = 8;
            this.CutPosY.Text = "CutPosY";
            // 
            // CutPosX
            // 
            this.CutPosX.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.CutPosX.FillColor = System.Drawing.Color.White;
            this.CutPosX.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.CutPosX.Location = new System.Drawing.Point(94, 94);
            this.CutPosX.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CutPosX.Maximum = 2147483647D;
            this.CutPosX.Minimum = -2147483648D;
            this.CutPosX.MinimumSize = new System.Drawing.Size(1, 1);
            this.CutPosX.Name = "CutPosX";
            this.CutPosX.Padding = new System.Windows.Forms.Padding(5);
            this.CutPosX.Size = new System.Drawing.Size(107, 29);
            this.CutPosX.TabIndex = 7;
            this.CutPosX.Text = "CutPosX";
            // 
            // CamerPosY
            // 
            this.CamerPosY.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.CamerPosY.FillColor = System.Drawing.Color.White;
            this.CamerPosY.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.CamerPosY.Location = new System.Drawing.Point(222, 45);
            this.CamerPosY.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CamerPosY.Maximum = 2147483647D;
            this.CamerPosY.Minimum = -2147483648D;
            this.CamerPosY.MinimumSize = new System.Drawing.Size(1, 1);
            this.CamerPosY.Name = "CamerPosY";
            this.CamerPosY.Padding = new System.Windows.Forms.Padding(5);
            this.CamerPosY.Size = new System.Drawing.Size(107, 29);
            this.CamerPosY.TabIndex = 6;
            this.CamerPosY.Text = "CamerPosY";
            // 
            // CamerPosX
            // 
            this.CamerPosX.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.CamerPosX.FillColor = System.Drawing.Color.White;
            this.CamerPosX.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.CamerPosX.Location = new System.Drawing.Point(94, 45);
            this.CamerPosX.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CamerPosX.Maximum = 2147483647D;
            this.CamerPosX.Minimum = -2147483648D;
            this.CamerPosX.MinimumSize = new System.Drawing.Size(1, 1);
            this.CamerPosX.Name = "CamerPosX";
            this.CamerPosX.Padding = new System.Windows.Forms.Padding(5);
            this.CamerPosX.Size = new System.Drawing.Size(107, 29);
            this.CamerPosX.TabIndex = 5;
            this.CamerPosX.Text = "CamerPosX";
            // 
            // uiLabel13
            // 
            this.uiLabel13.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLabel13.Location = new System.Drawing.Point(237, 17);
            this.uiLabel13.Name = "uiLabel13";
            this.uiLabel13.Size = new System.Drawing.Size(23, 23);
            this.uiLabel13.TabIndex = 4;
            this.uiLabel13.Text = "Y";
            this.uiLabel13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiLabel12
            // 
            this.uiLabel12.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLabel12.Location = new System.Drawing.Point(126, 17);
            this.uiLabel12.Name = "uiLabel12";
            this.uiLabel12.Size = new System.Drawing.Size(23, 23);
            this.uiLabel12.TabIndex = 3;
            this.uiLabel12.Text = "X";
            this.uiLabel12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiLabel9
            // 
            this.uiLabel9.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLabel9.Location = new System.Drawing.Point(3, 143);
            this.uiLabel9.Name = "uiLabel9";
            this.uiLabel9.Size = new System.Drawing.Size(100, 23);
            this.uiLabel9.TabIndex = 2;
            this.uiLabel9.Text = "计算偏差：";
            this.uiLabel9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiLabel8
            // 
            this.uiLabel8.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLabel8.Location = new System.Drawing.Point(3, 94);
            this.uiLabel8.Name = "uiLabel8";
            this.uiLabel8.Size = new System.Drawing.Size(100, 23);
            this.uiLabel8.TabIndex = 1;
            this.uiLabel8.Text = "切刀位置：";
            this.uiLabel8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiLabel7
            // 
            this.uiLabel7.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLabel7.Location = new System.Drawing.Point(3, 45);
            this.uiLabel7.Name = "uiLabel7";
            this.uiLabel7.Size = new System.Drawing.Size(100, 23);
            this.uiLabel7.TabIndex = 0;
            this.uiLabel7.Text = "相机位置：";
            this.uiLabel7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiGroupBox3
            // 
            this.uiGroupBox3.Controls.Add(this.uiButton5);
            this.uiGroupBox3.Controls.Add(this.CurrentCutY);
            this.uiGroupBox3.Controls.Add(this.CurrentCutX);
            this.uiGroupBox3.Controls.Add(this.CurrentCamY);
            this.uiGroupBox3.Controls.Add(this.CurrentCamX);
            this.uiGroupBox3.Controls.Add(this.uiLabel11);
            this.uiGroupBox3.Controls.Add(this.uiLabel10);
            this.uiGroupBox3.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiGroupBox3.Location = new System.Drawing.Point(738, 231);
            this.uiGroupBox3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiGroupBox3.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiGroupBox3.Name = "uiGroupBox3";
            this.uiGroupBox3.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            this.uiGroupBox3.Size = new System.Drawing.Size(397, 139);
            this.uiGroupBox3.TabIndex = 3;
            this.uiGroupBox3.Text = "获取切刀位置";
            // 
            // uiButton5
            // 
            this.uiButton5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiButton5.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiButton5.Location = new System.Drawing.Point(333, 35);
            this.uiButton5.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton5.Name = "uiButton5";
            this.uiButton5.Size = new System.Drawing.Size(55, 26);
            this.uiButton5.TabIndex = 14;
            this.uiButton5.Text = "GetPos";
            this.uiButton5.Click += new System.EventHandler(this.GetPosCurCam);
            // 
            // CurrentCutY
            // 
            this.CurrentCutY.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.CurrentCutY.FillColor = System.Drawing.Color.White;
            this.CurrentCutY.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.CurrentCutY.Location = new System.Drawing.Point(222, 96);
            this.CurrentCutY.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CurrentCutY.Maximum = 2147483647D;
            this.CurrentCutY.Minimum = -2147483648D;
            this.CurrentCutY.MinimumSize = new System.Drawing.Size(1, 1);
            this.CurrentCutY.Name = "CurrentCutY";
            this.CurrentCutY.Padding = new System.Windows.Forms.Padding(5);
            this.CurrentCutY.Size = new System.Drawing.Size(107, 29);
            this.CurrentCutY.TabIndex = 13;
            this.CurrentCutY.Text = "CurrentCutY";
            // 
            // CurrentCutX
            // 
            this.CurrentCutX.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.CurrentCutX.FillColor = System.Drawing.Color.White;
            this.CurrentCutX.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.CurrentCutX.Location = new System.Drawing.Point(94, 96);
            this.CurrentCutX.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CurrentCutX.Maximum = 2147483647D;
            this.CurrentCutX.Minimum = -2147483648D;
            this.CurrentCutX.MinimumSize = new System.Drawing.Size(1, 1);
            this.CurrentCutX.Name = "CurrentCutX";
            this.CurrentCutX.Padding = new System.Windows.Forms.Padding(5);
            this.CurrentCutX.Size = new System.Drawing.Size(120, 29);
            this.CurrentCutX.TabIndex = 12;
            this.CurrentCutX.Text = "CurrentCutX";
            // 
            // CurrentCamY
            // 
            this.CurrentCamY.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.CurrentCamY.FillColor = System.Drawing.Color.White;
            this.CurrentCamY.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.CurrentCamY.Location = new System.Drawing.Point(222, 36);
            this.CurrentCamY.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CurrentCamY.Maximum = 2147483647D;
            this.CurrentCamY.Minimum = -2147483648D;
            this.CurrentCamY.MinimumSize = new System.Drawing.Size(1, 1);
            this.CurrentCamY.Name = "CurrentCamY";
            this.CurrentCamY.Padding = new System.Windows.Forms.Padding(5);
            this.CurrentCamY.Size = new System.Drawing.Size(107, 29);
            this.CurrentCamY.TabIndex = 11;
            this.CurrentCamY.Text = "CurrentCamY";
            // 
            // CurrentCamX
            // 
            this.CurrentCamX.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.CurrentCamX.FillColor = System.Drawing.Color.White;
            this.CurrentCamX.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.CurrentCamX.Location = new System.Drawing.Point(94, 37);
            this.CurrentCamX.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CurrentCamX.Maximum = 2147483647D;
            this.CurrentCamX.Minimum = -2147483648D;
            this.CurrentCamX.MinimumSize = new System.Drawing.Size(1, 1);
            this.CurrentCamX.Name = "CurrentCamX";
            this.CurrentCamX.Padding = new System.Windows.Forms.Padding(5);
            this.CurrentCamX.Size = new System.Drawing.Size(120, 29);
            this.CurrentCamX.TabIndex = 10;
            this.CurrentCamX.Text = "CurrentCamX";
            // 
            // uiLabel11
            // 
            this.uiLabel11.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLabel11.Location = new System.Drawing.Point(3, 96);
            this.uiLabel11.Name = "uiLabel11";
            this.uiLabel11.Size = new System.Drawing.Size(100, 23);
            this.uiLabel11.TabIndex = 4;
            this.uiLabel11.Text = "当前切刀位置：";
            this.uiLabel11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiLabel10
            // 
            this.uiLabel10.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLabel10.Location = new System.Drawing.Point(3, 42);
            this.uiLabel10.Name = "uiLabel10";
            this.uiLabel10.Size = new System.Drawing.Size(100, 23);
            this.uiLabel10.TabIndex = 3;
            this.uiLabel10.Text = "当前相机位置：";
            this.uiLabel10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiGroupBox4
            // 
            this.uiGroupBox4.Controls.Add(this.uiButton7);
            this.uiGroupBox4.Controls.Add(this.uiButton6);
            this.uiGroupBox4.Controls.Add(this.uiButton1);
            this.uiGroupBox4.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiGroupBox4.Location = new System.Drawing.Point(740, 380);
            this.uiGroupBox4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiGroupBox4.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiGroupBox4.Name = "uiGroupBox4";
            this.uiGroupBox4.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            this.uiGroupBox4.Size = new System.Drawing.Size(395, 265);
            this.uiGroupBox4.TabIndex = 4;
            this.uiGroupBox4.Text = "标定操作";
            // 
            // uiButton6
            // 
            this.uiButton6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiButton6.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiButton6.Location = new System.Drawing.Point(157, 47);
            this.uiButton6.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton6.Name = "uiButton6";
            this.uiButton6.Size = new System.Drawing.Size(81, 35);
            this.uiButton6.TabIndex = 14;
            this.uiButton6.Text = "保存结果";
            this.uiButton6.Click += new System.EventHandler(this.uiButton6_Click);
            // 
            // uiButton1
            // 
            this.uiButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiButton1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiButton1.Location = new System.Drawing.Point(19, 47);
            this.uiButton1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton1.Name = "uiButton1";
            this.uiButton1.Size = new System.Drawing.Size(100, 35);
            this.uiButton1.TabIndex = 0;
            this.uiButton1.Text = "标定开始";
            this.uiButton1.Click += new System.EventHandler(this.btnStartCal);
            // 
            // uiGroupBox5
            // 
            this.uiGroupBox5.Controls.Add(this.Rms);
            this.uiGroupBox5.Controls.Add(this.Roation);
            this.uiGroupBox5.Controls.Add(this.Skew);
            this.uiGroupBox5.Controls.Add(this.Scale);
            this.uiGroupBox5.Controls.Add(this.MoveY);
            this.uiGroupBox5.Controls.Add(this.MoveX);
            this.uiGroupBox5.Controls.Add(this.uiLabel6);
            this.uiGroupBox5.Controls.Add(this.uiLabel5);
            this.uiGroupBox5.Controls.Add(this.uiLabel4);
            this.uiGroupBox5.Controls.Add(this.uiLabel3);
            this.uiGroupBox5.Controls.Add(this.uiLabel2);
            this.uiGroupBox5.Controls.Add(this.uiLabel1);
            this.uiGroupBox5.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiGroupBox5.Location = new System.Drawing.Point(13, 472);
            this.uiGroupBox5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiGroupBox5.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiGroupBox5.Name = "uiGroupBox5";
            this.uiGroupBox5.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            this.uiGroupBox5.Size = new System.Drawing.Size(549, 197);
            this.uiGroupBox5.TabIndex = 5;
            this.uiGroupBox5.Text = "标定结果";
            // 
            // Rms
            // 
            this.Rms.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.Rms.Location = new System.Drawing.Point(458, 87);
            this.Rms.Name = "Rms";
            this.Rms.Size = new System.Drawing.Size(100, 23);
            this.Rms.TabIndex = 11;
            this.Rms.Text = "Rms";
            this.Rms.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Roation
            // 
            this.Roation.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.Roation.Location = new System.Drawing.Point(446, 32);
            this.Roation.Name = "Roation";
            this.Roation.Size = new System.Drawing.Size(100, 23);
            this.Roation.TabIndex = 10;
            this.Roation.Text = "Roation";
            this.Roation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Skew
            // 
            this.Skew.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.Skew.Location = new System.Drawing.Point(269, 88);
            this.Skew.Name = "Skew";
            this.Skew.Size = new System.Drawing.Size(100, 23);
            this.Skew.TabIndex = 9;
            this.Skew.Text = "Skew";
            this.Skew.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Scale
            // 
            this.Scale.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.Scale.Location = new System.Drawing.Point(89, 89);
            this.Scale.Name = "Scale";
            this.Scale.Size = new System.Drawing.Size(100, 23);
            this.Scale.TabIndex = 8;
            this.Scale.Text = "Scale";
            this.Scale.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MoveY
            // 
            this.MoveY.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.MoveY.Location = new System.Drawing.Point(269, 32);
            this.MoveY.Name = "MoveY";
            this.MoveY.Size = new System.Drawing.Size(100, 23);
            this.MoveY.TabIndex = 7;
            this.MoveY.Text = "MoveY";
            this.MoveY.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MoveX
            // 
            this.MoveX.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.MoveX.Location = new System.Drawing.Point(89, 32);
            this.MoveX.Name = "MoveX";
            this.MoveX.Size = new System.Drawing.Size(100, 23);
            this.MoveX.TabIndex = 6;
            this.MoveX.Text = "MoveX";
            this.MoveX.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiLabel6
            // 
            this.uiLabel6.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLabel6.Location = new System.Drawing.Point(363, 87);
            this.uiLabel6.Name = "uiLabel6";
            this.uiLabel6.Size = new System.Drawing.Size(89, 23);
            this.uiLabel6.TabIndex = 5;
            this.uiLabel6.Text = "标准误差:";
            this.uiLabel6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiLabel5
            // 
            this.uiLabel5.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLabel5.Location = new System.Drawing.Point(372, 32);
            this.uiLabel5.Name = "uiLabel5";
            this.uiLabel5.Size = new System.Drawing.Size(73, 23);
            this.uiLabel5.TabIndex = 4;
            this.uiLabel5.Text = "旋转:";
            this.uiLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiLabel4
            // 
            this.uiLabel4.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLabel4.Location = new System.Drawing.Point(195, 87);
            this.uiLabel4.Name = "uiLabel4";
            this.uiLabel4.Size = new System.Drawing.Size(100, 25);
            this.uiLabel4.TabIndex = 3;
            this.uiLabel4.Text = "倾斜:";
            this.uiLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiLabel3
            // 
            this.uiLabel3.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLabel3.Location = new System.Drawing.Point(16, 89);
            this.uiLabel3.Name = "uiLabel3";
            this.uiLabel3.Size = new System.Drawing.Size(100, 23);
            this.uiLabel3.TabIndex = 2;
            this.uiLabel3.Text = "缩放:";
            this.uiLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiLabel2
            // 
            this.uiLabel2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLabel2.Location = new System.Drawing.Point(195, 32);
            this.uiLabel2.Name = "uiLabel2";
            this.uiLabel2.Size = new System.Drawing.Size(100, 23);
            this.uiLabel2.TabIndex = 1;
            this.uiLabel2.Text = "平移Y:";
            this.uiLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiLabel1
            // 
            this.uiLabel1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLabel1.Location = new System.Drawing.Point(16, 32);
            this.uiLabel1.Name = "uiLabel1";
            this.uiLabel1.Size = new System.Drawing.Size(100, 23);
            this.uiLabel1.TabIndex = 0;
            this.uiLabel1.Text = "平移X:";
            this.uiLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiButton7
            // 
            this.uiButton7.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiButton7.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiButton7.Location = new System.Drawing.Point(286, 47);
            this.uiButton7.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton7.Name = "uiButton7";
            this.uiButton7.Size = new System.Drawing.Size(81, 35);
            this.uiButton7.TabIndex = 15;
            this.uiButton7.Text = "MES测试";
            this.uiButton7.Click += new System.EventHandler(this.uiButton7_Click);
            // 
            // VisionCalibra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1279, 764);
            this.Name = "VisionCalibra";
            this.Text = "VisionCalibra";
            this.PagePanel.ResumeLayout(false);
            this.九点标定.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.UIDataGriew)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CaliDisply)).EndInit();
            this.uiGroupBox2.ResumeLayout(false);
            this.uiGroupBox3.ResumeLayout(false);
            this.uiGroupBox4.ResumeLayout(false);
            this.uiGroupBox5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UIGroupBox 九点标定;
        private Sunny.UI.UIDataGridView UIDataGriew;
        private Sunny.UI.UIGroupBox uiGroupBox1;
        private Cognex.VisionPro.CogRecordDisplay CaliDisply;
        private Sunny.UI.UIGroupBox uiGroupBox2;
        private Sunny.UI.UIGroupBox uiGroupBox3;
        private Sunny.UI.UIGroupBox uiGroupBox4;
        private Sunny.UI.UIGroupBox uiGroupBox5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private Sunny.UI.UILabel uiLabel1;
        private Sunny.UI.UILabel uiLabel2;
        private Sunny.UI.UILabel uiLabel3;
        private Sunny.UI.UILabel uiLabel4;
        private Sunny.UI.UILabel uiLabel6;
        private Sunny.UI.UILabel uiLabel5;
        private Sunny.UI.UILabel uiLabel7;
        private Sunny.UI.UILabel uiLabel9;
        private Sunny.UI.UILabel uiLabel8;
        private Sunny.UI.UILabel uiLabel10;
        private Sunny.UI.UILabel uiLabel11;
        private Sunny.UI.UILabel uiLabel13;
        private Sunny.UI.UILabel uiLabel12;
        private Sunny.UI.UITextBox CamerPosY;
        private Sunny.UI.UITextBox CamerPosX;
        private Sunny.UI.UITextBox DiffPosY;
        private Sunny.UI.UITextBox DiffPosX;
        private Sunny.UI.UITextBox CutPosY;
        private Sunny.UI.UITextBox CutPosX;
        private Sunny.UI.UITextBox CurrentCutY;
        private Sunny.UI.UITextBox CurrentCutX;
        private Sunny.UI.UITextBox CurrentCamY;
        private Sunny.UI.UITextBox CurrentCamX;
        private Sunny.UI.UIButton uiButton1;
        private Sunny.UI.UILabel Scale;
        private Sunny.UI.UILabel MoveY;
        private Sunny.UI.UILabel MoveX;
        private Sunny.UI.UILabel Roation;
        private Sunny.UI.UILabel Skew;
        private Sunny.UI.UILabel Rms;
        private Sunny.UI.UIButton uiButton2;
        private Sunny.UI.UIButton uiButton3;
        private Sunny.UI.UIButton uiButton4;
        private Sunny.UI.UIButton uiButton5;
        private Sunny.UI.UIButton uiButton6;
        private Sunny.UI.UIButton uiButton7;
    }
}