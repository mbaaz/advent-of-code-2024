using MBZ.AdventOfCode.Core.Solvers;

namespace MBZ.AdventOfCode.Year2024.Day06;

// This is my solution to the Advent of Code challenge!
// <see>https://adventofcode.com/2024/day/6</see>
[DaySolution(Day = 6, IsActive = true)]
public class Day06() : DaySolution(day: 6), IDaySolutionImplementation
{
    [ExpectedResult(testResult: 41, result: 5269)]
    public override int RunPart1(bool isTest, string[] input, Action<OutputMessage> output)
    {
        var (map, guard) = input.ParseToDay06Data();
        map.SimulateGuardMovements(guard, out var totalMovements, out var isLooped);
        var visitedTiles = map.GetVisitedTilesCount();

        output(new("Total movements made", $"{totalMovements:n0}"));
        output(new("Is Guard path looped", isLooped ? "true" : "false"));
        output(new("Total of visited tiles", $"{visitedTiles:n0}"));
        return visitedTiles;
    }

    [ExpectedResult(testResult: 6, result: 1957)]
    public override int RunPart2(bool isTest, string[] input, Action<OutputMessage> output)
    {
        var (map, guard) = input.ParseToDay06Data();
        var obstructionPositions = map.CalculateHowManyPositionsForObstructionToPlaceGuardInLoop(guard, out var positionsChecked);

        output(new("Positions checked", $"{positionsChecked:n0}"));
        output(new("Number or positions for obstructions to loop the guard", $"{obstructionPositions:n0}"));
        return obstructionPositions;
    }
}