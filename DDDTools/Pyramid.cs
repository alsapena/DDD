using System.Drawing;
using DDD;
using Rectangle = DDDTools.Rectangle;

namespace DDDTools
{
    public class Pyramid : Figure
    {
        public Pyramid(int p)
        {
            MyPoint one = new MyPoint(-p, -p, p);
            MyPoint two = new MyPoint(p, -p, p);
            MyPoint three = new MyPoint(p,-p,-p);
            MyPoint four = new MyPoint(-p,-p,-p);
            MyPoint five = new MyPoint(0, p, 0);

            Faces = new Face[]
            {
                new Rectangle(new[] {one, two, three, four}),
                new Triangle(new[] {one, two, five}),
                new Triangle(new[] {two, three, five}),
                new Triangle(new[] {three, four, five}),
                new Triangle(new[] {four, one, five})
            };

            SetColor(0, Color.Blue);
            SetColor(1, Color.Red);
            SetColor(2, Color.White);
            SetColor(3, Color.Yellow);
            SetColor(4, Color.Tomato);

            //SetColor(0, Color.Blue);
            //SetColor(1, Color.Blue);
            //SetColor(2, Color.Blue);
            //SetColor(3, Color.Blue);
            //SetColor(4, Color.Blue);
        }

        public override void Draw(Graphics g, PointF f)
        {
            Tools.Paint(g, this, f, true, false, Color.Aqua, 1);
        }
    }
}
