using Avalonia.ReactiveUI;
using AvaloniaTestOpeningDialog.ViewModels;
using ReactiveUI;
using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;

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
              .Subscribe(_ =>
              {
                  if (ViewModel.Result != null)
                      Close(ViewModel.Result);
              })
              .DisposeWith(disposables);
        });
    }
}
