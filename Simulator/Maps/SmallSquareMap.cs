namespace Simulator.Maps;

public class SmallSquareMap : SmallMap
{
    public SmallSquareMap(int x, int y) : base(x,y)
    {
       if (x > 20 || y > 20)
       {
           throw new ArgumentException("Maximum size for a SmallSquareMap is 20.");
       }
        Console.WriteLine($"[DEBUG] Created SmallSquareMap with size {x}x{y}.");
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


