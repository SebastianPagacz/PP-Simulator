﻿namespace Simulator;

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
        init
        {
            if (value < 0)
            {
                value = 0;
                _rage = value;
            }
            if (value > 10)
            {
                value = 10;
                _rage = value;
            }
        }
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

    public Orc() : base()
    {
        Rage = _rage;
    }

    public Orc(string name, Point position, int level = 1, int rage = 0) : base(name, position, level)
    {
        Rage = rage;
        _rage = rage;
    }

    public override string Greeting() => $"Hi, I'm {Name}, my level is {Level}, my rage is {Rage}.";
}
