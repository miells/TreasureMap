using TreasureMap.ConsoleApp.Models;

namespace TreasureMap.ConsoleApp.Services
{
    public class ModelConverter
    {
        public static Map ConvertStringsToMap(string[] fileLines)
        {
            var adventurersAndInstructions = fileLines.Where(line => line.StartsWith('A')).Select((s, index) => ConvertStringToAdventurerCaseAndInstructions(s, index));
            var instructions = adventurersAndInstructions.SelectMany(t => t.instructions);
            var adventurerCases = adventurersAndInstructions.Select(t => t.adventurer);
            var mountainCases = fileLines.Where(line => line.StartsWith('M')).Select(ConvertStringToMountainCase);
            var treasureCases = fileLines.Where(line => line.StartsWith('T')).SelectMany(ConvertStringToTreasureCases);
            var initialCases = new List<ICase>();
            initialCases.AddRange(adventurerCases);
            initialCases.AddRange(mountainCases);
            initialCases.AddRange(treasureCases);
            var map = ConvertStringToMap(fileLines.First(line => line.StartsWith('C')), initialCases, instructions);
            return map;
        }

        public static (AdventurerCase adventurer, IEnumerable<Instruction> instructions) ConvertStringToAdventurerCaseAndInstructions(string value, int apparitionOrder)
        {
            var properties = value.Split('-', StringSplitOptions.TrimEntries);
            var name = properties[1];
            var horizontalAxis = int.Parse(properties[2]);
            var verticalAxis = int.Parse(properties[3]);
            var direction = properties[4].First();
            if (direction != 'N' && direction != 'S' && direction != 'O' && direction != 'E')
                throw new Exception($"Could not translate initial direction '{direction}' of character '{name}'");
            var adventurer = new AdventurerCase(new Position(horizontalAxis, verticalAxis), name, apparitionOrder, (Direction)direction);
            var instructions = properties[5].Select((c, index) =>
            {
                switch (c)
                {
                    case 'A':
                    case 'D':
                    case 'G':
                        return new Instruction(adventurer, index, (AdventurerAction)c);
                    default:
                        Console.WriteLine($"WARNING: Could not translate character '{c}' to an action, this action will be ignored");
                        return null;
                };
            }).Where(i => i != null).Select(i => i!);
            return (adventurer, instructions);
        }

        public static MountainCase ConvertStringToMountainCase(string value)
        {
            var properties = value.Split('-', StringSplitOptions.TrimEntries);
            var horizontalAxis = int.Parse(properties[1]);
            var verticalAxis = int.Parse(properties[2]);
            return new MountainCase(new Position(horizontalAxis, verticalAxis));
        }

        public static IEnumerable<TreasureCase> ConvertStringToTreasureCases(string value)
        {
            var properties = value.Split('-', StringSplitOptions.TrimEntries);
            var horizontalAxis = int.Parse(properties[1]);
            var verticalAxis = int.Parse(properties[2]);
            var count = int.Parse(properties[3]);
            return Enumerable.Repeat(new TreasureCase(new Position(horizontalAxis, verticalAxis)), count);
        }

        public static Map ConvertStringToMap(string value, IList<ICase> initialCases, IEnumerable<Instruction> instructions)
        {
            var properties = value.Split('-', StringSplitOptions.TrimEntries);
            var width = int.Parse(properties[1]);
            var height = int.Parse(properties[2]);
            return new Map(width, height, initialCases, instructions);
        }
    }
}
