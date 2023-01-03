using Sunny.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
1、***启动每个区域，验证是否有回原点
2、***每次校准，弹出提示信息  （列表没有数据和校验第几个点）
3、端口参数保存、读取
4、每个螺丝的序号可以更改
5、****运行时三色灯，以及系统状态的正常显示
*/

namespace _2021LY.Forms
{
    public partial class FifthGeneratiaonForm : UITitlePage
    {
        public FifthGeneratiaonForm()
        {
            InitializeComponent();
            this.Text = "5G螺丝机";
            Ini_Moto();
            Ini_CAD_PointFile();
        }

        private static FifthGeneratiaonForm fifthGeneratiaonForm;

        public static FifthGeneratiaonForm getForm()
        {
            if (fifthGeneratiaonForm == null)
            {
                fifthGeneratiaonForm = new FifthGeneratiaonForm();
            }
            return fifthGeneratiaonForm;
        }

        //public Thread t;
        private void PagePanel_Load(object sender, EventArgs e)
        {
            uiA_Com.SelectedIndex = 2;
            uiB_Com.SelectedIndex = 1;
            uiBaudRate.SelectedIndex = 10;
            uiparity.SelectedIndex = 2;
            uiStopBits.SelectedIndex = 0;
            uidatabites.SelectedIndex = 1;
            uimoto.SelectedIndex = 0;

            Thread t = new Thread(() =>
            {
                Get_Status();
            });
            t.IsBackground = true;
            t.Start();

        }
        Mrtu4 mr_a;
        Mrtu4 mr_b;
        //Mrtu a;//A
        //Mrtu b;//B
        byte _stand = 1;
        int time = 5;
        //初始化电批
        private void Ini_Moto()
        {
            mr_a = new Mrtu4("COM3", 115200, StopBits.One, Parity.None, 8);
            mr_a._wuint16(61506, _stand, 0x3BBB);  // 切换
            Thread.Sleep(time);
            mr_a._wuint16(4060, _stand, 0xC8);
            Thread.Sleep(time);
            mr_a._wuint16(4061, _stand, 0x64);
            Thread.Sleep(time);
            mr_a._wuint16(4065, _stand, 0);
            Thread.Sleep(time);
            mr_a._wuint16(4066, _stand, 0);
            Thread.Sleep(time);
            mr_a._wuint16(4070, _stand, 0);
            Thread.Sleep(time);
            mr_a._wuint16(4071, _stand, 0);
            Thread.Sleep(time);
            mr_a._wuint16(4110, _stand, 0xC8);//拧松圈数
            Thread.Sleep(time);
            mr_a._wuint16(4111, _stand, 0x64);//拧松速度

            mr_b = new Mrtu4("COM2", 115200, StopBits.One, Parity.None, 8);
            mr_b._wuint16(61506, _stand, 0x3BBB);  // 切换
            Thread.Sleep(time);
            mr_b._wuint16(4060, _stand, 0xC8);
            Thread.Sleep(time);
            mr_b._wuint16(4061, _stand, 0x64);
            Thread.Sleep(time);
            mr_b._wuint16(4110, _stand, 0xC8);
            Thread.Sleep(time);
            mr_b._wuint16(4111, _stand, 0x64);
            //-------------------

            //a = new Mrtu("COM3", 115200, StopBits.One, Parity.None, 8);
            //a._wuint16(61506, _stand, 0x3BBB);  // 切换

            //b = new Mrtu("COM2", 115200, StopBits.One, Parity.None, 8);
            //b._wuint16(61506, _stand, 0x3BBB);  // 切换

            ///*  0x32 半圈    0x64  1圈  0xC8  2圈 */
            ////清楚数据
            //Task_MotoA(4060, 0xC8);//紧圈
            //Task_MotoA(4061, 0x64);//紧速
            //Task_MotoA(4065, 0);
            //Task_MotoA(4066, 0);
            //Task_MotoA(4070, 0);
            //Task_MotoA(4071, 0);

            //Task_MotoA(4110, 0xC8);
            //Task_MotoA(4111, 0x64);
            ////////////////////////
            //Task_MotoB(4060, 0xC8);
            //Task_MotoB(4061, 0x64);
            //Task_MotoB(4110, 0xC8);
            //Task_MotoB(4111, 0x64);

            //a._ruint16(4110, _stand, 1);//拧紧圈数
            //b._ruint16(4110, _stand, 1);//拧紧圈数

        }




