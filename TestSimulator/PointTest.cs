using Simulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSimulator
{
    public class PointTest
    {
        [Theory]
        [InlineData(0, 0, Direction.Left, -1, 0)]
        [InlineData(0, 0, Direction.Right, 1, 0)]
        [InlineData(0, 0, Direction.Up, 0, 1)]
        [InlineData(0, 0, Direction.Down, 0, -1)]
        public void Next_ShouldReturnCorrectPoint(int x, int y, Direction direction, int expectedX, int expectedY)
        {
            // Arrange
            var point = new Point(x, y);

            // Act
            var nextPoint = point.Next(direction);

            // Assert
            Assert.Equal(expectedX, nextPoint.X);
            Assert.Equal(expectedY, nextPoint.Y);
        }

        [Theory]
        [InlineData(0, 0, Direction.Left, -1, 1)]
        [InlineData(0, 0, Direction.Right, 1, -1)]
        [InlineData(0, 0, Direction.Up, 1, 1)]
        [InlineData(0, 0, Direction.Down, -1, -1)]
        public void NextDiagonal_ShouldReturnCorrectPoint(int x, int y, Direction direction, int expectedX, int expectedY)
        {
            // Arrange
            var point = new Point(x, y);

            // Act
            var nextPoint = point.NextDiagonal(direction);

            // Assert
            Assert.Equal(expectedX, nextPoint.X);
            Assert.Equal(expectedY, nextPoint.Y);
        }
    }
}
