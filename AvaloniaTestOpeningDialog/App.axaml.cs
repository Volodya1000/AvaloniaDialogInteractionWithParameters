using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using AvaloniaTestOpeningDialog.ViewModels;
using AvaloniaTestOpeningDialog.Views;
using Microsoft.Extensions.DependencyInjection;
using ViewModels;

namespace AvaloniaTestOpeningDialog;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var services = new ServiceCollection(); 
        services.AddSingleton<MainWindowViewModel>(); 
        services.AddTransient<MyDialogViewModel>();
        services.AddTransient<MyDialogWindow>(sp => new MyDialogWindow(sp.GetRequiredService<MyDialogViewModel>())); 
        services.AddTransient<MainWindow>(sp => 
        new MainWindow(sp.GetRequiredService<MainWindowViewModel>(), () => sp.GetRequiredService<MyDialogWindow>()));

        var provider = services.BuildServiceProvider();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = provider.GetRequiredService<MainWindow>();
        }

        base.OnFrameworkInitializationCompleted();
    }



}