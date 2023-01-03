//using _2021LY.Forms;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.IO.Ports;
//using System.Linq;
//using System.Text;
//using System.Threading;

//namespace _2021LY
//{   
//    class Mrtu
//    {

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="com">端口</param>
//        /// <param name="BaudRate">波特率</param>
//        /// <param name="stopbits">停止位</param>
//        /// <param name="parity">校验方式</param>
//        /// <param name="databites">数据位</param>
//        public Mrtu(string com, int BaudRate, StopBits stopbits, Parity parity, int databites)
//        {

//            try
//            {
//                _Portname = com.ToUpper();
//                _BaudRate = BaudRate;
//                _StopBites = stopbits;
//                _Parity = parity;
//                _Databites = databites;
//                string[] _allcom = SerialPort.GetPortNames();
//                for (int i = 0; i < _allcom.Length; i++)
//                {
//                    _allcom[i] = _allcom[i].ToUpper();
//                }
//                _switch1 = ((IList)_allcom).Contains(_Portname); //判断COM口是否存在。
//                _SerialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(_serportRece);
//                if (!_SerialPort.IsOpen)
//                {
//                    _SerialPort.Open();
//                }
//            }
//            catch (Exception ex)
//            {


//            }
//        }
//        private void _serportRece(object sender, SerialDataReceivedEventArgs e)
//        {


//            //MessageBox.Show("Test");
//        }

//        /// <summary>
//        /// 端口名称
//        /// </summary>
//        public string _Portname { get; set; }
//        /// <summary>
//        /// 波特率
//        /// </summary>
//        public int _BaudRate { get; set; }
//        /// <summary>
//        /// 停止位
//        /// </summary>
//        public StopBits _StopBites { get; set; }
//        /// <summary>
//        /// 校验位
//        /// </summary>
//        public Parity _Parity { get; set; }
//        /// <summary>
//        /// 数据位
//        /// </summary>
//        public int _Databites { get; set; }
//        /// <summary>
//        /// 开关1判断是否存在COM口
//        /// </summary>
//        public bool _switch1 = false;
//        private SerialPort _SerialPort1 = new SerialPort();
//        private SerialPort _SerialPort
//        {
//            get
//            {
//                try
//                {
//                    if (!_SerialPort1.IsOpen)//判断端口是否打开
//                    {
//                        _SerialPort1.DataBits = _Databites;
//                        _SerialPort1.BaudRate = _BaudRate;
//                        _SerialPort1.PortName = _Portname;
//                        _SerialPort1.StopBits = _StopBites;
//                        _SerialPort1.Parity = _Parity;
//                        _SerialPort1.Open();
//                        return _SerialPort1;
//                    }
//                    string[] _allcom = SerialPort.GetPortNames();


//                    for (int i = 0; i < _allcom.Length; i++)
//                    {
//                        _allcom[i] = _allcom[i].ToUpper();
//                    }

//                    _switch1 = ((IList)_allcom).Contains(_Portname.ToUpper()); //判断COM口是否存在。


//                }
//                catch (Exception ex)
//                {

//                }
//                return _SerialPort1;
//            }
//        }

//        public void closeCom()
//        {
//            _SerialPort1.Close();
//        }


//        public bool  _wuint16(int _addr, byte _stand, UInt16 _data)
//        {
//            bool _successful = false;
//            try
//            {
//                if (_switch1)
//                {
//                    byte[] _recebyte = new byte[1000];
//                    byte _addrH = 0;
//                    byte _addrL = 1;
//                    byte[] sedbyte = new byte[20];
//                    byte[] _addrbyte = BitConverter.GetBytes(_addr);
//                    if (_addr > 0xFF)
//                    {
//                        _addrL = _addrbyte[_addrbyte.Length - 3];
//                        _addrH = _addrbyte[_addrbyte.Length - 4];
//                    }
//                    if (_addr <= 0xFF)
//                    {
//                        _addrL = 0;
//                        _addrH = _addrbyte[0];
//                    }
//                    UInt16 _y = Convert.ToUInt16(_data);
//                    byte[] intBuff = BitConverter.GetBytes(_y);
//                    sedbyte[0] = _stand;
//                    sedbyte[1] = 0x06;
//                    sedbyte[2] = _addrL;
//                    sedbyte[3] = _addrH;
//                    sedbyte[4] = intBuff[1];
//                    sedbyte[5] = intBuff[0];
//                    sedbyte[6] = _crcchecking(sedbyte, 0, 6)[0];
//                    sedbyte[7] = _crcchecking(sedbyte, 0, 6)[1];
//                    _SerialPort.Write(sedbyte, 0, 8);//发送报文
//                    CommParam.Mrtu_Send = true;
//                    FifthGeneratiaonForm.getForm().Get_Status();
//                    int _reclen = _SerialPort.BytesToRead;
//                    _SerialPort.Read(_recebyte, 0, _reclen);
//                    if (_reclen > 5)
//                    {
//                        byte reccrcL = _recebyte[_reclen - 2];//取接收到的报文  校验码低位
//                        byte reccrcH = _recebyte[_reclen - 1];//取校验码高位
//                        byte[] crc = _crcchecking(_recebyte, 0, (uint)_reclen - 2);//重新校验报文
//                        if (reccrcL == crc[0] && reccrcH == crc[1])//比对校验码
//                        {
//                            byte fun = _recebyte[1];//功能码
//                            if (fun == 0x06)//modbus03功能码
//                            {
//                                _successful = true;
//                            }
//                        }

