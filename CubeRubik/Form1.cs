using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DDDTools;
using Timer = System.Threading.Timer;

namespace CubeRubik
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            c = new Rubik();
            word = Tools.Identity();

            c.Shuffle(30);

            timese = 90 / angleg;
        }

        private Rubik c;
        private double[,] word;

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            c.Draw(e.Graphics, new PointF(pictureBox1.Width / 2f, pictureBox1.Height / 2f), word);

            var cr = c.Rotations;

            label2.Text = cr.Item1.ToString();
            label3.Text = cr.Item2.ToString();
            label4.Text = cr.Item3.ToString();

        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            MyPoint center = new MyPoint(pictureBox1.Width/2f,pictureBox1.Height/2f);
            MyPoint loco = new MyPoint(e.X, e.Y);

            var lococs = Tools.ConvertToAxis(center, loco);

            label5.Text = string.Format("{0},{1}", lococs.X, lococs.Y); 

            if (click)
            {
                //if (e.X < old.X)
                //    Tools.Rotar(0, -4, 0, word);
                //if (e.X > old.X)
                //    Tools.Rotar(0, 4, 0, word);

                //if (e.Y < old.Y)
                //    Tools.Rotar(4, 0, 0, word);
                //if (e.Y > old.Y)
                //    Tools.Rotar(-4, 0, 0, word);

               
                MyPoint oldp = new MyPoint(old.X,old.Y);
                MyPoint ep = new MyPoint(e.X,e.Y);

                oldp = Tools.ConvertToAxis(center, oldp);
                ep = Tools.ConvertToAxis(center, ep);

                float angleyy = 0;
                float anglexx = 0;

                if (ep.X < oldp.X)
                    angleyy = -angle;
                if (ep.X > oldp.X)
                    angleyy = angle;

                if (ep.Y < oldp.Y)
                    anglexx = -angle;
                if (ep.Y > oldp.Y)
                    anglexx = angle;

                MRotate(anglexx, angleyy, 0);

                old = e.Location;

                pictureBox1.Invalidate();
            }

            if (clickInter)
            {

                Tuple<Face, Cube2H> r = c.GetFaceIntersection(center, e.Location,word);

                if (r.Item1 != null)
                {
                    Tuple<Axis, int, int> move = GetMove(oldclick, r);

                    if (move == null)
                        return;

                    Rotate(move.Item2, move.Item1, move.Item3);
                }
            }
        }

        private void MRotate(float anglex,float angley,float anglez)
        {
            Tuple<float, float, float> rotations = c.Rotations;

            float xr = rotations.Item1;
            float yr = rotations.Item2;
            float zr = rotations.Item3;

            //if (((int)xr/360)%2 == 1)
            //if(xr > 0)
            //    angley *= -1;

            //c.Rotar(-xr, -yr, -zr);

            Tools.Rotar(anglex, angley, anglez, word);

            //MyPoint p = Tools.MP(word, new MyPoint(1, 1, 1));

            //c.Rotar((float)p[0], (float)p[1], (float)p[2]);
            c.Rotar(anglex, angley, anglez);

            //c.Rotar(xr, yr, zr);
        }

        public bool VerifyArray(int[] a, int f1, int f2)
        {
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] == f1)
                {
                    return i <= a.Length - 2 && a[i + 1] == f2 || i == a.Length - 1 && a[0] == f2;
                }
            }

            return false;
        }

        private Tuple<Axis, int, int> GetMove(Tuple<Face, Cube2H> one, Tuple<Face, Cube2H> two)
        {
            RectangleH oner = (RectangleH) one.Item1;
            RectangleH twor = (RectangleH) two.Item1;

            if (oner.F != twor.F)
            {
                if (!one.Item2.Tuple.Equals(two.Item2.Tuple))
                    return null;

                int[] dirY = {1, 2, 3, 4};
                int[] dirX = {1, 5, 3, 6};
                int[] dirZ = {2, 5, 4, 6};

                //eje Y
                if (VerifyArray(dirY, oner.F, twor.F))
                    return new Tuple<Axis, int, int>(Axis.Y, one.Item2.Tuple.Item1, 1);
                if (VerifyArray(dirY.Reverse().ToArray(), oner.F, twor.F))
                    return new Tuple<Axis, int, int>(Axis.Y, one.Item2.Tuple.Item1, -1);

                //eje X
                if (VerifyArray(dirX, oner.F, twor.F))
                    return new Tuple<Axis, int, int>(Axis.X, one.Item2.Tuple.Item2, 1);
                if (VerifyArray(dirX.Reverse().ToArray(), oner.F, twor.F))
                    return new Tuple<Axis, int, int>(Axis.X, one.Item2.Tuple.Item2, -1);

                //eje Z
                if (VerifyArray(dirZ, oner.F, twor.F))
                    return new Tuple<Axis, int, int>(Axis.Z, one.Item2.Tuple.Item3, -1);
                if (VerifyArray(dirZ.Reverse().ToArray(), oner.F, twor.F))
                    return new Tuple<Axis, int, int>(Axis.Z, one.Item2.Tuple.Item3, 1);


            }

            switch (oner.F)
            {
                case 1:
                {
                    int e = DistinctAxis(one.Item2, two.Item2);

                    if (e == -1)
                        return null;

                    Axis a = (e == 0) ? Axis.X : Axis.Y;

                    int d = IndexTuple(one.Item2.Tuple, e) - IndexTuple(two.Item2.Tuple, e) < 0 ? -1 : 1;

                    if(a == Axis.Y)
                        d*=-1;

                    int c = a == Axis.X ? one.Item2.Tuple.Item2 : one.Item2.Tuple.Item1;

                    return new Tuple<Axis, int, int>(a, c, d);
                }
                case 2:
                {
                    int e = DistinctAxis(one.Item2, two.Item2);

                    if (e == -1)
                        return null;

                    Axis a = (e == 0) ? Axis.Z : Axis.Y;

                    int d = IndexTuple(two.Item2.Tuple, e) - IndexTuple(one.Item2.Tuple, e)   < 0 ? -1 : 1;

                    //if (a == Axis.Y)
                    //    d *= -1;

                    int c = a == Axis.Z ? one.Item2.Tuple.Item3 : one.Item2.Tuple.Item1;

                    return new Tuple<Axis, int, int>(a, c, d);
                }
                case 3:
                {
                    int e = DistinctAxis(one.Item2, two.Item2);

                    if (e == -1)
                        return null;

                    Axis a = (e == 0) ? Axis.X : Axis.Y;

                    int d = IndexTuple(two.Item2.Tuple, e) - IndexTuple(one.Item2.Tuple, e)  < 0 ? -1 : 1;

                    if (a == Axis.Y)
                        d *= -1;

                    int c = a == Axis.X ? one.Item2.Tuple.Item2 : one.Item2.Tuple.Item1;

                    return new Tuple<Axis, int, int>(a, c, d);
                }
                case 4:
                {
                    int e = DistinctAxis(one.Item2, two.Item2);

                    if (e == -1)
                        return null;

                    Axis a = (e == 0) ? Axis.Z : Axis.Y;

                    int d = IndexTuple(one.Item2.Tuple, e) - IndexTuple(two.Item2.Tuple, e)  < 0 ? -1 : 1;

                    //if (a == Axis.Y)
                    //    d *= -1;

                    int c = a == Axis.Z ? one.Item2.Tuple.Item3 : one.Item2.Tuple.Item1;

                    return new Tuple<Axis, int, int>(a, c, d);
                }
                case 5:
                {
                    int e = DistinctAxis(one.Item2, two.Item2);

                    if (e == -1)
                        return null;

                    Axis a = (e == 1) ? Axis.Z : Axis.X;

                    int d =   IndexTuple(two.Item2.Tuple, e) - IndexTuple(one.Item2.Tuple, e) < 0 ? -1 : 1;

                    int c = a == Axis.X ? one.Item2.Tuple.Item2 : one.Item2.Tuple.Item3;

                    return new Tuple<Axis, int, int>(a, c, d);
                }

                case 6:
                {
                    int e = DistinctAxis(one.Item2, two.Item2);

                    if (e == -1)
                        return null;

                    Axis a = (e == 1) ? Axis.Z : Axis.X;

                    int d = IndexTuple(one.Item2.Tuple, e) - IndexTuple(two.Item2.Tuple, e) < 0 ? -1 : 1;

                    int c = a == Axis.X ? one.Item2.Tuple.Item2 : one.Item2.Tuple.Item3;

                    return new Tuple<Axis, int, int>(a, c, d);
                }

            }

            return null;

            //if (one.Tuple.Item1 != two.Tuple.Item1)
            //{
            //    if (one.Tuple.Item1 > two.Tuple.Item1)
            //        return new Tuple<Axis, int, int>(Axis.X, one.Tuple.Item2, 1);
            //    else
            //        return new Tuple<Axis, int, int>(Axis.X, one.Tuple.Item2, -1);

            //}

            //if (one.Tuple.Item2 != two.Tuple.Item2)
            //{
            //    if (one.Tuple.Item2 > two.Tuple.Item2)
            //        return new Tuple<Axis, int, int>(Axis.Y, one.Tuple.Item1, 1);
            //    else
            //        return new Tuple<Axis, int, int>(Axis.Y, one.Tuple.Item1, -1);

            //}

            //if (one.Tuple.Item3 != two.Tuple.Item3)
            //{
            //    if (one.Tuple.Item3 > two.Tuple.Item3)
            //        return new Tuple<Axis, int, int>(Axis.Z, one.Tuple.Item1, 1);
            //    else
            //        return new Tuple<Axis, int, int>(Axis.Z, one.Tuple.Item1, -1);

            //}

            //return null;
        }

        private int IndexTuple(Tuple<int,int,int> t,int p)
        {
            switch (p)
            {
                case 0:
                    return t.Item1;
                case 1:
                    return t.Item2;
                case 2:
                    return t.Item3;
            }
            return -1;
        }

        private int DistinctAxis(Cube2H one, Cube2H two)
        {
            if (one.Tuple.Item1 != two.Tuple.Item1 && one.Tuple.Item2 == two.Tuple.Item2 &&
                one.Tuple.Item3 == two.Tuple.Item3)
                return 0;

            if (one.Tuple.Item1 == two.Tuple.Item1 && one.Tuple.Item2 != two.Tuple.Item2 &&
                one.Tuple.Item3 == two.Tuple.Item3)
                return 1;

            if (one.Tuple.Item1 == two.Tuple.Item1 && one.Tuple.Item2 == two.Tuple.Item2 &&
                one.Tuple.Item3 != two.Tuple.Item3)
                return 2;

            return -1;
        }

        private int angle = 5;

        private bool click;

        private Point old;

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            click = false;
            clickInter = false;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            click = false;
            clickInter = false;
        }

        private bool clickInter = false;

        private Tuple<Face, Cube2H> oldclick;

        private bool rotateting = false;

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if(rotateting)
                return;

            MyPoint center = new MyPoint(pictureBox1.Width / 2f, pictureBox1.Height / 2f);

            Tuple<Face, Cube2H> r = c.GetFaceIntersection(center, e.Location,word);

            if (r.Item1 != null)
            {
                //Rotate(1, Axis.Z, 1);
                //r.Item1.Color = Color.BlueViolet;
                oldclick = r;
                
                Tuple<int, int, int> g = r.Item2.Tuple;
                
                label1.Text = string.Format("{0},{1},{2}", g.Item1, g.Item2, g.Item3);

                pictureBox1.Invalidate();
                clickInter = true;
            }
            else
            {
                old = e.Location;
                click = true;
                clickInter = false;
            }
            //}
        }

        private int times = 0;
        private int iPos;
        private Axis Axis;
        private int iPosZ;
        private int directionsP;

        public void Init(int i,Axis a, int directions)
        {
            times = 0;
            iPos = i;
            Axis = a; 
            directionsP = directions;
        }

        public void Rotate(int i,Axis a, int directions)
        {
            if(rotateting)
                return;

            Init(i,a, directions);

            rotateting = true;

            System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();

            t.Interval = 10;
            t.Tick += TOnTick;
            t.Start();


        }

        int angleg = 6;
        int timese = 0;

        private void TOnTick(object sender, EventArgs eventArgs)
        {
            int cx = Axis == Axis.X ? 1 : 0;
            int cy = Axis == Axis.Y ? 1 : 0;
            int cz = Axis == Axis.Z ? 1 : 0;

            var cr = c.Rotations;

            float x = cr.Item1;
            float y = cr.Item2;
            float z = cr.Item3;

            for (int i = 0; i < 3; i++)
            {
                for (int k = 0; k < 3; k++)
                {
                    int position = 0;

                    switch (Axis)
                    {
                        case Axis.X:
                            position = 3*i + iPos + 9*k;
                            break;
                        case Axis.Y:
                            position = 3 * iPos + i + 9 * k;
                            break;
                        case Axis.Z:
                            position = 3 * i + k + 9 * iPos;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    //Tools.Rotar(-x, -y, -z, c.Cubes[position]);

                    Tools.Rotar(directionsP * angleg * cx, directionsP * angleg * cy, directionsP * angleg * cz, c.Cubes[position]);

                    //Tools.Rotar(directionsP * angleg * cx - x, directionsP * angleg * cy - y, directionsP * angleg * cz - z, c.Cubes[position]);

                    //Tools.Rotar(x, y, z, c.Cubes[position]);
                }
            }

            times++;

            pictureBox1.Invalidate();

            if (times >= timese)
            {
                System.Windows.Forms.Timer ef = sender as System.Windows.Forms.Timer;
                c.FinshedRotation(iPos,Axis, directionsP);
                rotateting = false;
                ef.Stop();

                if (c.IsFinished2())
                {
                    MessageBox.Show("You win");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int dir = checkBox1.Checked ? -1 : 1;

            int col = (int)numericUpDown1.Value;

            Rotate(col, Axis.X, dir);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int dir = checkBox2.Checked ? -1 : 1;

            int col = (int)numericUpDown2.Value;

            Rotate(col, Axis.Y, dir);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int dir = checkBox3.Checked ? -1 : 1;

            int col = (int)numericUpDown3.Value;

            Rotate(col, Axis.Z, dir);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (ColorDialog cd = new ColorDialog())
            {
                //cd.ShowHelp = true;
                //cd.FullOpen = true;
                //mspaicd.
                cd.ShowDialog();
            }
        }

        #region old  rotations

        //public void SwapRotationXPositive(int j)
        //{
        //    int[] x = { 0, 1, 2, 2, 2, 1, 0, 0 };
        //    int[] y = { 0, 0, 0, 1, 2, 2, 2, 1 };

        //    Tuple<int, int, int> temp = c.Cubes[3 * x[x.Length - 1] + j + 9 * y[x.Length - 1]].Tuple;
        //    Tuple<int, int, int> temp1 = c.Cubes[3 * x[x.Length - 2] + j + 9 * y[x.Length - 2]].Tuple;

        //    for (int i = x.Length - 1; i >= 2; i--)
        //    {
        //        c.Cubes[3 * x[i] + j + 9 * y[i]].Tuple = c.Cubes[3 * x[i - 2] + j + 9 * y[i - 2]].Tuple;
        //    }

        //    c.Cubes[3 * x[0] + j + 9 * y[0]].Tuple = temp1;
        //    c.Cubes[3 * x[1] + j + 9 * y[1]].Tuple = temp;

        //}

        //public void SwapRotationYPositive(int i)
        //{
        //    int[] x = { 0, 1, 2, 2, 2, 1, 0, 0 };
        //    int[] y = { 0, 0, 0, 1, 2, 2, 2, 1 };

        //    Tuple<int, int, int> temp = c.Cubes[3 * i + x[0] + 9 * y[0]].Tuple;
        //    Tuple<int, int, int> temp1 = c.Cubes[3 * i + x[1] + 9 * y[1]].Tuple;

        //    for (int j = 0; j < x.Length - 2; j++)
        //    {
        //        c.Cubes[3 * i + x[j] + 9 * y[j]].Tuple = c.Cubes[3 * i + x[j + 2] + 9 * y[j + 2]].Tuple;
        //    }

        //    c.Cubes[3 * i + x[x.Length - 2] + 9 * y[y.Length - 2]].Tuple = temp;
        //    c.Cubes[3 * i + x[x.Length - 1] + 9 * y[y.Length - 1]].Tuple = temp1;

        //}

        //public void SwapRotationYNegative(int i)
        //{
        //    int[] x = { 0, 1, 2, 2, 2, 1, 0, 0 };
        //    int[] y = { 0, 0, 0, 1, 2, 2, 2, 1 };

        //    Tuple<int, int, int> temp = c.Cubes[3 * i + x[x.Length - 2] + 9 * y[y.Length - 2]].Tuple;
        //    Tuple<int, int, int> temp1 = c.Cubes[3 * i + x[x.Length - 1] + 9 * y[y.Length - 1]].Tuple;

        //    for (int j = x.Length - 1; j >= 2; j--)
        //    {
        //        c.Cubes[3 * i + x[j] + 9 * y[j]].Tuple = c.Cubes[3 * i + x[j - 2] + 9 * y[j - 2]].Tuple;
        //    }

        //    c.Cubes[3 * i + x[0] + 9 * y[0]].Tuple = temp;
        //    c.Cubes[3 * i + x[1] + 9 * y[1]].Tuple = temp1;

        //}

        //public void SwapRotationZPositive(int i)
        //{
        //    int[] x = { 0, 1, 2, 2, 2, 1, 0, 0 };
        //    int[] y = { 0, 0, 0, 1, 2, 2, 2, 1 };

        //    Tuple<int, int, int> temp = c.Cubes[3 * x[0] + y[0] + 9 * i].Tuple;
        //    Tuple<int, int, int> temp1 = c.Cubes[3 * x[1] + y[1] + 9 * i].Tuple;

        //    for (int j = 0; j < x.Length - 2; j++)
        //    {
        //        c.Cubes[3 * x[j] + y[j] + 9 * i].Tuple = c.Cubes[3 * x[j + 2] + y[j + 2] + 9 * i].Tuple;
        //    }

        //    c.Cubes[3 * x[x.Length - 2] + y[y.Length - 2] + 9 * i].Tuple = temp;
        //    c.Cubes[3 * x[x.Length - 1] + y[y.Length - 1] + 9 * i].Tuple = temp1;

        //}

        //public void SwapRotationZNegative(int i)
        //{
        //    int[] x = { 0, 1, 2, 2, 2, 1, 0, 0 };
        //    int[] y = { 0, 0, 0, 1, 2, 2, 2, 1 };

        //    Tuple<int, int, int> temp = c.Cubes[3 * x[x.Length - 2] + y[y.Length - 2] + 9 * i].Tuple;
        //    Tuple<int, int, int> temp1 = c.Cubes[3 * x[x.Length - 1] + y[y.Length - 1] + 9 * i].Tuple;

        //    for (int j = x.Length - 1; j >= 2; j--)
        //    {
        //        c.Cubes[3 * x[j] + y[j] + 9 * i].Tuple = c.Cubes[3 * x[j - 2] + y[j - 2] + 9 * i].Tuple;
        //    }

        //    c.Cubes[3 * x[0] + y[0] + 9 * i].Tuple = temp;
        //    c.Cubes[3 * x[1] + y[1] + 9 * i].Tuple = temp1;

        //}

        #endregion

        #region Sin uso
        //public void RotateYAxis(int i, int directions)
        //{
        //    Init(i, directions);

        //    System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();

        //    t.Interval = 10;
        //    t.Tick += TOnTickY;
        //    t.Start();


        //}

        //private void TOnTickY(object sender, EventArgs eventArgs)
        //{
        //    for (int j = 0; j < 3; j++)
        //    {
        //        for (int k = 0; k < 3; k++)
        //        {
        //            Tools.Rotar(0, directionsP * 5, 0, c.Cubes[3 * iPos + j + 9 * k]);
        //        }
        //    }

        //    times++;

        //    pictureBox1.Invalidate();

        //    if (times >= 18)
        //    {
        //        System.Windows.Forms.Timer ef = sender as System.Windows.Forms.Timer;
        //        FinshedRotation(0, iPos, 0, directionsP);
        //        ef.Stop();
        //    }
        //}

        //public void RotateZAxis(int i, int directions)
        //{
        //    Init(i, directions);

        //    System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();

        //    t.Interval = 10;
        //    t.Tick += TOnTickZ;
        //    t.Start();


        //}

        //private void TOnTickZ(object sender, EventArgs eventArgs)
        //{
        //    for (int i = 0; i < 3; i++)
        //    {
        //        for (int j = 0; j < 3; j++)
        //        {
        //            Tools.Rotar(0, 0, directionsP * 5, c.Cubes[3 * i + j + 9 * iPos]);
        //        }
        //    }

        //    times++;

        //    pictureBox1.Invalidate();

        //    if (times >= 18)
        //    {
        //        System.Windows.Forms.Timer ef = sender as System.Windows.Forms.Timer;
        //        FinshedRotation(0, 0, iPos, directionsP);
        //        ef.Stop();
        //    }
        //}
        #endregion



    }
}
