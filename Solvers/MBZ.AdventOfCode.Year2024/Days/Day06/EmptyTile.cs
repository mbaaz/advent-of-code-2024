namespace MBZ.AdventOfCode.Year2024.Day06;

public class EmptyTile : Tile
{
    private readonly bool[] _visitedHeadings = new bool[4]; // Heading have values 0-3

    public bool IsVisited => _visitedHeadings.Any(headingVisited => headingVisited);

    public void MarkAsVisited(Heading guardHeading, out bool visitedBefore)
    {
        visitedBefore = _visitedHeadings[(short)guardHeading];
        _visitedHeadings[(short)guardHeading] = true;
    }

    public override EmptyTile Copy() =>
        new EmptyTile();
}