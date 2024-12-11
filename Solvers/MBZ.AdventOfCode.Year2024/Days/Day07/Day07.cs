using MBZ.AdventOfCode.Core.Solvers;

namespace MBZ.AdventOfCode.Year2024.Day07;

// This is my solution to the Advent of Code challenge!
// <see>https://adventofcode.com/2024/day/7</see>
[DaySolution(Day = 7, IsActive = true)]
public class Day07 : DaySolution, IDaySolutionImplementation
{
    [ExpectedResult(testResult: 3749, result: 465126289353)]
    public override long RunPart1(bool isTest, string[] input, Action<OutputMessage> output)
    {
        var calibrationService = input.ParseToDay07Data();
        var validEquations = calibrationService.GetValidEquations();
        var sumOfValidEquations = validEquations.Sum(eq => eq.Result);

        output(new("Valid Equations", $"{validEquations.Count:n0}"));
        output(new("Sum of valid equations", $"{sumOfValidEquations:n0}"));

        return sumOfValidEquations;
    }

    [ExpectedResult(testResult: 11387, result: 70597497486371)]
    public override long RunPart2(bool isTest, string[] input, Action<OutputMessage> output)
    {
        var calibrationService = input.ParseToDay07Data();
        var validEquations = calibrationService.GetValidEquations(withConcatenation: true);
        var sumOfValidEquations = validEquations.Sum(eq => eq.Result);

        output(new("Valid Equations", $"{validEquations.Count:n0}"));
        output(new("Sum of valid equations", $"{sumOfValidEquations:n0}"));

        return sumOfValidEquations;
    }
}

public class CalibrationService
{
    private List<Equation> Equations { get; }

    public CalibrationService(List<Equation> equations)
    {
        Equations = equations;
    }

    public List<Equation> GetValidEquations(bool withConcatenation = false)
    {
        var operators = new List<Func<long, long, long>>
        {
            OperatorPlus,
            OperatorMultiply
        };

        if (withConcatenation)
        {
            operators.Add(OperatorConcatenation);
        }

        var validEquations = Equations
            .Where(equation => IsValidEquation(equation, operators))
            .ToList()
        ;
        return validEquations;
    }

    private bool IsValidEquation(Equation equation, List<Func<long, long, long>> operators)
    {
        var operations = equation.Numbers.Length - 1;
        var operationPermutations = MakeOperatorPermutations(operators, operations);

        foreach (var permutation in operationPermutations)
        {
            long result = equation.Numbers.First();
            for (var i = 0; i < operations; i++)
            {
                result = permutation[i](result, equation.Numbers[i + 1]);
            }

            if(result == equation.Result)
                return true;
        }

        return false;
    }

    private List<List<Func<long, long, long>>> MakeOperatorPermutations(List<Func<long, long, long>> operators, int permutationLength)
    {
        var operationPermutations = operators
            .Select(@operator => (List<Func<long, long, long>>) [@operator])
            .ToList()
        ;
        for (var i = 1; i < permutationLength; i++)
        {
            var newPermutationList = new List<List<Func<long, long, long>>>();
            foreach (var @operator in operators)
            {
                foreach (var permutation in operationPermutations)
                {
                    var newPermutation = permutation.ToList();
                    newPermutation.Add(@operator);
                    newPermutationList.Add(newPermutation);
                }
            }

            operationPermutations = newPermutationList;
        }

        return operationPermutations;
    }

    private static long OperatorPlus(long number1, long number2) =>
        number1 + number2;

    private static long OperatorMultiply(long number1, long number2) =>
        number1 * number2;

    private static long OperatorConcatenation(long number1, long number2) =>
        (long)(Math.Pow(10, number2.ToString().Length) * number1) + number2;
}