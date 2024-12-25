using Simulator.Maps;

namespace Simulator;

internal class Program
{
    static void Main(string[] args)
    {
        //Lab5a();
        Lab5b();
    }
    public static void Lab5a()
    {
        try
        {
            // Test 1: Rectangle #1 (2, 3) : (5, 6)
            var rect1 = new Rectangle(2, 3, 5, 6);
            Console.WriteLine($"Rectangle #1: {rect1}");

            // Test 2: Rectangle #2 (5, 6) : (2, 3)
            var rect2 = new Rectangle(5, 6, 2, 3);
            Console.WriteLine($"Rectnelge #2: {rect2}");

            // Test 3: Rectangle #3 (5, 5) : (5, 5)
            try
            {
                var rect3 = new Rectangle(5, 5, 5, 5);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            // Test 4: Rectengle #4 using Points
            var p1 = new Point(2, 3);
            var p2 = new Point(5, 6);
            var rect4 = new Rectangle(p1, p2);
            Console.WriteLine($"Rectengle #4: {rect4}");

            // Test 5: inside of (2, 3) : (5, 6)
            var point1 = new Point(3, 4);
            Console.WriteLine($"Does rectengle #1 consist {point1}? {rect1.Contains(point1)}");

            // Test 6: on edge of  (2, 3) : (5, 6)
            var point2 = new Point(5, 6);
            Console.WriteLine($"Does rectengle #1 consist {point2}? {rect1.Contains(point2)}");

            // Test 7: oustisde of (2, 3) : (5, 6)
            var point3 = new Point(7, 7);
            Console.WriteLine($"Does rectengle #1 consist {point3}? {rect1.Contains(point3)}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }


    public static void Lab5b()
    {
        // Test class construction and size validation
        Console.WriteLine("Test construction and size validation:");

        try
        {
            var map = new SmallSquareMap(4);  // size out of range
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine(ex.Message);  // Check if the exception is thrown
        }

        try
        {
            var map = new SmallSquareMap(21);  // size out of range
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine(ex.Message);  // Check if the exception is thrown
        }

        var validMap = new SmallSquareMap(10);  // valid size
        Console.WriteLine($"Map created with size: {validMap.Size}");  // Check if map is created correctly

        // Test Exist method
        Console.WriteLine("\nTesting the Exist method:");

        var pointInside = new Point(5, 5);
        bool isInside = validMap.Exist(pointInside);
        Console.WriteLine($"Point {pointInside} inside map: {isInside}");

        var pointOutside = new Point(15, 15);
        bool isOutside = validMap.Exist(pointOutside);
        Console.WriteLine($"Point {pointOutside} inside map: {isOutside}");

        // Test Next method
        Console.WriteLine("\nTesting the Next method:");

        var startPoint = new Point(5, 5);
        var direction = Direction.Right;  // Example direction
        var nextPoint = validMap.Next(startPoint, direction);
        Console.WriteLine($"Next point from {startPoint} towards {direction}: {nextPoint}");

        var outOfBoundsPoint = new Point(10, 10);
        var nextOutOfBounds = validMap.Next(outOfBoundsPoint, Direction.Up);  // Point outside the map
        Console.WriteLine($"Next point from {outOfBoundsPoint} towards Up (out of bounds): {nextOutOfBounds}");

        // Test NextDiagonal method
        Console.WriteLine("\nTesting the NextDiagonal method:");

        var diagonalNextPoint = validMap.NextDiagonal(startPoint, Direction.UpRight);
        Console.WriteLine($"Next diagonal point from {startPoint} towards UpRight: {diagonalNextPoint}");

        var diagonalOutOfBounds = validMap.NextDiagonal(outOfBoundsPoint, Direction.UpRight);
        Console.WriteLine($"Next diagonal point from {outOfBoundsPoint} towards UpRight (out of bounds): {diagonalOutOfBounds}");
    }
}

