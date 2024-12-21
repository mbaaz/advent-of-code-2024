using System.Collections;
using MBZ.AdventOfCode.Year2024.Day11;

namespace MBZ.AdventOfCode.Year2024.Tests.Day11;

[TestFixture]
public class EvenNumberOfEngravedDigitsBlinkRuleTests
{
    public static IEnumerable IsMatchTestCases
    {
        get
        {
            yield return new TestCaseData(new Stone(22)).Returns(true);
            yield return new TestCaseData(new Stone(4736)).Returns(true);
            yield return new TestCaseData(new Stone(20241985)).Returns(true);
            yield return new TestCaseData(new Stone(123456)).Returns(true);
            yield return new TestCaseData(new Stone(4466)).Returns(true);

            yield return new TestCaseData(new Stone(7)).Returns(false);
            yield return new TestCaseData(new Stone(135)).Returns(false);
            yield return new TestCaseData(new Stone(49200)).Returns(false);
            yield return new TestCaseData(new Stone(777)).Returns(false);
            yield return new TestCaseData(new Stone(0)).Returns(false);
        }
    }

    [TestCaseSource(nameof(IsMatchTestCases))]
    public bool IsMatch_TrueForEvenDigits(Stone stone)
    {
        var rule = new EvenNumberOfEngravedDigitsBlinkRule();
        return rule.IsMatch(stone);
    }


    public static IEnumerable ApplyTestCases
    {
        get
        {
            yield return new TestCaseData(new Stone(22)).Returns(new List<Stone> { new(2), new(2) });
            yield return new TestCaseData(new Stone(4736)).Returns(new List<Stone> { new(47), new(36) });
            yield return new TestCaseData(new Stone(20241985)).Returns(new List<Stone> { new(2024), new(1985) });
            yield return new TestCaseData(new Stone(123456)).Returns(new List<Stone> { new(123), new(456) });
            yield return new TestCaseData(new Stone(4466)).Returns(new List<Stone> { new(44), new(66) });
            yield return new TestCaseData(new Stone(456789)).Returns(new List<Stone> { new(456), new(789) });
            yield return new TestCaseData(new Stone(1000)).Returns(new List<Stone> { new(10), new(0) });
        }
    }

    [TestCaseSource(nameof(ApplyTestCases))]
    public IEnumerable<Stone> Apply_SplitStonesCorrectly(Stone stone)
    {
        var rule = new EvenNumberOfEngravedDigitsBlinkRule();
        return rule.Apply(stone);
    }
}