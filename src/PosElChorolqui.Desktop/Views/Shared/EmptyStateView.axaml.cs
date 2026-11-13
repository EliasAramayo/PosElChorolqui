using Avalonia;
using Avalonia.Controls;

namespace PosElChorolqui.Desktop.Views.Shared;

// Shared empty state: decorative glyph badge, main message and an optional
// hint. All text is neutral Spanish passed in by the caller; the control
// itself invents no content.
public partial class EmptyStateView : UserControl
{
    public static readonly StyledProperty<string> GlyphProperty =
        AvaloniaProperty.Register<EmptyStateView, string>(nameof(Glyph), "○");

    public static readonly StyledProperty<string> MessageProperty =
        AvaloniaProperty.Register<EmptyStateView, string>(nameof(Message), string.Empty);

    public static readonly StyledProperty<string> HintProperty =
        AvaloniaProperty.Register<EmptyStateView, string>(nameof(Hint), string.Empty);

    public string Glyph
    {
        get => GetValue(GlyphProperty);
        set => SetValue(GlyphProperty, value);
    }

    public string Message
    {
        get => GetValue(MessageProperty);
        set => SetValue(MessageProperty, value);
    }

    public string Hint
    {
        get => GetValue(HintProperty);
        set => SetValue(HintProperty, value);
    }

    public EmptyStateView()
    {
        InitializeComponent();
    }
}
