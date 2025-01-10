using Simulator;
using Simulator.Maps;
namespace Runner;

internal class Program
{
    static void Main(string[] args)
    {
        Orc orc1 = new Orc("Dupka", new Point (1, 1));

        Console.WriteLine(orc1.Go(Direction.Up));

        orc1.Go(Direction.Right);
        orc1.Go(Direction.Right);

        Console.WriteLine(orc1.Greeting());
        Console.WriteLine(orc1.WhereCreature());

        SmallSquareMap map1 = new SmallSquareMap(10, 10);
        Console.WriteLine(map1.SizeX);
    }
}
