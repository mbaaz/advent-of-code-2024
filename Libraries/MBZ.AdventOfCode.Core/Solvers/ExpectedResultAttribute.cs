namespace MBZ.AdventOfCode.Core.Solvers;

[AttributeUsage(AttributeTargets.Method)]
public class ExpectedResultAttribute(int testResult, int result) : Attribute
{
    public int TestResult { get; } = testResult;
    public int Result { get; } = result;
}