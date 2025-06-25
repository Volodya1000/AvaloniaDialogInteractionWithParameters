using AvaloniaTestOpeningDialog.Models;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace AvaloniaTestOpeningDialog.ViewModels;

public class MainWindowViewModel : ReactiveObject
{
    public Interaction<MyDialogParams, MyDialogResult?> ShowDialogInteraction { get; }
    public ReactiveCommand<Unit, Unit> OpenDialogCommand { get; }
    public string? LastResult { get => _lastResult; set => this.RaiseAndSetIfChanged(ref _lastResult, value); }
    private string? _lastResult;

    public MainWindowViewModel()
    {
        ShowDialogInteraction = new Interaction<MyDialogParams, MyDialogResult?>();
        OpenDialogCommand = ReactiveCommand.CreateFromTask(OpenDialogAsync);
    }

    private async Task OpenDialogAsync()
    {
        var param = new MyDialogParams { Value = "Hello from MainWindow" };
        var result = await ShowDialogInteraction.Handle(param);
        if (result != null)
        {
            LastResult = result.Answer;
        }
    }

}
