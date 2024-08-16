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
        public void Serialize(Dictionary<Guid, IlterisDictionaryEntry> data);

        public void Serialize(IlterisDictionaryEntry data);

        public Dictionary<Guid, IlterisDictionaryEntry> Deserialize();
    }
}
