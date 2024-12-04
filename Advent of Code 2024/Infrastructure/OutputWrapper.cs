namespace AoC.Y24.Infrastructure;

public class OutputWrapper
{
    private List<OutputMessage> Messages { get; set; } = new List<OutputMessage>();

    public Action<string[]> GetAddOutputAction() => (string[] messages) => Messages.Add(new OutputMessage(messages));

    public void AddMessage(params IFormattable[] input)
    {
        var outputMessage = new OutputMessage(input);
        Messages.Add(outputMessage);
    }

    public void WriteOutput(Action<string> output, int maxLineLength)
    {
        var twoLineMessagesFirstTextLengths = Messages
            .Where(msg => msg.IsTwoLines)
            .Select(msg => msg.Text1.Length)
            .ToList()
        ;

        var twoLineMessagesFirstTextLength = twoLineMessagesFirstTextLengths.Any()
            ? twoLineMessagesFirstTextLengths.Max()
            : 0
        ;
        foreach(var msg in Messages)
        {
            output(msg.ToString(twoLineMessagesFirstTextLength, maxLineLength));
        }

        // Reset Messages for future
        Messages.Clear();
    }
}