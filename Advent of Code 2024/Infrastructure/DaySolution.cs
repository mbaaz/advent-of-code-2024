namespace AoC.Y24.Infrastructure;

public abstract class DaySolution(int day) : IDaySolutionDefinition
{
    public int Day { get; } = day;
    public bool UseTestFile { get; protected set; } = false;

    protected virtual string InputFileNameExtension => "txt";

    protected string GetWelcomeMessage => $"Welcome to Day {Day.ToString().PadLeft(2, '0')}!\n";
    private string InputFileNameSuffix => UseTestFile ? "-test" : "-input";
    private string InputFileName => $"Days\\Day01\\day{Day.ToString().PadLeft(2, '0')}{InputFileNameSuffix}.{InputFileNameExtension}";

    protected string[] GetInput()
    {
        var inputFileFullPath = Path.Combine(Environment.CurrentDirectory, InputFileName);
        try
        {
            var input = File.ReadAllLines(inputFileFullPath);
            return input;
        }
        catch (Exception)
        {
            return [];
        }
    }
}