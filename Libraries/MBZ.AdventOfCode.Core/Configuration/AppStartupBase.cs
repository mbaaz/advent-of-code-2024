using Microsoft.Extensions.Configuration;

namespace MBZ.AdventOfCode.Core.Configuration;

public abstract class AppStartupBase
{
    public virtual IConfigurationBuilder ConfigureSettings(IConfigurationBuilder configurationBuilder)
    {
        return configurationBuilder;
    }

    public virtual void ConfigureServices(IServiceCollection services)
    {
    }

    public virtual void Configure(IServiceProvider services)
    {
    }
}