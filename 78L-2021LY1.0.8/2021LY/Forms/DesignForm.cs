using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using WpfControlLibrary;
using static WpfControlLibrary.UserControl_Canvas;

namespace _2021LY.Forms
{
    public partial class DesignForm : UITitlePage
    {
        bool Selete = false;
        IniFunctoin iniFunctoin = new IniFunctoin();//得到配置文件信息
        /*************视觉变量*********************/
        public object loker = new object();
        int WindowsX, WindowsY;
        /*****************************************/

        private static DesignForm designForm;

        public bool Flag = true;
        //相机线程

        public static DesignForm getForm()
        {
            if (designForm == null)
            {
                designForm = new DesignForm();
            }
            return designForm;
        }

        static Bitmap bmp = new Bitmap(737, 363);
        Graphics g = Graphics.FromImage(bmp);
        Pen p = new Pen(Brushes.Red);
        Trajectory tj;


        public DesignForm()
        {
            InitializeComponent();
            tj = new Trajectory(Sport_Traje, g, Point_DataGrid, p, bmp);
            this.Text = "编程设计";
            this.Load += DesignForm_Load;
            this.FormClosing += DesignForm_FormClosing;

            Control.CheckForIllegalCrossThreadCalls = false;


            Thread aisx_Status = new Thread(() =>
            {
                Aisx_Status();
            });
            aisx_Status.IsBackground = true;
            aisx_Status.Start();


            Thread axis_Pose = new Thread(() =>
            {
                get_Axis_Pose();
            });
            axis_Pose.IsBackground = true;
            axis_Pose.Start();


            timer2.Start();

        }

        private void DesignForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Console.WriteLine("DesignForm_FormClosing");
            Goog.Reset();
            Goog.Close();
            Vision.AcqTool.Dispose();
            Vision.Inspection.Dispose();
            Application.Exit();
            Environment.Exit(0);
        }

        private void DesignForm_Load(object sender, EventArgs e)
        {
            // Control.CheckForIllegalCrossThreadCalls = false;

            InitializeWPFCanvas();
            action_opencsv();
        }

        

        #region
        //实时获取当前轴的位置
        void get_Axis_Pose()
        {
            try
            {
                while (CommParam.isAlive)//this.Visible
                {
                    Thread.Sleep(100);
                    CommParam.X_Pos = Goog.Get_AxisPos(CommParam.AxisX);
                    CommParam.Y_Pos = Goog.Get_AxisPos(CommParam.AxisY);
                    CommParam.Z_Pos = Goog.Get_AxisPos(CommParam.AxisZ);
                    CommParam.Y2_Pos = Goog.Get_AxisPos(CommParam.AxisY2);

                    XEnc_Pos.Text = (CommParam.X_Pos * CommParam.CirPerPulse).ToString();//CommParam.X_Pos.ToString();// CommParam.X_Pos.ToString();// 
                    YEnc_Pos.Text = (CommParam.Y_Pos * CommParam.CirPerPulse).ToString();// CommParam.Y_Pos.ToString(); //CommParam.Y_Pos.ToString(); //
                    ZEnc_Pos.Text = (CommParam.Z_Pos * CommParam.CirPerPulse).ToString();// CommParam.Z_Pos.ToString(); //CommParam.Z_Pos.ToString(); //
                }
            }
            catch (Exception x)
            {
                LogHelper.WriteErrorLog("DesignForm   get_Axis_Pose", x);
                // Logger.Recod_Log_File("DesignForm   get_Axis_Pose " + x.Message + "\r\n x.StackTrace" + x.StackTrace);
                // Console.WriteLine("DesignForm   get_Axis_Pose " + x.Message + "\r\n x.StackTrace" + x.StackTrace);
            }
        }

        //实时获取当前轴的状态  需要实时，不可睡眠，否则影响自动运行
        void Aisx_Status()//~
        {
            // Console.WriteLine("Aisx_Status :");
            while (CommParam.isAlive)//this.Visible
            {
                Thread.Sleep(20);
                Goog.Get_Status(CommParam.AxisX, out CommParam.X_Status);
                Goog.Get_Status(CommParam.AxisY, out CommParam.Y_Status);
                Goog.Get_Status(CommParam.AxisZ, out CommParam.Z_Status);
                //Goog.Get_Status(CommParam.AxisY2, out CommParam.Y2_Status);
                //Console.WriteLine("Z_Status  Timer :" + CommParam.Z_Status);
                //Console.WriteLine("X_Status  Timer :" + CommParam.X_Status);
                //Console.WriteLine("Y_Status  Timer :" + CommParam.Y_Status);
                //Console.WriteLine("Y2_Status  Timer :" + CommParam.Y2_Status);
            }
        }





        //起点
        private void start_grid_Click(object sender, EventArgs e)
        {
            if (Goog.gpi_status(6))
            {
                ShowErrorDialog("请切换到手动状态");
                return;
            }

            try
            {
                InsertDataGrid(1);
            }
            catch (Exception x)
            {
                LogHelper.WriteErrorLog("DesignForm   start_grid_Click", x);
                // Logger.Recod_Log_File("form1 QD_Click  " + x.Message + "\r\n x.StackTrace" + x.StackTrace);
                ShowErrorDialog(" 新建轨迹起点 操作异常");
            }

        }


        private void line_grid_Click(object sender, EventArgs e)
        {
            if (Goog.gpi_status(6))
            {
                ShowErrorDialog("请切换到手动状态");
                return;
            }

            try
            {
                if (this.Point_DataGrid.SelectedRows.Count > 0)
                {
                    Console.WriteLine(Point_DataGrid.SelectedRows.Count);
                    UpdateDataGrid(CommParam.Path_Line);
                }
                else
                {
                    InsertDataGrid(CommParam.Path_Line);
                }
                tj.Draw_Path_Grid();
            }
            catch (Exception x)
            {
                LogHelper.WriteErrorLog("新建轨迹直线 操作异常 DesignForm   line_grid_Click", x);
                // Logger.Recod_Log_File("form1 ZX_Click  " + x.Message + "\r\n x.StackTrace" + x.StackTrace);
                ShowErrorDialog(" 新建轨迹直线 操作异常");
            }
        }

        /// <summary>
        /// 向轨迹列表中新增数据  tag 类型   1 起点  2直线   3终点    4圆
        /// </summary>
        void InsertDataGrid(int tag)
        {
            string type = "";
            switch (tag)
            {
                case CommParam.Path_Start: type = "起点"; break;
                case CommParam.Path_Line: type = "直线"; break;
                case CommParam.Path_Acr: type = "圆弧"; break;
                case CommParam.Path_End: type = "终点"; break;
            }

            string x = VisionCalibra.ChangeVision();
            string[] xx = x.Split(",");

            int RowNum = this.Point_DataGrid.Rows.Add();
            Point_DataGrid.Rows[RowNum].Cells[0].Value = RowNum; //序号
            Point_DataGrid.Rows[RowNum].Cells[1].Value = type;
            Point_DataGrid.Rows[RowNum].Cells[2].Value = xx[0];// CommUtil.Pulse_To_Cir(CommParam.X_Pos);
            Point_DataGrid.Rows[RowNum].Cells[3].Value = xx[1];//CommUtil.Pulse_To_Cir(CommParam.Y_Pos);
            Point_DataGrid.Rows[RowNum].Cells[4].Value = CommUtil.Pulse_To_Cir(CommParam.Z_Pos);
            Point_DataGrid.Rows[RowNum].Cells[5].Value = CommUtil.Pulse_To_Cir(CommParam.Y2_Pos);
            Point_DataGrid.Rows[RowNum].Cells[6].Value = tag;
            Point_DataGrid.Rows[RowNum].Cells[7].Value = CommParam.CB_Vel;
            Point_DataGrid.Rows[RowNum].Cells[8].Value = CommParam.CB_Z_Vel;
        }


        /// <summary>
        /// 修改的方法
        /// </summary>
        public void UpdateDataGrid(int tag)
        {

            string type = "";
            switch (tag)
            {
                case CommParam.Path_Start: type = "起点"; break;
                case CommParam.Path_Line: type = "直线"; break;
                case CommParam.Path_Acr: type = "圆弧"; break;
                case CommParam.Path_End: type = "终点"; break;
            }
            //把修改后的文本框内容添加到listview中
            // int RowNum = this.Point_DataGrid.Rows.Add();

            // Point_DataGrid.SelectedRows[0].Cells[0].Value = RowNum; //序号

            string x = VisionCalibra.ChangeVision();

            string[] xx = x.Split(",");

            Point_DataGrid.SelectedRows[0].Cells[1].Value = type;
            Point_DataGrid.SelectedRows[0].Cells[2].Value = xx[0];// CommUtil.Pulse_To_Cir(CommParam.X_Pos);
            Point_DataGrid.SelectedRows[0].Cells[3].Value = xx[1];// CommUtil.Pulse_To_Cir(CommParam.Y_Pos);
            Point_DataGrid.SelectedRows[0].Cells[4].Value = CommUtil.Pulse_To_Cir(CommParam.Z_Pos);
            Point_DataGrid.SelectedRows[0].Cells[5].Value = CommUtil.Pulse_To_Cir(CommParam.Y2_Pos);
            Point_DataGrid.SelectedRows[0].Cells[6].Value = tag;
            Point_DataGrid.SelectedRows[0].Cells[7].Value = CommParam.CB_Vel;
            Point_DataGrid.SelectedRows[0].Cells[8].Value = CommParam.CB_Z_Vel;
        }


        private void end_gird_Click(object sender, EventArgs e)
        {
            if (Goog.gpi_status(6))
            {
                ShowErrorDialog("请切换到手动状态");
                return;
            }


            int count = this.Point_DataGrid.Rows.Count;
            try
            {
                InsertDataGrid(CommParam.Path_End);
                tj.Draw_Path_Grid();
            }
            catch (Exception x)
            {
                LogHelper.WriteErrorLog("新建轨迹直线 操作异常 ", x);
                //  Logger.Recod_Log_File("form1 ZD_Click  " + x.Message + "\r\n x.StackTrace" + x.StackTrace);
                ShowErrorDialog(" 新建轨迹终点 操作异常");
            }
        }

        private void save_grid_Click(object sender, EventArgs e)
        {

            //if (Goog.gpi_status(6))
            //{
            //    ShowErrorDialog("请切换到手动状态");
            //    return;
            //}

            try
            {
                if (SaveListView())
                {
                    ShowSuccessDialog(" 保存成功");
                }
            }
            catch (Exception x)
            {
                LogHelper.WriteErrorLog("保存轨迹列表 操作异常 ", x);
                // Logger.Recod_Log_File("form1 BC_Click  " + x.Message + "\r\n x.StackTrace" + x.StackTrace);
                ShowErrorDialog(" 保存轨迹列表 操作异常");
            }
        }


