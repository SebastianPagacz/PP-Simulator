namespace Simulator.Maps;

public class SmallMap : Map
{
    private Dictionary<Point, List<Creature>> creatures = new Dictionary<Point, List<Creature>>();
    public int SizeX { get; }
    public int SizeY { get; }
    public override bool Exist(Point p)
    {
        return p.X >= 0 && p.X < SizeX && p.Y >= 0 && p.Y < SizeY;
    }

    public override Point Next(Point p, Direction d)
    {
        throw new NotImplementedException();
    }

    public override Point NextDiagonal(Point p, Direction d)
    {
        throw new NotImplementedException();
    }

    public SmallMap(int sizeX, int sizeY)
    {
        if (sizeX < 5 || sizeX > 20)
        {
            throw new ArgumentOutOfRangeException(nameof(sizeX), "Size of vector X is out of respective range (5 to 20)");
        }
        if (sizeY < 5 || sizeY > 20)
        {
            throw new ArgumentOutOfRangeException(nameof(sizeY), "Size of vector Y is out of respective range (5 to 20)");
        }
        SizeX = sizeX;
        SizeY = sizeY;
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
        Point newPosition = from.Next(direction); // Używamy metody Next z Point
        if (Exist(newPosition))
        {
            Remove(creature, from);
            Add(creature, newPosition);
            return newPosition;
        }
        return from; // Jeśli ruch jest nielegalny, stwór nie zmienia pozycji
    }

    public IEnumerable<Creature> At(Point point)
    {
        return creatures.TryGetValue(point, out List<Creature> list) ? list : Enumerable.Empty<Creature>();
    }

    public IEnumerable<Creature> At(int x, int y)
    {
        return At(new Point(x, y));
    }
}
