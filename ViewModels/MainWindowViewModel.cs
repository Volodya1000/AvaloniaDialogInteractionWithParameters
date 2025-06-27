using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;
using ViewModels.DialogInteractionParams;

namespace ViewModels;

public class MainWindowViewModel : ReactiveObject
{
    public Interaction<MyDialogParams, MyDialogResult?> ShowDialogInteraction { get; } = new();
    public ReactiveCommand<Unit, Unit> OpenDialogCommand { get; }

    private string _userMessage = string.Empty;
    public string UserMessage
    {
        get => _userMessage;
        set => this.RaiseAndSetIfChanged(ref _userMessage, value);
    }

    public MainWindowViewModel()
    {
        OpenDialogCommand = ReactiveCommand.CreateFromTask(OpenDialogAsync);
    }

    private async Task OpenDialogAsync()
    {
        var param = new MyDialogParams { Value = UserMessage };
        var result = await ShowDialogInteraction.Handle(param);
        if (result != null)
        {
            UserMessage = result.Answer;
        }
    }
}
