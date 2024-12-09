namespace MBZ.AdventOfCode.Core.Solvers;

public interface IDaySolutionImplementation : IDaySolutionDefinition
{
    public void Run(Action<OutputMessage> output, bool useTestInput);
}