using TemperatureMonitor.Core;
namespace TemperatureMonitor.Tests;

[TestClass]
public class RangeTests
{
    [TestMethod]
    public void WithinRangeIsNormal()
    {
        var range = new TemperatureRange(18, 25);
        Assert.AreEqual(TemperatureBand.Normal,
            range.Classify(new TemperatureReading(20)));
    }

    [TestMethod]
    public void BothLimitsAreNormal()
    {
        var range = new TemperatureRange(18, 25);
        Assert.AreEqual(TemperatureBand.Low, range.Classify(new TemperatureReading(17.9)));
        Assert.AreEqual(TemperatureBand.Normal, range.Classify(new TemperatureReading(18)));
        Assert.AreEqual(TemperatureBand.Normal, range.Classify(new TemperatureReading(25)));
        Assert.AreEqual(TemperatureBand.High, range.Classify(new TemperatureReading(25.1)));
    }

    [TestMethod]
    public void InvalidLimitsAreRejected()
    {
        Assert.ThrowsExactly<ArgumentException>(() => new TemperatureRange(25, 18));
        Assert.ThrowsExactly<ArgumentException>(() => new TemperatureRange(20, 20));
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => new TemperatureRange(double.NaN, 25));
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => new TemperatureRange(18, double.PositiveInfinity));
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => new TemperatureRange(-50.1, 25));
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => new TemperatureRange(18, 150.1));
    }

    [TestMethod]
    public void RangeInstanceChangesClassification()
    {
        var reading = new TemperatureReading(17.9);
        var defaultRange = new TemperatureRange(18, 25);
        var customRange = new TemperatureRange(10, 30);
        Assert.AreEqual(TemperatureBand.Low, defaultRange.Classify(reading));
        Assert.AreEqual(TemperatureBand.Normal, customRange.Classify(reading));
    }
}