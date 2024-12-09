namespace MBZ.AdventOfCode.Year2024.Day06;

public record Point(int RowIndex, int ColumnIndex)
{
    public Point Move(Point movement) => 
        new(RowIndex + movement.RowIndex, ColumnIndex + movement.ColumnIndex);

    public override string ToString() =>
        $"{{{RowIndex}:{ColumnIndex}}}";
}