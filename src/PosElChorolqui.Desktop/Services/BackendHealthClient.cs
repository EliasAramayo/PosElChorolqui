using System.Net.Http.Json;
using PosElChorolqui.Contracts;

namespace PosElChorolqui.Desktop.Services;

/// <summary>Outcome of a health probe against the local backend.</summary>
public sealed record BackendHealthResult(bool IsHealthy, HealthResponse? Health, string? ErrorMessage);

/// <summary>
/// Typed HTTP client for the LocalHost backend. Base address and timeout
/// come from configuration; failures map to Spanish user-facing messages.
/// </summary>
public sealed class BackendHealthClient(HttpClient http)
{
    private readonly HttpClient _http = http;

    public async Task<BackendHealthResult> GetHealthAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            using var response = await _http.GetAsync("health", cancellationToken).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                return new BackendHealthResult(
                    false,
                    null,
                    $"Servicio no disponible (HTTP {(int)response.StatusCode}).");
            }

            var health = await response.Content
                .ReadFromJsonAsync<HealthResponse>(cancellationToken)
                .ConfigureAwait(false);

            return health is null
                ? new BackendHealthResult(false, null, "Respuesta vacía del servicio.")
                : new BackendHealthResult(true, health, null);
        }
        catch (TaskCanceledException) when (!cancellationToken.IsCancellationRequested)
        {
            return new BackendHealthResult(false, null, "Tiempo de espera agotado al contactar el servicio.");
        }
        catch (TaskCanceledException)
        {
            return new BackendHealthResult(false, null, "Operación cancelada.");
        }
        catch (HttpRequestException ex)
        {
            return new BackendHealthResult(false, null, $"No se pudo conectar con el servicio: {ex.Message}");
        }
        catch (Exception ex)
        {
            return new BackendHealthResult(false, null, $"Error inesperado: {ex.Message}");
        }
    }
}
