using Avalonia.Controls;
using System.Collections.Generic;

namespace Automatization
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var replacements = new Dictionary<string, string>
            {
                { "{{Name}}", Name.Text },
                { "{{Date}}", Date.Text },
                { "{{Comment}}", Comment.Text }
            };
            WordTemplateProcessor.ReplacePlaceholders("../assets/template.docx", "../assets/result.docx", replacements);
        }
    }
}