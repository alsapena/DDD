using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DDDTools;

namespace PolygonTest
{
    public partial class Form1 : Form
    {
        private List<Point> polygon;
        public Form1()
        {
            InitializeComponent();
            polygon = new List<Point>();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(Pens.Black, 0, pictureBox1.Height/2f, pictureBox1.Width, pictureBox1.Height/2f);
            e.Graphics.DrawLine(Pens.Black, pictureBox1.Width/2f, 0, pictureBox1.Width/2f, pictureBox1.Height);

            if (polygon.Count > 1)
                e.Graphics.DrawPolygon(Pens.Blue, polygon.ToArray());
        }

        private bool polygonC;
        private void button1_Click(object sender, EventArgs e)
        {
            if (!polygonC)
            {
                button1.Text = "Draw";
                polygon = new List<Point>();
            }
            else
            {
                button1.Text = "Polygon";
            }

            polygonC = !polygonC;

        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (polygonC)
            {
                polygon.Add(e.Location);
                pictureBox1.Invalidate();
            }
            else
            {
                if (polygon.Count > 2)
                {
                    MyPoint center = new MyPoint(pictureBox1.Width / 2f, pictureBox1.Height / 2f);
                    
                    string text =
                        Tools.PolygonContains(
                            polygon.Select(
                                point =>
                                    Tools.ConvertToAxis(center,
                                        new MyPoint(point.X, point.Y, 0))).ToList(),
                            Tools.ConvertToAxis(center, new MyPoint(e.X, e.Y, 0)))
                            ? "In"
                            : "Out";

                    MessageBox.Show(text, "Salida", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            MyPoint m = Tools.ConvertToAxis(new MyPoint(pictureBox1.Width / 2f, pictureBox1.Height / 2f), new MyPoint(e.X, e.Y, 0));
            label1.Text = string.Format("X: {0} ,Y: {1}", m.X, m.Y);
        }


    }
}
