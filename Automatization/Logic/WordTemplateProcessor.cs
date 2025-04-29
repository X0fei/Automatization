using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.IO;

namespace Automatization.Logic
{
    public static class WordTemplateProcessor
    {
        public static void ReplacePlaceholders(string templatePath, string outputPath, Dictionary<string, string?> replacements)
        {
            File.Copy(templatePath, outputPath, true); // копируем шаблон

            using WordprocessingDocument wordDoc = WordprocessingDocument.Open(outputPath, true);
            var mainDocumentPart = wordDoc.MainDocumentPart;
            if (mainDocumentPart?.Document?.Body == null)
            {
                Console.WriteLine("Ошибка: Тело документа отсутствует. Проверьте, что файл шаблона является допустимым документом Word.");
                return; // Завершаем выполнение метода, чтобы избежать дальнейших ошибок
            }

            var body = mainDocumentPart.Document.Body;

            foreach (var text in body.Descendants<Text>())
            {
                foreach (var pair in replacements)
                {
                    if (text.Text.Contains(pair.Key))
                    {
                        text.Text = text.Text.Replace(pair.Key, pair.Value);
                    }
                }
            }

            // Ensure mainDocumentPart and Document are not null before calling Save
            mainDocumentPart.Document?.Save();
        }

        public static string GetUniqueFileName(string fileName, string directory)
        {
            string filePath = fileName;
            string originalFileName = fileName;
            int counter = 1;

            // Проверяем, существует ли файл, и добавляем (1), (2), ... до тех пор, пока не найдем свободное имя
            while (File.Exists(filePath))
            {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(originalFileName);
                string extension = Path.GetExtension(originalFileName);

                // Формируем новое имя с суффиксом (1), (2) и т. д.
                fileName = $"{fileNameWithoutExtension} ({counter}){extension}";
                filePath = Path.Combine(directory, fileName);
                counter++;
            }

            return filePath;
        }

    }
}
