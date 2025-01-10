namespace Simulator.Maps;

public class SmallMap : Map
{
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

    public SmallMap(Point point)
    {
        if (point.X < 5 || point.X > 20)
        {
            throw new ArgumentOutOfRangeException(nameof(point.X), "Size of vector X is out of respective range (5 to 20)");
        }
        if (point.Y < 5 || point.Y > 20)
        {
            throw new ArgumentOutOfRangeException(nameof(point.Y), "Size of vector Y is out of respective range (5 to 20)");
        }
        Point = new Point(point.X, point.Y);
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
        if (Exist(newPosition))
        {
            Remove(item, from);
            Add(item, newPosition);
            return newPosition;
        }
        return from;
    }


    public override IEnumerable<IMappable> At(Point point)
    {
        return mapItems.TryGetValue(point, out List<IMappable> list) ? list : Enumerable.Empty<IMappable>();
    }
}
