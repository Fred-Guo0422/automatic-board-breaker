using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2021LY
{
    public class toolSetting
    {
        public static string Config = Application.StartupPath + "\\Config.ini";


        [DllImport("kernel32")]
        private static extern bool WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, byte[] retVal, int size, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        public static void WriteStringToIni(string Section, string Key, string Value, string iniFile)
        {
            WritePrivateProfileString(Section, Key, Value, iniFile);
        }

        public static string ReadIniString(string Section, string Key, string iniFile)
        {
            StringBuilder temp = new StringBuilder(255);
            GetPrivateProfileString(Section, Key, "", temp, 255, iniFile);
            return temp.ToString();
        }

        public static void Readspeed(ref string Vel, ref string Acc, ref string End)
        {
            Vel = ReadIniString("Dd_speed", "Dd_speed_Vel", Config);
            Acc = ReadIniString("Dd_speed", "Dd_speed_Acc", Config);
            End = ReadIniString("Dd_speed", "Dd_speed_End", Config);

            CommParam. Dd_Speed_Vel = Vel;//对左刀坐标点合集
            CommParam. Ddr_Speed_Acc = Acc;//对右刀坐标点合集
            CommParam. Ddr_Speed_End = End;//对左刀坐标点合集
        }

        public static void SaveSpeed(string Vel, string Acc, string End)
        {
            WriteStringToIni("Dd_speed", "Dd_speed_Vel", Vel, Config);
            WriteStringToIni("Dd_speed", "Dd_speed_Acc", Acc, Config);
            WriteStringToIni("Dd_speed", "Dd_speed_End", End, Config);
            CommParam.Dd_Speed_Vel = Vel;//对左刀坐标点合集
            CommParam.Ddr_Speed_Acc = Acc;//对右刀坐标点合集
            CommParam.Ddr_Speed_End = End;//对左刀坐标点合集
        }
    }
}
