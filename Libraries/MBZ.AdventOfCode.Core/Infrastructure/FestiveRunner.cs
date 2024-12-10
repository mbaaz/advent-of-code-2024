using System.Text.RegularExpressions;
using MBZ.AdventOfCode.Core.Configuration;

namespace MBZ.AdventOfCode.Core.Infrastructure;

public class FestiveRunner : IFestiveRunner
{
    private const int MAX_INPUT_TRIES = 3;

    private static readonly Regex InputRegex = new(@"^(?<Exit>exit|[Xx])|(?<Day>[0-9]+)(?<UseTestInput>[Tt])?$");

    private FestiveAppSettings AppSettings { get; }
    private SolverHelper SolverHelper { get; }

    private OutputWrapper Output { get; }
    private string DefaultInput { get; }
    
    public FestiveRunner(
        FestiveAppSettings appSettings,
        SolverHelper solverHelper
    )
    {
        // Prepare output
        Output = new OutputWrapper(
            maxLineLength: Console.WindowWidth - 1,  // Make maxLineLength smaller than window - otherwise if they match, some consoles with omit NewLines at the end of line
            writer: (line) => WriteOutput(line)
        );

        AppSettings = appSettings;
        SolverHelper = solverHelper;

        DefaultInput = $"{SolverHelper.LatestDayWithSolver}T";
    }

    public async Task Run()
    {
        if (!PerformWelcomeChecks())
        {
            return;
        }

        var prevLoopResult = new LoopResult();
        while(!prevLoopResult.Quit)
        {
            prevLoopResult = await RunLoop(prevLoopResult.FailCount);
        }
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
        Output.AddMessage(StringResources.GetWelcomeMessage(AppSettings.Year));

        if (SolverHelper.HasSolvers)
        {
            return true;
        }

        Output.AddMessage(StringResources.NO_SOLVERS_ACTIVE_MESSAGE);
        Output.Flush();

        Quit();
        return false;
    }

    private async Task<LoopResult> RunLoop(int failCount)
    {
        if (failCount > 0)
        {
            if (failCount >= MAX_INPUT_TRIES)
            {
                Output.AddMessage(StringResources.INVALID_INPUT_QUITTING_MESSAGE);
                Output.Flush();

                Quit();
                return new LoopResult(Quit: true);
            }

            Output.AddMessage(StringResources.INVALID_INPUT_TRY_AGAIN_MESSAGE);
        }

        var result = await HandleInput();

        return result switch
        {
            { Quit: true } => new LoopResult(Quit: true),
            { TaskHasRun: false } => new LoopResult(FailCount: failCount + 1),
            _ => new LoopResult()
        };
    }

    private async Task<InputResult> HandleInput()
    {
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
            return new InputResult(Quit: true);
        }

        if (match.Groups["Day"].Success)
        {
            var dayToRun = int.Parse(match.Groups["Day"].Value);
            var useTestInput = match.Groups["UseTestInput"].Success;

            var solver = SolverHelper.GetSolverForDay(dayToRun);
            if (solver == null)
            {
                return new InputResult();
            }

            try
            {
                Output.AddMessage($"{Environment.NewLine}{Environment.NewLine}");
                await solver.Run(Output.AddMessage, useTestInput);
                Output.AddMessage($"{Environment.NewLine}{Environment.NewLine}");
            }
            catch (Exception ex)
            {
                Output.AddMessage(string.Format(StringResources.SOLVER_EXCEPTION_MESSAGE_FORMAT, ex.Message));
            }

            return new InputResult(TaskHasRun: true);
        }

        return new InputResult();
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

    private record InputResult(bool TaskHasRun = false, bool Quit = false) {}
    private record LoopResult(int FailCount = 0, bool Quit = false) {}
}