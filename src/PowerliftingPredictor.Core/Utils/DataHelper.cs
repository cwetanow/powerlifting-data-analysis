using LumenWorks.Framework.IO.Csv;
using PowerliftingPredictor.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
	}
}
