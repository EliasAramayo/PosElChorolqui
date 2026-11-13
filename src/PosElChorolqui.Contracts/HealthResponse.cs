namespace PosElChorolqui.Contracts;

/// <summary>Health status reported by PosElChorolqui.LocalHost.</summary>
public sealed record HealthResponse(
    string Service,
    string Status,
    DateTimeOffset CheckedAtUtc);
