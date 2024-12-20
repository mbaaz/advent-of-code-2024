using MBZ.AdventOfCode.Core.Solvers;

namespace MBZ.AdventOfCode.Year2024.Day21;

// This is my solution to the Advent of Code challenge!
// <see>https://adventofcode.com/2024/day/21</see>
[DaySolution(Day = 21, IsActive = false)]
public class Day21 : DaySolution, IDaySolutionImplementation
{
    [ExpectedResult(testResult: int.MaxValue, result: int.MaxValue)]
    public override long RunPart1(bool isTest, string[] input, Action<OutputMessage> output)
    {
        var data = input.ParseToDay21Data();

        output(new("Result", "[not yet defined]"));
        return -1;
    }

    [ExpectedResult(testResult: int.MaxValue, result: int.MaxValue)]
    public override long RunPart2(bool isTest, string[] input, Action<OutputMessage> output)
    {
        var data = input.ParseToDay21Data();

        output(new("Result", "[not yet defined]"));
        return -1;
    }
}

internal static class Day21Extensions
{
    public static List<string> ParseToDay21Data(this string[] input)
    {
        return input.ToList();
    }
}