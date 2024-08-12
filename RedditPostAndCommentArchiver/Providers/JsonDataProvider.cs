using Newtonsoft.Json;
using RedditPostAndCommentArchiver.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditPostAndCommentArchiver.Providers
{
    public class JsonDataProvider
    {
        private readonly string _path = "";


        private bool SaveFileExists => File.Exists(_path);

        public JsonDataProvider(string fullPath)
        {
            _path = fullPath;
        }

        public void Serialize(Dictionary<string, Post> idPostMap, string newFileName)
        {
            Dictionary<string, Post> oldEntries = [];
            if (SaveFileExists)
            {
                string oldEntryString = File.ReadAllText(_path);
                oldEntries = JsonConvert.DeserializeObject<Dictionary<string, Post>>(oldEntryString) ?? [];

                WriteToFile(idPostMap, oldEntries, _path);
            }
            else if (!SaveFileExists)
            {
                var newPath = Path.Combine(_path, newFileName);
                newPath = Path.ChangeExtension(newPath, ".json");
                WriteToFile(idPostMap, oldEntries, newPath);
            }
        }

        private void WriteToFile(Dictionary<string, Post> idPostMap, Dictionary<string, Post> oldEntries, string path)
        {
            var newEntries = new Dictionary<string, Post>(oldEntries ?? []);
            foreach (var idPostPair in idPostMap)
            {
                newEntries.TryAdd(idPostPair.Key, idPostPair.Value);
            }

            CreateDirectoryIfNotExist(path);
            File.WriteAllText(path, JsonConvert.SerializeObject(newEntries), Encoding.UTF8);
        }

        private void CreateDirectoryIfNotExist(string path)
        {
            if (!Directory.Exists(Path.GetDirectoryName(path)))
            {
                Directory.CreateDirectory(path);
            }
        }

        public Dictionary<string, Post> Deserialize()
        {
            CreateDirectoryIfNotExist(_path);

            if (!SaveFileExists)
            {
                return [];
            }

            return JsonConvert.DeserializeObject<Dictionary<string, Post>>(File.ReadAllText(_path)) ?? [];
        }
    }
}
