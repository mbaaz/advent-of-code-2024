namespace MBZ.AdventOfCode.Year2024.Day11;

public class FallbackBlinkRule : IBlinkRule
{
    public bool IsMatch(long stoneEngraving) =>
        true;

    public IEnumerable<long> Apply(long stoneEngraving) =>
        [stoneEngraving * 2024];
}