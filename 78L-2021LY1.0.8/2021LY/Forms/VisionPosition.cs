using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sunny.UI;
using System.IO;
namespace _2021LY.Forms
{
    public partial class VisionPosition : UITitlePage
    {
        string ProductPath = Application.StartupPath + "\\产品选择\\" + "所有产品.txt";
        public static Boolean Vp = true;
        //假设最多100个点
        double[] UplimitX = new double[100];
        double[] DownlimitX = new double[100];

        double[] UplimitY = new double[100];
        double[] DownlimitY = new double[100];

        public static   string[]Distance=new string[200];
        List<string> DistanceList = new List<string>();

       double[]Standard=new double[100];
       public static string[] ReStan = new string[100];
       public static int Count;

       double StandardX1, StandardY1, StandardX2, StandardY2;

       double[] DistanceX=  new double[100];
       double[] DistanceY = new double[100];

       public static  string[] Namerer = new string[1];

       List<string> CurrentPoint = new List<string>();
        public VisionPosition()
        {
            InitializeComponent();
            this.Text = "视觉点位";
            ReadProduct();
            this.uiDataGridView1.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataDoubleClick);   //双击写入左侧表格点位
           

            this.uiDataGridView2.RowCount = 100;
            this.uiDataGridView2.Rows[0].Cells[0].Value = "Mark1点";
            this.uiDataGridView2.Rows[1].Cells[0].Value = "Mark2点";
            this.uiDataGridView2.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataDoubleClick2);   //双击写入右侧表格点位
      
        }
        public static VisionPosition visionPosition;
        public static VisionPosition getForm()
        {
            if (visionPosition == null)
            {
                visionPosition = new VisionPosition();
            }
            return visionPosition;
        }

        private void ReadProduct()
        {
            try
            {
                产品编号.Items.Clear();
                string[] Product = File.ReadAllLines(ProductPath);
                foreach (var item in Product)
                {
                    产品编号.Items.Add(item);
                }
               var Text= File.ReadAllLines(Application.StartupPath + "\\产品选择\\" + "当前产品.txt");
               产品编号.Text = Text[0];
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
           
        }
        private void ReadParamater(string  file)
        {
            if (!File.Exists(Application.StartupPath + "\\标准点位\\" + file + ".txt"))
            {

                File.Create(Application.StartupPath + "\\标准点位\\" + file + ".txt").Close();
           }
        string[] Point=  File.ReadAllLines(Application.StartupPath + "\\标准点位\\" +file + ".txt");
        if (Point.Length>0)
        {
            int v = int.Parse(Point[0]);
            uiDataGridView1.RowCount = v;
            int Number = 0;
            for (int i = 0; i < v; i++)
            {
                for (int y = 0; y < 7; y++)
                {
                    Number++;
                    uiDataGridView1.Rows[i].Cells[y].Value = Point[Number];
                }

            } 
        }      
       }

        private void 产品编号_TextChanged(object sender, EventArgs e)
        {
            try
            {

                Namerer[0] = 产品编号.Text;
                /***根据不同名称选择不同产品点位，程序********/
                uiDataGridView1.Rows.Clear();
                ReadParamater(Namerer[0]);

                MoveStand();
                ReaadStandard();

                File.WriteAllLines(Application.StartupPath + "\\产品选择\\" + "当前产品.txt", Namerer);
                //Distance = File.ReadAllLines(Application.StartupPath + "\\Distance\\" + Namerer[0] + ".txt");   //读取当前产品的2个distance;
               // ReadMark();                                                                                       //读取当前产品的2个mark点;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + "产品切换出错", "错误提示");
            }
        }

        /// <summary>
        /// 增加行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Addbtn(object sender, EventArgs e)
        {
            uiDataGridView1.Rows.Add();
        }
        /// <summary>
        /// 插入行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Insertbtn(object sender, EventArgs e)
        {
            uiDataGridView1.Rows.Insert(uiDataGridView1.CurrentCell.RowIndex, 1);
        }
        /// <summary>
        /// 删除行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Deletebtn(object sender, EventArgs e)
        {
            try
            {
                uiDataGridView1.Rows.RemoveAt(uiDataGridView1.CurrentCell.RowIndex);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + "最后一行不能删除");
            }
           
        }
        /// <summary>
        /// 双击写入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DataDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int Number = uiDataGridView1.CurrentCell.RowIndex;
            int Data = e.ColumnIndex;
            if (Data==0)
            {
                DialogResult t = MessageBox.Show("是否写入点位?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                ///写入X，Y
                if (t == DialogResult.OK)
                {
                    //写入当前转化后位置
                    uiDataGridView1.Rows[Number].Cells[1].Value = Number + "X" + VisionCalibra.DistanceX;
                    uiDataGridView1.Rows[Number].Cells[2].Value = Number + "Y" + VisionCalibra.DistanceY;
                }
            }          
        }

        public void DataDoubleClick2(object sender, DataGridViewCellMouseEventArgs e)
        {
            int Number = uiDataGridView2.CurrentCell.RowIndex;
            int Data = e.ColumnIndex;
            if (Data == 0)
            {
                DialogResult t = MessageBox.Show("是否写入点位?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                ///写入X，Y
                if (t == DialogResult.OK)
                {
                    //写入当前实际位置
                    uiDataGridView2.Rows[Number].Cells[1].Value = Number + "X";
                    uiDataGridView2.Rows[Number].Cells[2].Value = Number + "Y";
                }
            }
        }

        private void Savebtn(object sender, EventArgs e)
        {
            try
            {
                /*************************写入点位**********************************/
                int v = uiDataGridView1.Rows.Count;//列数
                string[] n = new string[7 * v + 1];
                int b = 0;
                for (int i = 0; i < v; i++)
                {
                    for (int y = 0; y < 7; y++)
                    {
                        if (uiDataGridView1.Rows[i].Cells[y].Value == null)
                        {
                            MessageBox.Show("请输入数据或者删除空白行", "提示", MessageBoxButtons.OK);                          
                            return;
                        }
                        try
                        {
                            b++;
                            n[b] = uiDataGridView1.Rows[i].Cells[y].Value.ToString().Trim();

                        }
                        catch (Exception ex)
                        {

                            MessageBox.Show(ex.Message);

                        }

                    }

                }
                n[0] = v.ToString();

                if (!File.Exists(Application.StartupPath + "\\标准点位\\"  + 产品编号.Text + ".txt"))
                {

                    File.Create(Application.StartupPath + "\\标准点位\\"  + 产品编号.Text + ".txt").Close();
                }
                
                File.WriteAllLines(Application.StartupPath + "\\标准点位\\"  + 产品编号.Text + ".txt", n);    //当前的点位保存
                              
               
                /****************************导入视觉系***********************************************/
                MoveStand();
                ReaadStandard();

                MessageBox.Show("保存设置成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
          
        }
        /// <summary>
        /// 视觉开启或者关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="value"></param>
        private void uiCheckBox1_ValueChanged(object sender, bool value)
        {
            if (uiCheckBox1.Checked==true)
            {
                Vp = true;
            }
            else
            {
                Vp = false;
            }
        }
        /*******************多个参数保存*****************************/
        private void UpSave(object sender, EventArgs e)
        {
            try
            {
                int number = int.Parse(Number.Text.Trim());
                if (number <= (uiDataGridView1.Rows.Count))
                {
                    for (int i = 0; i < number; i++)
                    {
                        uiDataGridView1.Rows[i].Cells[3].Value = Convert.ToDouble(uiDataGridView1.Rows[i].Cells[3].Value) + Convert.ToDouble(Uplimit.Text);
                        

                        //UplimitX[i] = Convert.ToDouble(uiDataGridView1.Rows[i].Cells[3].Value) + Convert.ToDouble(uiDataGridView1.Rows[i].Cells[1].Value);   比较时候再用,不放在此处
                        //UplimitY[i] = Convert.ToDouble(uiDataGridView1.Rows[i].Cells[3].Value) + Convert.ToDouble(uiDataGridView1.Rows[i].Cells[2].Value);   比较时候再用
                    }

                    Savebtn(sender, e);
                }
                else
                {
                    MessageBox.Show("设置个数超限", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            
        }
        private void Down_save(object sender, EventArgs e)
        {
            try
            {
                int number = int.Parse(Number.Text.Trim());
                if (number <= (uiDataGridView1.Rows.Count))
                {
                    for (int i = 0; i < number; i++)
                    {
                        uiDataGridView1.Rows[i].Cells[4].Value = Convert.ToDouble(uiDataGridView1.Rows[i].Cells[4].Value) + Convert.ToDouble(Downlimit.Text);

                        //DownlimitX[i] = Convert.ToDouble(uiDataGridView1.Rows[i].Cells[4].Value) + Convert.ToDouble(uiDataGridView1.Rows[i].Cells[1].Value);   比较时候再用,不放在此处
                        //DownlimitY[i] = Convert.ToDouble(uiDataGridView1.Rows[i].Cells[4].Value) + Convert.ToDouble(uiDataGridView1.Rows[i].Cells[2].Value);   比较时候再用
                    }

                    Savebtn(sender, e);
                }
                else
                {
                    MessageBox.Show("设置个数超限", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }    
       
        private void X_Save(object sender, EventArgs e)
        {
            try
            {
                int number = int.Parse(Number.Text.Trim());
                if (number <= (uiDataGridView1.Rows.Count))
                {
                    for (int i = 0; i < number; i++)
                    {
                        uiDataGridView1.Rows[i].Cells[5].Value = Convert.ToDouble(uiDataGridView1.Rows[i].Cells[5].Value) + Convert.ToDouble(Xoffset.Text);

                        // uiDataGridView1.Rows[i].Cells[1].Value = Convert.ToDouble(uiDataGridView1.Rows[i].Cells[1].Value) + Convert.ToDouble(uiDataGridView1.Rows[i].Cells[5].Value); ,不放这里，放最后给上位机里面

                    }

                    Savebtn(sender, e);
                }
                else
                {
                    MessageBox.Show("设置个数超限", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void Y_Save(object sender, EventArgs e)
        {
            try
            {
                int number = int.Parse(Number.Text.Trim());
                if (number <= (uiDataGridView1.Rows.Count))
                {
                    for (int i = 0; i < number; i++)
                    {
                        uiDataGridView1.Rows[i].Cells[6].Value = Convert.ToDouble(uiDataGridView1.Rows[i].Cells[6].Value) + Convert.ToDouble(Yoffset.Text);

                        // uiDataGridView1.Rows[i].Cells[2].Value = Convert.ToDouble(uiDataGridView1.Rows[i].Cells[2].Value) + Convert.ToDouble(uiDataGridView1.Rows[i].Cells[6].Value); ,不放这里，放最后给上位机里面
                    }

                    Savebtn(sender, e);
                }
                else
                {
                    MessageBox.Show("设置个数超限", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        /***********************************保存屏蔽视觉行驶点***************************************/
        public void MoveStand()
        {
            try
            {
                string[] StringStand = new string[100];

                int Nm = 0;
                int Length = uiDataGridView1.Rows.Count;
                for (int i = 0; i < Length; i++)
                {
                    Standard[Nm] = Convert.ToDouble(uiDataGridView1.Rows[i].Cells[1].Value) + Convert.ToDouble(uiDataGridView1.Rows[i].Cells[5].Value);  //加上补偿 X
                    Standard[Nm + 1] = Convert.ToDouble(uiDataGridView1.Rows[i].Cells[2].Value) + Convert.ToDouble(uiDataGridView1.Rows[i].Cells[6].Value); //加上补偿  Y            
                    Nm = Nm + 2;

                }
                for (int i = 0; i < Length * 2; i++)
                {

                    StringStand[i] = Standard[i].ToString("f3");
                }
                if (!File.Exists(Application.StartupPath + "\\行进路径\\无视觉点位\\" + 产品编号.Text + ".txt"))
                {

                    File.Create(Application.StartupPath + "\\行进路径\\无视觉点位\\" + 产品编号.Text + ".txt").Close();
                }

                File.WriteAllLines(Application.StartupPath + "\\行进路径\\无视觉点位\\" + 产品编号.Text + ".txt", StringStand);   //当前的点位保存
            }
            catch (Exception ex)
            {
                
               MessageBox.Show(ex.Message+"保存行驶点出错!");
            }
          
        }


        public void ReaadStandard()
        {
            try
            {
                ReStan = File.ReadAllLines(Application.StartupPath + "\\行进路径\\无视觉点位\\" + 产品编号.Text + ".txt");
                foreach (var item in ReStan)
                {
                    if (item==string.Empty)
                    {
                        break;
                    }
                    Count++;
                }
                Count = Count / 2;
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message+"读取行驶标准点出错");
            }            
        }



        /****************************视觉系转化***********************************************/
      

        /***********************自动运行视觉流控**************************************/
        public static void Mark1()
        {
            try
            {
                Vision.AcqTool.Run();
                Vision.Mark1.Run();
                //图像显示
            }
            catch (Exception ex)
            {                
               MessageBox.Show(ex.Message+"Mark1拍照失败");
            }          
        }

        public static void Mark2()
        {
            try
            {
                Vision.AcqTool.Run();
                Vision.Mark2.Run();
                //图像显示
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + "Mark2拍照失败");
            }
        }
        /***********考虑用纯数学方式计算************************/
        public static void GetPoint()
        {
            try
            {
               
                        
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + "获取位置失败");
            }
        }
        //把Mark的2个点，和标准值进行绑定处理。
        private void ToVision(object sender, EventArgs e)
        {
          
            try
            {
              StandardX1=Convert.ToDouble(Vision.StandardBlock.Inputs[0].Value);
              StandardY1 = Convert.ToDouble(Vision.StandardBlock.Inputs[1].Value);
              StandardX2 = Convert.ToDouble(Vision.StandardBlock.Inputs[2].Value);
              StandardY2 = Convert.ToDouble(Vision.StandardBlock.Inputs[3].Value);
              double K = (StandardY2 - StandardY1) / (StandardX2 - StandardX1);   //Mark1，Mark2点连线的斜率
              double[] K1 = new double[100];
              double[] distance1 = new double[100];
              double[] Angle = new double[100];
              int It = 0;
              DistanceList.Clear();
              for (int i = 0; i < Count; i++)
              {

                  K1[It] = (Convert.ToDouble(ReStan[i + 1]) - StandardY1) / (Convert.ToDouble(ReStan[i]) - StandardY1);  //切点和Mark1的斜率

                  distance1[It] = Math.Sqrt((Math.Pow(Convert.ToDouble(ReStan[i]) - StandardX1, 2)) + (Math.Pow(Convert.ToDouble(ReStan[i + 1]) - StandardY1, 2)));   //切点和Mark的距离            

                  Angle[It] = Math.Atan(Math.Abs(K1[It] - K) / Math.Abs(1 + K * K1[It]));
                 
                  if (K>K1[It])        //比较斜率，确认点在上方还是下方
                  {
                      DistanceX[It] = Math.Sin(Angle[It]) * distance1[It];  
                  }
                  else
                  {
                      DistanceX[It] = -Math.Sin(Angle[It]) * distance1[It];  
                  }
                 
                 
                  DistanceY[It] = Math.Cos(Angle[It]) * distance1[It];                
                  DistanceList.Add(DistanceX[It].ToString());
                  DistanceList.Add(DistanceY[It].ToString());
                  i++;
                  It++;
              }
           
              if (!File.Exists(Application.StartupPath + "\\Distance\\" + 产品编号.Text + ".txt"))
              {

                  File.Create(Application.StartupPath + "\\Distance\\" + 产品编号.Text + ".txt").Close();
              }

              File.WriteAllLines(Application.StartupPath + "\\Distance\\" + 产品编号.Text + ".txt", DistanceList);   //当前的点位距离保存
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message+"导入视觉系出错!", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
        }

        private void btnSaveMark(object sender, EventArgs e)
        {
            
            try
            {
                for (int i = 0; i < 2; i++)
			    {
                    for (int y = 0; i < 2; y++)
                    {
                        CurrentPoint.Add(uiDataGridView1.Rows[i].Cells[y+1].Value.ToString());
                    }                  
		        }

                if (!File.Exists(Application.StartupPath + "\\行进路径\\有视觉点位\\" + 产品编号.Text + ".txt"))
                {

                    File.Create(Application.StartupPath + "\\行进路径\\有视觉点位\\" + 产品编号.Text + ".txt").Close();
                }

                File.WriteAllLines(Application.StartupPath + "\\行进路径\\有视觉点位\\" + 产品编号.Text + ".txt", CurrentPoint);   //当前的点位距离保存
               
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message+"保存Mark点出错");
            }
        }

        private void ReadMark()
        {
            try
            {
                
                string[]Mark=File.ReadAllLines(Application.StartupPath + "\\行进路径\\有视觉点位\\" + 产品编号.Text + ".txt");

                int Cot=0;
                 for (int i = 0; i < 2; i++)
			    {
                    for (int y = 0; i < 2; y++)
                    {

                        uiDataGridView1.Rows[i].Cells[y+1].Value= Mark[Cot];
                        Cot++;
                    }
                  
		        }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + "读取Mark点失败");
            }
        }
    }
 }
