using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Windows.Gaming.Input;
using AutoMapper;

namespace JudgeScores
{
	public partial class ScoresForm: Form
	{
		private class ScoresBinds
		{
			public Label Label { get; set; }
			public Func<GamepadButtons, string> MessageFunc { get; set; }
		}

		private const string SettingsFilePath = "settings.json";
		private readonly Timer _firstTimer = new Timer();
		private readonly Timer _secondTimer = new Timer();
		private readonly Timer _mainRoundTimer = new Timer();
		private readonly Timer _pauseRoundTimer = new Timer();
		private readonly Timer _extraTimer1 = new Timer();
		private readonly Timer _extraTimer2 = new Timer();

		private GamepadWatcher _firstGamepad;
		private GamepadWatcher _secondGamepad;

		private readonly Player _firstPlayer = new Player();
		private readonly Player _secondPlayer = new Player();

		private TimeSpan _countdownTime = TimeSpan.FromSeconds(30);
		private TimeSpan _pauseCountdownTime = TimeSpan.FromSeconds(30);
		private Dictionary<GamepadButtons, MainActionsType> _funcButtonsBinds = new Dictionary<GamepadButtons, MainActionsType>();
		private readonly Dictionary<MainActionsType, Action> _funcActions;

		private Dictionary<MainActionsType, string> _funcButtonsSounds = new Dictionary<MainActionsType, string>();

		private ConcurrentStack<GamepadAction> _additionalActions = new ConcurrentStack<GamepadAction>();
		private bool IsRoundCompleted => !_mainRoundTimer.Enabled;

		private DashboardSettings _dashboardSettings = new DashboardSettings
		{
			RoundsCount = 1,
			RoundsCompleted = 0,
		};
		
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

		private IMapper _objectMapper;

		public ScoresForm()
		{
			InitializeComponent();
			
			InitAutoMapper();
			
			
			Gamepad.GamepadAdded += GamepadAdded;
			Gamepad.GamepadRemoved += GamepadRemoved;

			_firstTimer.Tick += FirstTimer_Tick;
			_firstTimer.Interval = 100;
			_firstTimer.Start();

			_secondTimer.Tick += SecondTimer_Tick;
			_secondTimer.Interval = 100;
			_secondTimer.Start();

			_mainRoundTimer.Interval = 1000;
			_mainRoundTimer.Tick += MainRoundTimerTick;
			
			_pauseRoundTimer.Interval = 1000;
			_pauseRoundTimer.Tick += PauseRoundTimerTick;
			
			_extraTimer1.Tick += ExtraTimer1_Tick;
			_extraTimer2.Tick += ExtraTimer2_Tick;

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

			ResizeLabels();

			_funcActions = new Dictionary<MainActionsType, Action>()
			{
				{
					MainActionsType.StartTimer,
					new Action(() => 
					{
						if ( IsRoundCompleted )
						{
							StartRound();
							PlaySoundForAction(MainActionsType.StartTimer);
						}
						else
						{
							StopRound();
							PlaySoundForAction(MainActionsType.StopTimer);
						}
					})
				},
				{
					MainActionsType.StopTimer,
					new Action(() =>
					{
							if ( IsRoundCompleted )
						{
							StartRound();
							PlaySoundForAction(MainActionsType.StartTimer);
						}
						else
						{
							StopRound();
							PlaySoundForAction(MainActionsType.StopTimer);
						}
					})
				},
				{
					MainActionsType.ResetTimer,
					new Action(() =>
					{
						int minutes = (int)Math.Min(59, minutesPartTimer.Value), seconds = (int)Math.Min(59, secondsPartTimer.Value);

						_countdownTime = TimeSpan.FromMinutes(minutes).Add(TimeSpan.FromSeconds(seconds));

						countdownTimer.Text = _countdownTime.ToString(@"mm\:ss");

						_firstPlayer.ResetScores();
						_secondPlayer.ResetScores();
						PlaySoundForAction(MainActionsType.ResetTimer);
					})
				},
			};
			LoadSettings(SettingsFilePath);
			
		}

