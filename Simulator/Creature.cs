namespace Simulator;

internal class Creature
{
    // Member variables
    private string _name = "Unknown";
    private int _level = 1;

    // Name Lenght variables
    int shortNameLen = 3;
    int longNameLen = 25;

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

                if(_name.Length > longNameLen)
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
    public Creature(string name, int level = 1)
    {
        Name = name;
        Level = level;
    }

    public Creature()
    {

    }
    
    // Creature methods
    public void SayHi() 
    {
        Console.WriteLine($"Hi, I'm {Name}, my level is {Level}");
    }

    public void Upgrade()
    {
        if(_level < 10)
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
        foreach(var directions in direction)
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
}
