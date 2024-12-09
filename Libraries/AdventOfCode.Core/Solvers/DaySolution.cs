﻿using System.Diagnostics;
using System.Reflection;
using MBZ.AdventOfCode.Core.Infrastructure;
using MBZ.AdventOfCode.Core.Input;

namespace MBZ.AdventOfCode.Core.Solvers;

public abstract class DaySolution(int day) : IDaySolutionDefinition
{
    private const string TEST_ACTIVE_WARNING = " - RUNNING WITH TEST DATA ACTIVE";
    private const string WELCOME_MESSAGE_FORMAT = "Welcome to Day {0}!";

    private const string TIMER_RESULT = "[Code executed for {0}s {1}ms]";

    private const char BORDERED_OUTPUT_CHAR = '#';

    public int Day { get; } = day;

    protected string GetWelcomeMessage(bool useTestInput) =>
        string.Format(WELCOME_MESSAGE_FORMAT, Day.ToString().PadLeft(2, '0')) + (useTestInput ? TEST_ACTIVE_WARNING : string.Empty);

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
        // Present the puzzle part that is executing
        output(new($"{Environment.NewLine}PART {(short)puzzlePart}"));

        // Prepare to run the puzzle part
        var inputWrapper = InputHelper.GetInput(Day, puzzlePart, useTestInput);

        // Write info about input data
        IndentedOutput(new(inputWrapper switch
        {
            { IsEmpty: false } => $"""Using input from file "{inputWrapper.FileName}"!""",
            { IsEmpty: true, IsFetched: false } => $"""Input file "{inputWrapper.FileName}" is empty!""",
            { IsEmpty: true, IsFetched: true }  => $"""Input file "{inputWrapper.FileName}" is empty - tried to load from website but apparently failed (check sessionID possibly)!""",
            _ => throw new UnreachableException("Input wrapper should not ever be in this state!")
        }));

        // Run the requested puzzle part
        Func<bool, string[], Action<OutputMessage>, int> puzzlePartRunner = puzzlePart switch
        {
            PuzzlePart.Part1 => RunPart1,
            PuzzlePart.Part2 => RunPart2,
            _ => throw new ArgumentOutOfRangeException(nameof(puzzlePart), puzzlePart, null)
        };

        var expectedResultAttribute = puzzlePartRunner.GetMethodInfo().GetCustomAttribute<ExpectedResultAttribute>();
        var expectedResult = expectedResultAttribute == null 
            ? int.MinValue
            : useTestInput
                ? expectedResultAttribute.TestResult
                : expectedResultAttribute.Result
        ;


        var executionTime = RunWithTimer(expectedResult, () => puzzlePartRunner(useTestInput, inputWrapper.Input, IndentedOutput), HandlePostRunResult, HandleException);

        // Add the execution time to output
        var elapsedSeconds = Math.Floor(executionTime.TotalSeconds);
        var elapsedMilliseconds = executionTime.Milliseconds;
        IndentedOutput(new($"{string.Format(TIMER_RESULT, elapsedSeconds, elapsedMilliseconds)}"));
        
        
        return;

        void IndentedOutput(OutputMessage message) => output(new IndentedOutputMessage(message));
        void HandleException(Exception exception) => IndentedOutput(new($"An Exception was thrown: {exception.Message}"));
        void HandlePostRunResult(PostPuzzleSolverRunResult result)
        {
            output(new("")); // Make a new line before
            if(result.Success)
            {
                IndentedOutput(new($"""SUCCESS - Result "{result.Result:n0}" match expected output!"""));
                return;
            }

            if(result.Expected == int.MinValue)
            {
                IndentedOutput(new($"""Perhaps with "{result.Result:n0}" you have found the solution! Puzzle has no expected value yet!"""));
                return;
            }


            IndentedOutput(new($"""BOBBINS! The result "{result.Result:n0}" does not match the expected "{result.Expected:n0}"."""));
        }
    }

    [ExpectedResult(testResult: int.MinValue, result: int.MinValue)]
    public virtual int RunPart1(bool isTest, string[] input, Action<OutputMessage> output)
    {
        output(new("Puzzle Solver has not yet implemented [RunPart1] method!"));
        return -1;
    }

    [ExpectedResult(testResult: int.MinValue, result: int.MinValue)]
    public virtual int RunPart2(bool isTest, string[] input, Action<OutputMessage> output)
    {
        output(new("Puzzle Solver has not yet implemented [RunPart2] method!"));
        return -1;
    }

    private TimeSpan RunWithTimer(int expectedResult, Func<int> codeToTime, Action<PostPuzzleSolverRunResult> HandleResult, Action<Exception> handleException)
    {
        var watch = System.Diagnostics.Stopwatch.StartNew();

        try
        {
            var result = codeToTime();
            watch.Stop();

            var postRunResult = new PostPuzzleSolverRunResult(expectedResult, result);
            HandleResult(postRunResult);
        }
        catch(Exception ex)
        {
            watch.Stop();
            handleException(ex);
        }

        return watch.Elapsed;
    }
}