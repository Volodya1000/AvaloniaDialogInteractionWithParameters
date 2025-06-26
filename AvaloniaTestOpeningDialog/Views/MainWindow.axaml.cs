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

        // ���������� ���������� ��������� ��� ��������� ����
        this.WhenActivated(disposables =>
        {
            ViewModel.ShowDialogInteraction.RegisterHandler(async interaction =>
            {
                // ������� ViewModel ����� �������
                var dialogVm = dialogVmFactory(interaction.Input);

                // ������� ���� ����� �������, ��������� ViewModel � �����������
                var dialog = dialogFactory(interaction.Input); // ���� �����, ����� ���������� ��������� � � ����

                // ������������� ViewModel (ReactiveWindow ��� ������ ViewModel � DataContext)
                dialog.ViewModel = dialogVm;

                // ���������� ������ � ���� ���������
                var res = await dialog.ShowDialog<MyDialogResult?>(this);
                interaction.SetOutput(res);
            }).DisposeWith(disposables);
        });
    }

}
