namespace PowerliftingPredictor.Models
{

	public class MeetResult
	{
		public string MeetId { get; set; }

		public string Name { get; set; }

		public string Sex { get; set; }

		public string Equipment { get; set; }

		public double? Age { get; set; }

		public string Division { get; set; }

		public double? Bodyweight { get; set; }

		public string WeightClass { get; set; }

		public double? Squat { get; set; }

		public double? Bench { get; set; }

		public double? Deadlift { get; set; }

		public double? Total { get; set; }

		public string Place { get; set; }

		public double? Wilks { get; set; }

		public static MeetResult Create(string meetId, string name, string sex, string equipment, string age, string division, string bodyweight, string weightClass, string squat, string bench, string deadlift, string total, string place, string wilks)
		{
			var result = new MeetResult
			{
				MeetId = meetId,
				Name = name,
				Division = division,
				Sex = sex,
				WeightClass = weightClass,
				Equipment = equipment,
				Place = place,
				Age = !string.IsNullOrEmpty(age) ? double.Parse(age) : (double?) null,
				Bodyweight = !string.IsNullOrEmpty(bodyweight) ? double.Parse(bodyweight) : (double?) null,
				Squat = !string.IsNullOrEmpty(squat) ? double.Parse(squat) : (double?) null,
				Bench = !string.IsNullOrEmpty(bench) ? double.Parse(bench) : (double?) null,
				Deadlift = !string.IsNullOrEmpty(deadlift) ? double.Parse(deadlift) : (double?) null,
				Total = !string.IsNullOrEmpty(total) ? double.Parse(total) : (double?) null,
				Wilks = !string.IsNullOrEmpty(wilks) ? double.Parse(wilks) : (double?) null
			};

			return result;
		}
	}
}
