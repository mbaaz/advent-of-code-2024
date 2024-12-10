namespace MBZ.AdventOfCode.Year2024.Day02;

public static class Day02Extensions
{
    public static List<Report> ParseDay02Input(this string[] input)
    {
        var reports = input
                .Select(ParseReport)
                .ToList()
            ;
        return reports;
    }

    private static Report ParseReport(string input)
    {
        var levels = input
                .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(byte.Parse)
                .ToArray()
            ;
        return new Report(levels);
    }
}