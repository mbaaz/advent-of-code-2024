using System.Numerics;
using MBZ.AdventOfCode.Core.Solvers;

namespace MBZ.AdventOfCode.Year2024.Day11;

// This is my solution to the Advent of Code challenge!
// <see>https://adventofcode.com/2024/day/11</see>
[DaySolution(Day = 11, IsActive = true)]
public class Day11 : DaySolution, IDaySolutionImplementation
{
    [ExpectedResult(testResult: 55312, result: 224529)]
    public override long RunPart1(bool isTest, string[] input, Action<OutputMessage> output)
    {
        var stones = input.ParseToDay11Data();
        var result = stones.BlinkAndReportNumberOfStones(25, output);

        output(new("Number of Stones", $"{result:n0}"));
        return (long)result;
    }

    [ExpectedResult(testResult: 65601038650482, result: 266820198587914)]
    public override long RunPart2(bool isTest, string[] input, Action<OutputMessage> output)
    {
        var stones = input.ParseToDay11Data();
        var result = stones.BlinkAndReportNumberOfStones(75, output);

        output(new("Number of Stones", $"{result:n0}"));
        return (long)result;
    }
}

internal static class Day11Extensions
{
    public static StoneRow ParseToDay11Data(this string[] input)
    {
        var stones = input
            .SelectMany(line => line
                .Split(" ")
                .Select(int.Parse)
            )
            .Select(s => new Stone(s))
        ;
        var stoneRow = new StoneRow(stones);
        return stoneRow;
    }
}