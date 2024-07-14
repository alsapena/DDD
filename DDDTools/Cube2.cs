using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDTools
{
    public class Cube2:Figure
    {
        public Cube2(MyPoint center,float profundidad)
        {

            float half = profundidad/2;

            //front
            MyPoint myPoints2 = new MyPoint(center.X + half, center.Y + half, center.Z + half);
            MyPoint myPoints3 = new MyPoint(center.X + half, center.Y - half, center.Z + half);
            MyPoint myPoints4 = new MyPoint(center.X - half, center.Y + half, center.Z + half);
            MyPoint myPoints5 = new MyPoint(center.X - half, center.Y - half, center.Z + half);

            //back
            MyPoint myPoints6 = new MyPoint(center.X + half, center.Y + half, center.Z - half);
            MyPoint myPoints7 = new MyPoint(center.X + half, center.Y - half, center.Z - half);
            MyPoint myPoints8 = new MyPoint(center.X - half, center.Y + half, center.Z - half);
            MyPoint myPoints9 = new MyPoint(center.X - half, center.Y - half, center.Z - half);

            Faces = new Face[]
            {
                //lado
                new Rectangle(new[] {myPoints2, myPoints3, myPoints5, myPoints4}){Color = Color.Black},
                new Rectangle(new[] {myPoints2, myPoints3, myPoints7, myPoints6}){Color = Color.Black},
                new Rectangle(new[] {myPoints6, myPoints7, myPoints9, myPoints8}){Color = Color.Black},
                new Rectangle(new[] {myPoints8, myPoints9, myPoints5, myPoints4}){Color = Color.Black},

                //arriba y abajo
                new Rectangle(new[] {myPoints4, myPoints2, myPoints6, myPoints8}){Color = Color.Black},
                new Rectangle(new[] {myPoints5, myPoints3, myPoints7, myPoints9}){Color = Color.Black}
            };

            
        }

        public double Average()
        {
            return Faces.Average(x => Face.AverageZ(x));
        }

        public double Average(float xAngle,float yAngle,float zAngle)
        {
            return Faces.Average(x => Face.AverageZ(x, xAngle, yAngle, zAngle));
        }

        public double Average(double[,] word)
        {
            return Faces.Average(x => Face.AverageZ(x, word));
        }

        public override void Draw(Graphics g, PointF f,float xAngle,float yAngle,float zAngle)
        {
            Tools.Paint(g, this, f, true, true, Color.Black, 1, xAngle, yAngle, zAngle);
        }

        public override void Draw(Graphics g, PointF f, double[,] word)
        {
            Tools.Paint(g, this, f, true, true, Color.Black, 1,word);
        }
    }
}
