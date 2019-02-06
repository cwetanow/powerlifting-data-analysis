using PowerliftingPredictor.Core.Utils;

namespace PowerliftingPredictor.CLI
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var helper = new DataHelper();

			var (results, meets) = helper.LoadData(@"../../../Data/openpowerlifting.csv");
		}
	}
}
