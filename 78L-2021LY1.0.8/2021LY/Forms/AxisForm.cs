using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2021LY.Forms
{
    public partial class AxisForm : UITitlePage
    {
        IniFunctoin iniFunctoin = new IniFunctoin();//得到配置文件信息
        //string iniPath = Application.StartupPath + "\\param.txt";
        public AxisForm()
        {
            InitializeComponent();

            CommParam.Param_Path = CommParam.Param_Path.Replace("\\\\","\\");

            this.Text = "机械参数";
            this.Load += AxisForm_Load;
            Read_Param();
        }

        private void AxisForm_Load(object sender, EventArgs e)
        {
            string Vel = string.Empty;
            string Acc = string.Empty;
            string End = string.Empty;
            toolSetting.Readspeed(ref Vel, ref Acc, ref End);

            Dd_speed_Vel.Text = Vel;
            Dd_speed_Acc.Text = Acc;
            Dd_speed_End.Text = End;
        }

        private static AxisForm axisForm;

        public static AxisForm getForm()
        {
            if (axisForm == null)
            {
                axisForm = new AxisForm();
            }
            return axisForm;
        }
    

        /// <summary>
        /// 读取参数，赋值到参数界面和公共参数类
        /// </summary>
        private void Read_Param()
        {
            //FileUtil f = new FileUtil();
            //string param = "";
            try
            {


                var Xsearch_home1 = iniFunctoin.ReadIniString("X_axis_parameters", "Xsearch_home", CommParam.Param_Path).ToString();
                var Xhome_acc1 = iniFunctoin.ReadIniString("X_axis_parameters", "Xhome_acc", CommParam.Param_Path).ToString();
                var Xhome_dec1 = iniFunctoin.ReadIniString("X_axis_parameters", "Xhome_dec", CommParam.Param_Path).ToString();
                var Xhome_vel1 = iniFunctoin.ReadIniString("X_axis_parameters", "Xhome_vel", CommParam.Param_Path).ToString();
                var Xhome_offset1 = iniFunctoin.ReadIniString("X_axis_parameters", "Xhome_offset", CommParam.Param_Path).ToString();

                var Ysearch_home1 = iniFunctoin.ReadIniString("Y_axis_parameters", "Ysearch_home", CommParam.Param_Path).ToString();
                var Yhome_acc1 = iniFunctoin.ReadIniString("Y_axis_parameters", "Yhome_acc", CommParam.Param_Path).ToString();
                var Yhome_dec1 = iniFunctoin.ReadIniString("Y_axis_parameters", "Yhome_dec", CommParam.Param_Path).ToString();
                var Yhome_vel1 = iniFunctoin.ReadIniString("Y_axis_parameters", "Yhome_vel", CommParam.Param_Path).ToString();
                var Yhome_offset1 = iniFunctoin.ReadIniString("Y_axis_parameters", "Yhome_offset", CommParam.Param_Path).ToString();

                var Y2search_home1 = iniFunctoin.ReadIniString("Y2_axis_parameters", "Y2search_home", CommParam.Param_Path).ToString();
                var Y2home_acc1 = iniFunctoin.ReadIniString("Y2_axis_parameters", "Y2home_acc", CommParam.Param_Path).ToString();
                var Y2home_dec1 = iniFunctoin.ReadIniString("Y2_axis_parameters", "Y2home_dec", CommParam.Param_Path).ToString();
                var Y2home_vel1 = iniFunctoin.ReadIniString("Y2_axis_parameters", "Y2home_vel", CommParam.Param_Path).ToString();
                var Y2home_offset1 = iniFunctoin.ReadIniString("Y2_axis_parameters", "Y2home_offset", CommParam.Param_Path).ToString();

                var Zsearch_home1 = iniFunctoin.ReadIniString("Z_axis_parameters", "Zsearch_home", CommParam.Param_Path).ToString();
                var Zhome_acc1 = iniFunctoin.ReadIniString("Z_axis_parameters", "Zhome_acc", CommParam.Param_Path).ToString();
                var Zhome_dec1 = iniFunctoin.ReadIniString("Z_axis_parameters", "Zhome_dec", CommParam.Param_Path).ToString();
                var Zhome_vel1 = iniFunctoin.ReadIniString("Z_axis_parameters", "Zhome_vel", CommParam.Param_Path).ToString();
                var Zhome_offset1 = iniFunctoin.ReadIniString("Z_axis_parameters", "Zhome_offset", CommParam.Param_Path).ToString();

                var CB_Vel1 = iniFunctoin.ReadIniString("CB_parameters", "CB_Vel", CommParam.Param_Path).ToString();
                var CB_Acc1 = iniFunctoin.ReadIniString("CB_parameters", "CB_Acc", CommParam.Param_Path).ToString();
                var CB_Dec1 = iniFunctoin.ReadIniString("CB_parameters", "CB_Dec", CommParam.Param_Path).ToString();
                var CB_SmoothTime1 = iniFunctoin.ReadIniString("CB_parameters", "CB_SmoothTime", CommParam.Param_Path).ToString();

                var ZBX_SynVelMax1 = iniFunctoin.ReadIniString("XY_parameters", "ZBX_SynVelMax", CommParam.Param_Path).ToString();
                var ZBX_SynAccMax1 = iniFunctoin.ReadIniString("XY_parameters", "ZBX_SynAccMax", CommParam.Param_Path).ToString();
                var ZBX_EvenTime1 = iniFunctoin.ReadIniString("XY_parameters", "ZBX_EvenTime", CommParam.Param_Path).ToString();

                var CB_Z_Vel1 = iniFunctoin.ReadIniString("XY_parameters", "CB_Z_Vel", CommParam.Param_Path).ToString();
                var XSD_Vel1 = iniFunctoin.ReadIniString("X_Manual_parameter", "XSD_Vel", CommParam.Param_Path).ToString();
                var XSD_Acc1 = iniFunctoin.ReadIniString("X_Manual_parameter", "XSD_Acc", CommParam.Param_Path).ToString();
                var XSD_Dec1 = iniFunctoin.ReadIniString("X_Manual_parameter", "XSD_Dec", CommParam.Param_Path).ToString();

                var YSD_Vel1 = iniFunctoin.ReadIniString("Y_Manual_parameter", "YSD_Vel", CommParam.Param_Path).ToString();
                var YSD_Acc1 = iniFunctoin.ReadIniString("Y_Manual_parameter", "YSD_Acc", CommParam.Param_Path).ToString();
                var YSD_Dec1 = iniFunctoin.ReadIniString("Y_Manual_parameter", "YSD_Dec", CommParam.Param_Path).ToString();

                var ZSD_Vel1 = iniFunctoin.ReadIniString("Z_Manual_parameter", "ZSD_Vel ", CommParam.Param_Path).ToString();
                var ZSD_Acc1 = iniFunctoin.ReadIniString("Z_Manual_parameter", "ZSD_Acc ", CommParam.Param_Path).ToString();
                var ZSD_Dec1 = iniFunctoin.ReadIniString("Z_Manual_parameter", "ZSD_Dec ", CommParam.Param_Path).ToString();

                var Y2SD_Acc1 = iniFunctoin.ReadIniString("Y2_Manual_parameter", "Y2SD_Acc", CommParam.Param_Path).ToString();
                var Y2SD_Dec1 = iniFunctoin.ReadIniString("Y2_Manual_parameter", "Y2SD_Dec", CommParam.Param_Path).ToString();
                var z_nu_run1 = iniFunctoin.ReadIniString("Duidao_parameter", "z_nu_run", CommParam.Param_Path).ToString();
                var ddr_uiTextBox1 = iniFunctoin.ReadIniString("Duidao_parameter", "ddr_uiTextBox", CommParam.Param_Path).ToString();
                var ddl_uiTextBox1 = iniFunctoin.ReadIniString("Duidao_parameter", "ddl_uiTextBox", CommParam.Param_Path).ToString();
                // f.Read_File(CommParam.Param_Path, out param);
                //if (!string.IsNullOrEmpty(param))
                //{
                // string[] All_param = param.Split(new char[] { ',' });

                CommParam.Xsearch_home = Xsearch_home1;
                this.Xsearch_home.Text = Xsearch_home1;

                CommParam.Xhome_acc = Convert.ToDouble(Xhome_acc1);
                this.Xhome_acc.Text = Xhome_acc1;

                CommParam.Xhome_dec = Convert.ToDouble(Xhome_dec1);
                this.Xhome_dec.Text = Xhome_dec1;

                CommParam.Xhome_vel = Convert.ToDouble(Xhome_vel1);
                this.Xhome_vel.Text = Xhome_vel1;

                CommParam.Xhome_offset = Xhome_offset1;
                this.Xhome_offset.Text = Xhome_offset1;

                CommParam.Ysearch_home = Ysearch_home1;
                this.Ysearch_home.Text = Ysearch_home1;

                CommParam.Yhome_acc = Convert.ToDouble(Yhome_acc1);
                this.Yhome_acc.Text = Yhome_acc1;

                CommParam.Yhome_dec = Convert.ToDouble(Yhome_dec1);
                this.Yhome_dec.Text = Yhome_dec1;

                CommParam.Yhome_vel = Convert.ToDouble(Yhome_vel1);
                this.Yhome_vel.Text = Yhome_vel1;

                CommParam.Yhome_offset = Yhome_offset1;
                this.Yhome_offset.Text = Yhome_offset1;

                CommParam.Y2search_home = Y2search_home1;
                this.Y2search_home.Text = Y2search_home1;

                CommParam.Y2home_acc = Convert.ToDouble(Y2home_acc1);
                this.Y2home_acc.Text = Y2home_acc1;

                CommParam.Y2home_dec = Convert.ToDouble(Y2home_dec1);
                this.Y2home_dec.Text = Y2home_dec1;

                CommParam.Y2home_vel = Convert.ToDouble(Y2home_vel1);
                this.Y2home_vel.Text = Y2home_vel1;

                CommParam.Y2home_offset = Y2home_offset1;
                this.Y2home_offset.Text = Y2home_offset1;

                CommParam.Zsearch_home = Zsearch_home1;
                this.Zsearch_home.Text = Zsearch_home1;

                CommParam.Zhome_acc = Convert.ToDouble(Zhome_acc1);
                this.Zhome_acc.Text = Zhome_acc1;

                CommParam.Zhome_dec = Convert.ToDouble(Zhome_dec1);
                this.Zhome_dec.Text = Zhome_dec1;

                CommParam.Zhome_vel = Convert.ToDouble(Zhome_vel1);
                this.Zhome_vel.Text = Zhome_vel1;

                CommParam.Zhome_offset = Zhome_offset1;
                this.Zhome_offset.Text = Zhome_offset1;

                CommParam.CB_Vel = Convert.ToDouble(CB_Vel1);
                this.CB_Vel.Text = CB_Vel1;

                CommParam.CB_Acc = Convert.ToDouble(CB_Acc1);
                this.CB_Acc.Text = CB_Acc1;

                CommParam.CB_Dec = Convert.ToDouble(CB_Dec1);
                this.CB_Dec.Text = CB_Dec1;

                CommParam.CB_SmoothTime = Convert.ToInt32(CB_SmoothTime1);
                this.CB_SmoothTime.Text = CB_SmoothTime1;

                CommParam.ZBX_SynVelMax = Convert.ToDouble(ZBX_SynVelMax1);
                this.ZBX_SynVelMax.Text = ZBX_SynVelMax1;

                CommParam.ZBX_SynAccMax = Convert.ToDouble(ZBX_SynAccMax1);
                this.ZBX_SynAccMax.Text = ZBX_SynAccMax1;

                CommParam.ZBX_EvenTime = (short)Convert.ToInt32(ZBX_EvenTime1);
                this.ZBX_EvenTime.Text = ZBX_EvenTime1;


                CommParam.XSD_Vel = Convert.ToDouble(XSD_Vel1);
                this.XSD_Vel.Text = XSD_Vel1;

                CommParam.XSD_Acc = Convert.ToDouble(XSD_Acc1);
                this.XSD_Acc.Text = XSD_Acc1;

                CommParam.XSD_Dec = Convert.ToDouble(XSD_Dec1);
                this.XSD_Dec.Text = XSD_Dec1;

                CommParam.YSD_Vel = Convert.ToDouble(YSD_Vel1);
                this.YSD_Vel.Text = YSD_Vel1;

                CommParam.YSD_Acc = Convert.ToDouble(YSD_Acc1);
                this.YSD_Acc.Text = YSD_Acc1;

                CommParam.YSD_Dec = Convert.ToDouble(YSD_Dec1);
                this.YSD_Dec.Text = YSD_Dec1;

                CommParam.ZSD_Vel = Convert.ToDouble(ZSD_Vel1);
                this.ZSD_Vel.Text = ZSD_Vel1;

                CommParam.ZSD_Acc = Convert.ToDouble(ZSD_Acc1);
                this.ZSD_Acc.Text = ZSD_Acc1;

                CommParam.ZSD_Dec = Convert.ToDouble(ZSD_Dec1);
                this.ZSD_Dec.Text = ZSD_Dec1;

                CommParam.Y2SD_Acc = Convert.ToDouble(Y2SD_Acc1);
                this.Y2SD_Acc.Text = Y2SD_Acc1;

                CommParam.Y2SD_Dec = Convert.ToDouble(Y2SD_Dec1);
                this.Y2SD_Dec.Text = Y2SD_Dec1;

                CommParam.Z_Up = CommUtil.Cir_To_Pulse(z_nu_run1);
                this.z_nu_run.Text = z_nu_run1;

                CommParam.CB_Z_Vel = Convert.ToDouble(CB_Z_Vel1);
                this.CB_Z_Vel.Text = CB_Z_Vel1;

                CommParam.Ddr = ddr_uiTextBox1;
                this.ddr_uiTextBox.Text = ddr_uiTextBox1;

                CommParam.Ddl = ddl_uiTextBox1;
                this.ddl_uiTextBox.Text = ddl_uiTextBox1;


                #region

                //CommParam.Xsearch_home = All_param[0];
                //    this.Xsearch_home.Text = All_param[0];

                //    CommParam.Xhome_acc = Convert.ToDouble(All_param[1]);
                //    this.Xhome_acc.Text = All_param[1];

                //    CommParam.Xhome_dec = Convert.ToDouble(All_param[2]);
                //    this.Xhome_dec.Text = All_param[2];

                //    CommParam.Xhome_vel = Convert.ToDouble(All_param[3]);
                //    this.Xhome_vel.Text = All_param[3];

                //    CommParam.Xhome_offset = All_param[4];
                //    this.Xhome_offset.Text = All_param[4];

                //    CommParam.Ysearch_home = All_param[5];
                //    this.Ysearch_home.Text = All_param[5];

                //    CommParam.Yhome_acc = Convert.ToDouble(All_param[6]);
                //    this.Yhome_acc.Text = All_param[6];

                //    CommParam.Yhome_dec = Convert.ToDouble(All_param[7]);
                //    this.Yhome_dec.Text = All_param[7];

                //    CommParam.Yhome_vel = Convert.ToDouble(All_param[8]);
                //    this.Yhome_vel.Text = All_param[8];

                //    CommParam.Yhome_offset = All_param[9];
                //    this.Yhome_offset.Text = All_param[9];

                //    CommParam.Y2search_home = All_param[10];
                //    this.Y2search_home.Text = All_param[10];

                //    CommParam.Y2home_acc = Convert.ToDouble(All_param[11]);
                //    this.Y2home_acc.Text = All_param[11];

                //    CommParam.Y2home_dec = Convert.ToDouble(All_param[12]);
                //    this.Y2home_dec.Text = All_param[12];

                //    CommParam.Y2home_vel = Convert.ToDouble(All_param[13]);
                //    this.Y2home_vel.Text = All_param[13];

                //    CommParam.Y2home_offset = All_param[14];
                //    this.Y2home_offset.Text = All_param[14];

                //    CommParam.Zsearch_home = All_param[15];
                //    this.Zsearch_home.Text = All_param[15];

                //    CommParam.Zhome_acc = Convert.ToDouble(All_param[16]);
                //    this.Zhome_acc.Text = All_param[16];

                //    CommParam.Zhome_dec = Convert.ToDouble(All_param[17]);
                //    this.Zhome_dec.Text = All_param[17];

                //    CommParam.Zhome_vel = Convert.ToDouble(All_param[18]);
                //    this.Zhome_vel.Text = All_param[18];

                //    CommParam.Zhome_offset = All_param[19];
                //    this.Zhome_offset.Text = All_param[19];

                //    CommParam.CB_Vel = Convert.ToDouble(All_param[20]);
                //    this.CB_Vel.Text = All_param[20];

                //    CommParam.CB_Acc = Convert.ToDouble(All_param[21]);
                //    this.CB_Acc.Text = All_param[21];

                //    CommParam.CB_Dec = Convert.ToDouble(All_param[22]);
                //    this.CB_Dec.Text = All_param[22];

                //    CommParam.CB_SmoothTime = Convert.ToInt32(All_param[23]);
                //    this.CB_SmoothTime.Text = All_param[23];

                //    CommParam.ZBX_SynVelMax = Convert.ToDouble(All_param[24]);
                //    this.ZBX_SynVelMax.Text = All_param[24];

                //    CommParam.ZBX_SynAccMax = Convert.ToDouble(All_param[25]);
                //    this.ZBX_SynAccMax.Text = All_param[25];

                //    CommParam.ZBX_EvenTime = (short)Convert.ToInt32(All_param[26]);
                //    this.ZBX_EvenTime.Text = All_param[26];


                //    CommParam.XSD_Vel = Convert.ToDouble(All_param[27]);
                //    this.XSD_Vel.Text = All_param[27];

                //    CommParam.XSD_Acc = Convert.ToDouble(All_param[28]);
                //    this.XSD_Acc.Text = All_param[28];

                //    CommParam.XSD_Dec = Convert.ToDouble(All_param[29]);
                //    this.XSD_Dec.Text = All_param[29];

                //    CommParam.YSD_Vel = Convert.ToDouble(All_param[30]);
                //    this.YSD_Vel.Text = All_param[30];

                //    CommParam.YSD_Acc = Convert.ToDouble(All_param[31]);
                //    this.YSD_Acc.Text = All_param[31];

                //    CommParam.YSD_Dec = Convert.ToDouble(All_param[32]);
                //    this.YSD_Dec.Text = All_param[32];

                //    CommParam.ZSD_Vel = Convert.ToDouble(All_param[33]);
                //    this.ZSD_Vel.Text = All_param[33];

                //    CommParam.ZSD_Acc = Convert.ToDouble(All_param[34]);
                //    this.ZSD_Acc.Text = All_param[34];

                //    CommParam.ZSD_Dec = Convert.ToDouble(All_param[35]);
                //    this.ZSD_Dec.Text = All_param[35];

                //    CommParam.Y2SD_Acc = Convert.ToDouble(All_param[36]);
                //    this.Y2SD_Acc.Text = All_param[36];

                //    CommParam.Y2SD_Dec = Convert.ToDouble(All_param[37]);
                //    this.Y2SD_Dec.Text = All_param[37];

                //    CommParam.Z_Up =CommUtil.Cir_To_Pulse (All_param[38]);
                //    this.z_nu_run.Text = All_param[38];

                //    CommParam.CB_Z_Vel = Convert.ToDouble(All_param[39]);
                //    this.CB_Z_Vel.Text = All_param[39];

                //    CommParam.Ddr = All_param[40];
                //    this.ddr_uiTextBox.Text = All_param[40];

                //    CommParam.Ddl =All_param[41];
                //    this.ddl_uiTextBox.Text = All_param[41];


                //}
                //else
                //{
                //    ShowErrorDialog("读取轴参数失败，请联系技术人员");
                //}
                #endregion
            }
            catch (Exception x)
            {
                LogHelper.WriteErrorLog("读取参数 操作异常 ", x);
                //Logger.Recod_Log_File("form1 Read_Param  " + x.Message + "\r\n x.StackTrace" + x.StackTrace);
                ShowErrorDialog(" 读取参数 操作异常");
            }
        }

        private void read_aixs_param_Click(object sender, EventArgs e)
        {
            try
            {
                Read_Param();
            }
            catch (Exception x)
            {
                LogHelper.WriteErrorLog(" 读取机械参数 操作异常 ", x);
                //  Logger.Recod_Log_File("  button7_Click " + x.Message + "\r\n x.StackTrace" + x.StackTrace);
                ShowErrorDialog(" 读取机械参数操作异常");
            }
        }

        private void save_aixs_param_Click(object sender, EventArgs e)
        {
            if (Goog.gpi_status(6))
            {
                ShowErrorDialog("请切换到手动状态");
                return;
            }
            //FileUtil f = new FileUtil();
            //string param = "";
            //param = Xsearch_home.Text + "," + Xhome_acc.Text + "," + Xhome_dec.Text + "," + Xhome_vel.Text + "," + Xhome_offset.Text + "," +
            //    Ysearch_home.Text + "," + Yhome_acc.Text + "," + Yhome_dec.Text + "," + Yhome_vel.Text + "," + Yhome_offset.Text + "," +
            //    Y2search_home.Text + "," + Y2home_acc.Text + "," + Y2home_dec.Text + "," + Y2home_vel.Text + "," + Y2home_offset.Text + "," +
            //    Zsearch_home.Text + "," + Zhome_acc.Text + "," + Zhome_dec.Text + "," + Zhome_vel.Text + "," + Zhome_offset.Text + "," +
            //    CB_Vel.Text + "," + CB_Acc.Text + "," + CB_Dec.Text + "," + CB_SmoothTime.Text + "," + ZBX_SynVelMax.Text + "," +
            //    ZBX_SynAccMax.Text + "," + ZBX_EvenTime.Text + "," + XSD_Vel.Text + "," + XSD_Acc.Text + "," + XSD_Dec.Text
            //     + "," + YSD_Vel.Text + "," + YSD_Acc.Text + "," + YSD_Dec.Text + "," + ZSD_Vel.Text + "," + ZSD_Acc.Text
            //      + "," + ZSD_Dec.Text + "," + Y2SD_Acc.Text + "," + Y2SD_Dec.Text+","+ z_nu_run.Text + "," + CB_Z_Vel.Text + "," + ddr_uiTextBox.Text + "," + ddl_uiTextBox.Text; 
            try
            {
                iniFunctoin.WriteStringToIni("X_axis_parameters", "Xsearch_home", Xsearch_home.Text, CommParam.Param_Path).ToString();
                iniFunctoin.WriteStringToIni("X_axis_parameters", "Xhome_acc", Xhome_acc.Text, CommParam.Param_Path).ToString();
                iniFunctoin.WriteStringToIni("X_axis_parameters", "Xhome_dec", Xhome_dec.Text, CommParam.Param_Path).ToString();
                iniFunctoin.WriteStringToIni("X_axis_parameters", "Xhome_vel", Xhome_vel.Text, CommParam.Param_Path).ToString();
                iniFunctoin.WriteStringToIni("X_axis_parameters", "Xhome_offset", Xhome_offset.Text, CommParam.Param_Path).ToString();

                iniFunctoin.WriteStringToIni("Y_axis_parameters", "Ysearch_home", Ysearch_home.Text, CommParam.Param_Path).ToString();
                iniFunctoin.WriteStringToIni("Y_axis_parameters", "Yhome_acc", Yhome_acc.Text, CommParam.Param_Path).ToString();
                iniFunctoin.WriteStringToIni("Y_axis_parameters", "Yhome_dec", Yhome_dec.Text, CommParam.Param_Path).ToString();
                iniFunctoin.WriteStringToIni("Y_axis_parameters", "Yhome_vel", Yhome_vel.Text, CommParam.Param_Path).ToString();
                iniFunctoin.WriteStringToIni("Y_axis_parameters", "Yhome_offset", Yhome_offset.Text, CommParam.Param_Path).ToString();

                iniFunctoin.WriteStringToIni("Y2_axis_parameters", "Y2search_home", Y2search_home.Text, CommParam.Param_Path).ToString();
                iniFunctoin.WriteStringToIni("Y2_axis_parameters", "Y2home_acc", Y2home_acc.Text, CommParam.Param_Path).ToString();
                iniFunctoin.WriteStringToIni("Y2_axis_parameters", "Y2home_dec", Y2home_dec.Text, CommParam.Param_Path).ToString();
                iniFunctoin.WriteStringToIni("Y2_axis_parameters", "Y2home_vel", Y2home_vel.Text, CommParam.Param_Path).ToString();
                iniFunctoin.WriteStringToIni("Y2_axis_parameters", "Y2home_offset", Y2home_offset.Text, CommParam.Param_Path).ToString();

                iniFunctoin.WriteStringToIni("Z_axis_parameters", "Zsearch_home", Zsearch_home.Text, CommParam.Param_Path).ToString();
                iniFunctoin.WriteStringToIni("Z_axis_parameters", "Zhome_acc", Zhome_acc.Text, CommParam.Param_Path).ToString();
                iniFunctoin.WriteStringToIni("Z_axis_parameters", "Zhome_dec", Zhome_dec.Text, CommParam.Param_Path).ToString();
                iniFunctoin.WriteStringToIni("Z_axis_parameters", "Zhome_vel", Zhome_vel.Text, CommParam.Param_Path).ToString();
                iniFunctoin.WriteStringToIni("Z_axis_parameters", "Zhome_offset", Zhome_offset.Text, CommParam.Param_Path).ToString();

                iniFunctoin.WriteStringToIni("CB_parameters", "CB_Vel", CB_Vel.Text, CommParam.Param_Path).ToString();
                iniFunctoin.WriteStringToIni("CB_parameters", "CB_Acc", CB_Acc.Text, CommParam.Param_Path).ToString();
                iniFunctoin.WriteStringToIni("CB_parameters", "CB_Dec", CB_Dec.Text, CommParam.Param_Path).ToString();
                iniFunctoin.WriteStringToIni("CB_parameters", "CB_SmoothTime", CB_SmoothTime.Text, CommParam.Param_Path).ToString();

                iniFunctoin.WriteStringToIni("XY_parameters", "ZBX_SynVelMax", ZBX_SynVelMax.Text, CommParam.Param_Path).ToString();
                iniFunctoin.WriteStringToIni("XY_parameters", "ZBX_SynAccMax", ZBX_SynAccMax.Text, CommParam.Param_Path).ToString();
                iniFunctoin.WriteStringToIni("XY_parameters", "ZBX_EvenTime", ZBX_EvenTime.Text, CommParam.Param_Path).ToString();
                iniFunctoin.WriteStringToIni("XY_parameters", "CB_Z_Vel", CB_Z_Vel.Text, CommParam.Param_Path).ToString();

                iniFunctoin.WriteStringToIni("X_Manual_parameter", "XSD_Vel", XSD_Vel.Text, CommParam.Param_Path).ToString();
                iniFunctoin.WriteStringToIni("X_Manual_parameter", "XSD_Acc", XSD_Acc.Text, CommParam.Param_Path).ToString();
                iniFunctoin.WriteStringToIni("X_Manual_parameter", "XSD_Dec", XSD_Dec.Text, CommParam.Param_Path).ToString();

                iniFunctoin.WriteStringToIni("Y_Manual_parameter", "YSD_Vel", YSD_Vel.Text, CommParam.Param_Path).ToString();
                iniFunctoin.WriteStringToIni("Y_Manual_parameter", "YSD_Acc", YSD_Acc.Text, CommParam.Param_Path).ToString();
                iniFunctoin.WriteStringToIni("Y_Manual_parameter", "YSD_Dec", YSD_Dec.Text, CommParam.Param_Path).ToString();

                iniFunctoin.WriteStringToIni("Z_Manual_parameter", "ZSD_Vel ", ZSD_Vel.Text, CommParam.Param_Path).ToString();
                iniFunctoin.WriteStringToIni("Z_Manual_parameter", "ZSD_Acc ", ZSD_Acc.Text, CommParam.Param_Path).ToString();
                iniFunctoin.WriteStringToIni("Z_Manual_parameter", "ZSD_Dec ", ZSD_Dec.Text, CommParam.Param_Path).ToString();

                iniFunctoin.WriteStringToIni("Y2_Manual_parameter", "Y2SD_Acc", Y2SD_Acc.Text, CommParam.Param_Path).ToString();
                iniFunctoin.WriteStringToIni("Y2_Manual_parameter", "Y2SD_Dec", Y2SD_Dec.Text, CommParam.Param_Path).ToString();

                iniFunctoin.WriteStringToIni("Duidao_parameter", "z_nu_run", z_nu_run.Text, CommParam.Param_Path).ToString();
                iniFunctoin.WriteStringToIni("Duidao_parameter", "ddr_uiTextBox", ddr_uiTextBox.Text, CommParam.Param_Path).ToString();
                iniFunctoin.WriteStringToIni("Duidao_parameter", "ddl_uiTextBox", ddl_uiTextBox.Text, CommParam.Param_Path).ToString();
                //f.Del_File(CommParam.Param_Path);
                //bool status = f.Save_File(CommParam.Param_Path, param);
                //if (status)
                //{
                ShowSuccessDialog("保存成功!");
                Read_Param();
                //}
                //else
                //{
                //    ShowErrorDialog("保存失败!");
                //}
            }
            catch (Exception x)
            {
                LogHelper.WriteErrorLog("保存参数 操作异常 ", x);
                // Logger.Recod_Log_File("form1 button10_Click_1  " + x.Message + "\r\n x.StackTrace" + x.StackTrace);
                ShowErrorDialog(" 保存参数 操作异常");
            }
        }

        private void Z_Nu_Run_uiButton_Click(object sender, EventArgs e)
        {
           // if (!ShowAskDialog("确定更新吗？")) return;
            z_nu_run.Text=CommUtil.Pulse_To_Cir( CommParam.Z_Pos);  //当前脉冲对应的距离
        }

        private void DuiDaoR_uiButton_Click(object sender, EventArgs e)
        {
           // 23950_-2446_3956
            ddr_uiTextBox.Text = CommParam.X_Pos+"_"+ CommParam.Y_Pos + "_" + CommParam.Z_Pos;
        }

        private void DuiDaoL_uiButton_Click(object sender, EventArgs e)
        {
            //76990_-2446_3956
            ddl_uiTextBox.Text = CommParam.X_Pos + "_" + CommParam.Y_Pos + "_" + CommParam.Z_Pos;
        }

        private void uiButton1_Click(object sender, EventArgs e)
        {
            Business b = new Business();
            b.dui_Dao();
        }

        private void uiButton2_Click(object sender, EventArgs e)
        {
            toolSetting.SaveSpeed(Dd_speed_Vel.Text, Dd_speed_Acc.Text, Dd_speed_End.Text);

        }
    }
}
