namespace MBZ.AdventOfCode.Year2024.Day11;

public class A0BecomesA1BlinkRule : IBlinkRule
{
    public bool IsMatch(Stone stone) =>
        stone.Engraving == 0;
    
    public IEnumerable<Stone> Apply(Stone stone) =>
        [new Stone(Engraving: 1)];
}