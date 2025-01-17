using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Simulator;
using Simulator.Maps;

namespace SimWeb.Pages
{
    public class SimulationModel : PageModel
    {
        public int Turn { get; set; }
        public int MaxTurn { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public string MoveDescription { get; set; }
        public Dictionary<Point, List<string>> Symbols { get; set; } = new();

        public void OnGet()
        {
            int turn;
            if (!int.TryParse(Request.Query["turn"], out turn))
            {
                turn = 0;
            }

            SmallTorusMap map = new(8, 6);

            List<IMappable> creatures = new()
            {
                new Elf("Elandor"),
                new Orc("Gorbag"),
                new Animals("Rabbits"),
                new Birds("Eagles", true),
                new Birds("Ostriches", false),
            };

            List<Point> points = new()
            {
                new(2, 2),
                new(3, 1),
                new(4, 5),
                new(0, 1),
                new(1, 0),
            };

            string moves = "ruldlurdruldlur";

            Simulation simulation = new(map, creatures, points, moves);
            SimulationHistory history = new(simulation);

            if (turn > history.TurnLogs.Count - 1)
            {
                turn = history.TurnLogs.Count - 1;
            }
            else if (turn < 0)
            {
                turn = 0;
            }

            Turn = turn;
            MaxTurn = history.TurnLogs.Count - 1;
            X = map.SizeX;
            Y = map.SizeY;

            SimulationTurnLog currentTurn = history.TurnLogs[turn];
            MoveDescription = Turn == 0 ? currentTurn.Move : $"{currentTurn.Mappable} => {currentTurn.Move}";

            // Grupowanie ikon na polach mapy
            foreach (var entry in currentTurn.Symbols)
            {
                Point position = entry.Key;
                char symbol = entry.Value;
                string iconPath = symbol switch
                {
                    'E' => "/images/icons/elf.png",         // Elf
                    'O' => "/images/icons/orc.png",         // Orc
                    'A' => "/images/icons/animal.png",      // Domyœlny symbol zwierz¹t
                    'B' => "/images/icons/bird_fly.png",    // Ptaki lataj¹ce
                    'b' => "/images/icons/bird_walk.png",   // Ptaki nielataj¹ce
                    _ => "/images/icons/default.png"
                };

                if (!Symbols.ContainsKey(position))
                {
                    Symbols[position] = new List<string>();
                }
                Symbols[position].Add(iconPath);
            }
        }
    }
}
