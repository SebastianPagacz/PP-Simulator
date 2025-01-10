using Simulator;
using Simulator.Maps;
using System;
using System.Collections.Generic;
using System.Linq;

public class Simulation
{
    private readonly Map _map;
    private readonly List<Creature> _creatures;
    private readonly List<Point> _positions;
    private readonly List<Direction> _parsedMoves;
    private int _currentCreatureIndex = 0;
    private int _currentMoveIndex = 0;

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
    public bool Finished = false;

    /// <summary>
    /// Creature which will be moving current turn.
    /// </summary>
    public Creature CurrentCreature => _creatures[_currentCreatureIndex];

    /// <summary>
    /// Lowercase name of direction which will be used in current turn.
    /// </summary>
    public string CurrentMoveName
    {
        get
        {
            if (_currentMoveIndex < _parsedMoves.Count)
                return _parsedMoves[_currentMoveIndex].ToString().ToLower();
            return "";
        }
    }

    /// <summary>
    /// Simulation constructor.
    /// Throw errors:
    /// if creatures' list is empty,
    /// if number of creatures differs from 
    /// number of starting positions.
    /// </summary>
    public Simulation(Map map, List<Creature> creatures, List<Point> positions, string moves)
    {
        if (creatures == null || creatures.Count == 0)
            throw new ArgumentException("The list of creatures cannot be empty.");

        if (creatures.Count != positions.Count)
            throw new ArgumentException("The number of creatures must match the number of starting positions.");

        _map = map;
        _creatures = creatures;
        _positions = positions;
        Moves = moves;
        _parsedMoves = DirectionParser.Parse(moves);

        // Initialize creatures on the map
        for (int i = 0; i < creatures.Count; i++)
        {
            creatures[i].Position = positions[i];
            map.Add(creatures[i], positions[i]);
        }
    }

    /// <summary>
    /// Makes one move of current creature in current direction.
    /// Throw error if simulation is finished.
    /// </summary>
    public void Turn()
    {
        if (Finished)
            throw new InvalidOperationException("Simulation is already finished.");

        if (_currentMoveIndex < _parsedMoves.Count)
        {
            var direction = _parsedMoves[_currentMoveIndex];
            CurrentCreature.Go(direction);
        }

        _currentMoveIndex++;
        _currentCreatureIndex = (_currentCreatureIndex + 1) % _creatures.Count;

        if (_currentMoveIndex >= _parsedMoves.Count * _creatures.Count)
        {
            Finished = true;
        }
    }
}