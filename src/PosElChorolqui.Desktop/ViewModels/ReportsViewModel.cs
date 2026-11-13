using CommunityToolkit.Mvvm.ComponentModel;

namespace PosElChorolqui.Desktop.ViewModels;

// Report filters and result zones. No data to aggregate in this stage,
// so results always render an explicit empty state.
public sealed partial class ReportsViewModel : ViewModelBase
{
    [ObservableProperty]
    private DateTimeOffset? _startDate = DateTimeOffset.Now;

    [ObservableProperty]
    private DateTimeOffset? _endDate = DateTimeOffset.Now;
}
