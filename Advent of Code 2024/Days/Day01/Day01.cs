namespace AoC.Y24.days;

// This is my solution to the Advent of Code challenge!
// <see>https://adventofcode.com/2024/day/1</see>
public class Day01() : DaySolution(day: 1), IDaySolutionImplementation
{
    public bool IsActive => true;

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
        var (list1, list2) = GetListsFromInput(input);

        list1.Sort();
        list2.Sort();
        var diff = list1.Select((t, i) => Math.Abs(t - list2[i])).Sum();

        output($"Part 1: {diff:n0}");
    }

    private void RunPart2(string[] input, Action<string> output)
    {
        var (list1, list2) = GetListsFromInput(input);

        var totalScore = 0;
        var calculatedScores = new Dictionary<int, int>();
        foreach(var value in list1)
        {
            if(!calculatedScores.TryGetValue(value, out var score))
            {
                score = value * list2.Count(val => val == value);
                calculatedScores.Add(value, score);
            }
            totalScore += score;
        }

        output($"Part 2: {totalScore:n0}");
    }

    // ########################################################################################

    private static (List<int>, List<int>) GetListsFromInput(string[] input)
    {
        var list1 = new List<int>();
        var list2 = new List<int>();

        foreach (var row in input)
        {
            if (string.IsNullOrEmpty(row))
            {
                continue;
            }

            var numbers = row
                .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(int.Parse)
                .ToArray()
            ;
            list1.Add(numbers[0]);
            list2.Add(numbers[1]);
        }

        if (list1.Count != list2.Count)
        {
            throw new Exception("List of input numbers are not the same length!");
        }

        return (list1, list2);
    }
}