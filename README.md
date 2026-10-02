# Temperature monitor checkpoint

Stage: starter. SDK 10.0.401 (latest patch roll-forward), net10.0, C# 14, MSTest 4.0.2.
Open `TemperatureMonitor.slnx`, or run from this directory:

```powershell
dotnet restore TemperatureMonitor.slnx
dotnet build TemperatureMonitor.slnx --no-restore
dotnet test TemperatureMonitor.Tests/TemperatureMonitor.Tests.csproj --no-build
dotnet run --no-build --project TemperatureMonitor.Cli -- 17 18 20 25 26
```

Expected summary: count 5, minimum 17.00 C, maximum 26.00 C, mean 21.20 C.

STARTER: WithinRangeIsNormal deliberately fails; three TODO tests are skipped. Complete TemperatureRange and the TODO tests before relying on band output. The inherited summary and reading tests pass.
Use no arguments, `20 sensor-error 21`, `NaN`, `Infinity` or `151` to exercise failures.
Each failure writes Error: to standard error, returns 1 and prints no report. Inspect `$LASTEXITCODE` immediately after the run.
These thresholds are teaching requirements. See the parent lecturer runbook or lab sheet for the timed task.
