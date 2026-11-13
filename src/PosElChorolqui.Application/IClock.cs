namespace PosElChorolqui.Application;

/// <summary>Abstraction over the system clock for testable time.</summary>
public interface IClock
{
    DateTimeOffset UtcNow { get; }
}
