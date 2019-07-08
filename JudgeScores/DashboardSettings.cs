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
			
			public RandomTimerSettings RandomTimer1 { get; set; } = new RandomTimerSettings();
			public RandomTimerSettings RandomTimer2 { get; set; } = new RandomTimerSettings();
		}
	}
}