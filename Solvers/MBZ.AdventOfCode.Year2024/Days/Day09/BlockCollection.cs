using System.Text;

namespace MBZ.AdventOfCode.Year2024.Day09;

public record BlockCollection(Block?[] Blocks)
{
    private const char EMPTY_SPACE = '.';

    public BlockCollection() : this([])
    {
    }

    public long CalculateCheckSum()
    {
        var result = 0L;
        for (var i = 0; i < Blocks.Length; i++)
        {
            if (Blocks[i] == null)
            {
                continue;
            }

            result += Blocks[i]!.FileID * i;
        }

        return result;
    }

    public BlockCollection GetCompactBlocks(Action<OutputMessage>? output)
    {
        if (output != null)
        {
            output(new("Starting to compact", this));
        }

        var result = Blocks.ToArray(); // Make a copy of the blocks array

        var changeMade = true;
        while (changeMade)
        {
            var lastBlockIndex = GetLastBlockIndex(result);
            var firstEmptyIndex = Array.IndexOf(result, null);
            changeMade =
                lastBlockIndex > 0 &&
                firstEmptyIndex > 0 &&
                lastBlockIndex != firstEmptyIndex &&
                lastBlockIndex > firstEmptyIndex
                ;
            if (changeMade)
            {
                result[firstEmptyIndex] = result[lastBlockIndex];
                result[lastBlockIndex] = null;

                if (output != null)
                {
                    output(new(string.Empty, new BlockCollection(result)));
                }
            }
        }

        return new BlockCollection(result);
    }

    public BlockCollection GetCompactButNotFragmentedBlocks(Action<OutputMessage>? output)
    {
        if (output != null)
        {
            output(new("Starting to compact", this));
        }

        var result = Blocks.ToArray(); // Make a copy of the blocks array
        var highestFileID = result.Max(block => block?.FileID ?? -1);
        for(var currentFileID = highestFileID; currentFileID >= 0; currentFileID--)
        {
            var collection = new BlockCollection(result);
            var emptySpace = collection.GetAvailableSpace();
            var fileLength = result.Count(block => block?.FileID == currentFileID);
            var emptySpaceIndex = emptySpace.Where(space => space.Length >= fileLength).OrderBy(space => space.StartIndex).FirstOrDefault()?.StartIndex;
            var firstFileIndex = GetFileLocations(currentFileID).Min(location => location.StartIndex);

            if(!emptySpaceIndex.HasValue || firstFileIndex < emptySpaceIndex)
            {
                continue;
            }

            for(var i=0;i<fileLength;i++)
            {
                result[emptySpaceIndex.Value+i] = result[firstFileIndex+i];
                result[firstFileIndex + i] = null;
            }

            if(output != null)
            {
                output(new(string.Empty, new BlockCollection(result)));
            }
        }

        return new BlockCollection(result);
    }

    private List<DiskSpace> GetAvailableSpace() =>
        GetAvailableSpace((block) => block == null);

    private List<DiskSpace> GetFileLocations(int fileID) =>
        GetAvailableSpace((block) => block?.FileID == fileID);

    private List<DiskSpace> GetAvailableSpace(Func<Block?, bool> compareFunc)
    {
        var result = new List<DiskSpace>();
        var currentStartIndex = -1;
        var currentLength = 0;
        for(var i=0;i<Blocks.Length;i++)
        {
            if(!compareFunc(Blocks[i]))
            {
                if (currentStartIndex >= 0)
                {
                    result.Add(new DiskSpace(currentStartIndex, currentLength));
                    currentStartIndex = -1;
                    currentLength = 0;
                }
                continue;
            }

            if(currentStartIndex == -1)
            {
                currentStartIndex = i;
                currentLength = 1;
                continue;
            }

            currentLength += 1;
        }

        // Make sure to add last space as well
        if(currentStartIndex >= 0)
        {
            result.Add(new DiskSpace(currentStartIndex, currentLength));
        }

        return result;
    }

    private static int GetLastBlockIndex(Block?[] blocks)
    {
        for (var i = blocks.Length - 1; i >= 0; i--)
        {
            if (blocks[i] != null)
            {
                return i;
            }
        }

        return -1;
    }

    public override string ToString() =>
        GetBlocksAsString(Blocks);

    private static string GetBlocksAsString(Block?[] blocks)
    {
        if(blocks.Length == 0)
        {
            return string.Empty;
        }

        var result = new StringBuilder();
        foreach (var block in blocks)
        {
            result.Append(block?.ToString() ?? EMPTY_SPACE.ToString());
        }
        return result.ToString();
    }
}