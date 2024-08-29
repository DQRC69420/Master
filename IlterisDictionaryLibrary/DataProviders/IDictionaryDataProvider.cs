using IlterisDictionaryLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IlterisDictionaryLibrary.DataProviders
{
    public interface IDictionaryDataProvider
    {
        public  void Serialize(Dictionary<Guid, IlterisDictionaryEntry> entries);

        public Task<Dictionary<Guid, IlterisDictionaryEntry>> DeserializeRange(int startIndex, int count);

        public Task<Dictionary<Guid, IlterisDictionaryEntry>> DeserializeAll();
    }
}
