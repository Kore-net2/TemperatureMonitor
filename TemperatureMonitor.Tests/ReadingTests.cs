using TemperatureMonitor.Core;
namespace TemperatureMonitor.Tests;

[TestClass]
public class ReadingTests
{
    [TestMethod]
    [DataRow(-50.0)]
    [DataRow(20.0)]
    [DataRow(150.0)]
    public void ConstructorPreservesValidValue(double value)
    {
        Assert.AreEqual(value, new TemperatureReading(value).Celsius);
    }
    [TestMethod]
    [DataRow(-50.1)]
    [DataRow(150.1)]
    [DataRow(double.NaN)]
    [DataRow(double.PositiveInfinity)]
    [DataRow(double.NegativeInfinity)]
    public void ConstructorRejectsInvalidValue(double value)
    {
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(
            () => new TemperatureReading(value));
    }
    [TestMethod]
    public void FailedBatchExposesNoPartialReadings()
    {
        bool ok = TemperatureAnalyser.TryReadings(["20", "151"],
            out TemperatureReading[] readings, out string error);
        Assert.IsFalse(ok);
        Assert.IsEmpty(readings);
        StringAssert.Contains(error, "Reading 2");
    }
    [TestMethod]
    public void SummaryRejectsEmptyArray()
    {
        Assert.ThrowsExactly<ArgumentException>(
            () => TemperatureAnalyser.Summarise([]));
    }
    [TestMethod]
    public void SummaryRejectsNullArrayOrElement()
    {
        Assert.ThrowsExactly<ArgumentNullException>(
            () => TemperatureAnalyser.Summarise(null!));
        Assert.ThrowsExactly<ArgumentNullException>(
            () => TemperatureAnalyser.Summarise([null!]));
    }
    [TestMethod]
    [DataRow(0.0, 32.0)]
    [DataRow(100.0, 212.0)]
    [DataRow(-40.0, -40.0)]
    public void FahrenheitIsCalculated(double celsius, double expected)
    {
        Assert.AreEqual(expected, new TemperatureReading(celsius).Fahrenheit, 1e-10);
    }
}
