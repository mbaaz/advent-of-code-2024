using System.Reflection;

namespace MBZ.AdventOfCode.Core.Infrastructure;

internal record AssemblyWrapper
{
    public Assembly Assembly { get; }
    public int Depth { get; }

    public AssemblyWrapper(Assembly assembly, int depth)
    {
        Assembly = assembly;
        Depth = depth;
    }

    public AssemblyWrapper(AssemblyName assemblyName, int depth)
    {
        Assembly = Assembly.Load(assemblyName);
        Depth = depth;
    }
}