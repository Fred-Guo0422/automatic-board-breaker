using _2021LY.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// 报警处理类
/// </summary>
namespace _2021LY
{
    class Alarm
    {
        public static void Send_Alam_Type(string name)
        {
            if (name != null && name.Length > 3)
            {
                string type = name.Substring(0, 3);
                if (type.Equals("Exi"))
                {
                    GPI_Alam(name);
                }
                else if (type.Equals("Ala"))
                {
                    Servo_Alam(name);
                }
                else if (type.Equals("Lim"))
                {
                    Limit_Alam(name);
                }
            }
        }


        /// <summary>
        ///  1伺服报警   立刻停止所有轴
        /// </summary>
        /// <param name="name"> Ala0-X   Ala1-y   Ala2-Z   Ala3-Y2</param>
        private static void Servo_Alam(string name)
        {
            string msg = "";
            if (name.Equals("Ala0"))
            {
                msg = "X轴报警,查看伺服驱动报警代码后断电重启";
            }
            else if (name.Equals("Ala1"))
            {
                msg = "Y轴报警,查看伺服驱动报警代码后断电重启";
            }
            else if (name.Equals("Ala2"))
            {
                msg = "Z轴报警,查看伺服驱动报警代码后断电重启";
            }
            else if (name.Equals("Ala3"))
            {
                msg = "Y2轴报警,查看伺服驱动报警代码后断电重启";
            }
            // Console.WriteLine("Servo_Alam");
            Stop_And_msg(msg);
            CommParam.Alam = true;
            CommParam.AlarmMsg = msg;
        }
        /// <summary>
        /// 3、限位报警            立刻停止所有轴
        /// </summary>
        ///  Limitz0 X轴正限位 Limitf0 X轴负限位   Limitz1 Y轴正限位 Limitf1 Y轴负限位
        /// <param name="name"></param>  
        private static void Limit_Alam(string name)
        {
            string msg = "";

            if (name.Equals("Limitz0"))
            {
                msg = "X轴已到正限位，手动操作，离开限位，长按复位按钮2次";
            }
            else if (name.Equals("Limitf0"))
            {
                msg = "X轴已到负限位，手动操作，离开限位，长按复位按钮2次";
            }
            else if (name.Equals("Limitz1"))
            {
                msg = "Y轴已到正限位，手动操作，离开限位，长按复位按钮2次";
            }
            else if (name.Equals("Limitf1"))
            {
                msg = "Y轴已到负限位，手动操作，离开限位，长按复位按钮2次";
            }
            else if (name.Equals("Limitz2"))
            {
                msg = "Z轴已到正限位，手动操作，离开限位，长按复位按钮2次";
            }
            else if (name.Equals("Limitf2"))
            {
                msg = "Z轴已到负限位，手动操作，离开限位，长按复位按钮2次";
            }
            else if (name.Equals("Limitz3"))
            {
                msg = "Y2轴已到正限位，手动操作，离开限位，长按复位按钮2次";
            }
            else if (name.Equals("Limitf3"))
            {
                msg = "Y2轴已到负限位，手动操作，离开限位，长按复位按钮2次";
            }
            CommParam.Alam = true;
            // Console.WriteLine("Limit_Alam");
            Stop_And_msg(msg);
            CommParam.AlarmMsg = msg;

        }

        

        /// <summary>
        /// 输入 急停、安全门等输入信号
        /// </summary>
        private static void GPI_Alam(string name)
        {
            string msg = "";
            string Reset_set = "";
            EventArgs e = null;
            int DI_Index = Convert.ToInt16(name.Substring(3, name.Length-3));
            //Console.WriteLine("name = {0},DI_Index= {1}",name, DI_Index);
            if (CommParam.Safe_Door_Check && (DI_Index == CommParam.Safe_Door_Buttom|| Goog.GetExtIoBit(10) || Goog.GetExtIoBit(12))) //1008
            {
                msg = "安全门已打开";
                CommParam.Alam = true;
                Console.WriteLine("Safe_Door_Buttom");
                //  Stop_And_msg(msg);
                CommParam.AlarmMsg = msg;
            }
            if (DI_Index == CommParam.Start_Buttom)//启动自动运行
            {
                Console.WriteLine("Start_Buttom");
                DesignForm.getForm().auto_run_Click("", e);
            }
            else if (DI_Index == CommParam.Stop_Buttom)// 停止 
            {
                Console.WriteLine("Stop_Buttom");
                CommParam.Stop_Buttom_On = true;
                DesignForm.getForm().goHome.Enabled = true;
                CommParam.Is_Run = 0;
                CommParam.auto_Run_On = false;
                DesignForm.getForm().auto_run.Enabled = true;
                DesignForm.getForm().Stop_Auto();
            }
            else if (DI_Index == CommParam.Reset_Buttom)//复位即回原点
            {
                //Console.WriteLine("Reset_Buttom");
                //CommParam.Stop_Buttom_On = true;
                ////Goog.Clear_Sts(1, 4);
                //DesignForm.getForm().goHome.Enabled = true;
                //CommParam.Is_Run = 0;
                //DesignForm.getForm().auto_run.Enabled = true;
            }
           

            else if (DI_Index == CommParam.Urgent_Buttom)//急停  则断开所有伺服使能
            {
                //if (!CommParam.Urgent_Buttom_On)
                //{
                Console.WriteLine("Urgent_Buttom");
                Goog.All_Axis_Off();
                IOForm.getForm().SetDoOn(CommParam.Z_Axis_Brake, IOForm.getForm().Exo[4]);//如果是Z轴，则开启刹车
                CommParam.AixsOnOrOff = 0;
                CommParam.Is_Run = 0;
                CommParam.Urgent_Buttom_On = true;
                DesignForm.getForm().Stop_Auto();
                //  }
            }
            else if (DI_Index == CommParam.Urgent_Cancel)  //报警取消，则关闭红灯以及凤鸣，但是如果报警原因没解决，该取消是无效的
            {
                IOForm.getForm().SetDoOn(CommParam.Error_Red_Light+1, IOForm.getForm().Exo[CommParam.Error_Red_Light]);
                IOForm.getForm().SetDoOn(CommParam.Error_Sound + 1, IOForm.getForm().Exo[CommParam.Error_Sound]);
                Console.WriteLine("Urgent_Cancel");
            }


            if (DI_Index != CommParam.Stop_Buttom)
            {
                CommParam.Stop_Buttom_On = false;
            }
            if (DI_Index != CommParam.Urgent_Buttom)
            {
                CommParam.Urgent_Buttom_On = false;
            }
        }

        private static void Stop_And_msg(string msg)
        {
            if (CommParam.isAlive)
                Goog.Stop_All_Axis();
        }
    }
}
