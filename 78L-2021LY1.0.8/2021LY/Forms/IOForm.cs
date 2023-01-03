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

namespace _2021LY.Forms
{
    public partial class IOForm : UITitlePage
    {
        UILight[] Exi = new UILight[16];  //DI
        UILight[] Limitz = new UILight[4];  //正限位
        UILight[] Limitf = new UILight[4];  //负限位

        UILight[] Home = new UILight[4];  //负限位
        public UILight[] Exo = new UILight[16];  //DI
        UILight[] Ala = new UILight[4];  //DI

        private static IOForm ioform;

        public static IOForm getForm()
        {
            if (ioform == null)
            {
                ioform = new IOForm();
            }
            return ioform;
        }


        public IOForm()
        {
            InitializeComponent();
            this.Text = "IO监控";
            this.Load += IOForm_Load;
            //this.FormClosed += IOForm_FormClosed;
            this.FormClosing += IOForm_FormClosing;
            Control.CheckForIllegalCrossThreadCalls = false;
            timer1.Start();
        }



        // CancellationTokenSource tokenSource = new CancellationTokenSource();//创建取消task实例
        private void IOForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Console.WriteLine("IOForm_FormClosing窗口关闭");
            // System.Environment.Exit(0);
            //tokenSource.Token.Register(() =>
            //{
            //    Console.WriteLine("task is to cancel");
            //});
            //tokenSource.Cancel();

            // Goog.Reset();
            // Goog.Close();
        }
        private void IOForm_Load(object sender, EventArgs e)
        {
            Init_DiDo();//将输入输出图像初始化，方便即时显示通断~


            //Task Show_DiDo_Task = Task.Factory.StartNew(() => //实时监控DiDo
            //{
            //    Thread.CurrentThread.IsBackground = true;
            //    Show_DiDo();
            //});

            //Task Do_Dido_Action_Task = Task.Factory.StartNew(() => //执行DI和DO的动作
            //{
            //     Thread.CurrentThread.IsBackground = true;
            //    Do_Dido_Action();
            //});


            Thread t = new Thread(() =>
            {
                Do_Dido_Action();
            });
            t.IsBackground = true;
            t.Start();

            Thread t2 = new Thread(() =>
            {
                Show_DiDo();
            });
            t2.IsBackground = true;
            t2.Start();
        }

        //执行DI和DO的动作
        public void Do_Dido_Action()
        {
            try
            {
                while (CommParam.isAlive)
                {
                    Thread.Sleep(200);
                    if (CommParam.Ding == true || CommParam.Saoma == true || CommParam.Duidao == true || CommParam.ZhuZhou == true)
                    {
                        CommParam.Alam = true;
                        SetDoOff(CommParam.Error_Sound + 1, Exo[CommParam.Error_Sound]);//开凤鸣
                        SetDoOff(CommParam.Error_Red_Light + 1, Exo[CommParam.Error_Red_Light]);//开红灯
                        if (CommParam.Bussiness_Err_Msg != "")
                        {
                            ShowErrorDialog(CommParam.Bussiness_Err_Msg,false);//确认后取消报警
                            Cancel_Bussi_Alarm();
                        }
                    }

                   

                    //如果有报警信息，并且蜂鸣没关
                    if (CommParam.Alam && CommParam.Sound_ShutDowm)//报警并且凤鸣打开的情况下，打开凤鸣
                    {
                        SetDoOff(CommParam.Error_Sound + 1, Exo[CommParam.Error_Sound]);
                    }
                    else //if (!CommParam.Alam || !CommParam.Sound_ShutDowm)//没有报警或者蜂鸣关闭，都关闭凤鸣
                    {
                        SetDoOn(CommParam.Error_Sound + 1, Exo[CommParam.Error_Sound]);
                    }

                    if (CommParam.Alam && CommParam.isAlive)
                    {
                        SetDoOff(CommParam.Error_Red_Light + 1, Exo[CommParam.Error_Red_Light]);

                    }
                    else
                    {
                        SetDoOn(CommParam.Error_Red_Light + 1, Exo[CommParam.Error_Red_Light]);
                    }



                    if (CommParam.Is_Run == 0 && CommParam.isAlive && !CommParam.Alam)//停止
                    {
                        SetDoOff(CommParam.Stop_Yellow_Light + 1, Exo[CommParam.Stop_Yellow_Light]);
                        SetDoOn(CommParam.Nomal_Green_Light + 1, Exo[CommParam.Nomal_Green_Light]);
                    }

                    if (CommParam.Is_Run > 0 && !CommParam.Alam)//运行
                    {
                        SetDoOff(CommParam.Nomal_Green_Light + 1, Exo[CommParam.Nomal_Green_Light]);
                        SetDoOn(CommParam.Stop_Yellow_Light + 1, Exo[CommParam.Stop_Yellow_Light]);
                    }

                    Display_status(); //左下角状态显示

                    if (CommParam.AixsOnOrOff == 1)
                    {
                        DesignForm.getForm().axisOnOrOff.Text = "已使能";
                    }
                    else
                    {
                        DesignForm.getForm().axisOnOrOff.Text = "未使能";
                    }

                    CommParam.Alam = false;//如果出现报警，该状态会持续刷新

                    //if (CommParam.Tcpip_IsConnect)
                    //{
                    //    NetAnalysisForm.getForm().conn_state.State = UILightState.On;
                    //}
                    //else
                    //{
                    //    NetAnalysisForm.getForm().conn_state.State = UILightState.Off;
                    //}
                    Thread.Sleep(20);
                }

            }
            catch (Exception x)
            {
                LogHelper.WriteErrorLog("IOForm Do_Dido_Action 操作异常 ", x);
                // Logger.Recod_Log_File("IOForm Do_Dido_Action  " + x.Message + "\r\n x.StackTrace" + x.StackTrace);
                // Logger.MessageShow(" 新建轨迹终点 操作异常");
            }
        }

