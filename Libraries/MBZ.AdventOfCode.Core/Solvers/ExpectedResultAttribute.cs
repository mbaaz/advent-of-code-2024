namespace MBZ.AdventOfCode.Core.Solvers;

[AttributeUsage(AttributeTargets.Method)]
public class ExpectedResultAttribute(long testResult, long result) : Attribute
{
    public long TestResult { get; } = testResult;
    public long Result { get; } = result;
}