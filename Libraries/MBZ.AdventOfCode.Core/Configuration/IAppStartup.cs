using Microsoft.Extensions.Configuration;

namespace MBZ.AdventOfCode.Core.Configuration;

public interface IAppStartup
{
    IConfigurationBuilder ConfigureSettings(IConfigurationBuilder configurationBuilder);
    void ConfigureServices(IServiceCollection services);
    void Configure(IServiceProvider services);
}