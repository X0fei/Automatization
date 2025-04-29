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
                ConfirmationModalWindow confirm = new("��������� ���� �� ��������� ��� ������� ������ �� ��������. �� ������ ���������� �������� ���������?");
                await confirm.ShowDialog(this);  // ������� ��������������, ���� ���� ������
                
                if (confirm.ShouldContinue)
                {
                    // ���������� �������� ��������� ���� � ������� ������
                    string templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Templates", "template.docx");
                    string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    string outputFile = Path.Combine(documentsPath, Name.Text + " � ���������.docx");

                    var replacements = new Dictionary<string, string>
                    {
                        { "{{Name}}", Name.Text },
                        { "{{Animal}}", Animal.Text },
                        { "{{Comment}}", Comment.Text }
                    };

                    try
                    {
                        TemplateProcessor.CreateDocumentFromTemplate(templatePath, outputFile, replacements);
                        ControlStyler.MessageTextBlock(MessageTextBlock, 1, "�������� ������!");
                    }
                    catch (FileNotFoundException)
                    {
                        ControlStyler.MessageTextBlock(MessageTextBlock, 2, "������ �� ������.");  // ���� ������ �� ������
                    }
                }
                else
                {
                    // ������������ ������� ��������
                    ControlStyler.HighlightAll(Name, Animal, Comment);
                }
            }
        }
        private void TextBox_KeyUp(object? sender, Avalonia.Input.KeyEventArgs e)
        {
            ControlStyler.HighlightIfEmpty(sender as Avalonia.Controls.TextBox);
            ControlStyler.MessageTextBlock(MessageTextBlock, 0, null);
        }

        //private async Task ShowError()
        //{
        //    var box = MessageBoxManager.GetMessageBoxCustom(
        //        new MessageBoxCustomParams
        //        {
        //            ContentTitle = "������",
        //            ContentMessage = "������ �� ������.",
        //            Icon = MsBox.Avalonia.Enums.Icon.Error,
        //            WindowStartupLocation = WindowStartupLocation.CenterOwner,
        //            CanResize = false,
        //            ShowInCenter = true,
        //        });

        //    var result = await box.ShowWindowDialogAsync(this);
        //}

        //private async Task ShowWarning()
        //{
        //    var box = MessageBoxManager.GetMessageBoxCustom(
        //        new MessageBoxCustomParams
        //        {
        //            ContentTitle = "��������",
        //            ContentMessage = "��������� ���� �� ��������� ��� �������� ������ �������. �� ����� ������ ����������?",
        //            ButtonDefinitions = new List<ButtonDefinition>
        //            {
        //                new ButtonDefinition { Name = "��", IsDefault = true},
        //                new ButtonDefinition { Name = "���", IsCancel = true}
        //            },
        //            Icon = MsBox.Avalonia.Enums.Icon.Warning,
        //            WindowStartupLocation = WindowStartupLocation.CenterOwner,
        //            CanResize = false,
        //            ShowInCenter = true,
        //        });

        //    var result = await box.ShowWindowDialogAsync(this);
        //}

    }
}