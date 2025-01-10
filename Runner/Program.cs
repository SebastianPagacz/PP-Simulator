using SimConsole;
using Simulator;
using Simulator.Maps;
using System.Text;
namespace Runner;

public class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        SmallSquareMap map = new SmallSquareMap(new Point(5, 5));
        List<Creature> creatures = new List<Creature>
            {
                new Orc("Gorbag"),
                new Elf("Elandor")
            };
        List<Point> points = new List<Point>
            {
                new Point(2, 2),
                new Point(3, 1)
            };
        string moves = "dlrludl";

        Simulation simulation = new Simulation(map, creatures, points, moves);
        MapVisualizer mapVisualizer = new MapVisualizer(simulation.Map);

        while (!simulation.Finished)
        {
            mapVisualizer.Draw();
            Console.WriteLine("Press any key to proceed to the next turn...");
            Console.ReadKey(true); 
            simulation.Turn();
        }

        mapVisualizer.Draw();
        Console.WriteLine("Simulation finished!");
    }
}
