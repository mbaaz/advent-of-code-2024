namespace AoC.Y24.Infrastructure;

public class OutputWrapper(int maxLineLength)
{
    private List<OutputMessage> Messages { get; set; } = new List<OutputMessage>();

    public Action<object[]> GetAddOutputAction() => AddMessage;

    public void AddMessage(params object[] input)
    {
        Messages.Add(new OutputMessage(input));
    }

    public void WriteOutput(Action<string> output)
    {
        var twoPartMessagesFirstTextLength = Messages.Max(msg => msg.IfTwoPartsThenFirstPartLength);

        
        foreach(var message in Messages)
        {
            message.WriteToOutput(output, twoPartMessagesFirstTextLength, maxLineLength);
        }

        // Reset Messages for future
        Messages.Clear();
    }
}