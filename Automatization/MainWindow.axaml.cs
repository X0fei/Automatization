using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using DocumentFormat.OpenXml.Vml;
using MsBox.Avalonia;
using MsBox.Avalonia.Dto;
using MsBox.Avalonia.Enums;
using MsBox.Avalonia.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Path = System.IO.Path;

namespace Automatization
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async Task ShowError()
        {
            var box = MessageBoxManager.GetMessageBoxCustom(
                new MessageBoxCustomParams
                {
                    ContentTitle = "Ошибка",
                    ContentMessage = "Шаблон не найден или повреждён.",
                    Icon = MsBox.Avalonia.Enums.Icon.Error,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                    CanResize = false,
                    ShowInCenter = true,
                });

            var result = await box.ShowWindowDialogAsync(this);
        }

        private async Task ShowWarning()
        {
            var box = MessageBoxManager.GetMessageBoxCustom(
                new MessageBoxCustomParams
                {
                    ContentTitle = "Внимание",
                    ContentMessage = "Некоторые поля не заполнены или содержат только пробелы. Вы точно хотите продолжить?",
                    ButtonDefinitions = new List<ButtonDefinition>
                    {
                        new ButtonDefinition { Name = "Да", IsDefault = true},
                        new ButtonDefinition { Name = "Нет", IsCancel = true}
                    },
                    Icon = MsBox.Avalonia.Enums.Icon.Warning,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                    CanResize = false,
                    ShowInCenter = true,
                });

            var result = await box.ShowWindowDialogAsync(this);
        }

        private bool EmptyBoxesCheck()
        {
            bool NullOrWhiteSpace = false;

            if (string.IsNullOrWhiteSpace(Name.Text))
            {
                Name.BorderBrush = Brushes.Red;
                Name.BorderThickness = new Thickness(2);
                NullOrWhiteSpace = true;
            }
            else
            {
                Name.BorderBrush = Brushes.Gray;
                Name.BorderThickness = new Thickness(1);
            }

            if (string.IsNullOrWhiteSpace(Animal.Text))
            {
                Animal.BorderBrush = Brushes.Red;
                Animal.BorderThickness = new Thickness(2);
                NullOrWhiteSpace = true;
            }
            else
            {
                Animal.BorderBrush = Brushes.Gray;
                Animal.BorderThickness = new Thickness(1);
            }

            if (string.IsNullOrWhiteSpace(Comment.Text))
            {
                Comment.BorderBrush = Brushes.Red;
                Comment.BorderThickness = new Thickness(2);
                NullOrWhiteSpace = true;
            }
            else
            {
                Comment.BorderBrush = Brushes.Gray;
                Comment.BorderThickness = new Thickness(1);
            }

            return NullOrWhiteSpace;
        }
        private void Button_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (EmptyBoxesCheck())
            {
                ShowWarning();
            }

            string templatePath = "../assets/template.docx";
            if (TemplateChecker.IsTemplateValid(templatePath))
            {
                // Получаем путь к папке «Документы»
                string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                // Путь, куда мы будем сохранять файл
                string outputFile = Path.Combine(documentsPath, Name.Text + " — шаблонный.docx");

                var replacements = new Dictionary<string, string>
                {
                    { "{{Name}}", Name.Text },
                    { "{{Animal}}", Animal.Text },
                    { "{{Comment}}", Comment.Text }
                };
                WordTemplateProcessor.ReplacePlaceholders(templatePath, outputFile, replacements);
            }
            else
            {
                ShowError();
            }
        }
    }
}