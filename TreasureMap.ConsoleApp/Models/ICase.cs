namespace TreasureMap.ConsoleApp.Models
{
    public interface ICase
    {
        Position Position { get; }
        bool IsCollectable { get; }
    }
}
