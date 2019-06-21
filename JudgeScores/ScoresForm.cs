using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using Windows.Gaming.Input;

namespace JudgeScores
{
	public partial class ScoresForm: Form
	{
		private enum MainActionsType
		{
			StartTimer = 1,
			StopTimer = 2,
			ResetTimer = 3,
		}

		private Timer _firstTimer = new Timer();
		private Timer _secondTimer = new Timer();
		private Timer _countdownTimer = new Timer();

		private GamepadWatcher _firstGamepad;
		private GamepadWatcher _secondGamepad;

		private Player _firstPlayer = new Player();
		private Player _secondPlayer = new Player();

		private TimeSpan _countdownTime = TimeSpan.FromSeconds(30);
		private Dictionary<GamepadButtons, Action> _funcButtons = new Dictionary<GamepadButtons, Action>();
		private Dictionary<MainActionsType, string> _funcButtonsSounds = new Dictionary<MainActionsType, string>();

		private ConcurrentStack<GamepadAction> _additionalActions = new ConcurrentStack<GamepadAction>();
		private bool _isRoundCompleted => !_countdownTimer.Enabled;

		private static readonly Dictionary<GamepadButtons, string> __buttonsNames = new Dictionary<GamepadButtons, string>
		{
			{ GamepadButtons.A, "A" },
			{ GamepadButtons.B, "B" },
			{ GamepadButtons.DPadDown, "Стрелка вниз" },
			{ GamepadButtons.DPadLeft, "Стрелка влево" },
			{ GamepadButtons.DPadRight, "Стрелка вправо" },
			{ GamepadButtons.DPadUp, "Стрелка вверх" },
			{ GamepadButtons.LeftShoulder, "Левый шифт 1" },
			{ GamepadButtons.LeftThumbstick, "Левый шифт 2" },
			{ GamepadButtons.RightShoulder, "Правый шифт 1" },
			{ GamepadButtons.RightThumbstick, "Правый шифт 2" },
			{ GamepadButtons.Menu, "Start" },
			/*{ GamepadButtons.Paddle1, "" },
			{ GamepadButtons.Paddle2, "" },
			{ GamepadButtons.Paddle3, "" },
			{ GamepadButtons.Paddle4, "" },*/
			{ GamepadButtons.View, "Select" },
			{ GamepadButtons.X, "X" },
			{ GamepadButtons.Y, "Y" },
		};

		public ScoresForm()
		{
			InitializeComponent();
			Gamepad.GamepadAdded += GamepadAdded;
			Gamepad.GamepadRemoved += GamepadRemoved;

			_firstTimer.Tick += FirstTimer_Tick;
			_firstTimer.Interval = 100;
			_firstTimer.Start();

			_secondTimer.Tick += SecondTimer_Tick;
			_secondTimer.Interval = 100;
			_secondTimer.Start();

			_countdownTimer.Interval = 1000;
			_countdownTimer.Tick += _countdownTimer_Tick;

			countdownTimer.Text = _countdownTime.ToString(@"mm\:ss");

			firstPlayerScores.Text = "0";
			secondPlayerScores.Text = "0";
			notifyText.Text = "";

			_firstPlayer.Init(() =>
			{
				firstPlayerScores.Text = _firstPlayer.Scores.ToString("D");
			}, 
			(file) => 
			{
				PlaySound(file);
			});

			_secondPlayer.Init(() =>
			{
				secondPlayerScores.Text = _secondPlayer.Scores.ToString("D");
			},
			(file) =>
			{
				PlaySound(file);
			});
		}

		private void PlaySound(string fileName)
		{
			WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
			wplayer.URL = fileName;
			wplayer.controls.play();
		}

		private void _countdownTimer_Tick(object sender, EventArgs e)
		{
			_countdownTime = _countdownTime.Subtract(TimeSpan.FromSeconds(1));

			countdownTimer.Text = _countdownTime.ToString(@"mm\:ss");

			if(_countdownTime.TotalSeconds <= 0)
			{
				_countdownTimer.Stop();
			}
		}

