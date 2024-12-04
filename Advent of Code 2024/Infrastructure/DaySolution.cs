namespace AoC.Y24.Infrastructure;

public abstract class DaySolution(int day) : IDaySolutionDefinition
{
    private const string TEST_ACTIVE_WARNING = """

#######################################
### RUNNING WITH TEST DATA ACTIVE ! ###
#######################################

""";
    private const string WELCOME_MESSAGE_FORMAT = """

Welcome to Day {0}!

""";

    private const string INPUT_FILE_NAME_FORMAT = """Days\Day{0}\day{0}-{1}.{2}""";
    private const string INPUT_DATA_FILE_NAME = "input";
    private const string INPUT_TEST_FILE_NAME = "test";
    private const string INPUT_FILE_PART_SUFFIX = "-part";

    private const string TIMER_RESULT = """
[Code executed for {0}s {1}ms]

""";

    public int Day { get; } = day;

    protected string InputFileNameExtension { get; set; } = "txt";

    
    protected string GetWelcomeMessage => string.Format(WELCOME_MESSAGE_FORMAT, GetDayNumber);

    private string GetDayNumber => Day.ToString().PadLeft(2, '0');

    private string GetInputFilePath(int? part, bool useTestInput) => string.Format(INPUT_FILE_NAME_FORMAT
        , GetDayNumber
        , (useTestInput ? INPUT_TEST_FILE_NAME : INPUT_DATA_FILE_NAME) + (part.HasValue ? INPUT_FILE_PART_SUFFIX + part : string.Empty)
        , InputFileNameExtension
    );

    public virtual void Run(Action<string> output, bool useTestInput)
    {
        output(GetWelcomeMessage);
        if(useTestInput)
        {
            output(TEST_ACTIVE_WARNING);
        }


        // Run part 1
        {
            var input = GetInput(1, useTestInput);
            RunPart1(input, output);
        }

        // Run part 2
        {
            var input = GetInput(2, useTestInput);
            RunPart2(input, output);
        }
    }

    public abstract void RunPart1(string[] input, Action<string> output);
    public abstract void RunPart2(string[] input, Action<string> output);

    protected string[] GetInput(int runningPuzzlePart, bool useTestInput)
    {
        if(runningPuzzlePart != 1 && runningPuzzlePart != 2)
        {
            throw new Exception("The only known puzzle parts are 1 and 2!");
        }

        var partSpecificFilePath = GetInputFilePath(runningPuzzlePart, useTestInput);
        var inputFileFullPath = Path.Combine(Environment.CurrentDirectory, partSpecificFilePath);
        var file = new FileInfo(inputFileFullPath);

        if(!file.Exists)
        {
            var genericFilePath = GetInputFilePath(null, useTestInput);
            inputFileFullPath = Path.Combine(Environment.CurrentDirectory, genericFilePath);
            file = new FileInfo(inputFileFullPath);
        }

        if(!file.Exists)
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

    protected void RunWithTimer(Action<string> output, Action codeToTime)
    {
        var watch = System.Diagnostics.Stopwatch.StartNew();

        codeToTime();

        watch.Stop();
        var elapsed = watch.Elapsed;
        var elapsedSeconds = Math.Floor(elapsed.TotalSeconds);
        var elapsedMilliseconds = elapsed.Milliseconds;
        output(string.Format(TIMER_RESULT, elapsedSeconds, elapsedMilliseconds));
    }
}