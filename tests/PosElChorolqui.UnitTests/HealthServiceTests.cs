using PosElChorolqui.Application;

namespace PosElChorolqui.UnitTests;

public sealed class HealthServiceTests
{
    private sealed class FixedClock(DateTimeOffset utcNow) : IClock
    {
        public DateTimeOffset UtcNow { get; } = utcNow;
    }

    [Fact]
    public void GetHealth_ReturnsServiceNameHealthyAndClockTime()
    {
        var fixedTime = new DateTimeOffset(2026, 11, 13, 12, 0, 0, TimeSpan.Zero);
        var service = new HealthService(new FixedClock(fixedTime));

        var health = service.GetHealth();

        Assert.Equal(HealthService.ServiceName, health.Service);
        Assert.Equal(HealthService.HealthyStatus, health.Status);
        Assert.Equal(fixedTime, health.CheckedAtUtc);
    }

    [Fact]
    public void ServiceName_IsLocalHostService()
    {
        Assert.Equal("PosElChorolqui.LocalHost", HealthService.ServiceName);
    }
}
