// See https://aka.ms/new-console-template for more information
using System.Text;
using TreasureMap.ConsoleApp.Services;

Console.WriteLine("Welcome to the Treasure Map Console App!\n");

/* Possible files */
//var fileName = "example.txt";
//var fileName = "blocked_by_height.txt";
//var fileName = "blocked_by_mountain.txt";
//var fileName = "blocked_by_width.txt";
//var fileName = "two_adventurers.txt";
//var fileName = "error_invalid_int_cast.txt";
//var fileName = "error_adventurers_same_case.txt";
var fileName = "warning_invalid_action.txt";

var inputFilePath = $"Datas/{fileName}";
var outputFilePath = $"Datas/output_{fileName}";

try
{
    Console.WriteLine("------ Currently reading from input file ------");
    var lines = File.ReadAllLines(inputFilePath);
    var map = ModelConverter.ConvertStringsToMap(lines);
    Console.WriteLine("------ Operation was successful! ------\n");

    Console.WriteLine("------ Currently computing the instructions ------");
    map.ExecuteInstructions();
    Console.WriteLine("------ Operation was successful! ------\n");

    Console.WriteLine("------ Currently writing to output file ------");
    var outputText = StringConverter.ConvertMapToString(map);
    File.WriteAllText(outputFilePath, outputText);
    Console.WriteLine("------ Operation was successful! ------");
}
catch(Exception ex)
{
    Console.WriteLine($"ERROR: {ex.Message}");
    Console.WriteLine("------ Operation ended with error! ------");
    return -1;
}

return 0;