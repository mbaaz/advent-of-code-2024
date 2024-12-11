using MBZ.AdventOfCode.Core.Solvers;

namespace MBZ.AdventOfCode.Year2024.Day16;

// This is my solution to the Advent of Code challenge!
// <see>https://adventofcode.com/2024/day/16</see>
[DaySolution(Day = 16, IsActive = false)]
public class Day16 : DaySolution, IDaySolutionImplementation
{
    [ExpectedResult(testResult: int.MaxValue, result: int.MaxValue)]
    public override long RunPart1(bool isTest, string[] input, Action<OutputMessage> output)
    {
        var data = input.ParseToDay16Data();

        output(new("Result", "[not yet defined]"));
        return -1;
    }

    [ExpectedResult(testResult: int.MaxValue, result: int.MaxValue)]
    public override long RunPart2(bool isTest, string[] input, Action<OutputMessage> output)
    {
        var data = input.ParseToDay16Data();

        output(new("Result", "[not yet defined]"));
        return -1;
    }
}

public static class Day16Extensions
{
    public static List<string> ParseToDay16Data(this string[] input)
    {
        return input.ToList();
    }
}