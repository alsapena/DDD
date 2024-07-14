using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDDTools;
using Rectangle = DDDTools.Rectangle;

namespace CubeRubik
{
    public class Cube2H : Cube2
    {
        public Cube2H(MyPoint center, float profundidad)
            : base(center, profundidad)
        {
        }

        public Tuple<int, int, int> Tuple { get; set; }

        public Tuple<int, int, int> OriginalTuple { get; set; }

        public Cube2H(MyPoint center, float profundidad, int i, int j, int k)
            : base(center, profundidad)
        {


            float half = profundidad / 2;

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
                    new RectangleH(new[] {myPoints2, myPoints3, myPoints5, myPoints4}, i, j, k,1,1) {Color = Color.Black},
                    new RectangleH(new[] {myPoints2, myPoints3, myPoints7, myPoints6}, i, j, k,2,2) {Color = Color.Black},
                    new RectangleH(new[] {myPoints6, myPoints7, myPoints9, myPoints8}, i, j, k,3,3) {Color = Color.Black},
                    new RectangleH(new[] {myPoints8, myPoints9, myPoints5, myPoints4}, i, j, k,4,4) {Color = Color.Black},

                    //arriba y abajo
                    new RectangleH(new[] {myPoints5, myPoints3, myPoints7, myPoints9}, i, j, k,5,5) {Color = Color.Black},
                    new RectangleH(new[] {myPoints4, myPoints2, myPoints6, myPoints8}, i, j, k,6,6) {Color = Color.Black}

                };

            Tuple = new Tuple<int, int, int>(i, j, k);
            OriginalTuple = new Tuple<int, int, int>(i, j, k);

        }

        public override void Draw(Graphics g, PointF f, float xAngle, float yAngle, float zAngle)
        {
            foreach (var face in Faces)
            {
                ((RectangleH) face).pos = Tuple;
            }

            base.Draw(g, f, xAngle, yAngle, zAngle);
            
        }

        public override void Draw(Graphics g, PointF f, double[,] world)
        {
            foreach (var face in Faces)
            {
                ((RectangleH)face).pos = Tuple;
            }

            base.Draw(g, f, world);

        }
    }
}
