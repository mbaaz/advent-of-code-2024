namespace MBZ.AdventOfCode.Domain;

public record Position(int RowIndex, int ColumnIndex)
{
    public Position Move(Position movement) => 
        Add(movement);

    public Position DistanceTo(Position otherPosition) =>
        new(otherPosition.RowIndex - RowIndex, otherPosition.ColumnIndex - ColumnIndex);

    public Position Add(Position otherPosition) =>
        new(RowIndex + otherPosition.RowIndex, ColumnIndex + otherPosition.ColumnIndex);

    public Position Subtract(Position otherPosition) =>
        new(RowIndex - otherPosition.RowIndex, ColumnIndex - otherPosition.ColumnIndex);

    public override string ToString() =>
        $"{{{RowIndex}:{ColumnIndex}}}";
}