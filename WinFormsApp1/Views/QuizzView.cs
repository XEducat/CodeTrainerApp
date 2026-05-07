using CodeTrainerApp.Model;
using CodeTrainerApp.Services;
using CodeTrainerApp.UI;

namespace CodeTrainerApp.Views
{
	public partial class QuizView : Form
	{
		private int currentTaskIndex = 0;
		private readonly Quiz _quiz;
		private User _currentUser;
		private int _passedCount = 0;

		public QuizView(Quiz selectedQuiz)
		{
			InitializeComponent();
			_quiz = selectedQuiz;
			_currentUser = UserService.Instance.CurrentUser;

			ApplyModernStyles();

			InitializeAccess();
			LoadCurrentTask();
		}

		private void ApplyModernStyles()
		{
			// Кнопки
			StyleHelper.ApplySuccessButton(CheckButton);
			StyleHelper.ApplyPrimaryButton(NextButton);

			SkipButton.FlatStyle = FlatStyle.Flat;
			SkipButton.FlatAppearance.BorderSize = 0;
			SkipButton.BackColor = Theme.Warning;
			SkipButton.ForeColor = Color.White;
			SkipButton.Cursor = Cursors.Hand;
			SkipButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

			// Налаштування LoginButton (використовуємо кастомні кольори без дублювання подій)
			StyleHelper.ApplyPrimaryButton(LoginButton);
			LoginButton.BackColor = Theme.TextSecondary;

			// Оновлюємо кольори для Hover, видаляючи старі та додаючи нові, 
			// але оскільки це конструктор, краще просто перевизначити логіку
			LoginButton.MouseEnter += (s, e) => { if (LoginButton.Enabled) LoginButton.BackColor = Theme.TextPrimary; };
			LoginButton.MouseLeave += (s, e) => { if (LoginButton.Enabled) LoginButton.BackColor = Theme.TextSecondary; };

			// Панелі
			TopPanel.BackColor = Theme.Primary;
			SidePanel.BackColor = Theme.Sidebar;
			EditorPanel.BackColor = Theme.Background;

			CodePanel.BackColor = Theme.CodeBackground;
			CodeTextBox.BackColor = Theme.CodeBackground;
			CodeTextBox.ForeColor = Theme.CodeForeground;
			CodeHeaderLabel.BackColor = Color.FromArgb(17, 24, 39);

			ResultPanel.BackColor = Color.White;
			ResultHeaderLabel.BackColor = Color.FromArgb(249, 250, 251);

			if (_quiz != null)
			{
				QuizTitleLabel.Text = _quiz.Title;
				QuizProgressBar.Maximum = _quiz.Tasks.Count;
			}
		}

		private void InitializeAccess()
		{
			if (_currentUser == null)
			{
				LoginButton.Visible = true;
				NextButton.Enabled = false;
				LoginButton.Text = "👤 Увійти";
			}
			else
			{
				LoginButton.Visible = true;
				LoginButton.Text = $"👤 {_currentUser.Email.Split('@')[0]}";
				LoginButton.Enabled = false;
			}
		}

		private void LoadCurrentTask()
		{
			if (_quiz?.Tasks == null || _quiz.Tasks.Count == 0)
			{
				CurrentTaskLabel.Text = "Квіз порожній";
				return;
			}

			if (currentTaskIndex >= _quiz.Tasks.Count) return;

			var task = _quiz.Tasks[currentTaskIndex];

			CurrentTaskLabel.Text = $"Завдання {currentTaskIndex + 1}";
			ProgressLabel.Text = $"ПРОГРЕС: {currentTaskIndex} / {_quiz.Tasks.Count}";
			QuizProgressBar.Value = currentTaskIndex;

			TaskDescriptionLabel.Text = task.Description;
			CodeTextBox.Text = task.CodeTemplate.Replace("\n", "\r\n");

			ResultTextBox.Clear();
			ResultTextBox.ForeColor = Theme.TextPrimary;
			ResultHeaderLabel.Text = "КОНСОЛЬ ВИВОДУ";

			CodeTextBox.Enabled = true;
			NextButton.Enabled = false;
			SkipButton.Enabled = true;

			NextButton.Text = currentTaskIndex == _quiz.Tasks.Count - 1 ? "Завершити 🏁" : "Наступне ➡";
			NextButton.BackColor = Color.Gray;
		}

