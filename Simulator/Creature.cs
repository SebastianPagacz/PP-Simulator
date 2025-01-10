using Simulator.Maps;

namespace Simulator
{
    public abstract class Creature : IMappable
    {
        private string _name = "Unknown";
        private int _level = 1;

        public string Name
        {
            get => _name;
            init
            {
                _name = value.Trim();
                if (_name.Length < 3)
                    _name = _name.PadRight(3, '#');
                if (_name.Length > 25)
                    _name = _name.Substring(0, 25);
                _name = char.ToUpper(_name[0]) + _name.Substring(1);
            }
        }

        public Point? Position { get; set; }

        public Map? Map { get; set; }

        public int Level
        {
            get => _level;
            init
            {
                _level = value < 1 ? 1 : (value > 10 ? 10 : value);
            }
        }

        public abstract int Power { get; }

        public abstract string Info { get; }

        protected Creature(string name = "Unknown", Map? map = null, int level = 1, Point? position = null)
        {
            Name = name;
            Position = position; 
            Map = map;
        }

        public Point Go(Direction direction)
        {
            if (Map == null)
                throw new InvalidOperationException($"Creature {Name} is not assigned to any map.");

            if (Position == null)
                throw new InvalidOperationException($"Creature {Name} does not have a valid position.");

            Point newPosition = Map.Move(this, Position.Value, direction);
            Position = newPosition;
            return Position.Value;
        }

        public void Move(Direction direction)
        {
            Go(direction);
        }
        public void Upgrade()
        {
            if (Level < 10)
                _level++;
        }

        public abstract string Greeting();
        public override string ToString()
        {
            return $"{GetType().Name.ToUpper()}: {Info}";
        }

        public string WhereCreature()
        {
            return $"{Name} is on position {Position}";
        }
    }
}
