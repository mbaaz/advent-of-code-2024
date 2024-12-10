namespace MBZ.AdventOfCode.Core.Infrastructure;

public class FestiveApplication : IFestiveApplication
{
    private IServiceProvider? Services { get; set; }

    public void Setup(IServiceProvider serviceProvider)
    {
        Services = serviceProvider;

        // Prepare Console
        Console.WindowWidth = 150;
        Console.WindowHeight = 60;
    }

    public async Task Run()
    {
        if(Services == null)
        {
            throw new Exception("Cannot run festive application - Setup has not been called!");
        }

        var festiveRunner = Services.GetRequiredService<IFestiveRunner>();
        await festiveRunner.Run();
    }
}