using Automatization.Windows;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Automatization;

public partial class ErrorModalWindow : BaseModalWindow
{
    public ErrorModalWindow() : base("���������.")
    {
        InitializeComponent();
    }
    public ErrorModalWindow(string message) : base(message)
    {
        InitializeComponent();
        MessageText.Text = message;
    }
}