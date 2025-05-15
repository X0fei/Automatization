using DocumentFormat.OpenXml.Packaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automatization.Logic
{
    public static class TemplateProcessor
    {
        public static bool IsTemplateValid(string templatePath, string outputFile)
        {
            using WordprocessingDocument wordDoc = WordprocessingDocument.Open(templatePath, true);
            var mainDocumentPart = wordDoc.MainDocumentPart;
            return File.Exists(templatePath)
                && string.Equals(Path.GetExtension(templatePath), ".docx", StringComparison.OrdinalIgnoreCase)
                && mainDocumentPart?.Document?.Body != null;
        }

        public static void CreateDocumentFromTemplate(string templatePath, string outputFile, Dictionary<string, string?> replacements)
        {
            if (IsTemplateValid(templatePath, outputFile))
            {
                WordTemplateProcessor.ReplacePlaceholders(templatePath, outputFile, replacements);
            }
            else
            {
                throw new FileNotFoundException("Шаблон не найден", templatePath);
            }
        }
    }
}
