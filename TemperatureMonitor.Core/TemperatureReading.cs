namespace TemperatureMonitor.Core;

public class TemperatureReading
{
    public const double MinimumAllowed = -50;
    public const double MaximumAllowed = 150;

    public TemperatureReading(double celsius)
    {
        if (!double.IsFinite(celsius))
            throw new ArgumentOutOfRangeException(nameof(celsius),
                "Temperature is not a valid finite number.");
        if (celsius < MinimumAllowed || celsius > MaximumAllowed)
            throw new ArgumentOutOfRangeException(nameof(celsius),
                "Temperature must be between -50.0 C and 150.0 C.");
        Celsius = celsius;
    }

    public double Celsius { get; }
    public double Fahrenheit => Celsius * 9.0 / 5.0 + 32;
    
}
