﻿namespace AoC.Y24.days;

// This is my solution to the Advent of Code challenge!
// <see>https://adventofcode.com/2024/day/10</see>
public class Day10() : DaySolution(day: 10), IDaySolutionImplementation
{
    public bool IsActive => false;

    public override void Run(Action<string> output)
    {
        UseTestFile = false;

        base.Run(output);
    }

    public override void RunPart1(string[] input, Action<string> output)
    {
        RunWithTimer(output, () =>
        {

            output($"""
PART 1
    result: [not yet defined!] 
""");
        });
    }

    public override void RunPart2(string[] input, Action<string> output)
    {
        RunWithTimer(output, () =>
        {

            output($"""
PART 2
    result: [not yet defined!] 
""");
        });
    }

    // ########################################################################################
}