using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DDDTools;

namespace CubeRubik
{
    public class Rubik
    {
        private Cube2H[] cubes;


        public Rubik()
        {
            cubes = new Cube2H[27];
            int x = -100;
            int y = -100;
            int z = 100;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        cubes[3 * i + j + 9 * k] = new Cube2H(new MyPoint(j * 100 + x, i * 100 + y, z - k * 100), 100, i, j, k);

                        SetColor(i, j, k);
                    }
                }
            }
        }

        public Cube2H[] Cubes
        {
            get { return cubes; }
        }

        private void SetColor(int i, int j, int k)
        {
            //frente y atras
            if (k == 0)
                cubes[3 * i + j + 9 * k].Faces[0].Color = Color.Yellow;
            if (k == 2)
                cubes[3 * i + j + 9 * k].Faces[2].Color = Color.White;

            //lados
            if (j == 0)
                //cubes[3 * i + j + 9 * k].Faces[3].Color = Color.Orange;
                cubes[3*i + j + 9*k].Faces[3].Color = Color.FromArgb(255, 128, 0);
            if (j == 2)
                //cubes[3 * i + j + 9 * k].Faces[1].Color = Color.Red;237 28 36
                cubes[3 * i + j + 9 * k].Faces[1].Color = Color.FromArgb(237, 28, 10);

            //arriba y abajo
            if (i == 0)
                //cubes[3 * i + j + 9 * k].Faces[4].Color = Color.Green;
                 cubes[3 * i + j + 9 * k].Faces[4].Color = Color.FromArgb(58,224,35);
            if (i == 2)
                //cubes[3 * i + j + 9 * k].Faces[5].Color = Color.Blue;
                cubes[3 * i + j + 9 * k].Faces[5].Color = Color.FromArgb(0,30,200);


        }


        public void Draw(Graphics g, PointF f, double[,] word)
        {
            List<Cube2H> cube = new List<Cube2H>(cubes);

            //cube.Sort(
            //    (x, y) =>
            //        x.Average(xRotation, yRotation, zRotation).CompareTo(y.Average(xRotation, yRotation, zRotation)));

            cube.Sort(
                (x, y) =>
                    x.Average(word).CompareTo(y.Average(word)));

            for (int i = 0; i < cubes.Length; i++)
            {
                cube[i].Draw(g, f,word);
            }
        }

        public void Rotar(int x, int y, int z)
        {
            //for (int i = 0; i < cubes.Length; i++)
            //{
            //    Tools.Rotar(x, y, z, cubes[i]);
            //}

            SaveRotation(x, y, z);

        }

        public void Rotar(float x, float y, float z)
        {
            //for (int i = 0; i < cubes.Length; i++)
            //{
            //    Tools.Rotar(x, y, z, cubes[i]);
            //}

            SaveRotation(x, y, z);

        }

        //public void SwapRotationXPositive(int j)
        //{
        //    int[] x = { 0, 1, 2, 2, 2, 1, 0, 0 };
        //    int[] y = { 0, 0, 0, 1, 2, 2, 2, 1 };

        //    Cube2H temp = cubes[3 * x[0] + j + 9 * y[0]];
        //    Cube2H temp1 = cubes[3 * x[1] + j + 9 * y[1]];

        //    for (int i = 0; i < x.Length - 2; i++)
        //    {
        //        cubes[3 * x[i] + j + 9 * y[i]] = cubes[3 * x[i + 2] + j + 9 * y[i + 2]];
        //    }

        //    cubes[3 * x[x.Length - 2] + j + 9 * y[x.Length - 2]] = temp;
        //    cubes[3 * x[x.Length - 1] + j + 9 * y[x.Length - 1]] = temp1;

        //}

        //public void SwapRotationXNegative(int j)
        //{
        //    int[] x = { 0, 1, 2, 2, 2, 1, 0, 0 };
        //    int[] y = { 0, 0, 0, 1, 2, 2, 2, 1 };

        //    Cube2H temp = cubes[3 * x[x.Length - 1] + j + 9 * y[x.Length - 1]];
        //    Cube2H temp1 = cubes[3 * x[x.Length - 2] + j + 9 * y[x.Length - 2]];

        //    for (int i = x.Length - 1; i >= 2; i--)
        //    {
        //        cubes[3 * x[i] + j + 9 * y[i]] = cubes[3 * x[i - 2] + j + 9 * y[i - 2]];
        //    }

        //    cubes[3 * x[0] + j + 9 * y[0]] = temp;
        //    cubes[3 * x[1] + j + 9 * y[1]] = temp1;

        //}

        //public void SwapRotationYPositive(int i)
        //{
        //    int[] x = { 0, 1, 2, 2, 2, 1, 0, 0 };
        //    int[] y = { 0, 0, 0, 1, 2, 2, 2, 1 };

        //    Cube2H temp = cubes[3 * x[i] + 0 + 9 * y[0]];
        //    Cube2H temp1 = cubes[3 * x[i] + 1 + 9 * y[1]];

        //    for (int j = 0; j < x.Length - 2; j++)
        //    {
        //        cubes[3 * x[i] + j + 9 * y[j]] = cubes[3 * x[i + 2] + j + 9 * y[j + 2]];
        //    }

        //    cubes[3 * x[i] + x.Length - 2 + 9 * y[x.Length - 2]] = temp;
        //    cubes[3 * x[i] + x.Length - 1 + 9 * y[x.Length - 1]] = temp1;

        //}

        //public void SwapRotationYNegative(int i)
        //{
        //    int[] x = { 0, 1, 2, 2, 2, 1, 0, 0 };
        //    int[] y = { 0, 0, 0, 1, 2, 2, 2, 1 };

        //    Cube2H temp = cubes[3 * x[i] + x.Length - 1 + 9 * y[x.Length - 1]];
        //    Cube2H temp1 = cubes[3 * x[i] + x.Length - 2 + 9 * y[x.Length - 2]];

        //    for (int j = x.Length - 1; j >= 2; j--)
        //    {
        //        cubes[3 * x[i] + j + 9 * y[j]] = cubes[3 * x[i] + j - 2 + 9 * y[j - 2]];
        //    }

        //    cubes[3 * x[i] + 0 + 9 * y[0]] = temp;
        //    cubes[3 * x[i] + 1 + 9 * y[1]] = temp1;

        //}


        private float xRotation;
        private float yRotation;
        private float zRotation;

        private void SaveRotation(float x, float y, float z)
        {
            xRotation += x; //VerifyAngle(xRotation, x);

            yRotation += y; // VerifyAngle(yRotation, y);

            zRotation += z; //VerifyAngle(zRotation, z);
        }

        public Tuple<float, float, float> Rotations
        {
            get { return new Tuple<float, float, float>(xRotation, yRotation, zRotation); }
        }

        private float VerifyAngle(float rotation, float angle)
        {
            float n = rotation + angle;

            if (n >= 360)
                return n - 360;
            else if (n < 0)
                return 360 + n;
            else
                return n;
        }

        public Tuple<Face,Cube2H> GetFaceIntersection(MyPoint center, Point e)
        {
            Face result = null;
            Cube2H re = null;

            double z = double.MinValue;

            foreach (var cube in Cubes)
            {
                foreach (var face in cube.Faces)
                {
                    double nz = Face.AverageZ(face, xRotation, yRotation, zRotation);

                    //((DDDTools.Rectangle)face).Tuple.X==0 && ((DDDTools.Rectangle)face).Tuple.Y==1&&((DDDTools.Rectangle)face).Tuple.Z==1

                    if (Tools.PolygonContains(face.Points.Select(x => Tools.ConvertToAxis(center, Tools.RotarR(xRotation, yRotation, zRotation, x) + center)).ToList(),
                         Tools.ConvertToAxis(center, new MyPoint(e.X, e.Y, 0))) && nz > z)
                    //{
                    //if(Tools.PolygonContains(face.Points.Select(x => x + center).ToList(),
                    //new MyPoint(e.X, e.Y, 0))&& nz > z)
                    {
                        z = nz;
                        result = face;
                        re = cube;
                    }
                }
            }

            return new Tuple<Face, Cube2H>(result, re);
        }

        public Tuple<Face, Cube2H> GetFaceIntersection(MyPoint center, Point e, double[,] word)
        {
            Face result = null;
            Cube2H re = null;

            double z = double.MinValue;

            foreach (var cube in Cubes)
            {
                foreach (var face in cube.Faces)
                {
                    double nz = Face.AverageZ(face,word);

                    //((DDDTools.Rectangle)face).Tuple.X==0 && ((DDDTools.Rectangle)face).Tuple.Y==1&&((DDDTools.Rectangle)face).Tuple.Z==1

                    //if (Tools.PolygonContains(face.Points.Select(x => Tools.MP(word, x + center)).ToList(),
                    //     Tools.ConvertToAxis(center, new MyPoint(e.X, e.Y, 0))) && nz > z)
                    if (Tools.PolygonContains(face.Points.Select(x => Tools.ConvertToAxis(center, Tools.MP(word,x) + center)).ToList(),
                        Tools.ConvertToAxis(center, new MyPoint(e.X, e.Y, 0))) && nz > z)
                    //{
                    //if(Tools.PolygonContains(face.Points.Select(x => x + center).ToList(),
                    //new MyPoint(e.X, e.Y, 0))&& nz > z)
                    {
                        z = nz;
                        result = face;
                        re = cube;
                    }
                }
            }

            return new Tuple<Face, Cube2H>(result, re);
        }

        #region rotaciones

        public void FinshedRotation(int x, Axis a, int d)
        {
            switch (a)
            {
                case Axis.X:
                    if (d > 0)
                        SwapRotationXNegative(x);
                    else
                        SwapRotationXPositive(x);
                    break;
                case Axis.Y:
                    if (d > 0)
                        SwapRotationYPositive(x);
                    else
                        SwapRotationYNegative(x);
                    break;
                case Axis.Z:
                    if (d > 0)
                        SwapRotationZNegative(x);
                    else
                        SwapRotationZPositive(x);
                    break;
            }
        }

        public void SwapRotationXNegative(int j)
        {
            int[] x = { 0, 1, 2, 2, 2, 1, 0, 0 };
            int[] y = { 0, 0, 0, 1, 2, 2, 2, 1 };

            RealRotateAxis(x, y, j, Axis.X, RotaFaceXNegative);
        }

        private void SwapRotationXPositive(int j)
        {
            int[] x = { 0, 0, 1, 2, 2, 2, 1, 0 };
            int[] y = { 1, 2, 2, 2, 1, 0, 0, 0 };

            RealRotateAxis(x, y, j, Axis.X, RotaFaceXPositive);
        }

        private void SwapRotationYPositive(int j)
        {
            int[] x = { 0, 0, 1, 2, 2, 2, 1, 0 };
            int[] y = { 1, 2, 2, 2, 1, 0, 0, 0 };

            RealRotateAxis(x, y, j, Axis.Y, RotaFaceYPositive);
        }

        private void SwapRotationYNegative(int j)
        {
            int[] x = { 0, 1, 2, 2, 2, 1, 0, 0 };
            int[] y = { 0, 0, 0, 1, 2, 2, 2, 1 };

            RealRotateAxis(x, y, j, Axis.Y, RotaFaceYNegative);
        }

        private void SwapRotationZPositive(int j)
        {
            int[] x = { 0, 0, 1, 2, 2, 2, 1, 0 };
            int[] y = { 1, 2, 2, 2, 1, 0, 0, 0 };

            RealRotateAxis(x, y, j, Axis.Z, RotaFaceZPositive);
        }

        private void SwapRotationZNegative(int j)
        {
            int[] x = { 0, 1, 2, 2, 2, 1, 0, 0 };
            int[] y = { 0, 0, 0, 1, 2, 2, 2, 1 };

            RealRotateAxis(x, y, j, Axis.Z, RotaFaceZNegative);
        }

        private static int GetPosition(int[] x, int[] y, int col, Axis axis, int pos)
        {
            int position = 0;

            switch (axis)
            {
                case Axis.X:
                    position = 3 * x[pos] + col + 9 * y[pos];
                    break;
                case Axis.Y:
                    position = 3 * col + x[pos] + 9 * y[pos];
                    break;
                case Axis.Z:
                    position = 3 * x[pos] + y[pos] + 9 * col;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return position;
        }


        private void RealRotateAxis(int[] x, int[] y, int col, Axis axis, Action<Cube2H> actt)
        {
            Cube2H temp = cubes[GetPosition(x, y, col, axis, 0)];
            Cube2H temp1 = cubes[GetPosition(x, y, col, axis, 1)];

            for (int i = 0; i < x.Length - 2; i++)
            {
                int posTo = GetPosition(x, y, col, axis, i);
                int posFrom = GetPosition(x, y, col, axis, i + 2);

                cubes[posTo] = cubes[posFrom];

                cubes[posTo].Tuple = ToTuple(posTo);

                actt(cubes[posTo]);

            }

            int beforelast = GetPosition(x, y, col, axis, x.Length - 2);
            int last = GetPosition(x, y, col, axis, x.Length - 1);

            cubes[beforelast] = temp;

            cubes[beforelast].Tuple = ToTuple(beforelast);

            actt(Cubes[beforelast]);

            Cubes[last] = temp1;

            cubes[last].Tuple = ToTuple(last);

            actt(cubes[last]);
        }

        private Tuple<int, int, int> ToTuple(int i)
        {
            int z = i / 9;

            int y = (i % 9) % 3;

            int x = ((i % 9) / 3) % 3;

            return new Tuple<int, int, int>(x, y, z);
        }

        private void RotaFaceXPositive(Cube2H cu)
        {
            //front 1
            //right 2
            //back 3
            //left 4
            //up 5
            //down 6

            int[] f = { 6, 3, 5, 1, 6 };

            RealRotateFace(cu, f);

        }

        static void RealRotateFace(Cube2H c, int[] f)
        {
            Stack<Tuple<int, RectangleH>> s = new Stack<Tuple<int, RectangleH>>();

            for (int i = 0; i < f.Length - 1; i++)
            {
                RectangleH fi = (RectangleH)c.Faces.First(x => ((RectangleH)x).F == f[i]);

                s.Push(new Tuple<int, RectangleH>(f[i + 1], fi));
            }

            while (s.Count > 0)
            {
                var pair = s.Pop();

                pair.Item2.F = pair.Item1;
            }
        }

        private void RotaFaceXNegative(Cube2H cu)
        {
            //front 1
            //right 2
            //back 3
            //left 4
            //up 5
            //down 6

            int[] f = { 1, 5, 3, 6, 1 };

            RealRotateFace(cu, f);
        }

        private void RotaFaceYPositive(Cube2H cu)
        {
            //front 1
            //right 2
            //back 3
            //left 4
            //up 5
            //down 6

            int[] f = { 4, 1, 2, 3, 4 };

            RealRotateFace(cu, f);
        }

        private void RotaFaceYNegative(Cube2H cu)
        {
            //front 1
            //right 2
            //back 3
            //left 4
            //up 5
            //down 6

            int[] f = { 1, 4, 3, 2, 1 };

            RealRotateFace(cu, f);
        }

        private void RotaFaceZPositive(Cube2H cu)
        {
            //front 1
            //right 2
            //back 3
            //left 4
            //up 5
            //down 6

            int[] f = { 4, 6, 2, 5, 4 };

            RealRotateFace(cu, f);
        }

        private void RotaFaceZNegative(Cube2H cu)
        {
            //front 1
            //right 2
            //back 3
            //left 4
            //up 5
            //down 6

            int[] f = { 5, 2, 6, 4, 5 };

            RealRotateFace(cu, f);
        }

        #endregion


        public bool IsFinished()
        {
            for (int i = 0; i < cubes.Length; i++)
            {
                Cube2H c = cubes[i];

                if (c.OriginalTuple.Item1 != c.Tuple.Item1 || c.OriginalTuple.Item2 != c.Tuple.Item2 ||
                    c.OriginalTuple.Item3 != c.Tuple.Item3)
                    return false;

                if (c.Faces.Cast<RectangleH>().Any(rh => rh.F != rh.Of))
                {
                    return false;
                }
            }

            return true;
        }

        public bool IsFinished2()
        {
            int[] faces = {1, 2, 3, 4, 5, 6};

            Axis[] axies = {Axis.Z, Axis.X, Axis.Z, Axis.X, Axis.Y, Axis.Y};

            int[] cols = {0, 2, 2, 0, 0, 2};

            //sufre
            // return !faces.Where((t, i) => !CheckFaceColor(t, axies[i], cols[i])).Any();
            //es lo mismo
            for (int i = 0; i < faces.Length; i++)
            {
                if (!CheckFaceColor(faces[i], axies[i], cols[i]))
                    return false;
            }

            return true;
        }

        private bool CheckFaceColor(int face, Axis axis, int pos)
        {
            List<Color> colors = new List<Color>();

            for (int i = 0; i < 3; i++)
            {
                for (int k = 0; k < 3; k++)
                {
                    int position = 0;

                    switch (axis)
                    {
                        case Axis.X:
                            position = 3 * i + pos + 9 * k;
                            break;
                        case Axis.Y:
                            position = 3 * pos + i + 9 * k;
                            break;
                        case Axis.Z:
                            position = 3 * i + k + 9 * pos;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    RectangleH rh = cubes[position].Faces.Cast<RectangleH>().First(x => x.F == face);

                    colors.Add(rh.Color);
                }
            }

            return colors.Distinct().Count() == 1;
        }

        public void Shuffle(int count)
        {
            Random r = new Random(Environment.TickCount);

            for (int i = 0; i < count; i++)
            {
                int pos = r.Next(0, 3);

                int a = r.Next(0, 3);

                Axis axis = GetAxis(a);

                int dir = r.Next(0, 2);

                dir = dir == 0 ? -1 : 1;

                Rotate(axis, pos, dir);

            }
        }

        private void Rotate(Axis axis, int pos, int dir)
        {
            int cx = axis == Axis.X ? 1 : 0;
            int cy = axis == Axis.Y ? 1 : 0;
            int cz = axis == Axis.Z ? 1 : 0;

            for (int i = 0; i < 3; i++)
            {
                for (int k = 0; k < 3; k++)
                {
                    int position = 0;

                    switch (axis)
                    {
                        case Axis.X:
                            position = 3 * i + pos + 9 * k;
                            break;
                        case Axis.Y:
                            position = 3 * pos + i + 9 * k;
                            break;
                        case Axis.Z:
                            position = 3 * i + k + 9 * pos;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    Tools.Rotar(dir * 90 * cx, dir * 90 * cy, dir * 90 * cz, cubes[position]);
                }
            }

            FinshedRotation(pos, axis, dir);
        }

        public static Axis GetAxis(int e)
        {
            switch (e)
            {
                case 1:
                    return Axis.X;
                case 2:
                    return Axis.Y;
                case 3:
                    return Axis.Z;
            }

            return Axis.X;
        }
        
    }
}
