using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IlterisDictionaryLibrary.Data
{
    public record class MeaningEntry
    {
        public MeaningEntry(Guid ilterisDictionaryEntryID)
        {
            IlterisDictionaryEntryID = ilterisDictionaryEntryID;
        }

        public Guid IlterisDictionaryEntryID { get; }

        public IEnumerable<string> Word { get; set; } = [];

        public string MeaningDescription { get; set; } = string.Empty;
    }
}
