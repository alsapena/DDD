using System.Drawing;

namespace DDDTools
{
    public abstract class Figure
    {
        public Face[] Faces { get; set; }

        public Figure(Face[] faces)
        {
            Faces = faces;
        }

        public Figure()
        {

        }

        public void SetColor(int face, Color c)
        {
            Faces[face].Color = c;

        }

        public virtual void Draw(Graphics g, PointF f)
        {
            Tools.Paint(g, this, f, false, true, Color.Aqua, 1);
        }



        public virtual void Draw(Graphics g, PointF f, float xAngle, float yAngle, float zAngle)
        {
            Tools.Paint(g, this, f, false, true, Color.Aqua, 1, xAngle, yAngle, zAngle);
        }

        public virtual void Draw(Graphics g, PointF f, double[,]world)
        {
            Tools.Paint(g, this, f, false, true, Color.Aqua, 1, world);
        }
    }

}
