using TreasureMap.ConsoleApp.Models;

namespace TreasureMap.UnitTests.Models
{
    public class PositionShould
    {
        [Fact]
        public void Be_Created()
        {
            // Given
            int horizontalAxis = 1;
            int verticalAxis = 2;

            // When
            var actual = new Position(horizontalAxis, verticalAxis);

            // Then
            Assert.Equal(horizontalAxis, actual.HorizontalAxis);
            Assert.Equal(verticalAxis, actual.VerticalAxis);
        }

        [Theory]
        [InlineData(-1, 0)]
        [InlineData(0, -1)]
        [InlineData(-1, -1)]
        [InlineData(2, 0)]
        [InlineData(0, 2)]
        [InlineData(2, 2)]
        public void Return_False_When_Position_Is_Invalid(int horizontalAxis, int verticalAxis)
        {
            // Given
            int width = 2;
            int length = 2;
            var position = new Position(horizontalAxis, verticalAxis);

            // When
            var res = position.IsValid(width, length);

            // Then
            Assert.False(res);
        }
    }
}
