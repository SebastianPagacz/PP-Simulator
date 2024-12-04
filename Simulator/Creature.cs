namespace Simulator;

public class Creature
{
//Wlasciwosci automatyczne
    public string Name { get; set; }
    public int Level { get; set; }

//Konstruktor
    public Creature(string name, int level =  1)
    {
        Name = name;
        Level = level;
    }
    public Creature()
    {
       //nic nie robi 
    }

    public void SayHi()
    {
        Console.WriteLine($"Hi, I'm {Name}, my level is {Level}.");
    }
}