using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
namespace _2021LY.Forms
{
    public partial class MainForm : UIHeaderAsideMainFrame
    {

        public MainForm()
        {
           LoadVisionVpp();
            InitializeComponent();

            #region/********不能重复打开多个EXE**********/
            bool isNotRunning;  //互斥体判断
            System.Threading.Mutex instance = new System.Threading.Mutex(true, "MutexName", out isNotRunning);   //同步基元变量
            if (!isNotRunning)  // 如果不是未运行状态
            {
                MessageBox.Show("程序已在运行，请勿重新打开");
                Environment.Exit(1);
            }
            ShowAskDialog("欢迎使用自动分板软件");
           // MessageBox.Show("欢迎使用自动分板软件");

            //bool isAppRunning = false;
            ////设置一个名称为进程名的互斥体
            //Mutex mutex = new Mutex(true, System.Diagnostics.Process.GetCurrentProcess().ProcessName, out isAppRunning);
            //if (!isAppRunning)
            //{
            //    MessageBox.Show("程序已运行，不能再次打开！");
            //    Environment.Exit(1);
            //}
            //MessageBox.Show("欢迎使用本软件");

            #endregion

            CommParam.isAlive = true;
            this.Load += MainForm_Load;
            int pageIndex = 1000;
            //Header.SetNodePageIndex(Header.Nodes[0], pageIndex);
            //Header.SetNodeSymbol(Header.Nodes[0], 61451);
            //创建节点，给对应的page标号为pageIndex
            TreeNode sys_param = Aside.CreateNode("机械设计", 61451, 24, pageIndex);
            Aside.CreateChildNode(sys_param, 61640, 24, AddPage(IOForm.getForm(), ++pageIndex));
            Aside.CreateChildNode(sys_param, 61640, 24, AddPage(BusinessForm.getForm(), ++pageIndex));
            Aside.CreateChildNode(sys_param, 61640, 24, AddPage(AxisForm.getForm(), ++pageIndex));
            Aside.CreateChildNode(sys_param, 61640, 24, AddPage(DesignForm.getForm(), ++pageIndex));
            TreeNode vision = Aside.CreateNode("视觉设计", 61451, 24, pageIndex);
            Aside.CreateChildNode(vision, 61640, 24, AddPage(VisionDesignForm.getForm(), ++pageIndex));
            Aside.CreateChildNode(vision, 61640, 24, AddPage(VisionCalibra.getForm(), ++pageIndex));
            Aside.CreateChildNode(vision, 61640, 24, AddPage(VisionPosition.getForm(), ++pageIndex));
            //TreeNode outside = Aside.CreateNode("外围设备", 61451, 24, pageIndex);
            //Aside.CreateChildNode(outside, 61640, 24, AddPage(NetAnalysisForm.getForm(), ++pageIndex));
            //Aside.CreateChildNode(outside, 61640, 24, AddPage(FifthGeneratiaonForm.getForm(), ++pageIndex));
            TreeNode MESID = Aside.CreateNode("MES设计", 61451, 24, pageIndex);
            Aside.CreateChildNode(MESID, 61840, 24, AddPage(MES.getForm(), ++pageIndex));


        }

        //配置系统启动需要提前加载的事件
        private void MainForm_Load(object sender, EventArgs e)
        {
            Ini_Card();
            Control.CheckForIllegalCrossThreadCalls = false;
        }


        //初始化固高控制板卡
        private void Ini_Card()
        {
            string msg = "";
            ControlCard cc = new Goog();
            try
            {
                if (!cc.Initial_Card(out msg))
                {
                    Logger.MessageShow(msg);
                    return;
                }
                Console.WriteLine(" //MainForm_FormClosing初始化固高控制板卡");
            }
            catch (Exception x)
            {
                LogHelper.WriteErrorLog(" 系统初始化运动控制卡操作异常 ", x);
               // Logger.Recod_Log_File("form1 MainForm_Load  " + x.Message + "\r\n x.StackTrace" + x.StackTrace);
                Logger.MessageShow(" 系统初始化运动控制卡 操作异常");
            }
        }
        //复位重置系统
        public void reset_System()
        {

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                CommParam.isAlive = false;
                Goog.Reset();
                Goog.Close();
                Goog.GT_CloseExtM();
                Console.WriteLine(" //MainForm_FormClosing释放固高控制板卡");
                if (CommParam.PLC_bot != null)
                {

                 CommParam.PLC_bot.Write_D_short("D280", 1);
                }
                Environment.Exit(0);
                Process.GetCurrentProcess().Kill();
            }
            catch (Exception x)
            {
            }
        }
        public void LoadVisionVpp()
        {

            Vision.LoadVpp();  //加载Vpp程序
        }
    }
}
