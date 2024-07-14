using System;
using System.Collections.Generic;
using System.Drawing;

namespace DDDTools
{
    public static class Tools
    {

        public static MyPoint MP(double[,] y, MyPoint x)
        {
            double[,] temp = new double[x.DimensionHomogenea, 1];

            MyPoint result = new MyPoint();

            for (int i = 0; i < x.DimensionHomogenea; i++)
                temp[i, 0] = x[i];

            temp = MP(y, temp);

            for (int i = 0; i < x.DimensionHomogenea; i++)
                result[i] = temp[i, 0];

            return result;
        }

        public static double[,] Identity()
        {
            double[,] result = new double[4, 4];

            for (int i = 0; i < 4; i++)
            {
                result[i, i] = 1;
            }

            return result;
        }

        public static double[,] MP(double[,] x, double[,] y)
        {
            if (x.GetLength(1) != y.GetLength(0))
                throw new Exception("Las matrizes no son compatibles para la multiplicacion");

            double[,] resultante = new double[x.GetLength(0), y.GetLength(1)];

            for (int k = 0; k < y.GetLength(1); k++)
                for (int i = 0; i < x.GetLength(0); i++)
                    for (int j = 0; j < x.GetLength(1); j++)
                        resultante[i, k] += x[i, j] * y[j, k];

            return resultante;
        }

        public static double ConvertirARadianes(double Sexagesimal)
        {
            return (Sexagesimal / 180) * Math.PI;
        }

        private static double[,] MRotarX(double grados)
        {
            double cos = Math.Cos(ConvertirARadianes(grados));
            double sen = Math.Sin(ConvertirARadianes(grados));
            return new double[,]
            {
                {1, 0, 0, 0},
                {0, cos, -sen, 0},
                {0, sen, cos, 0},
                {0, 0, 0, 1}
            };
        }

        private static double[,] MRotarY(double grados)
        {
            double cos = Math.Cos(ConvertirARadianes(grados));
            double sen = Math.Sin(ConvertirARadianes(grados));
            return new double[,]
            {
                {cos, 0, sen, 0},
                {0, 1, 0, 0},
                {-sen, 0, cos, 0},
                {0, 0, 0, 1}
            };
        }

        private static double[,] MRotarZ(double grados)
        {
            double cos = Math.Cos(ConvertirARadianes(grados));
            double sen = Math.Sin(ConvertirARadianes(grados));
            return new double[,]
            {
                {cos, -sen, 0, 0},
                {sen, cos, 0, 0},
                {0, 0, 1, 0},
                {0, 0, 0, 1}
            };
        }

        private static double[,] MTrasladar(double x, double y, double z)
        {
            return new double[,]
            {
                {1, 0, 0, x},
                {0, 1, 0, y},
                {0, 0, 1, z},
                {0, 0, 0, 1}
            };
        }

        public static double[,] MEscalar(double x, double y, double z)
        {
            return new double[,]
            {
                {x, 0, 0, 0},
                {0, y, 0, 0},
                {0, 0, z, 0},
                {0, 0, 0, 1}
            };
        }

        private static double[,] MEscalar(double p)
        {
            return MEscalar(p, p, p);
        }

        public static MyPoint Proyeccion(MyPoint[] _myPoints, int punto)
        {
            return new MyPoint(_myPoints[punto].X, _myPoints[punto].Y, 0);
        }

        public static void RotarZ(MyPoint[] _myPoints, double x, double y, double z, int grados)
        {
            double[,] transformacion = MP(MTrasladar(x, y, z), MP(MRotarZ(grados), MTrasladar(-x, -y, -z)));
            for (int i = 0; i < _myPoints.Length; i++)
                _myPoints[i] = MP(transformacion, _myPoints[i]);
        }

        public static void RotarY(MyPoint[] _myPoints, double x, double y, double z, int grados)
        {
            double[,] transformacion = MP(MTrasladar(x, y, z), MP(MRotarY(grados), MTrasladar(-x, -y, -z)));
            for (int i = 0; i < _myPoints.Length; i++)
                _myPoints[i] = MP(transformacion, _myPoints[i]);
        }


        public static void RotarZ(MyPoint[] my, MyPoint P, int grados)
        {
            RotarZ(my, P.X, P.Y, P.Z, grados);
        }

        public static void RotarX(MyPoint[] _myPoints, double x, double y, double z, int grados)
        {
            double[,] transformacion = MP(MTrasladar(x, y, z), MP(MRotarX(grados), MTrasladar(-x, -y, -z)));
            for (int i = 0; i < _myPoints.Length; i++)
                _myPoints[i] = MP(transformacion, _myPoints[i]);
        }

        public static void RotarX(MyPoint[] my, MyPoint P, int grados)
        {
            RotarX(my, P.X, P.Y, P.Z, grados);
        }

