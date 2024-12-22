namespace MBZ.AdventOfCode.Core.Output;

public class OutputWrapper(int maxLineLength, Action<string> writer)
{
    private List<OutputMessage> Messages { get; } = [];

    public void AddMessage(params object[] input)
    {
        AddMessage(new OutputMessage(input));
    }

    public void AddMessage(OutputMessage message)
    {
        if (!message.SoftFlush)
        {
            Messages.Add(message);
            return;
        }

        var lastSoftFlushIndex = Messages.Any() == false
            ? 0 
            : Messages.FindLastIndex(0, msg => msg.SoftFlush) + 1
        ;
        
        Messages.Add(message);
        SoftFlush(lastSoftFlushIndex);
    }

    public void SoftFlush(int fromIndex)
    {
        if (!Messages.Any())
            return;

        var twoPartMessagesFirstTextLength = Messages.Max(msg => msg.IfTwoPartsThenFirstPartLength);
        var messages = Messages[fromIndex..];
        foreach (var message in messages)
        {
            message.WriteToOutput(writer, twoPartMessagesFirstTextLength, maxLineLength);
        }

        Clear();
    }

    public void Flush()
    {
        if(!Messages.Any())
            return;

        var lastSoftFlushIndex = Messages.FindLastIndex(0, msg => msg.SoftFlush) + 1;
        if (Messages.Count <= lastSoftFlushIndex)
        {
            Clear();
            return;
        }

        var twoPartMessagesFirstTextLength = Messages.Max(msg => msg.IfTwoPartsThenFirstPartLength);
        var messages = Messages[lastSoftFlushIndex..];
        foreach (var message in messages)
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