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
        public List<Creature> Creatures { get; }

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
        public Creature CurrentCreature => Creatures[_currentTurn % Creatures.Count];

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
    /// <param name="creatures">List of creatures.</param>
    /// <param name="positions">Starting positions of creatures.</param>
    /// <param name="moves">String of moves.</param>
    /// <exception cref="ArgumentException">Thrown if the creatures list is empty or 
    /// the number of creatures differs from the number of positions.</exception>
    public Simulation(Map map, List<Creature> creatures, List<Point> positions, string moves)
    {
        if (creatures == null || creatures.Count == 0)
            throw new ArgumentException("Creatures list cannot be empty.");

        if (creatures.Count != positions.Count)
            throw new ArgumentException("Number of creatures must match the number of positions.");

        Map = map;
        Creatures = creatures;
        Positions = positions;
        Moves = moves;

        _parsedMoves = DirectionParser.Parse(moves);

        for (int i = 0; i < creatures.Count; i++)
        {
            creatures[i].Map = map;          
            creatures[i].Position = positions[i]; 
            map.Add(creatures[i], positions[i]);  
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

        var currentMove = _parsedMoves[_currentTurn % _parsedMoves.Count];
        var currentCreature = CurrentCreature;

        Console.WriteLine($"Turn {_currentTurn + 1}: Moving {currentCreature.Name}");
        Console.WriteLine($"Current position: {currentCreature.Position}");

        Point newPosition = currentCreature.Go(currentMove);

        Console.WriteLine($"New position: {newPosition}");

        _currentTurn++;

        if (_currentTurn >= _parsedMoves.Count * Creatures.Count)
        {
            Finished = true;
        }
    }
}

