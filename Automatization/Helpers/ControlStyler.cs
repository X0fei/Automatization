using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automatization.Helpers
{
    public static class ControlStyler
    {
        public static void HighlightIfEmpty(TextBox textBox)
        {
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.BorderBrush = Brushes.Red;
                textBox.BorderThickness = new Thickness(2);
            }
            else
            {
                textBox.BorderBrush = Brushes.Gray;
                textBox.BorderThickness = new Thickness(1);
            }
        }

        public static void HighlightAll(params TextBox[] textBoxes)
        {
            foreach (var textBox in textBoxes)
                HighlightIfEmpty(textBox);
        }

        public static void MessageTextBlock(TextBlock textBlock, int type, string? message)
        {
            textBlock.Text = message;

            switch (type)
            {
                case 1:
                    textBlock.Foreground = Brushes.Green;
                    break;
                case 2:
                    textBlock.Foreground = Brushes.Red;
                    break;
                default:
                    textBlock.Foreground = Brushes.Black;
                    break;
            }
        }
    }
}
