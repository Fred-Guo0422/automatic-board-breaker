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
    public partial class VisionDesignForm : UITitlePage
    {
      
        public VisionDesignForm()
        {
            InitializeComponent();
            this.Text = "视觉程序";
           cogToolBlockEditV21.Subject = Vision.Inspection; //视觉程序显示
           
        }

        private static VisionDesignForm visionDesignForm;

        public static VisionDesignForm getForm()
        {
            if (visionDesignForm == null)
            {
                visionDesignForm = new VisionDesignForm();
            }
       
            return visionDesignForm;
        }

        private void btnSave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                /******画圆形和矩形改*******************/
                Vision.Inspection = this.cogToolBlockEditV21.Subject;
                Vision.SaveVpp();
                Vision.LoadVpp();
                MessageBox.Show("程序保存成功", "提示", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + "程序保存失败", "提示");
            }
            this.Cursor = Cursors.Arrow;

        }
    }
}
