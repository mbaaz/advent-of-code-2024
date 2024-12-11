using MBZ.AdventOfCode.Core.Solvers;

namespace MBZ.AdventOfCode.Year2024.Day12;

// This is my solution to the Advent of Code challenge!
// <see>https://adventofcode.com/2024/day/12</see>
[DaySolution(Day = 12, IsActive = false)]
public class Day12 : DaySolution, IDaySolutionImplementation
{
    [ExpectedResult(testResult: int.MaxValue, result: int.MaxValue)]
    public override long RunPart1(bool isTest, string[] input, Action<OutputMessage> output)
    {
        var data = input.ParseToDay12Data();

        output(new("Result", "[not yet defined]"));
        return -1;
    }

    [ExpectedResult(testResult: int.MaxValue, result: int.MaxValue)]
    public override long RunPart2(bool isTest, string[] input, Action<OutputMessage> output)
    {
        var data = input.ParseToDay12Data();

        output(new("Result", "[not yet defined]"));
        return -1;
    }
}

public static class Day12Extensions
{
    public static List<string> ParseToDay12Data(this string[] input)
    {
        return input.ToList();
    }
}