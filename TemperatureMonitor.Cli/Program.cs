using System.Globalization;
using TemperatureMonitor.Core;

if (!TemperatureAnalyser.TryReadings(args, out TemperatureReading[] readings,
    out string error))
{
    Console.Error.WriteLine($"Error: {error}");
    return 1;
}
TemperatureSummary result = TemperatureAnalyser.Summarise(readings);
var range = new TemperatureRange(18, 25);
int low = 0, normal = 0, high = 0;
foreach (TemperatureReading reading in readings)
{
    TemperatureBand band = range.Classify(reading);
    switch (band)
    {
        case TemperatureBand.Low: low++; break;
        case TemperatureBand.Normal: normal++; break;
        case TemperatureBand.High: high++; break;
    }
}
// Emit a report only after validation and classification have succeeded.
Console.WriteLine($"Readings: {result.Count}");
Console.WriteLine($"Minimum: {result.Minimum.ToString("F2", CultureInfo.InvariantCulture)} C");
Console.WriteLine($"Maximum: {result.Maximum.ToString("F2", CultureInfo.InvariantCulture)} C");
Console.WriteLine($"Mean: {result.Mean.ToString("F2", CultureInfo.InvariantCulture)} C");
Console.WriteLine($"Low: {low}");
Console.WriteLine($"Normal: {normal}");
Console.WriteLine($"High: {high}");
return 0;
