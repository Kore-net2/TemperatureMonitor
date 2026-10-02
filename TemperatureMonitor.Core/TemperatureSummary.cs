namespace TemperatureMonitor.Core;

public sealed class TemperatureSummary
{
    public TemperatureSummary(
        int count,
        double minimum,
        double maximum,
        double mean)
    {
        Count = count;
        Minimum = minimum;
        Maximum = maximum;
        Mean = mean;
    }

    public int Count { get; }

    public double Minimum { get; }

    public double Maximum { get; }

    public double Mean { get; }
}
