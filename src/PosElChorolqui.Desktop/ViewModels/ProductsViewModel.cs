using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace PosElChorolqui.Desktop.ViewModels;

// Product catalog screen. No real CRUD in this stage.
public sealed partial class ProductsViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _searchText = string.Empty;

    [RelayCommand]
    private void ClearFilters()
    {
        SearchText = string.Empty;
    }
}
