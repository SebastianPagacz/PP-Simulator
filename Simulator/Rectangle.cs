namespace Simulator
{
    internal class Rectangle
    {
        public readonly int X1;
        public readonly int Y1;
        public readonly int X2;
        public readonly int Y2;

        public Rectangle(int x1, int y1, int x2, int y2)
        {
            if (x1 == x2 || y1 == y2)
            {
                throw new ArgumentException("Collinear point are not allowed");
            }

            X1 = Math.Min(x1, x2);
            Y1 = Math.Min(y1, y2);
            X2 = Math.Max(x2, x1);
            Y2 = Math.Max(y2, y1);
        }

        public Rectangle(Point p1, Point p2) : this(p1.X, p1.Y, p2.X, p2.Y)
        {
            
        }

        public bool Contains(Point point)
        {
            return X1 <= point.X && Y1 <= point.Y && X2 >= point.X && Y2 >= point.Y;
        }

        public override string ToString()
        {
            return $"({X1},{Y1}):({X2},{Y2})";
        }
    }
}
