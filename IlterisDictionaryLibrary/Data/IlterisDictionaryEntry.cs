namespace IlterisDictionaryLibrary.Data
{
    public class IlterisDictionaryEntry
    {
        public Guid EntryID { get; } = Guid.NewGuid();

        public IEnumerable<string> ProtoTurkicRoot { get; set; } = [];

        public IEnumerable<string> TurkishVariant { get; set; } = [];

        public IEnumerable<string> Loanwords { get; set; } = [];

        public string Meaning { get; set; } = string.Empty;

        public IEnumerable<string> FurtherReadings { get; set; } = [];

        public TurkicVariants OtherVariants { get; set; }

        public IEnumerable<string> RelatedTo { get; set; } = [];

        public bool IsFavourited { get; set; } = false;
    }
}
