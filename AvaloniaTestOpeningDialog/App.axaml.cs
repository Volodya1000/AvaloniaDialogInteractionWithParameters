using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using AvaloniaTestOpeningDialog.Views;
using Microsoft.Extensions.DependencyInjection;
using Model;
using System;
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
        services.AddSingleton<MainWindow>();

        // Remove this line as we'll create it via factory
        // services.AddTransient<MyDialogViewModel>();

        services.AddTransient<MyDialogWindow>(sp =>
            new MyDialogWindow(sp.GetRequiredService<MyDialogViewModel>()));

        // Simplified factory that creates the VM with parameters
        services.AddTransient<Func<MyDialogParams, MyDialogViewModel>>(provider =>
            param => new MyDialogViewModel(param));

        // Simplified window factory that uses the VM factory
        services.AddTransient<Func<MyDialogParams, MyDialogWindow>>(provider =>
            param =>
            {
                var vmFactory = provider.GetRequiredService<Func<MyDialogParams, MyDialogViewModel>>();
                var vm = vmFactory(param);
                return new MyDialogWindow(vm);
            });

        var provider = services.BuildServiceProvider();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = provider.GetRequiredService<MainWindow>();
        }

        base.OnFrameworkInitializationCompleted();
    }



}