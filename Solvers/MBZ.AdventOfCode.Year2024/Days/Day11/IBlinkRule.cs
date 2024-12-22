namespace MBZ.AdventOfCode.Year2024.Day11;

public interface IBlinkRule
{
    bool IsMatch(long stoneEngraving);
    IEnumerable<long> Apply(long stoneEngraving);
}