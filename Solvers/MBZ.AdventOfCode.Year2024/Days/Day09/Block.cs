namespace MBZ.AdventOfCode.Year2024.Day09;

public record Block(int FileID)
{
    public override string ToString() =>
        FileID.ToString();
}