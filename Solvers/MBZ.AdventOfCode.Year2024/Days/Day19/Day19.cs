using MBZ.AdventOfCode.Core.Solvers;

namespace MBZ.AdventOfCode.Year2024.Day19;

// This is my solution to the Advent of Code challenge!
// <see>https://adventofcode.com/2024/day/19</see>
[DaySolution(Day = 19, IsActive = false)]
public class Day19 : DaySolution, IDaySolutionImplementation
{
    [ExpectedResult(testResult: int.MaxValue, result: int.MaxValue)]
    public override long RunPart1(bool isTest, string[] input, Action<OutputMessage> output)
    {
        var data = input.ParseToDay19Data();

        output(new("Result", "[not yet defined]"));
        return -1;
    }

    [ExpectedResult(testResult: int.MaxValue, result: int.MaxValue)]
    public override long RunPart2(bool isTest, string[] input, Action<OutputMessage> output)
    {
        var data = input.ParseToDay19Data();

        output(new("Result", "[not yet defined]"));
        return -1;
    }
}

internal static class Day19Extensions
{
    public static List<string> ParseToDay19Data(this string[] input)
    {
        return input.ToList();
    }
}