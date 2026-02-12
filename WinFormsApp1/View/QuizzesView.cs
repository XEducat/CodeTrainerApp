using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CodeTrainerApp.Model;
using CodeTrainerApp.View;

namespace CodeTrainerApp.View
{
	public partial class QuizzesView : Form
	{
		private List<Quiz> _quizzes = new List<Quiz>();
		private bool _isLoggedIn = false;
		private string _userEmail = "";
		private string _userRole = "";

		public QuizzesView()
		{
			InitializeComponent();
			LoadQuizzes();
		}

		private void LoadQuizzes()
		{
			_quizzes = new List<Quiz>
			{
				new Quiz
				{
					Id = 1,
					Title = "Базовий C#",
					Description = "Прості задачі для початківців",
					Tasks = ProgrammingTask.GetTasks()
				},
				new Quiz
				{
					Id = 2,
					Title = "Алгоритми",
					Description = "Задачі на логіку",
					Tasks = ProgrammingTask.GetTasks().GetRange(0, 3)
				}
			};

			QuizListBox.Items.Clear();

			foreach (var quiz in _quizzes)
			{
				QuizListBox.Items.Add(quiz.Title);
			}
		}

		private void QuizListBox_DoubleClick(object sender, EventArgs e)
		{
			StartQuiz();
		}

		private void StartQuizButton_Click(object sender, EventArgs e)
		{
			StartQuiz();
		}

		private void StartQuiz()
		{
			if (QuizListBox.SelectedIndex == -1)
			{
				MessageBox.Show("Оберіть тест зі списку.",
					"Помилка",
					MessageBoxButtons.OK,
					MessageBoxIcon.Warning);
				return;
			}

			var selectedQuiz = _quizzes[QuizListBox.SelectedIndex];

			MessageBox.Show($"Запуск тесту: {selectedQuiz.Title}",
				"Інформація",
				MessageBoxButtons.OK,
				MessageBoxIcon.Information);

			var mainView = new QuizView(selectedQuiz);
			mainView.ShowDialog();
		}

		private void LoginButton_Click(object sender, EventArgs e)
		{
			if (!_isLoggedIn)
			{
				using (var loginForm = new LoginView())
				{
					if (loginForm.ShowDialog() == DialogResult.OK)
					{
						_isLoggedIn = true;
						_userEmail = loginForm.LoggedEmail;
						_userRole = loginForm.UserRole;

						UserInfoLabel.Text = $"{_userEmail} ({_userRole})";
						LoginButton.Text = "Вийти";
					}
				}
			}
			else
			{
				_isLoggedIn = false;
				_userEmail = "";
				_userRole = "";

				UserInfoLabel.Text = "Гість";
				LoginButton.Text = "Увійти";
			}
		}

		private void BackButton_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
