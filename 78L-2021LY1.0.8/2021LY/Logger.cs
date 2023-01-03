using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
/// <summary>   
/// 日志类
/// </summary>
namespace _2021LY
{
    class Logger
    {
        private static volatile object threadLocker = new object();

        //日志文件保存目录
        

        /// <summary>
        /// 弹出日志窗口
        /// </summary>
        /// <param name="msg"></param>
        public static void MessageShow(string msg)
        {
            MessageBox.Show(msg);
        }


        /// <summary>
        /// 记录日志文件
        /// </summary>
        /// <param name="msg"></param>
        //public static void Recod_Log_File(string msg)
        //{
        //    try
        //    {
        //        var Recod_Log_Task = Task.Factory.StartNew(() =>
        //        {
        //            lock (threadLocker)
        //            {
        //                FileUtil fu = new FileUtil();
        //                fu.Append_File(CommParam.LogFile_Path, CommUtil.Get_YMDHMS() + "：" + msg);
        //        }
        //        });
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine("Recod_Log_File日志记录文件异常：" + "\r\n x.StackTrace" + e.StackTrace);
        //    }
        //}

    }
}
