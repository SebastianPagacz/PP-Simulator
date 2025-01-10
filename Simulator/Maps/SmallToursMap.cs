namespace Simulator.Maps;

public class SmallTorusMap : SmallMap
{
    public int SizeX { get; }
    public int SizeY { get; }
    
    public SmallTorusMap(Point point) : base(point)
    {
        
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
