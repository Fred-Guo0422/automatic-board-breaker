using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sunny.UI;
using CMESAPI;
using System.Xml;
using System.IO;
using System.Diagnostics;
namespace _2021LY.Forms
{
    public partial class MES : UITitlePage
    {
     public static  byte[] Sendbytes = { 0x4c, 0x4f, 0x4e, 0x0d };
     public static byte[] Stopbytes = { 0x4C, 0x4F, 0x46, 0x46, 0x0d };
     public static String ID;
     public  delegate void Log(string Mess);    //声明一个委托

     public static MES MS;//声明个静态，避免控件使用静态定义,创建构造函数
     public static string []SN = new string[16];

     public static string[] SnResult = new string[17];
     SePort Sesetup = new SePort();

     Label[] label = new Label[16];
        Stopwatch TT = new Stopwatch();
        public MES()
        {
           // this.Load += MESForm_Load;
           // this.FormClosing += MESForm_Close;
            InitializeComponent();
            MS = this;
            try
            {


                TT.Reset();
                TT.Start();
                Seriport("COM2", 115200, 8, "EVEN", "1");
                TT.Stop();
               
                Addmessage(TT.ElapsedMilliseconds+"串口打开成功！");

                for (int i = 0; i < 16; i++)
                {
                    string le = "label" + (i + 1);
                    label[i] = (Label)this.Controls.Find(le, true)[0];
                }
            }
            catch (Exception ex)
            {
                Addmessage(ex.Message + "串口打开失败！");
            }
           
        }

        public static MES MESID;
        public static MES getForm()
        {
            if (MESID == null)
            {
                MESID = new MES();
            }
            return MESID;
        }
        private void MESForm_Load(object sender, EventArgs e)
        {
            try
            {
                
                Seriport("COM1", 115200, 8, "EVEN", "1");
                Addmessage("串口打开成功！");

                for (int i = 0; i < 16; i++)
                {
                    string le = "label" + (i + 1);
                    label[i] = (Label)this.Controls.Find(le, true)[0];
                }
            }
            catch (Exception ex)
            {
                Addmessage(ex.Message + "串口打开失败！");
            }                
        }


       
      
        /// <summary>
        /// 控制调用方法
        /// </summary>
        public static string[] ReadID()
        {
            Array.Clear(SnResult, 0, 17);

            if (!MS.MesCheck.Checked)        //MES关闭，默认全部OK
	        {
                //执行个方法传出去SnResult[];   
                for (int i = 0; i < 16; i++)
                {
                    SnResult[i] = "1"; //1直接做下去,都是OK;
                }
                //SnResult[16] = "a";
                return SnResult;
            }
          
            Log method = Addmessage;
         
            try
            {
               
                MS.SnTextBox.Text = null;
                ID = null;
                try
                {
                   
                    SePort.Senbyte(Sendbytes, 0, 4);
                   
                }
                catch (Exception ex)
                {
                    for (int i = 0; i < 16; i++)
                    {
                        SnResult[i] = "3"; //3,报警;
                    }
                    SnResult[16] = "null";
                    SePort.Senbyte(Stopbytes, 0, 5);
                    method.Invoke(ex.Message + "扫码指令出错");
                    return SnResult;
                }
               
                System.Threading.Thread.Sleep(2000);
                MS.SnTextBox.Text = ID;
                
                if (ID!=null)

                {
                    method.Invoke("大板二维码;"+ID);
                        //由大码获取每个小码结果                  
                    if ( GetBlockSNs(ID)==1 && CallMESRouteCheck(SN)==1)
                    {
                        //执行个方法传出去SnResult[];   1是OK，2是NG；
                        SnResult[16] = ID;
                        return SnResult;
                    }
                    else
                    {
                        //执行个方法传出去SnResult[];   3报警；
                        for (int i = 0; i < 16; i++)
                        {
                            SnResult[i] = "3"; //
                        }
                        SnResult[16] = ID;
                        return SnResult;

                    }
                }
               else
                {
                    SePort.Senbyte(Stopbytes, 0, 5);
                  
                    method.Invoke("大板二维码读取失败");
                    for (int i = 0; i < 16; i++)
                    {
                        SnResult[i] = "3"; //
                    }
                    SnResult[16] = "null";
                    return SnResult;
                    //执行个方法传出去SnResult[];   3报警；
                }
               
            }
            catch (Exception ex)
            {
                //执行个方法传出去SnResult[];   3报警；

                for (int i = 0; i < 16; i++)
                {
                    SnResult[i] = "3"; //3,报警;
                }
                SnResult[16] = "null";
                SePort.Senbyte(Stopbytes, 0, 5);
                method.Invoke(ex.Message + "绑定MES流程出错");
                return SnResult;
               
            }
        }
        //串口参数配置
        private void 手动读码(object sender, EventArgs e)
        {
            SePort.Senbyte(Sendbytes, 0, 4);
        }
        private void 停止读码(object sender, EventArgs e)
        {

            SePort.Senbyte(Stopbytes, 0, 5);
        }
        public  void Seriport(string portName, int baudRate, int dataBits, string parity, string stopbit)
        {
            try
            {
                Sesetup.Se(portName, baudRate, dataBits, parity, stopbit);
                // Seri1.Se(Camera.param.seriaparam.m_ComName, Camera.param.seriaparam.m_ComBaudRate, Camera.param.seriaparam.m_ComDataBits, Camera.param.seriaparam.m_ComParity, Camera.param.seriaparam.m_ComStopBit);
                Sesetup.NewDataRecived += mCom_NewDataRecived;
            }
            catch (Exception)
            {

                throw;
            }
          
        }
        public void mCom_NewDataRecived(object sender, SePort.DataEventArgs e)
        {
            try
            {
                System.Threading.Thread.Sleep(300);
                string str = e.Message.Trim();
                ID = str;
                BeginInvoke(new MethodInvoker(delegate
                {
                    Addmessage("串口接收" + str);
                }
                 ));
            }
            catch (Exception ex)
            {

                BeginInvoke(new MethodInvoker(delegate
                {
                    Addmessage("Com接收信息:" + ex.Message);
                }));
            }
        }

