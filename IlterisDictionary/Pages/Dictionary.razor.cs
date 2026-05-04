using IlterisDictionary.Helper;
using IlterisDictionaryLibrary.Data;
using IlterisDictionaryLibrary.ViewModels;
using Microsoft.AspNetCore.Components;
using System.Diagnostics;

namespace IlterisDictionary.Pages
{
    public partial class Dictionary
    {
        private string _searchString = string.Empty;



        private string GetMeaningLink(IlterisDictionaryEntryVm entry)
        {
            return $"{Routes.Meanings}#{entry.EntryID}";
        }

        
        private string GetRelationsLink(Guid entryId)
        {
            return $"{Routes.RelatedTo}#{entryId}";
        }

        public string GetVariantLink(IlterisDictionaryLibrary.Data.TurkicVariants turkicVariants)
        {
            return $"#{turkicVariants.VariantID}";
        }
    }
}
