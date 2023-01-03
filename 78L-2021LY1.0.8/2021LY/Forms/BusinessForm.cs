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


/**

    ********日志************
1、加入log4net.dll  ，log4net.xml  到debug目录 
2、在App.config 加入配置
3、加入log类  LogHelper.cs
4、 引用加入log4net的dll

    ********铣刀寿命 ************
    CommUtil.Distance_PP(3,4)    计算距离

    Alarm.cs 修改报警内容

    IOForm,加timer1,右下角异常显示
    将 CommParam.AlarmMsg = "" 删除 

    DesignForm:
        加入 Down_Knife_Km方法 下刀数
        Zb_Run1方法 加入计算铣刀寿命  （ if (type == CommParam.Path_End)//终点，计算里程）一段




**/

namespace _2021LY.Forms
{
    public partial class BusinessForm : UITitlePage
    {
        IniFunctoin iniFunctoin = new IniFunctoin();//得到配置文件信息
        //string KniefParamPath = Application.StartupPath + "\\KniefParam.txt";
        FileUtil f = new FileUtil();
        public BusinessForm()
        {
            InitializeComponent();
            string pa = string.Empty;
            CommParam.Knief_Param_Path=CommParam.Knief_Param_Path.Replace("\\\\","\\");
            
            f.Read_File(CommParam.Knief_Param_Path, out pa);
            if (pa.Contains("\0\0\0\0"))
            {
                string param = "150000,12342,3,0";
                f.Save_File(CommParam.Knief_Param_Path, param);
            }
            Read_Knife_Param();
            Read_Change_Knife_Pos_Param();
            Read_Start_Knife_Param();
            timer1.Start();
            logtype_uiComboBox.Items.Clear();
            logtype_uiComboBox.Items.Add("运行日志");
            logtype_uiComboBox.Items.Add("报警日志");
            logtype_uiComboBox.SelectedIndex = 0;
            Control.CheckForIllegalCrossThreadCalls = false;
            this.Text = "业务数据";
        }


        private static BusinessForm businessForm;

        public static BusinessForm getForm()
        {
            if (businessForm == null)
            {
                businessForm = new BusinessForm();
            }
            return businessForm;
        }

