namespace Simulator;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Starting Simulator!\n");
        Creature elf = new Creature("");
        elf.SayHi();
        Console.WriteLine(elf.Ifno);
    }
}
