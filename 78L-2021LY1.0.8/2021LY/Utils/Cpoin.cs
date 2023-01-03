using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2021LY
{
    class Cpoin
    {
        public struct Point_Change
        {
            public int Row_Num;
            public double X_Pos;
            public double Y_Pos;
            public string R;
        };


        public static List<Point_Change> GetPoint(PointF lineFirstStar, PointF lineFirstEnd, PointF lineSecondStar, PointF lineSecondEnd, List<CommParam.Point_Data> da)
        {

            float a = 0, b = 0;
            PointF np = new PointF(0, 0);
            int state = 0;
            if (lineFirstStar.X != lineFirstEnd.X)
            {
                a = (lineFirstEnd.Y - lineFirstStar.Y) / (lineFirstEnd.X - lineFirstStar.X);
                state |= 1;
            }
            if (lineSecondStar.X != lineSecondEnd.X)
            {
                b = (lineSecondEnd.Y - lineSecondStar.Y) / (lineSecondEnd.X - lineSecondStar.X);
                state |= 2;
            }
            switch (state)
            {
                case 0: //L1与L2都平行Y轴
                    {
                        if (lineFirstStar.X == lineSecondStar.X)
                        {
                            //throw new Exception("两条直线互相重合，且平行于Y轴，无法计算交点。");
                            np = new PointF(0, 0); break;
                        }
                        else
                        {
                            //throw new Exception("两条直线互相平行，且平行于Y轴，无法计算交点。");
                            np = new PointF(0, 0); break;
                        }
                    }
                case 1: //L1存在斜率, L2平行Y轴
                    {
                        float xa = lineSecondStar.X;
                        float ya = (lineFirstStar.X - xa) * (-a) + lineFirstStar.Y;
                        np = new PointF(xa, ya);
                        break;
                    }
                case 2: //L1 平行Y轴，L2存在斜率
                    {
                        float xb = lineFirstStar.X;
                        //网上有相似代码的，这一处是错误的。你可以对比case 1 的逻辑 进行分析
                        //源code:lineSecondStar * x + lineSecondStar * lineSecondStar.X + p3.Y;
                        float yb = (lineSecondStar.X - xb) * (-b) + lineSecondStar.Y;
                        np = new PointF(xb, yb);
                        break;
                    }
                case 3: //L1，L2都存在斜率
                    {
                        if (a == b)
                        {
                            // throw new Exception("两条直线平行或重合，无法计算交点。");
                            np = new PointF(0, 0);
                        }
                        float xc = (a * lineFirstStar.X - b * lineSecondStar.X - lineFirstStar.Y + lineSecondStar.Y) / (a - b);
                        float yc = a * xc - a * lineFirstStar.X + lineFirstStar.Y;
                        np = new PointF(xc, yc);
                        break;
                    }
            }
            //

            Console.WriteLine(" 旋转点 坐标X={0}  Y={1}", np.X, np.Y);

           // Console.WriteLine("根据旋转点 坐标求角度");
            //double an = Angle(np, Firstend, Secondend);
            double an = 0;
            double cosfi = 0, fi = 0, norm = 0;
            double dsx = lineFirstEnd.X - np.X;
            double dsy = lineFirstEnd.Y - np.Y;
            double dex = lineSecondEnd.X - np.X;
            double dey = lineSecondEnd.Y - np.Y;

            cosfi = dsx * dex + dsy * dey;
            norm = (dsx * dsx + dsy * dsy) * (dex * dex + dey * dey);
            cosfi /= Math.Sqrt(norm);

            if (cosfi >= 1.0) an = 0;
            if (cosfi <= -1.0) an = Math.PI;
            fi = Math.Acos(cosfi);

            if (180 * fi / Math.PI < 180)
            {
                an = 180 * fi / Math.PI;
            }
            else
            {
                an = 360 - 180 * fi / Math.PI;
            }

            Console.WriteLine("角度{0}", an);

            List<Point_Change> point_Change_List = new List<Point_Change>();
            foreach (var item in da)
            {
                string x = item.Point_X_Pos;
                string y = item.Point_Y_Pos;

                double xx =Convert.ToDouble(x);// -1300;
                double yy = Convert.ToDouble(y);// +4250;


                Point_Change p = new Point_Change();
                double anx = (Math.PI / 180.0) * (an);
                Point npx = new Point(Convert.ToInt32(np.X), Convert.ToInt32(np.Y));
                Point ppp =  RotatePoint(new Point(xx,yy), npx, anx,false);
                p.X_Pos = ppp.x;
                p.Y_Pos = ppp.y;
                p.Row_Num = item.RowNum;
                p.R = item.Point_RY2_Pos;
                Console.WriteLine();
                Console.WriteLine("旋转操作完成 脉冲位置为 x ={0} y ={1} ", p.X_Pos, p.Y_Pos);
                point_Change_List.Add(p);
            }

            return point_Change_List;


        }

       









        struct Point
        {
            //横、纵坐标
            public double x, y;
            //构造函数
            public Point(double x, double y)
            {
                this.x = x;
                this.y = y;
            }
            //该点到指定点pTarget的距离
            public double DistanceTo(Point p)
            {
                return Math.Sqrt((p.x - x) * (p.x - x) + (p.y - y) * (p.y - y));
            }
            //重写ToString方法
            public override string ToString()
            {
                return string.Concat("Point (",
                    this.x.ToString("#0.000"), ',',
                    this.y.ToString("#0.000"), ')');
            }
        }

        private static Point RotatePoint(Point P, Point A,
            double rad, bool isClockwise = true)
        {
            //点Temp1
            Point Temp1 = new Point(P.x - A.x, P.y - A.y);
            //点Temp1到原点的长度
            double lenO2Temp1 = Temp1.DistanceTo(new Point(0, 0));
            //∠T1OX弧度
            double angT1OX = radPOX(Temp1.x, Temp1.y);
            //∠T2OX弧度（T2为T1以O为圆心旋转弧度rad）
            double angT2OX = angT1OX - (isClockwise ? 1 : -1) * rad;
            //点Temp2
            Point Temp2 = new Point(
                lenO2Temp1 * Math.Cos(angT2OX),
                lenO2Temp1 * Math.Sin(angT2OX));
            //点Q
            return new Point(Temp2.x + A.x, Temp2.y + A.y);
        }

        private static double radPOX(double x, double y)
        {
            //P在(0,0)的情况
            if (x == 0 && y == 0) return 0;

            //P在四个坐标轴上的情况：x正、x负、y正、y负
            if (y == 0 && x > 0) return 0;
            if (y == 0 && x < 0) return Math.PI;
            if (x == 0 && y > 0) return Math.PI / 2;
            if (x == 0 && y < 0) return Math.PI / 2 * 3;

            //点在第一、二、三、四象限时的情况
            if (x > 0 && y > 0) return Math.Atan(y / x);
            if (x < 0 && y > 0) return Math.PI - Math.Atan(y / -x);
            if (x < 0 && y < 0) return Math.PI + Math.Atan(-y / -x);
            if (x > 0 && y < 0) return Math.PI * 2 - Math.Atan(-y / x);

            return 0;
        }


    }
}
