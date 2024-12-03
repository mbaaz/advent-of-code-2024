namespace AoC.Y24.Infrastructure;

public interface IDaySolutionImplementation : IDaySolutionDefinition
{
    public bool IsActive { get; }
    public void Run(Action<string> outputDelegate);
    public void RunPart1(string[] input, Action<string> output);
    public void RunPart2(string[] input, Action<string> output);
}