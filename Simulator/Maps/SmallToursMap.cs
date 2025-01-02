namespace Simulator.Maps;

public class SmallTorusMap : SmallMap
{
    public int SizeX { get; }
    public int SizeY { get; }
    
    public SmallTorusMap(int sizeX, int sizeY) : base(sizeX, sizeY)
    {
        SizeX = sizeX;
        SizeY = sizeY;
    }
    public override bool Exist(Point p)
    {
        return p.X >= 0 && p.X < SizeX && p.Y >= 0 && p.Y < SizeY;
    }

    public override Point Next(Point p, Direction d)
    {
        return d switch
        {
            Direction.Up => new Point(p.X, (p.Y + 1) % SizeY),
            Direction.Down => new Point(p.X, (p.Y - 1 + SizeY) % SizeY),
            Direction.Right => new Point((p.X + 1) % SizeX, p.Y),
            Direction.Left => new Point((p.X - 1 + SizeX) % SizeX, p.Y),
            _ => p
        };
    }

    public override Point NextDiagonal(Point p, Direction d)
    {
        return d switch
        {
            Direction.Up => new Point((p.X + 1) % SizeX, (p.Y + 1) % SizeY),
            Direction.Down => new Point((p.X - 1 + SizeX) % SizeX, (p.Y - 1 + SizeY) % SizeY),
            Direction.Right => new Point((p.X + 1) % SizeX, (p.Y - 1 + SizeY) % SizeY),
            Direction.Left => new Point((p.X - 1 + SizeX) % SizeX, (p.Y + 1) % SizeY),
            _ => p
        };
    }
}
