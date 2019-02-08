using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using PowerliftingPredictor.Helpers;
using PowerliftingPredictor.Models;
using PowerliftingPredictor.Services;

namespace PowerliftingPredictor
{
	public class Program
	{
		private static readonly PredictionService predictionService = new PredictionService();

		public static IList<MeetResult> Dataset { get; set; }

		public static void Main(string[] args)
		{
			LoadData();

			Console.WriteLine(Dataset.Count);

			DoTest();

			//DoNFoldCrossValidationTest();
		}

		public static void DoTest()
		{
			var random = new Random();
			var elementsToTest = 100;

			var skipCount = random.Next(Dataset.Count);

			var testData = Dataset
				.Skip(skipCount)
				.Take(elementsToTest)
				.ToList();

			var trainingData = Dataset
				.Except(testData)
				.ToList();

			var errors = 0;
			foreach (var item in testData)
			{
				var prediction = predictionService.PredictPlace(item, trainingData);

				if (prediction != item.Place)
				{
					errors++;
				}
			}

			Console.WriteLine($"{errors} errors - {1 - (errors / (double)testData.Count)}% success");
		}

		public static void DoNFoldCrossValidationTest()
		{
			var n = 100;

			var nthOfDataset = Dataset.Count / n;

			var average = 0;

			Console.WriteLine(nthOfDataset);
			Parallel.For(0, n, new ParallelOptions { MaxDegreeOfParallelism = 6 }, (index) => {
				var errors = 0;
				var testData = Dataset
					.Skip(nthOfDataset * index)
					.Take(nthOfDataset)
					.ToList();

				var trainingData = Dataset
					.Except(testData)
					.ToList();

				foreach (var item in testData)
				{
					var prediction = predictionService.PredictPlace(item, trainingData);

					if (prediction != item.Place)
					{
						errors++;
					}
				}

				Console.WriteLine($"{index}: {errors} errors - {1 - (errors / (double)testData.Count)}% success");
			});

			Console.WriteLine($"Average: {(average / (double)Dataset.Count):F2}%");
		}

		public static int Predict(IList<MeetResult> trainingData, IList<MeetResult> testData)
		{
			Console.WriteLine(testData.Count);
			var errors = 0;

			foreach (var item in testData)
			{
				var prediction = predictionService.PredictPlace(item, trainingData);

				if (prediction != item.Place)
				{
					errors++;
				}
			}

			return errors;
		}

		private static void LoadData()
		{
			var path = $"{Directory.GetCurrentDirectory()}/../../../Data/openpowerlifting.csv";

			var helper = new DataHelper();

			var dataset = helper.LoadData(path);

			Dataset = dataset.ToList();
		}
	}
}
