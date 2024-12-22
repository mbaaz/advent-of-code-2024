namespace MBZ.AdventOfCode.Year2024.Day11;

public class EvenNumberOfEngravedDigitsBlinkRule : IBlinkRule
{
    public bool IsMatch(long stoneEngraving) =>
        stoneEngraving.ToString().Length % 2 == 0;

    public IEnumerable<long> Apply(long stoneEngraving)
    {
        var engraving = stoneEngraving.ToString();
        yield return long.Parse(engraving[..(engraving.Length / 2)]);
        yield return long.Parse(engraving[(engraving.Length / 2)..]);
    }
}