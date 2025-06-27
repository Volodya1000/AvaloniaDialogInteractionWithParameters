using Avalonia.ReactiveUI;
using ReactiveUI;
using System;
using System.Reactive.Disposables;
using ViewModels;
using ViewModels.DialogInteractionParams;

namespace AvaloniaTestOpeningDialog.Views;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    public MainWindow(
        MainWindowViewModel viewModel,
        Func<MyDialogParams, MyDialogWindow> dialogFactory) 
    {
        InitializeComponent();
        ViewModel = viewModel;

        this.WhenActivated(disposables =>
        {
            ViewModel.ShowDialogInteraction.RegisterHandler(async interaction =>
            {
                var dialog = dialogFactory(interaction.Input);

                var res = await dialog.ShowDialog<MyDialogResult?>(this);

                interaction.SetOutput(res);
            }).DisposeWith(disposables);
        });
    }
}