        //左下角状态显示   ~
        void Display_status()
        {
            string msg = "";
            if (CommParam.AlarmMsg.Length > 1)
            {
                msg = CommParam.AlarmMsg;

            }
            else
            {
                switch (CommParam.Is_Run)
                {
                    case 0: msg = "停止中"; break;
                    case 1: msg = "运行中"; break;
                    case 2: msg = "回零中"; break;
                }
            }
            try
            {

                this.Status_Msg.Text = msg;
                // ShowErrorNotifier(msg);
            }
            catch (Exception)
            {

            }

            //CommParam.AlarmMsg = "";//显示完就清空，如果有报警，信息会在触发端传来
        }

        /// <summary>
        ///  接通  DoNum  值为  1-16   代表DO0 ~DO15~~~~
        /// </summary>
        public void SetDoOff(int DoNum, UILight Exo)
        {
            if (DoNum == 0) return;//如果为0 ，则返回，不做任何动作
            Goog.SetDoBit(gts.mc.MC_GPO, (short)DoNum, 0);
            Exo.State = UILightState.On;

        }


        /// <summary>
        /// 断开  DoNum  值为  1-16   代表DO0 ~DO15    ~~~
        /// </summary>
        public void SetDoOn(int DoNum, UILight Exo)
        {
            if (DoNum == 0) return;//如果为0 ，则返回，不做任何动作
            Goog.SetDoBit(gts.mc.MC_GPO, (short)DoNum, 1);
            Exo.State = UILightState.Off;
        }



        /// <summary>
        /// 实时获取DIDO状态并给与反馈
        /// </summary>
        int lGpiValue;
        private void Show_DiDo()
        {
            while (CommParam.isAlive)//  while (this.Visible)//   while (true)// 
            {

                //bool ww = Goog.gpi_status(15);
                //bool ww1 = Goog.GetExtIoBit(0);
                //Console.WriteLine("scan_status:" + ww+ ww1);


                // Console.WriteLine("Show_DiDo");
                Thread.Sleep(1000);
                //根据固高板卡设定，不同的数字，代表不同的输入类型 
                for (short i = 0; i < 5; i++) //5种需要查询的类型
                {
                    Goog.GetDi(i, out lGpiValue);
                    if (gts.mc.MC_LIMIT_POSITIVE == i)//正限位
                    {
                        Change_Status(CommParam.Use_Axis, Limitz, 1);//3轴的限位 
                    }
                    else if (gts.mc.MC_LIMIT_NEGATIVE == i)//负限位
                    {
                        Change_Status(CommParam.Use_Axis, Limitf, 1);//3轴的限位
                    }
                    else if (gts.mc.MC_ALARM == i)//驱动报警
                    {
                        Change_Status(CommParam.Use_Axis, Ala, 1);//3轴的驱动
                    }
                    else if (gts.mc.MC_HOME == i)//原点开关 
                    {
                        Change_Status(CommParam.Use_Axis, Home, 1);//3轴的原点开关
                    }
                    else if (gts.mc.MC_GPI == i)//通用输入
                    {
                        // Console.WriteLine(lGpiValue);
                        Change_Status(15, Exi, 0);
                    }
                }
               
            }
        }

