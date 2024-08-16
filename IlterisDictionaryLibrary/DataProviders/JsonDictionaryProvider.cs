using IlterisDictionaryLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IlterisDictionaryLibrary.DataProviders
{
    public class JsonDictionaryProvider : IDictionaryDataProvider
    {
        public Dictionary<Guid, IlterisDictionaryEntry> Deserialize()
        {
            throw new NotImplementedException();
        }

        public void Serialize(Dictionary<Guid, IlterisDictionaryEntry> data)
        {
            throw new NotImplementedException();
        }

        public void Serialize(IlterisDictionaryEntry data)
        {
            throw new NotImplementedException();
        }
    }
}
