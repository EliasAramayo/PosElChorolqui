using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PosElChorolqui.Desktop.Services;

namespace PosElChorolqui.Desktop.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private readonly BackendHealthClient _healthClient;

    [ObservableProperty]
    private string _statusText = "Estado del servicio: comprobando…";

    [ObservableProperty]
    private bool _isHealthy;

    [ObservableProperty]
    private bool _isBusy;

    [ObservableProperty]
    private string? _serviceName;

    [ObservableProperty]
    private string? _lastCheckedText;

    // Design-time constructor for the XAML previewer. Runtime uses DI.
    public MainViewModel() : this(new BackendHealthClient(new HttpClient()))
    {
    }

    public MainViewModel(BackendHealthClient healthClient)
    {
        _healthClient = healthClient;
    }

    public Task InitializeAsync() => RefreshAsync();

    [RelayCommand]
    private async Task RefreshAsync()
    {
        if (IsBusy)
        {
            return;
        }

        try
        {
            IsBusy = true;
            StatusText = "Estado del servicio: comprobando…";

            var result = await _healthClient.GetHealthAsync();

            IsHealthy = result.IsHealthy;
            ServiceName = result.Health?.Service;
            LastCheckedText = result.Health is null
                ? null
                : $"Última comprobación: {result.Health.CheckedAtUtc:u}";

            StatusText = result.IsHealthy
                ? "Estado del servicio: Conectado"
                : $"No disponible: {result.ErrorMessage}";
        }
        catch (Exception ex)
        {
            IsHealthy = false;
            StatusText = $"No disponible: {ex.Message}";
        }
        finally
        {
            IsBusy = false;
        }
    }
}
