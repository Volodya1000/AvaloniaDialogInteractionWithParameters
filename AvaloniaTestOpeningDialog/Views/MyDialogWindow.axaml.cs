using Avalonia.ReactiveUI;
using AvaloniaTestOpeningDialog.ViewModels;
using ReactiveUI;
using System.Reactive.Disposables;

namespace AvaloniaTestOpeningDialog;

public partial class MyDialogWindow : ReactiveWindow<MyDialogViewModel>
{
    public MyDialogWindow(MyDialogViewModel viewModel)
    {
        InitializeComponent();
        ViewModel = viewModel;

        this.WhenActivated(disposables =>
        {
            this.BindCommand(ViewModel, vm => vm.AcceptCommand, v => v.AcceptButton)
                .DisposeWith(disposables);

            ViewModel.AcceptCommand
                .InvokeCommand(ReactiveCommand.Create(() => Close(ViewModel.Result)))
                .DisposeWith(disposables);
        });
    }
}
