using System;
using System.Drawing;

namespace DDDTools
{
    public class MyPoint
    {
        double[] punto = new double[4];

        public MyPoint()
        {
            punto[3] = 1;
        }
        public MyPoint(double x, double y, double z)
        {
            punto[0] = x;
            punto[1] = y;
            punto[2] = z;
            punto[3] = 1;
        }
        public MyPoint(double x, double y)
        {
            punto[0] = x;
            punto[1] = y;
            punto[3] = 1;
        }

        public double X
        {
            get { return punto[0]; }
            set { punto[0] = value; }
        }
        public double Y
        {
            get { return punto[1]; }
            set { punto[1] = value; }
        }
        public double Z
        {
            get { return punto[2]; }
            set { punto[2] = value; }
        }

        public MyPoint Clone()
        {
            return new MyPoint(X, Y, Z);
        }

        public static MyPoint operator -(MyPoint a, MyPoint b)
        {
            return new MyPoint(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        public static MyPoint operator +(MyPoint a, MyPoint b)
        {
            return new MyPoint(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public double CoordenadaHomogenea
        {
            get { return punto[3]; }
        }
        public double this[int pos]
        {
            get
            {
                if (pos >= 4) throw new Exception("La coordenada a la que se refiere no existe");
                return punto[pos];
            }
            set
            {
                if (pos >= 4) throw new Exception("La coordenada a la que se refiere no existe");
                punto[pos] = value;
            }
        }
        public PointF ToPointF()
        {
            return new PointF((float)punto[0], (float)punto[1]);
        }
        public Point ToPoint()
        {
            return new Point((int)punto[0], (int)punto[1]);
        }
        public override string ToString()
        {
            return punto[0] + ", " + punto[1] + ", " + punto[2];
        }
        public int Dimension
        {
            get { return punto.Length - 1; }
        }
        public int DimensionHomogenea
        {
            get { return punto.Length; }
        }
    }
}
