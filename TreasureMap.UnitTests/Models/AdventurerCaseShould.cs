using TreasureMap.ConsoleApp.Models;

namespace TreasureMap.UnitTests.Models
{
    public class AdventurerCaseShould
    {
        [Fact]
        public void Be_Created()
        {
            // Given
            var position = new Position(1, 1);
            var name = "John";
            var apparitionOrder = 0;
            var direction = Direction.North;

            // When
            var actual = new AdventurerCase(position, name, apparitionOrder, direction);

            // Then
            Assert.Equal(name, actual.Name);
            Assert.Equal(apparitionOrder, actual.ApparitionOrder);
            Assert.Equal(direction, actual.CurrentDirection);
            Assert.False(actual.IsCollectable);
            Assert.Equal(position, actual.Position);
            Assert.Equal(0, actual.TreasureCount);
        }

        [Theory]
        [InlineData(Direction.North, Direction.West)]
        [InlineData(Direction.West, Direction.South)]
        [InlineData(Direction.South, Direction.East)]
        [InlineData(Direction.East, Direction.North)]
        public void Turn_Left(Direction initialDirection, Direction expectedDirection)
        {
            // Given
            var adventurer = new AdventurerCase(new(1, 1), "John", 0, initialDirection);

            // When
            adventurer.TurnLeft();

            // Then
            Assert.Equal(expectedDirection, adventurer.CurrentDirection);
        }

        [Theory]
        [InlineData(Direction.North, Direction.East)]
        [InlineData(Direction.East, Direction.South)]
        [InlineData(Direction.South, Direction.West)]
        [InlineData(Direction.West, Direction.North)]
        public void Turn_Right(Direction initialDirection, Direction expectedDirection)
        {
            // Given
            var adventurer = new AdventurerCase(new(1, 1), "John", 0, initialDirection);

            // When
            adventurer.TurnRight();

            // Then
            Assert.Equal(expectedDirection, adventurer.CurrentDirection);
        }

        [Theory]
        [InlineData(Direction.North, 1, 0)]
        [InlineData(Direction.South, 1, 2)]
        [InlineData(Direction.East, 2, 1)]
        [InlineData(Direction.West, 0, 1)]
        public void Calculate_Move_Forward_Position(Direction direction, int expectedHorizontalAxis, int expectedVerticalAxis)
        {
            // Given
            var adventurer = new AdventurerCase(new(1, 1), "John", 0, direction);

            // When
            var position = adventurer.CalculateMoveForwardPosition();

            // Then
            Assert.Equal(expectedHorizontalAxis, position.HorizontalAxis);
            Assert.Equal(expectedVerticalAxis, position.VerticalAxis);
        }

        [Fact]
        public void Set_Position()
        {
            // Given
            var newPosition = new Position(2, 2);
            var adventurer = new AdventurerCase(new(1, 1), "John", 0, Direction.South);

            // When
            adventurer.SetPosition(newPosition);

            // Then
            Assert.Equal(newPosition.HorizontalAxis, adventurer.Position.HorizontalAxis);
            Assert.Equal(newPosition.VerticalAxis, adventurer.Position.VerticalAxis);
        }

        [Fact]
        public void Add_Treasure()
        {
            // Given
            var adventurer = new AdventurerCase(new(1, 1), "John", 0, Direction.South);

            // When
            adventurer.AddTreasure();

            // Then
            Assert.Equal(1, adventurer.TreasureCount);
        }
    }
}
