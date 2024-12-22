namespace MBZ.AdventOfCode.Year2024.Day11;

public record Stone(long Engraving)
{
    public CompositeStone ToCompositeStone() =>
        new(Engraving: Engraving, Count: 1);
}