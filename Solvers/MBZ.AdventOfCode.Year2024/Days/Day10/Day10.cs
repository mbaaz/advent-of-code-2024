using MBZ.AdventOfCode.Core.Solvers;

namespace MBZ.AdventOfCode.Year2024.Day10;

// This is my solution to the Advent of Code challenge!
// <see>https://adventofcode.com/2024/day/10</see>
[DaySolution(Day = 10, IsActive = false)]
public class Day10 : DaySolution, IDaySolutionImplementation
{
    [ExpectedResult(testResult: int.MaxValue, result: int.MaxValue)]
    public override long RunPart1(bool isTest, string[] input, Action<OutputMessage> output)
    {
        var data = input.ParseToDay10Data();

        output(new("Result", "[not yet defined]"));
        return -1;
    }

    [ExpectedResult(testResult: int.MaxValue, result: int.MaxValue)]
    public override long RunPart2(bool isTest, string[] input, Action<OutputMessage> output)
    {
        var data = input.ParseToDay10Data();

        output(new("Result", "[not yet defined]"));
        return -1;
    }
}

public static class Day10Extensions
{
    public static List<string> ParseToDay10Data(this string[] input)
    {
        return input.ToList();
    }
}