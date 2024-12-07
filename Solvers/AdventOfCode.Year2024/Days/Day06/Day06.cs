﻿namespace MBZ.AdventOfCode.Year2024.Day06;

// This is my solution to the Advent of Code challenge!
// <see>https://adventofcode.com/2024/day/6</see>
[DaySolution(Day = 6, IsActive = false)]
public class Day06() : DaySolution(day: 6), IDaySolutionImplementation
{
    public override void RunPart1(bool isTest, string[] input, Action<OutputMessage> output)
    {
        var data = input.ParseToDay06Data();

        output(new("Result", "[not yet defined]"));
    }

    public override void RunPart2(bool isTest, string[] input, Action<OutputMessage> output)
    {
        var data = input.ParseToDay06Data();

        output(new("Result", "[not yet defined]"));
    }
}

public static class Day06Extensions
{
    public static List<string> ParseToDay06Data(this string[] input)
    {
        return input.ToList();
    }
}