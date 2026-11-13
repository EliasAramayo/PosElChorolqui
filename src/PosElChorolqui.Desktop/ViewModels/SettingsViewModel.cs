using CommunityToolkit.Mvvm.ComponentModel;
using PosElChorolqui.Desktop.Services;

namespace PosElChorolqui.Desktop.ViewModels;

// Application settings. Only the theme switch is functional in this stage;
// business, device and general sections are reserved placeholders.
public sealed partial class SettingsViewModel : ViewModelBase
{
    [ObservableProperty]
    private bool _isDarkTheme;

    public SettingsViewModel()
    {
        _isDarkTheme = AppThemeService.IsDark;
        AppThemeService.ThemeChanged += OnThemeChangedExternally;
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
}
