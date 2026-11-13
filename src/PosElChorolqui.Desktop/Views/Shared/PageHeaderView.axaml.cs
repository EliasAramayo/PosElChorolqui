using Avalonia;
using Avalonia.Controls;

namespace PosElChorolqui.Desktop.Views.Shared;

// Shared page header: title plus subtitle with the standard scale and
// spacing, so every section keeps the same hierarchy without duplicating XAML.
public partial class PageHeaderView : UserControl
{
    public static readonly StyledProperty<string> TitleProperty =
        AvaloniaProperty.Register<PageHeaderView, string>(nameof(Title), string.Empty);

    public static readonly StyledProperty<string> SubtitleProperty =
        AvaloniaProperty.Register<PageHeaderView, string>(nameof(Subtitle), string.Empty);

    public string Title
    {
        get => GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public string Subtitle
    {
        get => GetValue(SubtitleProperty);
        set => SetValue(SubtitleProperty, value);
    }

    public PageHeaderView()
    {
        InitializeComponent();
    }
}
