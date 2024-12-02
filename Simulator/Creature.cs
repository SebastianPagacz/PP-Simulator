namespace Simulator;

public class Creature
{
    public string name;
    public int level;

    public Creature(string name, int level)
    {
        Name = name;
        Level = level;
    }
    public string Name
    {
        get { return name; }
        set { name = value; }
    }
    public int Level
    {
        get { return level; }
        set { level = level > 0 ? level : 1; }
    }
}