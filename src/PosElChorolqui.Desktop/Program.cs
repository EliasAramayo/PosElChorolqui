using System;
using Avalonia;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PosElChorolqui.Desktop.Services;
using PosElChorolqui.Desktop.ViewModels;

namespace PosElChorolqui.Desktop;

sealed class Program
{
    private static IHost? s_host;

    public static IServiceProvider Services =>
        s_host?.Services ?? throw new InvalidOperationException("El host aún no fue iniciado.");

    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    {
        s_host = CreateHostBuilder(args).Build();
        try
        {
            BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
        }
        finally
        {
            DisposeHost();
        }
    }

    public static void DisposeHost()
    {
        s_host?.Dispose();
        s_host = null;
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(configuration => configuration
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true))
            .ConfigureServices((context, services) =>
            {
                var section = context.Configuration.GetSection("LocalHost");
                var baseUrl = section.GetValue<string>("BaseUrl") ?? "http://127.0.0.1:5117";
                var timeout = TimeSpan.FromSeconds(section.GetValue<int?>("TimeoutSeconds") ?? 5);

                services.AddHttpClient<BackendHealthClient>(client =>
                {
                    client.BaseAddress = new Uri(baseUrl, UriKind.Absolute);
                    client.Timeout = timeout;
                });

                services.AddSingleton<MainViewModel>();
            });

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
#if DEBUG
            .WithDeveloperTools()
#endif
            .WithInterFont()
            .LogToTrace();
}
