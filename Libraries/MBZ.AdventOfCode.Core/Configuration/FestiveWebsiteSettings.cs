using Microsoft.Extensions.Configuration;

namespace MBZ.AdventOfCode.Core.Configuration;

public record FestiveWebsiteSettings : IFestiveSettings
{
    private const string CONFIGURATION_SECTION_NAME = nameof(FestiveWebsiteSettings);
    
    private const string DEFAULT_SESSION_COOKIE_NAME = "session";
    private const string DEFAULT_INPUT_URL_FORMAT    = "https://adventofcode.com/{0}/day/{1}/input";

    public readonly string SessionID         = string.Empty;
    public readonly string SessionCookieName = DEFAULT_SESSION_COOKIE_NAME;
    public readonly string InputUrlFormat    = DEFAULT_INPUT_URL_FORMAT;

    public FestiveWebsiteSettings()
    {
    }

    public FestiveWebsiteSettings(
        string sessionID,
        string sessionCookieName,
        string inputUrlFormat
    )
    {
        SessionID = string.IsNullOrWhiteSpace(sessionID)
            ? string.Empty 
            : sessionID;

        SessionCookieName = string.IsNullOrWhiteSpace(sessionCookieName) 
            ? DEFAULT_SESSION_COOKIE_NAME 
            : sessionCookieName;

        InputUrlFormat = string.IsNullOrWhiteSpace(inputUrlFormat)
            ? DEFAULT_INPUT_URL_FORMAT
            : inputUrlFormat;
    }

    public FestiveWebsiteSettings(IConfiguration configuration)
    {
        var section = configuration.GetSection(CONFIGURATION_SECTION_NAME);

        var sessionID = section[nameof(SessionID)];
        var sessionCookieName = section[nameof(SessionCookieName)];
        var inputUrlFormat = section[nameof(InputUrlFormat)];

        SessionID = string.IsNullOrWhiteSpace(sessionID)
            ? string.Empty
            : sessionID;

        SessionCookieName = string.IsNullOrWhiteSpace(sessionCookieName)
            ? DEFAULT_SESSION_COOKIE_NAME
            : sessionCookieName;

        InputUrlFormat = string.IsNullOrWhiteSpace(inputUrlFormat)
            ? DEFAULT_INPUT_URL_FORMAT
            : inputUrlFormat;
    }

    public string GetInputUrl(int year, int day) =>
        string.Format(InputUrlFormat, year, day);
}