        /// <summary>
        /// 保存轨迹到文件
        /// </summary>
        public bool SaveListView()
        {
            StringBuilder msg = new StringBuilder();
            string path = "";
            FileUtil f = new FileUtil();
            bool saveTag = false;
            for (int i = 0; i < Point_DataGrid.Rows.Count; i++)
            {

                for (int j = 0; j < Point_DataGrid.Rows[i].Cells.Count; j++)
                {
                    if (j == 1) continue;//跳过列表中文类型行
                    msg.Append((Point_DataGrid.Rows[i].Cells[j].Value.ToString()) + ",");
                }
                msg.Append(";");
            }
            f.Save_File_Dialog(out path);
            if (!string.IsNullOrEmpty(path))
            {
                f.Del_File(path);
                saveTag = f.Save_File(path, msg.ToString());

                userControl_Canvas.ResetCanvas();
                GetData_TXT_To_Canvas(path);
            }

            return saveTag;
        }

        //打开文件
        private void open_file_Click(object sender, EventArgs e)
        {
            string path;
            //if (Goog.gpi_status(6))
            //{
            //    ShowErrorDialog("请切换到手动状态");
            //    return;
            //}
            FileUtil f = new FileUtil();
            try
            {
                if (!f.Open_File_Dialog(out path))
                {
                    return;
                }
                RemoveAll();//清空表格内容

                CommParam.open_file_path = path;//为轨迹显示打开文件路径提供数据
                uiTextBox1.Text = path;
                iniFunctoin.WriteStringToIni("Run_Path", "Path", path, CommParam.Knief_Param_Path).ToString();
                if (!string.IsNullOrEmpty(path))
                {
                    string end = path.Substring(path.Length - 3, 3);

                    if (string.Equals(end, "txt", StringComparison.OrdinalIgnoreCase)) //如果是TXT
                    {
                        GetData_TXT(path, f);

                        userControl_Canvas.ResetCanvas();
                        GetData_TXT_To_Canvas(path);
                    }
                    else if (string.Equals(end, "dxf", StringComparison.OrdinalIgnoreCase))//如果是DXF
                    {
                        GetData_DXF(path);
                    }
                }
                tj.Draw_Path_Grid();
            }
            catch (Exception x)
            {
                LogHelper.WriteErrorLog("打开轨迹文件 操作异常 ", x);
                // Console.WriteLine("form1 DK_Click  " + x.Message + "\r\n x.StackTrace" + x.StackTrace);
                // Logger.Recod_Log_File("form1 DK_Click  " + x.Message + "\r\n x.StackTrace" + x.StackTrace);
                ShowErrorDialog(" 打开轨迹文件 操作异常");
            }
        }
        void action_opencsv()
        {

            FileUtil f = new FileUtil();


            try
            {
                var path1 = iniFunctoin.ReadIniString("Run_Path", "Path", CommParam.Knief_Param_Path).ToString();
                uiTextBox1.Text = path1;
                if (!string.IsNullOrEmpty(path1))
                {
                    string end = path1.Substring(path1.Length - 3, 3);
                    if (string.Equals(end, "txt", StringComparison.OrdinalIgnoreCase)) //如果是TXT
                    {
                        GetData_TXT(path1, f);

                        userControl_Canvas.ResetCanvas();
                        GetData_TXT_To_Canvas(path1);

                    }
                    else if (string.Equals(end, "dxf", StringComparison.OrdinalIgnoreCase))//如果是DXF
                    {
                        GetData_DXF(path1);
                    }
                }
            }
            catch
            {
                LogHelper.WriteErrorLog("打开轨迹文件 操作异常 ");
                ShowErrorDialog(" 打开轨迹文件 操作异常");
            }
        }
        /// <summary>
        /// 从TXT轨迹文件中读取轨迹信息展示到列表
        /// </summary>
        void GetData_TXT(string path, FileUtil f)
        {
            string msg = "";
            f.Read_File(path, out msg);
            string[] aa = msg.Split(';');
            // List<Point_Data> point_List = new List<Point_Data>();
            int RowNum;
            for (int i = 0; i < aa.Length - 1; i++)
            {
                string c = aa[i];
                string[] ab = c.Split(',');
                int se = Convert.ToInt32(ab[5]);
                string type = "";
                switch (se)
                {
                    case CommParam.Path_Start: type = "起点"; break;
                    case CommParam.Path_Line: type = "直线"; break;
                    case CommParam.Path_End: type = "终点"; break;
                }
                RowNum = this.Point_DataGrid.Rows.Add();
                Point_DataGrid.Rows[RowNum].Cells[0].Value = RowNum; //序号
                Point_DataGrid.Rows[RowNum].Cells[1].Value = type;
                Point_DataGrid.Rows[RowNum].Cells[2].Value = ab[1];
                Point_DataGrid.Rows[RowNum].Cells[3].Value = ab[2];
                Point_DataGrid.Rows[RowNum].Cells[4].Value = ab[3];
                Point_DataGrid.Rows[RowNum].Cells[5].Value = ab[4];
                Point_DataGrid.Rows[RowNum].Cells[6].Value = ab[5];
                Point_DataGrid.Rows[RowNum].Cells[7].Value = ab[6];
                Point_DataGrid.Rows[RowNum].Cells[8].Value = ab[7];
            }

        }

        /// <summary>
        /// 从DXF文件获取轨迹信息
        /// </summary>
        PointF CAD_p1;
        PointF CAD_p2;
        double min = 0;
        List<CommParam.Point_Data> cad_Point_Data_List = new List<CommParam.Point_Data>();
        void GetData_DXF(string path)
        {
            DXFUtil dxf = new DXFUtil(path);
            List<DXFUtil.DataCircleStruct> circles = dxf.Get_Circles();
            List<CommParam.Point_Data> temp = new List<CommParam.Point_Data>();
            int RowNum;
            for (int i = 0; i < circles.Count; i++)
            {
                double r = circles[i].RPos;
                double x = circles[i].XPos;
                double y = circles[i].YPos;
                double z = circles[i].ZPos;
                // Console.WriteLine("圆心X :{0}   圆心Y:{1}   圆半径{2}", x, y, r);

                RowNum = this.Point_DataGrid.Rows.Add();
                Point_DataGrid.Rows[RowNum].Cells[0].Value = RowNum; //序号
                Point_DataGrid.Rows[RowNum].Cells[1].Value = "圆";
                Point_DataGrid.Rows[RowNum].Cells[2].Value = x;
                Point_DataGrid.Rows[RowNum].Cells[3].Value = y;
                Point_DataGrid.Rows[RowNum].Cells[4].Value = z;
                Point_DataGrid.Rows[RowNum].Cells[5].Value = r;
                Point_DataGrid.Rows[RowNum].Cells[6].Value = CommParam.Path_Circle;

                CommParam.Point_Data p = new CommParam.Point_Data();
                p.RowNum = RowNum;
                p.Point_X_Pos = x + "";
                p.Point_Y_Pos = y + "";
                p.Point_RY2_Pos = r + "";

                if (y < min)
                {
                    // min = y;
                    min = -5500;
                }

                temp.Add(p);
            }

            Console.WriteLine("min=" + min);
            foreach (var item in temp)
            {
                CommParam.Point_Data p1 = new CommParam.Point_Data();
                //修正
                Console.WriteLine("item.Point_Y_Pos=" + item.Point_Y_Pos);
                string y_xiuzheng = Convert.ToDouble(item.Point_Y_Pos) - min + "";
                //距离转脉冲
                int xc = CommUtil.Cir_To_Pulse(item.Point_X_Pos);
                int yc = CommUtil.Cir_To_Pulse(y_xiuzheng);

                double rr = Convert.ToDouble(item.Point_RY2_Pos);

                p1.RowNum = item.RowNum;
                p1.Point_X_Pos = xc + "";
                p1.Point_Y_Pos = yc + "";
                p1.Point_RY2_Pos = Convert.ToInt32(rr) + "";


                if (rr > 3)
                {
                    CAD_p1 = new PointF(float.Parse(p1.Point_X_Pos), float.Parse(p1.Point_Y_Pos));
                }
                else if (rr > 2 && rr < 3)
                {
                    CAD_p2 = new PointF(float.Parse(p1.Point_X_Pos), float.Parse(p1.Point_Y_Pos));
                }

                Console.WriteLine("CAD y轴修正后 RowNum = {0}  X_Pos = {1}   Y_Pos = {2}   R_Pos = {3} ", p1.RowNum, p1.Point_X_Pos, p1.Point_Y_Pos, p1.Point_RY2_Pos);

                cad_Point_Data_List.Add(p1);
            }
        }
        /// <summary>
        /// 移除所有行
        /// </summary>
        public void RemoveAll()
        {
            Point_DataGrid.Rows.Clear();
        }

        private void uiButton3_Click(object sender, EventArgs e)
        {
            if (Goog.gpi_status(6))
            {
                ShowErrorDialog("请切换到手动状态");
                return;
            }
            RemoveAll();
        }

