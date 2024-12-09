namespace MBZ.AdventOfCode.Core.Solvers;

public record PostPuzzleSolverRunResult(int Expected, int Result)
{
    public bool Success => Expected == Result;
}