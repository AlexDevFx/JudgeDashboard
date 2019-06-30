namespace JudgeScores
{
	public partial class ScoresForm
	{
		public class RandomTimers
		{
			public bool IsEnabled { get; set; }
			public int LowerLimit { get; set; }
			public int UpperLimit { get; set; }
			public string FilePath { get; set; }
		}
	}
}