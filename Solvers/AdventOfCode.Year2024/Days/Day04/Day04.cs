namespace MBZ.AdventOfCode.Year2024.Day04;

// This is my solution to the Advent of Code challenge!
// <see>https://adventofcode.com/2024/day/4</see>
[DaySolution(Day = 4, IsActive = true)]
public class Day04() : DaySolution(day: 4), IDaySolutionImplementation
{
    public override void RunPart1(bool isTest, string[] input, Action<OutputMessage> output)
    {
        var grid = new WordSearchGrid(input);
        var xmasOccurrences = grid.CalculateXmasOccurrences();
        
        output(new("Rows", grid.Rows));
        output(new("Columns", grid.Columns));
        output(new("XMAS occurs this many times", $"{xmasOccurrences:n0}"));
    }

    public override void RunPart2(bool isTest, string[] input, Action<OutputMessage> output)
    {
        var grid = new WordSearchGrid(input);

        var crossMasOccurrences = grid.CalculateCrossMasOccurrences();

        output(new("Rows", grid.Rows));
        output(new("Columns", grid.Columns));
        output(new("X-MAS occurs this many times", $"{crossMasOccurrences:n0}"));
    }
}