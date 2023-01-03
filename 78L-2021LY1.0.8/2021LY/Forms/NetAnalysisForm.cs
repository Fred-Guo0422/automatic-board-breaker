using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2021LY.Forms
{
    public partial class NetAnalysisForm : UITitlePage
    {
        public NetAnalysisForm()
        {
            InitializeComponent();
            this.Text = "网络分析";
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private static NetAnalysisForm netAnalysisForm;

        public static NetAnalysisForm getForm()
        {
            if (netAnalysisForm == null)
            {
                netAnalysisForm = new NetAnalysisForm();
            }
            return netAnalysisForm;
        }
        NetSocket ns;
        private void connect_server_Click(object sender, EventArgs e)
        {
            IPAddress ip = IPAddress.Parse(server_ip.Text);
            int port = Convert.ToInt32(server_port.Text);
            ns = new NetSocket(ip, port);
            ns.heart_time = Convert.ToInt32(heart_time.Text);//设置心跳时间
            if (ns.getConnect())
            {
                ns.Recive();
                CommParam.Tcpip_IsConnect = true;
            }
            else
            {
                Logger.MessageShow("连接失败!请检查网络!");
                return;
            }
        }
        //接收到网分给的数据，进行逻辑处理
        public void get_Msg_From_Server(string msg)
        {
            //处理信息后进行相应动作处理
            FifthGeneratiaonForm.getForm().GetNetInfo_DoMoto(msg);
            //输出到监控页面
           ShowMsg(msg);
        }

        //显示在textBox中
        public void ShowMsg(string str)
        {
            cs_msg.AppendText(CommUtil.Get_YMDHMS() + " " + str + "\r\n");
        }

        private void cut_server_Click(object sender, EventArgs e)
        {
            ns.cutConnect();
        }

    }
}
