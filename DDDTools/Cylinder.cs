using System;
using System.Collections.Generic;
using System.Drawing;
using DDD;

namespace DDDTools
{
    public class Cylinder : Figure
    {
        public Cylinder()
        {

            List<MyPoint> points = new List<MyPoint>();
            double w = 37;
            int scalar = 100;
            for (double x = 0; x <= 1; x += 1 / w)
                for (double y = 0; y <= 1; y += 1 / w)
                {
                    points.Add(
                        new MyPoint()
                        {
                            X = scalar * Math.Cos(2 * Math.PI * x),//* Math.Sqrt(1 - Math.Pow((1 - 2 * y), 2)),
                            Y = (scalar) * (1 - 2*y),
                            Z = scalar * Math.Sin(2 * Math.PI * x)// * Math.Sqrt(1 - Math.Pow((1 - 2 * y), 2))


                            //X = scalar * Math.Cos(2 * Math.PI * x)*1.41,//* Math.Sqrt(1 - Math.Pow((1 - 2 * y), 2)),
                            //Y = (scalar) * (1 - 2*y),
                            //Z = scalar * Math.Sin(2 * Math.PI * x)*1.41// * Math.Sqrt(1 - Math.Pow((1 - 2 * y), 2))
                        });
                }

            w = w + 1;

            Faces = new Face[((int)(w - 1) * (int)(w - 1))];
            int c = 0;
            for (int i = 0; i < w - 1; i++)
            {
                for (int j = 0; j < w - 1; j++)
                {
                    Faces[c] =
                        new Triangle(new[]
                        {
                            points[(int) (i*w + j)],
                            points[(int) ((i + 1)*w + j)],
                            points[(int) ((i + 1)*w + j + 1)]
                        });
                    c++;
                }
            }
        }

        private double GetX(double d)
        {
            if (d >= 0 && d < 90)
                return 1;

            if (d >= 90 && d <= 180)
                return 1 - (Math.Sqrt(1) * Math.Sin((d - 90) * Math.PI / 180)) / Math.Sin((180 - 45 - (d - 90)) * Math.PI / 180);

            if (d > 180 && d <= 270)
                return -1;

            return (Math.Sqrt(1) * Math.Sin((d - 270) * Math.PI / 180)) / Math.Sin((180 - 45 - (d - 270)) * Math.PI / 180) - 1;
        }

        private double GetZ(double d)
        {
            if (d >= 0 && d < 90)
                return (Math.Sqrt(1) * Math.Sin(d * Math.PI / 180)) / Math.Sin((180 - 45 - d) * Math.PI / 180) - 1;

            if (d >= 90 && d <= 180)
                return 1;

            if (d > 180 && d <= 270)
                return 1 - (Math.Sqrt(1) * Math.Sin((d - 180) * Math.PI / 180)) / Math.Sin((180 - 45 - (d - 180)) * Math.PI / 180);

            return -1;
        }
    }
}
