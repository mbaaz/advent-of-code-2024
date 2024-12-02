namespace AoC.Y24.days;

// This is my solution to the Advent of Code challenge!
// <see>https://adventofcode.com/2024/day/5</see>
public class Day05() : DaySolution(day: 5), IDaySolutionImplementation
{
    public bool IsActive => false;

    public void Run(Action<string> output)
    {
        UseTestFile = false;

        output(GetWelcomeMessage);
        var input = GetInput();
        RunPart1(input, output);
        RunPart2(input, output);
    }

    private void RunPart1(string[] input, Action<string> output)
    {

        output($"Part 1: ");
    }

    private void RunPart2(string[] input, Action<string> output)
    {

        output($"Part 2: ");
    }

    // ########################################################################################
}