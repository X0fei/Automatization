using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automatization.Logic
{
    public static class ValidationProcessor
    {
        public static bool AreRequiredFieldsFilled(string name, string animal, string comment)
        {
            return !string.IsNullOrWhiteSpace(name) 
                && !string.IsNullOrWhiteSpace(animal) 
                && !string.IsNullOrWhiteSpace(comment);
        }
    }
}
