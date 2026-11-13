using Microsoft.EntityFrameworkCore;

namespace PosElChorolqui.Infrastructure;

/// <summary>
/// SQLite persistence for the backend. No entities are mapped yet;
/// entity configurations arrive with future stages and ApplyConfigurationsFromAssembly picks them up.
/// </summary>
public class PosDbContext(DbContextOptions<PosDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PosDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
