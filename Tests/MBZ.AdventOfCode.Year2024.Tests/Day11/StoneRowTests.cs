using MBZ.AdventOfCode.Year2024.Day11;

namespace MBZ.AdventOfCode.Year2024.Tests.Day11;

[TestFixture]
public class StoneRowTests
{
    [Test]
    public void Blink_ThrowsExceptionForNegativeBlinks()
    {
        var row = new StoneRow([
            new(10), 
            new(20),
        ]);
        Assert.Throws<ArgumentOutOfRangeException>(() => row.Blink(-1));
    }

    [Test]
    public void Blink_ReturnsSelfForNoBlinks()
    {
        var row = new StoneRow([
            new(10), 
            new(20),
        ]);
        Assert.That(row.Blink(0), Is.EqualTo(row));
    }

    [Test]
    public void Blink_Example1()
    {
        var row = new StoneRow([
            new(0), 
            new(1),
            new(10),
            new(99),
            new(999),
        ]);

        var expected = new StoneRow([
            new(1),
            new(2024),
            new(1),
            new(0),
            new(9),
            new(9),
            new(2021976),
        ]);
        Assert.That(row.Blink(1).Stones, Is.EquivalentTo(expected.Stones));
    }

    [Test]
    public void Blink_Example2_Blink1Times()
    {
        var blinks = 1;
        var row = new StoneRow([
            new(125),
            new(17),
        ]);

        var expected = new StoneRow([
            new(253000),
            new(1),
            new(7),
        ]);
        Assert.That(row.Blink(blinks).ToString(), Is.EqualTo(expected.ToString()));
    }

    [Test]
    public void Blink_Example2_Blink2Times()
    {
        var blinks = 2;
        var row = new StoneRow([
            new(125),
            new(17),
        ]);

        var expected = new StoneRow([
            new(253),
            new(0),
            new(2024),
            new(14168),
        ]);
        Assert.That(row.Blink(blinks).ToString(), Is.EqualTo(expected.ToString()));
    }

    [Test]
    public void Blink_Example2_Blink3Times()
    {
        var blinks = 3;
        var row = new StoneRow([
            new(125),
            new(17),
        ]);

        var expected = new StoneRow([
            new(512072),
            new(1),
            new(20),
            new(24),
            new(28676032),
        ]);
        Assert.That(row.Blink(blinks).ToString(), Is.EqualTo(expected.ToString()));
    }

    [Test]
    public void Blink_Example2_Blink4Times()
    {
        var blinks = 4;
        var row = new StoneRow([
            new(125),
            new(17),
        ]);

        var expected = new StoneRow([
            new(512),
            new(72),
            new(2024),
            new(2),
            new(0),
            new(2),
            new(4),
            new(2867),
            new(6032),
        ]);
        Assert.That(row.Blink(blinks).ToString(), Is.EqualTo(expected.ToString()));
    }

    [Test]
    public void Blink_Example2_Blink5Times()
    {
        var blinks = 5;
        var row = new StoneRow([
            new(125),
            new(17),
        ]);

        var expected = new StoneRow([
            new(1036288),
            new(7),
            new(2),
            new(20),
            new(24),
            new(4048),
            new(1),
            new(4048),
            new(8096),
            new(28),
            new(67),
            new(60),
            new(32),
        ]);
        Assert.That(row.Blink(blinks).ToString(), Is.EqualTo(expected.ToString()));
    }

    [Test]
    public void Blink_Example2_Blink6Times()
    {
        var blinks = 6;
        var row = new StoneRow([
            new(125),
            new(17),
        ]);

        var expected = new StoneRow([
            new(2097446912),
            new(14168),
            new(4048),
            new(2),
            new(0),
            new(2),
            new(4),
            new(40),
            new(48),
            new(2024),
            new(40),
            new(48),
            new(80),
            new(96),
            new(2),
            new(8),
            new(6),
            new(7),
            new(6),
            new(0),
            new(3),
            new(2),
        ]);
        Assert.That(row.Blink(blinks).ToString(), Is.EqualTo(expected.ToString()));
    }
}