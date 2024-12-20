namespace MBZ.AdventOfCode.Year2024.Day10;

internal static class Day10Extensions
{
    public static TrailMap ParseToDay10Data(this string[] input)
    {
        var mapData = new TrailMapTile[input.Length][];
        for (var rowIndex = 0; rowIndex < input.Length; rowIndex++)
        {
            mapData[rowIndex] = ParseRow(input[rowIndex], rowIndex);
        }
        return new TrailMap(mapData);
    }

    private static TrailMapTile[] ParseRow(string row, int rowIndex)
    {
        var mapTiles = new TrailMapTile[row.Length];
        for (var columnIndex = 0; columnIndex < row.Length; columnIndex++)
        {
            var position = new Position(rowIndex, columnIndex);
            var value = short.Parse(row[columnIndex].ToString());
            mapTiles[columnIndex] = new TrailMapTile(position, value);
        }
        return mapTiles;
    }
}