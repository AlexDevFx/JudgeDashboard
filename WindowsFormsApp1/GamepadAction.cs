using System;
using Windows.Gaming.Input;

namespace JudgeScores
{
	public enum GamepadSource
	{
		Any = -1,
		First = 1,
		Second = 2,
	}

	public class GamepadAction
	{
		public Action<GamepadButtons> Action { get; set; }
		public GamepadSource Source { get; set; }
	}
}
