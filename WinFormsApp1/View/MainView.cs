using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp1.Model;
using WinFormsApp1.Services;
using WinFormsApp1.View;

namespace CodeTrainerApp
{
	public partial class MainView : Form
	{
		private List<ProgrammingTask> tasks = new List<ProgrammingTask>();
		private int currentTaskIndex = 0;

		private QuizService _quizService;

		public MainView()
		{
			InitializeComponent();

			_quizService = new QuizService("https://localhost:5181//");

			CheckButton.Click += CheckButton_Click;
			NextButton.Click += NextButton_Click;
			SkipButton.Click += SkipButton_Click;

			LoginButton.Click += LoginButton_Click;

			// При завантаженні форми кнопки Check/Next блоковані
			CheckButton.Enabled = false;
			NextButton.Enabled = false;
		}

		private void LoadCurrentTask()
		{
			if (currentTaskIndex < tasks.Count)
			{
				var task = tasks[currentTaskIndex];

				CurrentTaskLabel.Text = $"Завдання {currentTaskIndex + 1} з {tasks.Count}: {task.Title}";
				TaskDescriptionLabel.Text = task.Description;
				CodeTextBox.Text = task.CodeTemplate.Replace("\n", "\r\n");

				ResultTextBox.Clear();
				CodeTextBox.Enabled = true;
				CheckButton.Enabled = true;
				NextButton.Enabled = false;
				ResultTextBox.BackColor = SystemColors.Window;

				NextButton.Text = currentTaskIndex == tasks.Count - 1 ? "Завершити" : "Далі >>";
			}
			else
			{
				CurrentTaskLabel.Text = "Вітаємо! Усі завдання виконано!";
				TaskDescriptionLabel.Text = "Ви успішно пройшли курс навчання. Можна закрити програму.";
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
			var currentTask = tasks[currentTaskIndex];

			ResultTextBox.Clear();
			if (currentTask.CodeTemplate == userCode)
			{
				ResultTextBox.Text = "Помилка компіляції, ви не виконували задачу\n";
				ResultTextBox.BackColor = Color.Yellow;
				return;
			}

			var result = await CodeCompiler.RunCode(currentTask, userCode);
			ResultTextBox.Text = result.output;

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
				SkipButton.BackColor = Color.LightGray;
				CheckButton.Enabled = false;
				CheckButton.BackColor = Color.LightGray;
			}
			else
			{
				ResultTextBox.Text = result.output;
				ResultTextBox.BackColor = Color.Yellow;
			}
		}

		// !!! НОВИЙ МЕТОД: Пропуск завдання !!!
		private void SkipButton_Click(object sender, EventArgs e)
		{
			ResultTextBox.Text = "Завдання пропущено. Можна переходити далі.";
			ResultTextBox.BackColor = Color.LightYellow;
			NextButton.Enabled = true; // Дозволяємо перехід вперед
			NextButton.BackColor = Color.FromArgb(13, 110, 253);
			CheckButton.Enabled = false; // Блокуємо перевірку
			CodeTextBox.Enabled = false;
		}

		private void NextButton_Click(object sender, EventArgs e)
		{
			currentTaskIndex++;
			LoadCurrentTask();
		}

		private void LoginButton_Click(object sender, EventArgs e)
		{
			using (var loginForm = new LoginView())
			{
				var result = loginForm.ShowDialog();
				if (result == DialogResult.OK && loginForm.IsLoggedIn)
				{
					MessageBox.Show($"Ви увійшли як {loginForm.LoggedEmail}", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);

					// Тепер можна активувати кнопки Check/Next
					CheckButton.Enabled = true;
					NextButton.Enabled = true;

					// Можна приховати кнопку Login
					LoginButton.Visible = false;
				}
			}
		}

		private async void MainView_Load(object sender, EventArgs e)
		{
			try
			{
				var quizzes = await _quizService.GetAllQuizzesAsync();
				if (quizzes.Count == 0)
				{
					MessageBox.Show("Квізи відсутні на сервері.", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}

				tasks = quizzes[0].Tasks; // перший квіз для прикладу
				currentTaskIndex = 0;
				LoadCurrentTask();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Помилка підвантаження квізів: " + ex.Message);
			}
		}
	}
}