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

            SmallTorusMap map = new(7, 7); // Create a SmallTorusMap with size 7x7

            List<IMappable> creatures = new()
{
    new Orc("Gorbag",null, 8),       // Orc
    new Animals("Rabbit"),   // Animal
    new Elf("Elandor"),      // Elf
    new Birds("Eagle", true) // Bird
};

            List<Point> points = new()
{
    new Point(2, 1), // Orc's starting position
    new Point(1, 2), // Animal's position
    new Point(5, 5), // Elf's starting position
    new Point(6, 5)  // Bird's position
};

            // Moves designed for all interactions and battle
            string moves = "drlrruullddrruuullddll";

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

                // Sprawdzenie, czy pole jest już zajęte przez inne ikony
                if (Symbols.ContainsKey(position) && Symbols[position].Count > 0)
                {
                    // Wyświetlanie ikony "X" dla zajętego pola
                    Symbols[position] = new List<string> { "/images/icons/multiple.png" };
                }
                else
                {
                    // Normalne dodawanie ikon na wolnych polach
                    string iconPath = symbol switch
                    {
                        'E' => "/images/icons/elf.png",         // Elf
                        'O' => "/images/icons/orc.png",         // Orc
                        'A' => "/images/icons/animal.png",      // Domyślny symbol zwierząt
                        'B' => "/images/icons/bird_fly.png",    // Ptaki latające
                        'b' => "/images/icons/bird_walk.png",   // Ptaki nielatające
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
}
