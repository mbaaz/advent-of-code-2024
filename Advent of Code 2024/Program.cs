using System.Text.RegularExpressions;

namespace AoC.Y24;

public static class Program
{
    private const int MAX_TRIES = 3;

    private const string WELCOME_MESSAGE = """
Welcome to Advent of Code 2024!


""";
    private const string NO_SOLVERS_ACTIVE_MESSAGE = """
No solvers are active yet - if you would attempt any puzzle first
(and mark as active) then please come back again soon to try me!


""";
    private const string QUITTING_MESSAGE = """

Thanks for this time!

""";
    private const string ENTER_KEY_TO_EXIT_MESSAGE = "Press any key to exit: ";
    private const string GREETING_FORMAT = """
Solvers for the following days puzzles are defined: {0}
To use test input rather than puzzle input, suffix input with [Tt].
Enter "exit" or [Xx] to quit.
Which day do you wish to run? [{1}]: 
""";


    private const string INVALID_INPUT_TRY_AGAIN_MESSAGE = """
Invalid input. Try again...


""";
    private const string INVALID_INPUT_QUITTING_MESSAGE = """
Invalid input.
Maximum number of invalid tries reached, quitting!


""";

    private const string SOLVER_EXCEPTION_MESSAGE_FORMAT = """
Exception was thrown in solver:
    {0}


""";

    private static readonly Regex InputRegex = new(@"^(?<Exit>exit|[Xx])|(?<Day>[0-9]+)(?<UseTestInput>[Tt])?$");

    public static void Output(string message)
    {
        Console.Write(message);
    }

    public static void Main(string[] input)
    {
        // Prepare Console
        Console.WindowWidth = 150;
        Console.WindowHeight = 60;

        // Prepare output
        var outputWrapper = new OutputWrapper();
        var output = outputWrapper.GetAddOutputAction();

        output(new(WELCOME_MESSAGE));

        var solverHelper = new SolverHelper();

        if(!solverHelper.HasSolvers)
        {
            Output(NO_SOLVERS_ACTIVE_MESSAGE);
            Quit();
            return;
        }

        var defaultInput = $"{solverHelper.LatestDayWithSolver}T";

        var greeting = string.Format(GREETING_FORMAT, solverHelper.DefinedSolversHumanReadable, defaultInput);
        var @try = 0;

        while (true)
        {
            if (@try > 0)
            {
                if (@try < MAX_TRIES)
                {
                    Output(INVALID_INPUT_TRY_AGAIN_MESSAGE);
                }
                else
                {
                    Output(INVALID_INPUT_QUITTING_MESSAGE);
                    Quit();
                    return;
                }
            }

            var solverHasRun = HandleInput(solverHelper, greeting, defaultInput, out var quit);
            @try = solverHasRun ? 0 : @try + 1; // If solver has run - reset try count

            if(quit)
            {
                return;
            }
        }
    }
     
    private static bool HandleInput(SolverHelper solverHelper, string greeting, string defaultInput, out bool quit)
    {
        quit = false;
        Output(greeting);

        var dayToRunInput = Console.ReadLine() ?? defaultInput;
        if (string.IsNullOrWhiteSpace(dayToRunInput))
        {
            dayToRunInput = defaultInput;
        }

        var match = InputRegex.Match(dayToRunInput);

        if (match.Groups["Exit"].Success)
        {
            Quit(bypassInput: true);
            quit = true;
            return false;
        }

        if (match.Groups["Day"].Success)
        {
            var dayToRun = int.Parse(match.Groups["Day"].Value);
            var useTestInput = match.Groups["UseTestInput"].Success;

            var solver = solverHelper.GetSolverForDay(dayToRun);
            if (solver == null)
                return false;

            try
            {
                var maxWidth = Console.WindowWidth - 1; // Make maxWidth smaller than window - otherwise if they match some consoles with omit NewLines at the end of input
                solver.Run(Output, maxWidth, useTestInput);
                Output($"{Environment.NewLine}{Environment.NewLine}");
            }
            catch (Exception ex)
            {
                Output(string.Format(SOLVER_EXCEPTION_MESSAGE_FORMAT, ex.Message));
            }

            return true;
        }

        return false;
    }

    private static void Quit(bool bypassInput = false)
    {
        Output(QUITTING_MESSAGE);

        if(!bypassInput)
        {
            Output(ENTER_KEY_TO_EXIT_MESSAGE);
            Console.ReadKey();
        }
    }
}