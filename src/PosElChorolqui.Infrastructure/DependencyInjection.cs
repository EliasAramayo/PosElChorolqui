using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PosElChorolqui.Infrastructure;

/// <summary>Persistence wiring owned by the backend. The Desktop client never touches this.</summary>
public static class DependencyInjection
{
    public const string ConnectionStringName = "PosDatabase";

    public const string DefaultConnectionString = "Data Source=poselchorolqui.db";

    public static IServiceCollection AddPosPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(ConnectionStringName)
            ?? DefaultConnectionString;

        services.AddDbContext<PosDbContext>(options => options.UseSqlite(connectionString));

        return services;
    }
}
