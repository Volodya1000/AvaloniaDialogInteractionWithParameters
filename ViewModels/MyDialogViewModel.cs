using Model;
using ReactiveUI;
using System.Reactive;

namespace ViewModels;

public class MyDialogViewModel : ReactiveObject
{
    public MyDialogParams Params { get; }
    public ReactiveCommand<Unit, Unit> AcceptCommand { get; }
    public MyDialogResult? Result { get; private set; }

    private string _userInput;
    public string UserInput
    {
        get => _userInput;
        set => this.RaiseAndSetIfChanged(ref _userInput, value);
    }

    public MyDialogViewModel(MyDialogParams param)
    {
        Params = param ?? throw new ArgumentNullException(nameof(param));
        UserInput = param.Value;

        AcceptCommand = ReactiveCommand.Create(() =>
        {
            Result = new MyDialogResult { Answer = UserInput };
        });
    }
}