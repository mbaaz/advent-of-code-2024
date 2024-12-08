namespace MBZ.AdventOfCode.Year2024.Day07;

// This is my solution to the Advent of Code challenge!
// <see>https://adventofcode.com/2024/day/7</see>
[DaySolution(Day = 7, IsActive = false)]
public class Day07() : DaySolution(day: 7), IDaySolutionImplementation
{
    public override void RunPart1(bool isTest, string[] input, Action<OutputMessage> output)
    {
        var data = input.ParseToDay07Data();

        output(new("Result", "[not yet defined]"));
    }

    public override void RunPart2(bool isTest, string[] input, Action<OutputMessage> output)
    {
        var data = input.ParseToDay07Data();

        output(new("Result", "[not yet defined]"));
    }
}

public static class Day07Extensions
{
    public static List<string> ParseToDay07Data(this string[] input)
    {
        return input.ToList();
    }
}