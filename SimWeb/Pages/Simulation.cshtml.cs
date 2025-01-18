using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Simulator;
using Simulator.Maps;
using System;
using System.Collections.Generic;

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

        private static readonly List<string> OrcNames = new() { "Gorath", "Thargok", "Mogrim", "Drogath" };
        private static readonly List<string> ElfNames = new() { "Elandor", "Sylwen", "Therion", "Lirael" };
        private static readonly List<string> AnimalTypes = new() { "Rabbit", "Pig", "Fox", "Wolf" };
        private static readonly List<string> BirdTypes = new() { "Eagle", "Sparrow", "Hawk", "Raven" };

        private static readonly Random Random = new();

        private static string PersistentOrcName;
        private static string PersistentElfName;
        private static string PersistentAnimalType;
        private static string PersistentBirdType;

        private static readonly List<string> PossibleMoves = new()
        {
            "drlrruullddrruuullddll",
            "rullddrruulldrrddluurr",
            "llrruuddrruullrrddluud",
            "ddrruullrrddlluurrddll"
        };

        private static string PersistentMoves; // Deklaracja pola

        private static void InitializeRandomMoves() // Definicja metody
        {
            Random random = new();
            PersistentMoves = PossibleMoves[random.Next(PossibleMoves.Count)];
        }

        private static void InitializeRandomNames()
        {
            PersistentOrcName = OrcNames[Random.Next(OrcNames.Count)];
            PersistentElfName = ElfNames[Random.Next(ElfNames.Count)];
            PersistentAnimalType = AnimalTypes[Random.Next(AnimalTypes.Count)];
            PersistentBirdType = BirdTypes[Random.Next(BirdTypes.Count)];
        }

        public void OnGet()
        {
            int turn;
            if (!int.TryParse(Request.Query["turn"], out turn))
            {
                turn = 0;
            }

            if (PersistentOrcName == null || PersistentElfName == null || PersistentAnimalType == null || PersistentBirdType == null)
            {
                InitializeRandomNames();
            }

            // Inicjalizacja losowych ruchów, jeśli jeszcze nie zostały ustawione
            if (PersistentMoves == null)
            {
                InitializeRandomMoves();
            }

            SmallTorusMap map = new(7, 7); // Create a SmallTorusMap with size 7x7

            List<IMappable> creatures = new()
            {
                new Orc(PersistentOrcName, null, 8),       // Orc
                new Animals(PersistentAnimalType),   // Animal
                new Elf(PersistentElfName),      // Elf
                new Birds(PersistentBirdType, true) // Bird
            };

            List<Point> points = new()
            {
                new Point(2, 1), // Orc's starting position
                new Point(1, 2), // Animal's position
                new Point(5, 5), // Elf's starting position
                new Point(6, 5)  // Bird's position
            };

            Simulation simulation = new(map, creatures, points, PersistentMoves);
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