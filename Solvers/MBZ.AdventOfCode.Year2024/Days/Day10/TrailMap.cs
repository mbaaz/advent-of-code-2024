namespace MBZ.AdventOfCode.Year2024.Day10;

public record TrailMap : Map<TrailMapTile>
{
    public TrailMap(TrailMapTile[][] mapData) : base(mapData)
    {
    }

    public IEnumerable<TrailMapTile> GetTrailHeads() =>
        GetTiles().Where(tile => tile.IsTrailHead).ToList();

    public int CalculateScoreForTrailHead(TrailMapTile trailHead)
    {
        var currentHeight = trailHead.Height;
        var currentTiles = new[]{ trailHead };
        while (currentHeight < TrailMapTile.TRAIL_PEAK_HEIGHT)
        {
            var heightToFind = currentHeight + 1;
            currentTiles = currentTiles
                    .SelectMany(tile => GetNeighbours(tile.Position))
                    .Where(tile => tile.Height == heightToFind)
                    .DistinctBy(tile => tile.Position)
                    .ToArray()
                ;
            currentHeight++;
        }
        return currentTiles.Length;
    }

    public int CalculateScoreForTrailHeads() =>
        GetTrailHeads()
            .Select(CalculateScoreForTrailHead)
            .Sum();

    public int CalculateRatingForTrailHead(TrailMapTile trailHead)
    {
        var currentHeight = trailHead.Height;
        var paths = new List<List<TrailMapTile>> { new List<TrailMapTile> { trailHead } };
        while (currentHeight < TrailMapTile.TRAIL_PEAK_HEIGHT)
        {
            var heightToFind = currentHeight + 1;
            var newPaths = paths
                    .SelectMany(path => 
                        GetNeighbours(path.Last().Position)
                            .Where(tile => tile.Height == heightToFind)
                            .Select(tile => path.Append(tile).ToList())
                    )
                    .ToList()
                ;
            paths = newPaths;
            currentHeight++;
        }
        return paths.Count;
    }

    public int CalculateRatingForTrailHeads() =>
        GetTrailHeads()
            .Select(CalculateRatingForTrailHead)
            .Sum();
}