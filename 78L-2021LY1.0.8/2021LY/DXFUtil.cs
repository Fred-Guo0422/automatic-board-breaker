using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using netDxf;
using netDxf.Blocks;
using netDxf.Collections;
using netDxf.Entities;
using netDxf.Header;
using netDxf.Objects;
using netDxf.Tables;
using netDxf.Units;
namespace _2021LY
{
    /// <summary>
    /// DXF 操作类   
    /// </summary>
    class DXFUtil
    {

        /// <summary>
        /// 直线集合
        /// </summary>
        public struct DataLineStruct
        {
            public double StartPoint_X_Pos;
            public double StartPoint_Y_Pos;
            public double StartPoint_Z_Pos;
            public double EndPoint_X_Pos;
            public double EndPoint_Y_Pos;
            public double EndPoint_Z_Pos;
        };
        /// <summary>
        /// 多线段集合
        /// </summary>
        public struct DataPolyLineStruct
        {
            public int iNum;
            public float X1Pos;
            public float Y1Pos;
            public float X2Pos;
            public float Y2Pos;
        };
        /// <summary>
        /// 弧集合
        /// </summary>
        public struct DataArcStruct
        {
            public float XPos;
            public float YPos;
            public float X1Pos;
            public float Y1Pos;
            public float RPos;
            public float ptXPos;
            public float ptYPos;
            public float Angle1Pos;
            public float Angle2Pos;
            public float IPos;
            public float JPos;
        };
        /// <summary>
        /// 圆集合
        /// </summary>
        public struct DataCircleStruct
        {
            public double XPos;
            public double YPos;
            public double ZPos;
            public double RPos;
        };

        DxfDocument dxf;
        List<DataPolyLineStruct> stuPolyLineArray = new List<DataPolyLineStruct>();
        List<DataLineStruct> stuLineArray = new List<DataLineStruct>();
        List<DataArcStruct> stuArcArray = new List<DataArcStruct>();
        List<DataCircleStruct> stuCircleArray = new List<DataCircleStruct>();



        public DXFUtil(string path)
        {
            dxf = DxfDocument.Load(path);
        }
        /// <summary>
        /// 返回DXF所有圆集合
        /// </summary>
        public List<DataCircleStruct> Get_Circles()
        {
            DataCircleStruct stu1;
            IEnumerable<netDxf.Entities.Circle> circles =
    (IEnumerable<netDxf.Entities.Circle>)dxf.Circles.GetEnumerator();
             
            foreach (netDxf.Entities.Circle item in circles)
            {
                stu1.XPos = item.Center.X;
                stu1.YPos = item.Center.Y;
                stu1.ZPos = item.Center.Z;
                stu1.RPos = item.Radius;

                stuCircleArray.Add(stu1);
            }
            return stuCircleArray;
        }


        /// <summary>
        /// 返回DXF所有直线集合
        /// </summary>
        public List<DataLineStruct> Get_Lines()
        {
            DataLineStruct dls;
            IEnumerable<netDxf.Entities.Line> lines =
    (IEnumerable<netDxf.Entities.Line>)dxf.Lines.GetEnumerator();

            foreach (netDxf.Entities.Line item in lines)
            {
                dls.StartPoint_X_Pos = item.StartPoint.X;
                dls.StartPoint_Y_Pos = item.StartPoint.Y;
                dls.StartPoint_Z_Pos = item.StartPoint.Z;
                dls.EndPoint_X_Pos = item.EndPoint.X;
                dls.EndPoint_Y_Pos = item.EndPoint.Y;
                dls.EndPoint_Z_Pos = item.EndPoint.Z;

                stuLineArray.Add(dls);
            }
            return stuLineArray;
        }


        public List<DataArcStruct> Get_Arcs()
        {
            DataArcStruct dls;
            IEnumerable<netDxf.Entities.Arc> Arcs =
    (IEnumerable<netDxf.Entities.Arc>)dxf.Arcs.GetEnumerator();

            foreach (netDxf.Entities.Arc item in Arcs)
            {
                double endAngle =  item.EndAngle;
                double startAngle = item.StartAngle;
                double radius = item.Radius;
                
                //dls.StartPoint_X_Pos = item.StartPoint.X;
                //dls.StartPoint_Y_Pos = item.StartPoint.Y;
                //dls.StartPoint_Z_Pos = item.StartPoint.Z;
                //dls.EndPoint_X_Pos = item.EndPoint.X;
                //dls.EndPoint_Y_Pos = item.EndPoint.Y;
                //dls.EndPoint_Z_Pos = item.EndPoint.Z;

                //stuLineArray.Add(dls);
            }
            return stuArcArray;
        }



