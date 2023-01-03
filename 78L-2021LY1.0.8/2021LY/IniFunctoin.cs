using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace _2021LY
{
    public class IniFunctoin
    {
        //声明读写INI文件的API函数
        [DllImport("kernel32")]
        private static extern bool WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, byte[] retVal, int size, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        //[DllImport("kernel32")]
        //private static extern long WritePrivateProfileString(string sectionName, string key, string value, string filePath);

        //类的构造函数，传递INI文件名


        /// <summary>
        /// 移除指定的section
        /// 说明：key参数传入null就为移除指定的section。
        /// 
        /// </summary>
        /// <param name="sectionName">section名称</param>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        public object RemoveSection(string sectionName, string filePath)
        {
            bool rs = WritePrivateProfileString(sectionName, null, "", filePath);
            return rs;
        }
        /// <summary>
        /// 移除指定的key
        /// 说明：value参数传入null就为移除指定的key
        /// </summary>
        /// <param name="sectionName">section名称</param>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        public object Removekey(string sectionName, string key, string filePath)
        {
            bool rs = WritePrivateProfileString(sectionName, key, null, filePath);
            return rs;
        }
        /// <summary>
        /// 写String
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Ident"></param>
        /// <param name="Value"></param>
        /// <param name="iniFile"></param>
        /// <returns></returns>
        public object WriteStringToIni(string Section, string Key, string Value, string iniFile)
        {
            if (!WritePrivateProfileString(Section, Key, Value, iniFile))
            {
                //throw (new ApplicationException("写Ini文件出错"));
                return "写Ini文件出错";
            }
            return "写Ini文件成功";
        }

        /// <summary>
        /// 读取INI文件指定
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <param name="iniFile"></param>
        /// <returns></returns>
        public string ReadIniString(string Section, string Key, string iniFile)
        {
            StringBuilder temp = new StringBuilder(255);
            GetPrivateProfileString(Section, Key, "", temp, 255, iniFile);
            return temp.ToString();
        }


        /// <summary>
        /// 从Ini文件中，读取所有的Sections的名称
        /// </summary>
        /// <param name="SectionsFileName"></param>
        /// <returns></returns>
        public List<string> ReadAllSectionsName(string FilePath)
        {
            StringCollection SectionList = new StringCollection();//从Ini文件中，读取所有的Sections的名称
            //Note:必须得用Bytes来实现，StringBuilder只能取到第一个Section
            byte[] Buffer = new byte[65535];
            int bufLen = 0;
            bufLen = GetPrivateProfileString(null, null, null, Buffer,
            Buffer.GetUpperBound(0), FilePath);

            SectionList.Clear();
            if (bufLen != 0)
            {
                int start = 0;
                for (int i = 0; i < bufLen; i++)
                {
                    if ((Buffer[i] == 0) && ((i - start) > 0))
                    {
                        String s = Encoding.GetEncoding(0).GetString(Buffer, start, i - start);
                        SectionList.Add(s);
                        start = i + 1;
                    }
                }
            }

            List<string> Result = new List<string>();

            foreach (string value in SectionList)
            {
                Result.Add(value);
            }
            return Result;
        }

        /// <summary>
        /// 读取指定的Section的所有 key 到列表中，
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Values"></param>
        /// <param name="splitString"></param>
        public List<string> ReadSectionAllKey(string Section, string iniFile)
        {
            StringCollection KeyList = new StringCollection();
            NameValueCollection Values = new NameValueCollection();

            Byte[] Buffer = new Byte[16384];
            //Idents.Clear();

            int bufLen = GetPrivateProfileString(Section, null, null, Buffer, Buffer.GetUpperBound(0), iniFile);
            //对Section进行解析
            KeyList.Clear();
            if (bufLen != 0)
            {
                int start = 0;
                for (int i = 0; i < bufLen; i++)
                {
                    if ((Buffer[i] == 0) && ((i - start) > 0))
                    {
                        String s = Encoding.GetEncoding(0).GetString(Buffer, start, i - start);
                        KeyList.Add(s);
                        start = i + 1;
                    }
                }
            }

            Values.Clear();
            foreach (string key in KeyList)
            {
                Byte[] RedBuffer = new Byte[65535];
                int RedbufLen = GetPrivateProfileString(Section, key, "", RedBuffer, RedBuffer.GetUpperBound(0), iniFile);
                //必须设定0（系统默认的代码页）的编码方式，否则无法支持中文
                string s = Encoding.GetEncoding(0).GetString(RedBuffer);
                s = s.Substring(0, RedbufLen);

                Values.Add(key, s.Trim());
            }

            return Values.AllKeys.ToList();
        }

        /// <summary>
        /// 读取指定的Section的所有Value到列表中，
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Values"></param>
        /// <param name="splitString"></param>
        public List<string> ReadSectionAllValue(string Section, string iniFile)
        {
            StringCollection KeyList = new StringCollection();
            NameValueCollection Values = new NameValueCollection();

            Byte[] Buffer = new Byte[16384];
            //Idents.Clear();

            int bufLen = GetPrivateProfileString(Section, null, null, Buffer, Buffer.GetUpperBound(0), iniFile);
            //对Section进行解析
            KeyList.Clear();
            if (bufLen != 0)
            {
                int start = 0;
                for (int i = 0; i < bufLen; i++)
                {
                    if ((Buffer[i] == 0) && ((i - start) > 0))
                    {
                        String s = Encoding.GetEncoding(0).GetString(Buffer, start, i - start);
                        KeyList.Add(s);
                        start = i + 1;
                    }
                }
            }

            Values.Clear();
            List<string> Value = new List<string>();
            foreach (string key in KeyList)
            {
                Byte[] RedBuffer = new Byte[65535];
                int RedbufLen = GetPrivateProfileString(Section, key, "", RedBuffer, RedBuffer.GetUpperBound(0), iniFile);
                //必须设定0（系统默认的代码页）的编码方式，否则无法支持中文
                string s = Encoding.GetEncoding(0).GetString(RedBuffer);
                s = s.Substring(0, RedbufLen);
                Values.Add(key, s.Trim());

                StringBuilder temp = new StringBuilder(255);
                GetPrivateProfileString(Section, key, "", temp, 255, iniFile);
                Value.Add(temp.ToString());


            }



            return Value;
        }
    }
}
