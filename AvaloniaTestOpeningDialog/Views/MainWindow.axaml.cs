using Avalonia.ReactiveUI;
using Model;
using ReactiveUI;
using System;
using System.Reactive.Disposables;
using ViewModels;

namespace AvaloniaTestOpeningDialog.Views;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    public MainWindow(MainWindowViewModel viewModel, Func<MyDialogWindow> dialogFactory)
    {
        InitializeComponent(); 
        
        ViewModel = viewModel;

        // Добавление реактивных биндингов при активации окна
        this.WhenActivated(disposables =>
        {
            // Привязка команды OpenDialogCommand из ViewModel к кнопке OpenDialogButton в UI
            this.BindCommand(ViewModel, vm => vm.OpenDialogCommand, v => v.OpenDialogButton)
                .DisposeWith(disposables); // Автоматическое удаление привязки при деактивации

            ViewModel.ShowDialogInteraction.RegisterHandler(async interaction =>
            {
                // Создаем диалоговое окно через фабрику
                var dialog = dialogFactory();

                // Получаем ViewModel диалога и инициализируем ее входными данными
                var dialogVm = dialog.DataContext as MyDialogViewModel;
                dialogVm!.Init(interaction.Input);

                // Показываем диалог модально и ждем результат
                var res = await dialog.ShowDialog<MyDialogResult?>(this);

                // Передаем результат обратно в interaction
                interaction.SetOutput(res);
            }).DisposeWith(disposables);// Автоматическая отписка при деактивации
        });
    }

}