        bool moto_status = false;

        public void Get_Status()
        {

            while (true)
            {
                // Console.WriteLine("未扫描");
                while (CommParam.Mrtu_Send)
                {
                    Console.WriteLine("开始扫描");
                    moto_status = false;
                    Thread.Sleep(10);
                    ushort[] data_a = mr_a._ruint16(60642, _stand, 1);
                    Thread.Sleep(10);
                    ushort[] data_b = mr_b._ruint16(60642, _stand, 1);

                    //for (int i = 0; i < data_a.Length; i++)
                    //{
                    //    Console.WriteLine(data_a[i]);
                    //}

                    ushort status_a = 0;
                    ushort status_b = 0;

                    if (data_a.Length > 0)
                    {
                        status_a = data_a[0];
                    }

                    if (data_b.Length > 0)
                    {
                        status_b = data_b[0];
                    }

                    if (status_a > 2 || status_b > 2)
                    {
                        Console.WriteLine("电批异常");
                        uimoto_status.OnColor = Color.Red;
                        uimoto_status.State = UILightState.On;
                        CommParam.Mrtu_Send = false;
                        break;
                    }
                    else if (status_a == 1 || status_b == 1)
                    {
                        uimoto_status.State = UILightState.On;
                        Console.WriteLine("{0}运行中{1}", status_a, status_b);
                    }
                    else if (status_a == 2 && status_b == 2)
                    {
                        Console.WriteLine("{0}运行完成{1}", status_a, status_b);
                        uimoto_status.State = UILightState.Off;
                        moto_status = true;
                        CommParam.Mrtu_Send = false;
                        break;
                    }
                    else
                    {
                        uimoto_status.State = UILightState.Off;
                        CommParam.Mrtu_Send = false;
                        Console.WriteLine("跳出");
                        break;
                    }

                }
            }
        }
        public void CloseCom()
        {
            if (mr_a != null)
            {
                mr_a.closeCom();
            }
            if (mr_b != null)
            {
                mr_b.closeCom();
            }
        }


        private void Forward_Button_MouseDown(object sender, MouseEventArgs e)
        {
            int moto = uimoto.SelectedIndex;
            if (moto == 0)
            {
                mr_a._wuint16(61500, _stand, 0x2AAA);  // 开始拧紧
            }
            else
            {
                mr_b._wuint16(61500, _stand, 0x2AAA);  // 开始拧紧
            }
        }

        private void Forward_Button_MouseUp(object sender, MouseEventArgs e)
        {
            int moto = uimoto.SelectedIndex;
            if (moto == 0)
            {
                mr_a._wuint16(61500, _stand, 0x2BBB);  // 停止拧紧
            }
            else
            {
                mr_b._wuint16(61500, _stand, 0x2BBB);  // 停止拧紧
            }
        }

        private void Reversal_Button_MouseDown(object sender, MouseEventArgs e)
        {
            int moto = uimoto.SelectedIndex;
            if (moto == 0)
            {
                mr_a._wuint16(61500, _stand, 0x2CCC);  // 开始拧松
            }
            else
            {
                mr_b._wuint16(61500, _stand, 0x2CCC);  // 开始拧松
            }
        }

        private void Reversal_Button_MouseUp(object sender, MouseEventArgs e)
        {
            int moto = uimoto.SelectedIndex;
            if (moto == 0)
            {
                mr_a._wuint16(61500, _stand, 0x2DDD);  // 停止拧松
            }
            else
            {
                mr_b._wuint16(61500, _stand, 0x2DDD);  // 停止拧松
            }
        }


        //public void Task_MotoA(int addr, ushort data)
        //{
        //    Thread.Sleep(time);
        //    a._wuint16(addr, _stand, data);

        //}

        //public void Task_MotoB(int addr, ushort data)
        //{
        //    Thread.Sleep(time);
        //    b._wuint16(addr, _stand, data);
        //}

