using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.Extensions.DependencyInjection;

namespace MBZ.AdventOfCode.Core.Infrastructure;

public static partial class ApplicationContext
{
    public static IServiceCollection AddFestiveApplication(this IServiceCollection services, IFestiveApplication app, bool force = false)
    {
        if (services == null)
            throw new ArgumentNullException(nameof(services));

        if (app == null)
            throw new ArgumentNullException(nameof(app));


        services.AddSingleton<IFestiveApplication>(app);
        return services;
    }


    private const string MATCHING_ASSEMBLY_REGEX_PATTERN = @"^MBZ\.AdventOfCode\..*$";

    [GeneratedRegex(MATCHING_ASSEMBLY_REGEX_PATTERN, RegexOptions.Singleline)]
    private static partial Regex MatchingAssemblyRegex();

    public static IServiceCollection ConfigureFestiveAppStartup(this IServiceCollection services)
    {
        var assemblies = LoadAssemblies();

        var test = assemblies.Count();
        

        return services;
    }

    private static Assembly[] LoadAssemblies()
    {
        // Get starting assembly
        var startAssembly = Assembly.GetEntryAssembly()!;
        if (startAssembly == null)
        {
            throw new Exception("How can starting assembly be unavailable?! Fix please!");
        }

        var assemblies = LoadAssemblies(startAssembly).ToArray();

    }

    private static IEnumerable<AssemblyWrapper> LoadAssemblies(Assembly assembly)
    {
        // Wrap the assembly
        var startAssembly = new AssemblyWrapper(assembly, 0);

        // Start a list and add wrapped assembly
        var assemblies = new List<AssemblyWrapper> { startAssembly };

        // Add all referenced assemblies
        var referencedAssemblies = LoadReferencedAssemblies(startAssembly);
        foreach (var referencedAssembly in referencedAssemblies)
        {
            assemblies.Add(referencedAssembly);
            assemblies.Add(LoadAssemblies());
        }
        
        return assemblies;
    }

    private static IEnumerable<AssemblyWrapper> LoadReferencedAssemblies(AssemblyWrapper assemblyWrapper)
    {
        var referencedAssemblies = assemblyWrapper!.Assembly
            .GetReferencedAssemblies()
            .Where(assembly => MatchingAssemblyRegex().IsMatch(assembly.FullName))
            .Select(assemblyName => new AssemblyWrapper(assemblyName, assemblyWrapper.Depth + 1))
            .ToList()
        ;
        return referencedAssemblies;
    }

    private static void ConfigureFestiveAssembly(Assembly assembly)
    {

    }

}