namespace Simulator.Maps;

public class SmallMap : Map
{
    protected const int MinSizeX = 5;
    protected const int MinSizeY = 5;

    private const int MaxSizeX = 20;
    private const int MaxSizeY = 20;


    private Dictionary<Point, List<IMappable>> mapItems = new();
    public Point Point { get; }
    public override bool Exist(Point p)
    {
        return p.X >= 0 && p.X < Point.X && p.Y >= 0 && p.Y < Point.Y;
    }

    public override Point Next(Point p, Direction d)
    {
        throw new NotImplementedException();
    }

    public override Point NextDiagonal(Point p, Direction d)
    {
        throw new NotImplementedException();
    }

    public SmallMap(int x, int y) : base(x, y)
    {
        Point point1 = new Point(x, y);
        ValidateSize(point1.X, point1.Y);

        if (point1.X > MaxSizeX || point1.Y > MaxSizeY)
        {
            throw new ArgumentException($"Map dimensions must not exceed {MaxSizeX}x{MaxSizeY}.");
        }

        Point = point1;
    }

    protected void ValidateSize(int sizeX, int sizeY)
    {
        if (sizeX < MinSizeX || sizeY < MinSizeY)
        {
            throw new ArgumentException($"Map dimensions must be at least {MinSizeX}x{MinSizeY}.");
        }
    }

    public override void Add(IMappable item, Point point)
    {
        if (!mapItems.ContainsKey(point))
        {
            mapItems[point] = new List<IMappable>();
        }
        mapItems[point].Add(item);
    }


    public override void Remove(IMappable item, Point point)
    {
        if (mapItems.TryGetValue(point, out List<IMappable> list))
        {
            list.Remove(item);
            if (list.Count == 0)
            {
                mapItems.Remove(point);
            }
        }
    }


    public override Point Move(IMappable item, Point from, Direction direction)
    {
        Point newPosition = Next(from, direction);

        if (!Exist(newPosition))
        {
            Console.WriteLine($"[DEBUG] Move aborted: {item.Name} attempted to move outside the map to {newPosition}.");
            return from; 
        }

        Remove(item, from);
        Add(item, newPosition);

        return newPosition;
    }


    public override IEnumerable<IMappable> At(Point point)
    {
        if (!mapItems.ContainsKey(point))
            return Enumerable.Empty<IMappable>();

        return mapItems[point];
    }


}
