using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Automatization.Windows;

public partial class BaseModalWindow : Window
{
    public BaseModalWindow()
    {
        InitializeComponent();
    }
    public BaseModalWindow(string message)
    {
        InitializeComponent();
        MessageText.Text = message;
    }

    private void OkButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        Close();
    }
}