        /// <summary>
        /// int tag  是否取反值 ，当home 和限位都是常闭的情况下，从固高板卡出来的值竟然相反。。。。~
        /// </summary>
        private void Change_Status(int num, UILight[] pb, int tag)
        {
            //Console.WriteLine("----------------------");
            /*for (int ii = 0; ii < num; ii++)
            {
                if ((lGpiValue & (1 << ii)) != 0)
                {
                    Console.WriteLine("!=   " + ii);
                }
                if ((lGpiValue & (1 << ii)) == 0)
                {
                    Console.WriteLine("==   " + ii);
                }

            }*/

            //Console.WriteLine(lGpiValue);4   10 11 12  14  15
            //!= 0断开   == 0 接通     如果DI是常闭，就需要在固高配置文件中设置对应点位“取反”

            for (int ii = 0; ii < num; ii++)
            {
                if ((lGpiValue & (1 << ii)) != 0)//信号常开、控制卡Di“通用输入”的“输入反转”的值“正常”的情况下，！=0表示断开
                {
                    if (tag == 1)
                    {
                        pb[ii].State = UILightState.On;
                        Alarm.Send_Alam_Type(pb[ii].Name); //点亮信号，则发给报警处理// Console.WriteLine(pb[ii].Name);//  
                    }
                    else
                    {
                        pb[ii].State = UILightState.Off;
                    }
                }
                else if ((lGpiValue & (1 << ii)) == 0) //信号常开、控制卡Di“通用输入”的“输入反转”的值“正常”的情况下，==0表示接通
                {
                    if (tag == 1)
                    {
                        pb[ii].State = UILightState.Off;
                    }
                    else
                    {
                        pb[ii].State = UILightState.On;
                        Alarm.Send_Alam_Type(pb[ii].Name); //点亮信号，则发给报警处理    Console.WriteLine(pb[ii].Name);// 
                    }
                }
            }
        }

        private void Init_DiDo()
        {

            string pictrueStatus;
            //初始化DIDO
            for (int i = 0; i <= 15; i++)
            {
                pictrueStatus = "Exi" + (i).ToString();
                Exi[i] = (UILight)(this.Controls.Find(pictrueStatus, true)[0]);

                pictrueStatus = "Exo" + (i).ToString();
                Exo[i] = (UILight)(this.Controls.Find(pictrueStatus, true)[0]);
            }
            for (int i = 0; i <= 3; i++)
            {

                pictrueStatus = "Limitf" + (i).ToString();
                Limitf[i] = (UILight)(this.Controls.Find(pictrueStatus, true)[0]);

                pictrueStatus = "Limitz" + (i).ToString();
                Limitz[i] = (UILight)(this.Controls.Find(pictrueStatus, true)[0]);

                pictrueStatus = "Home" + (i).ToString();
                Home[i] = (UILight)(this.Controls.Find(pictrueStatus, true)[0]);

                pictrueStatus = "Ala" + (i).ToString();
                Ala[i] = (UILight)(this.Controls.Find(pictrueStatus, true)[0]);
            }
        }



        void SetDo(int Do)
        {
            int Exo_status = 0;
            Goog.GetDo(gts.mc.MC_GPO, out Exo_status);
            int DoNum = Do + 1;
            if ((Exo_status & (1 << Do)) == 0)
            {
                SetDoOn(DoNum, Exo[Do]);
                // Console.WriteLine("SetDoOn");
            }
            else if ((Exo_status & (1 << Do)) != 0)
            {
                SetDoOff(DoNum, Exo[Do]);
                //Console.WriteLine("SetDoOff");
            }

        }
        MelsecAscii_TCPIP aas;
   

        public void ShowErrorDialog_Msg(string msg)
        {
            Thread t = new Thread(() =>
            {
                ShowErrorDialog(msg);
            });
            t.IsBackground = true;
            t.Start();
        }

        int m = 0;

        private void Del_Alarm_uiButton_Click(object sender, EventArgs e)
        {
           
        }

        private void Cancel_Bussi_Alarm()
        {
            CommParam.Ding = false; //顶升不到位
            CommParam.Saoma = false; //扫码错误
            CommParam.Duidao = false; //对刀错误
            CommParam.ZhuZhou = false;//旋转主轴错误
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            if (CommParam.AlarmMsg.Length > 1)
            {
                ShowErrorNotifier(CommParam.AlarmMsg);
                CommParam.AlarmMsg = "";//显示完就清空，如果有报警，信息会在触发端传来
            }
        }

        private void uiGroupBox1_Click(object sender, EventArgs e)
        {

        }

