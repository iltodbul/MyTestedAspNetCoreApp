using MyTestedAspNetCoreApp.ValidationAttributes;
using Xunit;

namespace MyTestedApp.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class CurrentYearAttributeTest
    {
        [Fact]
        public void IsValidReturnsFalseForCurrentYear()
        {
            // Arrange
            var attribute = new MinToCurrentYearAttribute(1900);

            // Act
            var isValid = attribute.IsValid(DateTime.UtcNow.AddYears(1));

            // Assert
            Assert.False(isValid);

        }

        [Fact]
        public void IsValidReturnsFalseForYearAfterCurrentYear()
        {
            // Arrange
            var attribute = new MinToCurrentYearAttribute(1900);

            // Act
            var isValid = attribute.IsValid(DateTime.UtcNow.AddYears(1).Year);

            // Assert
            Assert.False(isValid);

        }

        [Fact]
        public void IsValidReturnsTrueForYearsBeforeCurrentYear()
        {
            // Arrange
            var attribute = new MinToCurrentYearAttribute(1900);

            // Act
            var isValid = attribute.IsValid(DateTime.UtcNow.AddYears(-1).Year);

            // Assert
            Assert.True(isValid);

        }

        [Fact]
        public void IsValidReturnsTrueForDateTameBeforeCurrentYear()
        {
            // Arrange
            var attribute = new MinToCurrentYearAttribute(1900);

            // Act
            var isValid = attribute.IsValid(DateTime.UtcNow.AddYears(-1));

            // Assert
            Assert.True(isValid);

        }
    }
}
