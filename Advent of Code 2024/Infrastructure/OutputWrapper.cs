namespace AoC.Y24.Infrastructure;

public class OutputWrapper(int maxLineLength, Action<string> writer)
{
    private List<OutputMessage> Messages { get; set; } = new List<OutputMessage>();

    public Action<object[]> GetAddOutputAction() => AddMessage;

    public void AddMessage(params object[] input)
    {
        Messages.Add(new OutputMessage(input));
    }

    public void Flush()
    {
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