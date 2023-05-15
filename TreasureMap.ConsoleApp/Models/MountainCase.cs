namespace TreasureMap.ConsoleApp.Models
{
    public class MountainCase : ICase
    {
        public bool IsCollectable => false;
        public Position Position { get; }

        public MountainCase(Position position)
        {
            Position = position;
        }
    }
}
