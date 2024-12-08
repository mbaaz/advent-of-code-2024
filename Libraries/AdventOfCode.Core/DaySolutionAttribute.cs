namespace MBZ.AdventOfCode.Core;

[AttributeUsage(AttributeTargets.Class)]
public class DaySolutionAttribute : Attribute
{
    public int Day { get; set; }
    public bool IsActive { get; set; } = false;
}