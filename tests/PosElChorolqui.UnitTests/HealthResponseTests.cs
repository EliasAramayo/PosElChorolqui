using System.Text.Json;
using PosElChorolqui.Contracts;

namespace PosElChorolqui.UnitTests;

public sealed class HealthResponseTests
{
    [Fact]
    public void JsonRoundTrip_PreservesContract()
    {
        var original = new HealthResponse(
            "PosElChorolqui.LocalHost",
            "Healthy",
            new DateTimeOffset(2026, 11, 13, 12, 0, 0, TimeSpan.Zero));

        var json = JsonSerializer.Serialize(original);
        var restored = JsonSerializer.Deserialize<HealthResponse>(json);

        Assert.Equal(original, restored);
    }
}
