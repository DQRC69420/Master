using IlterisDictionaryLibrary.Data;

namespace IlterisDictionaryLibrary.ViewModels
{
    public class MeaningEntryVm : VmBase
    {
        public MeaningEntryVm(Guid ilterisDictionaryEntryID)
        {
            _meaningEntry = new MeaningEntry(ilterisDictionaryEntryID);
        }


        private readonly MeaningEntry _meaningEntry;


        public Guid IlterisDictionaryEntryID
        {
            get => _meaningEntry.IlterisDictionaryEntryID;
        }


        public IEnumerable<string> Word
        {
            get => _meaningEntry.Word;
            init => _meaningEntry.Word = value;
        }


        public string MeaningDescription
        {
            get => _meaningEntry.MeaningDescription;
            init => _meaningEntry.MeaningDescription = value;
        }
    }
}
