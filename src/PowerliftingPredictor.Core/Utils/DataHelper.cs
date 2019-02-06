using LumenWorks.Framework.IO.Csv;
using PowerliftingPredictor.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PowerliftingPredictor.Core.Utils
{
	public class DataHelper
	{
		public (IEnumerable<MeetResult> results, IEnumerable<Meet> meets) LoadData(string resultFilePath, bool loadMeets = false, string meetFilePath = null)
		{
			var meets = new Dictionary<int, Meet>();

			var results = LoadResults(resultFilePath);

			return (results, meets.Select(kvp => kvp.Value).ToList());
		}

		private List<MeetResult> LoadResults(string resultFilePath)
		{
			using (var reader = new CsvReader(new StreamReader(resultFilePath), true))
			{
				var results = new List<MeetResult>();

				var fieldCount = reader.FieldCount;

				var headers = reader.GetFieldHeaders();
				while (reader.ReadNextRecord())
				{
					var result = MeetResult.Create(reader[0], reader[1], reader[2], reader[3], reader[4], reader[5], reader[6], reader[7], reader[9], reader[11], reader[13], reader[14], reader[15], reader[16]);

					results.Add(result);
				}

				return results;
			}
		}

		private async Task LoadMeets(string filePath, IDictionary<int, Meet> meets)
		{
			var reader = new StreamReader(filePath);

			var line = await reader.ReadLineAsync();

			while (true)
			{
				line = await reader.ReadLineAsync();

				if (string.IsNullOrEmpty(line))
				{
					break;
				}

				var meet = ParseMeet(line);

				meets.Add(meet.MeetId, meet);
			}
		}

		private Meet ParseMeet(string stringifiedMeet)
		{
			return null;
		}
	}
}
