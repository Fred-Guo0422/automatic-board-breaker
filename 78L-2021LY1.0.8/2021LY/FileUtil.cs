using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
/// <summary>
/// 文件操作类
/// </summary>
namespace _2021LY
{
    class FileUtil
    {
        //FileStream fs = null;
        /// <summary>
        /// 打开文件的对话框
        /// </summary>
        public bool Open_File_Dialog(out string path)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            bool tag = true;
            path = null;
            ofd.InitialDirectory = @"C:\Users\Administrator\Desktop";
            ofd.Title = "选择文件";
            ofd.Filter = "(*.txt;*.dxf)|*.txt;*.dxf";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                path = ofd.FileName;

            }
            else
            {
                tag = false;
            }
            return tag;
        }

        /// <summary>
        /// 保存文件对话框
        /// </summary>
        /// <param name="path"></param>
        public void Save_File_Dialog(out string path)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = @"C:\Users\Administrator\Desktop";
            sfd.Title = "输入名字";
            sfd.Filter = "文本文件(*.txt)|*.txt";//"|*.txt";
            sfd.ShowDialog();
            path = sfd.FileName;
        }


        /// <summary>
        /// 保存文件  
        /// </summary>
        //public bool Save_File1(string path, string msg)
        //{
        //    bool tag = true;
        //    try
        //    {
        //        fs = new FileStream(@path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write);
        //        byte[] cont = Encoding.Default.GetBytes(msg);
        //        fs.Write(cont, 0, cont.Length);
        //    }
        //    catch (Exception x)
        //    {
        //        Logger.Recod_Log_File("Save_File 保存文件 Exception " + x.Message + "\r\n x.StackTrace" + x.StackTrace + "\r\n path:" + path);
        //        tag = false;
        //        throw x;
        //    }
        //    finally
        //    {
        //        if (fs != null)
        //        {
        //            fs.Close();
        //            fs.Dispose();
        //        }
        //    }
        //    return tag;
        //}

        public bool Save_File(string path, string msg)
        {
            bool tag = true;
            try
            {
                using (FileStream fs = new FileStream(@path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write))
                {
                    byte[] cont = Encoding.Default.GetBytes(msg);
                    fs.Write(cont, 0, cont.Length);
                }
            }
            catch (Exception x)
            {
                LogHelper.WriteErrorLog("Save_File 保存文件 操作异常 ", x);
               // Logger.Recod_Log_File("Save_File 保存文件 Exception " + x.Message + "\r\n x.StackTrace" + x.StackTrace + "\r\n path:" + path);
                tag = false;
                 throw x;
            }
            return tag;
        }



        /// <summary>
        /// 读取文件
        /// </summary>
        public bool Read_File(string path, out string msg)
        {
            bool tag = true;
            try
            {
                using (FileStream fs = new FileStream(@path, FileMode.OpenOrCreate, FileAccess.Read))
                {
                    byte[] buff = new byte[1024 * 1024 * 5];
                    int x = fs.Read(buff, 0, buff.Length);
                    msg = Encoding.Default.GetString(buff, 0, x);
                    return tag;
                }
            }
            catch (Exception x)
            {
              
                msg = "";
                LogHelper.WriteErrorLog("Read_File 读取文件 操作异常 ", x);
              //  Logger.Recod_Log_File("Read_File 读取文件 Exception " + x.Message + "\r\n x.StackTrace" + x.StackTrace + "\r\n path:" + path);
                tag = false;
                throw x;
            }
            //finally
            //{
            //    if (fs != null)
            //    {
            //        fs.Close();
            //        fs.Dispose();
            //    }
        }

    public void Del_File(string path)
    {
        File.Delete(@path);
    }

    /// <summary>
    /// 读取文件，在文件末尾追加一行内容
    /// </summary>
    public void Append_File(string path, string msg)
    {

        string oldMsg = "";

        bool tag = Read_File(path, out oldMsg);
        string newMsg = oldMsg + "\r\n" + msg;
        if (tag)
        {
            Save_File(path, newMsg);
        }
    }





}
}
