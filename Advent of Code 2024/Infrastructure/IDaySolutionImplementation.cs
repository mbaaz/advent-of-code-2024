namespace AoC.Y24.Infrastructure;

public interface IDaySolutionImplementation : IDaySolutionDefinition
{
    public bool IsActive { get; }
    public void Run(Action<string> outputDelegate);
}