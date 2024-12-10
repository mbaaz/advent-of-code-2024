namespace MBZ.AdventOfCode.Core.Output;

public class OutputWrapper(int maxLineLength, Action<string> writer)
{
    private List<OutputMessage> Messages { get; } = [];

    public void AddMessage(params object[] input)
    {
        Messages.Add(new OutputMessage(input));
    }

    public void AddMessage(OutputMessage message)
    {
        Messages.Add(message);
    }

    public void Flush()
    {
        if(!Messages.Any())
            return;

        var twoPartMessagesFirstTextLength = Messages.Max(msg => msg.IfTwoPartsThenFirstPartLength);

        foreach(var message in Messages)
        {
            message.WriteToOutput(writer, twoPartMessagesFirstTextLength, maxLineLength);
        }

        Clear();
    }

    public void Clear()
    {
        // Reset Messages for future
        Messages.Clear();
    }
}