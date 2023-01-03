using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2021LY
{
    public partial class LoginForm : Form
    {
        private static LoginForm formSingle = null;
        public LoginForm()
        {
            InitializeComponent();
        }


        public static LoginForm getSingle()
        {
            if (formSingle == null)
            {
                formSingle = new LoginForm();
            }
            return formSingle;
        }
        //取消按钮
        private void button2_Click(object sender, EventArgs e)
        {
            if (formSingle != null)
            {
                formSingle.Close();
            }
        }
        //登录按钮
        private void button1_Click(object sender, EventArgs e)
        {
          
            short un = -1;
            switch (User_Name.SelectedIndex)
            {
                case 0: un = 0; break;//操作员
                case 1: un = 1; break;//技术员
                case 2: un = 2; break;//工程师
                case 3: un = 3; break;//管理员
            }
            string passWord = Password.Text.ToString();

            if (Check_Login(un, passWord))
            {
                CommParam.Authority = un;
                Authority au = new Authority();
                switch (un)
                {
                    case 0:
                        au.Operator_Auth();
                        break;
                    case 1:
                        au.Technician_Auth();
                        break;
                    case 2:
                        au.Engineer_Auth();
                        break;
                    case 3:
                        au.Admin_Auth();
                        break;
                }
                Console.WriteLine("密码正确");
                Password.Clear();
                formSingle.Close();
            }
            else
            {
                Logger.MessageShow("密码错误");
                return;
            }

        }


        private bool Check_Login(short un, string passWord)
        {
            bool OK = false;
            if (!string.IsNullOrEmpty(passWord)||un == 0)
            {

                bool isnum = CommUtil.Is_Number(passWord);
                if (isnum)
                {
                    //进行密码校验       
                    switch (un)
                    {
                        case 0://操作员 无需密码
                            OK = true;
                            break;
                        case 1://技术员0000
                            OK = "0000".Equals(passWord);
                            break;
                        case 2://工程师（当前年月日。6位）
                            OK = (CommUtil.Get_YMD()).Equals(passWord);
                            break;
                        case 3://管理员  （当前年月日小时，8位
                            OK = (CommUtil.Get_YMDH()).Equals(passWord);
                            break;
                    }
                }
            }
            return OK;
        }




    }
}
