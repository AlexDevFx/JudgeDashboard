using System.Collections.Generic;
using Windows.Gaming.Input;

namespace JudgeScores
{
	public partial class ScoresForm
	{
		public class JsonSettings
		{
			public Dictionary<MainActionsType, string> MainFunctionSounds { get; set; }
			public Dictionary<GamepadButtons, MainActionsType> MainFunctionBinds { get; set; }
			public Dictionary<GamepadButtons, ScoresRange> FirstPlayerBinds { get; set; }
			public Dictionary<GamepadButtons, ScoresRange> SecondPlayerBinds { get; set; }
			public Dictionary<ScoresRange, ushort> FirstPlayerHitsBinds { get; set; }
			public Dictionary<ScoresRange, ushort> SecondPlayerHitsBinds { get; set; }
			public Dictionary<ScoresRange, string> FirstPlayerSounds { get; set; }
			public Dictionary<ScoresRange, string> SecondPlayerSounds { get; set; }
			public double? RoundSeconds { get; set; }
			public double? PauseSeconds { get; set; }
			
			public int? RoundsCount { get; set; }
			
			public RandomTimerSettingsJson RandomTimer1 { get; set; }
			public RandomTimerSettingsJson RandomTimer2 { get; set; }
		}
		
		public class RandomTimerSettingsJson
		{
			public bool IsEnabled { get; set; }
			public int LowerLimit { get; set; }
			public int UpperLimit { get; set; }
			public string FilePath { get; set; }
		}
	}
}