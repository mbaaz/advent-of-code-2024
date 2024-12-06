namespace MBZ.AdventOfCode.Year2024.Day03;

// This is my solution to the Advent of Code challenge!
// <see>https://adventofcode.com/2024/day/3</see>
[DaySolution(Day = 3, IsActive = true)]
public class Day03() : DaySolution(day: 3), IDaySolutionImplementation
{
    public override void RunPart1(bool isTest, string[] input, Action<OutputMessage> output)
    {
        var multiplyInstructions = input.GetMultiplyInstructions();
        var multiplicationSum = multiplyInstructions.ProductSum();

        output(new("Number of operations", $"{multiplyInstructions.Count:n0}"));
        output(new("Sum of multiplications", $"{multiplicationSum:n0}"));
    }

    public override void RunPart2(bool isTest, string[] input, Action<OutputMessage> output)
    {
        var instructions = input.GetEnhancedInstructions();
        var multiplicationSum = instructions.EnhancedProductSum();

        output(new("Number of operations", $"{instructions.Count:n0}"));
        output(new("Sum of enabled multiplications", $"{multiplicationSum:n0}"));
    }
}