		private void PauseRoundTimerTick(object sender, EventArgs e)
		{
			_pauseCountdownTime = _pauseCountdownTime.Subtract(TimeSpan.FromSeconds(1));
            
            countdownTimer.Text = _pauseCountdownTime.ToString(@"mm\:ss");
            
            if (Math.Abs(_pauseCountdownTime.TotalSeconds - 10) < 1)
            {
            	PlaySoundForAction(MainActionsType.PauseRoundRemain10Seconds);
            }

            if (_pauseCountdownTime.TotalSeconds <= 0)
            {
            	_pauseRoundTimer.Stop();
                if ( _dashboardSettings.RoundsCount - _dashboardSettings.RoundsCompleted > 0 )
                {
                    _countdownTime = TimeSpan.FromSeconds(_dashboardSettings.RoundSeconds);
                    PlaySoundForAction(MainActionsType.StartTimer);
                    countdownTimer.ForeColor = Color.White;
                    countdownTimer.Text = _countdownTime.ToString(@"mm\:ss");
                    StartRound();
                }
            }
		}

		private void InitAutoMapper()
		{
			var config = new MapperConfiguration(cfg => {
				cfg.CreateMap<RandomTimerSettingsJson, RandomTimerSettings>();
				cfg.CreateMap<RandomTimerSettings, RandomTimerSettingsJson>();
			});

			_objectMapper = config.CreateMapper();
		}

		private void PlaySound(string fileName)
		{
			WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
			wplayer.URL = fileName;
			wplayer.controls.play();
		}

		private void MainRoundTimerTick(object sender, EventArgs e)
		{
			_countdownTime = _countdownTime.Subtract(TimeSpan.FromSeconds(1));

			countdownTimer.Text = _countdownTime.ToString(@"mm\:ss");

			if (Math.Abs(_countdownTime.TotalSeconds - 10) < 1)
			{
				PlaySoundForAction(MainActionsType.RoundRemain10Seconds);
			}

			if (_countdownTime.TotalSeconds <= 0)
			{
				StopRound();
				PlaySoundForAction(MainActionsType.RoundComplete);
				_dashboardSettings.RoundsCompleted++;
			
				if ( _dashboardSettings.RoundsCount - _dashboardSettings.RoundsCompleted > 0 )
				{
					_pauseCountdownTime = TimeSpan.FromSeconds(_dashboardSettings.PauseSeconds);
					countdownTimer.ForeColor = Color.Yellow;
					countdownTimer.Text = _pauseCountdownTime.ToString(@"mm\:ss");
					_pauseRoundTimer.Start();
				}
			}
		}
		
		private void ExtraTimer1_Tick(object sender, EventArgs e)
		{
			PlaySoundForAction(MainActionsType.RandomTimer1);
			if (!IsRoundCompleted && _dashboardSettings.RandomTimer1.IsEnabled)
			{
				RestartExtraTimer(_extraTimer1, _dashboardSettings.RandomTimer1.LowerLimit, _dashboardSettings.RandomTimer1.UpperLimit);
			}
		}

		private void ExtraTimer2_Tick(object sender, EventArgs e)
		{
			if(!string.IsNullOrEmpty(_dashboardSettings.RandomTimer2.FilePath))
			{
				string[] files = Directory.GetFiles(_dashboardSettings.RandomTimer2.FilePath, "*.mp3");

				if(files?.Any() == true)
				{
					int fileNumber = new Random(DateTime.Now.Second).Next(0, files.Length-1);

					if(fileNumber >= 0)
					{
						PlaySound(files[fileNumber]);
					}
				}
			}
			if (!IsRoundCompleted && _dashboardSettings.RandomTimer2.IsEnabled)
			{
				RestartExtraTimer(_extraTimer2, _dashboardSettings.RandomTimer2.LowerLimit, _dashboardSettings.RandomTimer2.UpperLimit);
			}
		}

		private void StartRound()
		{
			_mainRoundTimer.Start();

			if (_dashboardSettings?.RandomTimer1?.IsEnabled == true)
			{
				RestartExtraTimer(_extraTimer1, _dashboardSettings.RandomTimer1.LowerLimit, _dashboardSettings.RandomTimer1.UpperLimit);
			}
			if (_dashboardSettings?.RandomTimer2?.IsEnabled == true)
			{
				RestartExtraTimer(_extraTimer2, _dashboardSettings.RandomTimer2.LowerLimit, _dashboardSettings.RandomTimer2.UpperLimit);
			}
		}

