namespace MBZ.AdventOfCode.Year2024.Day08;

public record CityTile(Position Position, char Frequency = '.', bool IsAntiNode = false)
{
    public bool HasAntenna => Frequency != '.';

    public override string ToString() =>
        IsAntiNode ? "#" : Frequency.ToString();

    public CityTile AsAntiNode() =>
        new CityTile(Position, Frequency, IsAntiNode: true);
}