using System.Windows.Forms;

namespace JudgeScores
{
	public class RandomTimerSettings
	{
		public bool IsEnabled{ get; set; }

		public int LowerLimit{ get; set; }
		public int UpperLimit { get; set; }
		public string FilePath { get; set; }

		public NumericUpDown LowerLimitControl { get; set; }
		public NumericUpDown UpperLimitControl { get; set; }
		public CheckBox IsEnabledControl { get; set; }

		public void UpdateControls()
		{
			if (UpperLimitControl != null)
				UpperLimitControl.Value = UpperLimit;
			if (LowerLimitControl != null)
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