		private void FirstTimer_Tick(object sender, EventArgs e)
		{
			GamepadButtons? clickedButton = _firstGamepad?.GetClickedButton();

			if(clickedButton.HasValue && clickedButton.Value != GamepadButtons.None)
			{
				if(_additionalActions.TryPeek(out var actionData))
				{
					if (actionData.Source == GamepadSource.First || actionData.Source == GamepadSource.Any)
					{
						actionData.Action(clickedButton.Value);
						_additionalActions.TryPop(out actionData);
					}
				}
				else
				{
					Log($"1ый геймпад кнопка <{GetButtonName(clickedButton.Value)}> нажата");
					ProcessButton(clickedButton.Value, _firstPlayer);
				}
			}
		}

		private void SecondTimer_Tick(object sender, EventArgs e)
		{
			GamepadButtons? clickedButton = _secondGamepad?.GetClickedButton();

			if (clickedButton.HasValue && clickedButton.Value != GamepadButtons.None)
			{
				if (_additionalActions.TryPeek(out var actionData))
				{
					if(actionData.Source == GamepadSource.Second || actionData.Source == GamepadSource.Any)
					{ 
						actionData.Action(clickedButton.Value);
						_additionalActions.TryPop(out actionData);
					}
				}
				else
				{
					Log($"2ой геймпад кнопка <{GetButtonName(clickedButton.Value)}> нажата");
					ProcessButton(clickedButton.Value, _secondPlayer);
				}
			}
		}

		private void ProcessButton(GamepadButtons clickedButton, Player player)
		{
			if(_funcButtons.ContainsKey(clickedButton))
			{
				_funcButtons[clickedButton].Invoke();
			}
			else
			{
				if(!_isRoundCompleted)
					player.SetHit(clickedButton);
			}
		}

		private void GamepadRemoved(object sender, Gamepad e)
		{
			Log("Контроллер геймпада удален");
		}

		private void GamepadAdded(object sender, Gamepad e)
		{
			Log("Контроллер геймпада добавлен");

			if (Gamepad.Gamepads.Count > 0)
			{
				_firstGamepad = new GamepadWatcher(Gamepad.Gamepads[0]);
			}

			if (Gamepad.Gamepads.Count > 1)
			{
				_secondGamepad = new GamepadWatcher(Gamepad.Gamepads[1]);
			}
		}

		private void Log(string text)
		{
			string logText = $"{DateTime.Now.ToString(@"HH\:mm\:ss")} {text}";
			Debug.WriteLine(logText);

			if (loggerTextbox.Text.Length + logText.Length >= loggerTextbox.MaxLength)
			{
				loggerTextbox.Text = $"{logText}";
			}
			else
			{
				loggerTextbox.Text += $"\r\n{logText}";
			}
		}

		private void SettingsInfo(string text)
		{
			notifyText.Text = text;
		}

		private void assignStartButton_Click(object sender, EventArgs e)
		{
			AddButtonAssignment((b) =>
				{
					AssignButton(button: b,
						message: $"Кнопка <{GetButtonName(b)}> назначена на старт таймера",
						assignAction: btn =>
						{
							AssignFunctionalButton(b, () =>
							{
								_countdownTimer.Start();
								PlaySoundForAction(MainActionsType.StartTimer);
							});
							startTimerButton.Text = GetButtonName(b);
						});
				}, GamepadSource.Any);
		}

		private void PlaySoundForAction(MainActionsType actionType)
		{
			if (_funcButtonsSounds.TryGetValue(actionType, out string fileName))
			{
				if (!string.IsNullOrEmpty(fileName) && !string.IsNullOrWhiteSpace(fileName))
				{
					PlaySound(fileName);
				}
			}
		}

		private void assignStop_Click(object sender, EventArgs e)
		{
				AddButtonAssignment((b) =>
				{
					AssignButton(button: b,
						message: $"Кнопка <{GetButtonName(b)}> назначена на паузу таймера",
						assignAction: btn =>
						{
							AssignFunctionalButton(b, () =>
							{
								_countdownTimer.Stop();
								PlaySoundForAction(MainActionsType.StopTimer);
							});
							stopTimerButton.Text = GetButtonName(b);
						});
				}, GamepadSource.Any);
		}

		private static string GetButtonName(GamepadButtons arg)
		{
			string buttonName = $"{arg}";
			if (__buttonsNames.ContainsKey(arg))
			{
				buttonName = __buttonsNames[arg];
			}

			return buttonName;
		}

