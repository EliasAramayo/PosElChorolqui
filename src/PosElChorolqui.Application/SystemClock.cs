namespace PosElChorolqui.Application;

/// <summary>Production clock. Register as singleton.</summary>
public sealed class SystemClock : IClock
{
    public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
}
