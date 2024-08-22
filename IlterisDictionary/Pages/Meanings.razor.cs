using IlterisDictionary.Services;
using IlterisDictionaryLibrary.ViewModels;

namespace IlterisDictionary.Pages
{
    public partial class Meanings 
    {
        public Meanings()
        {
            MeaningEntries = SharedData.LoadMeaningEntries();
        }

        public IEnumerable<MeaningEntryVm> MeaningEntries { get; }
    }
}

