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

        var correctUpdates = updates.Where(rules.IsValidUpdate).ToList();

        if(isTest)
        {
            foreach(var update in correctUpdates)
            {
                output(new("Correct update", $"{update}"));
            }
        }

        var result = correctUpdates.Sum(update => update.GetMiddlePage());
        output(new("Result", $"{result:n0}"));
    }

    public override void RunPart2(bool isTest, string[] input, Action<OutputMessage> output)
    {
        var (rules, updates) = ParseInput(input);
        var incorrectUpdates = updates.Where(rules.IsNotValidUpdate).Select(upd => new { Invalid = upd, Fixed = rules.FixIncorrectUpdate(upd) }).ToList();

        if(isTest)
        {
            foreach(var update in incorrectUpdates)
            {
                output(new("Incorrect update", $"{update.Invalid} ---> {update.Fixed}"));
            }
        }

        var result = incorrectUpdates.Sum(update => update.Fixed.GetMiddlePage());
        output(new("Result", $"{result:n0}"));
    }

    // ########################################################################################

    public class RuleSet
    {
        private readonly List<Rule> _rules = [];

        public int Count => _rules.Count;

        public bool IsValidUpdate(Update update) => _rules.All(rule => !rule.AffectsUpdate(update) || update.SatisfiesRule(rule));
        public bool IsNotValidUpdate(Update update) => _rules.Any(rule => rule.AffectsUpdate(update) && !update.SatisfiesRule(rule));

        public void AddRule(byte pageBefore, byte pageAfter) => _rules.Add(new Rule(pageBefore, pageAfter));

        public byte[] CalculateCorrectPageOrder(byte[] pages)
        {
            var allAvailablePages = pages.ToList();
            var correctList = new List<byte> { allAvailablePages[0] };
            for(var i=1;i<allAvailablePages.Count;i++)
            {
                var testPage = allAvailablePages[i];
                var correctPlaceFound = false;
                for(var j=0;j<=correctList.Count;j++)
                {
                    var testList = correctList.ToList(); // Make a copy
                    testList.Insert(j, testPage);

                    if (IsNotValidUpdate(new Update(testList.ToArray())))
                        continue;
                    
                    correctPlaceFound = true;
                    correctList = testList;
                    break;
                }

                if(!correctPlaceFound)
                {
                    throw new Exception("Could not find correct placement for page!");
                }
            }

            return correctList.ToArray();
        }

        public Update FixIncorrectUpdate(Update update)
        {
            var pages = update.GetPages();
            var correctPageOrder = CalculateCorrectPageOrder(pages);
            return new Update(correctPageOrder);
        }
    }

    public class Rule(byte pageBefore, byte pageAfter)
    {
        public byte PageBefore { get; } = pageBefore;
        public byte PageAfter { get; } = pageAfter;

        public bool AffectsUpdate(Update update) => 
            update.ContainsPage(PageBefore) && update.ContainsPage(PageAfter);

        public override string ToString() => $"{PageBefore}|{PageAfter}";
        
    }

    public class Update(byte[] pagesToUpdate)
    {
        private byte[] Pages { get; } = pagesToUpdate;

        public bool ContainsPage(byte page) => 
            Pages.Contains(page);

        public byte[] GetPages() => Pages.ToArray(); // Return a copy
        public byte GetMiddlePage() => Pages[Pages.Length/2];

        public bool SatisfiesRule(Rule rule)
        {
            var beforeIndex = Array.IndexOf(Pages, rule.PageBefore);
            var afterIndex = Array.IndexOf(Pages, rule.PageAfter);
            return afterIndex > beforeIndex;
        }

        public override string ToString()
        {
            var middleIndex = Pages.Length / 2;
            return $"[{string.Join(',', Pages[..middleIndex])},({Pages[middleIndex]}),{string.Join(',', Pages[(middleIndex+1)..])}]";
        }
    }

    private static readonly Regex ParseInputRegex = new Regex("^(?<OrderingRule>(?<PageBefore>[0-9]+)\\|(?<PageAfter>[0-9]+))|(?<Update>([0-9]+(,[0-9]+)*))$");

    public (RuleSet, List<Update> pageUpdates) ParseInput(string[] input)
    {
        var rules = new RuleSet();
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
                var pageBefore = byte.Parse(match.Groups["PageBefore"].Value);
                var pageAfter  = byte.Parse(match.Groups["PageAfter"].Value);
                rules.AddRule(pageBefore, pageAfter);
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