namespace Simulator;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Starting Simulator!\n");
        Creature creature = new Creature("shrek", -10);
        creature.SayHi();
        Console.ReadKey();
    }
}
