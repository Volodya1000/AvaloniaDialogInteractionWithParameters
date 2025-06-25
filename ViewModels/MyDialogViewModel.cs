using Model;
using ReactiveUI;
using System.Reactive;

namespace ViewModels;

public class MyDialogViewModel : ReactiveObject
{
    public MyDialogParams Params { get; private set; } = new();
    public ReactiveCommand<Unit, Unit> AcceptCommand { get; }
    public MyDialogResult? Result { get; private set; }

    private string _userInput = string.Empty;
    public string UserInput
    {
        get => _userInput;
        set => this.RaiseAndSetIfChanged(ref _userInput, value);
    }

    public MyDialogViewModel()
    {
        AcceptCommand = ReactiveCommand.Create(() =>
        {
            Result = new MyDialogResult
            {
                Answer = UserInput
            };
        });
    }

    public void Init(MyDialogParams param)
    {
        Params = param;
        UserInput = param.Value;
    }
}
