using IlterisDictionaryLibrary.ViewModels;

namespace IlterisDictionary.Pages
{
    public partial class Meanings
    {
    IEnumerable<MeaningEntryVm> _meaningEntryVms = [];

        public void LoadMeaningEntries(IEnumerable<IlterisDictionaryEntryVm> dictionaryEntryVms)
        {
            _meaningEntryVms = dictionaryEntryVms.Select(d => new MeaningEntryVm(d.EntryId)
            {
                MeaningDescription = d.Meaning,
                Word = d.TurkishVariant
            });
        }
    }
}

