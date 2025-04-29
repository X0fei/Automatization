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
                ConfirmationModalWindow confirm = new("��������� ���� �� ��������� ��� ������� ������ �� ��������.\n�� ������ ���������� �������� ���������?");
                await confirm.ShowDialog(this);  // ������� ��������������, ���� ���� ������
                
                if (confirm.ShouldContinue)
                {
                    // ���������� �������� ��������� ���� � ������� ������
                    DocumentCreation();
                }
                else
                {
                    // ������������ ������� ��������
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

            // �������� ���� � ����� �����������
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            // ��������� ��� �����
            string outputFileName = Path.Combine(documentsPath, Name.Text + " � ���������.docx");

            // �������� ���������� ��� ��� �����
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
                ControlStyler.MessageTextBlock(MessageTextBlock, 1, "�������� ������!");
            }
            catch (FileNotFoundException)
            {
                ErrorModalWindow errorWindow = new("������ �� ������ ��� ��������.");
                await errorWindow.ShowDialog(this);  // ���� ������ �� ������
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