using System.Collections.Concurrent;

namespace MBZ.AdventOfCode.Year2024.Day06;

public class Map(Tile[][] tiles)
{
    public int GetVisitedTilesCount() =>
        tiles.Sum(row => row.Count(tile => (tile is EmptyTile { IsVisited: true })));

    public void SimulateGuardMovements(Guard guard, out int totalMovements, out bool isLooped)
    {
        totalMovements = 0;
        isLooped = false;

        while (guard.Position != null && !isLooped)
        {
            totalMovements++;
            MoveGuard(guard, out isLooped);
        }
    }

    private void MoveGuard(Guard guard, out bool isLooped)
    {
        isLooped = false;

        var nextPosition = guard.GetNextPosition();

        if (!IsOnMap(nextPosition))
        {
            // Guard moved out of map
            guard.Position = null;
            return;
        }

        var nextTile = GetTile(nextPosition!);
        
        if (nextTile is ObstacleTile)
        {
            // Rotate guard to face away from the obstacle
            guard.Rotate();

            // Guard stays in place
            return;
        }

        if (nextTile is EmptyTile nextEmptyTile)
        {
            // Move guard there
            guard.Move();

            // Mark next tile as visited
            nextEmptyTile.MarkAsVisited(guard.Heading, out isLooped);
            
            return;
        }

        throw new Exception("Unknown type of Tile encountered!");
    }

    private bool IsOnMap(Point? position) =>
        position is { RowIndex: >= 0, ColumnIndex: >= 0 } &&
        position.RowIndex < tiles.Length &&
        position.ColumnIndex < tiles[position.RowIndex].Length;

    private Tile? GetTile(Point position) =>
        IsOnMap(position)
            ? tiles[position.RowIndex][position.ColumnIndex]
            : null;

    public void SetTile(Point position, Tile tile)
    {
        if (!IsOnMap(position)) {
            throw new Exception("Cannot set a tile not within the grid!");
        }

        tiles[position.RowIndex][position.ColumnIndex] = tile;
    }

    public Map Copy() => 
        new(tiles.Select(row => row.Select(tile => tile.Copy()).ToArray()).ToArray());

    public int CalculateHowManyPositionsForObstructionToPlaceGuardInLoop(Guard guard, out int emptyPositionsChecked)
    {
        var possibleObstructionPositions = GetPossibleObstructionPositions(guard.Position!).ToList();
        emptyPositionsChecked = possibleObstructionPositions.Count;

        var obstructionPositions = new ConcurrentBag<Point>();

        Parallel.ForEach(possibleObstructionPositions, (position) =>
        {
            if(CheckIfObstructionWouldLoopGuard(position, guard)) {
                obstructionPositions.Add(position);
            }
        });
        
        return obstructionPositions.Count;
    }

    private IEnumerable<Point> GetPossibleObstructionPositions(Point? guardPosition)
    {
        for (var rowIndex = 0; rowIndex < tiles.Length; rowIndex++)
        {
            for (var columnIndex = 0; columnIndex < tiles[rowIndex].Length; columnIndex++)
            {
                var position = new Point(rowIndex, columnIndex);
                if (guardPosition == position)
                {
                    continue;
                }

                var tile = GetTile(position);
                if (tile is not EmptyTile)
                {
                    continue;
                }

                yield return position;
            }
        }
    }

    private bool CheckIfObstructionWouldLoopGuard(Point position, Guard guard)
    {
        var newGuard = guard.Copy();

        var newMap = this.Copy();
        newMap.SetTile(position, new ObstacleTile());

        newMap.SimulateGuardMovements(newGuard, out _, out var isLooped);
        return isLooped;
    }
}