        private static  void Addmessage(String message)
        {
            MS.richTextBox1.Focus();
            MS.richTextBox1.Select(MS.richTextBox1.Text.Length, 0);
            MS.richTextBox1.ScrollToCaret();
            MS.richTextBox1.AppendText(System.DateTime.Now.ToString("HH时mm分ss秒") + "-" + message + "\n");
            if (MS.richTextBox1.Lines.Length > 1000)
            {
                MS.richTextBox1.Clear();
            }            
        }

        /*******************从大码获取小码*************************/
        /// <summary>
        /// Sample Testing Method 
        /// </summary>
        /// <param name="PanelSN"></param>
        public static int GetBlockSNs(string PanelSN)
        {
            try
            {
                MS.uiListBox1.Items.Clear();
                //Invoking API
                /// <summary>
                ///  GetBlockSerialNumbers
                /// </summary>
                /// <param name="PanelSN"> Mandatory Input</param>
                /// <param name="CheckRoute"> Optional Default true</param>
                cSelcompAPI api = new cSelcompAPI();
                Dictionary<string, string> dictBlocks = api.GetBlockSerialNumbers(PanelSN);

                // Checking Api Blocks Count
                if (dictBlocks.Count() > 0)
                {
                    foreach (string Key in dictBlocks.Keys)
                    {
                        // Printing Sequence and SN
                        Addmessage("Sequence :" + Key + " = SN :" + dictBlocks[Key]);
                        MS.uiListBox1.Items.Add("Sequence :" + Key + " = SN :" + dictBlocks[Key]);
                        SN[int.Parse(Key)-1] = dictBlocks[Key].ToString();
                    }
                    return 1;
                }
                else
                {
                    // Error Message
                    Addmessage("大码获取小码错误:" + api.Message);
                    return -1;
                }
            }
            catch (Exception ex)
            {
                Addmessage(ex.Message+"大码获取小码错误:");
                return -1;
            }      
        }