        public void test()
        {

            Insert s = dxf.Inserts.ElementAt(0);
            
            Console.WriteLine("1");
    //        DataLineStruct dls;
    //        IEnumerable<netDxf.Entities.Line> lines =
    //(IEnumerable<netDxf.Entities.Line>)dxf.Lines.GetEnumerator();

            //        foreach (netDxf.Entities.Line item in lines)
            //        {
            //            dls.StartPoint_X_Pos = item.StartPoint.X;
            //            dls.StartPoint_Y_Pos = item.StartPoint.Y;
            //            dls.StartPoint_Z_Pos = item.StartPoint.Z;
            //            dls.EndPoint_X_Pos = item.EndPoint.X;
            //            dls.EndPoint_Y_Pos = item.EndPoint.Y;
            //            dls.EndPoint_Z_Pos = item.EndPoint.Z;

            //            stuLineArray.Add(dls);
            //        }
        }

        //private void PaintBack()
        //{
        //    Rectangle newRect = pictureBox1.DisplayRectangle;
        //    Bitmap bmp = new Bitmap(newRect.Width, newRect.Height);
        //    Graphics g = Graphics.FromImage(bmp);
        //    Graphics g1 = pictureBox1.CreateGraphics();
        //    SolidBrush mysbrush1 = new SolidBrush(Color.Black);
        //    g.FillRectangle(mysbrush1, newRect);
        //    Pen p = new Pen(Color.Blue, 2);
        //    Pen p1 = new Pen(Color.FromArgb(50, 50, 50), 1);
        //    PointF ptStart, ptEnd, ptRect, ptAngle, ptPt;
        //    float r, angle;
        //    RectangleF rect1 = new RectangleF();
        //    ptStart = new PointF();
        //    ptEnd = new PointF();
        //    ptRect = new PointF();
        //    ptAngle = new PointF();
        //    ptPt = new PointF();


        //    int n = 1;
        //    while (n * 40 < newRect.Height)
        //    {
        //        g.DrawLine(p1, 0, 40 * n, newRect.Width, 40 * n);
        //        n++;
        //    }
        //    n = 1;
        //    while (n * 40 < (newRect.Width))
        //    {
        //        g.DrawLine(p1, 40 * n, 0, 40 * n, newRect.Height);
        //        n++;
        //    }


        //    n = 0;

        //    for (int i = 0; i < stuLineArray.Count; i++)
        //    {
        //        ptStart.X = stuLineArray[i].fX1Pos;
        //        ptStart.Y = stuLineArray[i].fY1Pos;
        //        ptEnd.X = stuLineArray[i].fX2Pos;
        //        ptEnd.Y = stuLineArray[i].fY2Pos;
        //        ptStart.X = ptStart.X * m_fradio;
        //        ptStart.Y = ptStart.Y * m_fradio;
        //        ptEnd.X = ptEnd.X * m_fradio;
        //        ptEnd.Y = ptEnd.Y * m_fradio;

        //        ptStart.X = ptStart.X * m11 + ptStart.Y * m12 + m13;
        //        ptStart.Y = ptStart.X * m21 + ptStart.Y * m22 + m23;

        //        ptEnd.X = ptEnd.X * m11 + ptEnd.Y * m12 + m13;
        //        ptEnd.Y = ptEnd.X * m21 + ptEnd.Y * m22 + m23;

        //        g.DrawLine(p, ptStart, ptEnd);
        //    }

        //    for (int i = 0; i < stuPolyLineArray.Count; i++)
        //    {
        //        ptStart.X = stuPolyLineArray[i].fX1Pos;
        //        ptStart.Y = stuPolyLineArray[i].fY1Pos;
        //        ptEnd.X = stuPolyLineArray[i].fX2Pos;
        //        ptEnd.Y = stuPolyLineArray[i].fY2Pos;

        //        ptStart.X = ptStart.X * m_fradio;
        //        ptStart.Y = ptStart.Y * m_fradio;
        //        ptEnd.X = ptEnd.X * m_fradio;
        //        ptEnd.Y = ptEnd.Y * m_fradio;

        //        ptStart.X = ptStart.X * m11 + ptStart.Y * m12 + m13;
        //        ptStart.Y = ptStart.X * m21 + ptStart.Y * m22 + m23;

        //        ptEnd.X = ptEnd.X * m11 + ptEnd.Y * m12 + m13;
        //        ptEnd.Y = ptEnd.X * m21 + ptEnd.Y * m22 + m23;

        //        g.DrawLine(p, ptStart, ptEnd);
        //    }



