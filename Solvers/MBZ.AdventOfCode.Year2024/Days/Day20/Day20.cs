using MBZ.AdventOfCode.Core.Solvers;

namespace MBZ.AdventOfCode.Year2024.Day20;

// This is my solution to the Advent of Code challenge!
// <see>https://adventofcode.com/2024/day/20</see>
[DaySolution(Day = 20, IsActive = false)]
public class Day20 : DaySolution, IDaySolutionImplementation
{
    [ExpectedResult(testResult: int.MaxValue, result: int.MaxValue)]
    public override int RunPart1(bool isTest, string[] input, Action<OutputMessage> output)
    {
        var data = input.ParseToDay20Data();

        output(new("Result", "[not yet defined]"));
        return -1;
    }

    [ExpectedResult(testResult: int.MaxValue, result: int.MaxValue)]
    public override int RunPart2(bool isTest, string[] input, Action<OutputMessage> output)
    {
        var data = input.ParseToDay20Data();

        output(new("Result", "[not yet defined]"));
        return -1;
    }
}

public static class Day20Extensions
{
    public static List<string> ParseToDay20Data(this string[] input)
    {
        return input.ToList();
    }
}