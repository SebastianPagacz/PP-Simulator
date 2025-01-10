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

        SmallTorusMap map = new SmallTorusMap(5);
        List<IMappable> items = new List<IMappable>
{
    new Elf("Elandor"),
    new Orc("Gorbag"),
    new Animals("Rabbit"),
    new Animals.Birds("Eagle", canFly: true),
    new Animals.Birds("Ostrich", canFly: false)
};

        // Przypisujemy pozycje
        List<Point> positions = new List<Point>
{
    new Point(2, 2),
    new Point(3, 1),
    new Point(1, 4),
    new Point(5, 5),
    new Point(4, 3)
};

        // Tworzymy symulację
        Simulation simulation = new Simulation(map, items, positions, "dddd");

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