		private void StopRound()
		{
			_mainRoundTimer.Stop();
			_extraTimer1.Stop();
			_extraTimer2.Stop();
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
					ProcessButton(clickedButton.Value, _secondPlayer);
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
			if(_funcButtonsBinds.ContainsKey(clickedButton))
			{
				if(_funcActions.TryGetValue(_funcButtonsBinds[clickedButton], out Action action) )
				{
					action.Invoke();
				}
			}
			else
			{
				if(!IsRoundCompleted)
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
				Log("1ый геймпад добавлен");
			}

			if (Gamepad.Gamepads.Count > 1)
			{
				_secondGamepad = new GamepadWatcher(Gamepad.Gamepads[1]);
				Log("2ой геймпад добавлен");
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
							AssignFunctionalButton(b, MainActionsType.StartTimer);
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
							AssignFunctionalButton(b, MainActionsType.StopTimer);
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

		private void AssignFunctionalButton(GamepadButtons arg, MainActionsType actionType)
		{
			if(_funcButtonsBinds.ContainsKey(arg))
			{
				_funcButtonsBinds[arg] = actionType;
			}
			else
			{
				_funcButtonsBinds.Add(arg, actionType);
			}
		}

		private void SetTimer_Click(object sender, EventArgs e)
		{
			int minutes = (int)Math.Min(59, minutesPartTimer.Value), seconds = (int)Math.Min(59, secondsPartTimer.Value);
			
			_countdownTime = TimeSpan.FromMinutes(minutes).Add(TimeSpan.FromSeconds(seconds));
			_dashboardSettings.RoundSeconds = _countdownTime.TotalSeconds;
				
			countdownTimer.Text = _countdownTime.ToString(@"mm\:ss");
		}

		private void button3_Click(object sender, EventArgs e)
		{
			AddButtonAssignment((arg) =>
			{
				AssignButton(button: arg,
					message: $"Кнопка <{GetButtonName(arg)}> назначена на сброс таймера",
					assignAction: btn =>
					{
						AssignFunctionalButton(arg, MainActionsType.ResetTimer);
						resetTimerButton.Text = GetButtonName(arg);
					}
				);
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
			AddButtonAssignmentPlayer(GamepadSource.First, _firstPlayer, ScoresRange.First, button1Name1st, (arg) => $"Кнопка <{GetButtonName(arg)}> назначена +1 балл для первого участника");
		}

		private void firstPlayerTwoValue_Click(object sender, EventArgs e)
		{
			AddButtonAssignmentPlayer(GamepadSource.First, _firstPlayer, ScoresRange.Second, button2Name1st, (arg) => $"Кнопка <{GetButtonName(arg)}> назначена +2 балла для первого участника");
		}

		private void firstPlayerThreeValue_Click(object sender, EventArgs e)
		{
			AddButtonAssignmentPlayer(GamepadSource.First, _firstPlayer, ScoresRange.Third, button3Name1st, (arg) => $"Кнопка <{GetButtonName(arg)}> назначена +3 балла для первого участника");
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

		private void AssignButton(GamepadButtons button, string message, Action<GamepadButtons> assignAction, bool isShowDialog = true)
		{
			if(_funcButtonsBinds.ContainsKey(button) && isShowDialog)
			{
				DialogResult response = MessageBox.Show($"Кнопка <{GetButtonName(button)}> уже используется. Переназначить?", "Переназначение кнопок", MessageBoxButtons.YesNo);

				if(response != DialogResult.Yes)
				{
					return;
				}

				_funcButtonsBinds.Remove(button);
			}

			assignAction(button);

			SettingsInfo(message);
			Log(message);
		}

		private void secondPlayerOneValue_Click(object sender, EventArgs e)
		{
			AddButtonAssignmentPlayer(GamepadSource.First, _secondPlayer, ScoresRange.First, button1Name2nd, (arg) => $"Кнопка <{GetButtonName(arg)}> назначена +1 балл для второго участника");
		}

		private void secondPlayerTwoValue_Click(object sender, EventArgs e)
		{
			AddButtonAssignmentPlayer(GamepadSource.First, _secondPlayer, ScoresRange.Second, button2Name2nd, (arg) => $"Кнопка <{GetButtonName(arg)}> назначена +2 балла для второго участника");
		}

		private void secondPlayerThreeValue_Click(object sender, EventArgs e)
		{
			AddButtonAssignmentPlayer(GamepadSource.First, _secondPlayer, ScoresRange.Third, button3Name2nd, (arg) => $"Кнопка <{GetButtonName(arg)}> назначена +3 балла для второго участника");
		}

		private void AddSoundForPlayer(Player player, ScoresRange scoresRange)
		{
			openFileDialog.Filter = "Mp3|*.mp3|Wav|*.wav";
			openFileDialog.CheckFileExists = true;
			if(openFileDialog.ShowDialog() == DialogResult.OK)
			{
				string fileName = openFileDialog.FileName;
				player.AddSoundBinding(scoresRange, fileName);
			}
		}

		private string AddMainSound(MainActionsType button)
		{
			openFileDialog.Filter = "Mp3|*.mp3|Wav|*.wav";
			openFileDialog.CheckFileExists = true;
			string fileName = string.Empty;
			
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				fileName = openFileDialog.FileName;

				if(_funcButtonsSounds.ContainsKey(button))
				{
					_funcButtonsSounds[button] = fileName;
				}
				else
				{
					_funcButtonsSounds.Add(button, fileName);
				}
			}

			return fileName;
		}

		private void set1Sound1st_Click(object sender, EventArgs e)
		{
			AddSoundForPlayer(_firstPlayer, ScoresRange.First);
		}

		private void set2Sound1st_Click(object sender, EventArgs e)
		{
			AddSoundForPlayer(_firstPlayer, ScoresRange.Second);
		}

		private void set3Sound1st_Click(object sender, EventArgs e)
		{
			AddSoundForPlayer(_firstPlayer, ScoresRange.Third);
		}

		private void set1Sound2nd_Click(object sender, EventArgs e)
		{
			AddSoundForPlayer(_secondPlayer, ScoresRange.First);
		}

		private void set2Sound2nd_Click(object sender, EventArgs e)
		{
			AddSoundForPlayer(_secondPlayer, ScoresRange.Second);
		}

		private void set3Sound2nd_Click(object sender, EventArgs e)
		{
			AddSoundForPlayer(_secondPlayer, ScoresRange.Third);
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

			float fontSize = Math.Max(firstPlayerScores.Width * firstPlayerScores.Height / ((514 * 362) / 150F)*fontScale, 14F);

			firstPlayerScores.Font = new System.Drawing.Font("Microsoft Sans Serif", fontSize, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			secondPlayerScores.Font = new System.Drawing.Font("Microsoft Sans Serif", fontSize, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			countdownTimer.Font = new System.Drawing.Font("Microsoft Sans Serif", fontSize * 0.3F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));

			countdownTimer.Location = new System.Drawing.Point(firstPlayerScores.Width + firstPlayerScores.Location.X - countdownTimer.Width / 2, countdownTimer.Location.Y);
		}

		private void setRoundRemainSound_Click(object sender, EventArgs e)
		{
			AddMainSound(MainActionsType.RoundRemain10Seconds);
		}

		private void setRoundEndSound_Click(object sender, EventArgs e)
		{
			AddMainSound(MainActionsType.RoundComplete);
		}

		private void SaveSettings(string fileName)
		{
			JsonSerializer serializer = new JsonSerializer();
			serializer.NullValueHandling = NullValueHandling.Ignore;
			serializer.Formatting = Formatting.Indented;

			using (StreamWriter sw = File.CreateText(fileName))
			{
				sw.NewLine = Environment.NewLine;

				using (JsonWriter writer = new JsonTextWriter(sw))
				{
					JsonSettings bindings = new JsonSettings
					{
						MainFunctionSounds = _funcButtonsSounds,
						MainFunctionBinds = _funcButtonsBinds,
						FirstPlayerBinds = _firstPlayer.KeyBindings,
						SecondPlayerBinds = _secondPlayer.KeyBindings,
						FirstPlayerSounds = _firstPlayer.SoundBindings,
						SecondPlayerSounds = _secondPlayer.SoundBindings,
						RoundSeconds = _dashboardSettings.RoundSeconds,
						RoundsCount = _dashboardSettings.RoundsCount,
						PauseSeconds = _dashboardSettings.PauseSeconds,
						FirstPlayerHitsBinds = _firstPlayer.HitBindings,
						SecondPlayerHitsBinds = _secondPlayer.HitBindings,
						RandomTimer1 = _objectMapper.Map<RandomTimerSettingsJson>(_dashboardSettings.RandomTimer1),
						RandomTimer2 = _objectMapper.Map<RandomTimerSettingsJson>(_dashboardSettings.RandomTimer2),
					};

					serializer.Serialize(writer, bindings);
				}
			}
		}

		private void LoadSettings(string fileName)
		{
			if(!File.Exists(fileName))
			{
				return;
			}

			JsonSerializer serializer = new JsonSerializer();
			serializer.NullValueHandling = NullValueHandling.Ignore;
			serializer.Formatting = Formatting.Indented;

			_dashboardSettings.RandomTimer1 = new RandomTimerSettings();
			_dashboardSettings.RandomTimer2 = new RandomTimerSettings();

			using (StreamReader sr = File.OpenText(fileName))
			{
				string json = sr.ReadToEnd();
				JsonSettings settings = JsonConvert.DeserializeObject<JsonSettings>(json);

				if (settings != null)
				{
					if(settings.RoundSeconds.HasValue)
					{
						_dashboardSettings.RoundSeconds = settings.RoundSeconds.Value;
						_countdownTime = TimeSpan.FromSeconds(_dashboardSettings.RoundSeconds);
						countdownTimer.Text = _countdownTime.ToString(@"mm\:ss");
						secondsPartTimer.Value = _countdownTime.Seconds;
						minutesPartTimer.Value = _countdownTime.Minutes;
					}

					if ( settings.PauseSeconds.HasValue )
					{
						_dashboardSettings.PauseSeconds = settings.PauseSeconds.Value;
						_pauseCountdownTime = TimeSpan.FromSeconds(_dashboardSettings.PauseSeconds);
						pauseMinutes.Value = _pauseCountdownTime.Minutes;
						pauseSeconds.Value = _pauseCountdownTime.Seconds;
					}

					if ( settings.RoundsCount.HasValue )
					{
						_dashboardSettings.RoundsCount = settings.RoundsCount.Value;
						roundsCount.Value = _dashboardSettings.RoundsCount;
					}

					if ( settings.RandomTimer1 != null )
					{
						_dashboardSettings.RandomTimer1 = _objectMapper.Map<RandomTimerSettings>(settings.RandomTimer1);
						_dashboardSettings.RandomTimer1.SetAndUpdateControls(rnd1LowerLimit, rnd1UpperLimit, rnd1IsEnable);
					}
					
					if ( settings.RandomTimer2 != null )
					{
						_dashboardSettings.RandomTimer2 = _objectMapper.Map<RandomTimerSettings>(settings.RandomTimer2);
						_dashboardSettings.RandomTimer2.SetAndUpdateControls(rnd2LowerLimit, rnd2UpperLimit, rnd2IsEnable);
					}

					_funcButtonsSounds = settings.MainFunctionSounds;

					foreach (var bind in settings.MainFunctionBinds)
					{
						AssignFunctionalButton(bind.Key, bind.Value);

						switch (bind.Value)
						{
							case MainActionsType.ResetTimer:
								resetTimerButton.Text = GetButtonName(bind.Key);
								break;
							case MainActionsType.StartTimer:
								startTimerButton.Text = GetButtonName(bind.Key);
								break;
							case MainActionsType.StopTimer:
								stopTimerButton.Text = GetButtonName(bind.Key);
								break;
							default:
								break;
						}
					}

					Dictionary<ScoresRange, ScoresBinds> firstPlayerBinds = new Dictionary<ScoresRange, ScoresBinds>
					{
						{
							ScoresRange.First,
							new ScoresBinds
							{
								Label = button1Name1st,
								MessageFunc = new Func<GamepadButtons, string>((arg) => $"Кнопка <{GetButtonName(arg)}> назначена +1 балл для первого участника"),
							}
						},
						{
							ScoresRange.Second,
							new ScoresBinds
							{
								Label = button2Name1st,
								MessageFunc = new Func<GamepadButtons, string>((arg) => $"Кнопка <{GetButtonName(arg)}> назначена +2 балла для первого участника"),
							}
						},
						{
							ScoresRange.Third,
							new ScoresBinds
							{
								Label = button3Name1st,
								MessageFunc = new Func<GamepadButtons, string>((arg) => $"Кнопка <{GetButtonName(arg)}> назначена +3 балла для первого участника"),
							}
						}
					};

					Dictionary<ScoresRange, ScoresBinds> secondPlayerBinds = new Dictionary<ScoresRange, ScoresBinds>
					{
						{
							ScoresRange.First,
							new ScoresBinds
							{
								Label = button1Name2nd,
								MessageFunc = new Func<GamepadButtons, string>((arg) => $"Кнопка <{GetButtonName(arg)}> назначена +1 балл для второго участника"),
							}
						},
						{
							ScoresRange.Second,
							new ScoresBinds
							{
								Label = button2Name2nd,
								MessageFunc = new Func<GamepadButtons, string>((arg) => $"Кнопка <{GetButtonName(arg)}> назначена +2 балла для второго участника"),
							}
						},
						{
							ScoresRange.Third,
							new ScoresBinds
							{
								Label = button3Name2nd,
								MessageFunc = new Func<GamepadButtons, string>((arg) => $"Кнопка <{GetButtonName(arg)}> назначена +3 балла для второго участника"),
							}
						}
					};

					AssignPlayerBindings(settings.FirstPlayerBinds, firstPlayerBinds, GamepadSource.First, _firstPlayer);
					AssignPlayerBindings(settings.SecondPlayerBinds, secondPlayerBinds, GamepadSource.First, _secondPlayer);
					AssignPlayerSounds(settings.FirstPlayerSounds, _firstPlayer);
					AssignPlayerSounds(settings.SecondPlayerSounds, _secondPlayer);
					AssignPlayerHitsMap(settings.FirstPlayerHitsBinds, _firstPlayer,
						new Dictionary<ScoresRange, Button>
						{
							{ ScoresRange.First, firstPlayerOneValue },
							{ ScoresRange.Second, firstPlayerTwoValue },
							{ ScoresRange.Third, firstPlayerThreeValue }
						}, 
						new Dictionary<ScoresRange, Label>
						{
							{ ScoresRange.First, button1Name1st },
							{ ScoresRange.Second, button2Name1st },
							{ ScoresRange.Third, button3Name1st }
						},
						new Dictionary<ScoresRange, Button>
						{
							{ ScoresRange.First, set1Sound1st },
							{ ScoresRange.Second, set2Sound1st },
							{ ScoresRange.Third, set3Sound1st }
						},
						new Dictionary<ScoresRange, NumericUpDown>
						{
							{ ScoresRange.First, player1Scores1 },
							{ ScoresRange.Second, player1Scores2 },
							{ ScoresRange.Third, player1Scores3 }
						});
					
					AssignPlayerHitsMap(settings.SecondPlayerHitsBinds, _secondPlayer,new Dictionary<ScoresRange, Button>()
						{
							{ ScoresRange.First, secondPlayerOneValue },
							{ ScoresRange.Second, secondPlayerTwoValue },
							{ ScoresRange.Third, secondPlayerThreeValue }
						}, 
						new Dictionary<ScoresRange, Label>
						{
							{ ScoresRange.First, button1Name2nd },
							{ ScoresRange.Second, button2Name2nd },
							{ ScoresRange.Third, button3Name2nd }
						},
						new Dictionary<ScoresRange, Button>
						{
							{ ScoresRange.First, set1Sound2nd },
							{ ScoresRange.Second, set2Sound2nd },
							{ ScoresRange.Third, set3Sound2nd }
						},
						new Dictionary<ScoresRange, NumericUpDown>
						{
							{ ScoresRange.First, player2Scores1 },
							{ ScoresRange.Second, player2Scores2 },
							{ ScoresRange.Third, player2Scores3 }
						});
				}
			}
		}

		private void AssignPlayerBindings(Dictionary<GamepadButtons, ScoresRange> buttonBindings, Dictionary<ScoresRange, ScoresBinds> playerBinds, GamepadSource gamepadSource, Player player)
		{
			foreach (var playerBind in buttonBindings)
			{
				if (playerBinds.TryGetValue(playerBind.Value, out ScoresBinds scoresBinds))
				{
					AssignButton(button: playerBind.Key,
					message: scoresBinds.MessageFunc(playerBind.Key),
					assignAction: btn =>
					{
						player.AddScoresKeyBinding(playerBind.Value, playerBind.Key);
						if(scoresBinds.Label != null)
							scoresBinds.Label.Text = GetButtonName(playerBind.Key);
					});
				}
			}
		}

		private void AssignPlayerSounds(Dictionary<ScoresRange, string> playerSounds, Player player)
		{
			foreach (var sound in playerSounds)
			{
				player.AddSoundBinding(sound.Key, sound.Value);
			}
		}
		
		private void AssignPlayerHitsMap(Dictionary<ScoresRange, ushort> hitsMap, Player player, 
			Dictionary<ScoresRange, Button> buttons, Dictionary<ScoresRange, Label> buttonLabel, 
			Dictionary<ScoresRange, Button> soundButton, Dictionary<ScoresRange, NumericUpDown> numerics)
		{
			if ( hitsMap?.Any() != true )
				return;
			
			foreach (var hit in hitsMap)
			{
				player.AddScoresHitsAmount(hit.Key, hit.Value);
				UpdateTextButtonLabels(hit.Value, buttons[hit.Key], buttonLabel[hit.Key], soundButton[hit.Key]);

				if ( numerics != null && numerics.TryGetValue(hit.Key, out NumericUpDown numericControl) )
				{
					numericControl.Value = hit.Value;
				}
			}
		}

		private void ScoresForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			SaveSettings(SettingsFilePath);
		}

		private void setPauseRemainSound_Click(object sender, EventArgs e)
		{
			AddMainSound(MainActionsType.PauseRoundRemain10Seconds);
		}

		private void numericUpDown1_ValueChanged(object sender, EventArgs e)
		{
			_dashboardSettings.RoundsCount = (int)roundsCount.Value;
		}

		private void setPause_Click(object sender, EventArgs e)
		{
			int minutes = (int)Math.Min(59, pauseMinutes.Value), seconds = (int)Math.Min(59, pauseSeconds.Value);
			
			_pauseCountdownTime = TimeSpan.FromMinutes(minutes).Add(TimeSpan.FromSeconds(seconds));
			_dashboardSettings.PauseSeconds = _pauseCountdownTime.TotalSeconds;
		}

		private void SetHitsAmount(Player player, ScoresRange range, ushort amount, Button hitsButton, Label hitsLabel,
			Button soundButton)
		{
			player.AddScoresHitsAmount(range, amount);
			UpdateTextButtonLabels(amount, hitsButton, hitsLabel, soundButton);
		}

		private static void UpdateTextButtonLabels(ushort amount, Button hitsButton, Label hitsLabel, Button soundButton)
		{
			if(hitsButton != null)
				hitsButton.Text = $"+{amount} балл(а)";
			if(hitsLabel != null )
				hitsLabel.Text = $"Кнопка +{amount}";
			if(soundButton != null)
				soundButton.Text = $"Звук <+{amount}>";
		}

		private void player1Scores1_ValueChanged(object sender, EventArgs e)
		{
			SetHitsAmount(_firstPlayer, ScoresRange.First, (ushort) player1Scores1.Value, firstPlayerOneValue,
				button1Name1st, set1Sound1st);
		}

		private void player1Scores2_ValueChanged(object sender, EventArgs e)
		{
			SetHitsAmount(_firstPlayer, ScoresRange.Second, (ushort) player1Scores2.Value, firstPlayerTwoValue,
				button2Name1st, set2Sound1st);
		}

		private void player1Scores3_ValueChanged(object sender, EventArgs e)
		{
			SetHitsAmount(_firstPlayer, ScoresRange.Third, (ushort) player1Scores3.Value, firstPlayerThreeValue,
				button3Name1st, set3Sound1st);
		}

		private void player2Scores1_ValueChanged(object sender, EventArgs e)
		{
			SetHitsAmount(_secondPlayer, ScoresRange.First, (ushort) player2Scores1.Value, secondPlayerOneValue,
				button1Name2nd, set1Sound2nd);
		}

		private void player2Scores2_ValueChanged(object sender, EventArgs e)
		{
			SetHitsAmount(_secondPlayer, ScoresRange.Second, (ushort) player2Scores2.Value, secondPlayerTwoValue,
				button2Name2nd, set2Sound2nd);
		}

		private void player2Scores3_ValueChanged(object sender, EventArgs e)
		{
			SetHitsAmount(_secondPlayer, ScoresRange.Third, (ushort) player2Scores3.Value, secondPlayerThreeValue,
				button3Name2nd, set3Sound2nd);
		}

		private void loadSettings_Click(object sender, EventArgs e)
		{
			openFileDialog.Filter = "Json-config|*.json";
			openFileDialog.CheckFileExists = true;
			if(openFileDialog.ShowDialog() == DialogResult.OK)
			{
				string fileName = openFileDialog.FileName;
				if(!string.IsNullOrEmpty(fileName))
					LoadSettings(fileName);
			}
		}

		private void saveSettings_Click(object sender, EventArgs e)
		{
			openFileDialog.Filter = "Json-config|*.json";
			openFileDialog.CheckFileExists = false;
			if(openFileDialog.ShowDialog() == DialogResult.OK)
			{
				string fileName = openFileDialog.FileName;
				string extension = Path.GetExtension(fileName);
				if ( extension != ".json" )
				{
					fileName = $"{fileName}.json";
				}
				
				if(!string.IsNullOrEmpty(fileName))
					SaveSettings(fileName);
			}
		}

		private void RestartExtraTimer(Timer timer, int lowerLimit, int upperLimit)
		{
			timer.Stop();
			Random rnd = new Random(DateTime.Now.Second);
			int min = Math.Max(1, Math.Min(lowerLimit, upperLimit));
			int max = Math.Max(lowerLimit, upperLimit);
			int randomValue = rnd.Next(min, max);

			if(randomValue > 0)
			{
				timer.Interval = randomValue * 1000;
				timer.Start();
			}
		}

		private void rnd1IsEnable_CheckedChanged(object sender, EventArgs e)
		{
			_dashboardSettings.RandomTimer1.IsEnabled = rnd1IsEnable.Checked;

			if (!IsRoundCompleted && _dashboardSettings.RandomTimer1.IsEnabled)
			{
				RestartExtraTimer(_extraTimer1, _dashboardSettings.RandomTimer1.LowerLimit, _dashboardSettings.RandomTimer1.UpperLimit);
			}

			if (!_dashboardSettings.RandomTimer1.IsEnabled && _extraTimer1.Enabled)
			{
				_extraTimer1.Stop();
			}
		}

		private void rnd1LowerLimit_ValueChanged(object sender, EventArgs e)
		{
			_dashboardSettings.RandomTimer1.LowerLimit = (int)rnd1LowerLimit.Value;
		}

		private void rnd1UpperLimit_ValueChanged(object sender, EventArgs e)
		{
			_dashboardSettings.RandomTimer1.UpperLimit = (int) rnd1UpperLimit.Value;
		}

		private void rnd1SetSound_Click(object sender, EventArgs e)
		{
			_dashboardSettings.RandomTimer1.FilePath = AddMainSound(MainActionsType.RandomTimer1);
		}

		private void rnd2IsEnable_CheckedChanged(object sender, EventArgs e)
		{
			_dashboardSettings.RandomTimer2.IsEnabled = rnd2IsEnable.Checked;
			
			if (!IsRoundCompleted && _dashboardSettings.RandomTimer2.IsEnabled)
			{
				RestartExtraTimer(_extraTimer2, _dashboardSettings.RandomTimer2.LowerLimit, _dashboardSettings.RandomTimer2.UpperLimit);
			}

			if (!_dashboardSettings.RandomTimer2.IsEnabled && _extraTimer2.Enabled)
			{
				_extraTimer2.Stop();
			}
		}

		private void rnd2LowerLimit_ValueChanged(object sender, EventArgs e)
		{
			_dashboardSettings.RandomTimer2.LowerLimit = (int) rnd2LowerLimit.Value;
		}

		private void rnd2UpperLimit_ValueChanged(object sender, EventArgs e)
		{
			_dashboardSettings.RandomTimer2.UpperLimit = (int) rnd2UpperLimit.Value;
		}

		private void rnd2SetSound_Click(object sender, EventArgs e)
		{
			if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
			{
				_dashboardSettings.RandomTimer2.FilePath = folderBrowserDialog.SelectedPath;
			}
		}
	}
}
