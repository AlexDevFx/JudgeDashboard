using System;
using System.Collections.Generic;

namespace JudgeScores
{
	public enum ScoresRange
	{
		First = 1,
		Second = 2,
		Third = 3
	}

	public class Player
	{
		public ushort Scores { get; private set; }
		public Dictionary<ScoresRange, string> SoundBindings { get; private set; } = new Dictionary<ScoresRange, string>();

		public Dictionary<ScoresRange, ushort> HitBindings { get; private set; } = new Dictionary<ScoresRange, ushort>
		{
			{ ScoresRange.First, 1 },
			{ ScoresRange.Second, 2 },
			{ ScoresRange.Third, 3 },
		};
		
		private Action _setHitAction;
		private Action<string> _soundAction;

		public void Init(Action setHitAction, Action<string> soundAction)
		{
			_setHitAction = setHitAction;
			_soundAction = soundAction;
		}
		
		public void SetHit(ScoresRange range)
		{
			if ( !HitBindings.ContainsKey(range) )
				return;
			
			ushort hitsAmount = HitBindings[range];
			if (Scores + hitsAmount < short.MaxValue)
				Scores += hitsAmount;

			_setHitAction?.Invoke();

			if(SoundBindings.ContainsKey(range))
			{
				string soundFile = SoundBindings[range];

				if(!string.IsNullOrEmpty(soundFile) && !string.IsNullOrWhiteSpace(soundFile))
				{
					_soundAction(soundFile);
				}
			}
		}
		
		public void AddScoresHitsAmount(ScoresRange scoresRange, ushort hitsAmount)
		{
			if(HitBindings.ContainsKey(scoresRange))
			{
				HitBindings[scoresRange] = hitsAmount;
			}
			else
			{
				HitBindings.Add(scoresRange, hitsAmount);
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
