﻿using MBZ.AdventOfCode.Core.Solvers;

namespace MBZ.AdventOfCode.Year2024.Day01;

// This is my solution to the Advent of Code challenge!
// <see>https://adventofcode.com/2024/day/1</see>
[DaySolution(Day = 1, IsActive = true)]
public class Day01 : DaySolution, IDaySolutionImplementation
{
    [ExpectedResult(testResult: 11, result: 1660292)]
    public override long RunPart1(bool isTest, string[] input, Action<OutputMessage> output)
    {
        var (list1, list2) = input.ParseDay01Input();
        var distance = list1.CalculateTotalDistanceToList(list2);
        
        output(new("Total difference is", $"{distance:n0}"));
        return distance;
    }

    [ExpectedResult(testResult: 31, result: 22776016)]
    public override long RunPart2(bool isTest, string[] input, Action<OutputMessage> output)
    {
        var (list1, list2) = input.ParseDay01Input();
        var similarityScore = list1.CalculateSimilarityScore(list2);
        
        output(new("Similarity score is", $"{similarityScore:n0}"));
        return similarityScore;
    }
}