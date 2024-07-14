using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Markup;
using DDDTools;

namespace DDD
{
    public partial class Form1 : Form
    {
        private Box b;
        private Pyramid py;
        private Sphere sp;
        private Cylinder cy;
        private Vase v;
        private Cone c;
        private Torus t;

        private Egg egg;

        public double[,] word;

        //private DDDTools.Image i;
        public Form1()
        {
            InitializeComponent();
            b = new Box(new MyPoint(100, 100, 100),
                100, true);

            this.b.ColorDeLado(0, Color.Bisque);
            this.b.ColorDeLado(1, Color.Blue);
            this.b.ColorDeLado(2, Color.Green);
            this.b.ColorDeLado(3, Color.Yellow);
            this.b.ColorDeLado(4, Color.Red);
            this.b.ColorDeLado(5, Color.White);

            py = new Pyramid(50);
            sp = new Sphere();
            //Tools.Rotar(0, 0, 30, py);
            cy = new Cylinder();
            v = new Vase();
            c = new Cone();
            egg = new Egg();

            t = new Torus();
            word = Tools.Identity();
            //i = new DDDTools.Image(Properties.Resources.Captura);

        }

        private int p;
        private int q;
        private int s;
        //private int t;

        private void timer1_Tick(object sender, EventArgs e)
        {

            this.b.RotarZ(1);
            this.b.RotarY(1);
            this.b.RotarX(1);

            //Tools.Rotar(s, q, p, word);

            Tools.Rotar(1, 1, 1, py);
            Tools.Rotar(1, 1, 1, sp);
            Tools.Rotar(1, 1, 1, cy);
            Tools.Rotar(1, 1, 1, v);

            Tools.Rotar(1, 1, 1, c);
            Tools.Rotar(0, 1, 0, egg);

            Tools.Rotar(1, 1, 1, t);

            //this.t++;


            pictureBox1.Invalidate();
            pictureBox2.Invalidate();

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            b.Colorear(e.Graphics);

            py.Draw(e.Graphics, new PointF(100, 400));
            sp.Draw(e.Graphics, new PointF(650, 200));
            cy.Draw(e.Graphics, new PointF(350, 500));
            v.Draw(e.Graphics, new PointF(350, 200));

            t.Draw(e.Graphics, new PointF(650, 470));

            //e.Graphics.FillEllipse(Brushes.Aqua, 100, 300, 2  , 2);
            //e.Graphics.
        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            //i.Draw(e.Graphics, new PointF(50, 50));
            //i.Draw(e.Graphics, new PointF(20, 70));

            c.Draw(e.Graphics, new PointF(150, 100));

            egg.Draw(e.Graphics, new PointF(200, 400));
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
            pictureBox2.Invalidate();
        }
    }
}
