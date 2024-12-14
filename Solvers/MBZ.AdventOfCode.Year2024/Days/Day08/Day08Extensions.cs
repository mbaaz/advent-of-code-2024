namespace MBZ.AdventOfCode.Year2024.Day08;

public static class Day08Extensions
{
    public static City ParseToDay08Data(this string[] input)
    {
        var cityTiles = new List<CityTile[]>();
        for (var rowIndex = 0; rowIndex < input.Length; rowIndex++)
        {
            if(string.IsNullOrWhiteSpace(input[rowIndex]))
                continue;
            cityTiles.Add(ParseLine(rowIndex, input[rowIndex].Trim()));
        }
        return new City(cityTiles.ToArray());
    }

    private static CityTile[] ParseLine(int rowIndex, string line)
    {
        var cityTiles = new List<CityTile>();
        for (var columnIndex = 0; columnIndex < line.Length; columnIndex++)
        {
            var position = new Position(rowIndex, columnIndex);
            var cityTile = new CityTile(position, line[columnIndex]);
            cityTiles.Add(cityTile);
        }
        return cityTiles.ToArray();
    }

}