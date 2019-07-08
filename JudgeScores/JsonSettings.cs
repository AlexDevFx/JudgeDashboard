using System.Collections.Generic;
using System.Windows.Forms;

namespace JudgeScores
{
	public partial class ScoresForm
	{
		public class RandomTimerSettingsJson
		{
			public bool IsEnabled { get; set; }
			public int LowerLimit { get; set; }
			public int UpperLimit { get; set; }
			public string FilePath { get; set; }
		}
	}

	public class ButtonConfig
	{
		public Keys? Key { get; set; }
	}

	public class JsonSettings
	{
		public Dictionary<MainActionTypes, string> MainFunctionSounds { get; set; }
		public Dictionary<MainActionTypes, ButtonConfig> MainFunctionBinds { get; set; }
		public Dictionary<MainActionTypes, ButtonConfig> FirstPlayerBinds { get; set; }
		public Dictionary<MainActionTypes, ButtonConfig> SecondPlayerBinds { get; set; }
		public Dictionary<ScoresRange, ushort> FirstPlayerHitsBinds { get; set; }
		public Dictionary<ScoresRange, ushort> SecondPlayerHitsBinds { get; set; }
		public Dictionary<ScoresRange, string> FirstPlayerSounds { get; set; }
		public Dictionary<ScoresRange, string> SecondPlayerSounds { get; set; }
		public double? RoundSeconds { get; set; }
		public double? PauseSeconds { get; set; }
			
		public int? RoundsCount { get; set; }
			
		public ScoresForm.RandomTimerSettingsJson RandomTimer1 { get; set; }
		public ScoresForm.RandomTimerSettingsJson RandomTimer2 { get; set; }
	}
}