        //    for (int i = 0; i < stuCircleArray.Count; i++)
        //    {
        //        r = stuCircleArray[i].fRPos * m_fradio;
        //        ptStart.X = stuCircleArray[i].fXPos;
        //        ptStart.Y = stuCircleArray[i].fYPos;
        //        ptStart.X = ptStart.X * m_fradio;
        //        ptStart.Y = ptStart.Y * m_fradio;
        //        ptStart.X = ptStart.X * m11 + ptStart.Y * m12 + m13;
        //        ptStart.Y = ptStart.X * m21 + ptStart.Y * m22 + m23;
        //        rect1.X = ptStart.X - r;
        //        rect1.Y = ptStart.Y - r;
        //        rect1.Width = 2 * r;
        //        rect1.Height = 2 * r;
        //        //                 rect1.X = stuCircleArray[i].fXPos - stuCircleArray[i].fRPos;
        //        //                 rect1.Y = stuCircleArray[i].fYPos - stuCircleArray[i].fRPos;
        //        //                 rect1.Height = 2 *  stuCircleArray[i].fRPos;
        //        //                 rect1.Width = 2 * stuCircleArray[i].fRPos;
        //        g.DrawEllipse(p, rect1);
        //    }

        //    for (int i = 0; i < stuArcArray.Count; i++)
        //    {
        //        RectangleF rect = new RectangleF();
        //        ptStart.X = stuArcArray[i].fXPos;
        //        ptStart.Y = stuArcArray[i].fYPos;
        //        ptEnd.X = stuArcArray[i].fX1Pos;
        //        ptEnd.Y = stuArcArray[i].fY1Pos;
        //        ptRect.X = stuArcArray[i].fIPos;
        //        ptRect.Y = stuArcArray[i].fJPos;
        //        ptPt.X = stuArcArray[i].fptXPos;
        //        ptPt.Y = stuArcArray[i].fptYPos;
        //        ptStart.X = ptStart.X * m_fradio;
        //        ptStart.Y = ptStart.Y * m_fradio;
        //        ptEnd.X = ptEnd.X * m_fradio;
        //        ptEnd.Y = ptEnd.Y * m_fradio;
        //        ptRect.X = ptRect.X * m_fradio;
        //        ptRect.Y = ptRect.Y * m_fradio;

        //        ptStart.X = ptStart.X * m11 + ptStart.Y * m12 + m13;
        //        ptStart.Y = ptStart.X * m21 + ptStart.Y * m22 + m23;

        //        ptEnd.X = ptEnd.X * m11 + ptEnd.Y * m12 + m13;
        //        ptEnd.Y = ptEnd.X * m21 + ptEnd.Y * m22 + m23;
        //        //                 r = stuArcArray[i].fRPos;
        //        //                 ptAngle.X = stuArcArray[i].fAngle1Pos;
        //        //                 ptAngle.Y = stuArcArray[i].fAngle2Pos;
        //        //                 rect.X = ptPt.X - r;
        //        //                 rect.Y = ptPt.Y - r;
        //        //                 rect.Width = 2 * r;
        //        //                 rect.Height = 2 * r;
        //        //                 ptAngle.X = (float)(Math.Atan2(Convert.ToDouble(ptStart.Y - ptPt.Y), Convert.ToDouble(ptStart.X - ptPt.X)) / Math.PI * 180);
        //        //                 ptAngle.Y = (float)(Math.Atan2(Convert.ToDouble(ptEnd.Y - ptPt.Y), Convert.ToDouble(ptEnd.X) - ptPt.X) / Math.PI * 180);
        //        r = (float)Math.Pow((ptRect.X * ptRect.X + ptRect.Y * ptRect.Y), 0.5);
        //        rect.X = ptStart.X + ptRect.X - r;
        //        rect.Y = ptStart.Y + ptRect.Y - r;
        //        rect.Height = 2 * r;
        //        rect.Width = 2 * r;

        //        ptAngle.X = (float)(Math.Atan2(Convert.ToDouble(-ptRect.Y), Convert.ToDouble(-ptRect.X)) / 3.14 * 180);
        //        ptAngle.Y = (float)(Math.Atan2(Convert.ToDouble(ptEnd.Y - ptRect.Y - ptStart.Y), Convert.ToDouble(ptEnd.X - ptRect.X - ptStart.X)) / 3.14 * 180);

        //        if (ptAngle.X < 0)
        //        {
        //            ptAngle.X = 360 + ptAngle.X;
        //        }
        //        if (ptAngle.Y < 0)
        //        {
        //            ptAngle.Y = 360 + ptAngle.Y;
        //        }
        //        angle = ptAngle.Y - ptAngle.X;
        //        if (angle < 0)
        //        {
        //            angle += 360;
        //        }

        //        g.DrawArc(p, rect, ptAngle.X, angle);
        //    }


        //    Matrix M = new Matrix(1, 0, 0, -1, 0, 0);
        //    g1.Transform = M;
        //    g1.TranslateTransform(0, -newRect.Height);
        //    g1.DrawImage(bmp, 0, 0);

        //    M.Dispose();
        //    g.Dispose();
        //    g1.Dispose();
        //    mysbrush1.Dispose();
        //    p.Dispose();
        //    p1.Dispose();

        //}

    }
}