        private void LogQuery_uiButton_Click(object sender, EventArgs e)
        {
            int type = logtype_uiComboBox.SelectedIndex;
            string da = msg_uiDatePicker.Value.ToString("yyyyMMdd");
            DirectoryInfo di = new DirectoryInfo(string.Format(@"{0}..\..\", Application.StartupPath));
            string docu = "";
            if (type == 0) { docu = "LogInfo"; }
            else { docu = "LogDebug"; }
            string s = di.FullName + "Log\\" + docu + "\\" + da + ".txt";
            Console.WriteLine(s);
            msg_uiTextBox.Text = "";

            FileUtil f = new FileUtil();
            string param = "";
            f.Read_File(s, out param);
            msg_uiTextBox.Text = param;



        }
        private void Save_Knife_uiButton_Click(object sender, EventArgs e)
        {
            if (!ShowAskDialog("确定保存吗")) return;
            Save_Knife_Param();
        }

        /// <summary>
        /// 实时保存距离
        /// </summary>
        /// <param name="cu">走的mm数</param>

        string Knife_Total_Km;
        long Knife_Cur_Km;
        string Knife_Downs;
        string Knife_Downs_Open;
        public void Save_Knife_Param(string cu)
        {
          
            if (Knife_Downs_Switch.Active)
            {
               
                CommParam.iskd = true;
            }
            else
            {
               
                CommParam.iskd = false;
            }

            double xx = CommUtil.TransData(cu, 0);
            Knife_Cur_Km = Knife_Cur_Km + Convert.ToInt64(xx);
            Knife_Cur_Km_uiTextBox.Text = Knife_Cur_Km.ToString();
            try
            {
                iniFunctoin.WriteStringToIni("Cutter_parameters", "Knife_Total_Km_ui", Knife_Total_Km, CommParam.Knief_Param_Path).ToString();
                iniFunctoin.WriteStringToIni("Cutter_parameters", "Knife_Cur_Km_ui", Knife_Cur_Km.ToString(), CommParam.Knief_Param_Path).ToString();
                iniFunctoin.WriteStringToIni("Cutter_parameters", "Knife_Downs_ui", Knife_Downs, CommParam.Knief_Param_Path).ToString();
                iniFunctoin.WriteStringToIni("Cutter_parameters", "Knife_Downs_num", Knife_Downs_num.Text, CommParam.Knief_Param_Path).ToString();
                iniFunctoin.WriteStringToIni("Cutter_parameters", "Max_Knife_Downs", Max_Knife_Downs_num.Text, CommParam.Knief_Param_Path).ToString();
                // iniFunctoin.WriteStringToIni("Cutter_parameters", "taga", taga, CommParam.Knief_Param_Path).ToString();

                //f.Del_File(CommParam.Knief_Param_Path);
                //bool status = f.Save_File(CommParam.Knief_Param_Path, param);
                //if (status)
                //{
                //ShowSuccessDialog(" 保存成功");
                //Read_Knife_Param();
                //}
                //else
                //{
                //    ShowErrorDialog("保存失败!");
                //}
            }
            catch (Exception x)
            {
                LogHelper.WriteErrorLog("铣刀参数保存失败" + x.Message.ToString(), x);
            }

            //string param = Knife_Total_Km + "," + Knife_Cur_Km + "," + Knife_Downs + "," + Knife_Downs_Open;
            //f.Del_File(CommParam.Knief_Param_Path);
            //bool status = f.Save_File(CommParam.Knief_Param_Path, param);
        }
        /// <summary>
        /// 页面保存方法
        /// </summary>
        void Save_Knife_Param()
        {

            string taga = "0";
            if (Knife_Downs_Switch.Active)
            {
                taga = "1";
                CommParam.iskd = true;
            }
            else
            {
                taga = "0";
                CommParam.iskd = false;
            }



            //string param = Knife_Total_Km_uiTextBox.Text + "," + Knife_Cur_Km_uiTextBox.Text + "," + Knife_Downs_uiTextBox.Text + "," + taga;
            try
            {
                iniFunctoin.WriteStringToIni("Cutter_parameters", "Knife_Total_Km_ui", Knife_Total_Km_uiTextBox.Text, CommParam.Knief_Param_Path).ToString();
                iniFunctoin.WriteStringToIni("Cutter_parameters", "Knife_Cur_Km_ui", Knife_Cur_Km_uiTextBox.Text, CommParam.Knief_Param_Path).ToString();
                iniFunctoin.WriteStringToIni("Cutter_parameters", "Knife_Downs_ui", Knife_Downs_uiTextBox.Text, CommParam.Knief_Param_Path).ToString();
                iniFunctoin.WriteStringToIni("Cutter_parameters", "Knife_Downs_num", Knife_Downs_num.Text, CommParam.Knief_Param_Path).ToString();
                iniFunctoin.WriteStringToIni("Cutter_parameters", "Max_Knife_Downs", Max_Knife_Downs_num.Text, CommParam.Knief_Param_Path).ToString();
                // iniFunctoin.WriteStringToIni("Cutter_parameters", "taga", taga, CommParam.Knief_Param_Path).ToString();

                //f.Del_File(CommParam.Knief_Param_Path);
                //bool status = f.Save_File(CommParam.Knief_Param_Path, param);
                //if (status)
                //{
                ShowSuccessDialog(" 保存成功");
                    Read_Knife_Param();
                //}
                //else
                //{
                //    ShowErrorDialog("保存失败!");
                //}
            }
            catch (Exception x)
            {
                LogHelper.WriteErrorLog("铣刀参数保存失败" + x.Message.ToString(), x);
            }
        }

        void Read_Knife_Param()
        {

            string param = "";
            try
            {
                //f.Read_File(CommParam.Knief_Param_Path, out param);
                //if (!string.IsNullOrEmpty(param))
                //{


                //  string[] All_param = param.Split(new char[] { ',' });
                var Knife_Total_Km_ui= iniFunctoin.ReadIniString("Cutter_parameters", "Knife_Total_Km_ui", CommParam.Knief_Param_Path).ToString();
                var Knife_Cur_Km_ui = iniFunctoin.ReadIniString("Cutter_parameters", "Knife_Cur_Km_ui", CommParam.Knief_Param_Path).ToString();
                var Knife_Downs_ui = iniFunctoin.ReadIniString("Cutter_parameters", "Knife_Downs_ui", CommParam.Knief_Param_Path).ToString();
                var Knife_Downs_num1 = iniFunctoin.ReadIniString("Cutter_parameters", "Knife_Downs_num", CommParam.Knief_Param_Path).ToString();
                var Max_Knife_Downs1 = iniFunctoin.ReadIniString("Cutter_parameters", "Max_Knife_Downs", CommParam.Knief_Param_Path).ToString();
                // var taga = iniFunctoin.ReadIniString("Cutter_parameters", "taga", CommParam.Knief_Param_Path).ToString();


                Knife_Total_Km_uiTextBox.Text = Knife_Total_Km_ui;
                    Knife_Total_Km = Knife_Total_Km_ui;
                    Knife_Cur_Km_uiTextBox.Text = Knife_Cur_Km_ui;
                    Knife_Cur_Km = Convert.ToInt64(Knife_Cur_Km_ui);
                    Knife_Downs_uiTextBox.Text = Knife_Downs_ui;
                    Knife_Downs = Knife_Downs_ui;
                    Knife_Downs_num.Text = Knife_Downs_num1;
                Max_Knife_Downs_num.Text = Max_Knife_Downs1;
                //if (taga.Equals("1"))
                //{
                //    Knife_Downs_Switch.Active = true;
                //    CommParam.iskd = true;
                //}
                //else
                //{
                //    Knife_Downs_Switch.Active = false;
                //    CommParam.iskd = false;
                //}

                //Knife_Downs_Open = taga;
                //}
                //else
                //{
                //    ShowErrorDialog("读取铣刀参数失败，请联系技术人员");
                //}
            }
            catch (Exception x)
            {
                LogHelper.WriteErrorLog("铣刀参数读取失败" + x.Message.ToString(), x);
            }
        }



        private void uiButton1_Click(object sender, EventArgs e)
        {


            // singe_uiLedStopwatch.Active = true;
            // timer_sig_cut.ReStart();


            // Console.WriteLine(CommUtil.Distance_PP(3, 4));


            /*********************/

            //try
            //{
            //    string str = "测试运行日志es1212ces1212ces1212ces1212ces1212ces1212ces1212ces1212";
            //    LogHelper.WriteInfoLog(str);
            //    long C = DateTime.Now.Year * 10000000000 + DateTime.Now.Month * 100000000 + DateTime.Now.Day * 1000000 + DateTime.Now.Hour * 10000 + DateTime.Now.Minute * 100 + DateTime.Now.Second;
            //    int len = msg_uiTextBox.Lines.Length;
            //    if (len == 10) msg_uiTextBox.Text = "";
            //    msg_uiTextBox.AppendText(C.ToString() + " " + str + "\r\n");
            //    int value = 1 / int.Parse("0");
            //}
            //catch (Exception ex)
            //{
            //    LogHelper.WriteErrorLog("测试测试：" + ex.Message.ToString(), ex);
            //    LogHelper.WriteErrorLog("测试测试222：" + ex.Message.ToString());
            //    LogHelper.WriteDebugLog("Debug测试测试：" + ex.Message.ToString());
            //}
        }

        //实时显示里程、电量、计数    uiLedDisplay
        int wat = 0;
        public int totalNum = 0;
        public bool cl_num = false;  //是否开始计时
        public int clNum = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {

            /////////通过设置  cl_num的值，来显示单板计时时间//////////
            if (cl_num)
            {
                if (wat == 0)
                {
                    singe_uiLedStopwatch.Active = true;
                    wat++;
                }
            }
            if (!cl_num)
            {
                wat = 0;
                singe_uiLedStopwatch.Active = false;
            }


            /////////////当前剩余铣刀里程/////////////
            if (Knife_Total_Km_uiTextBox.Text != "")
            {
                //Knife_Total_Km_uiTextBox.Text = "90000";
            }
            Double perc = Convert.ToDouble(Knife_Cur_Km_uiTextBox.Text) / Convert.ToDouble(Knife_Total_Km_uiTextBox.Text) * 100;
            int xx = Convert.ToInt32(perc);//消耗电量
            Knife_uiBattery.Power = 100 - xx;//剩余电量

            /////////////当前产能/////////////
            total_Num.Text = totalNum + "";

        }


        private void Change_Knife_Pos_uiButton_Click(object sender, EventArgs e)
        {
            if (!ShowAskDialog("确定更新换刀位置吗？")) return;
            Change_Knife_Pos_uiTextBox.Text = CommParam.X_Pos + "_" + CommParam.Y_Pos + "_" + CommParam.Z_Pos;
            Save_Change_Knife_Pos_Param();
        }
        void Save_Change_Knife_Pos_Param()
        {
            string param = Change_Knife_Pos_uiTextBox.Text;
            try
            {
                f.Del_File(CommParam.Change_Knife_Pos);
                bool status = f.Save_File(CommParam.Change_Knife_Pos, param);
                if (status)
                {
                    Read_Change_Knife_Pos_Param();
                    ShowSuccessDialog(" 保存成功");
                }
                else
                {
                    ShowErrorDialog("保存失败!");
                }
            }
            catch (Exception x)
            {
                LogHelper.WriteErrorLog("铣刀参数保存失败" + x.Message.ToString(), x);
            }
        }

        void Read_Change_Knife_Pos_Param()
        {

            string param = "";
            try
            {
                f.Read_File(CommParam.Change_Knife_Pos, out param);
                if (!string.IsNullOrEmpty(param))
                {
                    Change_Knife_Pos_uiTextBox.Text = param;
                }
                else
                {
                    ShowErrorDialog("读取换刀参数失败，请联系技术人员");
                }
            }
            catch (Exception x)
            {
                LogHelper.WriteErrorLog("换刀位置参数读取失败" + x.Message.ToString(), x);
            }
        }

        private void Change_Knife_uiButton_Click(object sender, EventArgs e)
        {
            if (!CommParam.Is_Gohome)
            {
                //ShowErrorDialog("请先回原点");
                return ;
            }
            if (!ShowAskDialog("确定移动到换刀位置吗")) return;
            string pos = Change_Knife_Pos_uiTextBox.Text;
            string[] left = pos.Split('_');

            int x = Convert.ToInt32(left[0]);
            int y = Convert.ToInt32(left[1]);
            int z = Convert.ToInt32(left[2]);

            Thread Change_Knife = new Thread(() =>
            {
                Goog.Go_Single_Poin(x, y, z);
            });
            Change_Knife.IsBackground = true;
            Change_Knife.Start();
        }

        public void Add_Info_ToText(string info)
        {
            int length = msg_uiTextBox.Lines.Length;
            if (length > 20) msg_uiTextBox.Text = "";
            msg_uiTextBox.AppendText(CommUtil.Get_YMDHMS() + " " + info + "\r\n");
        }

        int pcb = 0;
        private void Up_PCB_uiButton_Click(object sender, EventArgs e)
        {
            if (pcb == 0)//上
            {
                Up_PCB_uiButton.FillColor = Color.Gray;
                Up_PCB_uiButton.FillHoverColor = Color.Gray;
                IOForm.getForm().SetDoOn(10, IOForm.getForm().Exo[9]);
                Up_PCB_uiButton.FillColor = Color.DarkGray;
                Goog.SetExtDo_On(6);
                pcb++;
            }
            else
            {
                Up_PCB_uiButton.FillColor = Color.DodgerBlue;
                Up_PCB_uiButton.FillHoverColor = Color.DodgerBlue;
                Goog.SetExtDo_Off(6);
                IOForm.getForm().SetDoOff(10, IOForm.getForm().Exo[9]);
                Up_PCB_uiButton.FillColor = Color.DodgerBlue;
                pcb = 0;
            }
        }

        int frontS = 0;
        private void Down_PCB_uiButton_Click(object sender, EventArgs e)
        {
            if (frontS == 0)//下
            {
                Down_PCB_uiButton.FillColor = Color.Gray;
                Down_PCB_uiButton.FillHoverColor = Color.Gray;
                IOForm.getForm().SetDoOff(7, IOForm.getForm().Exo[6]);
                Down_PCB_uiButton.FillColor = Color.DarkGray;
                frontS++;
            }
            else
            {
                Down_PCB_uiButton.FillColor = Color.DodgerBlue;
                Down_PCB_uiButton.FillHoverColor = Color.DodgerBlue;
                IOForm.getForm().SetDoOn(7, IOForm.getForm().Exo[6]);
                Down_PCB_uiButton.FillColor = Color.DodgerBlue;
                frontS = 0;
            }

        }

        public void Clear_Total_uiButton_Click(object sender, EventArgs e)
        {
            if (!ShowAskDialog("确定清零当前产能吗")) return;
            totalNum = 0;
        }

        public void uiButton2_Click(object sender, EventArgs e)
        {

        }

        private void Knife_Cur_Zero_uiButton_Click(object sender, EventArgs e)
        {
            if (!ShowAskDialog("确定清零当前里程并保存吗")) return;
            Knife_Cur_Km_uiTextBox.Text = "0";
            Save_Knife_Param();
        }


        ////////启动主轴////////

        void Save_Start_Knife_Param()
        {
            int left = 0, right = 0;
            if (left_Knife_Switch.Active) left = 1;
            if (right_Knife_Switch.Active) right = 1;
            try
            {
                f.Del_File(CommParam.Start_Knife_Path);
                bool status = f.Save_File(CommParam.Start_Knife_Path, left + "," + right);
                if (status)
                {
                    Read_Start_Knife_Param();
                    ShowSuccessDialog(" 保存成功");
                }
                else
                {
                    ShowErrorDialog("保存失败!");
                }
            }
            catch (Exception x)
            {
                LogHelper.WriteErrorLog("启动主轴设置保存失败" + x.Message.ToString(), x);
            }
        }

        void Read_Start_Knife_Param()
        {

            string param = "";
            try
            {
                f.Read_File(CommParam.Start_Knife_Path, out param);
                if (!string.IsNullOrEmpty(param))
                {
                    string[] p = param.Split(",");
                    if (p[0].Equals("1"))
                    {
                        left_Knife_Switch.Active = true;
                        CommParam.left_Knife_Switch = true;
                    }
                    else
                    {
                        left_Knife_Switch.Active = false;
                        CommParam.left_Knife_Switch = false;
                    }
                    if (p[1].Equals("1"))
                    {
                        right_Knife_Switch.Active = true;
                        CommParam.right_Knife_Switch = true;
                    }
                    else
                    {
                        right_Knife_Switch.Active = false;
                        CommParam.right_Knife_Switch = false;
                    }
                }
                else
                {
                    ShowErrorDialog("读取启动主轴设置失败，请联系技术人员");
                }
            }
            catch (Exception x)
            {
                LogHelper.WriteErrorLog("启动主轴参数读取失败" + x.Message.ToString(), x);
            }
        }

        private void right_Knife_Switch_MouseUp(object sender, MouseEventArgs e)
        {
            if (!ShowAskDialog("确定更改使用铣刀状态吗")) return;
            Save_Start_Knife_Param();

        }

        private void left_Knife_Switch_MouseUp(object sender, MouseEventArgs e)
        {
            if (!ShowAskDialog("确定更改使用铣刀状态吗")) return;
            Save_Start_Knife_Param();

        }

        private void Knife_Downs_Switch_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void uiButton2_Click_1(object sender, EventArgs e)
        {
            ShowSuccessDialog(CommParam.iskd+"");
        }

        int backS = 0;
        private void HouZuDang_Button_Click(object sender, EventArgs e)
        {
            if (backS == 0)//下
            {
                HouZuDang_Button.FillColor = Color.Gray;
                HouZuDang_Button.FillHoverColor = Color.Gray;

                IOForm.getForm().SetDoOff(11, IOForm.getForm().Exo[10]);
                HouZuDang_Button.FillColor = Color.DarkGray;
                backS++;
            }
            else
            {
                HouZuDang_Button.FillColor = Color.DodgerBlue;
                HouZuDang_Button.FillHoverColor = Color.DodgerBlue;

                IOForm.getForm().SetDoOn(11, IOForm.getForm().Exo[10]);
                HouZuDang_Button.FillColor = Color.DodgerBlue;
                backS = 0;
            }
        }

        int xichen = 0;
        private void XiChen_Button_Click(object sender, EventArgs e)
        {
            if (xichen == 0)//下
            {
                XiChen_Button.FillColor = Color.Gray;
                XiChen_Button.FillHoverColor = Color.Gray;

                IOForm.getForm().SetDoOff(16, IOForm.getForm().Exo[15]);
                XiChen_Button.FillColor = Color.DarkGray;
                xichen++;
            }
            else
            {
                XiChen_Button.FillColor = Color.DodgerBlue;
                XiChen_Button.FillHoverColor = Color.DodgerBlue;

                IOForm.getForm().SetDoOn(16, IOForm.getForm().Exo[15]);
                XiChen_Button.FillColor = Color.DodgerBlue;
                xichen = 0;
            }
        }

        int xidaoleft = 0;
        private void XiDao1_Button_Click(object sender, EventArgs e)
        {
            if (xidaoleft == 0)
            {
                XiDao1_Button.FillColor = Color.Gray;
                XiDao1_Button.FillHoverColor = Color.Gray;
                
                IOForm.getForm().SetDoOff(13, IOForm.getForm().Exo[12]); //DO12 主轴冷却吹风   开启
                Thread.Sleep(300);//吹风和主轴不可同时启动
                IOForm.getForm().SetDoOff(14, IOForm.getForm().Exo[13]);// DO13 主轴启动 开始旋转
                Thread.Sleep(550);//启动后等半秒开始检测
                bool run1 = Goog.GetExtIoBit(3);
                if (!run1)
                {
                    IOForm.getForm().SetDoOn(13, IOForm.getForm().Exo[12]);
                    IOForm.getForm().SetDoOn(14, IOForm.getForm().Exo[13]);
                    ShowErrorDialog("铣刀启动异常，请查看铣刀驱动显示屏");
                    return;
                }
                xidaoleft++;
            }
            else
            {
                XiDao1_Button.FillColor = Color.DodgerBlue;
                XiDao1_Button.FillHoverColor = Color.DodgerBlue;
               
                IOForm.getForm().SetDoOn(13, IOForm.getForm().Exo[12]);
                Thread.Sleep(300);
                IOForm.getForm().SetDoOn(14, IOForm.getForm().Exo[13]);
                xidaoleft = 0;
            }

        }

        int xidaor = 0;
        private void XiDao2_Button_Click(object sender, EventArgs e)
        {
            if (xidaor == 0)
            {
                XiDao2_Button.FillColor = Color.Gray;
                XiDao2_Button.FillHoverColor = Color.Gray;
               
                IOForm.getForm().SetDoOff(13, IOForm.getForm().Exo[12]); //DO12 主轴冷却吹风   开启
                Thread.Sleep(300);//吹风和主轴不可同时启动
                IOForm.getForm().SetDoOff(12, IOForm.getForm().Exo[11]);// DO13 主轴启动 开始旋转
                Thread.Sleep(550);//启动后等半秒开始检测
                bool run1 = Goog.GetExtIoBit(2);
                if (!run1)
                {
                    IOForm.getForm().SetDoOn(13, IOForm.getForm().Exo[12]);
                    IOForm.getForm().SetDoOn(12, IOForm.getForm().Exo[11]);
                    ShowErrorDialog("铣刀启动异常，请查看铣刀驱动显示屏");
                    return;
                }
                xidaor++;
            }
            else
            {
                XiDao2_Button.FillColor = Color.DodgerBlue;
                XiDao2_Button.FillHoverColor = Color.DodgerBlue;
               
                IOForm.getForm().SetDoOn(13, IOForm.getForm().Exo[12]);
                Thread.Sleep(300);
                IOForm.getForm().SetDoOn(12, IOForm.getForm().Exo[11]);
                xidaor = 0;
            }
        }

        private void safeDoor_Switch_MouseUp(object sender, MouseEventArgs e)
        {
            if (safeDoor_Switch.Active)
            {
                CommParam.Safe_Door_Check = true;
            }
            else
            {
                CommParam.Safe_Door_Check = false;
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Knife_Downs_Switch_ValueChanged(object sender, bool value)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           // Goog.P2PAbs(CommParam.AxisZ, 2000, 2, 1, 1, 1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
           // Goog.GT800_LnXY(1, 15000, 20000, 3, 1, 0, 0);
        }
    }
}
