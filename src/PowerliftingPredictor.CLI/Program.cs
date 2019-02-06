using PowerliftingPredictor.Core;
using PowerliftingPredictor.Core.Utils;
using PowerliftingPredictor.Models;

namespace PowerliftingPredictor.CLI
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var helper = new DataHelper();

			var (results, meets) = helper.LoadData(@"../../../Data/openpowerlifting.csv");

			var expectedResult = new MeetResult {
				Sex = "M",
				Equipment = "Raw",
				Division = "Men Junior 20-23",
				WeightClass = "105",
				Total = 585,
				Age = 23,
				Bodyweight = 98
			};

			var prediction = new PredictionService()
				.PredictPlace(expectedResult, results);

			System.Console.WriteLine(prediction);
		}
	}
}
