using System;

namespace Simulator;

internal abstract class Creature
{
    // Member variables
    private string _name = "Unknown";
    private int _level = 1;

    // Name Lenght variables
    int shortNameLen = 3;
    int longNameLen = 25;


    //Power for elfs and orcs

    public abstract int Power
    {
        get;
    }

    // Properties
    public string Name
    {
        get => _name;

        init
        {
            _name = value;
            while (_name[0] == ' ' || _name[^1] == ' ' || _name.Length < 3 || _name.Length > 25)
            {
                // Trimming whitespaces
                _name = _name.Trim();

                // Adjusting length
                if (_name.Length < 3)
                {
                    while (_name.Length < shortNameLen)
                    {
                        _name += "#";
                    }
                }

                if (_name.Length > longNameLen)
                {
                    int toCut = _name.Length - longNameLen;
                    _name = _name[..^toCut];
                }
            }

            // First liter capital
            _name = char.ToUpper(_name[0]) + _name.Substring(1);
        }
    }
    public int Level
    {
        get => _level;
        init
        {
            {
                if (value < 1)
                {
                    _level = 1;
                }
                else if (value > 10)
                {
                    _level = 10;
                }
                else
                {
                    _level = value;
                }
            }
        }
    }
    public string Info
    {
        get { return $"{Name} [{Level}]"; }
    }

    // Constructors
    protected Creature(string name, int level = 1)
    {
        Name = name;
        Level = level;
    }

    protected Creature()
    {

    }

    // Creature methods
    public abstract void SayHi();

    public void Upgrade()
    {
        if (_level < 10)
        {
            _level++;
        }
    }

    // Directions handling
    public void Go(Direction direction)
    {
        string directionStr = char.ToLower(direction.ToString()[0]) + direction.ToString().Substring(1);

        Console.WriteLine($"{Name} goes {directionStr}");
    }


    public void Go(Direction[] direction)
    {
        foreach (var directions in direction)
        {
            Go(directions);
        }
    }

    public void Go(string direction)
    {
        // Using parser
        Direction[] directions = DirectionParser.Parse(direction);

        foreach (var dir in directions)
        {
            Go(dir);
        }
    }

    // Elf and Orc
    public class Elf : Creature
    {
        private int _agility;
        // Count of sing() usage
        private int singCount;

        //Power level
        public override int Power => 8 * Level + 2 * _agility;
        public int Agility
        {
            get => _agility;
            init
            {
                if (value < 0)
                {
                    value = 0;
                    _agility = value;
                }
                if (value > 10)
                {
                    value = 10;
                    _agility = value;
                }
            }
        }
        public void Sing()
        {
            singCount++; 
            Console.WriteLine($"{Name} is singing.");

            if(singCount % 3 == 0)
            {
                _agility = Math.Min(_agility + 1, 10);
            }
        }

        public Elf() : base() 
        {
            _agility = Agility;
        }

        public Elf(string name, int level = 1, int agility = 1) : base(name, level)
        {
            Agility = agility;
            _agility = agility;
        }

        public override void SayHi() => Console.WriteLine(
            $"Hi, I'm {Name}, my level is {Level}, my agility is {Agility}."
        );
    }


    public class    Orc : Creature
    {
        private int _rage;
        //Hunt count
        private int rageCount;

        //Power level
        public override int Power => 7 * Level + 3 * _rage;
        public int Rage 
        {  
            get => _rage;
            init
            {
                if(value < 0)
                {
                    value = 0;
                    _rage = value;
                }
                if(value > 10)
                {
                    value = 10;
                    _rage = value;
                }
            } 
        }
        public void Hunt()
        {
            rageCount++;
            Console.WriteLine($"{Name} is hunting.");
            if (rageCount % 2 == 0)
            {
                _rage = Math.Min(_rage + 1, 10);
            }
        }

        public Orc() : base() 
        {
            Rage = _rage;
        }

        public Orc(string name, int level = 1, int rage = 0) : base(name, level)
        {
            Rage = rage;
            _rage = rage;
        }

        public override void SayHi() => Console.WriteLine(
            $"Hi, I'm {Name}, my level is {Level}, my rage is {Rage}."
        );
    }

}