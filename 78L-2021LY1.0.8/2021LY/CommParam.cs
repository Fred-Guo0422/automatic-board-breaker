using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/// <summary>
/// 公共参数类  
/// </summary>

namespace _2021LY
{
    class CommParam
    {

        public struct Point_Data
        {
            public int RowNum;//行号
            public string type;//类型
            public string Point_X_Pos;//X轴位置
            public string Point_Y_Pos;//Y轴位置
            public string Point_Z_Pos;//Z轴位置
            public string Point_RY2_Pos;//Y2轴位置 或者半径
            public string tag;// 类型标识符
        };


        public static short AxisX = 1;//轴号
        public static short AxisY = 2;
        public static short AxisZ = 3;
        public static short AxisY2 = 4;

        public static bool Is_Gohome = false;
        /*----------------- 轨迹类型 -start----------------*/
        public const int Path_Start = 1;//起点
        public const int Path_Line = 2;//直线
        public const int Path_End = 3;//终点
        public const int Path_Circle = 4;//圆
        public const int Path_Acr = 5;//圆弧
        /*------------------轨迹类型--end----------------*/

        public static short AixsOnOrOff = 0;//伺服使能状态 0未使能 1已使能
        //轴脉冲位置
        public static double X_Pos = 0;
        public static double Y_Pos = 0;
        public static double Z_Pos = 0;
        public static double Y2_Pos = 0;



        /*----------------- 系统状态 -start----------------*/
        public static short Is_Run = 0;// 0  停止   1 自动运行中   2 回原点中
        /*------------------系统状态--end----------------*/


        /*----------------- 各轴当前状态 -start----------------*/
        public static int X_Status = -1;
        public static int Y_Status = -1;
        public static int Z_Status = -1;
        public static int Y2_Status = -1;
        public static int AxisRunning = 1536;//轴运行中
        public static int AxisOn = 512;//轴已使能
        /*------------------各轴当前状态--end----------------*/


        /*------------------机械参数--start----------------*/
        public static string Xsearch_home = "";
        public static double Xhome_acc = -1;
        public static double Xhome_dec = -1;
        public static double Xhome_vel = -1;
        public static string Xhome_offset = "";

        public static string Ysearch_home = "";
        public static double Yhome_acc = -1;
        public static double Yhome_dec = -1;
        public static double Yhome_vel = -1;
        public static string Yhome_offset = "";

        public static string Y2search_home = "";
        public static double Y2home_acc = -1;
        public static double Y2home_dec = -1;
        public static double Y2home_vel = -1;
        public static string Y2home_offset = "";

        public static string Zsearch_home = "";
        public static double Zhome_acc = -1;
        public static double Zhome_dec = -1;
        public static double Zhome_vel = -1;
        public static string Zhome_offset = "";

        //插补参数
        public static double CB_Vel = -1;
        public static double CB_Acc = -1;
        public static double CB_Dec = -1;
        public static int CB_SmoothTime = -1;
        public static double ZBX_SynVelMax = -1;//XY二维坐标系参数
        public static double ZBX_SynAccMax = -1;
        public static short ZBX_EvenTime = -1;
        public static double CB_Z_Vel = -1;

        //手动参数
        public static double XSD_Vel = 0;
        public static double XSD_Acc = 0;
        public static double XSD_Dec = 0;

        public static double YSD_Vel = 0;
        public static double YSD_Acc = 0;
        public static double YSD_Dec = 0;

        public static double ZSD_Vel = 0;
        public static double ZSD_Acc = 0;
        public static double ZSD_Dec = 0;

        public static double Y2SD_Acc = 0;
        public static double Y2SD_Dec = 0;

        /*------------------机械参数--end----------------*/



        /*--------------报警---------------*/
        public static string AlarmMsg = "";
        public static bool Sound_ShutDowm = true;//是否蜂
        public static bool Alam = false;  //报警



        public static short Authority = -1;

        public static Form1 form1 = null;
        public static TabControl tabControl = null;
        public static TabPage JXCS_tab = null;


        public static int ListViewNum = 0;  //列表序号
        /*************不同系统需要修改的参数 start  ****************************/
        public static double Pulse = 10000;//电机一圈需要的脉冲数
        public static double Distance = 20;//电机一圈走的距离   mm   毫米
        public static int Use_Axis = 3; //当前连接使用的轴数


        public static int Z_Axis_Brake = 5;//Z轴刹车  通用输出点位   1~16    如果没有就填0 

        public static int sleeptime = 30;  //ms   轴硬件反应时间   如果出现轨迹跳点、3轴回原点Z和XY同时运动 的情况，应加大该值

        public static bool One_Extend_Car = true;//是否有一张固高扩展IO板块

        /*************不同系统需要修改的参数 end  ****************************/
        /// <summary>
        /// 1毫米需要的脉冲数
        /// </summary>
        public static double PulsePerCir = Pulse / Distance;
        /// <summary>
        /// 1脉冲走的毫米
        /// </summary>
        public static double CirPerPulse = Distance / Pulse;

