using Microsoft.Extensions.DependencyInjection;
using static System.Net.Mime.MediaTypeNames;

namespace MBZ.AdventOfCode.Year2024;

public static class Program
{
    public static void Main(string[] input)
    {
        // Build services (DI)
        var services = CreateServices();

        // Prepare Console
        Console.WindowWidth = 150;
        Console.WindowHeight = 60;

        // Fetch and run application
        var festiveApp = services.GetRequiredService<FestiveApplication>();
        festiveApp.Setup(services);
        festiveApp.Run();
    }

    private static ServiceProvider CreateServices()
    {
        var serviceCollection = new ServiceCollection()
            .AddSingleton<FestiveApplication>(new FestiveApplication())
        ;



        var serviceProvider = serviceCollection
            .BuildServiceProvider()
        ;

        return serviceProvider;
    }
}