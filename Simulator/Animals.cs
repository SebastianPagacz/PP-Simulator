namespace Simulator;

internal class Animals
{
    public string Info 
    { 
        get { return $"{Description} <{Size}>"; }
    }
    public required string Description { get; init; }
    public uint Size { get; set; } = 3;
}
