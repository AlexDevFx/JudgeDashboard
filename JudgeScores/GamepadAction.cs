using System;
using System.Windows.Forms;

namespace JudgeScores
{
	public enum InputButtonSource
	{
		Any = -1,
		First = 1,
		Second = 2,
		Keyboard = 3,
	}

	public class GamepadAction
	{
		public Action<Keys> Keyboard { get; set; }
		public InputButtonSource[] Sources { get; set; }
	}
}
