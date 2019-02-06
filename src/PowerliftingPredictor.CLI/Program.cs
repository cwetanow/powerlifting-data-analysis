using System;
using System.Linq;
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

			var (results, meets) = helper.LoadData(@"./Data/openpowerlifting.csv");

			var expectedResult = new MeetResult {
				Sex = "M",
				Equipment = "Raw",
				Division = "Juniors",
				WeightClass = "105",
				Total = 585,
				Age = 23,
				Bodyweight = 98,
				Squat = 220,
				Bench = 125,
				Deadlift = 240
			};

			var (place, percentile) = new PredictionService()
				.PredictPlace(expectedResult, results);

			System.Console.WriteLine($"Place {place}, Percentile {percentile * 100:F2}%");
		}
	}
}
