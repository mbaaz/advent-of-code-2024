﻿namespace MBZ.AdventOfCode.Year2024.Day15;

// This is my solution to the Advent of Code challenge!
// <see>https://adventofcode.com/2024/day/15</see>
[DaySolution(Day = 15, IsActive = false)]
public class Day15() : DaySolution(day: 15), IDaySolutionImplementation
{
    public override void RunPart1(bool isTest, string[] input, Action<OutputMessage> output)
    {
        var data = input.ParseToDay15Data();

        output(new("Result", "[not yet defined]"));
    }

    public override void RunPart2(bool isTest, string[] input, Action<OutputMessage> output)
    {
        var data = input.ParseToDay15Data();

        output(new("Result", "[not yet defined]"));
    }
}

public static class Day15Extensions
{
    public static List<string> ParseToDay15Data(this string[] input)
    {
        return input.ToList();
    }
}