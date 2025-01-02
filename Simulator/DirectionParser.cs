namespace Simulator;

public static class DirectionParser
{
    public static List<Direction> Parse(string input)
    {
        // directions list
        List<Direction> Parser = new();

        foreach (var c in input.ToUpper()) // Capitals
        {
            switch (c)
            {
                case 'U':
                    Parser.Add(Direction.Up);
                    break;
                case 'R':
                    Parser.Add(Direction.Right);
                    break;
                case 'D':
                    Parser.Add(Direction.Down);
                    break;
                case 'L':
                    Parser.Add(Direction.Left);
                    break;
            }
        }
        return Parser;
    }
}