//                    }
//                }
//            }

//            catch
//            {


//            }
//            return _successful;
//        }


//        public byte[] _crcchecking(byte[] instructions, uint start, uint length)
//        {
//            uint i, j;
//            uint crc16 = 0xFFFF;//crc寄存器赋初值
//            try
//            {
//                length = length + start;
//                for (i = start; i < length; i++)
//                {
//                    crc16 ^= instructions[i];
//                    for (j = 0; j < 8; j++)
//                    {
//                        if ((crc16 & 0x01) == 1)
//                        {
//                            crc16 = (crc16 >> 1) ^ 0xA001;
//                        }
//                        else
//                        {
//                            crc16 = crc16 >> 1;
//                        }
//                    }
//                }
//                //   UInt16 X = (UInt16)(crc16 *256);
//                // UInt16 Y = (UInt16)(crc16/256);
//                //crc16 = (UInt16)(X ^ Y);
//            }
//            catch
//            {


//            }
//            return BitConverter.GetBytes(crc16);
//        }
//        /// <summary>
//        /// 读取无符号16位数据
//        /// </summary>
//        /// <param name="_addr">MODBUS地址</param>
//        /// <param name="_stand">站号</param>
//        /// <param name="_len">读取长度</param>
//        /// <returns></returns>
//        public UInt16[] _ruint16(int _addr, byte _stand, int _len)
//        {
//            UInt16[] _s = new UInt16[_len];
//            byte[] _send = new byte[20];
//            try
//            {
//                byte[] _addrbyte = BitConverter.GetBytes(_addr);
//                if (_switch1)
//                {
//                    _send[0] = _stand;
//                    _send[1] = 0x03;
//                    _send[2] = _bytearr(_addr)[0];
//                    _send[3] = _bytearr(_addr)[1];
//                    _send[4] = _bytearr(_len)[0];
//                    _send[5] = _bytearr(_len)[1];
//                    _send[6] = _crcchecking(_send, 0, 6)[0];
//                    _send[7] = _crcchecking(_send, 0, 6)[1];
//                    _SerialPort.Write(_send, 0, 8);//发送报文
//                    Thread.Sleep(15); 
//                    int _reclen = _SerialPort.BytesToRead;
//                    byte[] _rec = new byte[_reclen];
//                    if (_reclen > 5)
//                    {
//                        _SerialPort.Read(_rec, 0, _reclen);
//                        byte reccrcL = _rec[_reclen - 1];//取接收到的报文  校验码低位
//                        byte reccrcH = _rec[_reclen - 2];//取校验码高位
//                        byte[] crc = _crcchecking(_rec, 0, (uint)_reclen - 2);//重新校验报文
//                        if (reccrcL == crc[1] && reccrcH == crc[0])//比对校验码
//                        {
//                            byte fun = _rec[1];//功能码
//                            if (fun == 0x03)//modbus03功能码
//                            {
//                                byte databyteNum = _rec[2];//接收字节数
//                                for (int i = 0; i < databyteNum-1; i++)
//                                {
//                                    _s[i] = (UInt16)(_rec[i * 2 + 3] << 8 | _rec[i * 2 + 4]);
//                                }
//                            }
//                        }
//                    }

//                }
//            }
//            catch (Exception ex)
//            {

//                Console.WriteLine(ex.StackTrace);
//            }
//            return _s;
//        }

//        private byte[] _bytearr(int _data)
//        {
//            byte[] _s = new byte[2];
//            try
//            {
//                byte[] s1 = BitConverter.GetBytes(_data);
//                if (_data > 0xFF)
//                {
//                    _s[0] = s1[s1.Length - 3];
//                    _s[1] = s1[s1.Length - 4];
//                }
//                if (_data <= 0xFF)
//                {
//                    _s[0] = 0;
//                    _s[1] = s1[0];
//                }
//            }
//            catch (Exception ex)
//            {


//            }
//            //  s = BitConverter.GetBytes(_data);
//            return _s;
//        }

//    }
//}
