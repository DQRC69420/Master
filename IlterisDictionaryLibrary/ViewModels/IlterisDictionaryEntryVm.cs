using IlterisDictionaryLibrary.Data;

namespace IlterisDictionaryLibrary.ViewModels
{
    public class IlterisDictionaryEntryVm : VmBase
    {
        public IlterisDictionaryEntryVm(IEnumerable<string> protoTurkicRoot, IEnumerable<string> turkishVariant, IEnumerable<string> furtherReading, IEnumerable<string> loanwords, string meaning, IEnumerable<string> otherVariants, IEnumerable<string> furtherReadings, IEnumerable<string> relatedTo)
        {
            _entry = new IlterisDictionaryEntry()
            {
                ProtoTurkicRoot = protoTurkicRoot ?? throw new ArgumentNullException(nameof(protoTurkicRoot)),
                TurkishVariant = turkishVariant ?? throw new ArgumentNullException(nameof(turkishVariant)),
                Meaning = meaning ?? throw new ArgumentNullException(nameof(meaning)),
                Loanwords = loanwords ?? throw new ArgumentNullException(nameof(loanwords)),
                FurtherReadings = furtherReadings ?? throw new ArgumentNullException(nameof(furtherReadings)),
                RelatedTo = relatedTo ?? throw new ArgumentNullException(nameof(relatedTo)),
            };
        }



        private readonly IlterisDictionaryLibrary.Data.IlterisDictionaryEntry _entry;


        public Guid EntryID => _entry.EntryID;


        public IEnumerable<string> ProtoTurkicRoot
        {
            get => _entry.ProtoTurkicRoot;
            init => _entry.ProtoTurkicRoot = value;
        }

        public IEnumerable<string> TurkishVariant
        {
            get => _entry.TurkishVariant;
            init => _entry.TurkishVariant = value;
        }


        public IEnumerable<string> FurtherReading
        {
            get => _entry.FurtherReadings;
            init => _entry.FurtherReadings = value;
        }


        public IEnumerable<string> Loanwords
        {
            get => _entry.Loanwords;
            init => _entry.Loanwords = value;
        }


        public string Meaning
        {
            get => _entry.Meaning;
            init => _entry.Meaning = value;
        }


        public TurkicVariants OtherVariants
        {
            get => _entry.OtherVariants;
            set => _entry.OtherVariants = value;
        }


        public IEnumerable<string> FurtherReadings
        {
            get => _entry.FurtherReadings;
            init => _entry.FurtherReadings = value;
        }


        public IEnumerable<string> RelatedTo
        {
            get => _entry.RelatedTo;
            init => _entry.RelatedTo = value;
        }
    }
}
