using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            return item as Creature; // Zwróć null, jeśli nie jest typu Creature
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
    /// <exception cref="ArgumentException">Thrown if the items list is empty or 
    /// the number of items differs from the number of positions.</exception>
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
                position = new Point(0, 0); // Przypisz pozycję domyślną
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
    /// <exception cref="InvalidOperationException">Thrown if simulation is finished.</exception>
    public void Turn()
    {
        if (Finished)
            throw new InvalidOperationException("Simulation is already finished.");

        var currentItem = Items[_currentTurn % Items.Count];

        if (currentItem == null)
        {
            Console.WriteLine("[ERROR] Current item is null.");
            throw new NullReferenceException("Current item is null.");
        }

        if (currentItem.Map == null)
        {
            Console.WriteLine($"[ERROR] {currentItem.Name} has no assigned map.");
            throw new NullReferenceException($"Item {currentItem.Name} has no assigned map.");
        }

        if (currentItem.Position == null)
        {
            Console.WriteLine($"[ERROR] {currentItem.Name} has no assigned position.");
            throw new NullReferenceException($"Item {currentItem.Name} has no assigned position.");
        }

        Console.WriteLine($"[DEBUG] Turn {_currentTurn + 1}: Moving {currentItem.Name} ({currentItem.Symbol}).");

        // Wykonanie ruchu
        currentItem.Move(_parsedMoves[_currentTurn % _parsedMoves.Count]);

        Console.WriteLine($"[DEBUG] {currentItem.Name} moved to {currentItem.Position}.");

        _currentTurn++;

        if (_currentTurn >= _parsedMoves.Count * Items.Count)
        {
            Finished = true;
        }
    }
}
