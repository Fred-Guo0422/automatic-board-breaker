using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Ports;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Runtime.ExceptionServices;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using Cognex.VisionPro;
using Cognex.VisionPro.ToolBlock;
using Cognex.VisionPro.CalibFix;

namespace _2021LY
{
    
    public static  class Vision
    {
        public static CogToolBlock Inspection, CalibrationBlock, Mark1, Mark2, StandardBlock, CurrentBlock, RunBlock;
        public static CogCalibNPointToNPointTool Calib, CalibCaMark1, CalibCaMark2, CalibRun;
        public static string path = Application.StartupPath + "\\Vpp\\" + "Inspection.vpp";
        public static CogAcqFifoTool AcqTool;
        //public static CogToolBlock StandardBlock, CurrentBlock;
        /// <summary>
        /// 加载Vpp
        /// </summary>
        /// 
        public static int Circle, Wide, High;
        public static void LoadVpp()
        {
            try
            {
              
                Inspection = CogSerializer.LoadObjectFromFile(path) as CogToolBlock;
                AcqTool = Inspection.Tools[0] as CogAcqFifoTool;
                CalibrationBlock = Inspection.Tools[1] as CogToolBlock;
                Mark1 = Inspection.Tools[2] as CogToolBlock;
                Mark2 = Inspection.Tools[3] as CogToolBlock;
                StandardBlock = Inspection.Tools[4] as CogToolBlock;
                CurrentBlock = Inspection.Tools[5] as CogToolBlock;
               // StandardBlock = RunBlock.Tools[1] as CogToolBlock;
               // CurrentBlock = RunBlock.Tools[2] as CogToolBlock;

                Calib = CalibrationBlock.Tools[0] as CogCalibNPointToNPointTool;
                CalibCaMark1= Mark1.Tools[0] as CogCalibNPointToNPointTool;
                CalibCaMark2 = Mark2.Tools[0] as CogCalibNPointToNPointTool;
               // CalibRun= RunBlock.Tools[0] as CogCalibNPointToNPointTool;

                /******画圆形和矩形改*******************/
                Circle = (int)Inspection.Inputs[0].Value;
                Wide = (int)Inspection.Inputs[1].Value;
                High = (int)Inspection.Inputs[2].Value;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message+"Vpp加载错误","错误提示",MessageBoxButtons.OK,MessageBoxIcon.Error);
                
            }
           
        }

        public static int SaveVpp()
        {
            try
            {
                CogSerializer.SaveObjectToFile(Inspection,path);

                return 1;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + "Vpp保存错误", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
        }

    }
}
