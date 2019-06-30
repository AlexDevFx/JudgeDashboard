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
			
			public RandomTimers RandomTimer1 { get; set; }
			public RandomTimers RandomTimer2 { get; set; }
		}
	}
}