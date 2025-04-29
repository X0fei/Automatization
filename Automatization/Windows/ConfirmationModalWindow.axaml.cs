using Automatization.Windows;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace Automatization;

public partial class ConfirmationModalWindow : BaseModalWindow
{
    public bool ShouldContinue { get; private set; } = false;

    public ConfirmationModalWindow() : base("Сообщение.")
    {
        InitializeComponent();
    }
    public ConfirmationModalWindow(string message) : base(message)
    {
        InitializeComponent();
        MessageText.Text = message;
    }

    private void NoButton_Click(object? sender, RoutedEventArgs e)
    {
        ShouldContinue = false;
        Close();
    }

    private void YesButton_Click(object? sender, RoutedEventArgs e)
    {
        ShouldContinue = true;
        Close();
    }
}