		private async void CheckButton_Click(object sender, EventArgs e)
		{
			//if (_currentUser == null) return;

			if (string.IsNullOrWhiteSpace(CodeTextBox.Text))
			{
				SetConsoleOutput("Помилка: Код не може бути порожнім!", Theme.Danger);
				return;
			}

			ResultHeaderLabel.Text = "⌛ ПЕРЕВІРКА...";
			var task = _quiz.Tasks[currentTaskIndex];
			var result = await CodeCompiler.RunCode(task, CodeTextBox.Text);

			if (!string.IsNullOrEmpty(result.errorMessage))
			{
				SetConsoleOutput(result.errorMessage, Theme.Danger);
				return;
			}

			if (result.success)
			{
				SetConsoleOutput("✅ ТЕСТИ ПРОЙДЕНО!\n" + result.output, Theme.Success);
				_passedCount++;

				NextButton.Enabled = true;
				StyleHelper.ApplyPrimaryButton(NextButton);

				CheckButton.Enabled = false;
				SkipButton.Enabled = false;
				CodeTextBox.Enabled = false;
			}
			else
			{
				SetConsoleOutput("❌ ТЕСТИ ПРОВАЛЕНО:\n" + result.output, Theme.Warning);
			}
		}

		private void SetConsoleOutput(string text, Color color)
		{
			ResultTextBox.Text = text;
			ResultTextBox.ForeColor = color;
			ResultHeaderLabel.Text = "РЕЗУЛЬТАТ ПЕРЕВІРКИ";
		}

		private void SkipButton_Click(object sender, EventArgs e)
		{
			SetConsoleOutput("Завдання пропущено.", Theme.TextSecondary);
			NextButton.Enabled = true;
			StyleHelper.ApplyPrimaryButton(NextButton);
			CheckButton.Enabled = false;
			SkipButton.Enabled = false;
			CodeTextBox.Enabled = false;
		}

		private async void NextButton_Click(object sender, EventArgs e)
		{
			currentTaskIndex++;

			if (currentTaskIndex >= _quiz.Tasks.Count)
			{
				QuizProgressBar.Value = _quiz.Tasks.Count;
				ProgressLabel.Text = $"ПРОГРЕС: {_quiz.Tasks.Count} / {_quiz.Tasks.Count}";

				if (_currentUser != null)
				{
					MessageBox.Show($"Вітаємо! Ваш результат: {_passedCount} / {_quiz.Tasks.Count}",
						"Квіз завершено", MessageBoxButtons.OK, MessageBoxIcon.Information);

					try
					{
						var attempt = new Model.UserHistory
						{
							QuizId = _quiz.Id,
							QuizTitle = _quiz.Title,
							Score = _passedCount,
							MaxScore = _quiz.Tasks.Count,
							CompletedAt = DateTime.UtcNow
						};
						await new UserHistoryService().CreateHistoryAsync(attempt, _currentUser.Id);
					}
					catch { }
				}
				this.FormClosing -= QuizView_FormClosing;
				this.Close();
				return;
			}

			LoadCurrentTask();
		}

		private void LoginButton_Click(object sender, EventArgs e)
		{
			using (var loginView = new LoginView())
			{
				if (loginView.ShowDialog() != DialogResult.OK) return;
				if (!loginView.IsLoggedIn) return;

				_currentUser = loginView.LoggedUser;
				InitializeAccess();
			}
		}

		private void QuizView_FormClosing(object sender, FormClosingEventArgs e)
		{
			using (var confirmForm = new ConfirmCloseView())
			{
				if (confirmForm.ShowDialog() != DialogResult.Yes)
					e.Cancel = true;
			}
		}
	}
}