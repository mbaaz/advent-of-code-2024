using System.Text.RegularExpressions;

namespace AoC.Y24.days;

// This is my solution to the Advent of Code challenge!
// <see>https://adventofcode.com/2024/day/3</see>
public class Day03() : DaySolution(day: 3), IDaySolutionImplementation
{
    public bool IsActive => true;

    public override void Run(Action<string> output)
    {
        UseTestFile = true;

        base.Run(output);
    }

    public override void RunPart1(string[] input, Action<string> output)
    {
        RunWithTimer(output, () =>
        {
            var multiplyOperations = input.SelectMany(GetMultiplyInstructions).ToList();

            var multiplyOperationSum = multiplyOperations.Sum(tuple => tuple.Item1 * tuple.Item2);

            output($"""
PART 1
                     input: {input[0]}
      number of operations: {multiplyOperations.Count:n0}
    sum of multiplications: {multiplyOperationSum:n0}
""");
        });
    }

    public override void RunPart2(string[] input, Action<string> output)
    {
        RunWithTimer(output, () =>
        {

            output($"""
PART 2
     input: {input[0]}
    result: [not yet defined!] 
""");
        });
    }

    // ########################################################################################

    private static readonly Regex MultiplyInstructionRegex = new(@"mul\((?<Factor1>[0-9]+),(?<Factor2>[0-9]+)\)");

    private IEnumerable<(int, int)> GetMultiplyInstructions(string input)
    {
        var matches = MultiplyInstructionRegex.Matches(input);
        foreach(Match match in matches)
        {
            var factor1 = int.Parse(match.Groups["Factor1"].Value);
            var factor2 = int.Parse(match.Groups["Factor2"].Value);
            yield return (factor1, factor2);
        }
    }

    private static readonly Regex EnhancedMultiplyInstructionRegex = new(@"mul\((?<Factor1>[0-9]+),(?<Factor2>[0-9]+)\)");

    private IEnumerable<(int, int)> GetEnhancedMultiplyInstructions(string input)
    {
        var matches = MultiplyInstructionRegex.Matches(input);
        foreach (Match match in matches)
        {
            var factor1 = int.Parse(match.Groups["Factor1"].Value);
            var factor2 = int.Parse(match.Groups["Factor2"].Value);
            yield return (factor1, factor2);
        }
    }
}