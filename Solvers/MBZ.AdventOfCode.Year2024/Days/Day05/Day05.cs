using MBZ.AdventOfCode.Core.Solvers;

namespace MBZ.AdventOfCode.Year2024.Day05;

// This is my solution to the Advent of Code challenge!
// <see>https://adventofcode.com/2024/day/5</see>
[DaySolution(Day = 5, IsActive = true)]
public class Day05 : DaySolution, IDaySolutionImplementation
{
    [ExpectedResult(testResult: 143, result: 4814)]
    public override int RunPart1(bool isTest, string[] input, Action<OutputMessage> output)
    {
        var (rules, updates) = input.ParseToDay05Data();

        output(new("Number of rules", $"{rules.Count:n0}"));
        output(new("Number of updates", $"{updates.Count:n0}"));

        var correctUpdates = updates.Where(rules.IsValidUpdate).ToList();

        if(isTest)
        {
            foreach(var update in correctUpdates)
            {
                output(new("Correct update", $"{update}"));
            }
        }

        var result = correctUpdates.Sum(update => update.GetMiddlePage());
        output(new("Result", $"{result:n0}"));
        return result;
    }

    [ExpectedResult(testResult: 123, result: 5448)]
    public override int RunPart2(bool isTest, string[] input, Action<OutputMessage> output)
    {
        var (rules, updates) = input.ParseToDay05Data();
        var incorrectUpdates = updates.Where(rules.IsNotValidUpdate).Select(upd => new { Invalid = upd, Fixed = rules.FixIncorrectUpdate(upd) }).ToList();

        if(isTest)
        {
            foreach(var update in incorrectUpdates)
            {
                output(new("Incorrect update", $"{update.Invalid} ---> {update.Fixed}"));
            }
        }

        var result = incorrectUpdates.Sum(update => update.Fixed.GetMiddlePage());
        output(new("Result", $"{result:n0}"));
        return result;
    }
}