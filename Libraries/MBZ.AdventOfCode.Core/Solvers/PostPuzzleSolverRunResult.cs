namespace MBZ.AdventOfCode.Core.Solvers;

public record PostPuzzleSolverRunResult(long Expected, long Result)
{
    public bool Success => Expected == Result;
}