using Simulator.Maps;
using Simulator;

public class SmallTorusMap : SmallMap
{
    public SmallTorusMap(int size) : base(new Point(size, size))
    {
        if (size > 20)
        {
            throw new ArgumentException("Maximum size for a SmallSquareMap is 20.");
        }
        Console.WriteLine($"[DEBUG] Created SmallSquareMap with size {size}x{size}.");
    }

    public override Point Next(Point p, Direction d)
    {
        int width = Point.X;
        int height = Point.Y;

        return d switch
        {
            Direction.Up => new Point(p.X, (p.Y - 1 + height) % height),
            Direction.Down => new Point(p.X, (p.Y + 1) % height),
            Direction.Right => new Point((p.X + 1) % width, p.Y),
            Direction.Left => new Point((p.X - 1 + width) % width, p.Y),
            _ => p
        };
    }

    public override Point NextDiagonal(Point p, Direction d)
    {
        int width = Point.X;
        int height = Point.Y;

        return d switch
        {
            Direction.Up => new Point((p.X + 1) % width, (p.Y - 1 + height) % height),
            Direction.Down => new Point((p.X - 1 + width) % width, (p.Y + 1) % height),
            Direction.Right => new Point((p.X + 1) % width, (p.Y + 1) % height),
            Direction.Left => new Point((p.X - 1 + width) % width, (p.Y - 1 + height) % height),
            _ => p
        };
    }
}
