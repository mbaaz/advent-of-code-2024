namespace MBZ.AdventOfCode.Year2024.Day09;

public class Disk
{
    public string DiskMap { get; }
    public BlockCollection Blocks { get; }

    public Disk(string diskMap)
    {
        DiskMap = diskMap.Trim();
        Blocks = GetBlocksFromDiskMap(DiskMap);
    }

    private static BlockCollection GetBlocksFromDiskMap(string diskMap)
    {
        if(string.IsNullOrWhiteSpace(diskMap))
        {
            return new BlockCollection();
        }

        var diskMapArray = diskMap
                .ToCharArray()
                .Select(c => int.Parse(c.ToString()))
                .ToArray()
            ;
        if(diskMapArray.Length == 0)
        {
            return new BlockCollection();
        }

        var nextFileID = 0;
        var blocks = new List<Block?>();

        for (var i = 0; i < diskMapArray.Length; i++)
        {
            var isBlock = i % 2 == 0;
            var fileID = isBlock ? nextFileID++ : -1;
            for (var j = 0; j < diskMapArray[i]; j++)
            {
                blocks.Add(isBlock ? new Block(fileID) : null);
            }
        }

        var blocksCollection = new BlockCollection(blocks.ToArray());
        return blocksCollection;
    }
}