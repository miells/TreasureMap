using TreasureMap.ConsoleApp.Models;
using TreasureMap.ConsoleApp.Services;

namespace TreasureMap.UnitTests.Services
{
    public class ModelConverterShould
    {
        [Theory]
        [InlineData('N', Direction.North, 'A', AdventurerAction.MoveForward)]
        [InlineData('S', Direction.South, 'G', AdventurerAction.TurnLeft)]
        [InlineData('O', Direction.West, 'D', AdventurerAction.TurnRight)]
        [InlineData('E', Direction.East, 'D', AdventurerAction.TurnRight)]
        public void Convert_String_To_Adventure_Case_And_Instructions(char directionChar, Direction expectedDirection, char actionChar, AdventurerAction expectedAction)
        {
            // Given
            var apparitionOrder = 1;
            var adventurerString = $"A - Lara - 2 - 1 - {directionChar} - {actionChar}";

            // When
            var (adventurer, instructions) = ModelConverter.ConvertStringToAdventurerCaseAndInstructions(adventurerString, apparitionOrder);

            // Then
            Assert.Equal("Lara", adventurer.Name);
            Assert.Equal(2, adventurer.Position.HorizontalAxis);
            Assert.Equal(1, adventurer.Position.VerticalAxis);
            Assert.Equal(apparitionOrder, adventurer.ApparitionOrder);
            Assert.False(adventurer.IsCollectable);
            Assert.Equal(expectedDirection, adventurer.CurrentDirection);
            Assert.Single(instructions);
            var instruction = instructions.First();
            Assert.Equal(expectedAction, instruction.Action);
            Assert.Equal(adventurer, instruction.Adventurer);
            Assert.Equal(0, instruction.RoundNumber);
        }

        [Fact]
        public void Convert_String_To_Adventure_Case_And_Instructions_With_Empty_Instruction_When_Action_Not_Recognized()
        {
            // Given
            var apparitionOrder = 1;
            var adventurerString = $"A - Lara - 2 - 1 - S - Z";

            // When
            var (adventurer, instructions) = ModelConverter.ConvertStringToAdventurerCaseAndInstructions(adventurerString, apparitionOrder);

            // Then
            Assert.Equal("Lara", adventurer.Name);
            Assert.Equal(2, adventurer.Position.HorizontalAxis);
            Assert.Equal(1, adventurer.Position.VerticalAxis);
            Assert.Equal(apparitionOrder, adventurer.ApparitionOrder);
            Assert.False(adventurer.IsCollectable);
            Assert.Equal(Direction.South, adventurer.CurrentDirection);
            Assert.Empty(instructions);
        }

        [Fact]
        public void Throw_Exception_When_Convert_String_To_Adventure_Case_And_Instructions_Has_Unrecognized_Direction()
        {
            // Given
            var apparitionOrder = 1;
            var adventurerString = $"A - Lara - 2 - 1 - Z - A";

            // When
            var exception = Assert.Throws<Exception>(() => ModelConverter.ConvertStringToAdventurerCaseAndInstructions(adventurerString, apparitionOrder));

            // Then
            Assert.Equal("Could not translate initial direction 'Z' of character 'Lara'", exception.Message);
        }

        [Fact]
        public void Convert_String_To_Mountain_Case()
        {
            // Given
            var mountainString = "M - 1 - 0";

            // When
            var mountain = ModelConverter.ConvertStringToMountainCase(mountainString);

            // Then
            Assert.Equal(1, mountain.Position.HorizontalAxis);
            Assert.Equal(0, mountain.Position.VerticalAxis);
            Assert.False(mountain.IsCollectable);
        }

        [Fact]
        public void Convert_String_To_Treasure_Cases()
        {
            // Given
            var treasureString = "T - 1 - 0 - 2";

            // When
            var treasures = ModelConverter.ConvertStringToTreasureCases(treasureString);

            // Then
            Assert.Equal(2, treasures.Count());
            Assert.All(treasures, t =>
            {
                Assert.Equal(1, t.Position.HorizontalAxis);
                Assert.Equal(0, t.Position.VerticalAxis);
                Assert.True(t.IsCollectable);
            });
        }

        [Fact]
        public void Convert_String_To_Map()
        {
            // Given
            var mapString = "C - 3 - 4";
            var initialCases = new List<ICase>();
            var instructions = new List<Instruction>();

            // When
            var map = ModelConverter.ConvertStringToMap(mapString, initialCases, instructions);

            // Then
            Assert.Equal(3, map.Width);
            Assert.Equal(4, map.Height);
            Assert.Empty(map.Instructions);
            Assert.Empty(map.Cases);
        }

        [Fact]
        public void Convert_Strings_To_Map()
        {
            // Given
            var lines = new string[]
            {
                "C - 3 - 4",
                "M - 1 - 0",
                "M - 2 - 1",
                "T - 0 - 3 - 2",
                "T - 1 - 3 - 3",
                "A - Lara - 1 - 1 - S - AADADAGGA"
            };

            // When
            var map = ModelConverter.ConvertStringsToMap(lines);

            // Then
            Assert.NotNull(map);
            Assert.Single(map.Cases.Where(c => c is AdventurerCase));
            Assert.Equal(2, map.Cases.Where(c => c is MountainCase).Count());
            Assert.Equal(5, map.Cases.Where(c => c is TreasureCase).Count());
        }
    }
}
