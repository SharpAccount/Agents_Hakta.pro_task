using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using DemoExamTraining2.ViewModels;

namespace DemoExamTraining2.Views;

public partial class AddOrEditWindow : Window
{
    public AddOrEditWindow(bool isOnEdit)
    {
        InitializeComponent();
        DataContext = new AddOrEditViewModel(isOnEdit);
    }
}