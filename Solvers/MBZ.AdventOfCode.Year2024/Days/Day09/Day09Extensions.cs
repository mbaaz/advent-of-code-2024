namespace MBZ.AdventOfCode.Year2024.Day09;

internal static class Day09Extensions
{
    public static Disk ParseToDay09Data(this string[] input)
    {
        var oneLineInput = string.Join("", input).Trim();
        var disk = new Disk(oneLineInput);
        return disk;
    }
}