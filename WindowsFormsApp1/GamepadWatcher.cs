using System;
using Windows.Gaming.Input;

namespace JudgeScores
{
	public class GamepadWatcher
	{
		private Gamepad _controller;
		private Action<GamepadButtons> _onButtonClick;
		private GamepadReading _previousReading;
		private GamepadReading _currentReading;

		public GamepadWatcher(Gamepad controller)
		{
			_controller = controller ?? throw new ArgumentNullException(nameof(controller));
		}

		public void Init(Action<GamepadButtons> onButtonClick)
		{
			_onButtonClick = onButtonClick;
		}

		public GamepadButtons? GetClickedButton()
		{
			if (_controller == null)
			{
				return null;
			}

			_previousReading = _currentReading;
			_currentReading = _controller.GetCurrentReading();

			if(_previousReading.Buttons != _currentReading.Buttons)
				return _previousReading.Buttons;

			return null;
		}

		private bool IsButtonJustPressed(GamepadButtons selection)
		{

			bool newSelectionPressed = (selection == (_currentReading.Buttons & selection));
			bool oldSelectionPressed = (selection == (_previousReading.Buttons & selection));

			return newSelectionPressed && !oldSelectionPressed;
		}

		private bool IsButtonJustReleased(GamepadButtons selection)
		{
			bool newSelectionReleased =
				(GamepadButtons.None == (_currentReading.Buttons & selection));

			bool oldSelectionReleased =
				(GamepadButtons.None == (_previousReading.Buttons & selection));

			return newSelectionReleased && !oldSelectionReleased;
		}
	}
}
