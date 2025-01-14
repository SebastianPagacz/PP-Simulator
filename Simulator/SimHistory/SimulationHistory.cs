using Simulator;

public class SimulationHistory
{
    private Simulation _simulation { get; }
    public int SizeX { get; }
    public int SizeY { get; }
    public List<SimulationTurnLog> TurnLogs { get; } = [];
    // store starting positions at index 0

    public SimulationHistory(Simulation simulation)
    {
        _simulation = simulation ??
            throw new ArgumentNullException(nameof(simulation));
        SizeX = _simulation.Map.SizeX;
        SizeY = _simulation.Map.SizeY;
        CaptureInitialState();
        Run();
    }

    private void CaptureInitialState()
    {
        // Capture initial positions and map state
        var initialSymbols = new Dictionary<Point, char>();
        foreach (var item in _simulation.Items)
        {
            if (item.Position != null)
                initialSymbols[item.Position.Value] = item.Symbol;
        }

        TurnLogs.Add(new SimulationTurnLog
        {
            Mappable = "Initial",
            Move = "Start",
            Symbols = initialSymbols
        });
    }
    private void Run()
    {
        while (!_simulation.Finished)
        {
            var currentCreature = _simulation.CurrentCreature;
            var currentMove = _simulation.CurrentMoveName;

            if (currentCreature != null && currentCreature.Position != null)
            {
                // Record state before the move
                var beforeMoveSymbols = new Dictionary<Point, char>();
                foreach (var item in _simulation.Items)
                {
                    if (item.Position != null)
                        beforeMoveSymbols[item.Position.Value] = item.Symbol;
                }

                TurnLogs.Add(new SimulationTurnLog
                {
                    Mappable = currentCreature.ToString(),
                    Move = currentMove,
                    Symbols = beforeMoveSymbols
                });
            }

            // Perform the move
            _simulation.Turn();
        }
    }
}