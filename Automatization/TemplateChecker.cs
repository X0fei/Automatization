using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.IO;

public static class TemplateChecker
{
    public static bool IsTemplateValid(string templatePath)
    {
        return File.Exists(templatePath) && Path.GetExtension(templatePath).ToLower() == ".docx";
    }
}
