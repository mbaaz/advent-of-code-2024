using Microsoft.Extensions.DependencyInjection;

namespace MBZ.AdventOfCode.Year2024;

public static class Program
{
    public static void Main(string[] input)
    {
        // Build services (DI)
        var services = CreateServices();

        // Fetch and run application
        var festiveApp = services.GetRequiredService<IFestiveApplication>();
        festiveApp.Setup(services);

        var test = new MBZ.AdventOfCode.Aardward.Class1();

        Console.Write("Press any key to exit: ");
        Console.ReadKey();
    }

    private static ServiceProvider CreateServices()
    {
        var serviceCollection = new ServiceCollection()
            .AddFestiveApplication(new FestiveApplication())
            .ConfigureFestiveAppServices()
        ;

        var serviceProvider = serviceCollection
            .BuildServiceProvider()
            .ConfigureFestiveApp()
        ;

        return serviceProvider;
    }
}