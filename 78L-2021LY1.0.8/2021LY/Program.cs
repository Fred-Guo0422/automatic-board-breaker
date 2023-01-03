using _2021LY.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2021LY
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
           return;


          //  if (Check_Xuke())
            if (true)
                {
                Form1 f = new Form1();
                CommParam.form1 = (Form1)f;
                f.Text = "领略智能";
                f.Icon = Properties.Resources._2021_05_15_170014;
                f.StartPosition = FormStartPosition.CenterScreen;
                Application.Run(f);
            }
            else
            {
                Application.Run(new Error());
            }
        }

        /// <summary>
        /// 检查软件是否授权
        /// </summary>
        /// <returns></returns>
        static bool Check_Xuke()
        {
            bool tag = false;
            XuKe xk = new XuKe();
            string a = "OBJECT_GTHRSVC_009_HELP =" + xk.Encrypt(xk.GetInfo());
            string b = xk.Read(Application.StartupPath + "\\Xuke.dat", 24);
            if (!("OBJECT_GTHRSVC_009_HELP =" + xk.Encrypt(xk.GetInfo())).Equals(xk.Read(Application.StartupPath + "\\Xuke.dat", 24)))
            {
                //Application.Run(new Error());
                tag = false;
            }
            else
            {
                string KSSJ = xk.Read(Application.StartupPath + "\\Xuke.dat", 33);
                KSSJ = xk.Decrypt(KSSJ.Substring(29, KSSJ.Length - 29 - 39));
                string JSSJ = xk.Read(Application.StartupPath + "\\Xuke.dat", 39);
                JSSJ = xk.Decrypt(JSSJ.Substring(21, JSSJ.Length - 21 - 45));
                if (DateTime.Parse(DateTime.Now.ToShortDateString()) > DateTime.Parse(JSSJ) || DateTime.Parse(DateTime.Now.ToShortDateString()) < DateTime.Parse(KSSJ))
                {
                    tag = false;
                    //Application.Run(new Error());
                }
                else
                {
                    tag = true;

                }

            }
            return tag;
        }






    }
}
