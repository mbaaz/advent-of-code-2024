namespace MBZ.AdventOfCode.Core.Solvers;

public interface IDaySolutionImplementation : IDaySolutionDefinition
{
    Task Run(Action<OutputMessage> output, bool useTestInput);
}