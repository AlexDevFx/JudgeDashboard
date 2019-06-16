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
		public Dictionary<GamepadButtons, ScoresRange> _keyBindings = new Dictionary<GamepadButtons, ScoresRange>();
		public Dictionary<ScoresRange, string> _soundBindings = new Dictionary<ScoresRange, string>();
		private Action _setHitAction;
		private Action<string> _soundAction;

		public void Init(Action setHitAction, Action<string> soundAction)
		{
			_setHitAction = setHitAction;
			_soundAction = soundAction;
		}

		public void SetHit(GamepadButtons button)
		{
			if (!_keyBindings.ContainsKey(button))
				return;

			ushort scores = (ushort)_keyBindings[button];
			if (Scores + scores < short.MaxValue)
				Scores += scores;

			_setHitAction?.Invoke();

			if(_soundBindings.ContainsKey(_keyBindings[button]))
			{
				string soundFile = _soundBindings[_keyBindings[button]];

				if(!string.IsNullOrEmpty(soundFile) && !string.IsNullOrWhiteSpace(soundFile))
				{
					_soundAction(soundFile);
				}
			}
		}

		public void AddScoresKeyBinding(ScoresRange scoresRange, GamepadButtons button)
		{
			if(_keyBindings.ContainsKey(button))
			{
				_keyBindings[button] = scoresRange;
			}
			else
			{
				_keyBindings.Add(button, scoresRange);
			}
		}

		public void ResetScores()
		{
			Scores = 0;
			_setHitAction?.Invoke();
		}

		public void AddSoundBinding(ScoresRange scoresRange, string fileName)
		{
			if (_soundBindings.ContainsKey(scoresRange))
			{
				_soundBindings[scoresRange] = fileName;
			}
			else
			{
				_soundBindings.Add(scoresRange, fileName);
			}
		}
	}
}
