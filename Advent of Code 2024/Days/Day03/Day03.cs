using System.Text.RegularExpressions;

namespace AoC.Y24.days;

// This is my solution to the Advent of Code challenge!
// <see>https://adventofcode.com/2024/day/3</see>
[DaySolution(Day = 3, IsActive = true)]
public class Day03() : DaySolution(day: 3), IDaySolutionImplementation
{
    public override void RunPart1(string[] input, Action<OutputMessage> output)
    {
        var multiplyInstructions = input.SelectMany(GetMultiplyInstructions).ToList();
        var multiplicationSum = multiplyInstructions.Sum(instruction => instruction.GetProduct());

        output(new("Number of operations", $"{multiplyInstructions.Count:n0}"));
        output(new("Sum of multiplications", $"{multiplicationSum:n0}"));
    }

    public override void RunPart2(string[] input, Action<OutputMessage> output)
    {
        var instructions = input.SelectMany(GetEnhancedInstructions).ToList();

        var mulEnabled = true;
        var multiplicationSum = 0;
        foreach(var instruction in instructions)
        {
            if(instruction is DoInstruction)
            {
                mulEnabled = true;
                continue;
            }

            if (instruction is DontInstruction)
            {
                mulEnabled = false;
                continue;
            }

            if(mulEnabled && instruction is MultiplyInstruction multiplyInstruction)
            {
                multiplicationSum += multiplyInstruction.GetProduct();
            }
        }

        output(new("Number of operations", $"{instructions.Count:n0}"));
        output(new("Sum of enabled multiplications", $"{multiplicationSum:n0}"));
    }

    // ########################################################################################

    private static readonly Regex MultiplyInstructionRegex = new(@"mul\((?<Factor1>[0-9]+),(?<Factor2>[0-9]+)\)");

    private IEnumerable<MultiplyInstruction> GetMultiplyInstructions(string input)
    {
        var matches = MultiplyInstructionRegex.Matches(input);
        foreach(Match match in matches)
        {
            var factor1 = int.Parse(match.Groups["Factor1"].Value);
            var factor2 = int.Parse(match.Groups["Factor2"].Value);
            yield return new MultiplyInstruction(factor1, factor2);
        }
    }

    private static readonly Regex EnhancedMultiplyInstructionRegex = new(@"(?<Multiplication>mul\((?<Factor1>[0-9]+),(?<Factor2>[0-9]+)\))|(?<Do>do\(\))|(?<Dont>don\'t\(\))");

    private IEnumerable<IInstruction> GetEnhancedInstructions(string input)
    {
        var matches = EnhancedMultiplyInstructionRegex.Matches(input);
        foreach (Match match in matches)
        {
            if (match.Groups["Multiplication"].Success)
            {
                var factor1 = int.Parse(match.Groups["Factor1"].Value);
                var factor2 = int.Parse(match.Groups["Factor2"].Value);
                yield return new MultiplyInstruction(factor1, factor2);
                continue;
            }

            if(match.Groups["Do"].Success)
            {
                yield return new DoInstruction();
                continue;
            }

            if (match.Groups["Dont"].Success)
            {
                yield return new DontInstruction();
                continue;
            }


            throw new Exception("Unknown instruction has been encountered!");
        }
    }

    public abstract class IInstruction { }

    public class DoInstruction : IInstruction
    {
        
    }

    public class DontInstruction : IInstruction
    {
        
    }

    public class MultiplyInstruction(int factor1, int factor2) : IInstruction
    {
        public int Factor1 { get; } = factor1;
        public int Factor2 { get; } = factor2;

        public int GetProduct() => factor1 * factor2;
    }
}
