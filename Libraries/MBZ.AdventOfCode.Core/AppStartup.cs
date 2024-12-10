using MBZ.AdventOfCode.Core.Configuration;
using MBZ.AdventOfCode.Core.Infrastructure;
using MBZ.AdventOfCode.Core.Input;
using Microsoft.Extensions.Configuration;

namespace MBZ.AdventOfCode.Core;

public class AppStartup : AppStartupBase, IAppStartup
{
    public override IConfigurationBuilder ConfigureSettings(IConfigurationBuilder configurationBuilder)
    {
        return configurationBuilder
            .AddUserSecrets<FestiveApplication>()  // Register User Secrets
        ;
    }

    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IFestiveApplication, FestiveApplication>();
        services.AddTransient<IFestiveRunner, FestiveRunner>();
        services.AddTransient<SolverHelper>();
        services.AddTransient<InputHelper>();
    }

    public override void Configure(IServiceProvider services)
    {
        
    }
}