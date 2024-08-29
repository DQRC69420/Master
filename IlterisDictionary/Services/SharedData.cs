using IlterisDictionaryLibrary.DataProviders;
using IlterisDictionaryLibrary.ViewModels;
using System.Diagnostics;

namespace IlterisDictionary.Services
{
    public class SharedData
    {
        private const int _chunkSize = 100;

        public readonly List<IlterisDictionaryEntryVm> DictionaryEntries = [];

        public IEnumerable<IlterisDictionaryEntryVm>? ShownEntries { get; set; } = [];

        private readonly IDictionaryDataProvider _dataProvider = new JsonDictionaryProvider();

        public async void LoadDictionaryEntries()
        {
            try
            {
                DictionaryEntries.Clear();

                foreach (var entry in (await _dataProvider.DeserializeAll()).Select(e => new IlterisDictionaryEntryVm(e.Value)))
                {
                    DictionaryEntries.Add(entry);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Fail("something went wrong");
            }
        }


        public void LoadNextChunk(int startingPoint)
        {
            int ending = startingPoint + _chunkSize;
            if (int.IsNegative(ending))
            {
                ending = 0;
            }

            System.Diagnostics.Debug.Assert(ending > startingPoint, "shouldnt be possible");

            List<IlterisDictionaryEntryVm> list = new(100);
            for (int i = startingPoint; i < ending; i++)
            {
                if (i == DictionaryEntries.Count)
                {
                    break;
                }
                list.Add(DictionaryEntries[i]);
            }
            ShownEntries = list;
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