        private void delete_rows_Click(object sender, EventArgs e)
        {

            if (Goog.gpi_status(6))
            {
                ShowErrorDialog("请切换到手动状态");
                return;
            }

            foreach (DataGridViewRow r in Point_DataGrid.SelectedRows)
            {
                if (!r.IsNewRow)
                {
                    Point_DataGrid.Rows.Remove(r);
                }
            }
        }
        //Point_DataGrid 双击事件
        private void Point_DataGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {


                if (Goog.gpi_status(6))
                {
                    ShowErrorDialog("请切换到手动状态");
                    return;
                }

                if (!Check_Status())
                {
                    return;
                }

                if (!ShowAskDialog("确定运行到该点位吗？")) return;

                DataGridViewRow r = Point_DataGrid.SelectedRows[0];
                string xx = r.Cells[2].Value.ToString();
                string yy = r.Cells[3].Value.ToString();
                //1008
                int x;
                int y;
                if (camera_knife_CheckBox.Checked)//相机位置
                {
                    string[] cameraPos = VisionCalibra.GetCameraPos(xx, yy);
                    xx = cameraPos[0];
                    yy = cameraPos[1];
                }
                x = CommUtil.Cir_To_Pulse(xx);
                y = CommUtil.Cir_To_Pulse(yy);

                var Go_Single_Poin_Task = Task.Factory.StartNew(() =>
                {
                    Goog.Go_Single_Poin(x, y, 10);
                });
            }
            catch
            {
                ShowErrorDialog("请选择行");
            }
        }
        /// <summary>
        /// 回原点
        /// </summary>
        public void goHome_Click(object sender, EventArgs e)
        {

            if (Goog.gpi_status(6))
            {
                ShowErrorDialog("请切换到手动状态");
                return;
            }
            if (!ShowAskDialog("确定回原点吗？")) return;
            if (CommParam.X_Status != CommParam.AxisOn && CommParam.Y_Status != CommParam.AxisOn && CommParam.Z_Status != CommParam.AxisOn)// && y2_status !=CommParam.AxisRunning
            {
                ShowErrorDialog("伺服未使能");
                Console.WriteLine("伺服未使能");
                return;

            }
            //if (CommParam.Is_Run > 0)
            //{
            //    ShowErrorDialog("机器运动中，请先停止");
            //    // Console.WriteLine("回零中");
            //    return;
            //}
            goHome.Enabled = false;
            CommParam.Is_Run = 2;

            //先回Z轴
            var Z_goHome_Task = Task.Factory.StartNew(() =>
            {

                Z_goHome();
            });

            var Check_Z_Status_Task = Task.Factory.StartNew(() =>
            {
                Check_Z_Status();
            });
        }

        void Check_Z_Status()
        {
            //   Console.WriteLine("Z回零中");
            Thread.Sleep(CommParam.sleeptime);
            while (true && CommParam.AixsOnOrOff == 1 && !CommParam.Stop_Buttom_On) //没有按下急停和停止
            {
                if (!Goog.Axis_Is_Run() && !CommParam.Aixs_Z_isGohome)//Z轴没有运动
                {
                    var X_goHome_Task = Task.Factory.StartNew(() =>
                    {
                        X_goHome();
                    });

                    var Y_goHome_Task = Task.Factory.StartNew(() =>
                    {
                        Y_goHome();
                    });

                    var Ckeck_GoHome_Over_Task = Task.Factory.StartNew(() =>
                    {
                        Ckeck_GoHome_Over();
                    });

                    break;
                }
            }
        }

        void X_goHome()
        {
            int Xsearch_home = CommUtil.Cir_To_Pulse(CommParam.Xsearch_home);
            double Xhome_acc = CommParam.Xhome_acc;
            double Xhome_dec = CommParam.Xhome_dec;
            double Xhome_vel = CommParam.Xhome_vel;
            int Xhome_offset = CommUtil.Cir_To_Pulse(CommParam.Xhome_offset);
            Goog.GoHome(CommParam.AxisX, Xsearch_home, Xhome_acc, Xhome_dec, Xhome_vel, Xhome_offset);
        }

        void Y_goHome()
        {
            int Ysearch_home = CommUtil.Cir_To_Pulse(CommParam.Ysearch_home);
            double Yhome_acc = CommParam.Yhome_acc;
            double Yhome_dec = CommParam.Yhome_dec;
            double Yhome_vel = CommParam.Yhome_vel;
            int Yhome_offset = CommUtil.Cir_To_Pulse(CommParam.Yhome_offset);
            Goog.GoHome(CommParam.AxisY, Ysearch_home, Yhome_acc, Yhome_dec, Yhome_vel, Yhome_offset);
        }

        void Z_goHome()
        {
            CommParam.Aixs_Z_isGohome = true;
            int Zsearch_home = CommUtil.Cir_To_Pulse(CommParam.Zsearch_home);
            double Zhome_acc = CommParam.Zhome_acc;
            double Zhome_dec = CommParam.Zhome_dec;
            double Zhome_vel = CommParam.Zhome_vel;
            int Zhome_offset = CommUtil.Cir_To_Pulse(CommParam.Zhome_offset);
            Goog.GoHome(CommParam.AxisZ, Zsearch_home, Zhome_acc, Zhome_dec, Zhome_vel, Zhome_offset);
            CommParam.Aixs_Z_isGohome = false;
        }
        void Y2_goHome()
        {
            int Y2search_home = CommUtil.Cir_To_Pulse(CommParam.Y2search_home);
            double Y2home_acc = CommParam.Y2home_acc;
            double Y2home_dec = CommParam.Y2home_dec;
            double Y2home_vel = CommParam.Y2home_vel;
            int Y2home_offset = CommUtil.Cir_To_Pulse(CommParam.Y2home_offset);
            Goog.GoHome(CommParam.AxisY2, Y2search_home, Y2home_acc, Y2home_dec, Y2home_vel, Y2home_offset);
        }
        void Ckeck_GoHome_Over()
        {
            Console.WriteLine("Ckeck_GoHome_Over");
            while (true && CommParam.AixsOnOrOff == 1 && !CommParam.Stop_Buttom_On) //没有按下急停和停止
            {
                Thread.Sleep(100);
                Thread.Sleep(CommParam.sleeptime);
                if (!Goog.Axis_Is_Run())
                {
                    Thread.Sleep(1000);// 3轴停止运行后，给该时间让停稳，避免位置轻微波动，然后再编码器位置清零，使得位置更精准，后期可以考虑伺服参数优化
                    Goog.Zero_Pos();//位置清零
                    CommParam.Is_Gohome = true;//是否已经回过原点的标示位
                    goHome.Enabled = true;
                    CommParam.Is_Run = 0;
                    break;
                }
            }
            //Console.WriteLine("Ckeck_GoHome_Over22222");
            //goHome.Enabled = true;
            //CommParam.Is_Run = 0;
        }
        /// <summary>
        /// 使能3轴
        /// </summary>
        private void axisOnOrOff_Click(object sender, EventArgs e)
        {
            short OnOrOff = 0;
            try
            {
                Goog.Zero_Pos();//位置清零
                if (CommParam.AixsOnOrOff == 0)
                {
                    Goog.All_Axis_On();
                    Thread.Sleep(50);
                    if (CommParam.Z_Status != 512 || CommParam.X_Status != 512 || CommParam.Y_Status != 512)
                    {
                        ShowErrorDialog("伺服使能失败！关闭软件重新打开进行使能");
                        return;
                    }
                    OnOrOff = 1;
                    IOForm.getForm().SetDoOff(CommParam.Z_Axis_Brake, IOForm.getForm().Exo[4]);//释放刹车
                }
                else
                {
                    IOForm.getForm().SetDoOn(CommParam.Z_Axis_Brake, IOForm.getForm().Exo[4]);//如果是Z轴，则开启刹车
                                                                                              // Thread.Sleep(10);
                    Goog.All_Axis_Off();
                    OnOrOff = 0;
                }
                CommParam.AixsOnOrOff = OnOrOff;
            }
            catch (Exception x)
            {
                // Goog.All_Axis_Off();
                // OnOrOff = 0;
                LogHelper.WriteErrorLog("伺服使能 操作异常" + x.Message.ToString(), x);
                ShowErrorDialog("伺服使能 操作异常");
            }
        }

        private void x_left_MouseDown(object sender, MouseEventArgs e)
        {
            if (Goog.gpi_status(6))
            {
                ShowErrorDialog("请切换到手动状态");
                return;
            }


            double vel = CommParam.XSD_Vel; // Convert.ToDouble(this.XSD_Vel.Text);
            double acc = CommParam.XSD_Acc; // Convert.ToDouble(this.XSD_Acc.Text);0421
            double dec = CommParam.XSD_Dec; // Convert.ToDouble(this.XSD_Dec.Text);
            try
            {
                Goog.Jog_Action(CommParam.AxisX, vel, acc, dec, -1);
            }
            catch (Exception x)
            {
                LogHelper.WriteErrorLog("X向左 操作异常 ", x);
                // Logger.Recod_Log_File("form1 button1_MouseDown  " + x.Message + "\r\n x.StackTrace" + x.StackTrace);
                ShowErrorDialog("X向左 操作异常");
            }
        }

        private void x_left_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                Goog.Stop_Axis(CommParam.AxisX);
            }
            catch (Exception x)
            {
                LogHelper.WriteErrorLog("X左停 操作异常 ", x);
                //Logger.Recod_Log_File("form1 button1_MouseUp  " + x.Message + "\r\n x.StackTrace" + x.StackTrace);
                ShowErrorDialog("X左停 操作异常");
            }
        }

        private void x_right_MouseDown(object sender, MouseEventArgs e)
        {
            if (Goog.gpi_status(6))
            {
                ShowErrorDialog("请切换到手动状态");
                return;
            }
            double vel = CommParam.XSD_Vel; //Convert.ToDouble(this.XSD_Vel.Text); 0421
            double acc = CommParam.XSD_Acc; //Convert.ToDouble(this.XSD_Acc.Text);
            double dec = CommParam.XSD_Dec;// Convert.ToDouble(this.XSD_Dec.Text);
            try
            {
                Goog.Jog_Action(CommParam.AxisX, vel, acc, dec, 1);
            }
            catch (Exception x)
            {
                LogHelper.WriteErrorLog("X向右 操作异常 ", x);
                // Logger.Recod_Log_File("form1 button2_MouseDown  " + x.Message + "\r\n x.StackTrace" + x.StackTrace);
                ShowErrorDialog("X向右 操作异常");
            }
        }

        private void x_right_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                Goog.Stop_Axis(CommParam.AxisX);
            }
            catch (Exception x)
            {
                LogHelper.WriteErrorLog("X向右停止 操作异常 ", x);
                // Logger.Recod_Log_File("form1 button2_MouseUp  " + x.Message + "\r\n x.StackTrace" + x.StackTrace);
                ShowErrorDialog("X向右停止 操作异常");
            }
        }

        private void y_up_MouseDown(object sender, MouseEventArgs e)
        {
            if (Goog.gpi_status(6))
            {
                ShowErrorDialog("请切换到手动状态");
                return;
            }



            double vel = CommParam.YSD_Vel;// Convert.ToDouble(this.YSD_Vel.Text);
            double acc = CommParam.YSD_Acc;// Convert.ToDouble(this.YSD_Acc.Text);
            double dec = CommParam.YSD_Dec;// Convert.ToDouble(this.YSD_Dec.Text);0421
            try
            {
                Goog.Jog_Action(CommParam.AxisY, vel, acc, dec, -1);
            }
            catch (Exception x)
            {

                LogHelper.WriteErrorLog("Y上 操作异常 ", x);
                //Logger.Recod_Log_File("form1 button4_MouseDown  " + x.Message + "\r\n x.StackTrace" + x.StackTrace);
                ShowErrorDialog(" Y上 操作异常");
            }
        }

        private void y_up_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                Goog.Stop_Axis(CommParam.AxisY);
            }
            catch (Exception x)
            {
                LogHelper.WriteErrorLog("Y上停 操作异常 ", x);
                // Logger.Recod_Log_File("form1 button4_MouseUp  " + x.Message + "\r\n x.StackTrace" + x.StackTrace);
                ShowErrorDialog(" Y上停 操作异常");
            }
        }

        private void y_down_MouseDown(object sender, MouseEventArgs e)
        {
            if (Goog.gpi_status(6))
            {
                ShowErrorDialog("请切换到手动状态");
                return;
            }

            double vel = CommParam.YSD_Vel;// Convert.ToDouble(this.YSD_Vel.Text);
            double acc = CommParam.YSD_Acc;// Convert.ToDouble(this.YSD_Acc.Text);0421
            double dec = CommParam.YSD_Dec;// Convert.ToDouble(this.YSD_Dec.Text);
            try
            {
                Goog.Jog_Action(CommParam.AxisY, vel, acc, dec, 1);
            }
            catch (Exception x)
            {
                LogHelper.WriteErrorLog(" Y下 操作异常 ", x);
                // Logger.Recod_Log_File("form1 button3_MouseDown  " + x.Message + "\r\n x.StackTrace" + x.StackTrace);
                ShowErrorDialog(" Y下 操作异常");
            }
        }

        private void y_down_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                Goog.Stop_Axis(CommParam.AxisY);
            }
            catch (Exception x)
            {
                LogHelper.WriteErrorLog("Y下停 操作异常 ", x);
                // Logger.Recod_Log_File("form1 button3_MouseUp  " + x.Message + "\r\n x.StackTrace" + x.StackTrace);
                ShowErrorDialog(" Y下停 操作异常");
            }
        }

        private void z_up_MouseDown(object sender, MouseEventArgs e)
        {
            if (Goog.gpi_status(6))
            {
                ShowErrorDialog("请切换到手动状态");
                return;
            }
            double vel = CommParam.ZSD_Vel;
            double acc = CommParam.ZSD_Acc;
            double dec = CommParam.ZSD_Dec;

            try
            {
                Goog.Jog_Action(CommParam.AxisZ, vel, acc, dec, -1);
            }
            catch (Exception x)
            {
                LogHelper.WriteErrorLog("Z上 操作异常 ", x);
                //  Logger.Recod_Log_File("form1 button6_MouseDown  " + x.Message + "\r\n x.StackTrace" + x.StackTrace);
                ShowErrorDialog(" Z上 操作异常");
            }
        }

        private void z_up_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                Goog.Stop_Axis(CommParam.AxisZ);
            }
            catch (Exception x)
            {
                LogHelper.WriteErrorLog("Z上停 操作异常 ", x);
                // Logger.Recod_Log_File("form1 button6_MouseUp  " + x.Message + "\r\n x.StackTrace" + x.StackTrace);
                ShowErrorDialog(" Z上停 操作异常");
            }
        }

        private void z_down_MouseDown(object sender, MouseEventArgs e)
        {
            if (Goog.gpi_status(6))
            {
                ShowErrorDialog("请切换到手动状态");
                return;
            }
            double vel = CommParam.ZSD_Vel;
            double acc = CommParam.ZSD_Acc;
            double dec = CommParam.ZSD_Dec;
            try
            {
                Goog.Jog_Action(CommParam.AxisZ, vel, acc, dec, 1);
            }
            catch (Exception x)
            {
                LogHelper.WriteErrorLog("  Z下 操作异常 ", x);
                // Logger.Recod_Log_File("form1 button5_MouseDown  " + x.Message + "\r\n x.StackTrace" + x.StackTrace);
                ShowErrorDialog(" Z下 操作异常");
            }
        }

        private void z_down_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                Goog.Stop_Axis(CommParam.AxisZ);
            }
            catch (Exception x)
            {
                LogHelper.WriteErrorLog("Z下停 操作异常 ", x);
                // Logger.Recod_Log_File("form1 button5_MouseUp  " + x.Message + "\r\n x.StackTrace" + x.StackTrace);
                ShowErrorDialog(" Z下停 操作异常");
            }
        }

        Thread Run1;
        Business bu;

        //自动启动
        public void auto_run_Click(object sender, EventArgs e)
        {
            //if (!ShowAskDialog("确定启动自动运行吗")) return;

            //先检查自动运行条件是否合适
            try
            {
              
                bool ok =  Check_Auto_Status();
               
                // 调用自动运行
                if (ok)
                {

                    CommParam.auto_Run_On = true;
                   
                    bu = new Business();
                   

                   // Run1 = new Thread(() =>
                   //{
                       // Zb_Run1();
                       bu.get_Pcb();
                   //});
                   // Run1.IsBackground = true;
                   // Run1.Start();

                    CommParam.Is_Run = 1;
                    auto_run.Enabled = false;
                   
                }
            }
            catch (Exception x)
            {
                LogHelper.WriteErrorLog("自动运行 操作异常 ", x);
                //  Logger.Recod_Log_File("form1 Auto_Run_Click  " + x.Message + "\r\n x.StackTrace" + x.StackTrace);
                ShowErrorDialog(" 自动运行 操作异常");
            }
        }

        /// <summary>
        /// 运行前检查
        /// </summary>
        public bool Check_Auto_Status()
        {
            //检查是否有数据
            int count = Point_DataGrid.Rows.Count;
            if (count < 1 && !CommParam.Use_Vision)//如果视觉关闭，则需要检查
            {
                ShowErrorDialog("没有执行数据，请加载数据");
                return false;
            }

            //是否在原点
            if (!CommParam.Is_Gohome)
            {
                ShowErrorDialog("请先回原点");
                return false;
            }

            //是否正在运行中
            if (CommParam.Is_Run == 1)
            {
                ShowErrorDialog("正在自动运行中");
                return false;
            }
            //检查是否使能
            if (CommParam.X_Status != CommParam.AxisOn && CommParam.Y_Status != CommParam.AxisOn)// && y2_status != CommParam.AxisRunning)
            {
                ShowErrorDialog("伺服未使能");
                return false;

            }

            //各轴是否处于停止状态
            if (Goog.Axis_Is_Run())
            {
                ShowErrorDialog("轴处于运动中，请先停止");
                return false;
            }

            if (!Goog.gpi_status(6))//手自切换
            {
                ShowErrorDialog("现在手动状态，请切换到自动状态");
                return false;
            }

            /*检查PLC*/
            //if (CommParam.PLC_top == null)
            //{

            //    CommParam.PLC_top = new MelsecAscii_TCPIP();
            //    bool Ascii_top = CommParam.PLC_top.ConnectServer_TCP(CommParam.PLC_topIp, "4899");

            //    if (!Ascii_top)
            //    {
            //        ShowErrorDialog("PLC连接失败，检查网络");
            //        return false;
            //    }
            //}

            if (CommParam.PLC_bot == null)
            {

                CommParam.PLC_bot = new MelsecAscii_TCPIP();
                bool Ascii_bot = CommParam.PLC_bot.ConnectServer_TCP(CommParam.PLC_botIp, "4899");

                if (!Ascii_bot)
                {
                    ShowErrorDialog("PLC连接失败，检查网络");
                    return false;
                }
            }

            return true;
        }

        int zt = 0;
        //根据下刀数，计算需要下去的深度
        int Down_Knife_Km()
        {
            zt++;
            int to = 0;
            if (CommParam.iskd)//是否打开功能
            {
                double t = Convert.ToDouble(BusinessForm.getForm().Knife_Total_Km_uiTextBox.Text);//总里程
                double kc = Convert.ToDouble(BusinessForm.getForm().Knife_Cur_Km_uiTextBox.Text);//已使用里程
                double kd = Convert.ToDouble(BusinessForm.getForm().Knife_Downs_uiTextBox.Text);//下刀次数
                double knum = Convert.ToDouble(BusinessForm.getForm().Knife_Downs_num.Text);//每次下刀距离
                double at = kc / t * kd;
                double x = Math.Floor(at);

                if (x > kd - 1) x = kd - 1; //如果超过规定下刀数，则用最大下刀数
                //Console.WriteLine("----------------------当前阶段:" + x);
                //Console.WriteLine("----------------------当前循环次数:" + zt);
                if (x != 0)
                {
                    double a = Convert.ToInt32(x) * knum;   //CommParam.Knife_Length;
                    string xa = a.ToString(); //进给总长度  毫米
                    return CommUtil.Cir_To_Pulse(xa);
                }
            }
            return to;

        }
        public Thread Zb_Run1_Task;
        private object O = new object();
        //单起线程调用
        public void Zb_Run1()
        {
            int dp = Down_Knife_Km();

            //CommParam.XYZ_First = true;
            // BusinessForm.getForm().totalNum++;   测试产能代码 ，正式生产需要注释
            BusinessForm.getForm().cl_num = true; //开始计时
            //CommParam.XYZ_Second = false;
            Selete = true;

            //int k_z = CommUtil.Cir_To_Pulse(Point_DataGrid.Rows[0].Cells[4].Value.ToString());
           
            string max_k = BusinessForm.getForm().Max_Knife_Downs_num.Text;
            int Max_k = CommUtil.Cir_To_Pulse(max_k);
            int In_k= CommUtil.Cir_To_Pulse(BusinessForm.getForm().In_Knife_Downs_num.Text);
            for (int i = 0; i < Point_DataGrid.Rows.Count; i++)
            {
                if (CommParam.Stop_Buttom_On || CommParam.Urgent_Buttom_On || !CommParam.auto_Run_On)
                {  
                    break;  //如果停止或者急停，则跳出
                }
                CommParam.XYZ_First = true;
                if (CommParam.Stop_Buttom_On || CommParam.Urgent_Buttom_On || !CommParam.auto_Run_On) break;
                int x = CommUtil.Cir_To_Pulse(Point_DataGrid.Rows[i].Cells[2].Value.ToString());
                int y = CommUtil.Cir_To_Pulse(Point_DataGrid.Rows[i].Cells[3].Value.ToString());
                int z = CommUtil.Cir_To_Pulse(Point_DataGrid.Rows[i].Cells[4].Value.ToString());
                //Console.WriteLine("原始脉冲:" + z);
                //z = z + dp;//在原始下刀距离+下刀数的距离
                if (In_k < Max_k)
                {
                    z = z + dp;
                    BusinessForm.getForm().In_Knife_Downs_num.Text = CommUtil.Pulse_To_Cir(z).ToString();
                }
                else if (In_k >= Max_k)
                {
                    z = Max_k;
                    BusinessForm.getForm().In_Knife_Downs_num.Text = CommUtil.Pulse_To_Cir(z).ToString();
                }
                
                //Console.WriteLine("新增脉冲:" + dp);
                //Console.WriteLine("总脉冲:" + z);
                int type = Convert.ToInt32(Point_DataGrid.Rows[i].Cells[6].Value);
                double zrun_vel = Convert.ToDouble(Point_DataGrid.Rows[i].Cells[7].Value);
                double xyvel = Convert.ToDouble(Point_DataGrid.Rows[i].Cells[8].Value);
                //Console.WriteLine("x = {0} ,y = {1} ,z = {2} ,type = {3} ,下刀速 = {4} ,走刀速 = {5}",
                //Point_DataGrid.Rows[i].Cells[2].Value.ToString(),Point_DataGrid.Rows[i].Cells[3].Value.ToString(),
                //Point_DataGrid.Rows[i].Cells[4].Value.ToString(),type,zrun_vel, xyvel);

                if (type == CommParam.Path_End)//终点，计算里程
                {
                    double Pulse = CommUtil.Distance_PP(x, y); //得到距离脉冲数
                    string mm = CommUtil.Pulse_To_Cir(Pulse);
                    if (!mm.EqualsIgnoreCase("NaN"))
                    {
                        BusinessForm.getForm().Save_Knife_Param(mm);
                    }
                }

                while (true)
                {
                    //Thread.Sleep(5);
                    if (CommParam.Stop_Buttom_On || CommParam.Urgent_Buttom_On || !CommParam.auto_Run_On)
                    {
                        Zb_Run1_Task.Abort();
                        break;  //如果停止或者急停，则跳出
                    }
                    else
                    {
                        // Console.WriteLine("CommParam.XYZ_First"+ CommParam.XYZ_First);

                        if (CommParam.XYZ_First)
                        {
                            //Zb_Run1_Task = new Thread(() =>
                            //{
                            Goog.Go_Single_Lint(x, y, z, type, xyvel, zrun_vel);
                            //});
                            //Zb_Run1_Task.IsBackground = true;
                            //Zb_Run1_Task.Start();

                            CommParam.XYZ_First = false;
                            break;
                        }

                    }                  
                  }
                //while (true)
                //{
                //    Thread.Sleep(20);

                //    // Console.WriteLine("XYZ_Second ");
                //    if (CommParam.Stop_Buttom_On || CommParam.Urgent_Buttom_On) break;  //如果停止或者急停，则跳出
                //    if (CommParam.XYZ_Second)
                //    {//编写当前点位运行到位后，需要做的业务逻辑代码
                //        //3轴到点位并且停止状态，视觉拍照
                //        Thread.Sleep(50);
                //        CommParam.XYZ_First = true;
                //        CommParam.XYZ_Second = false;
                //        break;
                //    }
                //}
            }

            Thread.Sleep(500);
            CommParam.cut_pcb_ing = false;
            //结束计时并清零
            BusinessForm.getForm().cl_num = false;
            //Thread.Sleep(100);
            //CommParam.Is_Run = 0;//执行完毕，状态置0
            //auto_run.Enabled = true;//释放自动按钮
            //Console.WriteLine("Enabled ");
            Selete = false;
        }

        public void Zb_Run1_StartPoint()
        {
            if (Point_DataGrid.Rows[0].Cells[2].Value.ToString() == "") { return; }
            //int dp = Down_Knife_Km();
            // BusinessForm.getForm().totalNum++;   测试产能代码 ，正式生产需要注释
            //BusinessForm.getForm().cl_num = true; //开始计时
            //CommParam.XYZ_Second = false;
            //for (int i = 0; i < Point_DataGrid.Rows.Count; i++)
            //{
            //if (CommParam.Stop_Buttom_On || CommParam.Urgent_Buttom_On) break;
            int x = CommUtil.Cir_To_Pulse(Point_DataGrid.Rows[0].Cells[2].Value.ToString());
            int y = CommUtil.Cir_To_Pulse(Point_DataGrid.Rows[0].Cells[3].Value.ToString());
            int z = CommUtil.Cir_To_Pulse(Point_DataGrid.Rows[0].Cells[4].Value.ToString());
            //Console.WriteLine("原始脉冲:" + z);
            //z = z + dp;//在原始下刀距离+下刀数的距离
            //Console.WriteLine("新增脉冲:" + dp);
            //Console.WriteLine("总脉冲:" + z);
            z = 0;
            int type = Convert.ToInt32(Point_DataGrid.Rows[0].Cells[6].Value);
            double zrun_vel = Convert.ToDouble(Point_DataGrid.Rows[0].Cells[7].Value);
            double xyvel = Convert.ToDouble(Point_DataGrid.Rows[0].Cells[8].Value);
            //Console.WriteLine("x = {0} ,y = {1} ,z = {2} ,type = {3} ,下刀速 = {4} ,走刀速 = {5}",
            //Point_DataGrid.Rows[i].Cells[2].Value.ToString(),Point_DataGrid.Rows[i].Cells[3].Value.ToString(),
            //Point_DataGrid.Rows[i].Cells[4].Value.ToString(),type,zrun_vel, xyvel);
            
            if (type == CommParam.Path_End)//终点，计算里程
            {
                double Pulse = CommUtil.Distance_PP(x, y); //得到距离脉冲数
                string mm = CommUtil.Pulse_To_Cir(Pulse);
                if (!mm.EqualsIgnoreCase("NaN"))
                {
                    BusinessForm.getForm().Save_Knife_Param(mm);
                }
            }

            Goog.Go_Single_Lint(x, y, z, type, xyvel, zrun_vel);

            //Thread Zb_Run1_Task = new Thread(() =>
            //{
            //    Goog.Go_Single_Lint(x, y, z, type, xyvel, zrun_vel);
            //});
            //Zb_Run1_Task.IsBackground = true;
            //Zb_Run1_Task.Start();

           

        }

        public void Zb_Run1_SeleteStartPoint(string Cir_X, string Cir_Y, string Cir_Z)
        {
            if (Cir_X == ""|| Cir_Y == ""|| Cir_Z == "") { return; }
            //int dp = Down_Knife_Km();
            // BusinessForm.getForm().totalNum++;   测试产能代码 ，正式生产需要注释
            //BusinessForm.getForm().cl_num = true; //开始计时
            //CommParam.XYZ_Second = false;
            //for (int i = 0; i < Point_DataGrid.Rows.Count; i++)
            //{
            //if (CommParam.Stop_Buttom_On || CommParam.Urgent_Buttom_On) break;
            int x = CommUtil.Cir_To_Pulse(Cir_X);
            int y = CommUtil.Cir_To_Pulse(Cir_Y);

            int z = CommUtil.Cir_To_Pulse(Cir_Z);
            //Console.WriteLine("原始脉冲:" + z);
            //z = z + dp;//在原始下刀距离+下刀数的距离
            //Console.WriteLine("新增脉冲:" + dp);
            //Console.WriteLine("总脉冲:" + z);

            int type = 1;// Convert.ToInt32(Point_DataGrid.Rows[0].Cells[6].Value);
            double zrun_vel = Convert.ToDouble(Point_DataGrid.Rows[0].Cells[7].Value);
            double xyvel = Convert.ToDouble(Point_DataGrid.Rows[0].Cells[8].Value);
            //Console.WriteLine("x = {0} ,y = {1} ,z = {2} ,type = {3} ,下刀速 = {4} ,走刀速 = {5}",
            //Point_DataGrid.Rows[i].Cells[2].Value.ToString(),Point_DataGrid.Rows[i].Cells[3].Value.ToString(),
            //Point_DataGrid.Rows[i].Cells[4].Value.ToString(),type,zrun_vel, xyvel);

            //if (type == CommParam.Path_End)//终点，计算里程
            //{
            //    double Pulse = CommUtil.Distance_PP(x, y); //得到距离脉冲数
            //    string mm = CommUtil.Pulse_To_Cir(Pulse);
            //    if (!mm.EqualsIgnoreCase("NaN"))
            //    {
            //        BusinessForm.getForm().Save_Knife_Param(mm);
            //    }
            //}

            Goog.Go_Single_Lint(x, y, z, type, xyvel, zrun_vel);

            //Thread Zb_Run1_Task = new Thread(() =>
            //{
            //    Goog.Go_Single_Lint(x, y, z, type, xyvel, zrun_vel);
            //});
            //Zb_Run1_Task.IsBackground = true;
            //Zb_Run1_Task.Start();



        }

        private void auto_stop_Click(object sender, EventArgs e)
        {
            try
            {

                Stop_Auto();
                     

            }
            catch (Exception x)
            {
                LogHelper.WriteErrorLog("auto_stop_Click", x);
                // Logger.Recod_Log_File(" Auto_Stop_Click  " + x.Message + "\r\n x.StackTrace" + x.StackTrace);
                ShowErrorDialog(" 自动停止 操作异常");
            }
        }


        public void Stop_Auto()
        {
            CommParam.auto_Run_On = false;
            CommParam.Is_Run = 0;
            DesignForm.getForm().auto_run.Enabled = true;
            CommParam.XYZ_First = true;
            CommParam.XYZ_Second = false;
            IOForm.getForm().SetDoOn(8, IOForm.getForm().Exo[7]);// DO7 停止进料电机
            IOForm.getForm().SetDoOn(9, IOForm.getForm().Exo[8]);//
            Goog.SetExtDo_Off(9);

            if (CommParam.xianchen_on)
            {
                bu.stop_xiancheng();//退出线程
                CommParam.xianchen_on=false;
            }
           

            //if (CommParam.PLC_bot != null)
            //{

            //    CommParam.PLC_bot.Write_D_short("D280", 1);
            //}


            //if (bu != null) bu.m = 0;
            //try
            //{
            //    bu.stop_Cut();
            //}
            //catch (Exception) { Console.WriteLine(" bu.t.Abort();--"); }

            //try
            //{
            //    bu.t.Abort();
            //}
            //catch (Exception) { Console.WriteLine(" bu.t.Abort();--"); }


            //try
            //{
            //    bu.t1.Abort();
            //}
            //catch (Exception) { Console.WriteLine("   bu.t1.Abort();--"); }

            //try
            //{
            //    bu.t2.Abort();
            //}
            //catch (Exception) { Console.WriteLine(" bu.t2.Abort(); --"); }

            //try
            //{
            //    Run1.Abort();
            //}
            //catch (Exception) { Console.WriteLine("Run1.Abort(); --"); }

            //try
            //{
            //    bu.PcbRun.Abort();
            //}
            //catch (Exception) { Console.WriteLine(" bu.PcbRun.Abort(); --"); }

            //try
            //{
            //    bu.checkNif.Abort();
            //}
            //catch (Exception) { Console.WriteLine(" bu.checkNif.Abort(); --"); }



        }




        #region 视觉部分
        private void btn实时(object sender, EventArgs e)
        {
            RecordDis.StartLiveDisplay(Vision.AcqTool.Operator, false);

            Task t1 = new Task(DrawLine);
            t1.Start();
        }

        private void btnStopLive(object sender, EventArgs e)
        {
            RecordDis.StopLiveDisplay();


        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            RecordDis.Fit();
        }

        public void DrawLine()
        {
            while (true)
            {
                Thread.Sleep(40);
                try
                {

                    lock (loker)
                    {
                        Graphics g = RecordDis.CreateGraphics();
                        if (this.Location.X != WindowsX || this.Location.Y != WindowsY)
                        {
                            g.Clear(this.RecordDis.BackColor);
                        }
                        WindowsX = this.Location.X;
                        WindowsY = this.Location.Y;
                        int X = RecordDis.Size.Width;
                        int Y = RecordDis.Size.Height;
                        Pen pen = new Pen(Color.Red);
                        pen.Width = 2;
                        g.DrawLine(pen, 0, Y / 2, X, Y / 2);
                        g.DrawLine(pen, X / 2, 0, X / 2, Y);

                        /******画圆形和矩形改*******************/
                        if (uiCheckBoxCircle.Checked)
                        {
                            g.DrawEllipse(pen, X / 2 - Vision.Circle / 2, Y / 2 - Vision.Circle / 2, Vision.Circle, Vision.Circle);
                        }
                        if (uiCheckBoxRantangle.Checked)
                        {
                            g.DrawRectangle(pen, X / 2 - Vision.Wide / 2, Y / 2 - Vision.High / 2, Vision.Wide, Vision.High);
                        }
                        pen.Dispose();
                        g.Dispose();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "图形绘制出错", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion





        // int cad_cali = 0;
        PointF p1;
        PointF p2;
        private void CAD_Cali_Button_Click(object sender, EventArgs e)
        {

            if (Goog.gpi_status(6))
            {
                ShowErrorDialog("请切换到手动状态");
                return;
            }



            int count = Point_DataGrid.Rows.Count;
            if (count == 0)
            {
                UIMessageDialog.ShowMessageDialog("请先导入CAD数据", UILocalize.InfoTitle, false, Style);
                return;
            }



            int x = Point_DataGrid.SelectedIndex;
            if (x == 0)
            {
                if (!ShowAskDialog("确认校准第一个点吗")) return;
                p1 = new PointF();
                p1.X = (float)CommParam.X_Pos;//39905;//
                p1.Y = (float)CommParam.Y_Pos;//83904;// 
                Console.WriteLine("---P1----");
                Console.WriteLine(p1.X);
                Console.WriteLine(p1.Y);
                Console.WriteLine("---P1----");
                // cad_cali++;


            }
            else
            {
                if (!ShowAskDialog("确认校准第二个点吗")) return;
                p2 = new PointF();
                p2.X = (float)CommParam.X_Pos;//60115;//
                p2.Y = (float)CommParam.Y_Pos;//69936;// 
                Console.WriteLine("---P2----");
                Console.WriteLine(p2.X);
                Console.WriteLine(p2.Y);
                Console.WriteLine("---P2----");
                //test1();
            }

        }

        /*
       


            */

        void test1()
        {
            double y_off = 0;
            double x_off = 0;
            foreach (var item in cad_Point_Data_List)
            {

                PointF cad_p1;
                string r = item.Point_RY2_Pos;
                if (int.Parse(r) == 3)
                {
                    cad_p1 = new PointF(float.Parse(item.Point_X_Pos + ""), float.Parse(item.Point_Y_Pos + ""));
                    // 偏移量
                    x_off = Math.Abs(cad_p1.X - p1.X);
                    y_off = Math.Abs(cad_p1.Y - p1.Y);
                    Console.WriteLine("x_off={0}   y_off={1}", x_off, y_off);
                    break;
                }
            }

            RemoveAll();
            foreach (var itemt in cad_Point_Data_List)
            {

                double x = double.Parse(itemt.Point_X_Pos) - x_off;
                double y = double.Parse(itemt.Point_Y_Pos) - y_off;
                int row = itemt.RowNum;
                string r = itemt.Point_RY2_Pos;
                int xx = Convert.ToInt32(x);
                int yy = Convert.ToInt32(y);
                Console.WriteLine();
                Console.WriteLine("偏移后脉冲  row= {0}  x = {1}  y = {2}  r = {3}", row, xx, yy, r);
                string xxx = CommUtil.Pulse_To_Cir(x);
                string yyy = CommUtil.Pulse_To_Cir(y);
                Console.WriteLine("实际距离  row= {0}  x = {1}  y = {2}  r = {3}", row, xxx, yyy, r);
                tj.testCad(xx, yy, Convert.ToInt32(r));


                //计算后 在列表中显示
                int RowNum = this.Point_DataGrid.Rows.Add();
                Point_DataGrid.Rows[RowNum].Cells[0].Value = row; //序号
                Point_DataGrid.Rows[RowNum].Cells[1].Value = "圆";
                Point_DataGrid.Rows[RowNum].Cells[2].Value = xxx;
                Point_DataGrid.Rows[RowNum].Cells[3].Value = yyy;
                Point_DataGrid.Rows[RowNum].Cells[4].Value = 190;
                Point_DataGrid.Rows[RowNum].Cells[5].Value = r;
                Point_DataGrid.Rows[RowNum].Cells[6].Value = CommParam.Path_Circle;
                //分区保存
            }
            SaveCad_Point();
        }

        /*
        CAD导入后，实际走点顺序  对应螺丝板
 左上区	0	51	10		22	49	34	右上区
	    1	74	58		56	57	35	
	    2	73	11		23	87	36	
	    3	72	96		103	86	37	
	    67	71	12		24	85	81	
	    4	70	62		64	84	38	
		    63	13		25	48		
		    97	69		83	98		
		    59	14		26	61		
			    68		82			
			    15		27			
								
左下区	5	60	16		28	88	39	右下区
	    75	100	76		90	99	89	
	    6	47	17		29	46	40	
	    7	44	77		91	45	41	
	    8	78	18		30	92	42	
	    9	79	65		66	93	43	
		    80	19		31	94		
		    52	101		102	55		
		    50	20		32	95		
			    53		54			
			    21		33			
            
            
            */

        public void SaveCad_Point()
        {
            StringBuilder blt = new StringBuilder();
            FileUtil f = new FileUtil();

            //每个区域按照UI顺序  编排的位置
            // 比如  lb 中的5 即列表中的第6行数据（0开始），UI中左下区第一个点位27号
            int[] lt = new int[] { 0, 1, 2, 3, 67, 4, 51, 74, 73, 72, 71, 70, 63, 97, 59, 10, 58, 11, 96, 12, 62, 13, 69, 14, 68, 15 };
            int[] lb = new int[] { 5, 75, 6, 7, 8, 9, 60, 100, 47, 44, 78, 79, 80, 52, 50, 16, 76, 17, 77, 18, 65, 19, 101, 20, 53, 21 };
            int[] rt = new int[] { 22, 56, 23, 103, 24, 64, 25, 83, 26, 82, 27, 49, 57, 87, 86, 85, 84, 48, 98, 61, 34, 35, 36, 37, 81, 38 };
            int[] rb = new int[] { 28, 90, 29, 91, 30, 66, 31, 102, 32, 54, 33, 88, 99, 46, 45, 92, 93, 94, 55, 95, 39, 89, 40, 41, 42, 43 };

            for (int i = 0; i < lt.Length; i++)
            {

                int num = lt[i];
                int count = Point_DataGrid.Rows[num].Cells.Count;
                for (int j = 0; j < count; j++)
                {
                    if (j == 1) continue;
                    blt.Append((Point_DataGrid.Rows[num].Cells[j].Value.ToString()) + ",");
                }
                blt.Append(";");
            }
            f.Save_File(CommParam.FifthGe_leftTop, blt.ToString());

            blt.Clear();
            for (int i = 0; i < lb.Length; i++)
            {

                int num = lb[i];
                int count = Point_DataGrid.Rows[num].Cells.Count;
                for (int j = 0; j < count; j++)
                {
                    if (j == 1) continue;
                    blt.Append((Point_DataGrid.Rows[num].Cells[j].Value.ToString()) + ",");
                }
                blt.Append(";");
            }
            f.Save_File(CommParam.FifthGe_leftbot, blt.ToString());


            blt.Clear();
            for (int i = 0; i < rt.Length; i++)
            {
                int num = rt[i];
                int count = Point_DataGrid.Rows[num].Cells.Count;
                for (int j = 0; j < count; j++)
                {
                    if (j == 1) continue;
                    blt.Append((Point_DataGrid.Rows[num].Cells[j].Value.ToString()) + ",");
                }
                blt.Append(";");
            }
            f.Save_File(CommParam.FifthGe_rightTop, blt.ToString());

            blt.Clear();
            for (int i = 0; i < rb.Length; i++)
            {
                int num = rb[i];
                int count = Point_DataGrid.Rows[num].Cells.Count;
                for (int j = 0; j < count; j++)
                {
                    if (j == 1) continue;
                    blt.Append((Point_DataGrid.Rows[num].Cells[j].Value.ToString()) + ",");
                }
                blt.Append(";");
            }
            f.Save_File(CommParam.FifthGe_rightBot, blt.ToString());


        }

        private void Mod_Param_uiButton1_Click(object sender, EventArgs e)
        {
            if (mod_param_text.Text == "")
            {
                ShowErrorDialog("修改值不能为空！");
                return;
            }
            //if (Goog.gpi_status(6))
            //{
            //    ShowErrorDialog("请切换到手动状态");
            //    return;
            //}

            if (!ShowAskDialog("确定修改参数吗？")) return;

            int c = Point_DataGrid.SelectedCells.Count;
            for (int i = 0; i < c; i++)
            {
                Point_DataGrid.SelectedCells[i].Value = mod_param_text.Text;
            }



        }

        private void test()
        {
            double y_off = 0;
            double x_off = 0;
            PointF cad_p1;

            //旋转
            Console.WriteLine("min" + min);
            List<Cpoin.Point_Change> a = Cpoin.GetPoint(p1, p2, CAD_p1, CAD_p2, cad_Point_Data_List);

            foreach (var item in a)
            {
                string r = item.R;
                if (int.Parse(r) == 3)
                {
                    cad_p1 = new PointF(float.Parse(item.X_Pos + ""), float.Parse(item.Y_Pos + ""));
                    // 偏移量
                    x_off = cad_p1.X - p1.X;
                    y_off = cad_p1.Y - p1.Y;
                    Console.WriteLine("x_off={0}   x_off={1}", x_off, y_off);
                    break;
                }
            }

            foreach (var item in a)
            {
                //平移 
                double x = item.X_Pos - x_off;
                double y = item.Y_Pos - y_off;
                int row = item.Row_Num;
                string r = item.R;
                // double rr = CommUtil.TransData(r, 3);
                int xx = Convert.ToInt32(x);
                int yy = Convert.ToInt32(y);
                Console.WriteLine();
                Console.WriteLine("偏移后脉冲  row= {0}  x = {1}  y = {2}  r = {3}", row, xx, yy, r);
                Console.WriteLine("实际距离  row= {0}  x = {1}  y = {2}  r = {3}", row, CommUtil.Pulse_To_Cir(x), CommUtil.Pulse_To_Cir(y), r);
                tj.testCad(xx, yy, Convert.ToInt32(r));
            }


        }

        private void uiButton1_Click(object sender, EventArgs e)
        {
            IOForm.getForm().SetDoOff(13, IOForm.getForm().Exo[12]); //DO12 主轴冷却吹风   开启
            Thread.Sleep(300);//吹风和主轴不可同时启动
            IOForm.getForm().SetDoOff(14, IOForm.getForm().Exo[13]);// DO13 主轴启动 开始旋转
            Thread.Sleep(500);//启动后等半秒开始检测
            bool err = Goog.GetExtIoBit(1);
            Thread.Sleep(100);
            bool run1 = true;//Goog.GetExtIoBit(2);
            Thread.Sleep(100);
            bool run2 = Goog.GetExtIoBit(3);
            if (!err || !run1 || !run2)
            {
                ShowErrorDialog("切割旋转主轴异常，检查主轴控制器" + err + run1 + run2);
                return;
            }
        }

        private void uiButton2_Click(object sender, EventArgs e)
        {
            IOForm.getForm().SetDoOn(13, IOForm.getForm().Exo[12]);
            IOForm.getForm().SetDoOn(14, IOForm.getForm().Exo[13]);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string XY = VisionCalibra.ChangeVision();
        }

        private void Add_Param_uiButton_Click(object sender, EventArgs e)
        {
            if (mod_param_text.Text == "")
            {
                ShowErrorDialog("修改值不能为空！");
                return;
            }
            double val = Convert.ToDouble(mod_param_text.Text);
            if (!ShowAskDialog("确定将选定的参数加 " + val + " 吗？")) return;

            int c = Point_DataGrid.SelectedCells.Count;
            for (int i = 0; i < c; i++)
            {
                double old = Convert.ToDouble(Point_DataGrid.SelectedCells[i].Value);
                Point_DataGrid.SelectedCells[i].Value = old + val;
            }
        }

        private void Sub_Param_uiButton_Click(object sender, EventArgs e)
        {
            if (mod_param_text.Text == "")
            {
                ShowErrorDialog("修改值不能为空！");
                return;
            }
            double val = Convert.ToDouble(mod_param_text.Text);
            if (!ShowAskDialog("确定将选定的参数减 " + val + " 吗？")) return;

            int c = Point_DataGrid.SelectedCells.Count;
            for (int i = 0; i < c; i++)
            {
                double old = Convert.ToDouble(Point_DataGrid.SelectedCells[i].Value);
                Point_DataGrid.SelectedCells[i].Value = old - val;
            }
        }

        private void Cut_One_PCB_uiButton_Click(object sender, EventArgs e)
        {

            if (!Check_Status())
            {
                return;
            }
            if (!ShowAskDialog("确定启动铣刀切一次板吗")) return;


            var Go_Single_Poin_Task = Task.Factory.StartNew(() =>
            {
                For_More_Cut(1);
            });
        }


        void For_More_Cut(int x)
        {
            if (!Check_Status())
            {
                return;
            }
            CommParam.auto_Run_On = true;
            if (x == 1)//如果是单切，则启动切割主轴
            {
                bu = new Business();
                bu.Start_Cut();
            }

            for (int i = 0; i < x; i++)
            {
                if(CommParam.auto_Run_On == false) { break; }

                Zb_Run1();
            }

            if (x == 1)//如果是单切，则启动切割主轴
            {
                bu.stop_Cut();
            }
            CommParam.auto_Run_On = false;
        }

        private void uiButton1_Click_1(object sender, EventArgs e)
        {
            if (!Check_Status())
            {
                return;
            }
            string ss = mod_param_text.Text;
            if (ss == "")
            {
                ShowErrorDialog("在输入框中输入老化次数");
                return;
            }
            int time = Convert.ToInt32(ss);

            if (!ShowAskDialog("确定启动老化，循环跑 " + time + " 次吗")) return;
            var Go_Single_Poin_Task = Task.Factory.StartNew(() =>
            {
                For_More_Cut(time);
            });
        }

        public void showMessage(string msg)
        {
            ShowErrorDialog(msg);
        }



        bool Check_Status()
        {
            int count = Point_DataGrid.Rows.Count;
            if (count < 1 && !CommParam.Use_Vision)//如果视觉关闭，则需要检查
            {
                ShowErrorDialog("没有执行数据，请加载数据");
                return false;
            }

            //是否在原点
            if (!CommParam.Is_Gohome)
            {
                ShowErrorDialog("请先回原点");
                return false;
            }

            //是否正在运行中
            if (CommParam.Is_Run == 1)
            {
                ShowErrorDialog("正在自动运行中");
                return false;
            }
            //检查是否使能
            if (CommParam.X_Status != CommParam.AxisOn && CommParam.Y_Status != CommParam.AxisOn)// && y2_status != CommParam.AxisRunning)
            {
                ShowErrorDialog("伺服未使能");
                return false;

            }

            //各轴是否处于停止状态
            if (Goog.Axis_Is_Run())
            {
                ShowErrorDialog("轴处于运动中，请先停止");
                return false;
            }
            return true;
        }


        private void ZhenLie_Button_Click(object sender, EventArgs e)
        {
            int count = Point_DataGrid.Rows.Count;
            if (count < 1)
            {
                ShowErrorDialog("列表没有基础点位数据，请先定数据");
                return;
            }
            if (!ShowAskDialog("确定生成阵列吗")) return;

            int xNum = Convert.ToInt32(xNum_TextBox.Text);
            int yNum = Convert.ToInt32(yNum_TextBox.Text);
            double x_dis = Convert.ToDouble(x_dis_TextBox.Text);
            double y_dis = Convert.ToDouble(y_dis_TextBox.Text);

            ZhenLie(xNum, yNum, x_dis, y_dis);
        }
        /// <summary>
        /// <param name="xNum">X方向小板数</param>
        /// <param name="yNum">Y方向小板数</param>
        /// <param name="x_dis">X方向相鄰兩板相對點之間的距離</param>
        /// <param name="y_dis">Y方向相鄰兩板相對點之間的距離</param>
        /// </summary>
        void ZhenLie(int xNum, int yNum, double x_dis, double y_dis)
        {

            List<string> h = new List<string>();
            List<string> s = new List<string>();
            int count = Point_DataGrid.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                // int X_Xuhao = Convert.ToInt32(Point_DataGrid.Rows[i].Cells[0].Value);
                // int type = Convert.ToInt32(Point_DataGrid.Rows[i].Cells[6].Value);//轨迹类型 1 起点  2直线  3 终点
                double X_1 = Convert.ToDouble(Point_DataGrid.Rows[i].Cells[2].Value.ToString());
                double Y_1 = Convert.ToDouble(Point_DataGrid.Rows[i].Cells[3].Value.ToString());
                double X_2 = Convert.ToDouble(Point_DataGrid.Rows[i + 1].Cells[2].Value.ToString());
                double Y_2 = Convert.ToDouble(Point_DataGrid.Rows[i + 1].Cells[3].Value.ToString());

                double a = X_1 - X_2;
                double b = Y_1 - Y_2;
                if (Math.Abs(a) >= Math.Abs(b))
                {
                    h.Add(X_1 + "," + Y_1 + "," + X_2 + "," + Y_2);
                    //  Console.WriteLine(i + "heng" + X_1 + "," + Y_1 + "," + X_2 + "," + Y_2);
                }
                else
                {
                    s.Add(X_1 + "," + Y_1 + "," + X_2 + "," + Y_2);
                    //   Console.WriteLine(i + "shu" + X_1 + "," + Y_1 + "," + X_2 + "," + Y_2);
                }

                i++;
            }


            RemoveAll();
            Console.WriteLine("横切点");

            for (int ii = 0; ii < yNum + 1; ii++)
            {
                double yp = ii * y_dis;
                for (int j = 0; j < xNum; j++)
                {
                    double xp = j * x_dis;
                    for (int i = 0; i < h.Count; i++)
                    {
                        string p = h[i];
                        string[] pp = p.Split(",");
                        double psx = double.Parse(pp[0]) + xp;
                        double psy = double.Parse(pp[1]) + yp;
                        double pex = double.Parse(pp[2]) + xp;
                        double pey = double.Parse(pp[3]) + yp;
                        ZhenLie_Insert(CommParam.Path_Start, psx + "", psy + "");
                        ZhenLie_Insert(CommParam.Path_End, pex + "", pey + "");
                    }
                }
            }


            Console.WriteLine("竖切点");

            for (int ii = xNum; ii >= 0; ii--)
            {
                double xp = ii * x_dis;
                for (int j = yNum - 1; j >= 0; j--)
                {
                    double yp = j * y_dis;
                    for (int i = 0; i < s.Count; i++)
                    {
                        string p = s[i];
                        string[] pp = p.Split(",");
                        double psx = double.Parse(pp[0]) + xp;
                        double psy = double.Parse(pp[1]) + yp;
                        double pex = double.Parse(pp[2]) + xp;
                        double pey = double.Parse(pp[3]) + yp;
                        ZhenLie_Insert(CommParam.Path_Start, pex + "", pey + "");
                        ZhenLie_Insert(CommParam.Path_End, psx + "", psy + "");
                    }
                }
            }

            tj.Draw_Path_Grid();
        }


        void ZhenLie_Insert(int tag, string x, string y)
        {
            string type = "";
            switch (tag)
            {
                case CommParam.Path_Start: type = "起点"; break;
                case CommParam.Path_End: type = "终点"; break;
            }

            int RowNum = this.Point_DataGrid.Rows.Add();
            Point_DataGrid.Rows[RowNum].Cells[0].Value = RowNum; //序号
            Point_DataGrid.Rows[RowNum].Cells[1].Value = type;
            Point_DataGrid.Rows[RowNum].Cells[2].Value = x;
            Point_DataGrid.Rows[RowNum].Cells[3].Value = y;
            Point_DataGrid.Rows[RowNum].Cells[4].Value = CommUtil.Pulse_To_Cir(CommParam.Z_Pos);
            Point_DataGrid.Rows[RowNum].Cells[5].Value = 0;
            Point_DataGrid.Rows[RowNum].Cells[6].Value = tag;
            Point_DataGrid.Rows[RowNum].Cells[7].Value = CommParam.CB_Vel;
            Point_DataGrid.Rows[RowNum].Cells[8].Value = CommParam.CB_Z_Vel;
        }
        #endregion



        #region 新开发控件测试代码

        private UserControl_Canvas userControl_Canvas;

        private ElementHost _elemHost = new ElementHost(); // WPF载体

        //private ComBoBoxButton _cbb = new ComBoBoxButton(); // WPF控件
        public void InitializeWPFCanvas()
        {
            userControl_Canvas = new UserControl_Canvas();
            //_elemHost.Location = new Point(50, 50);

            _elemHost.Child = userControl_Canvas; // 绑定
            _elemHost.Dock = DockStyle.Fill;

            //_elemHost.Width = this.Width;

            //_elemHost.Height = this.Height;

            panel1.Controls.Add(_elemHost);

            userControl_Canvas.MouseDown += UserControl_Canvas_MouseDown;

            userControl_Canvas.ResetCanvas();

            //this.Point_DataGrid.SelectionMode = DataGridViewSelectionMode.FullColumnSelect;
            this.Point_DataGrid.ClearSelection();
        }

        private void UserControl_Canvas_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            for(int i=0;i< Point_DataGrid.Rows.Count;i++)
            {
                Point_DataGrid.Rows[i].Selected = false;
            }
            //throw new NotImplementedException();
            if (userControl_Canvas.SeleteListPointF.Count > 0 && Selete==false)
            {
                //Thread t = new Thread(() =>
                //{
                    //添加运行代码
                Thread.Sleep(100);
                var startPoing = userControl_Canvas.SeleteListPointF[0];

                int seleteRws = int.Parse( startPoing.ID.ToString())*2-2;
                Point_DataGrid.Rows[seleteRws].Selected = true;

                userControl_Canvas.Run_cursors(startPoing.StartPoint1);

                int x = CommUtil.Cir_To_Pulse(startPoing.StartPoint1.X.ToString());
                int y = CommUtil.Cir_To_Pulse(startPoing.StartPoint1.Y.ToString());
                int z = CommUtil.Cir_To_Pulse("0");

                Goog.Go_Single_Poin(x, y, z);

                Invoke(new Action(delegate//canvas.Dispatcher.BeginInvoke(new Action(() =>
                {
                    userControl_Canvas.ResetRunPath();
                }));
                userControl_Canvas.SeleteListPointF.Clear();

                //});
                //t.SetApartmentState(ApartmentState.STA);
                //t.IsBackground = true;
                //t.Start();
                
            }
        }

        public void OpenDXF()
        {
            userControl_Canvas.Open();
        }
        /// <summary>
        /// 模拟
        /// </summary>
        public void simulation_Run()
        {
            Thread t = new Thread(() =>
            {

                //while (true)
                //{
                Thread.Sleep(50);
                Invoke(new Action(delegate//canvas.Dispatcher.BeginInvoke(new Action(() =>
                {
                    userControl_Canvas.ResetRunPath();
                }));
                Run();
                //下面写一些在线程中处理的方法
                //}


            });
            t.SetApartmentState(ApartmentState.STA);
            t.IsBackground = true;
            t.Start();
        }

        public void AddTestPoint2(PointF startP1, PointF stopP1)
        {
            ListPointF NewlistPointF = new ListPointF();
            NewlistPointF.StartPoint1 = startP1;
            NewlistPointF.EndPoint1 = stopP1;

            userControl_Canvas.RunListPointF.Add(NewlistPointF);
        }

        public void Run()
        {
            List<ListPointF> listpointf = null;
            Invoke(new Action(delegate//canvas.Dispatcher.BeginInvoke(new Action(() =>
            {
                listpointf = userControl_Canvas.RunListPointF;
            }));
            foreach (ListPointF listPointF in listpointf)
            {

                string DrawType = listPointF.Type.ToUpper();
                switch (DrawType)
                {
                    case "POINT":
                        Invoke(new Action(delegate//canvas.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            userControl_Canvas.Run_cursors(listPointF.StartPoint1);
                            userControl_Canvas.SetRunPath(listPointF.ID, System.Windows.Media.Colors.Green);
                        }));
                        break;
                    case "LINE":
                        Invoke(new Action(delegate//canvas.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            userControl_Canvas.Run_cursors(listPointF.StartPoint1);
                            userControl_Canvas.SetRunPath(listPointF.ID, System.Windows.Media.Colors.Green);
                        }));
                        break;
                    case "SPLINE":
                        Invoke(new Action(delegate//canvas.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            userControl_Canvas.Run_cursors(listPointF.Polylines[0]);
                            userControl_Canvas.SetRunPath(listPointF.ID, System.Windows.Media.Colors.Green);
                        }));
                        break;
                    case "POLYLINES":
                        Invoke(new Action(delegate//canvas.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            userControl_Canvas.Run_cursors(listPointF.Polylines[0]);
                            userControl_Canvas.SetRunPath(listPointF.ID, System.Windows.Media.Colors.Green);
                        }));
                        break;
                    case "LWPOLYLINE":
                        Invoke(new Action(delegate//canvas.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            userControl_Canvas.Run_cursors(listPointF.Polylines[0]);
                            userControl_Canvas.SetRunPath(listPointF.ID, System.Windows.Media.Colors.Green);
                        }));
                        break;
                    case "CIRCLE":
                        Invoke(new Action(delegate//canvas.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            userControl_Canvas.Run_cursors(listPointF.CentrePoint);
                            userControl_Canvas.SetRunPath(listPointF.ID, System.Windows.Media.Colors.Green);
                        }));
                        break;
                    case "ELLIPSE":
                        //DrawEllipses(PointF Center, float Radius, int StartAngle, int EndAngle);
                        break;
                    case "ARC":
                        Invoke(new Action(delegate//canvas.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            userControl_Canvas.Run_cursors(listPointF.StartPoint1);
                            userControl_Canvas.SetRunPath(listPointF.ID, System.Windows.Media.Colors.Green);
                        }));
                        break;
                    case "MTEXT":
                        //drawText(PointF pointF, string text)
                        break;
                    case "HATCH":
                        break;
                    case "3DFACE":
                        break;
                    case "INSERT":
                        break;
                    case "SOLID":
                        break;
                    case "TEXT":
                        //drawText(PointF pointF, string text)
                        break;
                    case "TRACE":
                        break;
                    case "FXLINE":
                        break;
                    case "FVIEWPORT":


                        break;
                }
            }
        }

        /// <summary>
        /// 运行到选中
        /// </summary>
        public void RunSelete()
        {
            List<ListPointF> listpointf = null;
            Invoke(new Action(delegate//canvas.Dispatcher.BeginInvoke(new Action(() =>
            {
                listpointf = userControl_Canvas.SeleteListPointF;
            }));
            foreach (ListPointF listPointF in listpointf)
            {

                string DrawType = listPointF.Type.ToUpper();
                switch (DrawType)
                {
                    case "POINT":
                        Invoke(new Action(delegate//canvas.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            userControl_Canvas.Run_cursors(listPointF.StartPoint1);
                            userControl_Canvas.SetRunPath(listPointF.ID, System.Windows.Media.Colors.Green);
                        }));
                        break;
                    case "LINE":
                        Invoke(new Action(delegate//canvas.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            userControl_Canvas.Run_cursors(listPointF.StartPoint1);
                            userControl_Canvas.SetRunPath(listPointF.ID, System.Windows.Media.Colors.Green);

                            Zb_Run1_SeleteStartPoint("", "", "0");
                        }));
                        break;
                    case "SPLINE":
                        Invoke(new Action(delegate//canvas.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            userControl_Canvas.Run_cursors(listPointF.Polylines[0]);
                            userControl_Canvas.SetRunPath(listPointF.ID, System.Windows.Media.Colors.Green);
                        }));
                        break;
                    case "POLYLINES":
                        Invoke(new Action(delegate//canvas.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            userControl_Canvas.Run_cursors(listPointF.Polylines[0]);
                            userControl_Canvas.SetRunPath(listPointF.ID, System.Windows.Media.Colors.Green);
                        }));
                        break;
                    case "LWPOLYLINE":
                        Invoke(new Action(delegate//canvas.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            userControl_Canvas.Run_cursors(listPointF.Polylines[0]);
                            userControl_Canvas.SetRunPath(listPointF.ID, System.Windows.Media.Colors.Green);
                        }));
                        break;
                    case "CIRCLE":
                        Invoke(new Action(delegate//canvas.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            userControl_Canvas.Run_cursors(listPointF.CentrePoint);
                            userControl_Canvas.SetRunPath(listPointF.ID, System.Windows.Media.Colors.Green);
                        }));
                        break;
                    case "ELLIPSE":
                        //DrawEllipses(PointF Center, float Radius, int StartAngle, int EndAngle);
                        break;
                    case "ARC":
                        Invoke(new Action(delegate//canvas.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            userControl_Canvas.Run_cursors(listPointF.StartPoint1);
                            userControl_Canvas.SetRunPath(listPointF.ID, System.Windows.Media.Colors.Green);
                        }));
                        break;
                    case "MTEXT":
                        //drawText(PointF pointF, string text)
                        break;
                    case "HATCH":
                        break;
                    case "3DFACE":
                        break;
                    case "INSERT":
                        break;
                    case "SOLID":
                        break;
                    case "TEXT":
                        //drawText(PointF pointF, string text)
                        break;
                    case "TRACE":
                        break;
                    case "FXLINE":
                        break;
                    case "FVIEWPORT":


                        break;
                }
            }
        }

        public void ClearSelete()
        {
            userControl_Canvas.SeleteListPointF.Clear();
        }

        public void ResetCanvas()
        {
            ClearSelete();
            Invoke(new Action(delegate//canvas.Dispatcher.BeginInvoke(new Action(() =>
            {
                userControl_Canvas.ResetRunPath();
            }));
        }

        private void userControl_Canvas_MouseClick(object sender, MouseEventArgs e)
        {
            Thread t = new Thread(() =>
            {
                RunSelete();

                ResetCanvas();
            });
            t.SetApartmentState(ApartmentState.STA);
            t.IsBackground = true;
            t.Start();
            
        }
       

        List<ListPointF> ListPointF;

        void GetData_TXT_To_Canvas(string path)
        {
            ListPointF = new List<ListPointF>();
            string msg = "";
            Read_File(path, out msg);
            string[] aa = msg.Split(';');
            for (int i = 0; i < aa.Length - 1; i++)
            {
                //取开始点
                float Start_X = 0;
                float Start_Y = 0;
                //取结束点
                float Stop_X = 0;
                float Stop_Y = 0;

                PointF startP1 = new PointF();
                PointF stopP1 = new PointF();

                if (i == 0 && aa.Length - 1 >= 1)
                {
                    string start = aa[i];
                    string[] startab = start.Split(',');
                    int startse = Convert.ToInt32(startab[5]);

                    string ends = aa[i + 1];
                    string[] endsab = ends.Split(',');
                    int endsse = Convert.ToInt32(endsab[5]);

                    Start_X = float.Parse(startab[1]);
                    Start_Y = float.Parse(startab[2]);

                    Stop_X = float.Parse(endsab[1]);
                    Stop_Y = float.Parse(endsab[2]);

                    startP1 = new PointF(Start_X, Start_Y);
                    stopP1 = new PointF(Stop_X, Stop_Y);
                    AddTestPoint(startP1, stopP1);

                }
                if (i > 1 && i <= aa.Length - 1)
                {
                    if (!IsOdd2(i))
                    {
                        string start = aa[i];
                        string[] startab = start.Split(',');
                        int startse = Convert.ToInt32(startab[5]);

                        string ends = aa[i + 1];
                        string[] endsab = ends.Split(',');
                        int endsse = Convert.ToInt32(endsab[5]);

                        Start_X = float.Parse(startab[1]);
                        Start_Y = float.Parse(startab[2]);

                        Stop_X = float.Parse(endsab[1]);
                        Stop_Y = float.Parse(endsab[2]);


                        startP1 = new PointF(Start_X, Start_Y);
                        stopP1 = new PointF(Stop_X, Stop_Y);
                        AddTestPoint(startP1, stopP1);
                    }
                }

                var value = userControl_Canvas.RunListPointF;

            }

            foreach (ListPointF NewlistPointF in ListPointF)
            {
                userControl_Canvas.DrawLine(NewlistPointF.StartPoint1, NewlistPointF.EndPoint1);
            }
            userControl_Canvas.AddRunPath();
        }
        /// <summary>
        /// 读取文件
        /// </summary>
        public bool Read_File(string path, out string msg)
        {
            bool tag = true;
            try
            {
                using (FileStream fs = new FileStream(@path, FileMode.OpenOrCreate, FileAccess.Read))
                {
                    byte[] buff = new byte[1024 * 1024 * 5];
                    int x = fs.Read(buff, 0, buff.Length);
                    msg = Encoding.Default.GetString(buff, 0, x);
                    return tag;
                }
            }
            catch (Exception x)
            {

                msg = "";
                tag = false;
                throw x;
            }
        }


        public void AddTestPoint(PointF startP1, PointF stopP1)
        {
            ListPointF NewlistPointF = new ListPointF();
            NewlistPointF.StartPoint1 = startP1;
            NewlistPointF.EndPoint1 = stopP1;
            ListPointF.Add(NewlistPointF);
        }

        //一般普通版:
        private bool IsOdd(int num)
        {
            return (num % 2) == 1;
        }

       

        //通过判断取余


        //现在升级版:
        private bool IsOdd2(int num)
        {
            return (num & 1) == 1;
        }

        #endregion

        private void uiButton2_Click_1(object sender, EventArgs e)
        {
            //Goog.Auto_GoHomeTest(1);
            //Goog.btn_homeStart(1);

            Goog.btn_homeStart(2);
        }
    }
}


//  铣刀寿命   顶升上下    单切  老化