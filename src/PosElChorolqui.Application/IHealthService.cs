using PosElChorolqui.Contracts;

namespace PosElChorolqui.Application;

/// <summary>Reports the health of the local service.</summary>
public interface IHealthService
{
    HealthResponse GetHealth();
}
