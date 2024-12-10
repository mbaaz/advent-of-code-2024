using MBZ.AdventOfCode.Core.Solvers;

namespace MBZ.AdventOfCode.Year2024.Day08;

// This is my solution to the Advent of Code challenge!
// <see>https://adventofcode.com/2024/day/8</see>
[DaySolution(Day = 8, IsActive = false)]
public class Day08 : DaySolution, IDaySolutionImplementation
{
    [ExpectedResult(testResult: int.MaxValue, result: int.MaxValue)]
    public override int RunPart1(bool isTest, string[] input, Action<OutputMessage> output)
    {
        var data = input.ParseToDay08Data();

        output(new("Result", "[not yet defined]"));
        return -1;
    }

    [ExpectedResult(testResult: int.MaxValue, result: int.MaxValue)]
    public override int RunPart2(bool isTest, string[] input, Action<OutputMessage> output)
    {
        var data = input.ParseToDay08Data();

        output(new("Result", "[not yet defined]"));
        return -1;
    }
}

public static class Day08Extensions
{
    public static List<string> ParseToDay08Data(this string[] input)
    {
        return input.ToList();
    }
}