        private void Write_Button_Click(object sender, EventArgs e)
        {

            if (ShowAskDialog("确认信息提示框"))
            {
                ShowSuccessTip("您点击了确定按钮");
                mr_a._wuint16(4060, _stand, ushort.Parse(ajqTextBox.Text + "00"));//拧紧圈数
                mr_a._wuint16(4061, _stand, ushort.Parse(ajsTextBox.Text));//拧紧速度
                mr_a._wuint16(4110, _stand, ushort.Parse(asqTextBox.Text + "00"));//拧松行程
                mr_a._wuint16(4111, _stand, ushort.Parse(assTextBox.Text));//拧松速度

                mr_b._wuint16(4060, _stand, ushort.Parse(bjqTextBox.Text + "00"));//拧紧圈数
                mr_b._wuint16(4061, _stand, ushort.Parse(bjsTextBox.Text));//拧紧速度
                mr_b._wuint16(4110, _stand, ushort.Parse(bsqTextBox.Text + "00"));//拧松行程
                mr_b._wuint16(4111, _stand, ushort.Parse(bssTextBox.Text));//拧松速度
            }
            else
            {
                ShowErrorTip("您点击了取消按钮");
            }

        }

        private void Read_Button_Click(object sender, EventArgs e)
        {


            ushort[] a1 = mr_a._ruint16(4060, _stand, 1);//拧紧圈数
            ajqTextBox.Text = (a1[0] / 100).ToString();
            Thread.Sleep(time);
            ushort[] a2 = mr_a._ruint16(4061, _stand, 1);//拧紧速度
            ajsTextBox.Text = a2[0].ToString();
            Thread.Sleep(time);
            ushort[] a3 = mr_a._ruint16(4110, _stand, 1);//拧松行程
            asqTextBox.Text = (a3[0] / 100).ToString();
            Thread.Sleep(time);
            ushort[] a4 = mr_a._ruint16(4111, _stand, 1);//拧松速度
            assTextBox.Text = a4[0].ToString();
            Thread.Sleep(time);

            ushort[] b1 = mr_b._ruint16(4060, _stand, 1);//拧紧圈数
            bjqTextBox.Text = (b1[0] / 100).ToString();
            Thread.Sleep(time);
            ushort[] b2 = mr_b._ruint16(4061, _stand, 1);//拧紧速度
            bjsTextBox.Text = b2[0].ToString();
            Thread.Sleep(time);
            ushort[] b3 = mr_b._ruint16(4110, _stand, 1);//拧松行程
            bsqTextBox.Text = (b3[0] / 100).ToString();
            Thread.Sleep(time);
            ushort[] b4 = mr_b._ruint16(4111, _stand, 1);//拧松速度
            bssTextBox.Text = b4[0].ToString();

        }

        private void Left_Top_Button_Click(object sender, EventArgs e)
        {


            if (!CommParam.Is_Gohome)
            {
                UIMessageDialog.ShowMessageDialog("请先执行回原点！", UILocalize.InfoTitle, false, Style);
                return;
            }


            Thread a = new Thread(() =>
            {
                Go(CommParam.FifthGe_leftTop);
            });
            a.IsBackground = true;
            a.Start();
        }

        private void Right_Top_Button_Click(object sender, EventArgs e)
        {

            if (!CommParam.Is_Gohome)
            {
                UIMessageDialog.ShowMessageDialog("请先执行回原点！", UILocalize.InfoTitle, false, Style);
                return;
            }
            Thread a = new Thread(() =>
            {
                Go(CommParam.FifthGe_rightTop);
            });
            a.IsBackground = true;
            a.Start();
        }

        private void Left_Bottom_Button_Click(object sender, EventArgs e)
        {
            if (!CommParam.Is_Gohome)
            {
                UIMessageDialog.ShowMessageDialog("请先执行回原点！", UILocalize.InfoTitle, false, Style);
                return;
            }
            Thread a = new Thread(() =>
            {
                Go(CommParam.FifthGe_leftbot);
            });
            a.IsBackground = true;
            a.Start();
        }

