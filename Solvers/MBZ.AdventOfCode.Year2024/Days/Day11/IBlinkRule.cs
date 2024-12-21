namespace MBZ.AdventOfCode.Year2024.Day11;

public interface IBlinkRule
{
    bool IsMatch(Stone stone);
    IEnumerable<Stone> Apply(Stone stone);
}