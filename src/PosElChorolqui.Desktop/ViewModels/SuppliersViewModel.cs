using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace PosElChorolqui.Desktop.ViewModels;

// Supplier directory, ready for future tables and forms.
public sealed partial class SuppliersViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _searchText = string.Empty;

    [RelayCommand]
    private void ClearSearch()
    {
        SearchText = string.Empty;
    }
}
