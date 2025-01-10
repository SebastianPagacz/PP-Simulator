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

    public class Birds : Animals
    {
        public bool CanFly { get; init; } = true;
        public override char Symbol => CanFly ? 'B' : 'b';

        public Birds(string name = "Bird", bool canFly = true) : base(name)
        {
            CanFly = canFly;
        }

        public override void Move(Direction direction)
        {
            if (Map == null || Position == null)
                return;

            if (CanFly)
            {
                Point newPosition = Position.Value;

                for (int i = 0; i < 2; i++)
                {
                    newPosition = Map.Next(newPosition, direction);
                    if (!Map.Exist(newPosition))
                        return; 
                }

                Map.Remove(this, Position.Value);
                Map.Add(this, newPosition);
                Position = newPosition;
            }
            else
            {
                Point newPosition = Map.NextDiagonal(Position.Value, direction);
                if (Map.Exist(newPosition))
                {
                    Map.Remove(this, Position.Value);
                    Map.Add(this, newPosition);
                    Position = newPosition;
                }
            }
        }
    }
}