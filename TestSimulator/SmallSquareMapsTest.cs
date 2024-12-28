using Simulator.Maps;
using Simulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSimulator
{
    public class SmallSquareMapsTest
    {
        public class SmallSquareMapTests
        {
            [Fact]
            public void Constructor_ShouldSetSize_WhenSizeIsValid()
            {
                // Arrange & Act
                var map = new SmallSquareMap(10);

                // Assert
                Assert.Equal(10, map.Size);
            }

            [Theory]
            [InlineData(4)]  // Too small
            [InlineData(21)] // Too large
            public void Constructor_ShouldThrowException_WhenSizeIsOutOfRange(int size)
            {
                // Act & Assert
                var exception = Assert.Throws<ArgumentOutOfRangeException>(() => new SmallSquareMap(size));
                Assert.Contains("Size is out of respective range (5 to 20)", exception.Message);
            }

            [Theory]
            [InlineData(0, 0, true)]   // Within bounds
            [InlineData(10, 10, true)] // Edge case
            [InlineData(15, 15, true)] // Within bounds
            [InlineData(-1, 5, false)] // Negative X
            [InlineData(5, -1, false)] // Negative Y
            [InlineData(21, 10, false)] // X out of bounds
            [InlineData(10, 21, false)] // Y out of bounds
            public void Exist_ShouldReturnCorrectResult(int x, int y, bool expected)
            {
                // Arrange
                var map = new SmallSquareMap(20);
                var point = new Point(x, y);

                // Act
                var result = map.Exist(point);

                // Assert
                Assert.Equal(expected, result);
            }

            [Theory]
            [InlineData(10, 10, Direction.Up, 10, 11)]    // Valid move up
            [InlineData(0, 0, Direction.Left, 0, 0)]     // Invalid move left (boundary)
            [InlineData(5, 5, Direction.Right, 6, 5)]    // Valid move right
            [InlineData(20, 20, Direction.Down, 20, 19)] // Invalid move down (boundary)
            public void Next_ShouldReturnCorrectPoint(int startX, int startY, Direction direction, int expectedX, int expectedY)
            {
                // Arrange
                var map = new SmallSquareMap(20);
                var startPoint = new Point(startX, startY);

                // Act
                var result = map.Next(startPoint, direction);

                // Assert
                Assert.Equal(new Point(expectedX, expectedY), result);
            }

            [Theory]
            [InlineData(10, 10, Direction.Up, 11, 11)]     // Valid diagonal move
            [InlineData(0, 0, Direction.Down, 0, 0)]    // Invalid diagonal move (boundary)
            [InlineData(5, 5, Direction.Left, 4, 6)]     // Valid diagonal move
            [InlineData(20, 20, Direction.Up, 20, 20)]  // Invalid diagonal move (boundary)
            public void NextDiagonal_ShouldReturnCorrectPoint(int startX, int startY, Direction direction, int expectedX, int expectedY)
            {
                // Arrange
                var map = new SmallSquareMap(20);
                var startPoint = new Point(startX, startY);

                // Act
                var result = map.NextDiagonal(startPoint, direction);

                // Assert
                Assert.Equal(new Point(expectedX, expectedY), result);
            }
        }
    }
}
