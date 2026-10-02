# Temperature Monitor System (.NET 10)

A robust, enterprise-grade .NET 10 console application and class library engineered for physical sensor temperature monitoring, range validation, statistical analysis, and reading classification.

[![NET 10](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![C# 14](https://img.shields.io/badge/C%23-14.0-239120?logo=csharp)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![MSTest](https://img.shields.io/badge/Tests-25%20Passed-brightgreen)](https://github.com/Kore-net2/TemperatureMonitor)

---

## 📌 Implementation Details & Core Code

### 1. Range Validation & Classification (`TemperatureRange.cs`)
Enforces hardware limits ($-50^\circ\text{C}$ to $150^\circ\text{C}$), validates limits ordering ($\text{Lower} < \text{Upper}$), and classifies readings into `TemperatureBand`:

```csharp
namespace TemperatureMonitor.Core;

public class TemperatureRange
{
    public TemperatureRange(double lower, double upper)
    {
        // Guard 1: Reject non-finite numbers and out-of-sensor-range limits
        if (!double.IsFinite(lower) || lower < -50 || lower > 150)
        {
            throw new ArgumentOutOfRangeException(nameof(lower));
        }
        if (!double.IsFinite(upper) || upper < -50 || upper > 150)
        {
            throw new ArgumentOutOfRangeException(nameof(upper));
        }

        // Guard 2: Lower limit must be strictly less than upper limit
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
        ArgumentNullException.ThrowIfNull(reading);

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