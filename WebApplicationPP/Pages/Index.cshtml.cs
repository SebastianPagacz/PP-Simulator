using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Simulator;
using Simulator.Maps;

namespace WebApplicationPP.Pages
{
    public class IndexModel : PageModel
    {
        public SimulationHistory? History { get; private set; }
        public List<string>? Logs { get; private set; }

        public void OnGet()
        {
            // Przygotowanie symulacji
            var map = new SmallSquareMap(10);
            var elf = new Elf("Legolas", map, level: 3, position: new Point(0, 0), agility: 5);
            var orc = new Orc("Azog", map, level: 4, position: new Point(1, 1), rage: 7);

            var items = new List<IMappable> { elf, orc };
            var positions = new List<Point> { new Point(0, 0), new Point(1, 1) };
            string moves = "URDL";

            var simulation = new Simulation(map, items, positions, moves);

            // Generowanie historii symulacji
            History = new SimulationHistory(simulation);

            // Przygotowanie logów do wyœwietlenia
            Logs = History.TurnLogs.Select(log =>
                $"Mappable: {log.Mappable}, Move: {log.Move}, Symbols: {string.Join(", ", log.Symbols.Select(kvp => $"[{kvp.Key}: {kvp.Value}]"))}"
            ).ToList();
        }
    }
}
