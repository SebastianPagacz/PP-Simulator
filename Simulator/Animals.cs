namespace Simulator;

internal class Animals
{
    int shortNameLen = 3;
    int longNameLen = 15;
    private string _description = "Unknown";
    public virtual string Info 
    { 
        get { return $"{Description} <{Size}>"; }
    }
    public required string Description 
    { 
        get => _description;
        init
        {
            _description = Validator.Shortener(value, shortNameLen, longNameLen, '#');
            _description = char.ToUpper(_description[0]) + _description.Substring(1);
        }
    } 
    
    public uint Size { get; set; } = 3;

    public override string ToString()
    {
        return $"{GetType().Name.ToUpper()}: {Info}";
    }

    public class Birds : Animals
    {
        public bool CanFly { get; init; } = true;
        public override string Info => $"{Description} (fly{(CanFly ? "+" : "-")}) <{Size}>";

    }
}
