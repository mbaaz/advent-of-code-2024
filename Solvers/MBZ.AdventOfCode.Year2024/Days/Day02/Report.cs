namespace MBZ.AdventOfCode.Year2024.Day02;

public class Report(byte[] levels)
{
    public bool IsSafeReport(bool useProblemDampener = false) =>
        IsSafeReport(levels, useProblemDampener);
    
    private static bool IsSafeReport(byte[] levels, bool useProblemDampener = false)
    {
        var rowChange = ChangeType.NoChange;
        for (var i = 1; i < levels.Length; i++)
        {
            if (IsSafeChange(levels[i - 1], levels[i], rowChange, out rowChange))
            {
                continue;
            }

            if (!useProblemDampener)
            {
                return false;
            }

            return IsSafeReportWithProblemDampenerActive(levels);
        }

        return true;
    }

    private static bool IsSafeReportWithProblemDampenerActive(byte[] originalValues)
    {
        var removeIndex = 0;
        while (removeIndex < originalValues.Length)
        {
            var newValues = originalValues.ToList();
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
        if (val1 == val2)
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
}