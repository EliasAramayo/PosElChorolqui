using PosElChorolqui.Domain;

namespace PosElChorolqui.UnitTests;

public sealed class BaseEntityTests
{
    private sealed class TestEntity : BaseEntity
    {
        public void MarkUpdated(DateTimeOffset utcNow) => Touch(utcNow);
    }

    [Fact]
    public void NewEntity_HasIdentityAndCreationAudit()
    {
        var before = DateTimeOffset.UtcNow;

        var entity = new TestEntity();

        Assert.NotEqual(Guid.Empty, entity.Id);
        Assert.Null(entity.UpdatedAtUtc);
        Assert.InRange(entity.CreatedAtUtc, before, DateTimeOffset.UtcNow);
    }

    [Fact]
    public void Touch_SetsUpdatedAtUtc()
    {
        var entity = new TestEntity();
        var stamp = new DateTimeOffset(2026, 11, 13, 12, 0, 0, TimeSpan.Zero);

        entity.MarkUpdated(stamp);

        Assert.Equal(stamp, entity.UpdatedAtUtc);
    }
}
