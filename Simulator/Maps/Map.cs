namespace Simulator.Maps;
/// <summary>
/// Map of points.
/// </summary>
public abstract class Map
{

    public int SizeX { get; }
    public int SizeY { get; }

    public Map(int x, int y)
    {   
        if (x < 5|| y < 5) { throw new ArgumentOutOfRangeException("X and Y should be greater than 5"); }
        SizeX = x;
        SizeY = y;
    }

    /// <summary>
    /// Check if given point belongs to the map.
    /// </summary>
    /// <param name="p">Point to check.</param>
    /// <returns></returns>
    public abstract bool Exist(Point p);

    /// <summary>
    /// Next position to the point in a given direction.
    /// </summary>
    /// <param name="p">Starting point.</param>
    /// <param name="d">Direction.</param>
    /// <returns>Next point.</returns>
    public abstract Point Next(Point p, Direction d);

    /// <summary>
    /// Next diagonal position to the point in a given direction 
    /// rotated 45 degrees clockwise.
    /// </summary>
    /// <param name="p">Starting point.</param>
    /// <param name="d">Direction.</param>
    /// <returns>Next point.</returns>
    public abstract Point NextDiagonal(Point p, Direction d);

    /// <summary>
    /// Adds creature to the map 
    /// </summary>
    /// <param name="c">Creature.</param>
    /// <param name="p">Point.</param>
    /// <returns></returns>
    public abstract void Add(IMappable item, Point point);

    /// <summary>
    /// Moves creature on the map
    /// </summary>
    /// <param name="c">Creature.</param>
    /// <param name="p">Point.</param>
    /// <param name="d">Direction.</param>
    /// <returns>Next point.</returns>
    public abstract Point Move(IMappable item, Point from, Direction direction);

    /// <summary>
    /// removes creature from the map
    /// </summary>
    /// <param name="c">Creature.</param>
    /// <param name="p">Point.</param>
    /// <returns></returns>
    public abstract void Remove(IMappable item, Point point);

    public abstract IEnumerable<IMappable> At(Point point);
}
