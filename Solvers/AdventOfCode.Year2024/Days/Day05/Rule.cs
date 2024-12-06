namespace MBZ.AdventOfCode.Year2024.Day05;

public class Rule(byte pageBefore, byte pageAfter)
{
    public byte PageBefore { get; } = pageBefore;
    public byte PageAfter { get; } = pageAfter;

    public bool AffectsUpdate(PagesToUpdate update) =>
        update.ContainsPage(PageBefore) && update.ContainsPage(PageAfter);

    public override string ToString() => $"{PageBefore}|{PageAfter}";

}