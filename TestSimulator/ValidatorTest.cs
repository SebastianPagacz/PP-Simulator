using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simulator;
namespace TestSimulator
{
    public class ValidatorTest
    {
        [Theory]
        [InlineData(5, 1, 10, 5)]
        [InlineData(0, 1, 10, 1)]
        [InlineData(15, 1, 10, 10)]
        public void Limiter_ShouldReturnLimitedValue(int value, int min, int max, int expected)
        {
            // Act
            var result = Validator.Limiter(value, min, max);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(null, 3, 10, "***")]
        [InlineData("", 3, 10, "***")]
        [InlineData("   ", 3, 10, "***")]
        [InlineData("test", 3, 10, "test")]
        [InlineData("verylongstring", 3, 10, "verylongst")]
        [InlineData("short", 3, 10, "short")]
        [InlineData("sh", 3, 10, "sh*")]
        public void Shortener_ShouldReturnShortenedValue(string value, int min, int max, string expected)
        {
            // Act
            var result = Validator.Shortener(value, min, max, '*');

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
