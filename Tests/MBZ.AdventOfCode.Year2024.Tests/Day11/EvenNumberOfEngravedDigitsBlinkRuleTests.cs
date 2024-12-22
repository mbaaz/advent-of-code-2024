using System.Collections;
using System.Diagnostics;
using MBZ.AdventOfCode.Year2024.Day11;

namespace MBZ.AdventOfCode.Year2024.Tests.Day11;

[TestFixture]
public class EvenNumberOfEngravedDigitsBlinkRuleTests
{
    public static IEnumerable IsMatchTestCases
    {
        get
        {
            yield return new TestCaseData(22).Returns(true);
            yield return new TestCaseData(4736).Returns(true);
            yield return new TestCaseData(20241985).Returns(true);
            yield return new TestCaseData(123456).Returns(true);
            yield return new TestCaseData(4466).Returns(true);

            yield return new TestCaseData(7).Returns(false);
            yield return new TestCaseData(135).Returns(false);
            yield return new TestCaseData(49200).Returns(false);
            yield return new TestCaseData(777).Returns(false);
            yield return new TestCaseData(0).Returns(false);
        }
    }

    [TestCaseSource(nameof(IsMatchTestCases))]
    public bool IsMatch_TrueForEvenDigits(long stoneEngraving)
    {
        var rule = new EvenNumberOfEngravedDigitsBlinkRule();
        return rule.IsMatch(stoneEngraving);
    }


    public static IEnumerable ApplyTestCases
    {
        get
        {
            yield return new TestCaseData(22).Returns(new List<long> { 2, 2 });
            yield return new TestCaseData(4736).Returns(new List<long> { 47, 36 });
            yield return new TestCaseData(20241985).Returns(new List<long> { 2024, 1985 });
            yield return new TestCaseData(123456).Returns(new List<long> { 123, 456 });
            yield return new TestCaseData(4466).Returns(new List<long> { 44, 66 });
            yield return new TestCaseData(456789).Returns(new List<long> { 456, 789 });
            yield return new TestCaseData(1000).Returns(new List<long> { 10, 0 });
        }
    }

    [TestCaseSource(nameof(ApplyTestCases))]
    public IEnumerable<long> Apply_SplitStonesCorrectly(long stoneEngraving)
    {
        var rule = new EvenNumberOfEngravedDigitsBlinkRule();
        return rule.Apply(stoneEngraving);
    }

    [TestCase]
    public void Apply_1M_Timing()
    {
        var rule = new EvenNumberOfEngravedDigitsBlinkRule();
        var timer = new Stopwatch();
        timer.Start();
        for (var i = 0; i < 1_000_000; i++)
        {
            var test = rule.Apply(123456);
        }
        timer.Stop();

        Console.WriteLine($"Elapsed: {timer.ElapsedMilliseconds}");
        Assert.That(timer.ElapsedMilliseconds, Is.LessThanOrEqualTo(100));
    }
}
