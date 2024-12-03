using System.Reflection;

namespace AoC.Y24.Infrastructure;

public class SolverHelper
{
    public IDictionary<int, IDaySolutionImplementation?> GetSolvers()
    {
        var types = GetSolverTypes();
        var solvers = types
            .Select(t => Activator.CreateInstance(t) as IDaySolutionImplementation)
            .Where(s =>
                s != null &&
                s.IsActive
            )
            .ToDictionary(s => s!.Day)
        ;
        return solvers;
    }

    private IEnumerable<Type> GetSolverTypes()
    {
        var solverType = typeof(IDaySolutionImplementation);
        var types = Assembly.GetExecutingAssembly().GetTypes()
            .Where(type =>
                solverType.IsAssignableFrom(type) &&
                !type.IsInterface &&
                type.IsClass &&
                !type.IsAbstract
            )
            .ToList()
        ;
        return types;
    }
}