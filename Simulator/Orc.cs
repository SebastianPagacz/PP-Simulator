using Simulator.Maps;
namespace Simulator;

public class Orc : Creature
{
    private int _rage;
    //Hunt count
    private int rageCount;

    //Power level
    public override int Power => 7 * Level + 3 * _rage;

    public override string Info => $"{Name} [{Level}][{Rage}]";
    public int Rage
    {
        get => _rage;
        init => _rage = Validator.Limiter(value,0, 10);
    }
    public string Hunt()
    {
        rageCount++;
        if (rageCount % 2 == 0)
        {
            _rage = Math.Min(_rage + 1, 10);
        }
        return $"{Name} is hunting.";
    }
    public override char Symbol => 'O';
    public Orc() : base()
    {
        Rage = _rage;
    }

    public Orc(string name, Map? map = null, int level = 1, Point? position = null, int rage = 0) : base(name, map, level, position)
    {
        Rage = rage;
        _rage = rage;
    }

    public override string Greeting() => $"Hi, I'm {Name}, my level is {Level}, my rage is {Rage}.";

    public void InteractWithAnimal(Animals animal)
    {
        if (Map != null && Position.HasValue)
        {
            Map.Remove(animal, Position.Value);
            _rage += 2;
        }
    }
}
