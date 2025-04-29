using Automatization.Helpers;
using Automatization.Logic;
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

namespace Automatization.Windows
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void CreateDocumentButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            ControlStyler.MessageTextBlock(MessageTextBlock, 0, null);

            if (!ValidationProcessor.AreRequiredFieldsFilled(Name.Text, Animal.Text, Comment.Text))
            {
                ConfirmationModalWindow confirm = new("Некоторые поля не заполнены или состоят только из пробелов.\nВы хотите продолжить создание документа?");
                await confirm.ShowDialog(this);  // Выводим предупреждение, если поля пустые
                
                if (confirm.ShouldContinue)
                {
                    // Продолжаем создание документа даже с пустыми полями
                    DocumentCreation();
                }
                else
                {
                    // Пользователь отменил действие
                    ControlStyler.HighlightAll(Name, Animal, Comment);
                }
            }
            else
            {
                DocumentCreation();
            }
        }

        private async void DocumentCreation()
        {
            string templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Templates", "template.docx");

            // Получаем путь к папке «Документы»
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            // Начальное имя файла
            string outputFileName = Path.Combine(documentsPath, Name.Text + " — шаблонный.docx");

            // Получаем уникальное имя для файла
            string uniqueFileName = WordTemplateProcessor.GetUniqueFileName(outputFileName, documentsPath);

            var replacements = new Dictionary<string, string?>
            {
                { "{{Name}}", Name.Text },
                { "{{Animal}}", Animal.Text },
                { "{{Comment}}", Comment.Text }
            };

            try
            {
                TemplateProcessor.CreateDocumentFromTemplate(templatePath, uniqueFileName, replacements);
                ControlStyler.MessageTextBlock(MessageTextBlock, 1, "Документ создан!");
            }
            catch (FileNotFoundException)
            {
                ErrorModalWindow errorWindow = new("Шаблон не найден или повреждён.");
                await errorWindow.ShowDialog(this);  // Если шаблон не найден
            }
        }

        private void TextBox_KeyUp(object? sender, Avalonia.Input.KeyEventArgs e)
        {
            if (sender is Avalonia.Controls.TextBox textBox)
            {
                ControlStyler.HighlightIfEmpty(textBox);
            }
            ControlStyler.MessageTextBlock(MessageTextBlock, 0, null);
        }
    }
}