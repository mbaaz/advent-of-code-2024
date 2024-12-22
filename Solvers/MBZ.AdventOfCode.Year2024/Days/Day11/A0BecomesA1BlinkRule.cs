namespace MBZ.AdventOfCode.Year2024.Day11;

public class A0BecomesA1BlinkRule : IBlinkRule
{
    public bool IsMatch(long stoneEngraving) =>
        stoneEngraving == 0;

    public IEnumerable<long> Apply(long stoneEngraving) =>
        [1];
}