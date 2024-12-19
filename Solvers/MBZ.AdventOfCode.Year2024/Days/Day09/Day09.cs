using System.Collections.Generic;
using MBZ.AdventOfCode.Core.Configuration;
using MBZ.AdventOfCode.Core.Solvers;

namespace MBZ.AdventOfCode.Year2024.Day09;

// This is my solution to the Advent of Code challenge!
// <see>https://adventofcode.com/2024/day/9</see>
[DaySolution(Day = 9, IsActive = true)]
public class Day09 : DaySolution, IDaySolutionImplementation
{
    [ExpectedResult(testResult: 1928, result: 6310675819476)]
    public override long RunPart1(bool isTest, string[] input, Action<OutputMessage> output)
    {
        var disk = input.ParseToDay09Data();
        if (isTest)
        {
            output(new("Disk Map", string.Join("", disk.DiskMap)));
            output(new("Blocks", disk.Blocks));
        }

        var compactedBlocks = disk.Blocks.GetCompactBlocks(isTest ? output : null);
        
        var checkSum = compactedBlocks.CalculateCheckSum();
        output(new("Check Sum", $"{checkSum:n0}"));

        return checkSum;
    }

    [ExpectedResult(testResult: 2858, result: 6335972980679)]
    public override long RunPart2(bool isTest, string[] input, Action<OutputMessage> output)
    {
        var disk = input.ParseToDay09Data();
        if (isTest)
        {
            output(new("Disk Map", string.Join("", disk.DiskMap)));
            output(new("Blocks", disk.Blocks));
        }

        var compactedBlocks = disk.Blocks.GetCompactButNotFragmentedBlocks(isTest ? output : null);

        var checkSum = compactedBlocks.CalculateCheckSum();
        output(new("Check Sum", $"{checkSum:n0}"));

        return checkSum;
    }
}