using System.Drawing;
using DDD;

namespace DDDTools
{
    public abstract class Face
    {
        public MyPoint[] Points { get; set; }

        public Color Color { get; set; }

        public Face(MyPoint[] points)
        {
            Points = new MyPoint[points.Length];
            for (int i = 0; i < points.Length; i++)
            {
                Points[i] = points[i].Clone();
            }
        }

        public abstract int CountPoint { get; }

        public virtual void Draw(Graphics g, PointF p, bool faces, bool edges, Color edgesColor, int pen)
        {
            SolidBrush sb = new SolidBrush(Color);
            PointF[] c1 = new PointF[CountPoint];

            for (int j = 0; j < CountPoint; j++)
                c1[j] = Points[j].ToPointF();

            for (int i = 0; i < CountPoint; i++)
            {
                c1[i].X += p.X;
                c1[i].Y += p.Y;
            }

            if (faces)
                g.FillPolygon(sb, c1);
            if (edges)
                g.DrawPolygon(new Pen(edgesColor, pen), c1);
        }

        public virtual void Draw(Graphics g, PointF p, bool faces, bool edges, Color edgesColor, int pen,float xAngle,float yAngle,float zAngle)
        {
            SolidBrush sb = new SolidBrush(Color);
            PointF[] c1 = new PointF[CountPoint];

            for (int j = 0; j < CountPoint; j++)
                c1[j] = Tools.RotarR(xAngle, yAngle, zAngle, Points[j]).ToPointF();

            for (int i = 0; i < CountPoint; i++)
            {
                c1[i].X += p.X;
                c1[i].Y += p.Y;
            }

            if (faces)
                g.FillPolygon(sb, c1);
            if (edges)
                g.DrawPolygon(new Pen(edgesColor, pen), c1);
        }

        public static double AverageZ(Face f)
        {
            double sum = 0;

            for (int i = 0; i < f.CountPoint; i++)
            {
                sum += f.Points[i].Z;
            }

            return sum / f.CountPoint;
        }

        public static double AverageZ(Face f,float xAngle,float yAngle,float zAngle)
        {
            double sum = 0;

            for (int i = 0; i < f.CountPoint; i++)
            {
                sum += Tools.RotarR(xAngle, yAngle, zAngle, f.Points[i]).Z;
            }

            return sum / f.CountPoint;
        }

        public static double AverageZ(Face f, double[,] word)
        {
            double sum = 0;

            for (int i = 0; i < f.CountPoint; i++)
            {
                sum += Tools.MP(word, f.Points[i]).Z;
            }

            return sum / f.CountPoint;
        }

        public virtual void Draw(Graphics g, PointF p, bool faces, bool edges, Color edgesColor, int pen, double[,] word)
        {
            SolidBrush sb = new SolidBrush(Color);
            PointF[] c1 = new PointF[CountPoint];

            for (int j = 0; j < CountPoint; j++)
                c1[j] = Tools.MP(word, Points[j]).ToPointF();

            for (int i = 0; i < CountPoint; i++)
            {
                c1[i].X += p.X;
                c1[i].Y += p.Y;
            }

            if (faces)
                g.FillPolygon(sb, c1);
            if (edges)
                g.DrawPolygon(new Pen(edgesColor, pen), c1);
        }
    }
}
