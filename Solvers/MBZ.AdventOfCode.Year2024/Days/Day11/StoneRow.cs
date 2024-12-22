using System.Collections.Concurrent;
using System.Linq;
using System.Numerics;

namespace MBZ.AdventOfCode.Year2024.Day11;

public record StoneRow
{
    public Stone[] Stones { get; set; }

    public StoneRow(IEnumerable<Stone> stones)
    {
        Stones = stones.ToArray();
    }

    public StoneRow(Stone[] stones)
    {
        Stones = stones.ToArray();
    }

    private static IBlinkRule[]? _rules;
    private static IBlinkRule[] Rules =>
        _rules ??= GetBlinkRules();

    private static IBlinkRule[] GetBlinkRules() =>
        [
            new A0BecomesA1BlinkRule(),
            new EvenNumberOfEngravedDigitsBlinkRule(),
            new FallbackBlinkRule(),
        ];

    public StoneRow Blink(int numberOfBlinks = 1)
    {
        if (numberOfBlinks < 0)
            throw new ArgumentOutOfRangeException(nameof(numberOfBlinks), "Number of blinks must be positive");

        if (numberOfBlinks == 0)
            return this;


        var stoneEngravings = Stones.Select(stone => stone.Engraving).ToList();
        for (var i = 0; i < numberOfBlinks; i++)
        {
            stoneEngravings = stoneEngravings
                    .SelectMany(PerformBlink)
                    .ToList()
                ;
        }

        var result = new StoneRow(stoneEngravings.Select(se => new Stone(se)));
        return result;
    }

    public BigInteger BlinkAndReportNumberOfStones(int numberOfBlinks = 1)
    {
        if (numberOfBlinks < 0)
            throw new ArgumentOutOfRangeException(nameof(numberOfBlinks), "Number of blinks must be positive");

        if (numberOfBlinks == 0)
            return Stones.Length;


        var stones = Stones
            .GroupBy(stone => stone.Engraving)
            .Select(grp => new CompositeStone(grp.Key, grp.Count()))
            .ToList()
        ;
        for (var i = 0; i < numberOfBlinks; i++)
        {
            stones = PerformBlink(stones).ToList();
        }

        var stoneCount = stones.Aggregate(BigInteger.Zero, (current, stone) => current + stone.Count);
        return stoneCount;
    }

    public BigInteger BlinkAndReportNumberOfStones(int numberOfBlinks, Action<OutputMessage> output)
    {
        if (numberOfBlinks < 0)
            throw new ArgumentOutOfRangeException(nameof(numberOfBlinks), "Number of blinks must be positive");

        if (numberOfBlinks == 0)
            return Stones.Length;


        var stones = Stones
            .GroupBy(stone => stone.Engraving)
            .Select(grp => new CompositeStone(grp.Key, grp.Count()))
            .ToList()
        ;
        for (var i = 0; i < numberOfBlinks; i++)
        {
            stones = PerformBlink(stones).ToList();
            var blinkNumber = (i + 1).ToString().PadLeft(2, '0');
            var currentStoneCount = stones.Aggregate(BigInteger.Zero, (current, stone) => current + stone.Count);
            output(new(softFlush: true, $"Blink #{blinkNumber} results in {currentStoneCount:n0} stones ({stones.Count:n0} composite stones)"));
        }

        var stoneCount = stones.Aggregate(BigInteger.Zero, (current, stone) => current + stone.Count);
        return stoneCount;
    }

    private static readonly ConcurrentDictionary<long, IEnumerable<long>> BlinkDictionary = new();

    private static IEnumerable<CompositeStone> PerformBlink(IEnumerable<CompositeStone> stones)
    {
        var allNewStoneEngravings = new ConcurrentBag<CompositeStone>();
        stones
            .AsParallel()
            .ForAll(stone =>
            {
                var newStoneEngravings = BlinkDictionary.GetOrAdd(stone.Engraving, PerformBlink(stone.Engraving));
                foreach (var newStoneEngraving in newStoneEngravings)
                {
                    allNewStoneEngravings.Add(stone with { Engraving = newStoneEngraving });
                }
            })
        ;
        
        var result = allNewStoneEngravings
            .GroupBy(stone => stone.Engraving)
            .Select(grp => new CompositeStone(grp.Key, grp.Aggregate(BigInteger.Zero, (current, stone) => current + stone.Count)))
            .ToList()
        ;
        return result;
    }

    private static IEnumerable<long> PerformBlink(long stoneEngraving) =>
        Rules
            .FirstOrDefault(r => r.IsMatch(stoneEngraving))!
            .Apply(stoneEngraving)
        ;

    public override string ToString() =>
        string.Join(" ", Stones.Select(s => s.Engraving));
}