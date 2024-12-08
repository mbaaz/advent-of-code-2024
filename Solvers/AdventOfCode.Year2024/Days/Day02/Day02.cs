﻿namespace MBZ.AdventOfCode.Year2024.Day02;

// This is my solution to the Advent of Code challenge!
// <see>https://adventofcode.com/2024/day/2</see>
[DaySolution(Day = 2, IsActive = true)]
public class Day02() : DaySolution(day: 2), IDaySolutionImplementation
{
    public override void RunPart1(bool isTest, string[] input, Action<OutputMessage> output)
    {
        var reports = input.ParseDay02Input();
        var safeReports = reports
            .Count(report => report.IsSafeReport())
        ;

        output(new("Number of total reports", $"{reports.Count:n0}"));
        output(new("Number of safe reports", $"{safeReports:n0}"));
    }

    public override void RunPart2(bool isTest, string[] input, Action<OutputMessage> output)
    {
        var reports = input.ParseDay02Input();
        var safeReports = reports
            .Count(report => report.IsSafeReport(useProblemDampener: true))
        ;

        output(new("Number of total reports", $"{reports.Count:n0}"));
        output(new("Number of safe reports with Problem Dampener active", $"{safeReports:n0}"));
    }
}