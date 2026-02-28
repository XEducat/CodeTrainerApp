using CodeTrainerApp.Model;
using CodeTrainerApp.Services;
using CodeTrainerApp.View;
using System.Drawing;

namespace CodeTrainerApp
{
	public partial class QuizView : Form
	{
		private int currentTaskIndex = 0;
		private readonly Quiz _quiz;
		private User _currentUser;

		public QuizView(Quiz selectedQuiz, User user = null)
		{
			InitializeComponent();

			_quiz = selectedQuiz;
			_currentUser = user;

			CheckButton.Click += CheckButton_Click;
			NextButton.Click += NextButton_Click;
			SkipButton.Click += SkipButton_Click;
			LoginButton.Click += LoginButton_Click;

			CheckButton.Enabled = false;
			NextButton.Enabled = false;

			InitializeAccess();

			LoadCurrentTask();
		}

		// ================= ACCESS CONTROL =================

		private void InitializeAccess()
		{
			if (_currentUser == null)
			{
				LoginButton.Visible = true;
				CheckButton.Enabled = false;
				NextButton.Enabled = false;
			}
			else
			{
				LoginButton.Visible = false;
				CheckButton.Enabled = true;
			}
		}

		// ================= LOAD TASK =================

		private void LoadCurrentTask()
		{
			if (_quiz?.Tasks == null || _quiz.Tasks.Count == 0)
			{
				CurrentTaskLabel.Text = "У квізі відсутні завдання.";
				TaskDescriptionLabel.Text = "";
				CodeTextBox.Clear();
				CheckButton.Enabled = false;
				NextButton.Enabled = false;
				return;
			}

			if (currentTaskIndex >= _quiz.Tasks.Count)
			{
				CurrentTaskLabel.Text = "Вітаємо! Усі завдання виконано!";
				TaskDescriptionLabel.Text = "Ви успішно пройшли цей квіз.";
				CodeTextBox.Clear();
				CodeTextBox.Enabled = false;
				CheckButton.Enabled = false;
				NextButton.Enabled = false;
				return;
			}

			var task = _quiz.Tasks[currentTaskIndex];

			CurrentTaskLabel.Text =
				$"Завдання {currentTaskIndex + 1} з {_quiz.Tasks.Count}: {task.Title}";

			TaskDescriptionLabel.Text = task.Description;
			CodeTextBox.Text = task.CodeTemplate.Replace("\n", "\r\n");

			ResultTextBox.Clear();
			ResultTextBox.BackColor = SystemColors.Window;

			CodeTextBox.Enabled = true;
			CheckButton.Enabled = _currentUser != null;
			NextButton.Enabled = false;
			SkipButton.Enabled = true;

			NextButton.Text =
				currentTaskIndex == _quiz.Tasks.Count - 1 ? "Завершити" : "Далі >>";
		}

		// ================= CHECK =================

		private async void CheckButton_Click(object sender, EventArgs e)
		{
			if (_currentUser == null)
			{
				MessageBox.Show("Спочатку необхідно увійти.");
				return;
			}

			if (string.IsNullOrWhiteSpace(CodeTextBox.Text))
			{
				ResultTextBox.Text = "Поле для коду не може бути порожнім!";
				ResultTextBox.BackColor = Color.LightCoral;
				return;
			}

			var task = _quiz.Tasks[currentTaskIndex];
			string userCode = CodeTextBox.Text;

			if (task.CodeTemplate == userCode)
			{
				ResultTextBox.Text = "Ви не змінили шаблонний код.";
				ResultTextBox.BackColor = Color.Yellow;
				return;
			}

			ResultTextBox.Clear();

			var result = await CodeCompiler.RunCode(task, userCode);

			if (!string.IsNullOrEmpty(result.errorMessage))
			{
				ResultTextBox.Text = result.errorMessage;
				ResultTextBox.BackColor = Color.LightCoral;
				return;
			}

			if (result.success)
			{
				ResultTextBox.Text = result.output;
				ResultTextBox.BackColor = Color.LightGreen;

				NextButton.Enabled = true;
				CheckButton.Enabled = false;
				SkipButton.Enabled = false;
				CodeTextBox.Enabled = false;
			}
			else
			{
				ResultTextBox.Text = result.output;
				ResultTextBox.BackColor = Color.Yellow;
			}
		}

		// ================= SKIP =================

		private void SkipButton_Click(object sender, EventArgs e)
		{
			ResultTextBox.Text =
				"Завдання пропущено. Можна переходити далі.";
			ResultTextBox.BackColor = Color.LightYellow;

			NextButton.Enabled = true;
			CheckButton.Enabled = false;
			CodeTextBox.Enabled = false;
		}

		// ================= NEXT =================

		private void NextButton_Click(object sender, EventArgs e)
		{
			currentTaskIndex++;
			LoadCurrentTask();
		}

		// ================= LOGIN =================

		private void LoginButton_Click(object sender, EventArgs e)
		{
			using (var loginView = new LoginView())
			{
				if (loginView.ShowDialog() != DialogResult.OK)
					return;

				if (!loginView.IsLoggedIn)
					return;

				_currentUser = loginView.LoggedUser;

				MessageBox.Show(
					$"Вхід виконано як {_currentUser.Email}\nРоль: {_currentUser.Role}",
					"Успіх",
					MessageBoxButtons.OK,
					MessageBoxIcon.Information
				);

				InitializeAccess();
			}
		}

		// ================= CLOSE CONFIRM =================

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