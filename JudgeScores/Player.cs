using System;
using System.Collections.Generic;
using Windows.Gaming.Input;

namespace JudgeScores
{
	public enum ScoresRange
	{
		One = 1,
		Two = 2,
		Three = 3
	}

	public class Player
	{
		public ushort Scores { get; private set; }
		public Dictionary<GamepadButtons, ScoresRange> KeyBindings { get; private set; } = new Dictionary<GamepadButtons, ScoresRange>();
		public Dictionary<ScoresRange, string> SoundBindings { get; private set; } = new Dictionary<ScoresRange, string>();
		private Action _setHitAction;
		private Action<string> _soundAction;

		public void Init(Action setHitAction, Action<string> soundAction)
		{
			_setHitAction = setHitAction;
			_soundAction = soundAction;
		}

		public void SetHit(GamepadButtons button)
		{
			if (!KeyBindings.ContainsKey(button))
				return;

			ushort scores = (ushort)KeyBindings[button];
			if (Scores + scores < short.MaxValue)
				Scores += scores;

			_setHitAction?.Invoke();

			if(SoundBindings.ContainsKey(KeyBindings[button]))
			{
				string soundFile = SoundBindings[KeyBindings[button]];

				if(!string.IsNullOrEmpty(soundFile) && !string.IsNullOrWhiteSpace(soundFile))
				{
					_soundAction(soundFile);
				}
			}
		}

		public void AddScoresKeyBinding(ScoresRange scoresRange, GamepadButtons button)
		{
			if(KeyBindings.ContainsKey(button))
			{
				KeyBindings[button] = scoresRange;
			}
			else
			{
				KeyBindings.Add(button, scoresRange);
			}
		}

		public void ResetScores()
		{
			Scores = 0;
			_setHitAction?.Invoke();
		}

		public void AddSoundBinding(ScoresRange scoresRange, string fileName)
		{
			if (SoundBindings.ContainsKey(scoresRange))
			{
				SoundBindings[scoresRange] = fileName;
			}
			else
			{
				SoundBindings.Add(scoresRange, fileName);
			}
		}
	}
}
