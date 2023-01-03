using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// 工具类   
/// </summary>
namespace _2021LY
{
    class CommUtil
    {
        /// <summary>
        /// 判断是否是Double类型
        /// </summary>
        /// <returns></returns>
        public static bool IsDouble(string str, out double value)
        {
            char[] x = new char[] { ' ' };
            string tem = str.Trim(x);
            if (!double.TryParse(str, out value))
            {
                value = 0;
                return false;
            }
            return true;
        }


        public static void Print(string msg)
        {

            Console.WriteLine(DateTime.Now.ToString() + msg);
        }

        /// <summary>
        /// 检测执行结果。返回 true 为正常
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public static bool CheckState(short tag)
        {
            if (tag == 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// 得到年月日
        /// </summary>  
        public static string Get_YMD()
        {

            int C = DateTime.Now.Year * 10000 + DateTime.Now.Month * 100 + DateTime.Now.Day;
            return C.ToString();
        }
        /// <summary>
        /// 得到年月日时
        /// </summary>
        public static string Get_YMDH()
        {
            int C = DateTime.Now.Year * 1000000 + DateTime.Now.Month * 10000 + DateTime.Now.Day * 100 + DateTime.Now.Hour;
            return C.ToString();
        }

        /// <summary>
        /// 得到年月日时分秒
        /// </summary>
        public static string Get_YMDHMS()
        {
            long C = DateTime.Now.Year * 10000000000 + DateTime.Now.Month * 100000000 + DateTime.Now.Day * 1000000 + DateTime.Now.Hour * 10000 + DateTime.Now.Minute * 100 + DateTime.Now.Second;
            return C.ToString();
        }

        /// <summary>
        /// 检查字符串是否纯数字  是纯数字返回true
        /// </summary>

        public static bool Is_Number(string str)
        {

            if (!string.IsNullOrEmpty(str))
            {
                ASCIIEncoding ase = new ASCIIEncoding();
                byte[] byteStr = ase.GetBytes(str);
                foreach (byte c in byteStr)
                {
                    if (c < 48 || c > 57)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// 脉冲转距离  mm 毫米    界面显示
        /// </summary>
        public static string Pulse_To_Cir(double Pulse)
        {
            return (Pulse * CommParam.CirPerPulse).ToString();
        }

        /// <summary>
        /// 距离转脉冲        
        /// </summary>
        public static int Cir_To_Pulse(string Cir)
        {
            int pulse = 0;
            if (Cir!="")
                pulse = Convert.ToInt32(Convert.ToDouble(Cir) * CommParam.PulsePerCir);
            return pulse;
        }


        public static double  test(double x)
        {
            //return Math.Ceiling(x);
            return Math.Floor(x);
        }

        //画轨迹使用
        public static int Da_To_In(string Cir)
        {
            double xx = 0.1;
            Convert.ToInt32(xx);
            return Convert.ToInt32(Convert.ToDouble(Cir) * CommParam.PulsePerCir);
        }

        /// <summary>
        /// 四舍五入    data  需转换的数字  ，  num  保留的位数
        /// </summary>
        public static double TransData(string data, int num)
        {
            double da = Convert.ToDouble(data);
            return Math.Round(da, num);
        }


        /// <summary>
        /// 三点法计算圆心坐标和圆半径   X 圆心x坐标  Y 圆心y坐标  R 圆半径
        /// </summary>CalculateCicular(new PointF(120, 140), new PointF(200, 110), new PointF(201, 111), out x, out y, out rr);
        /// <param name="px1">第一个点</param>
        /// <param name="px2">第二个点</param>
        /// <param name="px3">第三个点</param>
        /// <param name="X">圆心x坐标</param>
        /// <param name="Y">圆心y坐标</param>
        /// <param name="R">圆半径</param>
        public static void CalculateCicular(PointF px1, PointF px2, PointF px3, out float X, out float Y, out float R)
        {
            float x1, y1, x2, y2, x3, y3;
            float a, b, c, g, e, f;
            x1 = px1.X;
            y1 = px1.Y;
            x2 = px2.X;
            y2 = px2.Y;
            x3 = px3.X;
            y3 = px3.Y;
            e = 2 * (x2 - x1);
            f = 2 * (y2 - y1);
            g = x2 * x2 - x1 * x1 + y2 * y2 - y1 * y1;
            a = 2 * (x3 - x2);
            b = 2 * (y3 - y2);
            c = x3 * x3 - x2 * x2 + y3 * y3 - y2 * y2;
            X = (g * b - c * f) / (e * b - a * f);
            Y = (a * g - c * e) / (a * f - b * e);
            R = (float)Math.Sqrt((X - x1) * (X - x1) + (Y - y1) * (Y - y1));
        }


        /// <summary>
        /// 十进制转16进制
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ushort int_To_Hex(int data)
        {
           return ushort.Parse(data.ToString("X"));
        }


        // 计算2点距离
        public static double Distance_PP(int x,int y)
        {
            int start_x = Convert.ToInt32( CommParam.X_Pos);
            int start_y = Convert.ToInt32(CommParam.Y_Pos);

            int xx = System.Math.Abs(start_x - x);
            int yy = System.Math.Abs(start_y - y);
            return Math.Sqrt(xx * xx + yy * yy);

        }

        public static string FormatTimeMsToStr(float time)
        {
            int second;
            int minute = 0;
            second = (int)time / 1000;
            if (second >= 60)
            {
                minute = second / 60;
                second = second % 60;
            }
            return string.Format("{0:D2}:{1:D2}", minute, second);
        }
    }
}
