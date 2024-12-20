namespace MBZ.AdventOfCode.Domain;

public abstract record Map<TEntity> where TEntity : class
{
    protected TEntity[][] Tiles { get; }


    protected Map(TEntity[][] tiles)
    {
        Tiles = tiles;
    }

    protected Map(IEnumerable<IEnumerable<TEntity>> tiles)
    {
        Tiles = tiles.Select(row => row.ToArray()).ToArray();
    }


    public int Rows =>
        Tiles.Length;

    public int Columns =>
        Tiles[0].Length;

    public IEnumerable<TEntity> GetTiles() =>
        Tiles.SelectMany(row => row).ToList();

    public TEntity GetTile(Position position) =>
        Tiles[position.RowIndex][position.ColumnIndex];

    public TEntity GetTile(int rowIndex, int columnIndex) =>
        Tiles[rowIndex][columnIndex];

    public bool TryGetTile(Position position, out TEntity? tile)
    {
        if (!IsInMap(position))
        {
            tile = null;
            return false;
        }

        tile = Tiles[position.RowIndex][position.ColumnIndex];
        return true;
    }


    public bool TryGetTile(int rowIndex, int columnIndex, out TEntity? tile)
    {
        if (!IsInMap(rowIndex, columnIndex))
        {
            tile = null;
            return false;
        }

        tile = Tiles[rowIndex][columnIndex];
        return true;
    }

    public bool IsInMap(Position? position) =>
        position is not null &&
        position.RowIndex >= 0 &&
        position.RowIndex < Rows &&
        position.ColumnIndex >= 0 &&
        position.ColumnIndex < Columns
    ;

    public bool IsInMap(int rowIndex, int columnIndex) =>
        rowIndex >= 0 &&
        rowIndex < Rows &&
        columnIndex >= 0 &&
        columnIndex < Columns
    ;

    public IEnumerable<TEntity> GetNeighbours(Position position) =>
        GetNeighbourPositions(position)
            .Where(IsInMap)
            .Select(GetTile)
            .ToList()
    ;

    private IEnumerable<Position> GetNeighbourPositions(Position position)
    {
        yield return position with { RowIndex = position.RowIndex - 1 };
        yield return position with { RowIndex = position.RowIndex + 1 };
        yield return position with { ColumnIndex = position.ColumnIndex - 1 };
        yield return position with { ColumnIndex = position.ColumnIndex + 1 };
    }
}