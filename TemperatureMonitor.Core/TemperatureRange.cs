namespace TemperatureMonitor.Core;

public class TemperatureRange
{
    public TemperatureRange(double lower, double upper)
    {
        // TODO 1: reject non-finite or out-of-sensor-range limits.
        if (!double.IsFinite(lower) || lower < -50 || lower > 150)
        {
            throw new ArgumentOutOfRangeException(nameof(lower));
        }
        if (!double.IsFinite(upper) || upper < -50 || upper > 150)
        {
            throw new ArgumentOutOfRangeException(nameof(upper));
        }

        // TODO 2: reject lower >= upper, then assign both properties.
        if (lower >= upper)
        {
            throw new ArgumentException("Lower limit must be less than upper limit.");
        }
        Lower = lower;
        Upper = upper;
    }

    public double Lower { get; }
    public double Upper { get; }

    public TemperatureBand Classify(TemperatureReading reading)
    {
        ArgumentNullException.ThrowIfNull(reading); // Supplied guard.

        // TODO 3: compare Celsius to this instance's limits.
        if (reading.Celsius < Lower)
        {
            return TemperatureBand.Low;
        }
        if (reading.Celsius <= Upper)
        {
            return TemperatureBand.Normal;
        }
        return TemperatureBand.High;
    }
}