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

    public static void Main(string[] input)
    {
        Console.Write(WELCOME_MESSAGE);

        var solverHelper = new SolverHelper();

        if(!solverHelper.HasSolvers)
        {
            Console.Write(NO_SOLVERS_ACTIVE_MESSAGE);
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
                    Console.Write(INVALID_INPUT_TRY_AGAIN_MESSAGE);
                }
                else
                {
                    Console.Write(INVALID_INPUT_QUITTING_MESSAGE);
                    Quit();
                    return;
                }
            }

            @try++;

            Console.Write(greeting);
            var dayToRunInput = Console.ReadLine() ?? defaultInput;
            if(string.IsNullOrWhiteSpace(dayToRunInput))
            {
                dayToRunInput = defaultInput;
            }

            var match = InputRegex.Match(dayToRunInput);

            if (match.Groups["Exit"].Success)
            {
                Quit(bypassInput: true);
                return;
            }

            if(match.Groups["Day"].Success)
            {
                var dayToRun = int.Parse(match.Groups["Day"].Value);
                var useTestInput = match.Groups["UseTestInput"].Success;

                var solver = solverHelper.GetSolverForDay(dayToRun);
                if(solver == null)
                    continue;

                @try = 0;
                try
                {
                    solver?.Run(Console.WriteLine, useTestInput);
                    Console.Write("\n\n");
                }
                catch (Exception ex)
                {
                    Console.Write(SOLVER_EXCEPTION_MESSAGE_FORMAT, ex.Message);
                }
            }
        }
    }
     
    private static void Quit(bool bypassInput = false)
    {
        Console.Write(QUITTING_MESSAGE);

        if(!bypassInput)
        {
            Console.Write(ENTER_KEY_TO_EXIT_MESSAGE);
            Console.ReadKey();
        }
    }
}