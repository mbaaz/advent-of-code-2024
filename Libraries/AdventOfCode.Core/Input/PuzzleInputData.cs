namespace MBZ.AdventOfCode.Core.Input;

public record PuzzleInputData(string FileName, string[] Input, bool IsFetched)
{
    public bool IsEmpty => !Input.All(string.IsNullOrEmpty);
}