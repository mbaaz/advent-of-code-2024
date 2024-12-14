namespace MBZ.AdventOfCode.Year2020;

public static class Program
{
    public static async Task Main(string[] input)
    {
        // Build DI Services
        var services = FestiveApplicationContext.CreateServices();

        // Fetch and run application
        var festiveApp = services.GetRequiredService<IFestiveApplication>();
        festiveApp.Setup(services);
        await festiveApp.Run();
    }
}