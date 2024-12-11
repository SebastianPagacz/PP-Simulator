namespace Simulator;

public static class DirectionParser
{
    public static Direction[] Parse(string input)
    {
        // directions list
        var directions = new System.Collections.Generic.List<Direction>();

        foreach (var c in input.ToUpper()) // Capitals
        {
            switch (c)
            {
                case 'U':
                    directions.Add(Direction.Up);
                    break;
                case 'R':
                    directions.Add(Direction.Right);
                    break;
                case 'D':
                    directions.Add(Direction.Down);
                    break;
                case 'L':
                    directions.Add(Direction.Left);
                    break;
            }
        }

        return directions.ToArray();
    }
}
