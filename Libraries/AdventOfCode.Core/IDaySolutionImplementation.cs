namespace MBZ.AdventOfCode.Core;

public interface IDaySolutionImplementation : IDaySolutionDefinition
{
    public void Run(Action<OutputMessage> output, bool useTestInput);
}