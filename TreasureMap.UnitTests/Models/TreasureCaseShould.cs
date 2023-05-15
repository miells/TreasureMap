using TreasureMap.ConsoleApp.Models;

namespace TreasureMap.UnitTests.Models
{
    public class TreasureCaseShould
    {
        [Fact]
        public void Be_Created()
        {
            // Given
            var position = new Position(1, 1);

            // When
            var actual = new TreasureCase(position);

            // Then
            Assert.Equal(position, actual.Position);
            Assert.True(actual.IsCollectable);
        }
    }
}
