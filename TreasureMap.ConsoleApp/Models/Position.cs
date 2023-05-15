namespace TreasureMap.ConsoleApp.Models
{
    public record Position
    {
        public int HorizontalAxis { get; }
        public int VerticalAxis { get; }

        public Position(int horizontalAxis, int verticalAxis)
        {
            HorizontalAxis = horizontalAxis;
            VerticalAxis = verticalAxis;
        }

        public bool IsValid(int width, int height)
            => HorizontalAxis < width && VerticalAxis < height && HorizontalAxis >= 0 && VerticalAxis >= 0;
    }
}
