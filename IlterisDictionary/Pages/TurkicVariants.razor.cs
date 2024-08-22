using IlterisDictionaryLibrary.Data;

namespace IlterisDictionary.Pages
{
    public partial class TurkicVariants
    {
        public TurkicVariants(Guid id)
        {
            OtherVariants = SharedData.GetDictionaryEntry(id)?.OtherVariants;
        }


        public IlterisDictionaryLibrary.Data.TurkicVariants? OtherVariants { get; }

        public IEnumerable<string>? LanguageNames => OtherVariants?.LanguageVariantMap.Keys.Select(key => key.ToString().Replace("_", " ")).ToArray();

        public IEnumerable<string>? Variants => OtherVariants?.LanguageVariantMap.Values.ToArray();
    }
}
