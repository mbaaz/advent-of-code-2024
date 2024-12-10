using System.Text;
using MBZ.AdventOfCode.Core.Configuration;
using MBZ.AdventOfCode.Core.Infrastructure;
using MBZ.AdventOfCode.Core.Solvers;

namespace MBZ.AdventOfCode.Core.Input;

public class InputHelper
{
    private const string INPUT_FILE_NAME_FORMAT = """Days\Day{0}\Day{0}-{1}.{2}""";
    private const string INPUT_DATA_FILE_NAME = "input";
    private const string INPUT_TEST_FILE_NAME = "test";
    private const string INPUT_FILE_PART_SUFFIX = "-part";

    private const string INPUT_FILE_NAME_EXTENSION = "txt";

    private static string GetInputFilePath(int day, PuzzlePart? puzzlePart, bool useTestInput) =>
        string.Format(INPUT_FILE_NAME_FORMAT
            , day.ToString().PadLeft(2, '0')
            , (useTestInput ? INPUT_TEST_FILE_NAME : INPUT_DATA_FILE_NAME) + (puzzlePart.HasValue ? INPUT_FILE_PART_SUFFIX + (short)puzzlePart : string.Empty)
            , INPUT_FILE_NAME_EXTENSION
        );

    private static FileInfo GetFileInfo(string relativePath) =>
        new FileInfo(Path.Combine(Environment.CurrentDirectory, relativePath));

    private static string[] ReadFile(FileInfo file) =>
        File.ReadAllLines(file.FullName);

    public async Task<PuzzleInputData> GetInput(int day, PuzzlePart puzzlePart, bool useTestInput)
    {
        var partSpecificFile = GetFileInfo(GetInputFilePath(day, puzzlePart, useTestInput));
        if(partSpecificFile is { Exists: true })
        {
            var content = ReadFile(partSpecificFile);
            if(content.Length > 0 && !content.All(string.IsNullOrWhiteSpace))
                return new PuzzleInputData(partSpecificFile.Name, content, false);
        }
        
        var genericFile = GetFileInfo(GetInputFilePath(day, null, useTestInput));
        if(genericFile is { Exists: true })
        {
            var content = ReadFile(genericFile);
            if (content.Length > 0 && !content.All(string.IsNullOrWhiteSpace))
                return new PuzzleInputData(genericFile.Name, content, false);
        }

        if(useTestInput)
        {
            return new PuzzleInputData(genericFile.Name, [string.Empty], false);
        }
        

        // OK, we need to try and load puzzle data!
        {
            var onlineContent = await FetchPuzzleDataOnline(day);
            if (onlineContent.Length == 0 || onlineContent.All(string.IsNullOrWhiteSpace))
            {
                return new PuzzleInputData(genericFile.Name, [string.Empty], true);
            }

            // Write content to disk
            var diskContent = string.Join(Environment.NewLine, onlineContent);
            await File.WriteAllTextAsync(genericFile.FullName, diskContent, Encoding.Latin1);

            // Return data
            return new PuzzleInputData(genericFile.Name, onlineContent, true);
        }
    }

    private static async Task<string[]> FetchPuzzleDataOnline(int day)
    {
        var appSettings = FestiveApplicationContext.Services.GetRequiredService<FestiveAppSettings>();
        var websiteSettings = FestiveApplicationContext.Services.GetRequiredService<FestiveWebsiteSettings>();
        var url = websiteSettings!.GetInputUrl(appSettings.Year, day);
        var sessionID = websiteSettings.SessionID;

        if(string.IsNullOrWhiteSpace(sessionID))
        {
            throw new Exception("Website Session ID is not set - You need to set the Session ID from a logged in session in a browser to the User Secret in Core project!");
        }

        using var stream = await GetInputFileStream(url, websiteSettings.SessionCookieName, websiteSettings.SessionID);
        var input = ReadStream(stream).ToArray();

        return input;
    }

    private static async Task<StreamReader> GetInputFileStream(string url, string sessionCookieName, string sessionID)
    {
        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("cookie", $"{ sessionCookieName }={ sessionID }");
        var stream = await httpClient.GetStreamAsync(url);
        return new StreamReader(stream);
    }

    private static IEnumerable<string> ReadStream(StreamReader inputStream)
    {
        while (inputStream.ReadLine() is { } line)
        {
            yield return line;
        }
    }
}