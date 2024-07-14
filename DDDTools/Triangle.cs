using System;

namespace DDDTools
{
    public class Triangle : Face
    {
        public Triangle(MyPoint[] points)
            : base(points)
        {
            if (points.Length != 3)
                throw new Exception();
        }

        public override int CountPoint
        {
            get { return 3; }
        }

    }
}
