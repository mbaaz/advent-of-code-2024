using MBZ.AdventOfCode.Core.Solvers;

namespace MBZ.AdventOfCode.Year2024.Day22;

// This is my solution to the Advent of Code challenge!
// <see>https://adventofcode.com/2024/day/22</see>
[DaySolution(Day = 22, IsActive = false)]
public class Day22() : DaySolution(day: 22), IDaySolutionImplementation
{
    [ExpectedResult(testResult: int.MaxValue, result: int.MaxValue)]
    public override int RunPart1(bool isTest, string[] input, Action<OutputMessage> output)
    {
        var data = input.ParseToDay22Data();

        output(new("Result", "[not yet defined]"));
        return -1;
    }

    [ExpectedResult(testResult: int.MaxValue, result: int.MaxValue)]
    public override int RunPart2(bool isTest, string[] input, Action<OutputMessage> output)
    {
        var data = input.ParseToDay22Data();

        output(new("Result", "[not yet defined]"));
        return -1;
    }
}

public static class Day22Extensions
{
    public static List<string> ParseToDay22Data(this string[] input)
    {
        return input.ToList();
    }
}