using IlterisDictionaryLibrary.ViewModels;
using System.Diagnostics;

namespace IlterisDictionary.Services
{
    public class SharedData
    {
        public readonly List<IlterisDictionaryEntryVm> DictionaryEntries = [];

        public void LoadDictionaryEntries()
        {
            try
            {

                DictionaryEntries.Clear();

                for (int i = 0; i < 100; i++)
                {
                    var array = new[] { $"{i}" };
                    var item = new IlterisDictionaryEntryVm(
                        protoTurkicRoot: array,
                        turkishVariant: array,
                        furtherReading: array,
                        loanwords: array,
                        meaning: $"{i}",
                        otherVariants: array,
                        furtherReadings: array,
                        relatedTo: array);
                    DictionaryEntries.Add(item);
                }
            }
            catch (Exception ex) 
            {
                Debug.Fail("something went wrong");
            }
        }


        public IEnumerable<MeaningEntryVm> LoadMeaningEntries()
        {
            return DictionaryEntries.Select(e => new MeaningEntryVm(e.EntryID) { MeaningDescription = e.Meaning, Word = e.TurkishVariant });
        }


        public IlterisDictionaryEntryVm? GetDictionaryEntry(Guid ID)
        {
            return DictionaryEntries.FirstOrDefault(e => Equals(e.EntryID, ID));
        }
    }
}
