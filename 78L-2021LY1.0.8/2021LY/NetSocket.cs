using _2021LY.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
/// <summary>
/// NetSocket 类   
/// </summary>
namespace _2021LY
{
    class NetSocket
    {
        private Socket socketSend = null;
        private IPAddress ip;
        private int port;
        Task heart_Task;
        public int heart_time = 0; //心跳间隔
        bool OK = false;//心跳信息接收与否

        /// <summary>
        /// "ip" IP地址    "port"端口
        /// </summary>
        public NetSocket(IPAddress _ip, int _port)
        {
            this.ip = _ip;
            this.port = _port;


        }

        /// <summary>
        ///  连接服务器，并启动心跳
        /// </summary>
        public bool getConnect()
        {
            try
            {
                socketSend = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint point = new IPEndPoint(ip, port);
                socketSend.Connect(point);
                NetAnalysisForm.getForm().ShowMsg(" getConnect ()连接成功" + socketSend.RemoteEndPoint.ToString());
                heart_Check_Conn();//连接成功，启动心跳
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("服务器连接失败" + ex.StackTrace);
                return false;
            }
        }
        private bool cut = false;

        public void cutConnect()
        {

            cut = true;
            if (socketSend != null)
            {
                socketSend.Close();
            }
            CommParam.Tcpip_IsConnect = false;

        }

        //接收
        public void Recive()
        {
            var NetSocket_recive_Task = Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    byte[] buffer = new byte[1024 * 1024 * 1];

                    if (!IsSocketConnected(socketSend))
                    {
                        NetAnalysisForm.getForm().ShowMsg("连接断开，跳出接收");//如果连接断开，则跳出
                        break;
                    }


                    int r = 0;
                    try
                    {
                        r = socketSend.Receive(buffer);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.StackTrace);
                    }
                    if (r == 0) break;//实际接收的有效字节数
                    string str = Encoding.UTF8.GetString(buffer, 0, r);
                    NetAnalysisForm.getForm().ShowMsg("接收消息:" + str);
                    if (String.CompareOrdinal("STATUS:NONE", str) == 0)//如果是心跳信息，则发往心跳
                    {
                        Console.WriteLine("心跳接收" + str);
                        OK = true;
                    }
                    else
                    {
                        NetAnalysisForm.getForm().get_Msg_From_Server(str);
                    }
                }
            });
        }


        //发送

        public bool sendMsg(string msg)
        {
            try
            {
                byte[] buff = Encoding.UTF8.GetBytes(msg);
                socketSend.Send(buff);
                NetAnalysisForm.getForm().ShowMsg("发送消息:" + msg);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("发送失败" + ex.StackTrace);
                return false;
            }
        }

        //心跳检查
        private void heart_Check_Conn()
        {
            Thread t = new Thread(() =>
            // {
            //     Do_Dido_Action();
            // });



            // heart_Task = Task.Factory.StartNew(() =>
            {
            while (true && !cut)
                {
                    sendMsg("QUERY");
            System.Threading.Thread.Sleep(heart_time);
            if (!OK)//到了心跳时间，仍然没收到服务器的心跳反馈信息，则报错提示处理
            {
                //提示报错
                NetAnalysisForm.getForm().ShowMsg("连接已断开");
                CommParam.Tcpip_IsConnect = false;
                //重新连接
                NetAnalysisForm.getForm().ShowMsg("自动连接中.....");
                var get_Conn_Again_Task = Task.Factory.StartNew(() =>
                {
                    get_Conn_Again();
                });
                break;//跳出循环
            }
            else //如果收到了正确的反馈
            {
                OK = false;
            }
        }
    });
            t.IsBackground = true;
            t.Start();
        }

int again_num = 0;//重连次数

private bool get_Conn_Again()
{
    while (true)
    {
        again_num++;
        NetAnalysisForm.getForm().ShowMsg("重复第" + again_num + "次连接中......");
        if (getConnect())
        {
            this.Recive();
            again_num = 0;
            CommParam.Tcpip_IsConnect = true;
            return true;
        }
        if (again_num == 10)
        {
            again_num = 0;
            NetAnalysisForm.getForm().ShowMsg("连接失败，请检查网络");
            return false; ;
        }
    }


}



private bool IsSocketConnected(Socket socket)
{
    lock (this)
    {
        bool ConnectState = true;
        bool state = socket.Blocking;
        try
        {
            byte[] temp = new byte[1];
            socket.Blocking = false;
            socket.Send(temp, 0, 0);
            ConnectState = true;
        }
        catch (SocketException e)
        {
            if (e.NativeErrorCode.Equals(10035)) //仍然是connect的
                ConnectState = true;
            else
                ConnectState = false;
        }
        finally
        {
            socket.Blocking = state;
        }
        return ConnectState;
    }
}





//废弃
private bool get_Conn_Again1()
{
    this.socketSend.Close();
    this.socketSend = null;
    NetSocket ns = new NetSocket(ip, port);
    again_num++;
    NetAnalysisForm.getForm().ShowMsg("重复第" + again_num + "次连接中......");
    if (!getConnect())
    {
        if (again_num == 10)
        {
            again_num = 0;
            NetAnalysisForm.getForm().ShowMsg("连接失败，请检查网络");
            return false;
        }
        get_Conn_Again1();
    }
    else
    {
        NetAnalysisForm.getForm().ShowMsg("连接成功" + socketSend.RemoteEndPoint.ToString());

        again_num = 0;
        return true;
    }
    return false;
}






    }
}
