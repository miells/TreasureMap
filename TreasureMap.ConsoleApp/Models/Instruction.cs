namespace TreasureMap.ConsoleApp.Models
{
    public class Instruction
    {
        public AdventurerCase Adventurer { get; }
        public int RoundNumber { get; }
        public AdventurerAction Action { get; }

        public Instruction(AdventurerCase adventurer, int roundNumber, AdventurerAction action)
        {
            Adventurer = adventurer;
            RoundNumber = roundNumber;
            Action = action;
        }
    }
}
