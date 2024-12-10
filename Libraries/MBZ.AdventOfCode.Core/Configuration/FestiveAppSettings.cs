using Microsoft.Extensions.Configuration;

namespace MBZ.AdventOfCode.Core.Configuration;


public record FestiveAppSettings : IFestiveSettings
{
    private const string CONFIGURATION_SECTION_NAME = nameof(FestiveAppSettings);

    private const int DEFAULT_YEAR = 1900;

    public readonly int Year = DEFAULT_YEAR;

    public FestiveAppSettings()
    {
    }

    public FestiveAppSettings(int year)
    {
        Year = year;
    }

    public FestiveAppSettings(IConfiguration config)
    {
        var section = config.GetSection(CONFIGURATION_SECTION_NAME);
        
        var strYear = section[nameof(Year)];
        Year = string.IsNullOrWhiteSpace(strYear) || !int.TryParse(strYear, out var tempYear)
            ? DEFAULT_YEAR
            : tempYear
        ;
    }
}