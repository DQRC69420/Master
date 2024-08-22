using IlterisDictionary.Helper;
using IlterisDictionaryLibrary.Data;
using IlterisDictionaryLibrary.ViewModels;
using Microsoft.AspNetCore.Components;

namespace IlterisDictionary.Pages
{
    public partial class Dictionary
    {
        private string _currentString = string.Empty;


        //private readonly List<IlterisDictionaryEntryVm> _dictionaryEntries = [];

        //private void LoadDictionaryEntries()
        //{
        //    _dictionaryEntries.Clear();

        //    for (int i = 0; i < 100; i++)
        //    {
        //        var array = new[] { $"{i}" };
        //        var item = new IlterisDictionaryEntryVm(
        //            protoTurkicRoot: array,
        //            turkishVariant: array,
        //            furtherReading: array,
        //            loanwords: array,
        //            meaning: $"{i}",
        //            otherVariants: array,
        //            furtherReadings: array,
        //            relatedTo: array);
        //        _dictionaryEntries.Add(item);
        //    }
        //}


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
