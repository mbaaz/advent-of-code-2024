namespace AoC.Y24.days;

// This is my solution to the Advent of Code challenge!
// <see>https://adventofcode.com/2024/day/2</see>
public class Day02() : DaySolution(day: 2), IDaySolutionImplementation
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
        RunWithTimer(output, () =>
        {
            var safeReports = input
                .Sum(row => IsSafeReport(GetValues(row)) ? 1 : 0)
            ;

            output($"""
PART 1
    Number of safe reports: {safeReports:n0}
""");
        });
    }

    private void RunPart2(string[] input, Action<string> output)
    {
        RunWithTimer(output, () =>
        {
            var safeReports = input
                .Sum(row => IsSafeReport(GetValues(row), useProblemDampener: true) ? 1 : 0)
            ;

            output($"""
PART 2
    Number of safe reports with Problem Dampener active: {safeReports:n0}
""");
        });
    }

    // ########################################################################################

    private static int[] GetValues(string input)
    {
        return input
            .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(int.Parse)
            .ToArray()
        ;
    }

    private static bool IsSafeReport(int[] values, bool useProblemDampener = false)
    {
        var rowChange = ChangeType.NoChange;
        for (var i = 1; i < values.Length; i++)
        {
            if(IsSafeChange(values[i - 1], values[i], rowChange, out rowChange))
            {
                continue;
            }

            if(!useProblemDampener)
            {
                return false;
            }

            return IsSafeReportWithProblemDampenerActive(values);
        }

        return true;
    }

    private static bool IsSafeReportWithProblemDampenerActive(int[] originalValues)
    {
        var removeIndex = 0;
        while (removeIndex < originalValues.Length)
        {
            var newValues = new List<int>(originalValues);
            newValues.RemoveAt(removeIndex);

            if (IsSafeReport(newValues.ToArray()))
            {
                return true;
            }
            
            removeIndex++;
        }

        return false;
    }

    /// <summary>
    /// Determine if a change of value is determined to be "Safe"
    /// </summary>
    /// <param name="val1">First value</param>
    /// <param name="val2">Second value</param>
    /// <param name="acceptedChangeType">Previous change type for row. This will help determine if values follow the same type of change</param>
    /// <param name="changeType">The change type of the input values</param>
    /// <returns>Boolean indicating if safe</returns>
    private static bool IsSafeChange(int val1, int val2, ChangeType acceptedChangeType, out ChangeType changeType)
    {
        if(val1 == val2)
        {
            changeType = ChangeType.NoChange;
            return false;
        }

        changeType = val1 > val2 ? ChangeType.Decrease : ChangeType.Increase;
        return 
            (acceptedChangeType == ChangeType.NoChange || changeType == acceptedChangeType) && 
            Math.Abs(val2 - val1) <= 3
        ;
    }

    private enum ChangeType: short
    {
        Increase = 1,
        Decrease = 2,
        NoChange = 3,
    }
}