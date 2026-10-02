using TemperatureMonitor.Core;

namespace TemperatureMonitor.Tests;

[TestClass]
public sealed class TemperatureAnalyserTests
{
    [TestMethod]
    public void ValidReadingsProduceExpectedSummary()
    {
        bool ok = TemperatureAnalyser.TryAnalyse(
            ["18.5", "20", "22", "19.5"],
            out TemperatureSummary? summary,
            out string error);

        Assert.IsTrue(ok);
        Assert.AreEqual(string.Empty, error);
        Assert.IsNotNull(summary);
        Assert.AreEqual(4, summary.Count);
        Assert.AreEqual(18.5, summary.Minimum, 1e-12);
        Assert.AreEqual(22.0, summary.Maximum, 1e-12);
        Assert.AreEqual(20.0, summary.Mean, 1e-12);
    }

    [TestMethod]
    public void EmptyInputIsRejected()
    {
        bool ok = TemperatureAnalyser.TryAnalyse(
            [], out TemperatureSummary? summary, out string error);

        Assert.IsFalse(ok);
        Assert.IsNull(summary);
        StringAssert.Contains(error, "at least one");
    }

    [TestMethod]
    public void MalformedReadingIsRejected()
    {
        bool ok = TemperatureAnalyser.TryAnalyse(
            ["20", "sensor-error", "21"],
            out TemperatureSummary? summary,
            out string error);

        Assert.IsFalse(ok);
        Assert.IsNull(summary);
        StringAssert.Contains(error, "Reading 2");
        StringAssert.Contains(error, "not a valid finite number");
    }

    [TestMethod]
    public void NonFiniteReadingIsRejected()
    {
        bool ok = TemperatureAnalyser.TryAnalyse(
            ["20", "NaN", "21"],
            out TemperatureSummary? summary,
            out string error);

        Assert.IsFalse(ok);
        Assert.IsNull(summary);
        StringAssert.Contains(error, "not a valid finite number");
    }

    [TestMethod]
    [DataRow("-50.1")]
    [DataRow("150.1")]
    public void OutOfRangeReadingIsRejected(string reading)
    {
        bool ok = TemperatureAnalyser.TryAnalyse(
            [reading], out TemperatureSummary? summary, out string error);

        Assert.IsFalse(ok);
        Assert.IsNull(summary);
        StringAssert.Contains(error, "between -50.0 C and 150.0 C");
    }

    [TestMethod]
    public void BoundaryReadingsAreAccepted()
    {
        bool ok = TemperatureAnalyser.TryAnalyse(
            ["-50", "150"],
            out TemperatureSummary? summary,
            out string error);

        Assert.IsTrue(ok);
        Assert.AreEqual(string.Empty, error);
        Assert.IsNotNull(summary);
        Assert.AreEqual(-50.0, summary.Minimum, 1e-12);
        Assert.AreEqual(150.0, summary.Maximum, 1e-12);
        Assert.AreEqual(50.0, summary.Mean, 1e-12);
    }
}
