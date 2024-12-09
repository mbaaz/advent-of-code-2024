using System.Text.RegularExpressions;

namespace MBZ.AdventOfCode.Year2024.Day05;

public static class Day05Extensions
{
    private static readonly Regex ParseInputRegex = new("^(?<OrderingRule>(?<PageBefore>[0-9]+)\\|(?<PageAfter>[0-9]+))|(?<Update>([0-9]+(,[0-9]+)*))$");

    public static (RuleSet, List<PagesToUpdate> pageUpdates) ParseToDay05Data(this string[] input)
    {
        var rules = new RuleSet();
        var pageUpdates = new List<PagesToUpdate>();

        foreach (var line in input)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            var match = ParseInputRegex.Match(line);
            if (!match.Success)
            {
                throw new Exception($"""Failed to parse input line "{line}"!""");
            }

            if (match.Groups["OrderingRule"].Success)
            {
                var pageBefore = byte.Parse(match.Groups["PageBefore"].Value);
                var pageAfter = byte.Parse(match.Groups["PageAfter"].Value);
                rules.AddRule(pageBefore, pageAfter);
                continue;
            }

            if (match.Groups["Update"].Success)
            {
                var pages = match.Groups["Update"].Value
                        .Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                        .Select(byte.Parse)
                        .ToArray()
                    ;
                var update = new PagesToUpdate(pages);
                pageUpdates.Add(update);
                continue;
            }

            throw new Exception("Ehm... Im not really sure how come this code got executed!");
        }

        return (rules, pageUpdates);
    }
}