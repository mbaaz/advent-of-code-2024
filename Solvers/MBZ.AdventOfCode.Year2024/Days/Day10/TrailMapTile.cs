namespace MBZ.AdventOfCode.Year2024.Day10;

public record TrailMapTile(Position Position, short Height)
{
    public const short TRAIL_HEAD_HEIGHT = 0;
    public const short TRAIL_PEAK_HEIGHT = 9;

    public bool IsTrailHead => Height == TRAIL_HEAD_HEIGHT;
    public bool IsPeak => Height == TRAIL_PEAK_HEIGHT;
}