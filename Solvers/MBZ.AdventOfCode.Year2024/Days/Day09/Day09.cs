using System.Text;
using MBZ.AdventOfCode.Core.Configuration;
using MBZ.AdventOfCode.Core.Solvers;

namespace MBZ.AdventOfCode.Year2024.Day09;

// This is my solution to the Advent of Code challenge!
// <see>https://adventofcode.com/2024/day/9</see>
[DaySolution(Day = 9, IsActive = true)]
public class Day09 : DaySolution, IDaySolutionImplementation
{
    [ExpectedResult(testResult: 1928, result: int.MaxValue)]
    public override long RunPart1(bool isTest, string[] input, Action<OutputMessage> output)
    {
        var diskMap = input.ParseToDay09Data();
        if (isTest)
        {
            output(new("Disk Map", string.Join("", diskMap)));
        }

        var blocks = diskMap.ToBlocks();
        if (isTest)
        {
            output(new("Blocks", blocks));
        }

        var compactedBlocks = blocks.CompactBlocks(isTest, output);
        if (isTest)
        {
            output(new("Compacted", compactedBlocks));
        }

        var checkSum = compactedBlocks.CalculateCheckSum();
        output(new("Check Sum", $"{checkSum:n0}"));

        return checkSum;
    }

    [ExpectedResult(testResult: int.MaxValue, result: int.MaxValue)]
    public override long RunPart2(bool isTest, string[] input, Action<OutputMessage> output)
    {
        var data = input.ParseToDay09Data();

        output(new("Result", "[not yet defined]"));
        return -1;
    }
}

public static class Day09Extensions
{
    public static List<int> ParseToDay09Data(this string[] input)
    {
        var result = string.Join("", input)
            .Select(c => int.Parse(c.ToString()))
            .ToList()
        ;
        return result;
    }

    private const char EMPTY_SPACE = '.';

    public static string ToBlocks(this List<int> diskMap)
    {
        if (!diskMap.Any())
        {
            return string.Empty;
        }

        var nextFileID = 0;
        var result = new StringBuilder();

        for (var i = 0; i < diskMap.Count; i++)
        {
            var chr = i % 2 == 0
                ? (nextFileID++).ToString()[0]
                : EMPTY_SPACE 
            ;
            // Empty space
            result.Append(new string(chr, diskMap[i]));
        }

        return result.ToString();
    }

    public static string CompactBlocks(this string blocks, bool isTest, Action<OutputMessage> output)
    {
        var result = blocks.ToCharArray();

        if (isTest)
        {
            output(new("Starting to compact", string.Join(string.Empty, result)));
        }


        var changeMade = true;
        while (changeMade)
        {
            var lastBlockIndex = GetLastBlockIndex(result);
            var firstEmptyIndex = Array.IndexOf(result, EMPTY_SPACE);
            changeMade = 
                lastBlockIndex > 0 && 
                firstEmptyIndex > 0 && 
                lastBlockIndex != firstEmptyIndex && 
                lastBlockIndex > firstEmptyIndex
            ;
            if (changeMade)
            {
                result[firstEmptyIndex] = result[lastBlockIndex];
                result[lastBlockIndex] = EMPTY_SPACE;

                if (isTest)
                {
                    output(new(string.Empty, string.Join(string.Empty, result)));
                }
            }
        }

        return string.Join(string.Empty, result);
    }

    private static int GetLastBlockIndex(char[] array)
    {
        for (var i = array.Length - 1; i >= 0; i--)
        {
            if (array[i] != EMPTY_SPACE)
            {
                return i;
            }
        }

        return -1;
    }

    public static long CalculateCheckSum(this string blocks)
    {
        var result = 0L;
        var blocksArray = blocks.ToCharArray();
        for (var i = 0; i < blocksArray.Length; i++)
        {
            if (blocksArray[i] == EMPTY_SPACE)
            {
                continue;
            }

            result += long.Parse(char.ToString(blocksArray[i])) * (long)i;
        }

        return result;
    }
}

public class Disk
{
    public Disk(string diskMap)
    {
        
    }
}

public record Block(int FileID)
{
}
