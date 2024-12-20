using MBZ.AdventOfCode.Core.Solvers;

namespace MBZ.AdventOfCode.Year2024.Day11;

// This is my solution to the Advent of Code challenge!
// <see>https://adventofcode.com/2024/day/11</see>
[DaySolution(Day = 11, IsActive = false)]
public class Day11 : DaySolution, IDaySolutionImplementation
{
    [ExpectedResult(testResult: int.MaxValue, result: int.MaxValue)]
    public override long RunPart1(bool isTest, string[] input, Action<OutputMessage> output)
    {
        var data = input.ParseToDay11Data();

        output(new("Result", "[not yet defined]"));
        return -1;
    }

    [ExpectedResult(testResult: int.MaxValue, result: int.MaxValue)]
    public override long RunPart2(bool isTest, string[] input, Action<OutputMessage> output)
    {
        var data = input.ParseToDay11Data();

        output(new("Result", "[not yet defined]"));
        return -1;
    }
}

internal static class Day11Extensions
{
    public static List<string> ParseToDay11Data(this string[] input)
    {
        return input.ToList();
    }
}