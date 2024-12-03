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
{1}

""";

    private const string INPUT_FILE_NAME_FORMAT = """Days\Day{0}\day{0}-{1}.{2}""";
    private const string INPUT_DATA_FILE_NAME = "input";
    private const string INPUT_TEST_FILE_NAME = "test";

    public int Day { get; } = day;
    public bool UseTestFile { get; protected set; } = false;

    protected string InputFileNameExtension { get; set; } = "txt";

    
    protected string GetWelcomeMessage => string.Format(WELCOME_MESSAGE_FORMAT
        , GetDayNumber
        , UseTestFile ? TEST_ACTIVE_WARNING : string.Empty
    );

    private string GetDayNumber => Day.ToString().PadLeft(2, '0');

    private string GetInputFilePath => string.Format(INPUT_FILE_NAME_FORMAT
        , GetDayNumber
        , UseTestFile ? INPUT_TEST_FILE_NAME : INPUT_DATA_FILE_NAME
        , InputFileNameExtension
    );

    protected string[] GetInput()
    {
        var inputFileFullPath = Path.Combine(Environment.CurrentDirectory, GetInputFilePath);
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