using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Styling;

namespace PosElChorolqui.Desktop.Services;

// Applies the light/dark theme dictionaries and keeps the Fluent theme in sync.
// No new UI dependencies: it only swaps XAML resource dictionaries shipped with the app.
// The service owns every theme dictionary it merges, so switching never leaves
// duplicate brush keys behind.
public static class AppThemeService
{
    private static IResourceProvider? _current;

    public static bool IsDark { get; private set; }

    public static event Action<bool>? ThemeChanged;

    public static void Apply(bool dark)
    {
        var application = Application.Current;
        if (application is null)
        {
            return;
        }

        var merged = application.Resources.MergedDictionaries;
        if (_current is not null)
        {
            merged.Remove(_current);
            _current = null;
        }

        var name = dark ? "DarkTheme" : "LightTheme";
        var uri = new Uri($"avares://PosElChorolqui.Desktop/Themes/{name}.axaml");
        _current = new ResourceInclude(uri) { Source = uri };
        merged.Add(_current);

        application.RequestedThemeVariant = dark ? ThemeVariant.Dark : ThemeVariant.Light;
        IsDark = dark;
        ThemeChanged?.Invoke(dark);
    }
}
