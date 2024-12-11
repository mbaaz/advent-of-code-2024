namespace MBZ.AdventOfCode.Year2024.Day07;

public record Equation(long Result, long[] Numbers)
{
    public override string ToString() =>
        $"{Result}: {string.Join(" ", Numbers)}";
}