		private void AssignFunctionalButton(GamepadButtons arg, Action action)
		{
			if(_funcButtons.ContainsKey(arg))
			{
				_funcButtons[arg] = action;
			}
			else
			{
				_funcButtons.Add(arg, action);
			}
		}

		private void SetTimer_Click(object sender, EventArgs e)
		{
			int minutes = (int)Math.Min(59, minutesPartTimer.Value), seconds = (int)Math.Min(59, secondsPartTimer.Value);

			_countdownTime = TimeSpan.FromMinutes(minutes).Add(TimeSpan.FromSeconds(seconds));

			countdownTimer.Text = _countdownTime.ToString(@"mm\:ss");
		}

		private void button3_Click(object sender, EventArgs e)
		{
			AddButtonAssignment((arg) =>
			{
				AssignButton(button: arg,
					message: $"Кнопка <{GetButtonName(arg)}> назначена на сброс таймера",
					assignAction: btn => 
						AssignFunctionalButton(arg, () =>
						{
							int minutes = (int)Math.Min(59, minutesPartTimer.Value), seconds = (int)Math.Min(59, secondsPartTimer.Value);

							_countdownTime = TimeSpan.FromMinutes(minutes).Add(TimeSpan.FromSeconds(seconds));

							countdownTimer.Text = _countdownTime.ToString(@"mm\:ss");
							resetTimerButton.Text = GetButtonName(arg);

							_firstPlayer.ResetScores();
							_secondPlayer.ResetScores();
							PlaySoundForAction(MainActionsType.ResetTimer);
						}));
			}, GamepadSource.Any);
		}

		private void AddButtonAssignmentPlayer(GamepadSource source, Player player, ScoresRange scoresRange, Label buttonLabel, Func<GamepadButtons, string> messageAction)
		{
			AddButtonAssignment((arg) =>
			{
				AssignButton(button: arg,
					message: messageAction(arg),
					assignAction: btn =>
					{
						player.AddScoresKeyBinding(scoresRange, arg);
						buttonLabel.Text = GetButtonName(arg);
					});
			}, source);
		}

		private void firstPlayerOneValue_Click(object sender, EventArgs e)
		{
			AddButtonAssignmentPlayer(GamepadSource.First, _firstPlayer, ScoresRange.One, button1Name1st, (arg) => $"Кнопка <{GetButtonName(arg)}> назначена +1 балл для первого участника");
		}

		private void firstPlayerTwoValue_Click(object sender, EventArgs e)
		{
			AddButtonAssignmentPlayer(GamepadSource.First, _firstPlayer, ScoresRange.Two, button2Name1st, (arg) => $"Кнопка <{GetButtonName(arg)}> назначена +2 балла для первого участника");
		}

		private void firstPlayerThreeValue_Click(object sender, EventArgs e)
		{
			AddButtonAssignmentPlayer(GamepadSource.First, _firstPlayer, ScoresRange.Three, button3Name1st, (arg) => $"Кнопка <{GetButtonName(arg)}> назначена +3 балла для первого участника");
		}

		private void AddButtonAssignment(Action<GamepadButtons> action, GamepadSource source)
		{
			if (_additionalActions.Count(e => e.Source != source) > 0)
				return;

			if (_additionalActions.Count(e => e.Source == source) > 0)
			{
				_additionalActions.TryPop(out var act);
				return;
			}

			_additionalActions.Push(new GamepadAction { Action = action, Source = source });
		}

		private void AssignButton(GamepadButtons button, string message, Action<GamepadButtons> assignAction)
		{
			if(_funcButtons.ContainsKey(button))
			{
				DialogResult response = MessageBox.Show($"Кнопка <{GetButtonName(button)}> уже используется. Переназначить?", "Переназначение кнопок", MessageBoxButtons.YesNo);

				if(response != DialogResult.Yes)
				{
					return;
				}

				_funcButtons.Remove(button);
			}

			assignAction(button);

			SettingsInfo(message);
			Log(message);
		}

		private void secondPlayerOneValue_Click(object sender, EventArgs e)
		{
			AddButtonAssignmentPlayer(GamepadSource.Second, _secondPlayer, ScoresRange.One, button1Name2nd, (arg) => $"Кнопка <{GetButtonName(arg)}> назначена +1 балл для второго участника");
		}

