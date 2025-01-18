using Simulator.Maps;
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
        init => _agility = Validator.Limiter(value, 0, 10); 
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
    public override char Symbol => 'E';
    public Elf() : base()
    {
        _agility = Agility;
    }

    public Elf(string name, Map? map = null, int level = 1, Point? position = null, int agility = 1) : base(name, map, level, position)
    {
        Agility = Validator.Limiter(agility, 0, 10);
    }

    public void InteractWithBird(Birds bird)
    {
        if (Map != null && Position.HasValue)
        {
            Map.Remove(bird, Position.Value);
            _agility += 2;
        }
    }
    public override string Greeting() => $"Hi, I'm {Name}, my level is {Level}, my agility is {Agility}.";
}
