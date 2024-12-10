namespace MBZ.AdventOfCode.Year2024.Day01;

public static class Day01Extensions
{
    public static (List<int>, List<int>) ParseDay01Input(this string[] input)
    {
        var parsedItems = input.Select(ParseRow).ToList();
        var list1 = parsedItems.Select(row => row.Item1).ToList();
        var list2 = parsedItems.Select(row => row.Item2).ToList();

        if (list1.Count != list2.Count)
        {
            throw new Exception("List of input numbers are not the same length!");
        }

        return (list1, list2);
    }

    private static (int, int) ParseRow(string input)
    {
        var numbers = input
                .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(int.Parse)
                .ToArray()
            ;

        if (numbers.Length != 2)
        {
            throw new Exception("Expected two numbers per input!");
        }

        return (numbers[0], numbers[1]);
    }

    public static int CalculateTotalDistanceToList(this List<int> list1, List<int> list2)
    {
        var innerList1 = list1.ToList();
        innerList1.Sort();

        var innerList2 = list2.ToList();
        innerList2.Sort();

        
        var distance = innerList1.Select((t, i) => Math.Abs(t - innerList2[i])).Sum();
        return distance;
    }

    public static int CalculateSimilarityScore(this List<int> list1, List<int> list2)
    {
        var totalScore = 0;
        var calculatedScores = new Dictionary<int, int>();
        foreach (var value in list1)
        {
            if (!calculatedScores.TryGetValue(value, out var score))
            {
                score = value * list2.Count(val => val == value);
                calculatedScores.Add(value, score);
            }
            totalScore += score;
        }

        return totalScore;
    }
}