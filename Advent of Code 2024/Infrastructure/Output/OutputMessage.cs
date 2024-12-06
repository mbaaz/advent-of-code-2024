namespace AoC.Y24.Infrastructure.Output;

public class OutputMessage
{
    private const char PAD_CHAR = ' ';
    private const string TWO_PARTS_SEPARATOR = ": ";

    private List<string> Parts { get; }

    public virtual int IfTwoPartsThenFirstPartLength => Parts.Count == 2 ? Parts[0].Length : 0;

    public OutputMessage(params object[] parts)
    {
        Parts = parts?
            .Select(item => item?.ToString() ?? string.Empty)
            .ToList() ?? []
        ;

        if(parts == null || !parts.Any())
            Parts.Add(string.Empty);

        if(Parts.Count > 2)
            throw new Exception("Output Message is not defined for more than 2 parts!");
    }

    public virtual void WriteToOutput(Action<string> output, int twoPartMessagesFirstTextLength, int maxLineLength)
    {
        if (Parts.Count == 1)
        {
            var lines = Parts[0].SplitToLines(maxLineLength).ToList();
            foreach (var line in lines)
            {
                output(line);
            }
            return;
        }

        if(Parts.Count != 2)
        {
            throw new Exception("Unknown amount of Parts of OutputMessage!");
        }

        var firstPart = Parts[0];
        var secondPart = Parts[1];

        if(firstPart.Length > maxLineLength)
        {
            throw new Exception("First part of Output Message exceeds max line length - I do not know what to do now!");
        }

        var part1 = firstPart.PadLeft(twoPartMessagesFirstTextLength, PAD_CHAR);
        var part2MaxLength = maxLineLength - part1.Length - TWO_PARTS_SEPARATOR.Length;
        var part2List = secondPart.SplitToLines(part2MaxLength).ToList();
        var part2FollowingLinesPadding = new string(PAD_CHAR, part1.Length + TWO_PARTS_SEPARATOR.Length);
        for(var i=0;i<part2List.Count;i++)
        {
            output((i == 0)
                ? $"{part1}{TWO_PARTS_SEPARATOR}{part2List[i]}"
                : $"{part2FollowingLinesPadding}{part2List[i]}"
            );
        }
    }
}
