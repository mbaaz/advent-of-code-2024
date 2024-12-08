namespace MBZ.AdventOfCode.Year2024.Day18;

// This is my solution to the Advent of Code challenge!
// <see>https://adventofcode.com/2024/day/18</see>
[DaySolution(Day = 18, IsActive = false)]
public class Day18() : DaySolution(day: 18), IDaySolutionImplementation
{
    public override void RunPart1(bool isTest, string[] input, Action<OutputMessage> output)
    {
        var data = input.ParseToDay18Data();

        output(new("Result", "[not yet defined]"));
    }

    public override void RunPart2(bool isTest, string[] input, Action<OutputMessage> output)
    {
        var data = input.ParseToDay18Data();

        output(new("Result", "[not yet defined]"));
    }
}

public static class Day18Extensions
{
    public static List<string> ParseToDay18Data(this string[] input)
    {
        return input.ToList();
    }
}