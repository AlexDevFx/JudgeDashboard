namespace JudgeScores
{
	public partial class ScoresForm
	{
		public class DashboardSettings
		{
			public int RoundsCount { get; set; }
			public int RoundsCompleted { get; set; }
			
			public double RoundSeconds { get; set; }
			public double PauseSeconds { get; set; }
			
			public RandomTimers RandomTimer1 { get; set; }
			public RandomTimers RandomTimer2 { get; set; }
		}
	}
}