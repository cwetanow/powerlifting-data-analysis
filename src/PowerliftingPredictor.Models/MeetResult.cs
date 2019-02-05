using PowerliftingPredictor.Models.Enums;

namespace PowerliftingPredictor.Models
{
	public class MeetResult
	{
		public int MeetId { get; set; }

		public string Name { get; set; }

		public Gender Sex { get; set; }

		public Equipment Equipment { get; set; }

		public string Division { get; set; }

		public double Bodyweight { get; set; }

		public double WeightClass { get; set; }

		public double Squat { get; set; }

		public double Bench { get; set; }

		public double Deadlift { get; set; }

		public double Total { get; set; }

		public int Place { get; set; }

		public double Wilks { get; set; }
	}
}
