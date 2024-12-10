using System.Reflection;
using System.Text.RegularExpressions;

namespace MBZ.AdventOfCode.Core.Infrastructure;

public partial class AppTypeFinder : ITypeFinder
{
    private const string MATCHING_ASSEMBLY_REGEX_PATTERN = @"^MBZ\.AdventOfCode\..*$";

    [GeneratedRegex(MATCHING_ASSEMBLY_REGEX_PATTERN, RegexOptions.Singleline)]
    private static partial Regex MatchingAssemblyRegex();

    private IEnumerable<Assembly>? _assemblies;
    public IEnumerable<Assembly> GetAssemblies() => _assemblies ??= LoadAssemblies();

    #region ITypeFinder

    public IEnumerable<Type> FindClassesOfType<T>(bool onlyConcreteClasses = true)
    {
        return FindClassesOfType(typeof(T), onlyConcreteClasses);
    }

    public IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, bool onlyConcreteClasses = true)
    {
        return FindClassesOfType(assignTypeFrom, GetAssemblies(), onlyConcreteClasses);
    }

    public IEnumerable<Type> FindClassesOfType<T>(IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true)
    {
        return FindClassesOfType(typeof(T), assemblies, onlyConcreteClasses);
    }

    public IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true)
    {
        var result = new List<Type>();
        try
        {
            var types = assemblies.SelectMany(assembly => assembly.GetTypes());
            var matchingTypes = types
                .Where(type => assignTypeFrom.IsAssignableFrom(type) || (assignTypeFrom.IsGenericTypeDefinition && DoesTypeImplementOpenGeneric(type, assignTypeFrom)))
                .Where(type => !type.IsInterface)
                .Where(type => !onlyConcreteClasses || type is { IsClass: true, IsAbstract: false })
            ;
            result.AddRange(matchingTypes);
        }
        catch (ReflectionTypeLoadException ex)
        {
            var msg = ex.LoaderExceptions.Aggregate(string.Empty, (current, e) => current + (e.Message + Environment.NewLine));
            var fail = new Exception(msg, ex);

            throw fail;
        }
        return result;
    }

    protected virtual bool DoesTypeImplementOpenGeneric(Type type, Type openGeneric)
    {
        try
        {
            var genericTypeDefinition = openGeneric.GetGenericTypeDefinition();
            foreach (var implementedInterface in type.FindInterfaces((objType, objCriteria) => true, null))
            {
                if (!implementedInterface.IsGenericType)
                    continue;

                var isMatch = genericTypeDefinition.IsAssignableFrom(implementedInterface.GetGenericTypeDefinition());
                return isMatch;
            }
            return false;
        }
        catch
        {
            return false;
        }
    }

    #endregion

    #region LoadAssemblies

    private IEnumerable<Assembly> LoadAssemblies()
    {
        // Get starting assembly
        var startAssembly = Assembly.GetEntryAssembly()!;
        if (startAssembly == null)
        {
            throw new Exception("How can starting assembly be unavailable?! Fix please!");
        }

        var assemblies = LoadAssemblies(startAssembly);
        return assemblies;
    }

    private IEnumerable<Assembly> LoadAssemblies(Assembly startAssembly)
    {
        // Wrap the assembly
        var assemblyWrapper = new AssemblyWrapper(startAssembly, 0);

        // Add all referenced assemblies
        assemblyWrapper.Parents = LoadReferencedAssemblies(assemblyWrapper);

        var unNestedAssemblies = new List<AssemblyWrapper>();
        UnNestAssembly(assemblyWrapper);

        var assemblies = unNestedAssemblies
            .GroupBy(ass => ass.Assembly.FullName)
            .Select(group => group.OrderByDescending(wrap => wrap.Depth).First())
            .OrderByDescending(wrap => wrap.Depth)
            .Select(wrap => wrap.Assembly)
            .ToArray()
        ;

        return assemblies;

        void UnNestAssembly(AssemblyWrapper assWrap)
        {
            unNestedAssemblies.Add(assWrap);
            foreach (var parent in assWrap.Parents)
            {
                UnNestAssembly(parent);
            }
        }
    }

    private static List<AssemblyWrapper> LoadReferencedAssemblies(AssemblyWrapper assemblyWrapper)
    {
        var referencedAssemblies = assemblyWrapper!.Assembly
            .GetReferencedAssemblies()
            .Where(assembly => MatchingAssemblyRegex().IsMatch(assembly.FullName))
            .Select(assemblyName => new AssemblyWrapper(assemblyName, assemblyWrapper.Depth + 1))
            .ToList()
        ;

        // Continue loading for parent assemblies        
        foreach (var referencedAssembly in referencedAssemblies)
        {
            referencedAssembly.Parents = LoadReferencedAssemblies(referencedAssembly);
        }

        return referencedAssemblies;
    }

    internal record AssemblyWrapper
    {
        public Assembly Assembly { get; }
        public int Depth { get; }
        public List<AssemblyWrapper> Parents { get; set; }

        public AssemblyWrapper(Assembly assembly, int depth)
        {
            Assembly = assembly;
            Depth = depth;
            Parents = new List<AssemblyWrapper>();
        }

        public AssemblyWrapper(AssemblyName assemblyName, int depth)
        {
            Assembly = Assembly.Load(assemblyName);
            Depth = depth;
            Parents = new List<AssemblyWrapper>();
        }
    }

    #endregion
}