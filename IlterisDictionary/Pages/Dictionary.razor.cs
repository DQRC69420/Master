using IlterisDictionary.Helper;
using IlterisDictionaryLibrary.Data;
using IlterisDictionaryLibrary.ViewModels;
using Microsoft.AspNetCore.Components;
using System.Diagnostics;

namespace IlterisDictionary.Pages
{
    public partial class Dictionary
    {
        private string _currentString = string.Empty;



        private string GetMeaningLink(IlterisDictionaryEntryVm entry)
        {
            return $"{Routes.Meanings}#{entry.EntryID}";
        }

        public string GetVariantLink(IlterisDictionaryLibrary.Data.TurkicVariants turkicVariants)
        {
            return $"#{turkicVariants.VariantID}";
        }
    }
}
