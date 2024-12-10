namespace Simulator;

internal class Creature
{
    // Properties
    public string Name { get; set; }
    public int Level { get; set; }
    public string Ifno 
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
    public void SayHi() 
    {
        Console.WriteLine($"Hi, I'm {Name}, my level is {Level}");
    }
}
