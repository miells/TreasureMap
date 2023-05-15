namespace TreasureMap.ConsoleApp.Models
{
    public class Map
    {
        public int Width { get; }
        public int Height { get; }
        public IEnumerable<Instruction> Instructions { get; }
        public IEnumerable<ICase> Cases => _cases;
        private readonly IList<ICase> _cases;

        public Map(int width, int height, IEnumerable<ICase> initialCases, IEnumerable<Instruction> instructions)
        {
            var adventurersWithSamePosition = initialCases.Where(c => c is AdventurerCase)
                .GroupBy(c => c.Position)
                .Select(kvp => (position: kvp.Key, count: kvp.Count()))
                .Where(tuple => tuple.count > 1);
            if (adventurersWithSamePosition.Any())
                throw new Exception("Cannot have multiple adventurers on the same case");
            if (initialCases.Any(c => !c.Position.IsValid(width, height)))
                throw new Exception("Some positions are invalid because they are out of range");
            Width = width;
            Height = height;
            _cases = initialCases.ToList();
            Instructions = instructions.OrderBy(i => i.RoundNumber).ThenBy(i => i.Adventurer.ApparitionOrder); ;
        }

        public void ExecuteInstructions()
        {
            foreach (var instruction in Instructions)
            {
                var adventurer = _cases.Where(c => c is AdventurerCase)
                    .Select(c => (AdventurerCase)c)
                    .First(c => c.Name == instruction.Adventurer.Name);
                switch (instruction.Action)
                {
                    case AdventurerAction.MoveForward:
                        var position = adventurer.CalculateMoveForwardPosition();
                        if (!position.IsValid(Width, Height) || (_cases.Any(c => c.Position == position && !c.IsCollectable)))
                            break;
                        adventurer.SetPosition(position);
                        var treasure = _cases.FirstOrDefault(c => c.Position == position && c is TreasureCase);
                        if(treasure != null)
                        {
                            adventurer.AddTreasure();
                            _cases.Remove(treasure);
                        }
                        break;
                    case AdventurerAction.TurnLeft:
                        adventurer.TurnLeft();
                        break;
                    case AdventurerAction.TurnRight:
                        adventurer.TurnRight();
                        break;
                };
            }
        }
    }
}
