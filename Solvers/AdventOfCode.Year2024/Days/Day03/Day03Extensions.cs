using System.Text.RegularExpressions;

namespace MBZ.AdventOfCode.Year2024.Day03;

public static class Day03Extensions
{
    private static readonly Regex MultiplyInstructionRegex = new(@"mul\((?<Factor1>[0-9]+),(?<Factor2>[0-9]+)\)");

    public static List<MultiplyInstruction> GetMultiplyInstructions(this string[] input)
    {
        var multiplyInstructions = input
            .SelectMany(GetMultiplyInstructions)
            .ToList()
        ;
        return multiplyInstructions;
    }

    private static IEnumerable<MultiplyInstruction> GetMultiplyInstructions(string input)
    {
        var matches = MultiplyInstructionRegex.Matches(input);
        foreach (Match match in matches)
        {
            var factor1 = int.Parse(match.Groups["Factor1"].Value);
            var factor2 = int.Parse(match.Groups["Factor2"].Value);
            yield return new MultiplyInstruction(factor1, factor2);
        }
    }

    private static readonly Regex EnhancedMultiplyInstructionRegex = new(@"(?<Multiplication>mul\((?<Factor1>[0-9]+),(?<Factor2>[0-9]+)\))|(?<Do>do\(\))|(?<Dont>don\'t\(\))");

    public static List<IInstruction> GetEnhancedInstructions(this string[] input)
    {
        var instructions = input
            .SelectMany(GetEnhancedInstructions)
            .ToList()
        ;
        return instructions;
    }

    private static IEnumerable<IInstruction> GetEnhancedInstructions(string input)
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

            if (match.Groups["Do"].Success)
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

    public static int ProductSum(this List<MultiplyInstruction> multiplyInstructions)
    {
        var multiplicationSum = multiplyInstructions
            .Sum(instruction => instruction.GetProduct())
        ;
        return multiplicationSum;
    }

    public static int EnhancedProductSum(this List<IInstruction> instructions)
    {
        var mulEnabled = true;
        var multiplicationSum = 0;
        foreach (var instruction in instructions)
        {
            switch (instruction)
            {
                case DoInstruction:
                    mulEnabled = true;
                    continue;
                case DontInstruction:
                    mulEnabled = false;
                    continue;
            }

            if (mulEnabled && instruction is MultiplyInstruction multiplyInstruction)
            {
                multiplicationSum += multiplyInstruction.GetProduct();
            }
        }

        return multiplicationSum;
    }
}