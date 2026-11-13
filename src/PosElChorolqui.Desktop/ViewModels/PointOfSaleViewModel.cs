using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace PosElChorolqui.Desktop.ViewModels;

// Point of sale workspace. No real sales or persistence in this stage:
// unavailable actions stay disabled and say so explicitly.
public sealed partial class PointOfSaleViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _searchText = string.Empty;

    [RelayCommand]
    private void ClearSearch()
    {
        SearchText = string.Empty;
    }
}
