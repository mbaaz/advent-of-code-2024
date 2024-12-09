namespace MBZ.AdventOfCode.Year2024.Day06;

public class Guard(Point? position, Heading heading)
{
    private static readonly Dictionary<Heading, Point> Movements = new()
    {
        { Heading.Up,    new Point(-1,  0) },
        { Heading.Down,  new Point( 1,  0) },
        { Heading.Left,  new Point( 0, -1) },
        { Heading.Right, new Point( 0,  1) },
    };

    private static Heading GetRotatedHeading(Heading fromHeading)
    {
        return fromHeading switch
        {
            Heading.Up => Heading.Right,
            Heading.Right => Heading.Down,
            Heading.Down => Heading.Left,
            Heading.Left => Heading.Up,

            _ => throw new Exception("Guard Heading is not valid!")
        };
    }

    public Point? Position { get; set; } = position;
    public Heading Heading { get; private set; } = heading;

    public Point? GetNextPosition() =>
        Position?.Move(Movements[Heading]);

    public void Move() =>
        Position = GetNextPosition();
    
    public void Rotate() =>
        Heading = GetRotatedHeading(Heading);

    public override string ToString() =>
        $"{Heading} {Position}";

    public Guard Copy() =>
        new Guard(Position, Heading);
}