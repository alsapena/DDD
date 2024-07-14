using System;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;

namespace DDDTools
{
    public class Rectangle : Face
    {
        public Rectangle(MyPoint[] points) : base(points)
        {
            if(points.Length != 4)
                throw new Exception();
        }

        public override int CountPoint
        {
            get { return 4; }
        }

        
    }
}
