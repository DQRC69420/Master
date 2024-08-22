using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IlterisDictionaryLibrary.Data
{
    public class TurkicVariants(Guid variantID)
    {
        public Guid VariantID { get; } = variantID;

        public string RootWord { get; set; } = string.Empty;

        public Dictionary<TurkicLanguage, string> LanguageVariantMap { get; set; } = [];
    }
}
