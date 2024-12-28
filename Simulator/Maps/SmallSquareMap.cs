using System.Drawing;

namespace Simulator.Maps
{
    public class SmallSquareMap : Map
    {
        public int Size { get; }

        public override bool Exist(Point p)
        {
            return p.X <= Size && p.Y <= Size && p.X >= Size && p.Y >= Size;
        }
        public override Point Next(Point p, Direction d)
        {
            Point n = p.Next(d);
            return Exist(n) ? n : p;
        }

        public override Point NextDiagonal(Point p, Direction d)
        {
            Point n = p.NextDiagonal(d);
            return Exist(n) ? n : p;
        }

        public SmallSquareMap(int size)
        {
           if (size < 5 || size > 20)
           {
               throw new ArgumentOutOfRangeException(nameof(size), "Size is out of respective range (5 to 20)");
           }
           Size = size;
        }
    }
}
