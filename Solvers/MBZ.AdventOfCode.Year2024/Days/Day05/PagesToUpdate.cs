namespace MBZ.AdventOfCode.Year2024.Day05;

public class PagesToUpdate(byte[] pagesToUpdate)
{
    private byte[] Pages { get; } = pagesToUpdate;

    public bool ContainsPage(byte page) =>
        Pages.Contains(page);

    public byte[] GetPages() => Pages.ToArray(); // Return a copy
    public byte GetMiddlePage() => Pages[Pages.Length / 2];

    public bool SatisfiesRule(Rule rule)
    {
        var beforeIndex = Array.IndexOf(Pages, rule.PageBefore);
        var afterIndex = Array.IndexOf(Pages, rule.PageAfter);
        return afterIndex > beforeIndex;
    }

    public override string ToString()
    {
        var middleIndex = Pages.Length / 2;
        return $"[{string.Join(',', Pages[..middleIndex])},({Pages[middleIndex]}),{string.Join(',', Pages[(middleIndex + 1)..])}]";
    }
}