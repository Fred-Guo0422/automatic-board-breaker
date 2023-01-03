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
using System.IO;
using Cognex.VisionPro;
namespace _2021LY.Forms
{
    public partial class VisionCalibra : UITitlePage
    {
      public static   string[] Data = new string[6];
        String IniPath = Application.StartupPath + "\\相机切刀差\\" + "Point.txt";
        string[] DataGriew = new string[41];
        String NinePointPath=Application.StartupPath + "\\9点标定\\" + "9点Point.txt";
        List<string> NineDa = new List<string>();

        public static double DistanceX, DistanceY;
        public static VisionCalibra Calia;

        public static string Position;
        public VisionCalibra()
        {

            this.FormClosing += VisionCalibraForm_Close;
            InitializeComponent();
            this.Text = "视觉标定";
            ReadPointData();
            ReadNinePoint();
            UIDataGriew.RowCount = 10;
            for (int i = 1; i < 10; i++)
            {
                UIDataGriew.Rows[i-1].Cells[0].Value = i;
            }
            Calia = this;
        }
        private void VisionCalibraForm_Close(object sender, EventArgs e)
        {
            try
            {
              
                Application.Exit();
                Environment.Exit(0);
                //serialPort1.Close();
            }
            catch (Exception ex)
            {

                //Addmessage(ex.Message + "串口关闭");
            }
        }

        private static VisionCalibra visionCalibra;

        public static VisionCalibra getForm()
        {
            if (visionCalibra == null)
            {
                visionCalibra = new VisionCalibra();
            }
            return visionCalibra;
        }

