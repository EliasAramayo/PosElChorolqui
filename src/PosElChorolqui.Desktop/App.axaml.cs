using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using Microsoft.Extensions.DependencyInjection;
using PosElChorolqui.Desktop.ViewModels;
using PosElChorolqui.Desktop.Views;

namespace PosElChorolqui.Desktop;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var viewModel = Program.Services.GetRequiredService<MainViewModel>();
            desktop.MainWindow = new MainWindow
            {
                DataContext = viewModel,
            };

            desktop.Exit += (_, _) => Program.DisposeHost();

            // Real backend state on startup; failures surface as status text, never as exceptions.
            Dispatcher.UIThread.Post(() => _ = viewModel.InitializeAsync());
        }

        base.OnFrameworkInitializationCompleted();
    }
}
