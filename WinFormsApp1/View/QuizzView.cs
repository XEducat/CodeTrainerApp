using CodeTrainerApp.Model;
using CodeTrainerApp.Services;
using CodeTrainerApp.View;
using System.Drawing;

namespace CodeTrainerApp
{
	public partial class QuizView : Form
	{
		private int currentTaskIndex = 0;
		private Quiz quiz;

		public QuizView(Quiz selectedQuiz)
		{
			InitializeComponent();

			quiz = selectedQuiz;

			CheckButton.Click += CheckButton_Click;
			NextButton.Click += NextButton_Click;
			SkipButton.Click += SkipButton_Click;
			LoginButton.Click += LoginButton_Click;

			CheckButton.Enabled = false;
			NextButton.Enabled = false;

			currentTaskIndex = 0;
			LoadCurrentTask();
		}

		private void LoadCurrentTask()
		{
			if (quiz == null || quiz.Tasks == null || quiz.Tasks.Count == 0)
			{
				CurrentTaskLabel.Text = "У квізі відсутні завдання.";
				TaskDescriptionLabel.Text = "";
				CodeTextBox.Clear();
				CheckButton.Enabled = false;
				NextButton.Enabled = false;
				return;
			}

			if (currentTaskIndex < quiz.Tasks.Count)
			{
				var task = quiz.Tasks[currentTaskIndex];

				CurrentTaskLabel.Text =
					$"Завдання {currentTaskIndex + 1} з {quiz.Tasks.Count}: {task.Title}";

				TaskDescriptionLabel.Text = task.Description;
				CodeTextBox.Text = task.CodeTemplate.Replace("\n", "\r\n");

				ResultTextBox.Clear();
				CodeTextBox.Enabled = true;
				CheckButton.Enabled = true;
				NextButton.Enabled = false;
				ResultTextBox.BackColor = SystemColors.Window;

				NextButton.Text =
					currentTaskIndex == quiz.Tasks.Count - 1 ? "Завершити" : "Далі >>";
			}
			else
			{
				CurrentTaskLabel.Text = "Вітаємо! Усі завдання виконано!";
				TaskDescriptionLabel.Text = "Ви успішно пройшли цей квіз.";
				CodeTextBox.Clear();
				CodeTextBox.Enabled = false;
				CheckButton.Enabled = false;
				NextButton.Enabled = false;
			}
		}

		private async void CheckButton_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrWhiteSpace(CodeTextBox.Text))
			{
				ResultTextBox.Text = "Поле для коду не може бути порожнім!";
				ResultTextBox.BackColor = Color.LightCoral;
				return;
			}

			string userCode = CodeTextBox.Text;
			var currentTask = quiz.Tasks[currentTaskIndex];

			ResultTextBox.Clear();

			if (currentTask.CodeTemplate == userCode)
			{
				ResultTextBox.Text =
					"Помилка: ви не змінили шаблонний код.";
				ResultTextBox.BackColor = Color.Yellow;
				return;
			}

			var result = await CodeCompiler.RunCode(currentTask, userCode);

			if (!string.IsNullOrEmpty(result.errorMessage))
			{
				ResultTextBox.Text = result.errorMessage;
				ResultTextBox.BackColor = Color.LightCoral;
			}
			else if (result.success)
			{
				ResultTextBox.Text = result.output;
				ResultTextBox.BackColor = Color.LightGreen;

				NextButton.Enabled = true;
				NextButton.BackColor = Color.FromArgb(13, 110, 253);

				SkipButton.Enabled = false;
				CheckButton.Enabled = false;
				CodeTextBox.Enabled = false;
			}
			else
			{
				ResultTextBox.Text = result.output;
				ResultTextBox.BackColor = Color.Yellow;
			}
		}

		private void SkipButton_Click(object sender, EventArgs e)
		{
			ResultTextBox.Text =
				"Завдання пропущено. Можна переходити далі.";
			ResultTextBox.BackColor = Color.LightYellow;

			NextButton.Enabled = true;
			NextButton.BackColor = Color.FromArgb(13, 110, 253);

			CheckButton.Enabled = false;
			CodeTextBox.Enabled = false;
		}

		private void NextButton_Click(object sender, EventArgs e)
		{
			currentTaskIndex++;
			LoadCurrentTask();
		}

		private void LoginButton_Click(object sender, EventArgs e)
		{
			using (var loginView = new LoginView())
			{
				if (loginView.ShowDialog() != DialogResult.OK)
					return;

				if (!loginView.IsLoggedIn)
					return;

				MessageBox.Show(
					$"Ви увійшли як {loginView.LoggedEmail}\nРоль: {loginView.UserRole}",
					"Успіх",
					MessageBoxButtons.OK,
					MessageBoxIcon.Information
				);

				CheckButton.Enabled = true;
				NextButton.Enabled = true;

				LoginButton.Visible = false;
			}
		}
	}
}
