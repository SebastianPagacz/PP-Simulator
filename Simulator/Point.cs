namespace Simulator;

    public readonly struct Point
    {
        public readonly int X, Y;
        public Point(int x, int y) => (X, Y) = (x, y);
        public override string ToString() => $"({X}, {Y})";
    //Nie dziala tak jak powinno 
        public Point Next(Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:
                    return new Point(X - 1, Y);
                case Direction.Right:
                    return new Point(X + 1, Y);
                case Direction.Up:
                    return new Point(X, Y + 1);
                case Direction.Down:
                    return new Point(X, Y - 1);
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), "Invalid direction");
            }
        }

    //Nie dziala tak jak powinno 
    //rotate given direction 45 degrees clockwise
    public Point NextDiagonal(Direction direction)
    {
        switch (direction)
        {
            case Direction.Left:
                return new Point(X - 1, Y + 1);
            case Direction.Right:
                return new Point(X + 1, Y - 1);
            case Direction.Up:
                return new Point(X + 1, Y + 1);
            case Direction.Down:
                return new Point(X - 1, Y - 1);
            default:
                throw new ArgumentOutOfRangeException(nameof(direction), "Invalid direction");
        }
    }
}
