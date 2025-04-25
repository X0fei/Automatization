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
        public static bool IsTemplateValid(string templatePath)
        {
            return File.Exists(templatePath) && Path.GetExtension(templatePath).ToLower() == ".docx";
        }

        public static void CreateDocumentFromTemplate(string templatePath, string outputFile, Dictionary<string, string> replacements)
        {
            if (IsTemplateValid(templatePath))
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
