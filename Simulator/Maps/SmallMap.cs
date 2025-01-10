namespace Simulator.Maps;

public class SmallMap : Map
{
    private Dictionary<Point, List<Creature>> creatures = new Dictionary<Point, List<Creature>>();
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

    public override void Add(Creature creature, Point point)
    {
        if (!creatures.ContainsKey(point))
        {
            creatures[point] = new List<Creature>();
        }
        creatures[point].Add(creature);
    }


    public override void Remove(Creature creature, Point point)
    {
        if (creatures.TryGetValue(point, out List<Creature> list))
        {
            list.Remove(creature);
            if (list.Count == 0)
            {
                creatures.Remove(point);
            }
        }
    }


    public override Point Move(Creature creature, Point from, Direction direction)
    {
        Point newPosition = Next(from, direction);

        if (Exist(newPosition))
        {
            Remove(creature, from);

            Add(creature, newPosition);

            return newPosition;
        }

        return from;
    }


    public override IEnumerable<Creature> At(Point point)
    {
        return creatures.TryGetValue(point, out List<Creature> list) ? list : Enumerable.Empty<Creature>();
    }
}
