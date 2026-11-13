namespace PosElChorolqui.Domain;

/// <summary>Minimal foundation for future entities. No business concepts yet.</summary>
public abstract class BaseEntity
{
    public Guid Id { get; protected set; } = Guid.NewGuid();

    public DateTimeOffset CreatedAtUtc { get; protected set; } = DateTimeOffset.UtcNow;

    public DateTimeOffset? UpdatedAtUtc { get; protected set; }

    protected void Touch(DateTimeOffset utcNow) => UpdatedAtUtc = utcNow;
}
