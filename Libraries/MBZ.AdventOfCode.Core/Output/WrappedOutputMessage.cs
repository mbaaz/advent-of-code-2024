namespace MBZ.AdventOfCode.Core.Output;

public class WrappedOutputMessage : OutputMessage
{
    private const char LINE_PADDING = ' ';

    private readonly string _prefix;
    private readonly string _suffix;
    private readonly OutputMessage _wrappedMessage;

    public override int IfTwoPartsThenFirstPartLength => _wrappedMessage.IfTwoPartsThenFirstPartLength;
    public override bool SoftFlush => _wrappedMessage.SoftFlush;

    public WrappedOutputMessage(string prefix, string suffix, OutputMessage wrappedMessage)
    {
        _prefix = prefix;
        _suffix = suffix;
        _wrappedMessage = wrappedMessage;
    }

    public override void WriteToOutput(Action<string> output, int twoPartMessagesFirstTextLength, int maxLineLength)
    {
        var innerMaxLineLength = (maxLineLength - _prefix.Length - _suffix.Length);

        _wrappedMessage.WriteToOutput(
            output: InnerOutput,
            twoPartMessagesFirstTextLength,
            maxLineLength: innerMaxLineLength
        );
        return;

        void InnerOutput(string message) => output($"{_prefix}{message.PadRight(innerMaxLineLength, LINE_PADDING)}{_suffix}");
    }
}