        private void Right_Bottom_Button_Click(object sender, EventArgs e)
        {
            if (!CommParam.Is_Gohome)
            {
                UIMessageDialog.ShowMessageDialog("请先执行回原点！", UILocalize.InfoTitle, false, Style);
                return;
            }
            Thread a = new Thread(() =>
            {
                Go(CommParam.FifthGe_rightBot);
            });
            a.IsBackground = true;
            a.Start();
        }


        private List<CommParam.Point_Data> Get_data_Txt(string path)
        {
            string msg;
            FileUtil f = new FileUtil();
            f.Read_File(path, out msg);
            string[] aa = msg.Split(';');
            List<CommParam.Point_Data> point_List = new List<CommParam.Point_Data>();
            for (int i = 0; i < aa.Length - 1; i++)
            {
                CommParam.Point_Data pd = new CommParam.Point_Data();
                string c = aa[i];
                string[] ab = c.Split(',');

                pd.Point_X_Pos = ab[1];
                pd.Point_Y_Pos = ab[2];
                pd.Point_Z_Pos = ab[3];
                point_List.Add(pd);

            }
            return point_List;
        }

        void Go(string path)
        {
            List<CommParam.Point_Data> pd = Get_data_Txt(path);
            Run_Point(pd);
        }


        void Run_Point(List<CommParam.Point_Data> pd)
        {
            int length = pd.Count;
            int cur = 0;
            CommParam.Is_Run = 1;
            foreach (CommParam.Point_Data cpd in pd)
            {
                cur++;
                int x = CommUtil.Cir_To_Pulse(cpd.Point_X_Pos);
                int y = CommUtil.Cir_To_Pulse(cpd.Point_Y_Pos);
                int z = CommUtil.Cir_To_Pulse(cpd.Point_Z_Pos);

                //  Console.WriteLine("Point_X_Pos = {0}  ,Point_Y_Pos = {1}  ,Point_Z_Pos = {2}  ", x, y, z);

                while (true)
                {
                    if (CommParam.XYZ_First)
                    {
                        Thread a = new Thread(() =>
                        {
                            Goog.Go_Single_Poin(x, y, z);
                        });
                        a.IsBackground = true;
                        a.Start();
                        CommParam.XYZ_First = false;
                        break;
                    }
                }

                while (true)
                {
                    if (CommParam.Stop_Buttom_On || CommParam.Urgent_Buttom_On) break;
                    if (CommParam.XYZ_Second)
                    {
                        Thread b = new Thread(() =>
                        {
                            Rotate_Screw();
                        });
                        b.IsBackground = true;
                        b.Start();
                        CommParam.XYZ_Second = false;
                        break;
                    }
                }

                if (cur == length)
                {
                    while (true)
                    {
                        if (CommParam.XYZ_First)
                        {
                            Goog.PrfTrap_Action(CommParam.AxisZ, 0, CommParam.CB_Vel, CommParam.CB_Acc, CommParam.CB_Dec, CommParam.CB_SmoothTime);

                            break;
                        }
                    }
                }


            }
            CommParam.Is_Run = 0;
        }







        void Rotate_Screw()
        {
            mr_a._wuint16(61500, _stand, 0x2AAA);
            Thread.Sleep(10);
            mr_b._wuint16(61500, _stand, 0x2AAA);
            while (true)
            {
                if (moto_status)//  螺丝打完
                {
                    mr_a._wuint16(61500, _stand, 0x2BBB);
                    mr_b._wuint16(61500, _stand, 0x2BBB);
                    Thread.Sleep(100);
                    CommParam.XYZ_First = true;
                    break;
                }

            }

        }

        private void Run_All_Button_Click(object sender, EventArgs e)
        {
            if (!CommParam.Is_Gohome)
            {
                UIMessageDialog.ShowMessageDialog("请先执行回原点！", UILocalize.InfoTitle, false, Style);
                return;
            }

            if (!ShowAskDialog("确定运行所有104个螺丝位吗")) return;

            Run_Point(point_List);
        }

