namespace AoC.Y24.Infrastructure;

public interface IDaySolutionImplementation : IDaySolutionDefinition
{
    public void Run(Action<OutputMessage> output, bool useTestInput);
}