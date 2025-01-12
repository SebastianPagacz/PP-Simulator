using Simulator.Maps;
using Simulator;

public class Animals : IMappable
{
    public string Name { get; init; }
    public Point? Position { get; set; }
    public Map? Map { get; set; }

    public virtual char Symbol => 'A'; // Domyślny symbol dla zwierząt

    public Animals(string name = "Animal")
    {
        Name = name;
    }

    public virtual void Move(Direction direction)
    {
        if (Map == null || Position == null)
            throw new InvalidOperationException($"{Name} has no assigned map or position.");

        Position = Map.Move(this, Position.Value, direction);
    }
}