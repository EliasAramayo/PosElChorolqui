using PosElChorolqui.Desktop.ViewModels;

namespace PosElChorolqui.Desktop.Models;

// A single entry of the sidebar. The shell binds the list to a ListBox and
// shows Content through the ViewLocator, so navigation stays in one window.
// Glyph is a decorative monogram rendered next to the title; the title
// always carries the meaning, so a missing font glyph never hides information.
public sealed class NavigationItem
{
    public NavigationItem(string key, string title, string glyph, ViewModelBase content)
    {
        Key = key;
        Title = title;
        Glyph = glyph;
        Content = content;
    }

    public string Key { get; }

    public string Title { get; }

    public string Glyph { get; }

    public ViewModelBase Content { get; }
}
