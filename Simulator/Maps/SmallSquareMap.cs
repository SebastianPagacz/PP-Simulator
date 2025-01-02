namespace Simulator.Maps;

public class SmallSquareMap : SmallMap
{
    public SmallSquareMap(int sizeX, int sizeY) : base(sizeX, sizeY)
    {
        SizeX = sizeX;
        SizeY = sizeY;
    }

    public int SizeX { get; }
    public int SizeY { get; }
    public override bool Exist(Point p)
    {
        return p.X >= 0 && p.Y >= 0 && p.X < SizeX && p.Y < SizeY;
    }
    public override Point Next(Point p, Direction d)
    {
        Point n = p.Next(d);
        if (Exist(n))
        {
            return n;
        }
        else
        {
            return p;
        }
    }

    public override Point NextDiagonal(Point p, Direction d)
    {
        Point n = p.NextDiagonal(d);
        if (Exist(n))
        {
            return n;
        }
        else
        {
            return p;
        }
    }
}


