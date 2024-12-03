using System.Reflection;

namespace AoC.Y24;

public static class Program
{
    private const int MAX_TRIES = 3;
    private const string EXIT = "exit";
    private const string EXIT_SHORT = "x";

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
    private const string GREETING_FORMAT = "Which day do you wish to run? [{0}]: ";


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



    public static void Main(string[] input)
    {
        Console.Write(WELCOME_MESSAGE);

        var solverHelper = new SolverHelper();
        var solvers = solverHelper.GetSolvers();

        if(!solvers.Any())
        {
            Console.Write(NO_SOLVERS_ACTIVE_MESSAGE);
            Quit();
            return;
        }

        var availableSolvers = solvers.Count == 1 ? solvers.First().Key.ToString() : $"{solvers.Keys.Min()} - {solvers.Keys.Max()}";
        var greeting = string.Format(GREETING_FORMAT, availableSolvers);
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
            var dayToRunInput = Console.ReadLine();

            if (
                string.Equals(dayToRunInput, EXIT, StringComparison.InvariantCultureIgnoreCase) ||
                string.Equals(dayToRunInput, EXIT_SHORT, StringComparison.InvariantCultureIgnoreCase)
            )
            {
                Quit(bypassInput: true);
                return;
            }

            IDaySolutionImplementation? solver = null;
            if (string.IsNullOrEmpty(dayToRunInput))
            {
                solver = solvers[solvers.Keys.Max()];
            }
            else if (int.TryParse(dayToRunInput, out var dayToRun) && solvers.TryGetValue(dayToRun, out var solver1))
            {
                solver = solver1;
            }
                
            if (solver != null)
            {
                @try = 0;
                try
                {
                    solver?.Run(Console.WriteLine);
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