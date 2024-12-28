using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSimulator
{
    public class RectangleTest
    {
        [Theory]
        [InlineData(1, 2, 3, 4, 1, 2, true)]
        [InlineData(1, 2, 3, 4, 3, 4, true)]
        [InlineData(1, 2, 3, 4, 2, 3, true)]
        [InlineData(1, 2, 3, 4, 0, 0, false)]
        [InlineData(1, 2, 3, 4, 5, 5, false)]
        public void Contains_ShouldReturnCorrectResult(int x1, int y1, int x2, int y2, int x, int y, bool expected)
        {
            // Arrange
            var rectangle = new Rectangle(x1, y1, x2, y2);
            var point = new Point(x, y);

            // Act
            var result = rectangle.Contains(point);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
