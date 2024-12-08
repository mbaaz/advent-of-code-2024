﻿namespace MBZ.AdventOfCode.Year2024.Day25;

// This is my solution to the Advent of Code challenge!
// <see>https://adventofcode.com/2024/day/25</see>
[DaySolution(Day = 25, IsActive = false)]
public class Day25() : DaySolution(day: 25), IDaySolutionImplementation
{
    public override void RunPart1(bool isTest, string[] input, Action<OutputMessage> output)
    {
        var data = input.ParseToDay25Data();

        output(new ("Result", "[not yet defined]"));
    }

    public override void RunPart2(bool isTest, string[] input, Action<OutputMessage> output)
    {
        var data = input.ParseToDay25Data();

        output(new("Result", "[not yet defined]"));
    }
}

public static class Day25Extensions
{
    public static List<string> ParseToDay25Data(this string[] input)
    {
        return input.ToList();
    }
}