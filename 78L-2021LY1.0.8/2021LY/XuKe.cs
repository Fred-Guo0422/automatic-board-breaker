using System;
using System.Text;
using System.Management;
using System.IO;
using System.Security.Cryptography;
using System.Web.Security;
 
/// <summary>
/// 所有加密方法
/// 定义字符串加密接口 [MD5加密,SHA1加密，字符串加密]    前台只需定这样定义：
/// sql_function.InterFaceStringEncryptionDecryption QueryStringEncodeCodeAndDncodeCode =new sql_function.StringEncryptionDecryption();
/// 说明：其中sql_function代表命名空间名称 InterFaceStringEncryptionDecryption代表接口名称   StringEncryptionDecryption代表类名
/// </summary>
public interface InterFaceStringEncryptionDecryption
{

    #region (1) QueryString加密与解密 开始

    /// <summary>
    /// QueryString加密
    /// </summary>
    /// <param name="StringSQL"></param>
    /// <returns></returns>

    string QueryStringEncodeCode(string StringSQL);

    /// <summary>
    /// QueryString解密
    /// </summary>
    /// <param name="StringSQL"></param>
    /// <returns></returns>
    string QueryStringDncodeCode(string StringSQL);

    #endregion


    #region (2) Rijndael算法

    /// <summary>
    /// 加密方法
    /// </summary>
    /// <param name="Source">待加密的串</param>
    /// <returns>经过加密的串</returns>
    string RijndaelEncrypt(string Source);

    /// <summary>
    /// 解密方法
    /// </summary>
    /// <param name="Source">待解密的串</param>
    /// <returns>经过解密的串</returns>
    string RijndaelDecrypt(string Source);

    #endregion (2) Rijndael算法


    #region ( 3 ) Base64与UTF8混用

    /// <summary>
    /// 字符串加密
    /// </summary>
    /// <param name="bb"></param>
    /// <returns></returns>
    string BUEncrypt(string bb);

    /// <summary>
    /// 字符串解密
    /// </summary>
    /// <param name="aa"></param>
    /// <returns></returns>
    string BUDecrypt(string aa);

    #endregion

    #region ( 4 )固定密钥算法

    /// <summary>
    /// 字符串加密
    /// </summary>
    /// <param name="strText"></param>
    /// <returns></returns>
    string SKeyEncrypt(string strText);


    /// <summary>
    /// 字符串解密
    /// </summary>
    /// <param name="strText"></param>
    /// <returns></returns>
    string SKeyDecrypt(string strText);

    #endregion


    #region ( 5 )DES算法

    /// <summary>
    /// DES加密
    /// </summary>
    /// <param name="strSource"></param>
    /// <returns></returns>
    string DESEncrypt(string strSource);

    /// <summary>
    /// DES解密
    /// </summary>
    /// <param name="strSource"></param>
    /// <returns></returns>
    string DESDecrypt(string strSource);


    #endregion


    #region ( 6 )   加密密码MD5T和SHA1

    /// <summary>
    /// 加密密码MD5T和SHA1
    /// </summary>
    /// <param name="strSource">字符串</param>
    /// <param name="strFlag">加密类别</param>
    /// <param name="substringlen">加密长度</param>
    /// <returns></returns>
    string encrypting(string strSource, int strFlag, int substringlen);

    #endregion
}

public class XuKe : InterFaceStringEncryptionDecryption
{
    public string GetInfo()
    {

        string cpuInfo = "";//cpu序列号
        ManagementClass cimobject = new ManagementClass("Win32_Processor");
        ManagementObjectCollection moc = cimobject.GetInstances();
        foreach (ManagementObject mo in moc)
        {
            cpuInfo = mo.Properties["ProcessorId"].Value.ToString();

        }
        //cpuInfo=
        //获取硬盘ID
        String HDid = "";
        ManagementClass cimobject1 = new ManagementClass("Win32_DiskDrive");
        ManagementObjectCollection moc1 = cimobject1.GetInstances();
        foreach (ManagementObject mo in moc1)
        {
            HDid = (string)mo.Properties["Model"].Value;

        }

        string MACInfo = GetHardWareInfo("Win32_BaseBoard", "SerialNumber");
        string BIOSInfo = GetHardWareInfo("Win32_BIOS", "SerialNumber");

        string newString = cpuInfo + HDid + MACInfo + BIOSInfo;

        MD5 mymd5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] getmd5 = Encoding.Default.GetBytes(newString);
        byte[] getjg = mymd5.ComputeHash(getmd5);
        newString = BitConverter.ToString(getjg).Replace("-", "");
        newString = newString.Replace("A", "-");

