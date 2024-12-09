using MBZ.AdventOfCode.Core.Solvers;

namespace MBZ.AdventOfCode.Year2024.Day09;

// This is my solution to the Advent of Code challenge!
// <see>https://adventofcode.com/2024/day/9</see>
[DaySolution(Day = 9, IsActive = false)]
public class Day09() : DaySolution(day: 9), IDaySolutionImplementation
{
    [ExpectedResult(testResult: int.MaxValue, result: int.MaxValue)]
    public override int RunPart1(bool isTest, string[] input, Action<OutputMessage> output)
    {
        var data = input.ParseToDay09Data();

        output(new("Result", "[not yet defined]"));
        return -1;
    }

    [ExpectedResult(testResult: int.MaxValue, result: int.MaxValue)]
    public override int RunPart2(bool isTest, string[] input, Action<OutputMessage> output)
    {
        var data = input.ParseToDay09Data();

        output(new("Result", "[not yet defined]"));
        return -1;
    }
}

public static class Day09Extensions
{
    public static List<string> ParseToDay09Data(this string[] input)
    {
        return input.ToList();
    }
}