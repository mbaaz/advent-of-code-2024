namespace MBZ.AdventOfCode.Year2024.Day05;

public class RuleSet
{
    private readonly List<Rule> _rules = [];

    public int Count => _rules.Count;

    public bool IsValidUpdate(PagesToUpdate update) => _rules.All(rule => !rule.AffectsUpdate(update) || update.SatisfiesRule(rule));
    public bool IsNotValidUpdate(PagesToUpdate update) => _rules.Any(rule => rule.AffectsUpdate(update) && !update.SatisfiesRule(rule));

    public void AddRule(byte pageBefore, byte pageAfter) => _rules.Add(new Rule(pageBefore, pageAfter));

    public byte[] CalculateCorrectPageOrder(byte[] pages)
    {
        var allAvailablePages = pages.ToList();
        var correctList = new List<byte> { allAvailablePages[0] };
        for (var i = 1; i < allAvailablePages.Count; i++)
        {
            var testPage = allAvailablePages[i];
            var correctPlaceFound = false;
            for (var j = 0; j <= correctList.Count; j++)
            {
                var testList = correctList.ToList(); // Make a copy
                testList.Insert(j, testPage);

                if (IsNotValidUpdate(new PagesToUpdate(testList.ToArray())))
                    continue;

                correctPlaceFound = true;
                correctList = testList;
                break;
            }

            if (!correctPlaceFound)
            {
                throw new Exception("Could not find correct placement for page!");
            }
        }

        return correctList.ToArray();
    }

    public PagesToUpdate FixIncorrectUpdate(PagesToUpdate update)
    {
        var pages = update.GetPages();
        var correctPageOrder = CalculateCorrectPageOrder(pages);
        return new PagesToUpdate(correctPageOrder);
    }
}