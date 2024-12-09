using Microsoft.Extensions.DependencyInjection;

namespace MBZ.AdventOfCode.Core.Infrastructure;

public interface IAppStartup
{
    void ConfigureServices(IServiceCollection services);
    void Configure(IServiceProvider services);
}