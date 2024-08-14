namespace IlterisDictionaryLibrary.Data
{
    public class IlterisDictionaryEntry
    {
        public Guid EntryId { get; } = Guid.NewGuid();

        public string ProtoTurkicRoot { get; init; }

        public string TurkishVariant { get; init; }

        public string Loanword { get; init; }

        public string Meaning { get; init; }

        public string FurtherReading { get; init; }

        public string OtherVariants { get; init; }

        public string RelatedTo { get; init; }

        public bool MeaningVisible { get; set; }
    }
}
