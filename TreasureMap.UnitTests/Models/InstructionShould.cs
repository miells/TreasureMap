using TreasureMap.ConsoleApp.Models;

namespace TreasureMap.UnitTests.Models
{
    public class InstructionShould
    {
        [Fact]
        public void Be_Created()
        {
            // Given
            var adventurer = new AdventurerCase(new(1, 1), "Name", 0, Direction.South);
            var roundNumber = 1;
            var action = AdventurerAction.MoveForward;

            // When
            var actual = new Instruction(adventurer, roundNumber, action);

            // Then
            Assert.Equal(adventurer.Name, actual.Adventurer.Name);
            Assert.Equal(roundNumber, actual.RoundNumber);
            Assert.Equal(action, actual.Action);
        }
    }
}
