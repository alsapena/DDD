using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDTools
{
    //elipsoide
    //points.Add(
    //                        new MyPoint()
    //                        {
    //                            X = scalar * Math.Cos(2 * Math.PI * x) * (Math.Sqrt((1 - Math.Pow((1 - 2 * y), 2))*0.64)),//Math.Sqrt((2.5 + (1 - 2 * y))/2.5),
    //                            Y = (scalar) * (1 - 2 * y),
    //                            Z = scalar * Math.Sin(2 * Math.PI * x) * (Math.Sqrt((1 - Math.Pow((1 - 2 * y), 2)) * 0.64))
    //                        });

    public class Egg : Figure
    {
        public Egg()
        {
            List<MyPoint> points = new List<MyPoint>();
            double w = 37;
            int scalar = 100;
            int count = 0;
            int count1 = 0;
            for (double x = 0; x <= 1; x += 1 / w)
                for (double y = 0; y <= 1; y += 1 / w)
                {
                    if (y > 0.55)
                    {
                        points.Add(
                            new MyPoint()
                            {
                                X = scalar * Math.Cos(2 * Math.PI * x) * (Math.Sqrt(((2 - 2 * y)) /0.9)),//Math.Sqrt((2.5 + (1 - 2 * y))/2.5),
                                Y = (scalar) * (1.5 - 3 * y),
                                Z = scalar * Math.Sin(2 * Math.PI * x) * (Math.Sqrt(((2 - 2 * y)) / 0.9))
                            });
                        count++;

                    }
                    else
                    {
                        points.Add(
                            new MyPoint()
                            {
                                X = scalar * Math.Cos(2 * Math.PI * x) * (Math.Sqrt((1 - Math.Pow((1 - 2 * y), 2)))),//Math.Sqrt((2.5 + (1 - 2 * y))/2.5),
                                Y = (scalar) * (1 - 2 * y),
                                Z = scalar * Math.Sin(2 * Math.PI * x) * (Math.Sqrt((1 - Math.Pow((1 - 2 * y), 2))))
                            });
                        count1++;
                    }


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
