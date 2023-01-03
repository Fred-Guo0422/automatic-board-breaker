using _2021LY.Forms;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2021LY
{
    class Trajectory
    {
        static int xishu = 850;
        static int right_xishu = 60000;
        PictureBox Sport_Traje;
        Graphics g;
        ListView listView;
        UIDataGridView GridView;
        Pen p;
        Bitmap bmp;
        public Trajectory(PictureBox pb, Graphics gh, ListView lv, Pen pen, Bitmap bip)
        {
            g = gh;
            p = pen;
            bmp = bip;
            listView = lv;
            Sport_Traje = pb;
        }

        public Trajectory(PictureBox pb, Graphics gh, UIDataGridView gv, Pen pen, Bitmap bip)
        {
            g = gh;
            p = pen;
            bmp = bip;
            GridView = gv;
            Sport_Traje = pb;
        }

        public void Draw_Path()
        {
            Clear_PictureBox();

            int count = listView.Items.Count;
            for (int i = 0; i < count; i++)
            {

                int X_Xuhao = Convert.ToInt32(listView.Items[i].SubItems[0].Text);

                int type = Convert.ToInt32(listView.Items[i].SubItems[6].Text);//轨迹类型

                int X_Start = CommUtil.Cir_To_Pulse(listView.Items[i].SubItems[2].Text);
                int Y_Start = CommUtil.Cir_To_Pulse(listView.Items[i].SubItems[3].Text);

                //处理图像与画布尺寸比例系数

                Point Start = new Point((X_Start + right_xishu) / xishu, Y_Start / xishu);
                //Point Start = new Point(X_Start + 100, Y_Start + 100);

                if (type == CommParam.Path_Circle)//如果是圆
                {
                    // int R = CommUtil.Cir_To_Pulse(listView.Items[i].SubItems[5].Text);//Y2为半径

                    double R = Convert.ToDouble(listView.Items[i].SubItems[5].Text);

                    DrawEllipse(Start, (int)R * 10, (int)R * 10);
                }
                else if (type == CommParam.Path_Acr && count >= i + 2)//如果当前是圆弧，而且后面还有两条数据  //xinzeng
                {

                    int type2 = Convert.ToInt32(listView.Items[i + 1].SubItems[6].Text);//轨迹类型
                    int type3 = Convert.ToInt32(listView.Items[i + 2].SubItems[6].Text);//轨迹类型

                    if (type == type2 && type2 == type3)//3条数据都是圆弧的情况下，才可以画圆弧
                    {
                        string x1 = listView.Items[i].SubItems[2].Text;
                        string y1 = listView.Items[i].SubItems[3].Text;
                        string x2 = listView.Items[i + 1].SubItems[2].Text;
                        string y2 = listView.Items[i + 1].SubItems[3].Text;
                        string x3 = listView.Items[i + 2].SubItems[2].Text;
                        string y3 = listView.Items[i + 2].SubItems[3].Text;
                        i += 2;
                        // DrawArcFromThreePoint(120, 140, 200, 110, 201, 111);
                        DrawArcFromThreePoint(Convert.ToDouble(x1), Convert.ToDouble(y1), Convert.ToDouble(x2), Convert.ToDouble(y2), Convert.ToDouble(x3), Convert.ToDouble(y3));
                    }
                }
                else
                {
                    DrawLaber(Start, X_Xuhao);
                    if (i > 1)
                    {
                        int X_End = CommUtil.Cir_To_Pulse(listView.Items[i + 1].SubItems[2].Text);
                        int Y_End = CommUtil.Cir_To_Pulse(listView.Items[i + 1].SubItems[3].Text);
                        Point End = new Point((X_End + right_xishu) / xishu, Y_End / xishu);
                        DrawLine(Start, End);

                        if (i == count - 2)
                        {
                            DrawLaber(End, X_Xuhao + 1);//画最后一个点的laber，所以序号+1
                        }
                    }
                }

                if (i + 2 == count)//首尾两个点去除
                {
                    break;
                }

            }
            Sport_Traje.Image = bmp;
        }


        public void testCad(int X_Start, int Y_Start, int R)
        {

            Point Start = new Point(((X_Start + right_xishu) / 550), (Y_Start * 2) / 1150);
            DrawEllipse(Start, R * 4, R * 4);
            Sport_Traje.Image = bmp;
        }

        public void Draw_Path_Grid()
        {
            /** 调整方法
            1、先调整x_pianyi、y_pianyi2个参数，让图从画幅左上角开始显示
            2、再调整x_xishu、y_xishu ，让图饱满填满画幅，不溢出画幅
            */
            //调整轨迹图在画幅中的位置
            int x_pianyi = -100;//越小越靠左
            int y_pianyi = -100;//越小越靠上

            
            //调整轨迹图饱满填满画幅
            int x_xishu = 120;//越小越往右拉伸
            int y_xishu = 450;//越小越往下拉伸

            Clear_PictureBox();

            int count = GridView.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                int X_Xuhao = Convert.ToInt32(GridView.Rows[i].Cells[0].Value);
                int type = Convert.ToInt32(GridView.Rows[i].Cells[6].Value);//轨迹类型 1 起点  2直线  3 终点
                int X_P = CommUtil.Cir_To_Pulse(GridView.Rows[i].Cells[2].Value.ToString());
                int Y_P = CommUtil.Cir_To_Pulse(GridView.Rows[i].Cells[3].Value.ToString());
                if (type == 1)
                {
                    Point S = new Point((X_P / x_xishu) + x_pianyi, (Y_P / y_xishu) + y_pianyi);
                    //Draw_Cir(S, 1);
                    DrawLaber(S, X_Xuhao, 1);//
                }
                else if (type == 3)
                {
                    int X_P1 = CommUtil.Cir_To_Pulse(GridView.Rows[i - 1].Cells[2].Value.ToString());
                    int Y_P1 = CommUtil.Cir_To_Pulse(GridView.Rows[i - 1].Cells[3].Value.ToString());
                    Point E = new Point((X_P1 / x_xishu) + x_pianyi, (Y_P1 / y_xishu) + y_pianyi);
                    Point S = new Point((X_P / x_xishu) + x_pianyi, (Y_P / y_xishu) + y_pianyi);
                    DrawLine(E, S);
                    // Draw_Cir(S, 0);
                    DrawLaber(S, X_Xuhao, 0);//
                    if (i < count - 1)
                    {
                        int X_P2 = CommUtil.Cir_To_Pulse(GridView.Rows[i + 1].Cells[2].Value.ToString());
                        int Y_P2 = CommUtil.Cir_To_Pulse(GridView.Rows[i + 1].Cells[3].Value.ToString());
                        Point E2 = new Point((X_P2 / x_xishu) + x_pianyi, (Y_P2 / y_xishu) + y_pianyi);
                        DrawPointLint(S, E2);
                    }
                }
            }

            DrawString(10,10);

            Sport_Traje.Image = bmp;
        }


        void DrawString(int x,int y)
        {
            string con = CommParam.open_file_path;
            g.FillRectangle(new SolidBrush(Color.White), x, y, con.Length * 6, 14);
            g.DrawString(con, new Font("Arial", 8), new SolidBrush(Color.Black), x, y);
        }

        void DrawPointLint(Point sta, Point en)
        {
            Pen pen1 = new Pen(Color.Green, 1);
            pen1.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
            pen1.DashPattern = new float[] { 5, 5 };
            g.DrawLine(pen1, sta, en);
        }

        /// <summary>
        /// 3点画圆弧
        /// </summary>

        void DrawArcFromThreePoint(double x1, double y1, double x2, double y2, double x3, double y3)
        {
            double a = x1 - x2;
            double b = y1 - y2;
            double c = x1 - x3;
            double d = y1 - y3;
            double e = ((x1 * x1 - x2 * x2) + (y1 * y1 - y2 * y2)) / 2.0;
            double f = ((x1 * x1 - x3 * x3) + (y1 * y1 - y3 * y3)) / 2.0;

            double det = b * c - a * d;
            if (Math.Abs(det) > 0.001)
            {
                //x0,y0为计算得到的原点
                double x0 = -(d * e - b * f) / det;
                double y0 = -(a * f - c * e) / det;
                // SolidBrush OriginBrush = new SolidBrush(Color.Blue);
                // g.FillEllipse(OriginBrush, (int)(x0 - 3), (int)(y0 - 3), 6, 6);
                double radius = Math.Sqrt((x1 - x0) * (x1 - x0) + (y1 - y0) * (y1 - y0));
                double angle1, angle2, angle3, sinValue1, cosValue1, sinValue2, cosValue2, sinValue3, cosValue3;
                sinValue1 = (y1 - y0) / radius;
                cosValue1 = (x1 - x0) / radius;
                if (cosValue1 >= 0.99999)
                {
                    cosValue1 = 0.99999;
                }
                if (cosValue1 <= -0.99999)
                {
                    cosValue1 = -0.99999;
                }
                angle1 = Math.Acos(cosValue1);
                angle1 = angle1 / 3.14 * 180;
                if (sinValue1 < -0.05)
                {
                    angle1 = 360 - angle1;
                }
                sinValue2 = (y2 - y0) / radius;
                cosValue2 = (x2 - x0) / radius;
                if (cosValue2 >= 0.99999) cosValue2 = 0.99999;
                if (cosValue2 <= -0.99999) cosValue2 = -0.99999;
                angle2 = Math.Acos(cosValue2);
                angle2 = angle2 / 3.14 * 180;
                if (sinValue2 < -0.05) angle2 = 360 - angle2;
                sinValue3 = (y3 - y0) / radius;
                cosValue3 = (x3 - x0) / radius;
                if (cosValue3 >= 0.99999) cosValue3 = 0.99999;
                if (cosValue3 <= -0.99999) cosValue3 = -0.99999;
                angle3 = Math.Acos(cosValue3);
                angle3 = angle3 / 3.14 * 180;
                if (sinValue3 < -0.05) angle3 = 360 - angle3;
                bool PosDown = false;
                double Delta13;
                if (angle1 < angle3)
                {
                    Delta13 = angle3 - angle1;
                }
                else Delta13 = angle3 - angle1 + 360;
                double Delta12;

                if (angle1 < angle2)
                {
                    Delta12 = angle2 - angle1;
                }
                else
                {
                    Delta12 = angle2 - angle1 + 360;
                }
                if (Delta13 > Delta12)
                {
                    PosDown = true;
                }
                else
                {
                    PosDown = false;
                }
                if (PosDown)
                {
                    if (angle3 > angle1)
                    {
                        g.DrawArc(p, (int)(x0 - radius), (int)(y0 - radius), (int)(2 * radius), (int)(2 * radius), (int)(angle1), (int)(angle3 - angle1));
                    }
                    else
                    {
                        g.DrawArc(p, (int)(x0 - radius), (int)(y0 - radius), (int)(2 * radius), (int)(2 * radius), (int)(angle1), (int)(angle3 - angle1 + 360));
                    }
                }
                else
                {
                    if (angle1 > angle3)
                    {
                        g.DrawArc(p, (int)(x0 - radius), (int)(y0 - radius), (int)(2 * radius), (int)(2 * radius), (int)(angle3), (int)(angle1 - angle3));
                    }
                    else
                    {
                        g.DrawArc(p, (int)(x0 - radius), (int)(y0 - radius), (int)(2 * radius), (int)(2 * radius), (int)(angle3), (int)(angle1 - angle3 + 360));
                    }
                }
            }
        }
        /// <summary>
        /// 得到整体缩放比例    使整体在300-300范围之内
        /// </summary>
        void Get_Coeff(int x, int y)
        {
            int x3 = x + "".Length;

        }

        //放大缩小重新画图
        public void Draw_Path(double step)
        {
            Clear_PictureBox();

            int count = listView.Items.Count;
            for (int i = 0; i < count; i++)
            {

                int X_Xuhao = Convert.ToInt32(listView.Items[i].SubItems[0].Text);

                int type = Convert.ToInt32(listView.Items[i].SubItems[6].Text);//轨迹类型

                string x1 = listView.Items[i].SubItems[2].Text;
                string y1 = listView.Items[i].SubItems[2].Text;



                int X_Start = Convert.ToInt32(listView.Items[i].SubItems[2].Text);
                int Y_Start = Convert.ToInt32(listView.Items[i].SubItems[3].Text);

                //处理图像与画布尺寸比例系数
                double xx = (X_Start + right_xishu) * step;
                double yy = Y_Start / xishu * step;

                Point Start = new Point((int)xx, (int)yy);
                //Point Start = new Point(X_Start + 100, Y_Start + 100);


                if (type == CommParam.Path_Circle)//如果是圆
                {
                    // int R = CommUtil.Cir_To_Pulse(listView.Items[i].SubItems[5].Text);//Y2为半径

                    double R = Convert.ToDouble(listView.Items[i].SubItems[5].Text);

                    DrawEllipse(Start, (int)R * 10, (int)R * 10);
                }
                else
                {
                    DrawLaber(Start, X_Xuhao);
                    int X_End = CommUtil.Cir_To_Pulse(listView.Items[i + 1].SubItems[2].Text);
                    int Y_End = CommUtil.Cir_To_Pulse(listView.Items[i + 1].SubItems[3].Text);
                    Point End = new Point((X_End + right_xishu) / xishu, Y_End / xishu);
                    DrawLine(Start, End);
                    if (i == count - 2)
                    {
                        DrawLaber(End, X_Xuhao + 1);//画最后一个点的laber，所以序号+1
                    }
                }

                if (i + 2 == count)//首尾两个点去除
                {
                    break;
                }

            }
            Sport_Traje.Image = bmp;
        }

        /// <summary>
        /// 画直线+		 
        /// </summary>

        public void DrawLine(Point Start, Point End)
        {
            try
            {

                g.DrawLine(p, Start, End);
                // Console.WriteLine("起点X{0},起点Y{1},终点X{2},终点Y{3},", Start.X, Start.Y, End.X, End.Y);

            }
            catch (Exception x)
            {
                LogHelper.WriteErrorLog("Trajectory.DrawLine 画直线 操作异常 ", x);
                //  Logger.Recod_Log_File("Trajectory.DrawLine 画直线操作异常" + x.Message + "\r\n x.StackTrace" + x.StackTrace);
                throw x;
            }
        }

        public void DrawLaber(Point End, int X_Xuhao)
        {
            try
            {
                Label l = new Label();
                l.Text = "" + X_Xuhao;
                //l.Name = "12";
                l.Width = 10;
                l.Height = 10;
                l.Location = End;
                l.BackColor = Color.Green;
                // l.AutoSize = true;
                l.DoubleClick += L_DoubleClick;
                Sport_Traje.Controls.Add(l);


            }
            catch (Exception x)
            {
                LogHelper.WriteErrorLog(" Trajectory.DrawLine 画直线操作异常 ", x);
                //  Logger.Recod_Log_File("Trajectory.DrawLine 画直线操作异常" + x.Message + "\r\n x.StackTrace" + x.StackTrace);
                throw x;
            }
        }

        public void DrawLaber(Point End, int X_Xuhao, int type)
        {
            try
            {
                Label l = new Label();
                l.Text = "" + X_Xuhao;
                l.Width = 5;
                l.Height = 5;
                l.Location = End;
                if (type == 0)
                {
                   // l.Location = new Point(End.X, End.Y);
                    l.BackColor = Color.Red;
                }
                else
                {
                  //  l.Location = new Point(End.X, End.Y);
                    l.BackColor = Color.Green;
                }
                l.DoubleClick += L_DoubleClick;
                Sport_Traje.Controls.Add(l);


            }
            catch (Exception x)
            {
                LogHelper.WriteErrorLog(" Trajectory.DrawLine 画直线操作异常 ", x);
                //  Logger.Recod_Log_File("Trajectory.DrawLine 画直线操作异常" + x.Message + "\r\n x.StackTrace" + x.StackTrace);
                throw x;
            }
        }

        private void L_DoubleClick(object sender, EventArgs e)
        {
            if (Goog.gpi_status(6))
            {
                DesignForm.getForm().ShowErrorDialog("请切换到手动状态");
                return;
            }
            Label df = (Label)sender;
            int index = Convert.ToInt32(df.Text);

            DataGridViewRow r = DesignForm.getForm().Point_DataGrid.Rows[index];
            if (!DesignForm.getForm().ShowAskDialog("确定运行到第"+ index + "点位吗？")) return;

            string xx = r.Cells[2].Value.ToString();
            string yy = r.Cells[3].Value.ToString();
            //1008
            int x;
            int y;
            if (DesignForm.getForm().camera_knife_CheckBox.Checked)//相机位置
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

        private void L_Click(object sender, EventArgs e)
        {
            Label df = (Label)sender;
            Console.WriteLine(df.Name);
            Console.WriteLine(df.Text);
        }

        /// <summary>
        /// 画圆、椭圆    如果是圆  width  height  即为直径
        /// </summary> 
        /// <param name="point">圆心</param>
        /// <param name="width">椭圆宽</param>
        /// <param name="height">椭圆高</param>
        public void DrawEllipse(Point point, int width, int height)
        {
            try
            {
                Rectangle rg = new Rectangle(point.X - width, point.Y - width, width, height);
                // Rectangle rg = new Rectangle(200,100, 50, 50);
                g.DrawEllipse(p, rg);
                //g.FillEllipse(Brushes.Red, rg);

            }
            catch (Exception x)
            {
                LogHelper.WriteErrorLog("Trajectory.DrawLine 画直线 操作异常 ", x);
                //  Logger.Recod_Log_File("Trajectory.DrawLine 画直线操作异常" + x.Message + "\r\n x.StackTrace" + x.StackTrace);
                throw x;
            }
        }

        /// <summary>
        /// 清空轨迹
        /// </summary>

        public void Clear_PictureBox()
        {
            g.Clear(Color.Gray);
            Sport_Traje.Controls.Clear();
            Sport_Traje.Image = null;
        }

        /// <summary>
        /// 检查数据中圆弧的数据。 3点组成圆弧     正确返回true   错误返回false
        /// </summary>
        public static bool Check_ViewList(ListView lv)
        {

            for (int i = 0; i < lv.Items.Count; i++)
            {
                int type = Convert.ToInt32(lv.Items[i].SubItems[6].Text);
                if (type == CommParam.Path_Acr)//如果是圆弧，则检查接下来的3条数据是否都是圆弧
                {
                    int type2 = Convert.ToInt32(lv.Items[i + 1].SubItems[6].Text);
                    int type3 = Convert.ToInt32(lv.Items[i + 2].SubItems[6].Text);
                    if (type != type2 || type != type3)
                    {
                        return false;
                    }
                }
            }

            return true;
        }



        void Draw_Cir(Point Start, int x)
        {
            Rectangle star = new Rectangle(Start.X, Start.Y - 7, 5, 5);
            Rectangle end = new Rectangle(Start.X, Start.Y, 5, 5);
            if (x == 0)//终点
            {
                g.FillEllipse(Brushes.Red, end);
                g.DrawEllipse(p, end);

            }
            else
            {
                g.FillEllipse(Brushes.Green, star);//填充圆
                g.DrawEllipse(new Pen(Brushes.Green), star);
            }
        }

    }
}