        private void UiButton1_Click_1(object sender, EventArgs e)
        {

        }

        private void UiButton8_Click_1(object sender, EventArgs e)
        {
            //if (aas == null)
            //{
            //    aas = new MelsecAscii_TCPIP();
            //    aas.ConnectServer_TCP("192.168.1.2", "4899");
            //}
            //short va = aas.Read_D_short("D100");
            //shang.Text = va.ToString();
            //aas. Write_D_short("D101", 1);

            //CommParam.PLC_top = new MelsecAscii_TCPIP();
            //bool Ascii_top = CommParam.PLC_top.ConnectServer_TCP(CommParam.PLC_topIp, "4899");
            //if (!Ascii_top)
            //{
            //    ShowErrorDialog(CommParam.PLC_topIp + "链接失败");
            //    return;
            //}
            CommParam.PLC_bot = new MelsecAscii_TCPIP();
            bool Ascii_bot = CommParam.PLC_bot.ConnectServer_TCP(CommParam.PLC_botIp, "4899");
            if (!Ascii_bot)
            {
                ShowErrorDialog(CommParam.PLC_botIp + "链接失败");
                PLC1.State = UILightState.Off;
                return;
            }
            else
            {

                PLC1.State = UILightState.On;
            }
        }

        private void GroupBox1_Enter(object sender, EventArgs e)
        {
            SetDo(Convert.ToInt32(dotext.Text));
        }

        private void UiButton2_Click_1(object sender, EventArgs e)
        {
            Goog.SetExtDo_On(short.Parse(exdotext.Text));
        }

        private void UiButton3_Click_1(object sender, EventArgs e)
        {
            Goog.SetExtDo_Off(short.Parse(exdotext.Text));
        }

        private void Dushang_TextChanged(object sender, EventArgs e)
        {

        }

        private void UiButton4_Click_1(object sender, EventArgs e)
        {
            // short va = CommParam.PLC_top.Read_D_short(dushang.Text);
            // value.Text = va.ToString();
        }

        private void UiButton7_Click_1(object sender, EventArgs e)
        {
               short va = CommParam.PLC_bot.Read_D_short(duxia.Text);
            value.Text = va.ToString();
        }

        private void UiButton5_Click_1(object sender, EventArgs e)
        {
            // CommParam.PLC_top.Write_D_short(xieshang.Text, 1);
        }

        private void UiButton6_Click_1(object sender, EventArgs e)
        {
            CommParam.PLC_bot.Write_D_short(xiexia.Text, 1);
        }

        private void UiButton10_Click_1(object sender, EventArgs e)
        {
            //IOForm.getForm().SetDoOn(CommParam.Error_Red_Light + 1, IOForm.getForm().Exo[CommParam.Error_Red_Light]);
            //IOForm.getForm().SetDoOn(CommParam.Error_Sound + 1, IOForm.getForm().Exo[CommParam.Error_Sound]);
            CommParam.Alam = false;
        }

        private void UiButton9_Click_1(object sender, EventArgs e)
        {
            string[] Resutlt = new string[] { "1", "2", "1", "2", "qwe" };

            string st = Resutlt[Resutlt.Length - 1];
            for (int i = 0; i < Resutlt.Length; i++)
            {
                if (Resutlt[i] == "2")
                {
                    //  int x = a * 10 + i;
                    // CommParam.PLC_bot.Write_D_short("D" + x, 1);
                    st += i;
                    // Thread.Sleep(10);
                }

            }
            Console.WriteLine(st);

            //CommParam.Ding = true; 
            //DesignForm.getForm().ShowErrorDialog("分板顶升气缸没到位，检查是否卡板");
            //CommParam.Ding = false;


        }

        private void Del_Alarm_uiButton_Click_1(object sender, EventArgs e)
        {
            Cancel_Bussi_Alarm();
        }

        private void Sound_uiButton_Click_1(object sender, EventArgs e)
        {

            if (CommParam.Sound_ShutDowm == true)
            {
                CommParam.Sound_ShutDowm = false;
                IOForm.getForm().SetDoOn(CommParam.Error_Sound + 1, IOForm.getForm().Exo[CommParam.Error_Sound]);
                this.Sound_uiButton.Text = "蜂鸣已关闭";
            }
            else if (CommParam.Sound_ShutDowm == false)
            {
                CommParam.Sound_ShutDowm = true;
                this.Sound_uiButton.Text = "蜂鸣已打开";
            }
        }

        private void IOForm_FormClosed_1(object sender, FormClosedEventArgs e)
        {
            Goog.Reset();
            Goog.Close();
        }
    }
}