        /*******************8检查小码*************************/
        private static int CallMESRouteCheck(string[]SerialNumber)
        {
            try
            {
               
                bool Response = false;
                cSelcompAPI api = new cSelcompAPI(); // Initialization 
                int Number = 0;
                foreach (var SnNumber in SerialNumber)
                {
                    MS.label[Number].BackColor = Color.White;
                    Response = api.CheckRouteForSerialNumber(SnNumber, api.EquipmentID);
                     if (Response==true)   //OK
                     {
                         //SnResult[Number] = "1";
                         
                        if (Save(SnNumber, Number)==1)
                        {
                            SnResult[Number] = "1";  //OK           //检查和上传都OK，为绿色
                            MS.label[Number].BackColor = Color.Green;
                        }
                        else
                        {
                            SnResult[Number] = "2";  //ng            //检查OK和上传NG，为黄色
                            MS.label[Number].BackColor = Color.Yellow;
                        }

                    }  
                    else                //NG
                     {
                        MS.label[Number].BackColor = Color.Red;      //检查小码错误，为红色
                        SnResult[Number] = "2";
                        string ErrorMsg = api.Message; //Error msg 
                        Addmessage("第"+Number+"NG"+ErrorMsg);
                     }

                    Number++;
                }
               
                return 1;              
            }
            catch (Exception)
            {

                Addmessage("检查小码出错");
                return -1;
               
            }          
        }

        /*******************保存XML**********************/
        public static int Save(string Code,int Nm)
        {

            try
            {
                string Time = System.DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fff");

                XmlDocument doc = new XmlDocument();

                XmlDeclaration xmldecl = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
                XmlElement root = doc.DocumentElement;
                doc.InsertBefore(xmldecl, root);

                XmlElement ele = doc.CreateElement("vCheckTester");
                ele.SetAttribute("xmlns", "Valor.vCheckTester.xsd");
                ele.SetAttribute("Version", "4.0");
                ele.SetAttribute("xmlns:xsd", "http://www.w3.org/2001/XMLSchema");
                ele.SetAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
                doc.AppendChild(ele);

                XmlElement row = doc.CreateElement("Unit");
                row.SetAttribute("StatusCode", "PASS");
                row.SetAttribute("SerialNumber", Code);
                row.SetAttribute("Timestamp", Time);

                XmlElement custmonerId = doc.CreateElement("Measurement");
                custmonerId.SetAttribute("StatusCode", "PASS");
                custmonerId.SetAttribute("Name", "Goole");
                custmonerId.SetAttribute("Value", "0");
                custmonerId.SetAttribute("ValueUnit", "Double");
                custmonerId.SetAttribute("UpperLimit", "0");
                custmonerId.SetAttribute("LowLimit", "0");
                custmonerId.SetAttribute("MeasurementUnit", "S");
                custmonerId.SetAttribute("DateTime", Time);
                row.AppendChild(custmonerId);

                XmlElement custmonerId2 = doc.CreateElement("Header");
                custmonerId2.SetAttribute("TestFixtureNumber", null);
                custmonerId2.SetAttribute("TestHeadType", "0");
                row.AppendChild(custmonerId2);

                ele.AppendChild(row);

                if (MS.Is保存Log.Checked)
                {
                    doc.Save("D:\\Up\\" + Code + ".log");
                    return 1;   ///上传OK
                }

                /*******XML转化为String*************/
                MemoryStream streamXml = new MemoryStream();
                XmlTextWriter writer = new XmlTextWriter(streamXml, Encoding.UTF8);
                writer.Formatting = Formatting.Indented;
                doc.Save(writer);

                StreamReader reader = new StreamReader(streamXml, Encoding.UTF8);
                streamXml.Position = 0;
                string Content = reader.ReadToEnd();
                reader.Close();
                streamXml.Close();


                /************上传***********/
              
                if (!MS.Is保存Log.Checked)
                {
                    cSelcompAPI api = new cSelcompAPI(); // Initialization 
                    bool Result = api.SaveResult(Content);
                    if (Result)
                    {
                       // Addmessage("第" + (Nm+1) + "上传OK");
                        return 1;   ///上传OK
                    }
                    else
                    {
                        string ErrorMsg = api.Message;

                       // Addmessage("第" + (Nm + 1) + "上传NG");

                        return -1;   ///上传NG
                    }
                }

                return 1;

            }
            catch (Exception ex)
            {
                Addmessage(ex.Message+"上传结果出错");
                return -1;   ///报警
            }
                   
        }

    }  
}
