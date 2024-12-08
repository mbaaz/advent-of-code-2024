namespace MBZ.AdventOfCode.Year2024.Day16;

// This is my solution to the Advent of Code challenge!
// <see>https://adventofcode.com/2024/day/16</see>
[DaySolution(Day = 16, IsActive = false)]
public class Day16() : DaySolution(day: 16), IDaySolutionImplementation
{
    public override void RunPart1(bool isTest, string[] input, Action<OutputMessage> output)
    {
        var data = input.ParseToDay16Data();

        output(new("Result", "[not yet defined]"));
    }

    public override void RunPart2(bool isTest, string[] input, Action<OutputMessage> output)
    {
        var data = input.ParseToDay16Data();

        output(new("Result", "[not yet defined]"));
    }
}

public static class Day16Extensions
{
    public static List<string> ParseToDay16Data(this string[] input)
    {
        return input.ToList();
    }
}