        /*************物理输入输出信号 start  ****************************/
        //三色灯、蜂鸣器  输出信号
        public static short Error_Red_Light = 0;
        public static short Error_Sound = 1;
        public static short Nomal_Green_Light = 2;
        public static short Stop_Yellow_Light = 3;
        public static short Axis_Z_Brake = 4; //Z轴刹车   xinzeng

        //输入信号
        public static short Start_Buttom = 0;//启动  输入信号
        public static short Stop_Buttom = 1;//停止 或者急停   输入信号
        public static short Reset_Buttom = 2;//复位   输入信号
        public static short Safe_Door_Buttom = 3;//安全门
        public static short Urgent_Buttom = 4; //急停
        public static short Urgent_Cancel = 5; //急停
        public static short M_A_Buttom = 6; //手自切换
        /*************物理输入输出信号 end  ****************************/


        public static bool Tcpip_IsConnect = false;//TCP/IP与服务端连接状态  网络分析 服务器连接指示灯

        public static DirectoryInfo di = new DirectoryInfo(string.Format(@"{0}..\..\", Application.StartupPath));
        //string path = di.FullName;
        public static string Param_Path = di.FullName + "\\param\\param.txt"; //参数保存路径
        public static string LogFile_Path = di.FullName + "\\logFile\\" + CommUtil.Get_YMD() + ".log";
        public static string Knief_Param_Path = di.FullName + "param\\KniefParam.txt"; //铣刀参数保存路径
        public static string Change_Knife_Pos = di.FullName + "\\param\\Change_Knife_Pos.txt"; //换刀位置保存路径
        public static string Start_Knife_Path = "../param/Start_Knife_Path.txt"; //使用轴保存路径
        //5G罗斯基点位文件位置
        public static string FifthGe_leftTop = Application.StartupPath + "\\5G\\leftTop.txt";
        public static string FifthGe_leftbot = Application.StartupPath + "\\5G\\leftbot.txt";
        public static string FifthGe_rightTop = Application.StartupPath + "\\5G\\rightTop.txt";
        public static string FifthGe_rightBot = Application.StartupPath + "\\5G\\rightBot.txt";
        /***********轨迹运行步骤 start **************/
        public static bool XYZ_First = true;
        public static bool XYZ_Second = false;
        public static bool XYZ_Third = false;
        /***********轨迹运行步骤 end **************/

        //已经发送信息
        public static bool Mrtu_Send = false;

        public static bool Use_Vision = false;//是否使用视觉来定点位


        //加此字段，是因为在关闭项目后，系统中的死循环的线程无法退出，待后续优化
        public static bool isAlive = false;

        public static bool Urgent_Buttom_On = false;
        public static bool Stop_Buttom_On = false;
        public static int Z_Up = 10;//Z轴提刀空跑的点位  是脉冲\
        public static bool auto_Run_On = false;
        public static bool cut_pcb_ing = false;

        //分板机上下游通信实例
        //public static MelsecAscii_TCPIP PLC_top;//上游
        //public static string PLC_topIp = "192.168.1.10";
       public static MelsecAscii_TCPIP PLC_bot;//下游
        public static string PLC_botIp = "192.168.1.11";


        //报警类型   停机、暂停、提醒
        public static string Bussiness_Err_Msg = "";//业务逻辑错误信息
        public static bool Ding = false; //顶升不到位   停机处理
        public static bool Saoma = false; //扫码错误   确定后拿掉即可
        public static bool Duidao = false; //对刀错误   暂停
        public static bool ZhuZhou = false;//旋转主轴错误   停机处理     人不在，如果旋转错误，要停机，否则损坏主轴

        public static string Ddl = "";//对左刀坐标点合集
        public static string Ddr = "";//对右刀坐标点合集

        public static string Dd_Speed_Vel = "10";//对左刀坐标点合集
        public static string Ddr_Speed_Acc= "0.1";//对右刀坐标点合集
        public static string Ddr_Speed_End = "0";//对左刀坐标点合集


        /// <summary>
        /// 
        /// </summary>
        public static int Knife_Cur_Km = 0;//铣刀当前使用里程   mm毫米

        public static bool iskd = false; //开启下刀次数功能  true 为开启  false 为关闭
        public static int Knife_Length = 2;//进给深度,单位 毫米。比如第一段用刀的刀刃铣板，疲劳后，Z轴会下2毫米深度，来铣板



        public static bool Aixs_Z_isGohome = false;

        ///左右主轴启动当前设置状态
        public static bool left_Knife_Switch = true;
        public static bool right_Knife_Switch = true;

        public static bool Safe_Door_Check = false;  //安全门报警 
        public static string open_file_path = "";


        public static bool dui_dao_OK = false;
        public static bool XYZ_duidao = true;

        public static bool xianchen_on = true;
    }
}
