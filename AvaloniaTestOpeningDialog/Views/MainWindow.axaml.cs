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

        // ���������� ���������� ��������� ��� ��������� ����
        this.WhenActivated(disposables =>
        {
            ViewModel.ShowDialogInteraction.RegisterHandler(async interaction =>
            {
                // ������� ���������� ���� ����� �������
                var dialog = dialogFactory();

                // �������� ViewModel ������� � �������������� �� �������� �������
                var dialogVm = dialog.DataContext as MyDialogViewModel;
                dialogVm!.Init(interaction.Input);

                // ���������� ������ �������� � ���� ���������
                var res = await dialog.ShowDialog<MyDialogResult?>(this);

                // �������� ��������� ������� � interaction
                interaction.SetOutput(res);
            }).DisposeWith(disposables);// �������������� ������� ��� �����������
        });
    }

}
