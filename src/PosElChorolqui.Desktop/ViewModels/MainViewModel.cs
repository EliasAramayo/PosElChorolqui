using System.Collections.ObjectModel;
using System.Globalization;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PosElChorolqui.Desktop.Models;
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

    [ObservableProperty]
    private NavigationItem? _selectedItem;

    [ObservableProperty]
    private ViewModelBase? _currentView;

    [ObservableProperty]
    private string _currentSectionTitle = "Tablero";

    [ObservableProperty]
    private string _clockText = string.Empty;

    [ObservableProperty]
    private bool _isDarkTheme;

    public ObservableCollection<NavigationItem> NavigationItems { get; } = new();

    public string ConnectionShortText => IsHealthy ? "Conectado" : "Sin conexión";

    // Design-time constructor for the XAML previewer. Runtime uses DI.
    public MainViewModel() : this(new BackendHealthClient(new HttpClient()))
    {
    }

    public MainViewModel(BackendHealthClient healthClient)
    {
        _healthClient = healthClient;

        NavigationItems.Add(new NavigationItem("dashboard", "Tablero", "▦", new DashboardViewModel()));
        NavigationItems.Add(new NavigationItem("pos", "Punto de venta", "◉", new PointOfSaleViewModel()));
        NavigationItems.Add(new NavigationItem("products", "Productos", "□", new ProductsViewModel()));
        NavigationItems.Add(new NavigationItem("inventory", "Inventario", "△", new InventoryViewModel()));
        NavigationItems.Add(new NavigationItem("customers", "Clientes", "○", new CustomersViewModel()));
        NavigationItems.Add(new NavigationItem("suppliers", "Proveedores", "◇", new SuppliersViewModel()));
        NavigationItems.Add(new NavigationItem("cash", "Caja", "▣", new CashRegisterViewModel()));
        NavigationItems.Add(new NavigationItem("reports", "Reportes", "▥", new ReportsViewModel()));
        NavigationItems.Add(new NavigationItem("settings", "Configuración", "≡", new SettingsViewModel()));

        _isDarkTheme = AppThemeService.IsDark;
        AppThemeService.ThemeChanged += OnThemeChangedExternally;

        UpdateClock();
        var clock = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
        clock.Tick += (_, _) => UpdateClock();
        clock.Start();

        SelectedItem = NavigationItems[0];
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

    [RelayCommand]
    private void NavigateToSettings()
    {
        var settings = NavigationItems.FirstOrDefault(item => item.Key == "settings");
        if (settings is not null)
        {
            SelectedItem = settings;
        }
    }

    partial void OnSelectedItemChanged(NavigationItem? value)
    {
        CurrentView = value?.Content;
        CurrentSectionTitle = value?.Title ?? string.Empty;
    }

    partial void OnIsHealthyChanged(bool value)
    {
        OnPropertyChanged(nameof(ConnectionShortText));
    }

    partial void OnIsDarkThemeChanged(bool value)
    {
        if (AppThemeService.IsDark != value)
        {
            AppThemeService.Apply(value);
        }
    }

    private void OnThemeChangedExternally(bool dark)
    {
        if (IsDarkTheme != dark)
        {
            IsDarkTheme = dark;
        }
    }

    private void UpdateClock()
    {
        ClockText = DateTime.Now.ToString(
            "dddd d 'de' MMMM 'de' yyyy · HH:mm",
            new CultureInfo("es"));
    }
}
