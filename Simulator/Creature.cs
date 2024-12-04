namespace Simulator;

public class Creature
{
//Wlasciwosci automatyczne
    public string Name
    {
        get; set;
    }

    private int level;
    public int Level
    {
        get { return level; }
        //Przez init Level jest niezmienne
        init 
        { 
            if (value < 1)
            {
                level = 1;
            }
            else if (value > 10)
            { 
                level = 10; 
            }
            else
            {
                level = value;
            }
        }
    }

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