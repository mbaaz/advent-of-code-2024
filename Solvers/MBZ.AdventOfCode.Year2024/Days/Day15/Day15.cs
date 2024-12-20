using MBZ.AdventOfCode.Core.Solvers;

namespace MBZ.AdventOfCode.Year2024.Day15;

// This is my solution to the Advent of Code challenge!
// <see>https://adventofcode.com/2024/day/15</see>
[DaySolution(Day = 15, IsActive = false)]
public class Day15 : DaySolution, IDaySolutionImplementation
{
    [ExpectedResult(testResult: int.MaxValue, result: int.MaxValue)]
    public override long RunPart1(bool isTest, string[] input, Action<OutputMessage> output)
    {
        var data = input.ParseToDay15Data();

        output(new("Result", "[not yet defined]"));
        return -1;
    }

    [ExpectedResult(testResult: int.MaxValue, result: int.MaxValue)]
    public override long RunPart2(bool isTest, string[] input, Action<OutputMessage> output)
    {
        var data = input.ParseToDay15Data();

        output(new("Result", "[not yet defined]"));
        return -1;
    }
}

internal static class Day15Extensions
{
    public static List<string> ParseToDay15Data(this string[] input)
    {
        return input.ToList();
    }
}