namespace MyTestedApp.Test
{
    using System;

    using Xunit;

    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            // Arrange
            var a = 2;
            var b = 2;

            // Act
            Assert.Throws<DivideByZeroException>(() => a + b / 0);
            var sum = a + b;

            // Assert
            Assert.Equal(4, sum);
        }
    }
}
