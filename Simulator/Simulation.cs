// Simulation.cs
using System;
using System.Collections.Generic;
using Simulator.Maps;

namespace Simulator;

public class Simulation
{
    /// <summary>
    /// Simulation's map.
    /// </summary>
    public Map Map { get; }

    /// <summary>
    /// Creatures moving on the map.
    /// </summary>
    public List<IMappable> Items { get; }

    /// <summary>
    /// Starting positions of creatures.
    /// </summary>
    public List<Point> Positions { get; }

    /// <summary>
    /// Cyclic list of creatures moves. 
    /// Bad moves are ignored - use DirectionParser.
    /// First move is for first creature, second for second and so on.
    /// When all creatures make moves, 
    /// next move is again for first creature and so on.
    /// </summary>
    public string Moves { get; }

    /// <summary>
    /// Has all moves been done?
    /// </summary>
    public bool Finished { get; private set; } = false;

    /// <summary>
    /// Creature which will be moving current turn.
    /// </summary>
    public Creature? CurrentCreature
    {
        get
        {
            var item = Items[_currentTurn % Items.Count];
            return item as Creature;
        }
    }

    /// <summary>
    /// Lowercase name of direction which will be used in current turn.
    /// </summary>
    public string CurrentMoveName => _parsedMoves[_currentTurn % _parsedMoves.Count].ToString().ToLower();

    private readonly List<Direction> _parsedMoves;
    private int _currentTurn = 0;

    /// <summary>
    /// Simulation constructor.
    /// </summary>
    /// <param name="map">Map of the simulation.</param>
    /// <param name="items">List of mappable items.</param>
    /// <param name="positions">Starting positions of items.</param>
    /// <param name="moves">String of moves.</param>
    public Simulation(Map map, List<IMappable> items, List<Point> positions, string moves)
    {
        if (items.Count != positions.Count)
            throw new ArgumentException("Number of items must match number of positions.");

        Map = map;
        Items = items;
        Positions = positions;
        Moves = moves;
        _parsedMoves = DirectionParser.Parse(moves);

        for (int i = 0; i < items.Count; i++)
        {
            Point position = positions[i];
            if (!map.Exist(position))
            {
                Console.WriteLine($"[WARNING] Initial position {position} for {items[i].Name} is outside the map boundaries. Assigning to position (0,0).");
                position = new Point(0, 0);
            }

            items[i].Map = map;
            items[i].Position = position;
            map.Add(items[i], position);

            Console.WriteLine($"[DEBUG] Added {items[i].Name} ({items[i].Symbol}) at position {position}.");
        }
    }

    /// <summary>
    /// Makes one move of current creature in current direction.
    /// </summary>
    public void Turn()
    {
        if (Finished)
            throw new InvalidOperationException("Simulation is already finished.");

        var currentItem = Items[_currentTurn % Items.Count];

        if (currentItem is Creature creature)
        {
            if (creature.Map == null || creature.Position == null)
                throw new InvalidOperationException($"Creature {creature.Name} is not properly initialized.");

            var position = creature.Position.Value;
            var occupants = Map.At(position).ToList(); // Create a copy to avoid modification issues

            foreach (var occupant in occupants)
            {
                if (creature is Orc orc && occupant is Animals animal)
                {
                    orc.InteractWithAnimal(animal);
                    Map.Remove(animal, position); // Remove animal after interaction
                    Items.Remove(animal);
                }
                else if (creature is Elf elf && occupant is Birds bird)
                {
                    elf.InteractWithBird(bird);
                    Map.Remove(bird, position); // Remove bird after interaction
                    Items.Remove(bird);
                }
                else if (creature is Orc orc2 && occupant is Elf elf2)
                {
                    HandleBattle(orc2, elf2);
                }
            }

            creature.Move(_parsedMoves[_currentTurn % _parsedMoves.Count]);
        }
        else if (currentItem is Animals || currentItem is Birds)
        {
            currentItem.Move(_parsedMoves[_currentTurn % _parsedMoves.Count]);
        }

        _currentTurn++;
        if (_currentTurn >= _parsedMoves.Count * Items.Count)
        {
            Finished = true;
        }
    }

    /// <summary>
    /// Handles a battle between an orc and an elf.
    /// </summary>
    /// <param name="orc">The orc involved in the battle.</param>
    /// <param name="elf">The elf involved in the battle.</param>
    private void HandleBattle(Orc orc, Elf elf)
    {
        int orcStats = (int)(orc.Level * 1.5 + orc.Rage) / 2;
        int elfStats = (int)(elf.Level * 1.5 + elf.Agility) / 2;

        if (orcStats > elfStats)
        {
            // Orc wins, Elf is removed
            Console.WriteLine($"Battle: {orc.Name} defeated {elf.Name}");
            Map.Remove(elf, elf.Position.Value);
            Items.Remove(elf);
        }
        else if (elfStats > orcStats)
        {
            // Elf wins, Orc is removed
            Console.WriteLine($"Battle: {elf.Name} defeated {orc.Name}");
            Map.Remove(orc, orc.Position.Value);
            Items.Remove(orc);
        }
        else
        {
            // Tie - no one is removed
            Console.WriteLine($"Battle: {orc.Name} and {elf.Name} tied.");
        }
    }
}
