namespace MBZ.AdventOfCode.Year2024.Day11;

public class EvenNumberOfEngravedDigitsBlinkRule : IBlinkRule
{
    public bool IsMatch(Stone stone) =>
        stone.Engraving.ToString().Length % 2 == 0;

    public IEnumerable<Stone> Apply(Stone stone)
    {
        var engraving = stone.Engraving.ToString();
        yield return new Stone(Engraving: int.Parse(engraving[..(engraving.Length / 2)]));
        yield return new Stone(Engraving: int.Parse(engraving[(engraving.Length / 2)..]));
    }
}