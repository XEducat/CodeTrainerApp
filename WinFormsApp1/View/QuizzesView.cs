using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CodeTrainerApp.Model;

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
				QuizListBox.Items.Add(quiz);
			}
		}

		private void QuizListBox_DrawItem(object sender, DrawItemEventArgs e)
		{
			if (e.Index < 0) return;

			var quiz = (Quiz)QuizListBox.Items[e.Index];

			e.DrawBackground();

			bool isSelected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;

			Color bgColor = isSelected
				? Color.FromArgb(233, 236, 239)
				: Color.White;

			using (var bgBrush = new SolidBrush(bgColor))
			{
				e.Graphics.FillRectangle(bgBrush, e.Bounds);
			}

			// Назва
			using (var titleFont = new Font("Segoe UI", 12, FontStyle.Bold))
			using (var titleBrush = new SolidBrush(Color.FromArgb(33, 37, 41)))
			{
				e.Graphics.DrawString(
					quiz.Title,
					titleFont,
					titleBrush,
					e.Bounds.Left + 15,
					e.Bounds.Top + 10);
			}

			// Опис
			using (var descFont = new Font("Segoe UI", 9))
			using (var descBrush = new SolidBrush(Color.FromArgb(108, 117, 125)))
			{
				e.Graphics.DrawString(
					quiz.Description,
					descFont,
					descBrush,
					e.Bounds.Left + 15,
					e.Bounds.Top + 40);
			}

			// Кількість запитань справа
			string questionsText = $"Питань: {quiz.Tasks.Count}";

			using (var countFont = new Font("Segoe UI", 11, FontStyle.Bold))
			using (var countBrush = new SolidBrush(Color.FromArgb(40, 167, 69)))
			{
				SizeF textSize = e.Graphics.MeasureString(questionsText, countFont);

				float x = e.Bounds.Right - textSize.Width - 20;
				float y = e.Bounds.Top + (e.Bounds.Height - textSize.Height) / 2;

				e.Graphics.DrawString(
					questionsText,
					countFont,
					countBrush,
					x,
					y);
			}

			e.DrawFocusRectangle();
		}

		private void StartQuizButton_Click(object sender, EventArgs e)
		{
			StartQuiz();
		}

		private void QuizListBox_DoubleClick(object sender, EventArgs e)
		{
			StartQuiz();
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

						LoginButton.Text = "🚪 Вийти";
					}
				}
			}
			else
			{
				_isLoggedIn = false;
				_userEmail = "";
				_userRole = "";
				LoginButton.Text = "👤 Увійти";
			}
		}

		private void StartQuiz()
		{
			if (QuizListBox.SelectedIndex == -1)
			{
				MessageBox.Show(
					"Оберіть тест зі списку.",
					"Помилка",
					MessageBoxButtons.OK,
					MessageBoxIcon.Warning);

				return;
			}

			var selectedQuiz = (Quiz)QuizListBox.SelectedItem;
			var quizView = new QuizView(selectedQuiz);

			// Підписуємось на подію закриття
			quizView.FormClosed += (s, args) =>
			{
				this.Show();
			};

			this.Hide();
			quizView.Show();
		}
	}
}