		private void secondPlayerTwoValue_Click(object sender, EventArgs e)
		{
			AddButtonAssignmentPlayer(GamepadSource.Second, _secondPlayer, ScoresRange.Two, button2Name2nd, (arg) => $"Кнопка <{GetButtonName(arg)}> назначена +2 балла для второго участника");
		}

		private void secondPlayerThreeValue_Click(object sender, EventArgs e)
		{
			AddButtonAssignmentPlayer(GamepadSource.Second, _secondPlayer, ScoresRange.Three, button3Name2nd, (arg) => $"Кнопка <{GetButtonName(arg)}> назначена +3 балла для второго участника");
		}

		private void AddSoundForPlayer(Player player, ScoresRange scoresRange)
		{
			if(openFileDialog.ShowDialog() == DialogResult.OK)
			{
				string fileName = openFileDialog.FileName;
				player.AddSoundBinding(scoresRange, fileName);
			}
		}

		private void AddMainSound(MainActionsType button)
		{
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				string fileName = openFileDialog.FileName;

				if(_funcButtonsSounds.ContainsKey(button))
				{
					_funcButtonsSounds[button] = fileName;
				}
				else
				{
					_funcButtonsSounds.Add(button, fileName);
				}
			}
		}

		private void set1Sound1st_Click(object sender, EventArgs e)
		{
			AddSoundForPlayer(_firstPlayer, ScoresRange.One);
		}

		private void set2Sound1st_Click(object sender, EventArgs e)
		{
			AddSoundForPlayer(_firstPlayer, ScoresRange.Two);
		}

		private void set3Sound1st_Click(object sender, EventArgs e)
		{
			AddSoundForPlayer(_firstPlayer, ScoresRange.Three);
		}

		private void set1Sound2nd_Click(object sender, EventArgs e)
		{
			AddSoundForPlayer(_secondPlayer, ScoresRange.One);
		}

		private void set2Sound2nd_Click(object sender, EventArgs e)
		{
			AddSoundForPlayer(_secondPlayer, ScoresRange.Two);
		}

		private void set3Sound2nd_Click(object sender, EventArgs e)
		{
			AddSoundForPlayer(_secondPlayer, ScoresRange.Three);
		}

		private void setSoundStart_Click(object sender, EventArgs e)
		{
			AddMainSound(MainActionsType.StartTimer);
		}

		private void setSoundStop_Click(object sender, EventArgs e)
		{
			AddMainSound(MainActionsType.StopTimer);
		}

		private void setSoundReset_Click(object sender, EventArgs e)
		{
			AddMainSound(MainActionsType.ResetTimer);
		}

		private void ScoresForm_SizeChanged(object sender, EventArgs e)
		{
			ResizeLabels();
		}

		private void ResizeLabels()
		{
			float fontScale = 1.0f;

			if (WindowState == FormWindowState.Maximized)
			{
				fontScale = 0.8f;
			}

			JudgesDashboard.Height = Height - 38;
			JudgesDashboard.Width = Width;

			firstPlayerScores.Width = (JudgesDashboard.Width - 3) / 2;
			secondPlayerScores.Width = (JudgesDashboard.Width - 3) / 2;
			countdownTimer.Width = (int)(firstPlayerScores.Width / 3.4);

			firstPlayerScores.Height = JudgesDashboard.Height - 32;
			secondPlayerScores.Height = JudgesDashboard.Height - 32;
			countdownTimer.Height = (int)(firstPlayerScores.Height / 6.5);

			secondPlayerScores.Location = new System.Drawing.Point(firstPlayerScores.Width + firstPlayerScores.Location.X, secondPlayerScores.Location.Y);

			float fontSize = Math.Max(firstPlayerScores.Width * firstPlayerScores.Height / ((514 * 362) / 240F)*fontScale, 14F);

			firstPlayerScores.Font = new System.Drawing.Font("Microsoft Sans Serif", fontSize, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			secondPlayerScores.Font = new System.Drawing.Font("Microsoft Sans Serif", fontSize, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			countdownTimer.Font = new System.Drawing.Font("Microsoft Sans Serif", fontSize * 0.15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));

			countdownTimer.Location = new System.Drawing.Point(firstPlayerScores.Width + firstPlayerScores.Location.X - countdownTimer.Width / 2, countdownTimer.Location.Y);
		}
	}
}