        return newString;




        //string cpuInfo = "";//cpu序列号
        //ManagementClass cimobject = new ManagementClass("Win32_Processor");
        //ManagementObjectCollection moc = cimobject.GetInstances();
        //foreach (ManagementObject mo in moc)
        //{
        //    cpuInfo = mo.Properties["ProcessorId"].Value.ToString();

        //}
        //MD5 mymd5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
        //byte[] getmd5 = Encoding.Default.GetBytes(cpuInfo);
        //byte[] getjg = mymd5.ComputeHash(getmd5);
        //cpuInfo = BitConverter.ToString(getjg).Replace("-", "");
        //cpuInfo = cpuInfo.Replace("A", "-");
        ////cpuInfo=
        ////获取硬盘ID
        //String HDid = "";
        //ManagementClass cimobject1 = new ManagementClass("Win32_DiskDrive");
        //ManagementObjectCollection moc1 = cimobject1.GetInstances();
        //foreach (ManagementObject mo in moc1)
        //{
        //    HDid = (string)mo.Properties["Model"].Value;

        //}
        //return cpuInfo.ToString();
    }



    private static string GetHardWareInfo(string typePath, string key)
    {
        try
        {
            ManagementClass managementClass = new ManagementClass(typePath);
            ManagementObjectCollection mn = managementClass.GetInstances();
            PropertyDataCollection properties = managementClass.Properties;
            foreach (PropertyData property in properties)
            {
                if (property.Name == key)
                {
                    foreach (ManagementObject m in mn)
                    {
                        return m.Properties[property.Name].Value.ToString();
                    }
                }

            }
        }
        catch (Exception ex)
        {
            //这里写异常的处理    
        }
        return string.Empty;
    }

    /// <summary>
    /// 用于加密字符串
    /// </summary>
    /// <param name="userstr">要加密的字符串</param>
    /// <returns>反回加密后的字符串</returns>
    public string Encrypt(string userstr)
    {
        string Value = userstr;
        int l = Value.Length;

        string OutputString = "";
        char[] buff = Value.ToCharArray(0, l--);
        for (int i = 0; i < buff.Length; i++)
        {
            OutputString += ((char)(buff[i] + 123)).ToString();

        }
        return OutputString;
    }
    /// <summary>
    /// 用于解密字符串
    /// </summary>
    /// <param name="userstr">要解密的字符串</param>
    /// <returns>反回解密后的字符串</returns>
    public string Decrypt(string userstr)
    {
        string Value = userstr;
        int l = Value.Length;
        string OutputString = "";
        char[] buff = Value.ToCharArray(0, l--);
        for (int i = 0; i < buff.Length; i++)
        {

            OutputString += ((char)(buff[i] - 123)).ToString();
        }
        return OutputString;
    }

    /// <summary>
    /// 写许可的方法
    /// </summary>
    /// <param name="Filepath">许可生成路径</param>
    public void WriteConfigFile(string Filepath, string Mystr24, string QSTime33, string JSTime39)
    {
        FileStream fs = new FileStream(@Filepath, FileMode.OpenOrCreate);
        fs.Close();
        StreamWriter sw = new StreamWriter(@Filepath);
        string stb = "";
        stb = stb + "[info]" + "\n";
        stb = stb + "drivername=MSSGTHRSVC" + "\n";
        stb = stb + "symbolfile=GthrCtr.h" + "\n";
        stb = stb + "[languages]" + "\n";
        stb = stb + "000=Neutral" + "\n";
        stb = stb + "009=English" + "\n";
        stb = stb + "004=Chinese (Traditional)" + "\n";
        stb = stb + "004=Chinese(PRC)" + "\n";
        stb = stb + "012=Korean" + "\n";
        stb = stb + "010=Italian" + "\n";
        stb = stb + "00A=Spanish" + "\n";
        stb = stb + "011=Japanese" + "\n";
        stb = stb + "007=German" + "\n";
        stb = stb + "00C=French" + "\n";
        stb = stb + "[text]" + "\n";
        stb = stb + "OBJECT_GTHRSVC_009_NAME = Microsoft Gatherer" + "\n";
        stb = stb + "OBJECT_GTHRSVC_004_NAME=Microsoft Gatherer" + "\n";
        stb = stb + "OBJECT_GTHRSVC_004_NAME=Microsoft Gatherer" + "\n";
        stb = stb + "OBJECT_GTHRSVC_012_NAME=Microsoft Gatherer" + "\n";
        stb = stb + "OBJECT_GTHRSVC_010_NAME=Microsoft Gatherer" + "\n";
        stb = stb + "OBJECT_GTHRSVC_00A_NAME=Recopilador de Microsoft" + "\n";
        stb = stb + "OBJECT_GTHRSVC_011_NAME=Microsoft Gatherer" + "\n";
        stb = stb + "OBJECT_GTHRSVC_007_NAME=Microsoft Gatherer" + "\n";
        stb = stb + "OBJECT_GTHRSVC_00C_NAME=Microsoft Gatherer" + "\n";
        stb = stb + "OBJECT_GTHRSVC_009_HELP =" + Encrypt(Mystr24) + "\n";
        stb = stb + "OBJECT_GTHRSVC_004_HELP=Microsoft Gathering 狝叭ン璸计竟." + "\n";
        stb = stb + "OBJECT_GTHRSVC_004_HELP=Microsoft Gathering 服务对象的计数器." + "\n";
        stb = stb + "OBJECT_GTHRSVC_012_HELP=Microsoft Gathering 辑厚胶 俺眉侩 墨款磐涝聪促" + "\n";
        stb = stb + "OBJECT_GTHRSVC_010_HELP=Contatori per Microsoft Gatherer" + "\n";
        stb = stb + "OBJECT_GTHRSVC_00A_HELP=Contadores para el objeto de servicio recopilador de Microsoft" + "\n";
        stb = stb + "OBJECT_GTHRSVC_011_HELP=Microsoft Gathering 僒乕價僗 僆僽僕僃僋僩偺僇僂儞僞偱偡丅" + "\n";
        stb = stb + "OBJECT_GTHRSVC_007_HELP=Datenquellen f黵 das Microsoft Gathering-Dienstobjekt" + "\n";
        stb = stb + "OBJECT_GTHRSVC_00C_HELP=Compteurs pour l'objet Service Microsoft Gatherer" + "\n";
        stb = stb + "COUNTER_GTHRSVC_NOTIFICATION_" + Encrypt(QSTime33) + "SOURCES_009_NAME = Notification Sources" + "\n";
        stb = stb + "COUNTER_GTHRSVC_NOTIFICATION_SOURCES_004_NAME=Notification Sources" + "\n";
        stb = stb + "COUNTER_GTHRSVC_NOTIFICATION_SOURCES_004_NAME=Notification Sources" + "\n";
        stb = stb + "COUNTER_GTHRSVC_NOTIFICATION_SOURCES_012_NAME=Notification Sources" + "\n";
        stb = stb + "COUNTER_GTHRSVC_NOTIFICATION_SOURCES_010_NAME=Origini di notifica" + "\n";
        stb = stb + "COUNTER_GTHRSVC_NOTIFICATION_SOURCES_00A_NAME=Or韌enes de notificaci髇" + "\n";
        stb = stb + "COUNTER_GTHRSVC_NOTIF" + Encrypt(JSTime39) + "ICATION_SOURCES_011_NAME=Notification Sources" + "\n";
        stb = stb + "COUNTER_GTHRSVC_NOTIFICATION_SOURCES_007_NAME=Benachrichtigungsquellen" + "\n";
        stb = stb + "COUNTER_GTHRSVC_EXTNOTIFICATIONS_RECEIVED_PER_SEC_010_NAME=Frequenza di notifiche esterne" + "\n";
        stb = stb + "COUNTER_GTHRSVC_EXTNOTIFICATIONS_RECEIVED_PER_SEC_00A_NAME=Tasa de notificaciones externas" + "\n";
        stb = stb + "COUNTER_GTHRSVC_EXTNOTIFICATIONS_RECEIVED_PER_SEC_011_NAME=Ext. Notifications Rate" + "\n";
        stb = stb + "COUNTER_GTHRSVC_EXTNOTIFICATIONS_RECEIVED_PER_SEC_007_NAME=Externe Benachrichtigungsrate" + "\n";
        stb = stb + "COUNTER_GTHRSVC_EXTNOTIFICATIONS_RECEIVED_PER_SEC_00C_NAME=Taux de notifications externes" + "\n";
        stb = stb + "COUNTER_GTHRSVC_EXTNOTIFICATIONS_RECEIVED_PER_SEC_009_HELP = The rate of external notifications received per second." + "\n";
        stb = stb + "COUNTER_GTHRSVC_EXTNOTIFICATIONS_RECEIVED_PER_SEC_004_HELP=–钡Μ场硄硉." + "\n";
        stb = stb + "COUNTER_GTHRSVC_EXTNOTIFICATIONS_RECEIVED_PER_SEC_004_HELP=每秒接收到外部通知的速率。." + "\n";
        stb = stb + "COUNTER_GTHRSVC_EXTNOTIFICATIONS_RECEIVED_PER_SEC_012_HELP=檬 寸 罐篮 寇何 舅覆 荐涝聪促." + "\n";
        stb = stb + "COUNTER_GTHRSVC_EXTNOTIFICATIONS_RECEIVED_PER_SEC_010_HELP=Numero di notifiche esterne ricevute al secondo" + "\n";
        stb = stb + "COUNTER_GTHRSVC_ADMIN_SOURCES_00C_HELP=Nombre de clients d'administration actuellement connect閟." + "\n";
        stb = stb + "COUNTER_GTHRSVC_HEARTBEATS_009_NAME = Heartbeats " + "\n";
        stb = stb + "COUNTER_GTHRSVC_HEARTBEATS_004_NAME=Heartbeats" + "\n";
        stb = stb + "COUNTER_GTHRSVC_HEARTBEATS_004_NAME=Heartbeats" + "\n";
        stb = stb + "COUNTER_GTHRSVC_HEARTBEATS_012_NAME=Heartbeats" + "\n";
        stb = stb + "COUNTER_GTHRSVC_HEARTBEATS_010_NAME=Heartbeat" + "\n";
        stb = stb + "COUNTER_GTHRSVC_IORATE_DETECTED_000_NAME = System IO traffic rate" + "\n";
        stb = stb + "COUNTER_GTHRSVC_IORATE_DETECTED_000_HELP = System IO (disk) traffic rate in KB/s detected by back off logic" + "\n";
        stb = stb + "COUNTER_GTHRSVC_BACKOFF_REASON_000_NAME = Reason to back off" + "\n";
        stb = stb + "COUNTER_GTHRSVC_BACKOFF_REASON_000_HELP = The code describing why gathering service went into back off state" + "\n";
        stb = stb + "COUNTER_GTHRSVC_THREADS_BLOCKED_ON_BACKOFF_000_NAME = Threads blocked due to back off" + "\n";
        stb = stb + "COUNTER_GTHRSVC_THREADS_BLOCKED_ON_BACKOFF_000_HELP = The number of threads blocked due to back off event" + "\n";
        string str = RijndaelEncrypt(stb.ToString());
        string str1 = DESEncrypt(str);
        string str2 = BUEncrypt(str1);
        string str3 = SKeyEncrypt(str2);
        sw.Write(str3);
        sw.Close();

    }
    /// <summary>
    /// 读ConfigFile文件的方法
    /// </summary>
    /// <param name="Filepath">ConfigFile的所在路径</param>
    /// <returns>返回一个字符串数组</returns>
    public string Read(string Filepath, int Row_index)
    {
        string ConfigContents = "";
        try
        {
            StreamReader sr = new StreamReader(@Filepath);
            string str = SKeyDecrypt(sr.ReadToEnd());
            string str1 = BUDecrypt(str);
            string str2 = DESDecrypt(str1);
            string str3 = RijndaelDecrypt(str2);
            string[] REncrypt = str3.Split('\n');
            ConfigContents = REncrypt[Row_index];
        }
        catch
        { }
        return ConfigContents;
    }




    #region (1) QueryString加密与解密 开始

    /// <summary>
    /// QueryString加密
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public string QueryStringEncodeCode(string code)
    {
        string result = "";
        if (code == null || code == "")
        {
            result = "";
        }
        else
        {
            result = Convert.ToBase64String(System.Text.Encoding.Default.GetBytes("" + code + "")).Replace("+", "%2B");

        }
        return result;
    }

    /// <summary>
    /// QueryString解密
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public string QueryStringDncodeCode(string code)
    {
        string result = "";

        if (code == null || code == "")
        {
            result = "";
        }
        else
        {
            try
            {
                result = System.Text.Encoding.Default.GetString(Convert.FromBase64String(code.Replace("%2B", "+")));
            }
            catch (FormatException ex)///抛出异常 [错误信息“Base-64字符数组的无效长度”]
            {
                result = "0";
            }
        }
        return result;
    }


    #endregion (1) QueryString加密与解密 结束


    #region ( 2 ) Rijndael算法
    private static SymmetricAlgorithm mobjCryptoService = new RijndaelManaged();

    private static string Key = "HeTao(%&hj7x89H$yuBI0456FtmaT5&fvHUFCy76h%(HilJ$lhj!y6&(*jkP87jH7";

    /// <summary>
    /// 获得密钥
    /// </summary>
    /// <returns>密钥</returns>
    private byte[] GetLegalKey()
    {
        string sTemp = Key;
        mobjCryptoService.GenerateKey();
        byte[] bytTemp = mobjCryptoService.Key;
        int KeyLength = bytTemp.Length;
        if (sTemp.Length > KeyLength)
            sTemp = sTemp.Substring(0, KeyLength);
        else if (sTemp.Length < KeyLength)
            sTemp = sTemp.PadRight(KeyLength, ' ');
        return ASCIIEncoding.ASCII.GetBytes(sTemp);
    }
    /// <summary>
    /// 获得初始向量IV
    /// </summary>
    /// <returns>初试向量IV</returns>
    private byte[] GetLegalIV()
    {
        string sTemp = "E4ghj*Ghg7!rNIfb&95GUY86GfghUb#er57HeTao%g6HJ($jhWk7&!hg4ui%$hjk";
        mobjCryptoService.GenerateIV();
        byte[] bytTemp = mobjCryptoService.IV;
        int IVLength = bytTemp.Length;
        if (sTemp.Length > IVLength)
            sTemp = sTemp.Substring(0, IVLength);
        else if (sTemp.Length < IVLength)
            sTemp = sTemp.PadRight(IVLength, ' ');
        return ASCIIEncoding.ASCII.GetBytes(sTemp);
    }
    /// <summary>
    /// 加密方法
    /// </summary>
    /// <param name="Source">待加密的串</param>
    /// <returns>经过加密的串</returns>
    public string RijndaelEncrypt(string Source)
    {
        byte[] bytIn = UTF8Encoding.UTF8.GetBytes(Source);
        MemoryStream ms = new MemoryStream();
        mobjCryptoService.Key = GetLegalKey();
        mobjCryptoService.IV = GetLegalIV();
        ICryptoTransform encrypto = mobjCryptoService.CreateEncryptor();
        CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Write);
        cs.Write(bytIn, 0, bytIn.Length);
        cs.FlushFinalBlock();
        ms.Close();
        byte[] bytOut = ms.ToArray();
        return Convert.ToBase64String(bytOut);
    }

    /// <summary>
    /// 解密方法
    /// </summary>
    /// <param name="Source">待解密的串</param>
    /// <returns>经过解密的串</returns>
    public string RijndaelDecrypt(string Source)
    {
        try
        {
            byte[] bytIn = Convert.FromBase64String(Source);
            MemoryStream ms = new MemoryStream(bytIn, 0, bytIn.Length);
            mobjCryptoService.Key = GetLegalKey();
            mobjCryptoService.IV = GetLegalIV();
            ICryptoTransform encrypto = mobjCryptoService.CreateDecryptor();
            CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Read);
            StreamReader sr = new StreamReader(cs);
            return sr.ReadToEnd();
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
    #endregion


    #region ( 3 )Base64与UTF8混用

    //字符串加密
    public string BUEncrypt(string bb)
    {
        byte[] by = new byte[bb.Length];
        by = System.Text.Encoding.UTF8.GetBytes(bb);

        string r = Convert.ToBase64String(by);
        return r;
    }

    //字符串解密
    public string BUDecrypt(string aa)
    {
        try
        {
            byte[] by = Convert.FromBase64String(aa);

            string r = Encoding.UTF8.GetString(by);

            return r;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    #endregion


    #region ( 4 )固定密钥算法

    public static Byte[] Iv64 = { 19, 82, 11, 14, 06, 56, 15, 02 };

    public static Byte[] byKey64 = { 02, 20, 50, 40, 61, 60, 75, 10 };
    //字符串加密
    public string SKeyEncrypt(string strText)
    {
        try
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            Byte[] inputByteArray = Encoding.UTF8.GetBytes(strText);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(byKey64, Iv64), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Convert.ToBase64String(ms.ToArray());
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    //字符串解密
    public string SKeyDecrypt(string strText)
    {
        Byte[] inputByteArray = new byte[strText.Length];
        try
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            inputByteArray = Convert.FromBase64String(strText);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(byKey64, Iv64), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            System.Text.Encoding encoding = System.Text.Encoding.UTF8;
            return encoding.GetString(ms.ToArray());
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
    #endregion


    #region ( 5 )DES算法

    //public static byte[] DESKey=new byte[]{0x82,0xBC,0xA1,0x6A,0xF5,0x87,0x3B,0xE6,0x59,0x6A,0x32,0x64,0x7F,0x3A,0x2A,0xBB,0x2B,0x68,0xE2,0x5F,0x06,0xFB,0xB8,0x2D,0x67,0xB3,0x55,0x19,0x4E,0xB8,0xBF,0xDD};
    public static byte[] DESKey = Encoding.Default.GetBytes("j1f2d5s3k8s3l9d3k8f1f7d3jzqzx32h");
    /// <summary>
    /// DES加密
    /// </summary>
    /// <param name="strSource">待加密字串</param>
    /// <param name="key">32位Key值</param>
    /// <returns>加密后的字符串</returns>
    public string DESEncrypt(string strSource)
    {
        return DESEncryptF(strSource, DESKey);
    }

    private string DESEncryptF(string strSource, byte[] key)
    {
        SymmetricAlgorithm sa = Rijndael.Create();
        sa.Key = key;
        sa.Mode = CipherMode.ECB;
        sa.Padding = PaddingMode.Zeros;
        MemoryStream ms = new MemoryStream();
        CryptoStream cs = new CryptoStream(ms, sa.CreateEncryptor(), CryptoStreamMode.Write);
        byte[] byt = Encoding.Unicode.GetBytes(strSource);
        cs.Write(byt, 0, byt.Length);
        cs.FlushFinalBlock();
        cs.Close();
        return Convert.ToBase64String(ms.ToArray());
    }

    /// <summary>
    /// DES解密
    /// </summary>
    /// <param name="strSource">待解密的字串</param>
    /// <param name="key">32位Key值</param>
    /// <returns>解密后的字符串</returns>
    public string DESDecrypt(string strSource)
    {
        return DESDecryptF(strSource, DESKey);
    }

    private string DESDecryptF(string strSource, byte[] key)
    {
        try
        {
            SymmetricAlgorithm sa = Rijndael.Create();
            sa.Key = key;
            sa.Mode = CipherMode.ECB;
            sa.Padding = PaddingMode.Zeros;
            ICryptoTransform ct = sa.CreateDecryptor();
            byte[] byt = Convert.FromBase64String(strSource);
            MemoryStream ms = new MemoryStream(byt);
            CryptoStream cs = new CryptoStream(ms, ct, CryptoStreamMode.Read);
            StreamReader sr = new StreamReader(cs, Encoding.Unicode);
            return sr.ReadToEnd();
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
    #endregion


    #region ( 6 )   加密密码MD5T和SHA1

    /// <summary>
    /// 加密密码MD5T和SHA1
    /// </summary>
    /// <param name="strSource">字符串</param>
    /// <param name="strFlag">加密类别</param>
    /// <param name="substringlen">加密长度</param>
    /// <returns></returns>
    /// 
    public string encrypting(string strSource, int strFlag, int substringlen)
    {
        string ss = "";
        if (strFlag == 1)///MD5加密
        {
            if (substringlen == 16)//16位MD5加密（取32位加密的9~25字符）
            {
                ss = FormsAuthentication.HashPasswordForStoringInConfigFile(strSource, "MD5").ToLower().Substring(8, 16);
            }
            else if (substringlen == 32)//32位加密
            {
                ss = FormsAuthentication.HashPasswordForStoringInConfigFile(strSource, "MD5").ToLower();
            }
        }
        else if (strFlag == 2)///SHA1加密
        {
            if (substringlen == 16)//SHA1 16位加密
            {
                ss = FormsAuthentication.HashPasswordForStoringInConfigFile(strSource, "SHA1").ToLower().Substring(8, 16);
            }
            else if (substringlen == 32)//SHA1 40位加密
            {
                ss = FormsAuthentication.HashPasswordForStoringInConfigFile(strSource, "SHA1").ToLower();
            }
        }
        else
        {
            ss = "";
        }
        return ss;
    }

    #endregion
}

