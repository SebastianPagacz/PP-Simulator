namespace Simulator.Maps;

public class SmallSquareMap : SmallMap
{
    public SmallSquareMap(int size) : base(new Point(size, size))
    {
       if (size > 20)
       {
           throw new ArgumentException("Maximum size for a SmallSquareMap is 20.");
       }
        Console.WriteLine($"[DEBUG] Created SmallSquareMap with size {size}x{size}.");
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


