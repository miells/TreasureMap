using TreasureMap.ConsoleApp.Models;

namespace TreasureMap.UnitTests.Models
{
    public class MapShould
    {
        [Fact]
        public void Be_Created()
        {
            // Given
            int width = 3;
            int height = 5;
            var adventurer = new AdventurerCase(new(1, 1), "John", 0, Direction.North);
            var initialCases = new List<ICase>() { adventurer };
            var instructions = new List<Instruction>() { new(adventurer, 0, AdventurerAction.TurnRight) };

            // When
            var actual = new Map(width, height, initialCases, instructions);

            // Then
            Assert.Equal(width, actual.Width);
            Assert.Equal(height, actual.Height);
            Assert.Single(actual.Cases);
            Assert.Single(actual.Instructions);
        }

        [Fact]
        public void Throw_Exception_When_Creating_With_Two_Adventurers_On_Same_Case()
        {
            // Given
            int width = 3;
            int height = 5;
            var adventurer1 = new AdventurerCase(new(1, 1), "John", 0, Direction.North);
            var adventurer2 = new AdventurerCase(new(1, 1), "Jane", 0, Direction.South);
            var initialCases = new List<ICase>() { adventurer1, adventurer2 };
            var instructions = new List<Instruction>() { new(adventurer1, 0, AdventurerAction.TurnRight) };

            // When
            var exception = Assert.Throws<Exception>(() => new Map(width, height, initialCases, instructions));

            // Then
            Assert.Equal("Cannot have multiple adventurers on the same case", exception.Message);
        }

        [Theory]
        [InlineData(-1, 0)]
        [InlineData(0, -1)]
        [InlineData(3, 0)]
        [InlineData(0, 5)]
        public void Throw_Exception_When_Creating_With_Invalid_Positions(int horizontalAxis, int verticalAxis)
        {
            // Given
            int width = 3;
            int height = 5;
            var initialCases = new List<ICase>() { new MountainCase(new(horizontalAxis, verticalAxis)) };
            var instructions = new List<Instruction>();

            // When
            var exception = Assert.Throws<Exception>(() => new Map(width, height, initialCases, instructions));

            // Then
            Assert.Equal("Some positions are invalid because they are out of range", exception.Message);
        }

        [Fact]
        public void Execute_Move_Forward_Instruction()
        {
            // Given
            var adventurer = new AdventurerCase(new(1, 1), "John", 0, Direction.South);
            var initialCases = new List<ICase>() { new MountainCase(new(0, 1)), new TreasureCase(new(2, 1)), adventurer };
            var instructions = new List<Instruction>() { new Instruction(adventurer, 0, AdventurerAction.MoveForward) };
            var map = new Map(3, 3, initialCases, instructions);

            // When
            map.ExecuteInstructions();

            // Then
            Assert.Equal(initialCases.Count, map.Cases.Count());
            Assert.Equal(instructions.Count, map.Instructions.Count());
            var actualAdventurer = map.Cases.First(c => c is AdventurerCase) as AdventurerCase;
            Assert.Equal(1, actualAdventurer!.Position.HorizontalAxis);
            Assert.Equal(2, actualAdventurer.Position.VerticalAxis);
            Assert.Equal(adventurer.CurrentDirection, actualAdventurer.CurrentDirection);
            Assert.Equal(adventurer.TreasureCount, actualAdventurer.TreasureCount);
        }

        [Fact]
        public void Execute_Move_Forward_Instruction_And_Collect_Treasure()
        {
            // Given
            var adventurer = new AdventurerCase(new(1, 1), "John", 0, Direction.East);
            var initialCases = new List<ICase>() { new MountainCase(new(0, 1)), new TreasureCase(new(2, 1)), adventurer };
            var instructions = new List<Instruction>() { new Instruction(adventurer, 0, AdventurerAction.MoveForward) };
            var map = new Map(3, 3, initialCases, instructions);

            // When
            map.ExecuteInstructions();

            // Then
            Assert.Equal(initialCases.Count - 1, map.Cases.Count());
            Assert.Equal(instructions.Count, map.Instructions.Count());
            var actualAdventurer = map.Cases.First(c => c is AdventurerCase) as AdventurerCase;
            Assert.Equal(2, actualAdventurer!.Position.HorizontalAxis);
            Assert.Equal(1, actualAdventurer.Position.VerticalAxis);
            Assert.Equal(adventurer.CurrentDirection, actualAdventurer.CurrentDirection);
            Assert.Equal(1, actualAdventurer.TreasureCount);
        }

