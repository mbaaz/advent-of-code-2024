namespace MBZ.AdventOfCode.Year2024.Day17;

// This is my solution to the Advent of Code challenge!
// <see>https://adventofcode.com/2024/day/17</see>
[DaySolution(Day = 17, IsActive = false)]
public class Day17() : DaySolution(day: 17), IDaySolutionImplementation
{
    public override void RunPart1(bool isTest, string[] input, Action<OutputMessage> output)
    {
        var data = input.ParseToDay17Data();

        output(new("Result", "[not yet defined]"));
    }

    public override void RunPart2(bool isTest, string[] input, Action<OutputMessage> output)
    {
        var data = input.ParseToDay17Data();

        output(new("Result", "[not yet defined]"));
    }
}

public static class Day17Extensions
{
    public static List<string> ParseToDay17Data(this string[] input)
    {
        return input.ToList();
    }
}