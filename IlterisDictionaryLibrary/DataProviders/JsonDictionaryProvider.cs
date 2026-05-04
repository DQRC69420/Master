using IlterisDictionaryLibrary.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using IlterisDictionaryLibrary.ViewModels;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components;

namespace IlterisDictionaryLibrary.DataProviders
{
	public class JsonDictionaryProvider : IDictionaryDataProvider
	{
		private readonly NavigationManager _manager;

		public JsonDictionaryProvider(NavigationManager manager)
		{
			_manager = manager;
		}

		//private readonly string _path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData, Environment.SpecialFolderOption.Create), "IlterisDictionary", "IlterisDictionaryData.json");
		//private const string _dictionaryFolderName = "IlterisDictionaryData";
		//private const string _dictionaryFileName = "IlterisDictionary";

		private readonly string _path = @"\Resources\IlterisDictionaryData.json";
		public async Task<Dictionary<Guid, IlterisDictionaryEntry>> DeserializeAll()
		{
			try
			{
				var httpRequest = new HttpClient();

				httpRequest.DefaultRequestHeaders.CacheControl = new System.Net.Http.Headers.CacheControlHeaderValue() { NoCache = true };

				var baseUri = _manager.BaseUri;

				var result = await httpRequest.GetFromJsonAsync<Dictionary<Guid, IlterisDictionaryEntry>>(baseUri + _path, new JsonSerializerOptions() { AllowTrailingCommas = true, ReadCommentHandling = JsonCommentHandling.Skip }) ?? [];

				return result;
				//if (result.Count != 0)
				//{
				//	var deserializedObject = System.Text.Json.JsonSerializer.Deserialize<Dictionary<Guid, IlterisDictionaryEntry>>(result);
				//	return deserializedObject ?? [];
				//}
			}
			catch (Exception ex)
			{
				Debug.Fail("something bad happened");
			}
			return [];
		}

		[Obsolete]
		public async void Serialize(Dictionary<Guid, IlterisDictionaryEntry> entries)
		{
			try
			{
				using var fileStream = new FileStream(_path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
				//using var reader = new StreamReader(fileStream, Encoding.UTF8);
				var jsonNode = await JsonNode.ParseAsync(fileStream, new JsonNodeOptions() { PropertyNameCaseInsensitive = true });
				var jsonArray = jsonNode?.AsArray();
				foreach (var entry in entries)
				{
					jsonArray?.Add(entry);
				}

				File.WriteAllText(_path, jsonArray?.ToJsonString(), Encoding.UTF8);
			}
			catch (Exception ex)
			{
				Debug.Fail("Something went wrong");
			}
		}

		[Obsolete]
		public async Task<Dictionary<Guid, IlterisDictionaryEntry>> DeserializeRange(int startIndex, int count)
		{
			using var fileStream = new FileStream(_path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write);
			using var jsonDocument = await System.Text.Json.JsonDocument.ParseAsync(fileStream);

			var root = jsonDocument.RootElement;

			Dictionary<Guid, IlterisDictionaryEntry> results = [];
			if (root.ValueKind == JsonValueKind.Array)
			{
				foreach (var item in root.EnumerateArray().Skip(startIndex).Take(count))
				{
					KeyValuePair<Guid, IlterisDictionaryEntry> pair = System.Text.Json.JsonSerializer.Deserialize<KeyValuePair<Guid, IlterisDictionaryEntry>>(item.GetRawText());
					results.Add(pair.Key, pair.Value);
				}
			}
			return results;
		}



	}
}
