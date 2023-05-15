namespace TreasureMap.ConsoleApp.Models
{
    public class TreasureCase : ICase
    {
        public bool IsCollectable => true;
        public Position Position { get; }

        public TreasureCase(Position position)
        {
            Position = position;
        }
    }
}
