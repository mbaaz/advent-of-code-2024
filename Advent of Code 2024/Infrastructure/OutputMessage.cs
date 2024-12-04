namespace AoC.Y24.Infrastructure;

public class OutputMessage
{
    private const char PAD_CHAR = ' ';
    private const string TWO_PARTS_SEPARATOR = ": ";

    private List<string> Parts { get; }

    public OutputMessage(params string[] parts)
    {
        Parts = parts.ToList();

        if(!parts.Any())
            Parts.Add(string.Empty);

        if(Parts.Count > 2)
            throw new Exception("Output Message is not defined for more than 2 parts!");
    }

    public IEnumerable<string> ToString(int ifTwoPartsFirstPartLength, int maxLineLength)
    {
        if (Parts.Count == 1)
        {
            yield return Parts.First();
            yield break;
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

        var part1 = firstPart.PadLeft(ifTwoPartsFirstPartLength, PAD_CHAR);
        var part2MaxLength = maxLineLength - part1.Length - TWO_PARTS_SEPARATOR.Length;
        var parts2 = secondPart.SplitToLines(part2MaxLength).ToList();
        for(var i=0;i<parts2.Count;i++)
        {
            yield return (i == 0)
                ? $"{part1}{TWO_PARTS_SEPARATOR}{parts2[i]}"
                : parts2[i]
            ;
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