        //初始化CAD点位
        List<CommParam.Point_Data> point_List = new List<CommParam.Point_Data>();
        public Hashtable Point_Num = new Hashtable(); //Point_Num[6]
        void Ini_CAD_PointFile()
        {
            List<CommParam.Point_Data> leftTop = Get_data_Txt(CommParam.FifthGe_leftTop);
            List<CommParam.Point_Data> leftbot = Get_data_Txt(CommParam.FifthGe_leftbot);
            List<CommParam.Point_Data> rightTop = Get_data_Txt(CommParam.FifthGe_rightTop);
            List<CommParam.Point_Data> rightBot = Get_data_Txt(CommParam.FifthGe_rightBot);

            point_List.AddRange(leftTop);
            point_List.AddRange(leftbot);
            point_List.AddRange(rightTop);
            point_List.AddRange(rightBot);

            int n = 0;
            foreach (var item in point_List)
            {
                Point_Num.Add(n, item);
                n++;
            }

        }


        public void GetNetInfo_DoMoto(string msg)
        {

            int num = 0;//螺丝编号
            int ir = 0;//圈数
            int dir = 0;//方向
            //ushort Cir = ushort.Parse(ir.ToString("X"));
            ushort Cir = CommUtil.int_To_Hex(ir);//圈数

            //1、运行到对应点位
            CommParam.Point_Data point_Data = (CommParam.Point_Data)Point_Num[num];

            int x = CommUtil.Cir_To_Pulse(point_Data.Point_X_Pos);
            int y = CommUtil.Cir_To_Pulse(point_Data.Point_Y_Pos);
            int z = CommUtil.Cir_To_Pulse(point_Data.Point_Z_Pos);

            Thread a = new Thread(() =>
            {
                Goog.Go_Single_Poin(x, y, z);
            });
            a.IsBackground = true;
            a.Start();

            while (true)//走到指定点位
            {
                if (CommParam.XYZ_Second)
                {
                    CommParam.XYZ_Second = false;
                    break;
                }
            }



            //2、松螺母
            mr_a._wuint16(4110, _stand, Cir);//拧松圈数
            Thread.Sleep(time);
            mr_a._wuint16(4111, _stand, 0x64);//拧松速度
            Thread.Sleep(time);
            mr_a._wuint16(61500, _stand, 0x2CCC);  // 开始拧松
            while (true)
            {
                if (moto_status)//  螺丝打完
                {
                    mr_a._wuint16(61500, _stand, 0x2DDD);  // 停止拧松
                    break;
                }
            }

            //3、处理螺丝

            //int ir = 0;//圈数
            //int dir = 0;//方向
            if (dir == 0)//正转 拧紧
            {
                Thread.Sleep(time);
                mr_b._wuint16(4060, _stand, Cir);
                Thread.Sleep(time);
                mr_b._wuint16(4061, _stand, 0x64);
                Thread.Sleep(time);
                mr_b._wuint16(61500, _stand, 0x2AAA);  // 开始拧紧
                while (true)
                {
                    if (moto_status)//  螺丝打完
                    {
                        mr_b._wuint16(61500, _stand, 0x2BBB);  // 停止拧紧
                        break;
                    }
                }
            }
            else if (dir == 1)//反转  拧松
            {

                Thread.Sleep(time);
                mr_b._wuint16(4110, _stand, Cir);
                Thread.Sleep(time);
                mr_b._wuint16(4111, _stand, 0x64);

                Thread.Sleep(time);
                mr_b._wuint16(61500, _stand, 0x2CCC);  // 开始拧松
                while (true)
                {
                    if (moto_status)//  螺丝打完
                    {
                        mr_b._wuint16(61500, _stand, 0x2DDD);  // 停止拧松
                        break;
                    }
                }
            }


            //4、紧螺母
            Thread.Sleep(time);
            mr_a._wuint16(4060, _stand, Cir);//拧紧圈数
            Thread.Sleep(time);
            mr_a._wuint16(4061, _stand, 0x64);//拧紧速度
            Thread.Sleep(time);
            mr_a._wuint16(61500, _stand, 0x2AAA);  // 开始拧紧

            while (true)
            {
                if (moto_status)//  螺丝打完
                {
                    mr_a._wuint16(61500, _stand, 0x2BBB);  // 停止拧紧
                    break;
                }
            }






        }








    }
}
