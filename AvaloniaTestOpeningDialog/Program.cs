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

        services.AddTransient<MyDialogWindow>(sp =>
            new MyDialogWindow(sp.GetRequiredService<MyDialogViewModel>()));

        services.AddTransient<Func<MyDialogParams, MyDialogViewModel>>(provider =>
            param => new MyDialogViewModel(param));

        services.AddTransient<Func<MyDialogParams, MyDialogWindow>>(provider =>
            param =>
            {
                var vmFactory = provider.GetRequiredService<Func<MyDialogParams, MyDialogViewModel>>();
                var vm = vmFactory(param);
                return new MyDialogWindow(vm);
            });
    }

    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace()
            .UseReactiveUI();
}
