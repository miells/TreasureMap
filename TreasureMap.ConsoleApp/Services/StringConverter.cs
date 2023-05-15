using System.Text;
using TreasureMap.ConsoleApp.Models;

namespace TreasureMap.ConsoleApp.Services
{
    public class StringConverter
    {
        public static string ConvertMapToString(Map map)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"C - {map.Width} - {map.Height}");
            foreach(var mountain in map.Cases.Where(c => c is MountainCase))
            {
                stringBuilder.AppendLine($"M - {mountain.Position.HorizontalAxis} - {mountain.Position.VerticalAxis}");
            }
            var treasures = map.Cases.Where(c => c is TreasureCase)
                .GroupBy(t => t.Position, t => 1)
                .Select(kvp => (position: kvp.Key, count: kvp.Count()));
            foreach (var (treasurePosition, treasureCount) in treasures)
            {
                stringBuilder.AppendLine($"T - {treasurePosition.HorizontalAxis} - {treasurePosition.VerticalAxis} - {treasureCount}");
            }
            var adventurers = map.Cases.Where(c => c is AdventurerCase)
                .Select(c => (AdventurerCase)c)
                .OrderBy(a => a.ApparitionOrder);
            foreach(var adventurer in adventurers)
            {
                stringBuilder.AppendLine($"A - {adventurer.Name} - {adventurer.Position.HorizontalAxis} - {adventurer.Position.VerticalAxis} - " +
                    $"{(char)adventurer.CurrentDirection} - {adventurer.TreasureCount}");
            }
            return stringBuilder.ToString();
        }
    }
}
