using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDTools
{
    public class Torus : Figure
    {
        public Torus()
        {
            List<MyPoint> points = new List<MyPoint>();
            double w = 38;
            int scalar = 3;
            int R = 20;
            int r = 10;
            for (double x = 0; x <= 1; x += 1 / w)
              for (double y = 0; y <= 1; y += 1 / w)
                {
                    double p = R + r * Math.Cos(2 * Math.PI * (1 - 2 * y));

                    points.Add(
                        new MyPoint()
                        {
                            X = scalar * Math.Cos(2 * Math.PI * x) * p,
                            Y = (scalar) * r *  Math.Sin(2 * Math.PI * (1 - 2 * y)),
                            Z = scalar * Math.Sin(2 * Math.PI * x) * p,
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
