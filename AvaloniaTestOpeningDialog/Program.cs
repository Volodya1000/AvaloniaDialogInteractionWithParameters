using Avalonia;
using Avalonia.ReactiveUI;
using AvaloniaTestOpeningDialog.Views;
using Microsoft.Extensions.DependencyInjection;
using Model;
using System;
using ViewModels;

namespace AvaloniaTestOpeningDialog;

internal sealed class Program
{
    public static IServiceProvider ServiceProvider { get; private set; }

    [STAThread]
    public static void Main(string[] args)
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        ServiceProvider = services.BuildServiceProvider();

        BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<MainWindowViewModel>();
        services.AddTransient<MainWindow>();

        services.AddTransient<Func<MyDialogParams, MyDialogWindow>>(provider =>
            param => new MyDialogWindow(new MyDialogViewModel(param)));
    }

    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace()
            .UseReactiveUI();
}
