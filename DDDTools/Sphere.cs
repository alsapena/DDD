using System;
using System.Collections.Generic;
using System.Drawing;
using DDD;

namespace DDDTools
{
    public class Sphere : Figure
    {
        public Sphere()
        {
            List<MyPoint> points = new List<MyPoint>();
            double w = 37;
            int scalar = 120;
            for (double x = 0; x <= 1; x += 1 / w)
                for (double y = 0; y <= 1; y += 1 / w)
                {
                    points.Add(
                        new MyPoint()
                        {
                            X = scalar * Math.Cos(2 * Math.PI * x) * Math.Sqrt(1 - Math.Pow((1 - 2 * y), 2)),
                            Y = (scalar) * (1 - 2 * y),
                            Z = scalar * Math.Sin(2 * Math.PI * x) * Math.Sqrt(1 - Math.Pow((1 - 2 * y), 2))
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
    }
}
