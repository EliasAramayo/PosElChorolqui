using PosElChorolqui.Application;
using PosElChorolqui.Infrastructure;

namespace PosElChorolqui.LocalHost;

/// <summary>
/// Host composition kept separate from <c>Program</c> so a future
/// Windows-service installation can reuse it without touching endpoints.
/// No service installation happens in this stage.
/// </summary>
public static class PosHost
{
    public const int DefaultPort = 5117;

    public static WebApplicationBuilder CreateBuilder(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddSingleton<IClock, SystemClock>();
        builder.Services.AddSingleton<IHealthService, HealthService>();
        builder.Services.AddPosPersistence(builder.Configuration);
        builder.Services.AddProblemDetails();

        // Loopback only. Only the port comes from configuration; 127.0.0.1 is never configurable.
        var port = ResolvePort(builder.Configuration);
        builder.WebHost.UseUrls($"http://127.0.0.1:{port}");

        return builder;
    }

    public static int ResolvePort(IConfiguration configuration) =>
        configuration.GetValue<int?>("LocalHost:Port") ?? DefaultPort;
}
