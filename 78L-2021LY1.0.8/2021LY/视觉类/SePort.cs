using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Ports;
using System.Windows.Forms;
using System.Threading;

namespace _2021LY
{
    public  class SePort
    {      
        
        /// <summary>
        /// 定义数据事件，传输信息
        /// </summary>
        public class DataEventArgs : EventArgs
        {
            public string Message { get; private set; }
            public DataEventArgs(string msg)
            {
                this.Message = msg;
            }
        }
        /// <summary>
        /// 串口接收到新数据事件
        /// </summary>
        public event EventHandler<DataEventArgs> NewDataRecived;
        /// <summary>
        /// 串口
        /// </summary>
        public static SerialPort _SerialPort;
        /// <summary>
        /// 通知正在等待的线程已发生事件
        /// </summary>
        private AutoResetEvent _ent; //通知正在等待的线程已发生事件
        /// <summary>
        /// 初始化串口
        /// </summary>
        /// <param name="portName">串口号</param>
        /// <param name="baudRate">波特率</param>
        /// <param name="dataBits">数据位</param>
        /// <param name="stopBits">停止位</param>
        /// <param name="parity">校验位</param>
        public void Se(string portName, int baudRate, int dataBits, string parity, string stopbit)
        {
            try
            {
                this._ent = new AutoResetEvent(true);
                _SerialPort = new SerialPort();
                _SerialPort.DataReceived += _SerialPort_DataReceived;
                if (_SerialPort.IsOpen)
                {
                    _SerialPort.Close();
                }
                
                _SerialPort.BaudRate = baudRate;
                _SerialPort.DataBits = dataBits;
                switch (parity)
                {
                    case"NONE":
                        _SerialPort.Parity = Parity.None;
                        break;
                    case "ODD":
                        _SerialPort.Parity = Parity.Odd;
                        break;
                    case "EVEN":
                        _SerialPort.Parity = Parity.Even;
                        break;
                    case "MARK":
                        _SerialPort.Parity = Parity.Mark;
                        break;
                    case "SPACE":
                        _SerialPort.Parity = Parity.Space;
                        break;
                    default:
                        break;
                }
                switch (stopbit)
                {
                    case "0":
                        _SerialPort.StopBits = StopBits.None;
                        break;
                    case "1":
                        _SerialPort.StopBits = StopBits.One;
                        break;
                    case "1.5":
                        _SerialPort.StopBits = StopBits.OnePointFive;
                        break;
                    case "2":
                        _SerialPort.StopBits = StopBits.Two;
                        break;
                    default:
                        break;
                }
                _SerialPort.PortName = portName;
                _SerialPort.Open();
            }
            catch (Exception)
            {              
                throw;
            }
        }
        /// <summary>
        /// 串口接收数据事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (this._ent.WaitOne(0))
            {
                Thread.Sleep(100);
                //string cmd = _SerialPort.ReadExisting();
                //OnNewDataEvent(new DataEventArgs(cmd));
                //获取缓冲区字节数
                int bytesToRead = _SerialPort.BytesToRead;
                byte[] buffer = new byte[bytesToRead];
                
                //读取缓冲区数据
                _SerialPort.Read(buffer, 0, bytesToRead);
                //数据格式转换
                string cmd = Encoding.Default.GetString(buffer);
               
                OnNewDataEvent(new DataEventArgs(cmd));

            }
        }
        protected virtual void OnNewDataEvent(DataEventArgs e)
        {
            try
            {
                EventHandler<DataEventArgs> handler = this.NewDataRecived;
                if (handler != null)
                {
                    AsyncCallback nn = new AsyncCallback(eventCallBack);
                    handler.BeginInvoke(handler, e, nn, e);//BeginInvoke方法可以使用线程异步地执行委托所指向的方法
                    //BeginInvoke创建的线程都是后台线程
                }
            }
            catch (Exception)
            {
                
                throw;
            }
          
        }
        void eventCallBack(IAsyncResult ar)
        {
            //调用 Set 向 AutoResetEvent 发信号以释放等待线程.AutoResetEvent 将保持终止状态，
            //直到一个正在等待的线程被释放，然后自动返回非终止状态。如果没有任何线程在等待，则状态将无限期地保持为终止状态。
            this._ent.Set();
        }
        /// <summary>
        /// 发送信息
        /// </summary>
        /// <param name="msg"></param>
        public void SendMessage(string msg)
        {
          
            if (_SerialPort.IsOpen)
            {
                _SerialPort.WriteLine(msg);
               
            }
        }
        public static void Senbyte(byte[] data,int offset,int count)
        {
            if (_SerialPort.IsOpen)
            {
                _SerialPort.Write(data, offset, count);
            }          
        }

        public void close()
        { 
         _SerialPort.Close();
        }
    }            
}

