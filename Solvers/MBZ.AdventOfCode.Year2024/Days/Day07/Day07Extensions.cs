using System.Text.RegularExpressions;

namespace MBZ.AdventOfCode.Year2024.Day07;

internal static partial class Day07Extensions
{
    private const string EQUATION_REGEX_PATTERN = @"(?<Result>[0-9]+)\:(\ (?<Number>[0-9]+))+";

    [GeneratedRegex(EQUATION_REGEX_PATTERN, RegexOptions.Singleline)]
    private static partial Regex ParseInputEquationRegex();

    public static CalibrationService ParseToDay07Data(this string[] input)
    {
        var equations = input
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .Select(ParseInputLine)
            .ToList()
        ;
        var calibrationService = new CalibrationService(equations);
        return calibrationService;
    }

    private static Equation ParseInputLine(string line)
    {
        var match = ParseInputEquationRegex().Match(line);

        if (!match.Success)
            throw new Exception("What is this - something wrong with the regex?!");

        var resultGroup = match.Groups["Result"];
        if (!resultGroup.Success)
            throw new Exception("Could not find Result part of input line");

        if (!long.TryParse(resultGroup.Value, out var result))
            throw new Exception("Could not parse result from input line");

        var numberGroup = match.Groups["Number"];
        if (!numberGroup.Success)
            throw new Exception("Could not find any number!");

        var numbers = new long[numberGroup.Captures.Count];
        for (var i = 0; i < numberGroup.Captures.Count; i++)
        {
            if (!long.TryParse(numberGroup.Captures[i].Value, out var numberTemp))
                throw new Exception("Something wrong has happened with number!");
            numbers[i] = numberTemp;
        }

        return new Equation(result, numbers);
    }
}