        [Fact]
        public void Not_Execute_Move_Forward_Instruction_When_Meeting_Mountain()
        {
            // Given
            var adventurer = new AdventurerCase(new(1, 1), "John", 0, Direction.North);
            var initialCases = new List<ICase>() { new MountainCase(new(0, 1)), new TreasureCase(new(2, 1)), adventurer };
            var instructions = new List<Instruction>() { new Instruction(adventurer, 0, AdventurerAction.MoveForward) };
            var map = new Map(3, 3, initialCases, instructions);

            // When
            map.ExecuteInstructions();

            // Then
            Assert.Equal(initialCases.Count, map.Cases.Count());
            Assert.Equal(instructions.Count, map.Instructions.Count());
            var actualAdventurer = map.Cases.First(c => c is AdventurerCase) as AdventurerCase;
            Assert.Equal(adventurer.Position.HorizontalAxis, actualAdventurer!.Position.HorizontalAxis);
            Assert.Equal(adventurer.Position.VerticalAxis, actualAdventurer.Position.VerticalAxis);
            Assert.Equal(adventurer.CurrentDirection, actualAdventurer.CurrentDirection);
            Assert.Equal(adventurer.TreasureCount, actualAdventurer.TreasureCount);
        }

        [Fact]
        public void Not_Execute_Move_Forward_Instruction_When_Meeting_Another_Adventurer()
        {
            // Given
            var adventurer = new AdventurerCase(new(1, 1), "John", 0, Direction.North);
            var adventurer2 = new AdventurerCase(new(0, 1), "Jane", 0, Direction.North);
            var initialCases = new List<ICase>() { new TreasureCase(new(2, 1)), adventurer, adventurer2 };
            var instructions = new List<Instruction>() { new Instruction(adventurer, 0, AdventurerAction.MoveForward) };
            var map = new Map(3, 3, initialCases, instructions);

            // When
            map.ExecuteInstructions();

            // Then
            Assert.Equal(initialCases.Count, map.Cases.Count());
            Assert.Equal(instructions.Count, map.Instructions.Count());
            var actualAdventurer = map.Cases.First(c => c is AdventurerCase) as AdventurerCase;
            Assert.Equal(adventurer.Position.HorizontalAxis, actualAdventurer!.Position.HorizontalAxis);
            Assert.Equal(adventurer.Position.VerticalAxis, actualAdventurer.Position.VerticalAxis);
            Assert.Equal(adventurer.CurrentDirection, actualAdventurer.CurrentDirection);
            Assert.Equal(adventurer.TreasureCount, actualAdventurer.TreasureCount);
        }

        [Theory]
        [InlineData(0,0,Direction.West)]
        [InlineData(0,0,Direction.North)]
        [InlineData(2,2,Direction.South)]
        [InlineData(2,2,Direction.East)]
        public void Not_Execute_Move_Forward_Instruction_When_Reaching_End_Of_Map(int horizontalAxis, int verticalAxis, Direction direction)
        {
            // Given
            var adventurer = new AdventurerCase(new(horizontalAxis, verticalAxis), "John", 0, direction);
            var initialCases = new List<ICase>() { adventurer };
            var instructions = new List<Instruction>() { new Instruction(adventurer, 0, AdventurerAction.MoveForward) };
            var map = new Map(3, 3, initialCases, instructions);

            // When
            map.ExecuteInstructions();

            // Then
            Assert.Equal(initialCases.Count, map.Cases.Count());
            Assert.Equal(instructions.Count, map.Instructions.Count());
            var actualAdventurer = map.Cases.First(c => c is AdventurerCase) as AdventurerCase;
            Assert.Equal(horizontalAxis, actualAdventurer!.Position.HorizontalAxis);
            Assert.Equal(verticalAxis, actualAdventurer.Position.VerticalAxis);
            Assert.Equal(adventurer.CurrentDirection, actualAdventurer.CurrentDirection);
            Assert.Equal(adventurer.TreasureCount, actualAdventurer.TreasureCount);
        }

        [Theory]
        [InlineData(AdventurerAction.TurnLeft, Direction.North, Direction.West)]
        [InlineData(AdventurerAction.TurnRight, Direction.North, Direction.East)]
        public void Execute_Turn_Instruction(AdventurerAction action, Direction initialDirection, Direction expectedDirection)
        {
            // Given
            var adventurer = new AdventurerCase(new(1,1), "John", 0, initialDirection);
            var initialCases = new List<ICase>() { adventurer };
            var instructions = new List<Instruction>() { new Instruction(adventurer, 0, action) };
            var map = new Map(3, 3, initialCases, instructions);

            // When
            map.ExecuteInstructions();

            // Then
            Assert.Equal(initialCases.Count, map.Cases.Count());
            Assert.Equal(instructions.Count, map.Instructions.Count());
            var actualAdventurer = map.Cases.First(c => c is AdventurerCase) as AdventurerCase;
            Assert.Equal(adventurer.Position.HorizontalAxis, actualAdventurer!.Position.HorizontalAxis);
            Assert.Equal(adventurer.Position.VerticalAxis, actualAdventurer.Position.VerticalAxis);
            Assert.Equal(expectedDirection, actualAdventurer.CurrentDirection);
            Assert.Equal(adventurer.TreasureCount, actualAdventurer.TreasureCount);
        }
    }
}
