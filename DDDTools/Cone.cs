using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDTools
{
    public class Cone:Figure
    {
        public Cone()
        {
            List<MyPoint> points = new List<MyPoint>();
            double w = 37;
            int scalar = 50;
            for (double x = 0; x <= 1; x += 1 / w)
                for (double y = 0; y <= 1; y += 1 / w)
                {
                    points.Add(
                        new MyPoint()
                        {
                            //cosa bonita
                            //X = scalar * Math.Cos(2 * Math.PI * x) * (1 - Math.Sin(2 * Math.PI * (1 - 2 * y))),//* Math.Sqrt(1 - Math.Pow((1 - 2 * y), 2)),
                            //Y = (scalar) * (1 - 2 * y),
                            //Z = scalar * Math.Sin(2 * Math.PI * x) * ((1 - Math.Cos(2 * Math.PI * (1 - 2 * y))))// * Math.Sqrt(1 - Math.Pow((1 - 2 * y), 2))

                            X = scalar * Math.Cos(2 * Math.PI * x) * (2 * y),//* Math.Sqrt(1 - Math.Pow((1 - 2 * y), 2)),
                            Y = (scalar) * (1 - 2 * y),
                            Z = scalar * Math.Sin(2 * Math.PI * x) * (2 * y)// * Math.Sqrt(1 - Math.Pow((1 - 2 * y), 2))
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
