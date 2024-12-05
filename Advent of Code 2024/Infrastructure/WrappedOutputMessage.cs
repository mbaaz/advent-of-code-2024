namespace AoC.Y24.Infrastructure;

public class WrappedOutputMessage : OutputMessage
{
    private readonly char _charToWrapWith;
    private readonly OutputMessage _wrappedMessage;

    public WrappedOutputMessage(char charToWrapWith, OutputMessage wrappedMessage)
    {
        _charToWrapWith = charToWrapWith;
        _wrappedMessage = wrappedMessage;
    }

    public override void WriteToOutput(Action<string> output, int twoPartMessagesFirstTextLength, int maxLineLength)
    {
        base.WriteToOutput(
            output: WrappedOutput, 
            twoPartMessagesFirstTextLength,
            maxLineLength: maxLineLength - 4 // Remove 4 since this is the length of padding what we wrap with
        );
        return;
        void WrappedOutput(string message) => output($"{_charToWrapWith} {message} {_charToWrapWith}");
    }
}