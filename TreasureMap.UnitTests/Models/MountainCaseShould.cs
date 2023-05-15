using TreasureMap.ConsoleApp.Models;

namespace TreasureMap.UnitTests.Models
{
    public class MountainCaseShould
    {
        [Fact]
        public void Be_Created()
        {
            // Given
            var position = new Position(1, 1);

            // When
            var actual = new MountainCase(position);

            // Then
            Assert.Equal(position, actual.Position);
            Assert.False(actual.IsCollectable);
        }
    }
}
