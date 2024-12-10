using System.Reflection;
using MBZ.AdventOfCode.Core.Configuration;
using Microsoft.Extensions.Configuration;

namespace MBZ.AdventOfCode.Core.Infrastructure;

public static class FestiveApplicationContext
{
    public static ServiceProvider Services { get; private set; }

    public static ServiceProvider CreateServices()
    {
        var serviceProvider = new ServiceCollection()
            .ConfigureFestiveAppServices()
            .BuildServiceProvider()
            .ConfigureFestiveApp()
        ;
        return serviceProvider;
    }

    public static IServiceCollection ConfigureFestiveAppServices(this IServiceCollection services)
    {
        // Create a new Type Finder
        var typeFinder = new AppTypeFinder();

        // Register Type Finder for DI later
        services.AddSingleton<ITypeFinder>(typeFinder);

        // Find all IAppStartup classes
        var appStartupClasses = typeFinder!
            .FindClassesOfType<IAppStartup>()
            .Select(startupType => Activator.CreateInstance(startupType) as IAppStartup)
            .Cast<IAppStartup>()
            .ToList()
        ;

        // Build configuration
        InitializeSettings(typeFinder, appStartupClasses, services);

        // Run all ConfigureServices-methods!
        foreach(var appStartup in appStartupClasses)
        {
            appStartup.ConfigureServices(services);
        }
        
        return services;
    }

    public static ServiceProvider ConfigureFestiveApp(this ServiceProvider serviceProvider)
    {
        // Store the serviceProvider in a statically available variable
        Services = serviceProvider;

        // Resolve Type Finder
        var typeFinder = serviceProvider.GetService<ITypeFinder>();

        // Find all IAppStartup classes
        var appStartupClasses = typeFinder!
            .FindClassesOfType<IAppStartup>()
            .Select(startupType => Activator.CreateInstance(startupType) as IAppStartup)
            .Cast<IAppStartup>()
            .ToList()
        ;

        // Run all Configure-methods!
        foreach (var appStartup in appStartupClasses)
        {
            appStartup.Configure(serviceProvider);
        }

        return serviceProvider;
    }

    private static void InitializeSettings(ITypeFinder typeFinder, IEnumerable<IAppStartup> appStartupClasses, IServiceCollection services)
    {
        // Gather all configuration sources from IAppStartup-classes
        var configurationBuilder = appStartupClasses
            .Aggregate((IConfigurationBuilder)new ConfigurationBuilder(), (current, appStartup) => appStartup.ConfigureSettings(current))
        ;

        // Build the final configuration object
        var configuration = configurationBuilder.Build();

        // Find all IFestiveSettings-classes
        var festiveSettingsTypes = typeFinder
            .FindClassesOfType<IFestiveSettings>()
            .ToList()
        ;

        // Try to instantiate all IFestiveSettings-classes with configuration as constructor argument
        // And save to DI services as singleton
        foreach(var festiveSettingsType in festiveSettingsTypes)
        {
            var settings = 
                Activator.CreateInstance(festiveSettingsType, configuration) as IFestiveSettings ??
                throw new Exception($"""Could not create an instance of IFestiveSettings type "{festiveSettingsType.Name}"!""")
            ;
            services.AddSingleton(festiveSettingsType, settings);
        }
    }
}