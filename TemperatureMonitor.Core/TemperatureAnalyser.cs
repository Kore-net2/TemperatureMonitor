using System.Globalization;
namespace TemperatureMonitor.Core;

// Prepared integration code: parsing belongs at the application boundary.
public static class TemperatureAnalyser
{
    public static bool TryReadings(string[] inputs,
        out TemperatureReading[] readings, out string error)
    {
        readings = [];
        error = string.Empty;
        if (inputs.Length == 0)
        {
            error = "Provide at least one temperature reading.";
            return false;
        }
        var candidates = new TemperatureReading[inputs.Length];
        for (int i = 0; i < inputs.Length; i++)
        {
            if (!double.TryParse(inputs[i], NumberStyles.Float,
                CultureInfo.InvariantCulture, out double value))
            {
                error = $"Reading {i + 1} ('{inputs[i]}') is not a valid finite number.";
                return false;
            }
            try
            {
                candidates[i] = new TemperatureReading(value);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                error = $"Reading {i + 1}: {ex.Message}";
                return false;
            }
        }
        // Publish only after every reading has passed validation.
        readings = candidates;
        return true;
    }

    // Compatibility entry point retains the Week 1 test contract.
    public static bool TryAnalyse(string[] inputs,
        out TemperatureSummary? summary, out string error)
    {
        summary = null;
        if (!TryReadings(inputs, out TemperatureReading[] readings, out error))
            return false;
        summary = Summarise(readings);
        return true;
    }

    public static TemperatureSummary Summarise(TemperatureReading[] readings)
    {
        ArgumentNullException.ThrowIfNull(readings);
        if (readings.Length == 0)
            throw new ArgumentException("Provide at least one reading.", nameof(readings));
        double minimum = double.PositiveInfinity;
        double maximum = double.NegativeInfinity;
        double total = 0;
        foreach (TemperatureReading reading in readings)
        {
            ArgumentNullException.ThrowIfNull(reading);
            minimum = Math.Min(minimum, reading.Celsius);
            maximum = Math.Max(maximum, reading.Celsius);
            total += reading.Celsius;
        }
        return new TemperatureSummary(readings.Length, minimum, maximum,
            total / readings.Length);
    }
}
