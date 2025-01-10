namespace Simulator;

public class Elf : Creature
{
    private int _agility;
    // Count of sing() usage
    private int singCount;

    //Power level
    public override int Power => 8 * Level + 2 * _agility;

    public override string Info => $"{Name} [{Level}][{Agility}]";
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
    public string Sing()
    {
        singCount++;

        if (singCount % 3 == 0)
        {
            _agility = Math.Min(_agility + 1, 10);
        }
        return $"{Name} is singing.";
    }

    public Elf() : base()
    {
        _agility = Agility;
    }

    public Elf(string name, Point position, int level = 1, int agility = 1) : base(name, position, level)
    {
        Agility = Validator.Limiter(agility, 0, 10);
    }

    public override string Greeting() => $"Hi, I'm {Name}, my level is {Level}, my agility is {Agility}.";
}
