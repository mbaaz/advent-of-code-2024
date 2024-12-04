namespace AoC.Y24.Infrastructure;

public interface IDaySolutionImplementation : IDaySolutionDefinition
{
    public void Run(Action<string> output, int outputWidth, bool useTestInput);
}