        public static void RotarY(MyPoint[] mypoint, MyPoint P, int grados)
        {
            RotarY(mypoint, P.X, P.Y, P.Z, grados);
        }


        public static void Escalar(MyPoint[] _myPoints, double x, double y, double z, double Proporcion)
        {
            double[,] transformacion = MP(MTrasladar(x, y, z), MP(MEscalar(Proporcion), MTrasladar(-x, -y, -z)));
            for (int i = 0; i < _myPoints.Length; i++)
                _myPoints[i] = MP(transformacion, _myPoints[i]);
        }

        public static void Escalar(MyPoint[] my, MyPoint P, double Proporcion)
        {
            Escalar(my, P.X, P.Y, P.Z, Proporcion);
        }

        public static void Paint(Graphics g, Figure f, PointF p, bool faces, bool edge, Color edgeColor, int pen, double[,] word)
        {
            //Array.Sort(f.Faces,
            //    (x, y) =>
            //        Face.AverageZ(x).CompareTo(Face.AverageZ(y)));

            Array.Sort(f.Faces,
                (x, y) =>
                    Face.AverageZ(x, word).CompareTo(Face.AverageZ(y, word)));

            for (int i = 0; i < f.Faces.Length; i++)
            {
                f.Faces[i].Draw(g, p, faces, edge, edgeColor, pen, word);
            }

            //for (int i = f.Faces.Length - 1; i >= 0; i--)
            //{
            //    f.Faces[i].Draw(g, p);
            //}
        }

        public static void Paint(Graphics g, Figure f, PointF p, bool faces, bool edge, Color edgeColor, int pen)
        {
            Array.Sort(f.Faces,
                (x, y) =>
                    Face.AverageZ(x).CompareTo(Face.AverageZ(y)));

            for (int i = 0; i < f.Faces.Length; i++)
            {
                f.Faces[i].Draw(g, p, faces, edge, edgeColor, pen);
            }

            //for (int i = f.Faces.Length - 1; i >= 0; i--)
            //{
            //    f.Faces[i].Draw(g, p);
            //}
        }

        public static void Paint(Graphics g, Figure f, PointF p, bool faces, bool edge, Color edgeColor, int pen, float xAngle, float yAngle, float zAngle)
        {
            Array.Sort(f.Faces,
                (x, y) =>
                    Face.AverageZ(x, xAngle, yAngle, zAngle).CompareTo(Face.AverageZ(y, xAngle, yAngle, zAngle)));

            for (int i = 0; i < f.Faces.Length; i++)
            {
                f.Faces[i].Draw(g, p, faces, edge, edgeColor, pen, xAngle, yAngle, zAngle);
            }

            //for (int i = f.Faces.Length - 1; i >= 0; i--)
            //{
            //    f.Faces[i].Draw(g, p);
            //}
        }

        public static void Rotar(int x, int y, int z, Figure f)
        {
            double[,] transformacion = MP(MRotarX(x), MP(MRotarY(y), MRotarZ(z)));
            //double[,] transformacion = MRotarZ(z);

            //transformacion = MP(MTrasladar(, -y, 0), MP(transformacion, MTrasladar(0, 0, 0)));

            for (int i = 0; i < f.Faces.Length; i++)
            {
                for (int j = 0; j < f.Faces[i].CountPoint; j++)
                {
                    //double[,] m = MP(MTrasladar(-xP, -yP, -zP), MP(transformacion, MTrasladar(xP, yP, zP)));
                    f.Faces[i].Points[j] = MP(transformacion, f.Faces[i].Points[j]);
                }
            }
        }

        public static void Rotar(int x, int y, int z, double[,] f)
        {
            double[,] transformacion = MP(MRotarX(x), MP(MRotarY(y), MRotarZ(z)));
            //double[,] transformacion = MRotarZ(z);

            //transformacion = MP(MTrasladar(, -y, 0), MP(transformacion, MTrasladar(0, 0, 0)));


            //double[,] m = MP(MTrasladar(-xP, -yP, -zP), MP(transformacion, MTrasladar(xP, yP, zP)));
            double[,] temp = MP(transformacion, f);

            for (int i = 0; i < f.GetLength(0); i++)
            {
                for (int j = 0; j < f.GetLength(1); j++)
                {
                    f[i, j] = temp[i, j];
                }
            }

        }

        public static void Rotar(double x, double y, double z, double[,] f)
        {
            double[,] transformacion = MP(MRotarX(x), MP(MRotarY(y), MRotarZ(z)));
            //double[,] transformacion = MRotarZ(z);

            //transformacion = MP(MTrasladar(, -y, 0), MP(transformacion, MTrasladar(0, 0, 0)));


            //double[,] m = MP(MTrasladar(-xP, -yP, -zP), MP(transformacion, MTrasladar(xP, yP, zP)));
            double[,] temp = MP(transformacion, f);

            for (int i = 0; i < f.GetLength(0); i++)
            {
                for (int j = 0; j < f.GetLength(1); j++)
                {
                    f[i, j] = temp[i, j];
                }
            }

        }

