using AvaloniaTestOpeningDialog.Models;
using ReactiveUI;
using System.Reactive;

namespace AvaloniaTestOpeningDialog.ViewModels;

public class MyDialogViewModel : ReactiveObject
{
    public MyDialogParams Params { get; private set; } = new(); public ReactiveCommand<Unit, Unit> AcceptCommand { get; }
    public MyDialogResult? Result { get; private set; }

    public MyDialogViewModel()
    {
        AcceptCommand = ReactiveCommand.Create(() =>
        {
            Result = new MyDialogResult { Answer = $"Dialog reply to: {Params.Value}" };
        });
    }

    public void Init(MyDialogParams param) => Params = param;

}

