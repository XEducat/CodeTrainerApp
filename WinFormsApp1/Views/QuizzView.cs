using CodeTrainerApp.Model;
using CodeTrainerApp.Services;
using CodeTrainerApp.UI;
using CodeTrainerApp.Views.RegisteredUserViews;

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

			Theme.ThemeChanged += OnThemeChanged;
			this.FormClosed += (s, e) => Theme.ThemeChanged -= OnThemeChanged;
			OnThemeChanged();

			_quiz = selectedQuiz;
			_currentUser = UserService.Instance.CurrentUser;

			// Встановлюємо дані квізу
			if (_quiz != null)
			{
				QuizTitleLabel.Text = $"Code Trainer - {_quiz.Title}";
				QuizProgressBar.Maximum = _quiz.Tasks.Count;
			}

			InitializeAccess();
			LoadCurrentTask();
		}

		private void OnThemeChanged()
		{
			StyleHelper.ApplyFormStyle(this);
			ApplyModernStyles();
		}

		private void InitializeAccess()

		{
			if (_currentUser == null)
			{
				LoginButton.Visible = true;
				NextButton.Enabled = false;
				LoginButton.Text = "👤 Увійти";
				LoginButton.Enabled = true;
			}
			else
			{
				LoginButton.Visible = true;
				LoginButton.Text = "👤 Профіль";
				LoginButton.Enabled = true;
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
			
			// Виправляємо відображення переносів рядків та табуляції
			string formattedCode = task.CodeTemplate ?? "";
			formattedCode = formattedCode.Replace("\r\n", "\n").Replace("\n", "\r\n");
			CodeTextBox.Text = formattedCode;

			ResultTextBox.Clear();
			ResultTextBox.ForeColor = Theme.TextPrimary;
			ResultHeaderLabel.Text = "КОНСОЛЬ ВИВОДУ";

			CodeTextBox.ReadOnly = false;
			CodeTextBox.BackColor = Theme.CodeBackground;
			NextButton.Enabled = false;
			SkipButton.Enabled = true;

			NextButton.Text = currentTaskIndex == _quiz.Tasks.Count - 1 ? "Завершити 🏁" : "Наступне ➡";
			NextButton.BackColor = Theme.Border;
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
				CodeTextBox.ReadOnly = true;
				CodeTextBox.BackColor = Color.FromArgb(45, 45, 45); // Трохи світліший фон для "заблокованого" стану
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
			CodeTextBox.ReadOnly = true;
			CodeTextBox.BackColor = Color.FromArgb(45, 45, 45); // Трохи світліший фон
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
			if (!UserService.Instance.IsLoggedIn)
			{
				using (var loginView = new LoginView())
				{
					if (loginView.ShowDialog() == DialogResult.OK)
					{
						_currentUser = UserService.Instance.CurrentUser;
						InitializeAccess();
					}
				}
			}
			else
			{
				var profile = new ProfileView(UserService.Instance.CurrentUser);

				profile.LoggedOut += (s, args) =>
				{
					_currentUser = null;
					InitializeAccess();
				};

				profile.ShowDialog();
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