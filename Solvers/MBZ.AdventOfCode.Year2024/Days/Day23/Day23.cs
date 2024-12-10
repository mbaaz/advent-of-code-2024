using MBZ.AdventOfCode.Core.Solvers;

namespace MBZ.AdventOfCode.Year2024.Day23;

// This is my solution to the Advent of Code challenge!
// <see>https://adventofcode.com/2024/day/23</see>
[DaySolution(Day = 23, IsActive = false)]
public class Day23 : DaySolution, IDaySolutionImplementation
{
    [ExpectedResult(testResult: int.MaxValue, result: int.MaxValue)]
    public override int RunPart1(bool isTest, string[] input, Action<OutputMessage> output)
    {
        var data = input.ParseToDay23Data();

        output(new("Result", "[not yet defined]"));
        return -1;
    }

    [ExpectedResult(testResult: int.MaxValue, result: int.MaxValue)]
    public override int RunPart2(bool isTest, string[] input, Action<OutputMessage> output)
    {
        var data = input.ParseToDay23Data();

        output(new("Result", "[not yet defined]"));
        return -1;
    }
}

public static class Day23Extensions
{
    public static List<string> ParseToDay23Data(this string[] input)
    {
        return input.ToList();
    }
}