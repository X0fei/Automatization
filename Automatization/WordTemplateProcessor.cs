using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Collections.Generic;
using System.IO;

namespace Automatization
{
    public static class WordTemplateProcessor
    {
        public static void ReplacePlaceholders(string templatePath, string outputPath, Dictionary<string, string> replacements)
        {
            File.Copy(templatePath, outputPath, true); // копируем шаблон
            
            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(outputPath, true))
            {
                var body = wordDoc.MainDocumentPart.Document.Body;

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

                wordDoc.MainDocumentPart.Document.Save();
            }
        }
    }
}
