using Microsoft.Extensions.DependencyInjection;

namespace MBZ.AdventOfCode.Core.Infrastructure;

public static class ApplicationContext
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

    public static IServiceCollection ConfigureFestiveAppServices(this IServiceCollection services)
    {
        // Create a new Type Finder
        var typeFinder = new AppTypeFinder();

        // Register Type Finder for DI later
        services.AddSingleton<ITypeFinder>(typeFinder);

        // Find all IAppStartup classes
        var appStartupClasses = typeFinder.FindClassesOfType<IAppStartup>();

        // Run all ConfigureServices-methods!
        foreach(var appStartupClass in appStartupClasses)
        {
            try
            {
                if(Activator.CreateInstance(appStartupClass) is IAppStartup appStartup)
                {
                    appStartup.ConfigureServices(services);
                }
            }
            catch(Exception ex)
            {
                // This would be a good place to log an error
                // At least if we would want to swallow it.
                throw;
            }
        }
        
        return services;
    }

    public static ServiceProvider ConfigureFestiveApp(this ServiceProvider serviceProvider)
    {
        // Resolve Type Finder
        var typeFinder = serviceProvider.GetService<ITypeFinder>();

        // Find all IAppStartup classes
        var appStartupClasses = typeFinder!.FindClassesOfType<IAppStartup>();

        // Run all ConfigureServices-methods!
        foreach (var appStartupClass in appStartupClasses)
        {
            try
            {
                if (Activator.CreateInstance(appStartupClass) is IAppStartup appStartup)
                {
                    appStartup.Configure(serviceProvider);
                }
            }
            catch (Exception ex)
            {
                // This would be a good place to log an error
                // At least if we would want to swallow it.
                throw;
            }
        }

        return serviceProvider;
    }
}