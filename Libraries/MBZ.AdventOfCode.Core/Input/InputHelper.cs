using System.Text;
using MBZ.AdventOfCode.Core.Solvers;

namespace MBZ.AdventOfCode.Core.Input;

public class InputHelper
{
    private const string INPUT_FILE_NAME_FORMAT = """Days\Day{0}\day{0}-{1}.{2}""";
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

    public static PuzzleInputData GetInput(int day, PuzzlePart puzzlePart, bool useTestInput)
    {
        var partSpecificFile = GetFileInfo(GetInputFilePath(day, puzzlePart, useTestInput));
        if(partSpecificFile.Exists)
        {
            var content = ReadFile(partSpecificFile);
            if(!content.All(string.IsNullOrWhiteSpace))
                return new PuzzleInputData(partSpecificFile.Name, content, false);
        }
        
        var genericFile = GetFileInfo(GetInputFilePath(day, null, useTestInput));
        if(genericFile.Exists)
        {
            var content = ReadFile(partSpecificFile);
            if (!content.All(string.IsNullOrWhiteSpace))
                return new PuzzleInputData(genericFile.Name, content, false);
        }

        if(useTestInput)
        {
            return new PuzzleInputData(genericFile.Name, [string.Empty], false);
        }
        

        // OK, we need to try and load puzzle data!
        {
            var onlineContent = FetchPuzzleDataOnline();
            string[] content = [string.Empty];
            if(!string.IsNullOrWhiteSpace(onlineContent))
            {
                // Write content to disk
                File.WriteAllText(genericFile.FullName, onlineContent, Encoding.Latin1);

                // Convert data to expected format
                content = onlineContent.Split(Environment.NewLine, StringSplitOptions.TrimEntries);
            }

            // Return data
            return new PuzzleInputData(genericFile.Name, content, true);
        }
    }

    private static string FetchPuzzleDataOnline()
    {
        return string.Empty;
    }
}