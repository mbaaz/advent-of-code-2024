namespace MBZ.AdventOfCode.Year2024.Day13;

// This is my solution to the Advent of Code challenge!
// <see>https://adventofcode.com/2024/day/13</see>
[DaySolution(Day = 13, IsActive = false)]
public class Day13() : DaySolution(day: 13), IDaySolutionImplementation
{
    public override void RunPart1(bool isTest, string[] input, Action<OutputMessage> output)
    {
        var data = input.ParseToDay13Data();

        output(new("Result", "[not yet defined]"));
    }

    public override void RunPart2(bool isTest, string[] input, Action<OutputMessage> output)
    {
        var data = input.ParseToDay13Data();

        output(new("Result", "[not yet defined]"));
    }
}

public static class Day13Extensions
{
    public static List<string> ParseToDay13Data(this string[] input)
    {
        return input.ToList();
    }
}