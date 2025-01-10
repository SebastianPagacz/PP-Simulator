using Simulator.Maps;
using System;

namespace Simulator;

public abstract class Creature
{
    // Member variables
    private string _name = "Unknown";
    private int _level = 1;
    private Point _position;
    private Map? _map;

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

    public Map? Map
    {
        get => _map;
        set
        {
            if (_map == null)
            {
                _map = value;
            }
        }
    }
    public abstract string Info
    {
        get;
    }
    public Point? Position { get; set; }
    // Constructors
    protected Creature(string name, Point? position, Map? map, int level = 1)
    {
        Name = name;
        Level = level;
        Position = position;
        Map = map;
    }

    protected Creature()
    {

    }

    // Creature methods
    public abstract string Greeting();

    public void Upgrade()
    {
        if (_level < 10)
        {
            _level++;
        }
    }

    // Directions handling
    public Point Go(Direction direction)
    {
        if (Map != null && Position != null)
        {
            Point newPosition = Map.Move(this, (Point)Position, direction);
            if (!newPosition.Equals((Point)Position)) 
            {
                Position = newPosition;
            }
        }
        return (Point)Position; 
    }

    public override string ToString()
    {
        return $"{GetType().Name.ToUpper()}: {Info}";
    }

    // Position handling
    public string WhereCreature()
    {
        return $"{Name} is on position {Position}";
    }


}