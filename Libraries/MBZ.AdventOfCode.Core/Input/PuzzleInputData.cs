namespace MBZ.AdventOfCode.Core.Input;

public record PuzzleInputData(string FileName, string[] Input, bool IsFetched)
{
    public bool IsEmpty => Input.Length == 0 || Input.All(string.IsNullOrEmpty);
}