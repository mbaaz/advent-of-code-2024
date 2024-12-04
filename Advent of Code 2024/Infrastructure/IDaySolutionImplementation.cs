namespace AoC.Y24.Infrastructure;

public interface IDaySolutionImplementation : IDaySolutionDefinition
{
    public void Run(Action<string> outputDelegate, bool useTestInput);
}