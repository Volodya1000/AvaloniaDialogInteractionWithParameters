using Avalonia.ReactiveUI;
using ReactiveUI;
using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ViewModels;

namespace AvaloniaTestOpeningDialog;

public partial class MyDialogWindow : ReactiveWindow<MyDialogViewModel>
{
    public MyDialogWindow(MyDialogViewModel viewModel)
    {
        InitializeComponent();
        ViewModel = viewModel;

        this.WhenActivated(disposables =>
        {
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
