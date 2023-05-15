using TreasureMap.ConsoleApp.Models;
using TreasureMap.ConsoleApp.Services;

namespace TreasureMap.UnitTests.Services
{
    public class StringConverterShould
    {
        [Fact]
        public void Convert_Map_To_String()
        {
            // Given
            var adventurer = new AdventurerCase(new(1, 1), "John", 0, Direction.North);
            var treasure = new TreasureCase(new(2, 1));
            var mountain = new MountainCase(new(0, 1));
            var initialCases = new List<ICase>() { adventurer, treasure, mountain };
            var map = new Map(3, 5, initialCases, new List<Instruction>());

            // When
            var res = StringConverter.ConvertMapToString(map);

            // Then
            var expected = "C - 3 - 5\r\nM - 0 - 1\r\nT - 2 - 1 - 1\r\nA - John - 1 - 1 - N - 0\r\n";
            Assert.Equal(expected, res);
        }
    }
}