        private void btnGetCamera(object sender, EventArgs e)
        {
          
            try
            {
                CamerPosX.Text = CommUtil.Pulse_To_Cir(CommParam.X_Pos).ToString();
                CamerPosY.Text = CommUtil.Pulse_To_Cir(CommParam.Y_Pos).ToString();
                Data[0] = "相机位置X:" + CamerPosX.Text;
                Data[1] = "相机位置Y:" + CamerPosY.Text;
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
          
        }
        public static string[] GetCameraPos(string x, string y)
        {

            double xx = double.Parse(x);
            double yy = double.Parse(y);

            double dx = double.Parse(Calia.DiffPosX.Text);
            double dy = double.Parse(Calia.DiffPosY.Text);

            string[] marks = { xx + dx + "", yy + dy + "" };
            return marks;

        }

        private void btnGetPosCut(object sender, EventArgs e)
        {
            try
            {
                CutPosX.Text = CommUtil.Pulse_To_Cir(CommParam.X_Pos).ToString();
                CutPosY.Text = CommUtil.Pulse_To_Cir(CommParam.Y_Pos).ToString();
                Data[2] = "切刀位置X:" + CutPosX.Text;
                Data[3] = "切刀位置Y:" + CutPosY.Text;
              
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

            }
          
        }

       
        private void btnCal(object sender, EventArgs e)
        {
            try
            {
                DiffPosX.Text = (Convert.ToDouble(CamerPosX.Text) - Convert.ToDouble(CutPosX.Text)).ToString();
                DiffPosY.Text = (Convert.ToDouble(CamerPosY.Text) - Convert.ToDouble(CutPosY.Text)).ToString();
                Data[4] = "差异X:" + DiffPosX.Text;
                Data[5] = "差异Y:" + DiffPosY.Text;
              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        private void SavePonitData(string[]Content)
        {
            try
            {
                File.WriteAllLines(IniPath, Content);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void ReadPointData()
        {
            string[] result = new string[12];
            int i = 0;
            try
            {
                String[] DataPoint = File.ReadAllLines(IniPath);
                foreach (var item in DataPoint)
                {
                    i++;
                    string Value = item.Split(':')[1];
                    result[i] = Value;
                }

                CamerPosX.Text = result[1];
                CamerPosY.Text = result[2];
                CutPosX.Text = result[3];
                CutPosY.Text = result[4];
                DiffPosX.Text= result[5];
                DiffPosY.Text = result[6];



                DistanceX =Convert.ToDouble(result[5]);
                DistanceY = Convert.ToDouble(result[6]);


                Data[0] = "相机位置X:" + CamerPosX.Text;
                Data[1] = "相机位置Y:" + CamerPosY.Text;
                Data[2] = "切刀位置X:" + CutPosX.Text;
                Data[3] = "切刀位置Y:" + CutPosY.Text;
                Data[4] = "差异X:" + DiffPosX.Text;
                Data[5] = "差异Y:" + DiffPosY.Text;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void GetPosCurCam(object sender, EventArgs e)
        {
            try
            {
                CurrentCamX.Text = CommUtil.Pulse_To_Cir(CommParam.X_Pos).ToString();
                CurrentCamY.Text = CommUtil.Pulse_To_Cir(CommParam.Y_Pos).ToString();

                CurrentCutX.Text = (Convert.ToDouble(CurrentCamX.Text) - Convert.ToDouble(Data[4])).ToString();
                CurrentCutY.Text = (Convert.ToDouble(CurrentCamY.Text) - Convert.ToDouble(Data[5])).ToString();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
           
        }



        /********************************标定开始******************/
        private void btnStartCal(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < 9; i++)
                {
                    /*******移动位置预留1-9**********/
                    System.Threading.Thread.Sleep(500);
                    Vision.AcqTool.Run();
                    Vision.CalibrationBlock.Run();
                    CaliDisply.Record = Vision.CalibrationBlock.CreateLastRunRecord().SubRecords[1];
                    CaliDisply.Fit();
                    Vision.Calib.Calibration.SetUncalibratedPoint(i, (double)Vision.CalibrationBlock.Outputs[0].Value, (double)Vision.CalibrationBlock.Outputs[1].Value);
                    Vision.Calib.Calibration.SetRawCalibratedPoint(i, Convert.ToDouble(CommUtil.Pulse_To_Cir(CommParam.X_Pos)), Convert.ToDouble(CommUtil.Pulse_To_Cir(CommParam.Y_Pos)));  // 填入当前世界坐标X，Y；

                    UIDataGriew.Rows[i].Cells[1].Value = Convert.ToDouble(Vision.CalibrationBlock.Outputs[0].Value).ToString("f3");
                    UIDataGriew.Rows[i].Cells[2].Value = Convert.ToDouble(Vision.CalibrationBlock.Outputs[1].Value).ToString("f3");

                    UIDataGriew.Rows[i].Cells[3].Value = CommUtil.Pulse_To_Cir(CommParam.X_Pos);       //世界坐标X
                    UIDataGriew.Rows[i].Cells[4].Value = CommUtil.Pulse_To_Cir(CommParam.Y_Pos);       // 世界坐标Y 

                    if (i == 8)
                    {
                        Vision.Calib.Calibration.Calibrate();
                        Vision.CalibCaMark1.Calibration = Vision.Calib.Calibration;
                        Vision.CalibCaMark2.Calibration = Vision.Calib.Calibration;
                        Vision.CalibRun.Calibration = Vision.Calib.Calibration;

                        CogTransform2DLinear Liner;
                        Liner = (CogTransform2DLinear)Vision.Calib.Calibration.GetComputedUncalibratedFromCalibratedTransform();

                        if (Vision.Calib.Calibration.Calibrated && Liner != null)
                        {
                            MoveX.Text = Liner.TranslationX.ToString("0.000");
                            MoveY.Text = Liner.TranslationY.ToString("0.000");
                            Scale.Text = Liner.Scaling.ToString("0.000");
                            Skew.Text = Liner.Skew.ToString("0.000");
                            Roation.Text = Liner.Rotation.ToString("0.000");
                            Rms.Text = Vision.Calib.Calibration.ComputedRMSError.ToString("0.000");
                        }
                        //保存标定数据
                        int Number = 0;
                        for (int ii = 1; ii < 5; ii++)
                        {
                            for (int Y = 0; Y < 9; Y++)
                            {

                                DataGriew[Number] = UIDataGriew.Rows[Y].Cells[ii].Value.ToString();
                                Number++;
                            }
                        }
                        NineDa.Clear();
                        NineDa.AddRange(DataGriew);
                        NineDa.Add(MoveX.Text);
                        NineDa.Add(MoveY.Text);
                        NineDa.Add(Scale.Text);
                        NineDa.Add(Skew.Text);
                        NineDa.Add(Roation.Text);
                        NineDa.Add(Rms.Text);
                        SaveNinePonit(NineDa.ToArray());
                    }
                }                           
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message+"9点保存");
            }
          
        }
        private void SaveNinePonit(string[] Content)
        {
            try
            {
                
                File.WriteAllLines(NinePointPath, Content);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message+"保存数据出错");
            }
        }

        private void ReadNinePoint()
        {
            try
            {
                UIDataGriew.RowCount = 10;
                DataGriew = File.ReadAllLines(NinePointPath);
                int Nm = 0;
                for (int i = 1; i < 5; i++)
                {
                    for (int Y = 0; Y < 9; Y++)
                    {
                        UIDataGriew.Rows[Y].Cells[i].Value = DataGriew[Nm];
                        Nm++;
                    }
                }
                MoveX.Text = DataGriew[36];
                MoveY.Text = DataGriew[37];
                Scale.Text = DataGriew[38];
                Skew.Text = DataGriew[39];
                Roation.Text = DataGriew[40];
                Rms.Text = DataGriew[41];
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message+"9点数据读取出错");
            }         
        }
        //保存
        private void uiButton6_Click(object sender, EventArgs e)
        {
            SavePonitData(Data);
        }

        private void uiButton7_Click(object sender, EventArgs e)
        {
           var Resutlt= MES.ReadID();
        }

        public static string ChangeVision()
        {
            try
            {
                Calia.CurrentCamX.Text = CommUtil.Pulse_To_Cir(CommParam.X_Pos).ToString();
                Calia.CurrentCamY.Text = CommUtil.Pulse_To_Cir(CommParam.Y_Pos).ToString();

                Calia.CurrentCutX.Text = (Convert.ToDouble(Calia.CurrentCamX.Text) - Convert.ToDouble(Calia.DiffPosX.Text)).ToString();
                Calia.CurrentCutY.Text = (Convert.ToDouble(Calia.CurrentCamY.Text) - Convert.ToDouble(Calia.DiffPosY.Text)).ToString();

                Position = (Calia.CurrentCutX.Text) + "," + Calia.CurrentCutY.Text;

                return Position;
            }
            catch (Exception)
            {

                MessageBox.Show("视觉点位转换出错");
                return "Error";
            }

        }
    }
}
