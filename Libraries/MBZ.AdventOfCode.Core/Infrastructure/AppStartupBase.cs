using Microsoft.Extensions.DependencyInjection;

namespace MBZ.AdventOfCode.Core.Infrastructure;

public abstract class AppStartupBase
{
    public virtual void ConfigureServices(IServiceCollection services)
    {
    }

    public virtual void Configure(IServiceProvider services)
    {
    }
}