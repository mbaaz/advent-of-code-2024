namespace MBZ.AdventOfCode.Year2024.Day11;

public class FallbackBlinkRule : IBlinkRule
{
    public bool IsMatch(Stone stone) =>
        true;

    public IEnumerable<Stone> Apply(Stone stone) =>
        [new Stone(stone.Engraving * 2024)];
}