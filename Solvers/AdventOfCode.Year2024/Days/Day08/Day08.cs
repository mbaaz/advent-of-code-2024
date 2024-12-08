namespace MBZ.AdventOfCode.Year2024.Day08;

// This is my solution to the Advent of Code challenge!
// <see>https://adventofcode.com/2024/day/8</see>
[DaySolution(Day = 8, IsActive = false)]
public class Day08() : DaySolution(day: 8), IDaySolutionImplementation
{
    public override void RunPart1(bool isTest, string[] input, Action<OutputMessage> output)
    {
        var data = input.ParseToDay08Data();

        output(new("Result", "[not yet defined]"));
    }

    public override void RunPart2(bool isTest, string[] input, Action<OutputMessage> output)
    {
        var data = input.ParseToDay08Data();

        output(new("Result", "[not yet defined]"));
    }
}

public static class Day08Extensions
{
    public static List<string> ParseToDay08Data(this string[] input)
    {
        return input.ToList();
    }
}