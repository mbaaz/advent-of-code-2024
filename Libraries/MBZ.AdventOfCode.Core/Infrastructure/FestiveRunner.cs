using System.Text.RegularExpressions;

namespace MBZ.AdventOfCode.Core.Infrastructure;

public class FestiveRunner : IFestiveRunner
{
    private const int MAX_INPUT_TRIES = 3;

    private static readonly Regex InputRegex = new(@"^(?<Exit>exit|[Xx])|(?<Day>[0-9]+)(?<UseTestInput>[Tt])?$");

    private OutputWrapper Output { get; }
    private SolverHelper SolverHelper { get; }
    private string DefaultInput { get; }
    
    public FestiveRunner()
    {
        // Prepare output
        Output = new OutputWrapper(
            maxLineLength: Console.WindowWidth - 1,  // Make maxLineLength smaller than window - otherwise if they match, some consoles with omit NewLines at the end of line
            writer: (line) => WriteOutput(line)
        );

        SolverHelper = new SolverHelper();

        DefaultInput = $"{SolverHelper.LatestDayWithSolver}T";
    }

    public void Run()
    {
        if (!PerformWelcomeChecks())
        {
            return;
        }

        RunLoop();
    }

    private static void WriteOutput(string line, bool endWithNewLine = true)
    {
        if (endWithNewLine)
        {
            Console.WriteLine(line);
        }
        else
        {
            Console.Write(line);
        }
    }

    private bool PerformWelcomeChecks()
    {
        Output.AddMessage(StringResources.GetWelcomeMessage(2024));

        if (SolverHelper.HasSolvers)
        {
            return true;
        }

        Output.AddMessage(StringResources.NO_SOLVERS_ACTIVE_MESSAGE);
        Output.Flush();

        Quit();
        return false;
    }

    private void RunLoop(int failCount = 0)
    {
        if (failCount > 0)
        {
            if (failCount >= MAX_INPUT_TRIES)
            {
                Output.AddMessage(StringResources.INVALID_INPUT_QUITTING_MESSAGE);
                Output.Flush();

                Quit();
                return;
            }

            Output.AddMessage(StringResources.INVALID_INPUT_TRY_AGAIN_MESSAGE);
        }

        var solverHasRun = HandleInput(out var quit);

        if (quit)
        {
            return;
        }

        // If solver has run - reset try count
        RunLoop(solverHasRun ? 0 : failCount + 1);
    }

    private bool HandleInput(out bool quit)
    {
        quit = false;

        Output.Flush();
        WriteOutput(StringResources.GetGreeting(SolverHelper.DefinedSolversHumanReadable, DefaultInput), endWithNewLine: false); // We need to do like this to make sure no line ending occurs after we wait for input

        var input = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(input))
        {
            input = DefaultInput;
        }

        var match = InputRegex.Match(input);

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

            var solver = SolverHelper.GetSolverForDay(dayToRun);
            if (solver == null)
                return false;

            try
            {
                Output.AddMessage($"{Environment.NewLine}{Environment.NewLine}");
                solver.Run(Output.AddMessage, useTestInput);
                Output.AddMessage($"{Environment.NewLine}{Environment.NewLine}");
            }
            catch (Exception ex)
            {
                Output.AddMessage(string.Format(StringResources.SOLVER_EXCEPTION_MESSAGE_FORMAT, ex.Message));
            }

            return true;
        }

        return false;
    }

    private static void Quit(bool bypassInput = false)
    {
        Console.Write(StringResources.QUITTING_MESSAGE);

        if (!bypassInput)
        {
            Console.Write(StringResources.ENTER_KEY_TO_EXIT_MESSAGE);
            Console.ReadKey();
        }
    }
}