using System.Collections.Generic;
using System.IO;
using LumenWorks.Framework.IO.Csv;
using PowerliftingPredictor.Models;

namespace PowerliftingPredictor.Helpers
{
	public class DataHelper
	{
		public IEnumerable<MeetResult> LoadData(string filePath)
		{
			using (var reader = new CsvReader(new StreamReader(filePath), true))
			{
				var results = new List<MeetResult>();

				while (reader.ReadNextRecord())
				{
					var result = MeetResult.Create(reader[0], reader[1], reader[2], reader[3], reader[4], reader[5], reader[6], reader[7], reader[9], reader[11], reader[13], reader[14], reader[15], reader[16]);

					if (result.Total.HasValue && result.Bodyweight.HasValue && !string.IsNullOrEmpty(result.Division) && !string.IsNullOrEmpty(result.Equipment) && !string.IsNullOrEmpty(result.WeightClass))
					{
						results.Add(result);
					}
				}

				return results;
			}
		}
	}
}
