using MBZ.AdventOfCode.Core.Solvers;

namespace MBZ.AdventOfCode.Year2024.Day08;

// This is my solution to the Advent of Code challenge!
// <see>https://adventofcode.com/2024/day/8</see>
[DaySolution(Day = 8, IsActive = true)]
public class Day08 : DaySolution, IDaySolutionImplementation
{
    [ExpectedResult(testResult: 14, result: 313)]
    public override long RunPart1(bool isTest, string[] input, Action<OutputMessage> output)
    {
        var city = input.ParseToDay08Data();
        city.CalculateAntiNodes();
        var antiNodesCount = city.CountAntiNodes();

        output(new("Number of anti-nodes in the city", $"{antiNodesCount:n0}"));
        return antiNodesCount;
    }

    [ExpectedResult(testResult: 34, result: 1064)]
    public override long RunPart2(bool isTest, string[] input, Action<OutputMessage> output)
    {
        var city = input.ParseToDay08Data();
        city.CalculateAntiNodes(extended: true);
        var antiNodesCount = city.CountAntiNodes();

        output(new("Number of anti-nodes in the city", $"{antiNodesCount:n0}"));
        return antiNodesCount;
    }
}