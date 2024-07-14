using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using DDDTools;
using Rectangle = DDDTools.Rectangle;

namespace CubeRubik
{
    class RectangleH:Rectangle
    {
        public RectangleH(MyPoint[] points) : base(points)
        {
        }

        public RectangleH(MyPoint[] points, int i, int j, int k,int f,int of):base(points)
        {
            Tuple = new MyPoint(i, j, k);
            pos = new Tuple<int, int, int>(0,0,0);
            F = f;
            Of = of;
        }

        public int Of { get; set; }

        public MyPoint Tuple { get; set; }

        public int F { get; set; }
        public Tuple<int,int,int> pos{ get; set; }

        public override void Draw(Graphics g, PointF p, bool faces, bool edges, Color edgesColor, int pen,float xAngle,float yAngle,float zAngle)
        {
            base.Draw(g, p, faces, edges, edgesColor, pen, xAngle, yAngle, zAngle);

            MyPoint p1 = Tools.RotarR(xAngle, yAngle, zAngle, Points[0]);
            MyPoint p2 = Tools.RotarR(xAngle, yAngle, zAngle, Points[2]);

            double x = (p1.X + p2.X) / 2 + p.X;

            double y = (p1.Y + p2.Y) / 2 + p.Y;

            StringFormat sf = new StringFormat();

            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;

            g.DrawString(string.Format("{0},{1},{2}", Tuple.X, Tuple.Y, Tuple.Z), new Font("Arial", 6), Brushes.Black, (float)x,
                (float)y, sf);
            g.DrawString(string.Format("{0},{1},{2}", pos.Item1, pos.Item2, pos.Item3), new Font("Arial", 6), Brushes.Black, (float)x,
                (float)y + 10, sf);

            g.DrawString(string.Format("{0}", F), new Font("Arial", 6), Brushes.Black, (float)x,
               (float)y + 20, sf);

        }

        public override void Draw(Graphics g, PointF p, bool faces, bool edges, Color edgesColor, int pen, double[,] world)
        {
            base.Draw(g, p, faces, edges, edgesColor, pen, world);

            MyPoint p1 = Tools.MP(world, Points[0]);
            MyPoint p2 = Tools.MP(world, Points[2]);

            double x = (p1.X + p2.X) / 2 + p.X;

            double y = (p1.Y + p2.Y) / 2 + p.Y;

            //StringFormat sf = new StringFormat();

            //sf.Alignment = StringAlignment.Center;
            //sf.LineAlignment = StringAlignment.Center;

            //g.DrawString(string.Format("{0},{1},{2}", Tuple.X, Tuple.Y, Tuple.Z), new Font("Arial", 6), Brushes.Black, (float)x,
            //    (float)y, sf);
            //g.DrawString(string.Format("{0},{1},{2}", pos.Item1, pos.Item2, pos.Item3), new Font("Arial", 6), Brushes.Black, (float)x,
            //    (float)y + 10, sf);

            //g.DrawString(string.Format("{0}", F), new Font("Arial", 6), Brushes.Black, (float)x,
            //   (float)y + 20, sf);

            //g.DrawString(string.Format("{0},{1}", Math.Round(x, 1), Math.Round(y, 1)), new Font("Arial", 6), Brushes.Black, (float)x,
            //  (float)y + 30, sf);

        }
    }
}
