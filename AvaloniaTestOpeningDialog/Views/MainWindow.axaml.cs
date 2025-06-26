using Avalonia.ReactiveUI;
using Model;
using ReactiveUI;
using System;
using System.Reactive.Disposables;
using ViewModels;

namespace AvaloniaTestOpeningDialog.Views;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    public MainWindow(MainWindowViewModel viewModel,
                    Func<MyDialogParams, MyDialogWindow> dialogFactory,
                    Func<MyDialogParams, MyDialogViewModel> dialogVmFactory)
    {
        InitializeComponent(); 
        
        ViewModel = viewModel;

        // Добавление реактивных биндингов при активации окна
        this.WhenActivated(disposables =>
        {
            ViewModel.ShowDialogInteraction.RegisterHandler(async interaction =>
            {
                // Создаем ViewModel через фабрику
                var dialogVm = dialogVmFactory(interaction.Input);

                // Создаем окно через фабрику, передавая ViewModel в конструктор
                var dialog = dialogFactory(interaction.Input); // Если нужно, можно передавать параметры и в окно

                // Устанавливаем ViewModel (ReactiveWindow сам свяжет ViewModel с DataContext)
                dialog.ViewModel = dialogVm;

                // Показываем диалог и ждем результат
                var res = await dialog.ShowDialog<MyDialogResult?>(this);
                interaction.SetOutput(res);
            }).DisposeWith(disposables);
        });
    }

}
