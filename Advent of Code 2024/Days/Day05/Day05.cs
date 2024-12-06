using System.Text.RegularExpressions;

namespace AoC.Y24.days;

// This is my solution to the Advent of Code challenge!
// <see>https://adventofcode.com/2024/day/5</see>
[DaySolution(Day = 5, IsActive = true)]
public class Day05() : DaySolution(day: 5), IDaySolutionImplementation
{
    public override void RunPart1(bool isTest, string[] input, Action<OutputMessage> output)
    {
        var (rules, updates) = ParseInput(input);

        output(new("Number of rules", $"{rules.Count:n0}"));
        output(new("Number of updates", $"{updates.Count:n0}"));

        var correctUpdates = updates
            .Where(update => rules.All(rule => !rule.AffectsUpdate(update) || update.SatisfiesRule(rule)))
            .ToList()
        ;

        if(isTest)
        {
            foreach(var update in correctUpdates)
            {
                output(new($"Correct update", $"{update} ({update.GetMiddlePage()})"));
            }
        }

        var result = correctUpdates.Sum(update => update.GetMiddlePage());
        output(new("Result", result));
    }

    public override void RunPart2(bool isTest, string[] input, Action<OutputMessage> output)
    {
        output(new("Result", "[not yet defined]"));
    }

    // ########################################################################################

    public class PageOrderingRule(byte pageBefore, byte pageAfter)
    {
        public byte PageBefore { get; } = pageBefore;
        public byte PageAfter { get; } = pageAfter;

        public bool AffectsUpdate(Update update) => 
            update.ContainsPage(PageBefore) && update.ContainsPage(PageAfter);
    }

    public class Update(byte[] pagesToUpdate)
    {
        private byte[] Pages { get; } = pagesToUpdate;

        public bool ContainsPage(byte page) => 
            Pages.Contains(page);

        public byte GetMiddlePage() => Pages[Pages.Length/2];

        public bool SatisfiesRule(PageOrderingRule rule)
        {
            var beforeIndex = Array.IndexOf(Pages, rule.PageBefore);
            var afterIndex = Array.IndexOf(Pages, rule.PageAfter);
            return afterIndex > beforeIndex;
        }

        public override string ToString()
        {
            return string.Join(',', Pages);
        }
    }

    private static readonly Regex ParseInputRegex = new Regex("^(?<OrderingRule>(?<Rule1>[0-9]+)\\|(?<Rule2>[0-9]+))|(?<Update>([0-9]+(,[0-9]+)*))$");

    public (List<PageOrderingRule>, List<Update> pageUpdates) ParseInput(string[] input)
    {
        var rules = new List<PageOrderingRule>();
        var pageUpdates = new List<Update>();

        foreach(var line in input)
        {
            if(string.IsNullOrWhiteSpace(line))
                continue;

            var match = ParseInputRegex.Match(line);
            if(!match.Success)
            {
                throw new Exception($"""Failed to parse input line "{line}"!""");
            }

            if(match.Groups["OrderingRule"].Success)
            {
                var firstPage  = byte.Parse(match.Groups["Rule1"].Value);
                var secondPage = byte.Parse(match.Groups["Rule2"].Value);
                var rule = new PageOrderingRule(firstPage, secondPage);
                rules.Add(rule);
                continue;
            }

            if(match.Groups["Update"].Success)
            {
                var pages = match.Groups["Update"].Value
                    .Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                    .Select(byte.Parse)
                    .ToArray()
                ;
                var update = new Update(pages);
                pageUpdates.Add(update);
                continue;
            }

            throw new Exception("Ehm... Im not really sure how come this code got executed!");
        }

        return (rules, pageUpdates);
    }
}