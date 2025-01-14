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

        // Tworzenie mapy
        var map = new SmallSquareMap(10, 10);

        // Tworzenie stworzeń
        var elf = new Elf("Legolas", map, level: 3, position: new Point(0, 0), agility: 5);
        var orc = new Orc("Azog", map, level: 4, position: new Point(1, 1), rage: 7);

        // Tworzenie listy stworzeń i ich początkowych pozycji
        var items = new List<IMappable> { elf, orc };
        var positions = new List<Point> { new Point(0, 0), new Point(1, 1) };

        // Definiowanie ruchów (np. URDL)
        string moves = "URDL";

        // Tworzenie symulacji
        var simulation = new Simulation(map, items, positions, moves);

        // Tworzenie historii symulacji
        var history = new SimulationHistory(simulation);

        // Wypisanie logów
        Console.WriteLine("Historia symulacji:");
        foreach (var log in history.TurnLogs)
        {
            Console.WriteLine($"Mappable: {log.Mappable}, Move: {log.Move}");
            Console.WriteLine("Stan mapy:");
            foreach (var symbol in log.Symbols)
            {
                Console.WriteLine($"  Pozycja {symbol.Key}: {symbol.Value}");
            }
            Console.WriteLine();
        }
    }
}