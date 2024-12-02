namespace AoC.Y24.Infrastructure;

public interface IDaySolutionDefinition
{
    public int Day { get; }
    public bool UseTestFile { get; }
}