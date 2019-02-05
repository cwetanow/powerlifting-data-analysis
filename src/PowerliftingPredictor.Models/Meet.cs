using System;
using System.Collections.Generic;

namespace PowerliftingPredictor.Models
{
	public class Meet
	{
		public int MeetId { get; set; }
		public string Federation { get; set; }
		public DateTime Date { get; set; }
		public string Country { get; set; }
		public string Name { get; set; }

		public IList<MeetResult> Results { get; set; }
	}
}
