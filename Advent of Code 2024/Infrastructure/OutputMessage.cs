namespace AoC.Y24.Infrastructure;

public class OutputMessage
{
    private const char PAD_CHAR = ' ';
    private const string TWO_PARTS_SEPARATOR = ": ";

    private List<string> Parts { get; }

    public int IfTwoPartsThenFirstPartLength => Parts.Count == 2 ? Parts[0].Length : 0;

    public OutputMessage(params object[] parts)
    {
        Parts = parts
            .Select(item => item?.ToString() ?? string.Empty)
            .ToList()
        ;

        if(!parts.Any())
            Parts.Add(string.Empty);

        if(Parts.Count > 2)
            throw new Exception("Output Message is not defined for more than 2 parts!");
    }

    public void WriteToOutput(Action<string> output, int twoPartMessagesFirstTextLength, int maxLineLength)
    {
        if (Parts.Count == 1)
        {
            output(Parts.First());
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
        var parts2 = secondPart.SplitToLines(part2MaxLength).ToList();
        var part2FollowingLinesPadding = new string(PAD_CHAR, part1.Length + TWO_PARTS_SEPARATOR.Length);
        for(var i=0;i<parts2.Count;i++)
        {
            output((i == 0)
                ? $"{part1}{TWO_PARTS_SEPARATOR}{parts2[i]}"
                : $"{part2FollowingLinesPadding}{parts2[i]}"
            );
        }
    }
}

//public class OutputMessage(string text1, string? text2)
//{
//    private const string TWO_LINE_FORMAT = "> {0}: {1}";
//    private const char PAD_CHAR = ' ';

//    public string Text1 { get; set; } = text1;
//    public string? Text2 { get; set; } = text2;

//    private bool HasText1 => !string.IsNullOrWhiteSpace(Text1);
//    private bool HasText2 => !string.IsNullOrWhiteSpace(Text2);

//    public bool HasText => HasText1 || HasText2;
//    public bool IsTwoLines => HasText1 && HasText2;


//    public OutputMessage(string output) 
//        : this(
//          output,
//          string.Empty
//        )
//    {
//    }

//    public OutputMessage(IFormattable output) 
//        : this(
//            output.ToString() ?? string.Empty,
//            string.Empty
//        )
//    {
//    }

//    public OutputMessage(IFormattable text1, IFormattable text2)
//        : this(
//            text1.ToString() ?? string.Empty, 
//            text2.ToString() ?? string.Empty
//        )
//    {
//    }

//    public OutputMessage(string text1, IFormattable text2)
//        : this(
//            text1,
//            text2.ToString() ?? string.Empty
//        )
//    {
//    }

//    public OutputMessage(IFormattable text1, string text2)
//        : this(
//            text1.ToString() ?? string.Empty,
//            text2
//        )
//    {
//    }

//    public string ToString(int twoLineTextFirstTextLength, int maxLineLength)
//    {
//        if(!IsTwoLines)
//        {
//            return Text1;
//        }

//        var text1 = Text1.PadLeft(twoLineTextFirstTextLength, PAD_CHAR);
//        return string.Format(TWO_LINE_FORMAT, text1, Text2);
//    }
//}