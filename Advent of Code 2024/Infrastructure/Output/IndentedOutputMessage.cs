namespace AoC.Y24.Infrastructure.Output;

public class IndentedOutputMessage(OutputMessage message)
    : WrappedOutputMessage(prefix: INDENTATION, suffix: string.Empty, message)
{
    private const string INDENTATION = "    ";
}