namespace MBZ.AdventOfCode.Year2024.Day03;

public class MultiplyInstruction(int factor1, int factor2) : IInstruction
{
    public int Factor1 { get; } = factor1;
    public int Factor2 { get; } = factor2;

    public int GetProduct() => factor1 * factor2;
}