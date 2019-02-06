using PowerliftingPredictor.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PowerliftingPredictor.Core
{
	public class PredictionService
	{
		public (string place, double percentile) PredictPlace(MeetResult expectedResult, IEnumerable<MeetResult> dataset)
		{
			var sameDivisionResults = dataset
				.Where(result => result.Division == expectedResult.Division &&
					result.Equipment == expectedResult.Equipment &&
					result.WeightClass == expectedResult.WeightClass &&
					result.Sex == expectedResult.Sex &&
					(!expectedResult.Age.HasValue || result.Age.HasValue) &&
					result.Total.HasValue && result.Bodyweight.HasValue &&
					AreSameMovements(expectedResult, result))
				.ToList();


			var k = (int)Math.Sqrt(sameDivisionResults.Count);

			var nearestNeighbors = GetNearestNeighbours(expectedResult, k, sameDivisionResults);

			var place = nearestNeighbors
				.GroupBy(n => n.Place)
				.Select(group => new { classification = group.Key, distanceSum = group.Select(x => 1 / GetDistance(expectedResult, x)).Sum() })
				.OrderByDescending(x => x.distanceSum)
				.Select(x => x.classification)
				.FirstOrDefault();

			sameDivisionResults.Add(expectedResult);

			var percentile = (sameDivisionResults
				.OrderByDescending(r => r.Total)
				.ThenBy(r => r.Bodyweight)
				.ToList()
				.IndexOf(expectedResult)) / (double)sameDivisionResults.Count;

			return (place, percentile);
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
			distance += Math.Pow(expectedResult.Bodyweight.Value - result.Bodyweight.Value, 2);

			if (expectedResult.Age.HasValue && result.Age.HasValue)
			{
				distance += Math.Pow(expectedResult.Age.Value - result.Age.Value, 2);
			}

			distance = Math.Sqrt(distance);

			return distance;
		}

		private bool AreSameMovements(MeetResult result, MeetResult otherResult)
		{
			// All movements
			if (result.Squat.HasValue && otherResult.Squat.HasValue &&
				result.Bench.HasValue && otherResult.Bench.HasValue &&
				result.Deadlift.HasValue && otherResult.Deadlift.HasValue)
			{
				return true;
			}

			if ((result.Squat.HasValue && otherResult.Squat.HasValue && result.Bench.HasValue && otherResult.Bench.HasValue) ||
				(result.Squat.HasValue && otherResult.Squat.HasValue && result.Deadlift.HasValue && otherResult.Deadlift.HasValue) ||
				(result.Deadlift.HasValue && otherResult.Deadlift.HasValue && result.Bench.HasValue && otherResult.Bench.HasValue))
			{
				return true;
			}

			if ((result.Squat.HasValue && otherResult.Squat.HasValue) ||
				(result.Deadlift.HasValue && otherResult.Deadlift.HasValue) ||
				(result.Bench.HasValue && otherResult.Bench.HasValue))
			{
				return true;
			}

			return false;
		}
	}
}
