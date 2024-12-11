using MBZ.AdventOfCode.Core.Solvers;

namespace MBZ.AdventOfCode.Year2024.Day24;

// This is my solution to the Advent of Code challenge!
// <see>https://adventofcode.com/2024/day/24</see>
[DaySolution(Day = 24, IsActive = false)]
public class Day24 : DaySolution, IDaySolutionImplementation
{
    [ExpectedResult(testResult: int.MaxValue, result: int.MaxValue)]
    public override long RunPart1(bool isTest, string[] input, Action<OutputMessage> output)
    {
        var data = input.ParseToDay24Data();

        output(new("Result", "[not yet defined]"));
        return -1;
    }

    [ExpectedResult(testResult: int.MaxValue, result: int.MaxValue)]
    public override long RunPart2(bool isTest, string[] input, Action<OutputMessage> output)
    {
        var data = input.ParseToDay24Data();

        output(new("Result", "[not yet defined]"));
        return -1;
    }
}

public static class Day24Extensions
{
    public static List<string> ParseToDay24Data(this string[] input)
    {
        return input.ToList();
    }
}