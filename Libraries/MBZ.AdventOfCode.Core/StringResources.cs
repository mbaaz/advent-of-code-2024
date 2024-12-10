namespace MBZ.AdventOfCode.Core;

public static class StringResources
{
    public static string GetWelcomeMessage(int year) => string.Format(WELCOME_MESSAGE, year);
    private const string WELCOME_MESSAGE = 
        """
        Welcome to Advent of Code {0}!
        
        
        """;

    public const string NO_SOLVERS_ACTIVE_MESSAGE = 
        """
        No solvers are active yet - if you would attempt any puzzle first
        (and mark as active) then please come back again soon to try me!


        """;

    public const string QUITTING_MESSAGE = 
        """

        Thanks for this time!

        """;

    public const string ENTER_KEY_TO_EXIT_MESSAGE = 
        "Press any key to exit: ";

    public static string GetGreeting(string definedSolvers, string defaultInput) => string.Format(GREETING_FORMAT, definedSolvers, defaultInput);
    private const string GREETING_FORMAT = 
        """
        Solvers for the following days puzzles are defined: {0}
        To use test input rather than puzzle input, suffix input with [Tt].
        Enter "exit" or [Xx] to quit.
        Which day do you wish to run? [{1}]: 
        """;

    public const string INVALID_INPUT_TRY_AGAIN_MESSAGE = 
        """

        INVALID INPUT. TRY AGAIN...


        """;
    
    public const string INVALID_INPUT_QUITTING_MESSAGE = 
        """

        INVALID INPUT.
        Maximum number of invalid tries reached, quitting!


        """;

    public const string SOLVER_EXCEPTION_MESSAGE_FORMAT = 
        """
        Exception was thrown in solver:
            {0}


        """;
}