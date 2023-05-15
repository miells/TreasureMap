namespace TreasureMap.ConsoleApp.Models
{
    public class AdventurerCase : ICase
    {
        public bool IsCollectable => false;
        public Position Position { get; private set; }
        public string Name { get; }
        public int ApparitionOrder { get; }
        public Direction CurrentDirection { get; private set; }
        public int TreasureCount { get; private set; }

        public AdventurerCase(Position position, string name, int apparitionOrder, Direction initialDirection)
        {
            Position = position;
            Name = name;
            ApparitionOrder = apparitionOrder;
            CurrentDirection = initialDirection;
            TreasureCount = 0;
        }

        public void TurnLeft()
        {
            CurrentDirection = CurrentDirection switch
            {
                Direction.North => Direction.West,
                Direction.West => Direction.South,
                Direction.South => Direction.East,
                Direction.East => Direction.North,
                _ => CurrentDirection
            };
        }

        public void TurnRight()
        {
            CurrentDirection = CurrentDirection switch
            {
                Direction.North => Direction.East,
                Direction.East => Direction.South,
                Direction.South => Direction.West,
                Direction.West => Direction.North,
                _ => CurrentDirection
            };
        }

        public Position CalculateMoveForwardPosition()
        {
            return CurrentDirection switch
            {
                Direction.North => new Position(Position.HorizontalAxis, Position.VerticalAxis - 1),
                Direction.South => new Position(Position.HorizontalAxis, Position.VerticalAxis + 1),
                Direction.East => new Position(Position.HorizontalAxis + 1, Position.VerticalAxis),
                Direction.West => new Position(Position.HorizontalAxis - 1, Position.VerticalAxis),
                _ => Position,
            };
        }

        public void SetPosition(Position position)
        {
            Position = position;
        }

        public void AddTreasure()
        {
            TreasureCount += 1;
        }
    }
}
