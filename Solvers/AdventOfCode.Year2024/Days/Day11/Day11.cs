namespace MBZ.AdventOfCode.Year2024.Day11;

// This is my solution to the Advent of Code challenge!
// <see>https://adventofcode.com/2024/day/11</see>
[DaySolution(Day = 11, IsActive = false)]
public class Day11() : DaySolution(day: 11), IDaySolutionImplementation
{
    public override void RunPart1(bool isTest, string[] input, Action<OutputMessage> output)
    {
        var data = input.ParseToDay11Data();

        output(new("Result", "[not yet defined]"));
    }

    public override void RunPart2(bool isTest, string[] input, Action<OutputMessage> output)
    {
        var data = input.ParseToDay11Data();

        output(new("Result", "[not yet defined]"));
    }
}

public static class Day11Extensions
{
    public static List<string> ParseToDay11Data(this string[] input)
    {
        return input.ToList();
    }
}