        public static void Rotar(int x, int y, int z, MyPoint m)
        {
            double[,] transformacion = MP(MRotarX(x), MP(MRotarY(y), MRotarZ(z)));

            MyPoint newPoint = MP(transformacion, m);

            m.X = newPoint.X;
            m.Y = newPoint.Y;
            m.Z = newPoint.Z;

        }

        public static MyPoint RotarR(float x, float y, float z, MyPoint m)
        {
            double[,] transformacion = MP(MRotarX(x), MP(MRotarY(y), MRotarZ(z)));

            MyPoint newPoint = MP(transformacion, m);

            return newPoint;

        }

        /// <summary>
        /// Convierte a un  eje
        /// </summary>
        /// <param name="center">El centro</param>
        /// <param name="a">El punto a convertir</param>
        /// <returns>El nuevo punto</returns>
        public static MyPoint ConvertToAxis(MyPoint center, MyPoint a)
        {
            double X = a.X - center.X;
            double Y = center.Y - a.Y;

            return new MyPoint(X, Y, 0);
        }

        public static bool PolygonContains(List<MyPoint> inVertices, MyPoint inPoint)
        {
            return PolygonContainsAntiHourlySense(inVertices, inPoint) ||
                   PolygonContainsHourlySense(inVertices, inPoint);
        }

        private static bool PolygonContainsAntiHourlySense(List<MyPoint> inVertices, MyPoint inPoint)
        {
            MyPoint v2 = inVertices[inVertices.Count - 1];
            // Loop through edges

            for (int i = 0; i < inVertices.Count; i++)
            {
                MyPoint v1 = inVertices[i];

                // If the point is outside this edge, the point is outside the polygon
                MyPoint v1V2 = v2 - v1;
                MyPoint v1Point = inPoint - v1;

                if (v1V2.X * v1Point.Y - v1Point.X * v1V2.Y > 0.0f)
                {
                    //Console.WriteLine(v1V2.X*v1Point.Y - v1Point.X*v1V2.Y);
                    return false;
                }

                v2 = v1;
            }

            return true;
        }

        private static bool PolygonContainsHourlySense(List<MyPoint> inVertices, MyPoint inPoint)
        {
            MyPoint v2 = inVertices[inVertices.Count - 1];
            // Loop through edges

            for (int i = 0; i < inVertices.Count; i++)
            {
                MyPoint v1 = inVertices[i];

                // If the point is outside this edge, the point is outside the polygon
                MyPoint v1V2 = v2 - v1;
                MyPoint v1Point = inPoint - v1;

                if (v1V2.X * v1Point.Y - v1Point.X * v1V2.Y < 0.0f)
                {
                    //Console.WriteLine(v1V2.X*v1Point.Y - v1Point.X*v1V2.Y);
                    return false;
                }

                v2 = v1;
            }

            return true;
        }

        public static void Rotar(float x, float y, float z, Figure f)
        {
            double[,] transformacion = MP(MRotarX(x), MP(MRotarY(y), MRotarZ(z)));
            //double[,] transformacion = MRotarZ(z);

            //transformacion = MP(MTrasladar(, -y, 0), MP(transformacion, MTrasladar(0, 0, 0)));

            for (int i = 0; i < f.Faces.Length; i++)
            {
                for (int j = 0; j < f.Faces[i].CountPoint; j++)
                {
                    //double[,] m = MP(MTrasladar(-xP, -yP, -zP), MP(transformacion, MTrasladar(xP, yP, zP)));
                    f.Faces[i].Points[j] = MP(transformacion, f.Faces[i].Points[j]);
                }
            }
        }

        public static void RotarFirstCenter(float x, float y, float z, Figure f)
        {
            double[,] transformacion = MP(MRotarX(x), MP(MRotarY(y), MRotarZ(z)));
            //double[,] transformacion = MRotarZ(z);

            //transformacion = MP(MTrasladar(, -y, 0), MP(transformacion, MTrasladar(0, 0, 0)));

            for (int i = 0; i < f.Faces.Length; i++)
            {
                for (int j = 0; j < f.Faces[i].CountPoint; j++)
                {
                    double xP = f.Faces[i].Points[j].X;
                    double yP = f.Faces[i].Points[j].Y;
                    double zP = f.Faces[i].Points[j].Z;

                    double[,] m = MP(MTrasladar(-xP, -yP, -zP), MP(transformacion, MTrasladar(xP, yP, zP)));
                    f.Faces[i].Points[j] = MP(m, f.Faces[i].Points[j]);
                }
            }
        }
    }
}
