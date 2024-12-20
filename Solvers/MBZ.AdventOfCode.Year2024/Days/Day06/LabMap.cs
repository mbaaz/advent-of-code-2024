using System.Collections.Concurrent;

namespace MBZ.AdventOfCode.Year2024.Day06;

public record LabMap : Map<LabRoomTile>
{
    public LabMap(LabRoomTile[][] labRoomTiles) : base(labRoomTiles)
    {
    }


    public int GetVisitedTilesCount() =>
        GetTiles().Count(tile => tile is EmptyTile { IsVisited: true });
    
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

        if (!IsInMap(nextPosition))
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

    public void SetTile(Position position, LabRoomTile tile)
    {
        if (!IsInMap(position)) {
            throw new Exception("Cannot set a tile not within the grid!");
        }

        Tiles[position.RowIndex][position.ColumnIndex] = tile;
    }

    public LabMap Copy() => 
        new(Tiles.Select(row => row.Select(tile => tile.Copy()).ToArray()).ToArray());

    public int CalculateHowManyPositionsForObstructionToPlaceGuardInLoop(Guard guard, out int emptyPositionsChecked)
    {
        var possibleObstructionPositions = GetPossibleObstructionPositions(guard.Position!).ToList();
        emptyPositionsChecked = possibleObstructionPositions.Count;

        var obstructionPositions = new ConcurrentBag<Position>();

        Parallel.ForEach(possibleObstructionPositions, (position) =>
        {
            if(CheckIfObstructionWouldLoopGuard(position, guard)) {
                obstructionPositions.Add(position);
            }
        });
        
        return obstructionPositions.Count;
    }

    private IEnumerable<Position> GetPossibleObstructionPositions(Position? guardPosition)
    {
        for (var rowIndex = 0; rowIndex < Rows; rowIndex++)
        {
            for (var columnIndex = 0; columnIndex < Columns; columnIndex++)
            {
                var position = new Position(rowIndex, columnIndex);
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

    private bool CheckIfObstructionWouldLoopGuard(Position position, Guard guard)
    {
        var newGuard = guard.Copy();

        var newMap = this.Copy();
        newMap.SetTile(position, new ObstacleTile());

        newMap.SimulateGuardMovements(newGuard, out _, out var isLooped);
        return isLooped;
    }
}