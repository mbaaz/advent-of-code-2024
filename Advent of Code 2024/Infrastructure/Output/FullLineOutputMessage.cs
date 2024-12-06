namespace AoC.Y24.Infrastructure.Output;

public class FullLineOutputMessage : OutputMessage
{
    private readonly char _charToRepeat;

    public FullLineOutputMessage(char charToRepeat)
    {
        _charToRepeat = charToRepeat;
    }

    public override void WriteToOutput(Action<string> output, int twoPartMessagesFirstTextLength, int maxLineLength)
    {
        output(new string(_charToRepeat, maxLineLength));
    }
}