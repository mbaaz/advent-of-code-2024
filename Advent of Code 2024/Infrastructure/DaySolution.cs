namespace AoC.Y24.Infrastructure;

public abstract class DaySolution(int day) : IDaySolutionDefinition
{
    private const string TEST_ACTIVE_WARNING = " - RUNNING WITH TEST DATA ACTIVE";
    private const string WELCOME_MESSAGE_FORMAT = "Welcome to Day {0}!";

    private const string INPUT_FILE_NAME_FORMAT = """Days\Day{0}\day{0}-{1}.{2}""";
    private const string INPUT_DATA_FILE_NAME = "input";
    private const string INPUT_TEST_FILE_NAME = "test";
    private const string INPUT_FILE_PART_SUFFIX = "-part";

    private const string TIMER_RESULT = "[Code executed for {0}s {1}ms]";

    private const char BORDERED_OUTPUT_CHAR = '#';
    private const string OUTPUT_INDENT = "    ";

    public int Day { get; } = day;

    protected string InputFileNameExtension { get; set; } = "txt";


    protected string GetWelcomeMessage(bool useTestInput) =>
        string.Format(WELCOME_MESSAGE_FORMAT, GetDayNumber()) + (useTestInput ? TEST_ACTIVE_WARNING : string.Empty);

    private string GetDayNumber() => Day.ToString().PadLeft(2, '0');

    private string GetInputFilePath(PuzzlePart? puzzlePart, bool useTestInput) =>
        string.Format(INPUT_FILE_NAME_FORMAT
            , GetDayNumber()
            , (useTestInput ? INPUT_TEST_FILE_NAME : INPUT_DATA_FILE_NAME) + (puzzlePart.HasValue ? INPUT_FILE_PART_SUFFIX + (short)puzzlePart : string.Empty)
            , InputFileNameExtension
        );

    public void Run(Action<OutputMessage> output, bool useTestInput)
    {
        // Print top border
        output(new FullLineOutputMessage(BORDERED_OUTPUT_CHAR));
        BorderedOutput(new(string.Empty));


        // Print Welcome Message
        BorderedOutput(new($"{GetWelcomeMessage(useTestInput)}"));
        
        // Run the puzzle parts
        RunPart(PuzzlePart.Part1, IndentedOutput, useTestInput); // Run part 1
        RunPart(PuzzlePart.Part2, IndentedOutput, useTestInput); // Run part 2

        // Print bottom border
        BorderedOutput(new(string.Empty));
        output(new FullLineOutputMessage(BORDERED_OUTPUT_CHAR));

        return;
        OutputMessage GetBorderedMessage(OutputMessage message) => new WrappedOutputMessage($"{BORDERED_OUTPUT_CHAR} ", $" {BORDERED_OUTPUT_CHAR}", message);
        void BorderedOutput(OutputMessage message) => output(GetBorderedMessage(message));
        void IndentedOutput(OutputMessage message) => output(GetBorderedMessage(new IndentedOutputMessage(message)));
    }

    private void RunPart(PuzzlePart puzzlePart, Action<OutputMessage> output, bool useTestInput)
    {
        // Prepare to run the puzzle part

        var input = GetInput(puzzlePart, useTestInput);

        // Present the puzzle part that is executing
        output(new($"{Environment.NewLine}PART {(short)puzzlePart}"));

        // Run the requested puzzle part
        
        var executionTime = puzzlePart switch
        {
            PuzzlePart.Part1 => RunWithTimer(() => RunPart1(input, IndentedOutput)),
            PuzzlePart.Part2 => RunWithTimer(() => RunPart2(input, IndentedOutput)),
            _ => throw new ArgumentOutOfRangeException(nameof(puzzlePart), puzzlePart, null)
        };

        // Handle the output
        //outputWrapper.WriteOutput((msg) => output(msg, 1), $"{OUTPUT_INDENT}{OUTPUT_INDENT}");

        // Add the execution time to output
        var elapsedSeconds = Math.Floor(executionTime.TotalSeconds);
        var elapsedMilliseconds = executionTime.Milliseconds;
        IndentedOutput(new($"{string.Format(TIMER_RESULT, elapsedSeconds, elapsedMilliseconds)}"));
        
        return;

        void IndentedOutput(OutputMessage message) => output(new IndentedOutputMessage(message));
    }

    public virtual void RunPart1(string[] input, Action<OutputMessage> output)
    {
        output(new("Puzzle Solver has not yet implemented [RunPart1] method!"));
    }

    public virtual void RunPart2(string[] input, Action<OutputMessage> output)
    {
        output(new("Puzzle Solver has not yet implemented [RunPart2] method!"));
    }

    protected string[] GetInput(PuzzlePart puzzlePart, bool useTestInput)
    {
        var partSpecificFilePath = GetInputFilePath(puzzlePart, useTestInput);
        var inputFileFullPath = Path.Combine(Environment.CurrentDirectory, partSpecificFilePath);
        var file = new FileInfo(inputFileFullPath);

        if (!file.Exists)
        {
            var genericFilePath = GetInputFilePath(null, useTestInput);
            inputFileFullPath = Path.Combine(Environment.CurrentDirectory, genericFilePath);
            file = new FileInfo(inputFileFullPath);
        }

        if (!file.Exists)
        {
            throw new Exception("""File "{ inputFileFullPath }" does not exist!""");
        }


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

    private TimeSpan RunWithTimer(Action codeToTime)
    {
        var watch = System.Diagnostics.Stopwatch.StartNew();

        codeToTime();

        watch.Stop();
        return watch.Elapsed;
    }
}