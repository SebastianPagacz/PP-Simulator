using System.Xml.Linq;

namespace Simulator;

internal class Animals
{
    int shortNameLen = 3;
    int longNameLen = 15;
    private string _description = "Unknown";
    public string Info 
    { 
        get { return $"{Description} <{Size}>"; }
    }
    public required string Description 
    { 
        get => _description;
        init
        {
        _description = value;
        while (_description[0] == ' ' || _description[^1] == ' ' || _description.Length < shortNameLen || _description.Length > longNameLen)
        {
            // Trimming whitespaces
            _description = _description.Trim();

            // Adjusting length
            if (_description.Length < 3)
            {
                while (_description.Length < shortNameLen)
                {
                    _description += "#";
                }
            }

            if (_description.Length > longNameLen)
            {
                int toCut = _description.Length - longNameLen;
                _description = _description[..^toCut];
            }
        }

            // First liter capital
            _description = char.ToUpper(_description[0]) + _description.Substring(1);
        }
    } 
    
    public uint Size { get; set; } = 3;
}
