using System;
using System.Windows.Forms;

namespace JudgeScores
{
	public class RandomTimerSettings
	{
		public RandomTimerSettings()
		{
			UpperLimit = 59;
			LowerLimit = 1;
		}

		public bool IsEnabled{ get; set; }

		public int LowerLimit
		{
			get { return _lowerLimit; }
			set
			{
				if (value > 0 && value < 60)
					_lowerLimit = value;
			}
		}

		public int UpperLimit
		{
			get { return _upperLimit; }
			set
			{
				if (value > 0 && value < 60)
					_upperLimit = value;
			}
		}
		public string FilePath { get; set; }

		public NumericUpDown LowerLimitControl { get; set; }
		public NumericUpDown UpperLimitControl { get; set; }
		public CheckBox IsEnabledControl { get; set; }

		private int _lowerLimit = 1;
		private int _upperLimit = 59;

		public void UpdateControls()
		{
			if (UpperLimitControl != null && UpperLimit > 0 && UpperLimit < 60)
				UpperLimitControl.Value = UpperLimit;
			if (LowerLimitControl != null && LowerLimit > 0 && LowerLimit < 60)
				LowerLimitControl.Value = LowerLimit;
			if (IsEnabledControl != null)
				IsEnabledControl.Checked = IsEnabled;
		}
		
		public void SetControls(NumericUpDown lowerLimitControl, NumericUpDown upperLimitControl, CheckBox isEnabledControl)
		{
			LowerLimitControl = lowerLimitControl;
			UpperLimitControl = upperLimitControl;
			IsEnabledControl = isEnabledControl;
		}
		
		public void SetAndUpdateControls(NumericUpDown lowerLimitControl, NumericUpDown upperLimitControl, CheckBox isEnabledControl)
		{
			SetControls(lowerLimitControl, upperLimitControl, isEnabledControl);
			UpdateControls();
		}
	}
}