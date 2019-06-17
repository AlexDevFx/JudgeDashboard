namespace JudgeScores
{
	partial class ScoresForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.countdownTimer = new System.Windows.Forms.Label();
			this.JudgesDashboard = new System.Windows.Forms.TabControl();
			this.ScoresPage = new System.Windows.Forms.TabPage();
			this.secondPlayerScores = new System.Windows.Forms.Label();
			this.firstPlayerScores = new System.Windows.Forms.Label();
			this.SettingsPage = new System.Windows.Forms.TabPage();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.set3Sound2nd = new System.Windows.Forms.Button();
			this.set2Sound2nd = new System.Windows.Forms.Button();
			this.set1Sound2nd = new System.Windows.Forms.Button();
			this.button2Name2nd = new System.Windows.Forms.Label();
			this.button3Name2nd = new System.Windows.Forms.Label();
			this.button1Name2nd = new System.Windows.Forms.Label();
			this.secondPlayerThreeValue = new System.Windows.Forms.Button();
			this.secondPlayerTwoValue = new System.Windows.Forms.Button();
			this.secondPlayerOneValue = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.set3Sound1st = new System.Windows.Forms.Button();
			this.set2Sound1st = new System.Windows.Forms.Button();
			this.set1Sound1st = new System.Windows.Forms.Button();
			this.button2Name1st = new System.Windows.Forms.Label();
			this.button3Name1st = new System.Windows.Forms.Label();
			this.button1Name1st = new System.Windows.Forms.Label();
			this.firstPlayerThreeValue = new System.Windows.Forms.Button();
			this.firstPlayerTwoValue = new System.Windows.Forms.Button();
			this.firstPlayerOneValue = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.setSoundReset = new System.Windows.Forms.Button();
			this.setSoundStop = new System.Windows.Forms.Button();
			this.setSoundStart = new System.Windows.Forms.Button();
			this.stopTimerButton = new System.Windows.Forms.Label();
			this.resetTimerButton = new System.Windows.Forms.Label();
			this.startTimerButton = new System.Windows.Forms.Label();
			this.notifyText = new System.Windows.Forms.Label();
			this.SetTimer = new System.Windows.Forms.Button();
			this.secondsPartTimer = new System.Windows.Forms.NumericUpDown();
			this.minutesPartTimer = new System.Windows.Forms.NumericUpDown();
			this.resetTimer = new System.Windows.Forms.Button();
			this.assignStop = new System.Windows.Forms.Button();
			this.assignStartButton = new System.Windows.Forms.Button();
			this.loggerPage = new System.Windows.Forms.TabPage();
			this.loggerTextbox = new System.Windows.Forms.TextBox();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.JudgesDashboard.SuspendLayout();
			this.ScoresPage.SuspendLayout();
			this.SettingsPage.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.secondsPartTimer)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.minutesPartTimer)).BeginInit();
			this.loggerPage.SuspendLayout();
			this.SuspendLayout();
			// 
			// countdownTimer
			// 
			this.countdownTimer.AutoSize = true;
			this.countdownTimer.BackColor = System.Drawing.Color.Black;
			this.countdownTimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.countdownTimer.ForeColor = System.Drawing.Color.Snow;
			this.countdownTimer.Location = new System.Drawing.Point(435, 3);
			this.countdownTimer.Name = "countdownTimer";
			this.countdownTimer.Size = new System.Drawing.Size(150, 55);
			this.countdownTimer.TabIndex = 0;
			this.countdownTimer.Text = "00:00";
			// 
			// JudgesDashboard
			// 
			this.JudgesDashboard.Controls.Add(this.ScoresPage);
			this.JudgesDashboard.Controls.Add(this.SettingsPage);
			this.JudgesDashboard.Controls.Add(this.loggerPage);
			this.JudgesDashboard.Location = new System.Drawing.Point(-1, 0);
			this.JudgesDashboard.Name = "JudgesDashboard";
			this.JudgesDashboard.SelectedIndex = 0;
			this.JudgesDashboard.Size = new System.Drawing.Size(1031, 394);
			this.JudgesDashboard.TabIndex = 1;
			// 
			// ScoresPage
			// 
			this.ScoresPage.Controls.Add(this.countdownTimer);
			this.ScoresPage.Controls.Add(this.secondPlayerScores);
			this.ScoresPage.Controls.Add(this.firstPlayerScores);
			this.ScoresPage.Location = new System.Drawing.Point(4, 22);
			this.ScoresPage.Name = "ScoresPage";
			this.ScoresPage.Padding = new System.Windows.Forms.Padding(3);
			this.ScoresPage.Size = new System.Drawing.Size(1023, 368);
			this.ScoresPage.TabIndex = 0;
			this.ScoresPage.Text = "Раунд";
			this.ScoresPage.UseVisualStyleBackColor = true;
			// 
			// secondPlayerScores
			// 
			this.secondPlayerScores.BackColor = System.Drawing.Color.Blue;
			this.secondPlayerScores.Font = new System.Drawing.Font("Microsoft Sans Serif", 240F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.secondPlayerScores.ForeColor = System.Drawing.SystemColors.Window;
			this.secondPlayerScores.Location = new System.Drawing.Point(510, 3);
			this.secondPlayerScores.Name = "secondPlayerScores";
			this.secondPlayerScores.Size = new System.Drawing.Size(514, 362);
			this.secondPlayerScores.TabIndex = 2;
			this.secondPlayerScores.Text = "99";
			this.secondPlayerScores.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// firstPlayerScores
			// 
			this.firstPlayerScores.BackColor = System.Drawing.Color.Red;
			this.firstPlayerScores.Font = new System.Drawing.Font("Microsoft Sans Serif", 240F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.firstPlayerScores.ForeColor = System.Drawing.SystemColors.Window;
			this.firstPlayerScores.Location = new System.Drawing.Point(3, 3);
			this.firstPlayerScores.Name = "firstPlayerScores";
			this.firstPlayerScores.Size = new System.Drawing.Size(514, 362);
			this.firstPlayerScores.TabIndex = 1;
			this.firstPlayerScores.Text = "99";
			this.firstPlayerScores.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// SettingsPage
			// 
			this.SettingsPage.Controls.Add(this.groupBox2);
			this.SettingsPage.Controls.Add(this.groupBox1);
			this.SettingsPage.Controls.Add(this.label1);
			this.SettingsPage.Controls.Add(this.setSoundReset);
			this.SettingsPage.Controls.Add(this.setSoundStop);
			this.SettingsPage.Controls.Add(this.setSoundStart);
			this.SettingsPage.Controls.Add(this.stopTimerButton);
			this.SettingsPage.Controls.Add(this.resetTimerButton);
			this.SettingsPage.Controls.Add(this.startTimerButton);
			this.SettingsPage.Controls.Add(this.notifyText);
			this.SettingsPage.Controls.Add(this.SetTimer);
			this.SettingsPage.Controls.Add(this.secondsPartTimer);
			this.SettingsPage.Controls.Add(this.minutesPartTimer);
			this.SettingsPage.Controls.Add(this.resetTimer);
			this.SettingsPage.Controls.Add(this.assignStop);
			this.SettingsPage.Controls.Add(this.assignStartButton);
			this.SettingsPage.Location = new System.Drawing.Point(4, 22);
			this.SettingsPage.Name = "SettingsPage";
			this.SettingsPage.Padding = new System.Windows.Forms.Padding(3);
			this.SettingsPage.Size = new System.Drawing.Size(1023, 368);
			this.SettingsPage.TabIndex = 1;
			this.SettingsPage.Text = "Настройки";
			this.SettingsPage.UseVisualStyleBackColor = true;
			// 
			// groupBox2
			// 
			this.groupBox2.BackColor = System.Drawing.Color.Turquoise;
			this.groupBox2.Controls.Add(this.set3Sound2nd);
			this.groupBox2.Controls.Add(this.set2Sound2nd);
			this.groupBox2.Controls.Add(this.set1Sound2nd);
			this.groupBox2.Controls.Add(this.button2Name2nd);
			this.groupBox2.Controls.Add(this.button3Name2nd);
			this.groupBox2.Controls.Add(this.button1Name2nd);
			this.groupBox2.Controls.Add(this.secondPlayerThreeValue);
			this.groupBox2.Controls.Add(this.secondPlayerTwoValue);
			this.groupBox2.Controls.Add(this.secondPlayerOneValue);
			this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.groupBox2.Location = new System.Drawing.Point(593, 91);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(420, 119);
			this.groupBox2.TabIndex = 35;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Второй участник";
			// 
			// set3Sound2nd
			// 
			this.set3Sound2nd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.set3Sound2nd.Location = new System.Drawing.Point(308, 83);
			this.set3Sound2nd.Name = "set3Sound2nd";
			this.set3Sound2nd.Size = new System.Drawing.Size(88, 23);
			this.set3Sound2nd.TabIndex = 29;
			this.set3Sound2nd.Text = "Звук <+3>";
			this.set3Sound2nd.UseVisualStyleBackColor = true;
			this.set3Sound2nd.Click += new System.EventHandler(this.set3Sound2nd_Click);
			// 
			// set2Sound2nd
			// 
			this.set2Sound2nd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.set2Sound2nd.Location = new System.Drawing.Point(308, 54);
			this.set2Sound2nd.Name = "set2Sound2nd";
			this.set2Sound2nd.Size = new System.Drawing.Size(88, 23);
			this.set2Sound2nd.TabIndex = 28;
			this.set2Sound2nd.Text = "Звук <+2>";
			this.set2Sound2nd.UseVisualStyleBackColor = true;
			this.set2Sound2nd.Click += new System.EventHandler(this.set2Sound2nd_Click);
			// 
			// set1Sound2nd
			// 
			this.set1Sound2nd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.set1Sound2nd.Location = new System.Drawing.Point(308, 24);
			this.set1Sound2nd.Name = "set1Sound2nd";
			this.set1Sound2nd.Size = new System.Drawing.Size(88, 23);
			this.set1Sound2nd.TabIndex = 27;
			this.set1Sound2nd.Text = "Звук <+1>";
			this.set1Sound2nd.UseVisualStyleBackColor = true;
			this.set1Sound2nd.Click += new System.EventHandler(this.set1Sound2nd_Click);
			// 
			// button2Name2nd
			// 
			this.button2Name2nd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.button2Name2nd.Location = new System.Drawing.Point(114, 53);
			this.button2Name2nd.Name = "button2Name2nd";
			this.button2Name2nd.Size = new System.Drawing.Size(173, 25);
			this.button2Name2nd.TabIndex = 19;
			this.button2Name2nd.Text = "Кнопка +2";
			this.button2Name2nd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// button3Name2nd
			// 
			this.button3Name2nd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.button3Name2nd.Location = new System.Drawing.Point(114, 81);
			this.button3Name2nd.Name = "button3Name2nd";
			this.button3Name2nd.Size = new System.Drawing.Size(173, 25);
			this.button3Name2nd.TabIndex = 18;
			this.button3Name2nd.Text = "Кнопка +3";
			this.button3Name2nd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// button1Name2nd
			// 
			this.button1Name2nd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.button1Name2nd.Location = new System.Drawing.Point(114, 24);
			this.button1Name2nd.Name = "button1Name2nd";
			this.button1Name2nd.Size = new System.Drawing.Size(173, 25);
			this.button1Name2nd.TabIndex = 17;
			this.button1Name2nd.Text = "Кнопка +1";
			this.button1Name2nd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// secondPlayerThreeValue
			// 
			this.secondPlayerThreeValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.secondPlayerThreeValue.Location = new System.Drawing.Point(20, 83);
			this.secondPlayerThreeValue.Name = "secondPlayerThreeValue";
			this.secondPlayerThreeValue.Size = new System.Drawing.Size(88, 23);
			this.secondPlayerThreeValue.TabIndex = 16;
			this.secondPlayerThreeValue.Text = "+3 балла";
			this.secondPlayerThreeValue.UseVisualStyleBackColor = true;
			this.secondPlayerThreeValue.Click += new System.EventHandler(this.secondPlayerThreeValue_Click);
			// 
			// secondPlayerTwoValue
			// 
			this.secondPlayerTwoValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.secondPlayerTwoValue.Location = new System.Drawing.Point(20, 54);
			this.secondPlayerTwoValue.Name = "secondPlayerTwoValue";
			this.secondPlayerTwoValue.Size = new System.Drawing.Size(88, 23);
			this.secondPlayerTwoValue.TabIndex = 15;
			this.secondPlayerTwoValue.Text = "+2 балла";
			this.secondPlayerTwoValue.UseVisualStyleBackColor = true;
			this.secondPlayerTwoValue.Click += new System.EventHandler(this.secondPlayerTwoValue_Click);
			// 
			// secondPlayerOneValue
			// 
			this.secondPlayerOneValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.secondPlayerOneValue.Location = new System.Drawing.Point(20, 24);
			this.secondPlayerOneValue.Name = "secondPlayerOneValue";
			this.secondPlayerOneValue.Size = new System.Drawing.Size(88, 23);
			this.secondPlayerOneValue.TabIndex = 14;
			this.secondPlayerOneValue.Text = "+1 балл";
			this.secondPlayerOneValue.UseVisualStyleBackColor = true;
			this.secondPlayerOneValue.Click += new System.EventHandler(this.secondPlayerOneValue_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.BackColor = System.Drawing.Color.LightCoral;
			this.groupBox1.Controls.Add(this.set3Sound1st);
			this.groupBox1.Controls.Add(this.set2Sound1st);
			this.groupBox1.Controls.Add(this.set1Sound1st);
			this.groupBox1.Controls.Add(this.button2Name1st);
			this.groupBox1.Controls.Add(this.button3Name1st);
			this.groupBox1.Controls.Add(this.button1Name1st);
			this.groupBox1.Controls.Add(this.firstPlayerThreeValue);
			this.groupBox1.Controls.Add(this.firstPlayerTwoValue);
			this.groupBox1.Controls.Add(this.firstPlayerOneValue);
			this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.groupBox1.Location = new System.Drawing.Point(9, 91);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(400, 119);
			this.groupBox1.TabIndex = 34;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Первый участник";
			// 
			// set3Sound1st
			// 
			this.set3Sound1st.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.set3Sound1st.Location = new System.Drawing.Point(286, 86);
			this.set3Sound1st.Name = "set3Sound1st";
			this.set3Sound1st.Size = new System.Drawing.Size(88, 23);
			this.set3Sound1st.TabIndex = 26;
			this.set3Sound1st.Text = "Звук <+3>";
			this.set3Sound1st.UseVisualStyleBackColor = true;
			this.set3Sound1st.Click += new System.EventHandler(this.set3Sound1st_Click);
			// 
			// set2Sound1st
			// 
			this.set2Sound1st.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.set2Sound1st.Location = new System.Drawing.Point(286, 57);
			this.set2Sound1st.Name = "set2Sound1st";
			this.set2Sound1st.Size = new System.Drawing.Size(88, 23);
			this.set2Sound1st.TabIndex = 25;
			this.set2Sound1st.Text = "Звук <+2>";
			this.set2Sound1st.UseVisualStyleBackColor = true;
			this.set2Sound1st.Click += new System.EventHandler(this.set2Sound1st_Click);
			// 
			// set1Sound1st
			// 
			this.set1Sound1st.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.set1Sound1st.Location = new System.Drawing.Point(286, 27);
			this.set1Sound1st.Name = "set1Sound1st";
			this.set1Sound1st.Size = new System.Drawing.Size(88, 23);
			this.set1Sound1st.TabIndex = 24;
			this.set1Sound1st.Text = "Звук <+1>";
			this.set1Sound1st.UseVisualStyleBackColor = true;
			this.set1Sound1st.Click += new System.EventHandler(this.set1Sound1st_Click);
			// 
			// button2Name1st
			// 
			this.button2Name1st.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.button2Name1st.Location = new System.Drawing.Point(109, 55);
			this.button2Name1st.Name = "button2Name1st";
			this.button2Name1st.Size = new System.Drawing.Size(173, 25);
			this.button2Name1st.TabIndex = 13;
			this.button2Name1st.Text = "Кнопка +2";
			this.button2Name1st.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// button3Name1st
			// 
			this.button3Name1st.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.button3Name1st.Location = new System.Drawing.Point(109, 83);
			this.button3Name1st.Name = "button3Name1st";
			this.button3Name1st.Size = new System.Drawing.Size(173, 25);
			this.button3Name1st.TabIndex = 12;
			this.button3Name1st.Text = "Кнопка +3";
			this.button3Name1st.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// button1Name1st
			// 
			this.button1Name1st.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.button1Name1st.Location = new System.Drawing.Point(109, 26);
			this.button1Name1st.Name = "button1Name1st";
			this.button1Name1st.Size = new System.Drawing.Size(173, 25);
			this.button1Name1st.TabIndex = 11;
			this.button1Name1st.Text = "Кнопка +1";
			this.button1Name1st.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// firstPlayerThreeValue
			// 
			this.firstPlayerThreeValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.firstPlayerThreeValue.Location = new System.Drawing.Point(15, 85);
			this.firstPlayerThreeValue.Name = "firstPlayerThreeValue";
			this.firstPlayerThreeValue.Size = new System.Drawing.Size(88, 23);
			this.firstPlayerThreeValue.TabIndex = 9;
			this.firstPlayerThreeValue.Text = "+3 балла";
			this.firstPlayerThreeValue.UseVisualStyleBackColor = true;
			this.firstPlayerThreeValue.Click += new System.EventHandler(this.firstPlayerThreeValue_Click);
			// 
			// firstPlayerTwoValue
			// 
			this.firstPlayerTwoValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.firstPlayerTwoValue.Location = new System.Drawing.Point(15, 56);
			this.firstPlayerTwoValue.Name = "firstPlayerTwoValue";
			this.firstPlayerTwoValue.Size = new System.Drawing.Size(88, 23);
			this.firstPlayerTwoValue.TabIndex = 8;
			this.firstPlayerTwoValue.Text = "+2 балла";
			this.firstPlayerTwoValue.UseVisualStyleBackColor = true;
			this.firstPlayerTwoValue.Click += new System.EventHandler(this.firstPlayerTwoValue_Click);
			// 
			// firstPlayerOneValue
			// 
			this.firstPlayerOneValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.firstPlayerOneValue.Location = new System.Drawing.Point(15, 26);
			this.firstPlayerOneValue.Name = "firstPlayerOneValue";
			this.firstPlayerOneValue.Size = new System.Drawing.Size(88, 23);
			this.firstPlayerOneValue.TabIndex = 7;
			this.firstPlayerOneValue.Text = "+1 балл";
			this.firstPlayerOneValue.UseVisualStyleBackColor = true;
			this.firstPlayerOneValue.Click += new System.EventHandler(this.firstPlayerOneValue_Click);
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.Location = new System.Drawing.Point(412, 2);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(166, 25);
			this.label1.TabIndex = 33;
			this.label1.Text = "Установка таймера";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// setSoundReset
			// 
			this.setSoundReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.setSoundReset.Location = new System.Drawing.Point(623, 288);
			this.setSoundReset.Name = "setSoundReset";
			this.setSoundReset.Size = new System.Drawing.Size(123, 23);
			this.setSoundReset.TabIndex = 32;
			this.setSoundReset.Text = "Звук <Сброс>";
			this.setSoundReset.UseVisualStyleBackColor = true;
			this.setSoundReset.Click += new System.EventHandler(this.setSoundReset_Click);
			// 
			// setSoundStop
			// 
			this.setSoundStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.setSoundStop.Location = new System.Drawing.Point(623, 259);
			this.setSoundStop.Name = "setSoundStop";
			this.setSoundStop.Size = new System.Drawing.Size(123, 23);
			this.setSoundStop.TabIndex = 31;
			this.setSoundStop.Text = "Звук <Стоп>";
			this.setSoundStop.UseVisualStyleBackColor = true;
			this.setSoundStop.Click += new System.EventHandler(this.setSoundStop_Click);
			// 
			// setSoundStart
			// 
			this.setSoundStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.setSoundStart.Location = new System.Drawing.Point(623, 229);
			this.setSoundStart.Name = "setSoundStart";
			this.setSoundStart.Size = new System.Drawing.Size(123, 23);
			this.setSoundStart.TabIndex = 30;
			this.setSoundStart.Text = "Звук <Старт>";
			this.setSoundStart.UseVisualStyleBackColor = true;
			this.setSoundStart.Click += new System.EventHandler(this.setSoundStart_Click);
			// 
			// stopTimerButton
			// 
			this.stopTimerButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.stopTimerButton.Location = new System.Drawing.Point(451, 257);
			this.stopTimerButton.Name = "stopTimerButton";
			this.stopTimerButton.Size = new System.Drawing.Size(134, 25);
			this.stopTimerButton.TabIndex = 22;
			this.stopTimerButton.Text = "Кнопка <Стоп>";
			this.stopTimerButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// resetTimerButton
			// 
			this.resetTimerButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.resetTimerButton.Location = new System.Drawing.Point(451, 289);
			this.resetTimerButton.Name = "resetTimerButton";
			this.resetTimerButton.Size = new System.Drawing.Size(134, 25);
			this.resetTimerButton.TabIndex = 21;
			this.resetTimerButton.Text = "Кнопка <Сброс>";
			this.resetTimerButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// startTimerButton
			// 
			this.startTimerButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.startTimerButton.Location = new System.Drawing.Point(451, 227);
			this.startTimerButton.Name = "startTimerButton";
			this.startTimerButton.Size = new System.Drawing.Size(134, 25);
			this.startTimerButton.TabIndex = 20;
			this.startTimerButton.Text = "Кнопка <Cтарт>";
			this.startTimerButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// notifyText
			// 
			this.notifyText.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.notifyText.Location = new System.Drawing.Point(3, 340);
			this.notifyText.Name = "notifyText";
			this.notifyText.Size = new System.Drawing.Size(1020, 25);
			this.notifyText.TabIndex = 10;
			this.notifyText.Text = "Уведомление";
			// 
			// SetTimer
			// 
			this.SetTimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.SetTimer.Location = new System.Drawing.Point(422, 62);
			this.SetTimer.Name = "SetTimer";
			this.SetTimer.Size = new System.Drawing.Size(156, 27);
			this.SetTimer.TabIndex = 6;
			this.SetTimer.Text = "Установить таймер";
			this.SetTimer.UseVisualStyleBackColor = true;
			this.SetTimer.Click += new System.EventHandler(this.SetTimer_Click);
			// 
			// secondsPartTimer
			// 
			this.secondsPartTimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.secondsPartTimer.Location = new System.Drawing.Point(502, 27);
			this.secondsPartTimer.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
			this.secondsPartTimer.Name = "secondsPartTimer";
			this.secondsPartTimer.Size = new System.Drawing.Size(45, 29);
			this.secondsPartTimer.TabIndex = 4;
			// 
			// minutesPartTimer
			// 
			this.minutesPartTimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.minutesPartTimer.Location = new System.Drawing.Point(442, 27);
			this.minutesPartTimer.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
			this.minutesPartTimer.Name = "minutesPartTimer";
			this.minutesPartTimer.Size = new System.Drawing.Size(47, 29);
			this.minutesPartTimer.TabIndex = 3;
			// 
			// resetTimer
			// 
			this.resetTimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.resetTimer.Location = new System.Drawing.Point(312, 289);
			this.resetTimer.Name = "resetTimer";
			this.resetTimer.Size = new System.Drawing.Size(133, 23);
			this.resetTimer.TabIndex = 2;
			this.resetTimer.Text = "Сброс таймера";
			this.resetTimer.UseVisualStyleBackColor = true;
			this.resetTimer.Click += new System.EventHandler(this.button3_Click);
			// 
			// assignStop
			// 
			this.assignStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.assignStop.Location = new System.Drawing.Point(312, 259);
			this.assignStop.Name = "assignStop";
			this.assignStop.Size = new System.Drawing.Size(133, 23);
			this.assignStop.TabIndex = 1;
			this.assignStop.Text = "Пауза таймера";
			this.assignStop.UseVisualStyleBackColor = true;
			this.assignStop.Click += new System.EventHandler(this.assignStop_Click);
			// 
			// assignStartButton
			// 
			this.assignStartButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
			this.assignStartButton.Location = new System.Drawing.Point(312, 229);
			this.assignStartButton.Name = "assignStartButton";
			this.assignStartButton.Size = new System.Drawing.Size(133, 23);
			this.assignStartButton.TabIndex = 0;
			this.assignStartButton.Text = "Старт таймера";
			this.assignStartButton.UseVisualStyleBackColor = true;
			this.assignStartButton.Click += new System.EventHandler(this.assignStartButton_Click);
			// 
			// loggerPage
			// 
			this.loggerPage.Controls.Add(this.loggerTextbox);
			this.loggerPage.Location = new System.Drawing.Point(4, 22);
			this.loggerPage.Name = "loggerPage";
			this.loggerPage.Padding = new System.Windows.Forms.Padding(3);
			this.loggerPage.Size = new System.Drawing.Size(1023, 368);
			this.loggerPage.TabIndex = 2;
			this.loggerPage.Text = "Лог";
			this.loggerPage.UseVisualStyleBackColor = true;
			// 
			// loggerTextbox
			// 
			this.loggerTextbox.Location = new System.Drawing.Point(1, 3);
			this.loggerTextbox.MaxLength = 512767;
			this.loggerTextbox.Multiline = true;
			this.loggerTextbox.Name = "loggerTextbox";
			this.loggerTextbox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.loggerTextbox.Size = new System.Drawing.Size(1021, 365);
			this.loggerTextbox.TabIndex = 0;
			// 
			// openFileDialog
			// 
			this.openFileDialog.FileName = "openFileDialog";
			this.openFileDialog.Filter = "Mp3|*.mp3|Wav|*.wav";
			// 
			// ScoresForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1028, 391);
			this.Controls.Add(this.JudgesDashboard);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "ScoresForm";
			this.Text = "Судейская доска";
			this.JudgesDashboard.ResumeLayout(false);
			this.ScoresPage.ResumeLayout(false);
			this.ScoresPage.PerformLayout();
			this.SettingsPage.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.secondsPartTimer)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.minutesPartTimer)).EndInit();
			this.loggerPage.ResumeLayout(false);
			this.loggerPage.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label countdownTimer;
		private System.Windows.Forms.TabControl JudgesDashboard;
		private System.Windows.Forms.TabPage ScoresPage;
		private System.Windows.Forms.TabPage SettingsPage;
		private System.Windows.Forms.Button resetTimer;
		private System.Windows.Forms.Button assignStop;
		private System.Windows.Forms.Button assignStartButton;
		private System.Windows.Forms.Button SetTimer;
		private System.Windows.Forms.NumericUpDown secondsPartTimer;
		private System.Windows.Forms.NumericUpDown minutesPartTimer;
		private System.Windows.Forms.Label secondPlayerScores;
		private System.Windows.Forms.Label firstPlayerScores;
		private System.Windows.Forms.Button firstPlayerThreeValue;
		private System.Windows.Forms.Button firstPlayerTwoValue;
		private System.Windows.Forms.Button firstPlayerOneValue;
		private System.Windows.Forms.TabPage loggerPage;
		private System.Windows.Forms.TextBox loggerTextbox;
		private System.Windows.Forms.Label notifyText;
		private System.Windows.Forms.Label stopTimerButton;
		private System.Windows.Forms.Label resetTimerButton;
		private System.Windows.Forms.Label startTimerButton;
		private System.Windows.Forms.Label button2Name2nd;
		private System.Windows.Forms.Label button3Name2nd;
		private System.Windows.Forms.Label button1Name2nd;
		private System.Windows.Forms.Button secondPlayerThreeValue;
		private System.Windows.Forms.Button secondPlayerTwoValue;
		private System.Windows.Forms.Button secondPlayerOneValue;
		private System.Windows.Forms.Label button2Name1st;
		private System.Windows.Forms.Label button3Name1st;
		private System.Windows.Forms.Label button1Name1st;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button setSoundReset;
		private System.Windows.Forms.Button setSoundStop;
		private System.Windows.Forms.Button setSoundStart;
		private System.Windows.Forms.Button set3Sound2nd;
		private System.Windows.Forms.Button set2Sound2nd;
		private System.Windows.Forms.Button set1Sound2nd;
		private System.Windows.Forms.Button set3Sound1st;
		private System.Windows.Forms.Button set2Sound1st;
		private System.Windows.Forms.Button set1Sound1st;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox1;
	}
}

