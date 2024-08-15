namespace IlterisDictionaryLibrary.Data
{
    public class IlterisDictionaryEntry
    {
        public Guid EntryId { get; } = Guid.NewGuid();

        public IEnumerable<string> ProtoTurkicRoot { get; set; } = [];

        public IEnumerable<string> TurkishVariant { get; set; } = [];

        public IEnumerable<string> Loanwords { get; set; } = [];

        public string Meaning { get; set; } = string.Empty;

        public IEnumerable<string> FurtherReadings { get; set; } = [];

        public IEnumerable<string> OtherVariants { get; set; } = [];

        public IEnumerable<string> RelatedTo { get; set; } = [];
    }
}
