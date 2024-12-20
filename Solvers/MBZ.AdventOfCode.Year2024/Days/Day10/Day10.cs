using MBZ.AdventOfCode.Core.Solvers;

namespace MBZ.AdventOfCode.Year2024.Day10;

// This is my solution to the Advent of Code challenge!
// <see>https://adventofcode.com/2024/day/10</see>
[DaySolution(Day = 10, IsActive = true)]
public class Day10 : DaySolution, IDaySolutionImplementation
{
    [ExpectedResult(testResult: 36, result: 574)]
    public override long RunPart1(bool isTest, string[] input, Action<OutputMessage> output)
    {
        var data = input.ParseToDay10Data();
        var score = data.CalculateScoreForTrailHeads();

        output(new("Score sum for all trail heads", $"{score:n0}"));
        return score;
    }

    [ExpectedResult(testResult: 81, result: 1238)]
    public override long RunPart2(bool isTest, string[] input, Action<OutputMessage> output)
    {
        var data = input.ParseToDay10Data();
        var rating = data.CalculateRatingForTrailHeads();

        output(new("Rating sum for all trail heads", $"{rating:n0}"));
        return rating;
    }
}