using MBZ.AdventOfCode.Core.Configuration;
using Microsoft.Extensions.Configuration;

namespace MBZ.AdventOfCode.Year2024;

public class AppStartup : AppStartupBase, IAppStartup
{
    public override IConfigurationBuilder ConfigureSettings(IConfigurationBuilder configurationBuilder)
    {
        return configurationBuilder
            .AddJsonFile("FestiveAppConfig.json")
        ;
    }

    public override void ConfigureServices(IServiceCollection services)
    {
        
    }

    public override void Configure(IServiceProvider services)
    {

    }

}