using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

/// <summary>
/// 权限配置类
/// </summary>
namespace _2021LY
{
    /*
    tabpage :
            IO_tab  JXCS_tab  BCSZ_tab
    buttom:
        机械参数
        button10   保存参数
        button7     读取参数

        编程设计
        QD          起点
        ZX          直线
        ZD          终点
        SC          删除行
        BC          保存
        XJ          新建
        DK          打开
        button12    回原点
        button1     X左
        button2     X右
        button4     Y上
        button3     Y下
        button6     Z上
        button5     Z下
        
 testBox:
        XEnc_Pos   X轴编码器位置
        YEnc_Pos   Y轴编码器位置
        ZEnc_Pos   Z轴编码器位置

        this.tabControl1.Controls["BCSZ_tab"].Parent = null; JXCS_tab  BCSJ_tab
        
        */
    class Authority
    {

        //操作员
        public void Operator_Auth()
        {


            string[] in_Auth_Button = new string[] { "DK", "button12" };//有的权限
            string[] out_Auth_Button = new string[] { "QD", "ZX", "ZD", "button10", "button7", "SC", "BC", "XJ", "button1", "button2", "button6", "button5", "button4", "button3" };//没有的权限
            Close_Button(out_Auth_Button);
            Open_Button(in_Auth_Button);

            string[] out_Auth_Text = new string[] { "XEnc_Pos", "YEnc_Pos", "ZEnc_Pos" };//有的权限
            Close_TextBox(out_Auth_Text);

            CommParam.JXCS_tab.Parent = null;
            //string[] out_Auth_labPage = new string[] { "JXCS_tab" };//没的权限
            //Close_tabPag(out_Auth_labPage);
        }
        //技术员
        public void Technician_Auth()
        {

            //string[] in_Auth_labPage = new string[] { "JXCS_tab" };//有的权限
            //Open_tabPag(in_Auth_labPage);
            CommParam.JXCS_tab.Parent = CommParam.tabControl;

            string[] in_Auth_Button = new string[] { "DK", "button12", "button10", "button7" };//有的权限
            string[] out_Auth_Button = new string[] { "button1", "button2", "button6", "button5", "button4", "button3", "QD", "ZX", "ZD", "SC", "BC", "XJ" };//没有的权限
            Close_Button(out_Auth_Button);
            Open_Button(in_Auth_Button);


            string[] out_Auth_Text = new string[] { "XEnc_Pos", "YEnc_Pos", "ZEnc_Pos" };//有的权限
            Close_TextBox(out_Auth_Text);
            //  Open_TextBox(in_Auth_Text);

        }


        //工程师
        public void Engineer_Auth()
        {
            CommParam.JXCS_tab.Parent = CommParam.tabControl;
            string[] in_Auth_Button = new string[] { "DK", "button12", "button10", "button7", "button1", "button2", "button6", "button5", "button4", "button3", "QD", "ZX", "ZD", "SC", "BC", "XJ" };//有的权限
            string[] out_Auth_Button = new string[] { };//没有的权限
            //Close_Button(out_Auth_Button);
            Open_Button(in_Auth_Button);

            string[] out_Auth_Text = new string[] { "XEnc_Pos", "YEnc_Pos", "ZEnc_Pos" };//有的权限
            Open_TextBox(out_Auth_Text);
        }

        //管理员
        public void Admin_Auth()
        {
            CommParam.JXCS_tab.Parent = CommParam.tabControl;
            string[] in_Auth_Button = new string[] { "DK", "button12", "button10", "button7", "button1", "button2", "button6", "button5", "button4", "button3", "QD", "ZX", "ZD", "SC", "BC", "XJ" };//有的权限
            string[] out_Auth_Button = new string[] { };//没有的权限
            //Close_Button(out_Auth_Button);
            Open_Button(in_Auth_Button);

            string[] out_Auth_Text = new string[] { "XEnc_Pos", "YEnc_Pos", "ZEnc_Pos" };//有的权限
            Open_TextBox(out_Auth_Text);

        }
        /// <summary>
        /// 打开按钮
        /// </summary>
        private void Open_Button(string[] str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                string s = str[i];
                Control[] bu =  CommParam.form1.Controls.Find(str[i], true);
                Button b = (Button)(CommParam.form1.Controls.Find(str[i], true)[0]);
                b.Visible = true;
            }
        }
        /// <summary>
        /// 关闭按钮
        /// </summary>
        private void Close_Button(string[] str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                Button b = (Button)(CommParam.form1.Controls.Find(str[i], true)[0]);
                b.Visible = false;
            }
        }

        /// <summary>
        /// 打开输入框
        /// </summary>
        private void Close_TextBox(string[] str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                TextBox b = (TextBox)(CommParam.form1.Controls.Find(str[i], true)[0]);
                b.Visible = false;
            }
        }

        /// <summary>
        /// 关闭输入框
        /// </summary>
        private void Open_TextBox(string[] str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                TextBox b = (TextBox)(CommParam.form1.Controls.Find(str[i], true)[0]);
                b.Visible = true;
            }
        }



        /// <summary>
        /// 关闭tabPage菜单    JXCS_tab BCSZ_tab
        /// </summary>
        private void Close_tabPag(string[] str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                string s = str[i];
                CommParam.tabControl.Controls[s].Parent = null;

            }
        }

        /// <summary>
        /// 打开tabPage菜单
        /// </summary>
        private void Open_tabPag(string[] str)
        {
            // for (int i = 0; i < str.Length; i++)
            //{
            //    string s = str[i];
            //  BCSJ_tab
            Control[] b = CommParam.form1.Controls.Find("JXCS_tab", true);
                 //  Control b = (Control)(CommParam.form1.Controls.Find("JXCS_tab", true)[0]);
            if (b == null && b.Length==0)
            {
                 CommParam.tabControl.Controls.Add(CommParam.JXCS_tab);
            }

          //  }
        }
    }
}
