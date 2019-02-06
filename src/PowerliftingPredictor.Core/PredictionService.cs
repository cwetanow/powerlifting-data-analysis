using PowerliftingPredictor.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PowerliftingPredictor.Core
{
	public class PredictionService
	{
		public string PredictPlace(MeetResult expectedResult, IEnumerable<MeetResult> dataset)
		{
			var sameDivisionResults = dataset
				.Where(result =>// result.Division == expectedResult.Division &&
				result.Equipment == expectedResult.Equipment &&
				result.WeightClass == expectedResult.WeightClass &&
				result.Sex == expectedResult.Sex)
				.ToList();

			var k = (int)Math.Sqrt(sameDivisionResults.Count);

			var nearestNeighbours = GetNearestNeighbours(expectedResult, k, sameDivisionResults);

			var place = nearestNeighbours
				.GroupBy(n => n.Place)
				.Select(group => new { classification = group.Key, distanceSum = group.Select(x => 1 / GetDistance(expectedResult, x)).Sum() })
				.OrderByDescending(x => x.distanceSum)
				.Select(x => x.classification)
				.FirstOrDefault();

			return place;
		}

		private ICollection<MeetResult> GetNearestNeighbours(MeetResult expectedResult, int k, IEnumerable<MeetResult> dataset)
		{
			var neighbours = dataset
				.OrderBy(r => GetDistance(expectedResult, r))
				.Take(k)
				.ToList();

			return neighbours;
		}

		private double GetDistance(MeetResult expectedResult, MeetResult result)
		{
			var distance = Math.Pow(expectedResult.Total.Value - result.Total.Value, 2);
			distance += Math.Pow(expectedResult.Age.Value - result.Age.Value, 2);
			distance += Math.Pow(expectedResult.Bodyweight.Value - result.Bodyweight.Value, 2);

			distance = Math.Sqrt(distance);

			return distance;
		}
	}
}
