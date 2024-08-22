using IlterisDictionaryLibrary.Data;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using IlterisDictionaryLibrary.ViewModels;
using System.Diagnostics.CodeAnalysis;

namespace IlterisDictionaryLibrary.DataProviders
{
    public class JsonDictionaryProvider : IDictionaryDataProvider
    {
        public JsonDictionaryProvider()
        {
            
        }

        private const string _dictionaryFolderName = "IlterisDictionaryData";
        private const string _dictionaryFileName = "IlterisDictionary";
        private readonly string _path = Path.ChangeExtension(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), _dictionaryFolderName, _dictionaryFileName), ".json");


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
            string? directoryPath = Path.GetDirectoryName(_path);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath ?? "");
            }


        }
    }
}
