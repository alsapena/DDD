using System.Drawing;
using DDD;

namespace DDDTools
{
    public class Image:Figure
    {
        public Image( Bitmap b )
        {
            Bitmap = b;

            Rectangle = new System.Drawing.Rectangle(0, 0, 270, 330);
        }

        public System.Drawing.Rectangle Rectangle { get; set; }

        public Bitmap Bitmap { get; set; }

        public override void Draw(Graphics g, PointF f)
        {
            for (int i = 0; i < Bitmap.Width; i++)
            {
                for (int j = 0; j < Bitmap.Height; j++)
                {
                    double x = i*Rectangle.Width/(double)Bitmap.Width;
                    double y = j*Rectangle.Height/(double) Bitmap.Height;

                    MyPoint m = new MyPoint(x - Rectangle.Width/2f, Rectangle.Height/2f  - y , 0);

                    Tools.Rotar(10, -40, -5, m);

                    //Pen p = new Pen(Bitmap.GetPixel(i, j));
                    //g.DrawLine(p, (float) (f.X + m.X), (float) (f.Y + m.Y), (float) (f.X + m.X), (float) (f.Y + m.Y));

                    Brush b = new SolidBrush(Bitmap.GetPixel(i, j));

                    g.FillEllipse(b, (float)(f.X + m.X + Rectangle.Width / 2f), (float)(f.Y - m.Y + Rectangle.Height / 2f), 2, 2);

                }
            }
        }
    }
}
