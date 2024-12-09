using System.Reflection;

namespace MBZ.AdventOfCode.Core.Infrastructure;

public interface ITypeFinder
{
    IList<Assembly> GetAssemblies();

    IEnumerable<Type> FindClassesWithAttribute<TAttribute>(bool onlyConcreteClasses = true) where TAttribute : Attribute;
    IEnumerable<Type> FindClassesWithAttribute(Type attributeType, bool onlyConcreteClasses = true);
    IEnumerable<Type> FindClassesWithAttribute<TAttribute>(IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true) where TAttribute : Attribute;
    IEnumerable<Type> FindClassesWithAttribute(Type attributeType, IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true);

    IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, bool onlyConcreteClasses = true);
    IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true);
    IEnumerable<Type> FindClassesOfType<T>(bool onlyConcreteClasses = true);
    IEnumerable<Type> FindClassesOfType<T>(IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true);
    IEnumerable<Type> FindClassesOfType<T, TAssemblyAttribute>(bool onlyConcreteClasses = true) where TAssemblyAttribute : Attribute;

    IEnumerable<Assembly> FindAssembliesWithAttribute<T>();
    IEnumerable<Assembly> FindAssembliesWithAttribute<T>(IEnumerable<Assembly> assemblies);
    IEnumerable<Assembly> FindAssembliesWithAttribute<T>(DirectoryInfo assemblyPath);
}