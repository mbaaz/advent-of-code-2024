namespace MBZ.AdventOfCode.Year2024.Day08;

public record City(CityTile[][] Tiles)
{
    public CityTile? GetTile(Position position) =>
        IsValidPosition(position) ? Tiles[position.RowIndex][position.ColumnIndex] : null;

    private bool IsValidPosition(Position position) =>
        position.RowIndex >= 0 && 
        position.RowIndex < Tiles.Length &&
        position.ColumnIndex >= 0 && 
        position.ColumnIndex < Tiles[position.RowIndex].Length
    ;

    public void CalculateAntiNodes(bool extended = false)
    {
        var frequencies = GetFrequencies();
        foreach (var frequency in frequencies)
        {
            CalculateAntiNodes(frequency, extended);
        }
    }

    private void CalculateAntiNodes(char frequency, bool extended)
    {
        // Fetch all antennas with the given frequency
        var antennaPositions = GetAntennaPositions(frequency);

        // No need to calculate anti-nodes if there is only one antenna
        if (antennaPositions.Count <= 1)
        {
            return;
        }

        // Calculate anti-nodes
        for (var i = 0; i < antennaPositions.Count - 1; i++)
        {
            for (var j = i + 1; j < antennaPositions.Count; j++)
            {
                var antennaPosition1 = antennaPositions[i];
                var antennaPosition2 = antennaPositions[j];
                var distance = antennaPosition1.DistanceTo(antennaPosition2);


                if (!extended)
                {
                    MarkAntiNode(antennaPosition1.Subtract(distance));
                    MarkAntiNode(antennaPosition2.Add(distance));
                }
                
                if (extended)
                {
                    var antiNodePosition = antennaPosition1;
                    while (MarkAntiNode(antiNodePosition))
                    {
                        antiNodePosition = antiNodePosition.Subtract(distance);
                    }

                    antiNodePosition = antennaPosition2;
                    while (MarkAntiNode(antiNodePosition))
                    {
                        antiNodePosition = antiNodePosition.Add(distance);
                    }
                }
            }
        }
    }

    private bool MarkAntiNode(Position position)
    {
        var tile = GetTile(position);
        if (tile == null)
        {
            return false;
        }

        // No need to update the tile if it is already an anti-node
        if (!tile.IsAntiNode)
        {
            // Update the tile to be an anti-node
            Tiles[position.RowIndex][position.ColumnIndex] = tile.AsAntiNode();
        }

        return true;
    }

    private List<Position> GetAntennaPositions(char frequency) =>
        Tiles
            .SelectMany(t => t)
            .Where(t => t.HasAntenna && t.Frequency == frequency)
            .Select(t => t.Position)
            .ToList()
    ;

    private List<char> GetFrequencies() =>
        Tiles
            .SelectMany(t => t)
            .Where(t => t.HasAntenna)
            .Select(t => t.Frequency)
            .Distinct()
            .ToList()
    ;
    
    public int CountAntiNodes() =>
        Tiles
            .SelectMany(t => t)
            .Count(t => t.IsAntiNode)
    ;

    public override string ToString() =>
        string.Join(Environment.NewLine, Tiles.Select(tileRow => string.Join("", tileRow.Select(t => t))));
}