using MBZ.AdventOfCode.Core.Solvers;

namespace MBZ.AdventOfCode.Year2024.Day04;

// This is my solution to the Advent of Code challenge!
// <see>https://adventofcode.com/2024/day/4</see>
[DaySolution(Day = 4, IsActive = true)]
public class Day04 : DaySolution, IDaySolutionImplementation
{
    [ExpectedResult(testResult: 18, result: 2458)]
    public override long RunPart1(bool isTest, string[] input, Action<OutputMessage> output)
    {
        var grid = new WordSearchGrid(input);
        var xmasOccurrences = grid.CalculateXmasOccurrences();
        
        output(new("Rows", grid.Rows));
        output(new("Columns", grid.Columns));
        output(new("XMAS occurs this many times", $"{xmasOccurrences:n0}"));
        return xmasOccurrences;
    }

    [ExpectedResult(testResult: 9, result: 1945)]
    public override long RunPart2(bool isTest, string[] input, Action<OutputMessage> output)
    {
        var grid = new WordSearchGrid(input);

        var crossMasOccurrences = grid.CalculateCrossMasOccurrences();

        output(new("Rows", grid.Rows));
        output(new("Columns", grid.Columns));
        output(new("X-MAS occurs this many times", $"{crossMasOccurrences:n0}"));
        return crossMasOccurrences;
    }
}