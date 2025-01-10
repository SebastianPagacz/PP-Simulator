namespace Simulator.Maps;

public interface IMappable
{
    string Name { get; }
    Point? Position { get; set; }
    Map? Map { get; set; }

    void Move(Direction direction);
}
