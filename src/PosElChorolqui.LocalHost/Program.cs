using PosElChorolqui.Application;
using PosElChorolqui.Contracts;
using PosElChorolqui.LocalHost;

var builder = PosHost.CreateBuilder(args);
var app = builder.Build();

app.UseExceptionHandler();

app.MapGet("/health", (IHealthService health, ILogger<Program> logger) =>
{
    try
    {
        return Results.Ok(health.GetHealth());
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Health check failed");
        return Results.Problem("No se pudo comprobar el estado del servicio.");
    }
})
.WithName("GetHealth");

app.Run();
