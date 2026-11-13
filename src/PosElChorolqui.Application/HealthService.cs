using PosElChorolqui.Contracts;

namespace PosElChorolqui.Application;

/// <summary>Trivial Etapa 1 implementation: the service is healthy when it can answer.</summary>
public sealed class HealthService(IClock clock) : IHealthService
{
    public const string ServiceName = "PosElChorolqui.LocalHost";

    public const string HealthyStatus = "Healthy";

    private readonly IClock _clock = clock;

    public HealthResponse GetHealth() => new(ServiceName, HealthyStatus, _clock.UtcNow);
}
