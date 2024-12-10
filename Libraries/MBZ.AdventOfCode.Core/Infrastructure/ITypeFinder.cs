using System.Reflection;

namespace MBZ.AdventOfCode.Core.Infrastructure;

public interface ITypeFinder
{
    IEnumerable<Assembly> GetAssemblies();

    IEnumerable<Type> FindClassesOfType<T>(bool onlyConcreteClasses = true);
    IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, bool onlyConcreteClasses = true);
    IEnumerable<Type> FindClassesOfType<T>(IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true);
    IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true);
}