using CodeTrainerApp.Model;
using CodeTrainerApp.Services;

namespace CodeTrainerApp.Views.MentorViews
{
	public partial class MentorQuizzesView : Form
	{
		private readonly QuizService _quizService = new QuizService();
		private List<Quiz> _quizzes = new List<Quiz>();

		public MentorQuizzesView()
		{
			InitializeComponent();

			this.Load += async (s, e) =>
			{
				await LoadQuizzes();
			};
		}

		private async Task LoadQuizzes()
		{
			try
			{
				_quizzes = await _quizService.GetMyQuizzesAsync();
				RefreshQuizList();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Помилка");
			}
		}

		private void RefreshQuizList()
		{
			_quizPanel.Controls.Clear();

			foreach (var quiz in _quizzes)
			{
				var panel = CreateQuizPanel(quiz);
				_quizPanel.Controls.Add(panel);
			}
		}

		private Panel CreateQuizPanel(Quiz quiz)
		{
			var panel = new Panel()
			{
				Width = _quizPanel.ClientSize.Width - 30,
				Height = 100,
				BackColor = Color.White,
				BorderStyle = BorderStyle.None,
				Padding = new Padding(10),
				Margin = new Padding(5),
			};

			// Тінь/рамка
			panel.Paint += (s, e) =>
			{
				var rect = panel.ClientRectangle;
				rect.Inflate(-1, -1);
				e.Graphics.DrawRectangle(Pens.LightGray, rect);
			};

			// Заголовок
			var lblTitle = new Label()
			{
				Text = quiz.Title,
				Font = new Font("Segoe UI", 11, FontStyle.Bold),
				Location = new Point(10, 10),
				AutoSize = true
			};
			panel.Controls.Add(lblTitle);

			// Опис
			var lblDesc = new Label()
			{
				Text = quiz.Description,
				Font = new Font("Segoe UI", 9, FontStyle.Regular),
				Location = new Point(10, 35),
				MaximumSize = new Size(panel.Width - 150, 0), // перенос рядків
				AutoSize = true
			};
			panel.Controls.Add(lblDesc);

			// Бейдж із кількістю задач
			var lblTasksCount = new Label()
			{
				Text = $"{quiz.Tasks?.Count ?? 0} задач",
				Font = new Font("Segoe UI", 8, FontStyle.Regular),
				BackColor = Color.LightGray,
				ForeColor = Color.Black,
				AutoSize = true,
				Padding = new Padding(5),
				Location = new Point(10, 65)
			};
			panel.Controls.Add(lblTasksCount);

			// Кнопка редагувати
			var btnEdit = new Button()
			{
				Text = "Редагувати",
				Size = new Size(90, 30),
				BackColor = Color.Orange,
				ForeColor = Color.White,
				FlatStyle = FlatStyle.Flat,
				Location = new Point(panel.Width - 200, 35)
			};
			btnEdit.FlatAppearance.BorderSize = 0;
			btnEdit.Click += (s, e) => EditQuiz(quiz);
			panel.Controls.Add(btnEdit);

			// Кнопка видалити
			var btnDelete = new Button()
			{
				Text = "Видалити",
				Size = new Size(90, 30),
				BackColor = Color.Red,
				ForeColor = Color.White,
				FlatStyle = FlatStyle.Flat,
				Location = new Point(panel.Width - 100, 35)
			};
			btnDelete.FlatAppearance.BorderSize = 0;
			btnDelete.Click += (s, e) => DeleteQuiz(quiz);
			panel.Controls.Add(btnDelete);

			return panel;
		}

		// ================= Логіка CRUD =================
		private async void AddQuizButton_Click(object sender, EventArgs e)
		{
			var form = new CreateQuizForm();

			if (form.ShowDialog() == DialogResult.OK)
			{
				var quiz = form.CreatedQuiz;

				var (success, message, createdQuiz) =
					await _quizService.AddQuizAsync(quiz);

				if (success && createdQuiz != null)
				{
					createdQuiz.Tasks = quiz.Tasks;

					_quizzes.Add(createdQuiz);
					RefreshQuizList();
				}
			}
		}

		private async void EditQuiz(Quiz quiz)
		{
			var form = new CreateQuizForm(quiz);

			if (form.ShowDialog() == DialogResult.OK)
			{
				try
				{
					await _quizService.UpdateQuizAsync(quiz.Id, form.CreatedQuiz);

					RefreshQuizList();

					MessageBox.Show("Квіз оновлено");
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "Помилка");
				}
			}
		}

		private async void DeleteQuiz(Quiz quiz)
		{
			if (MessageBox.Show(
				$"Видалити квіз '{quiz.Title}'?",
				"Підтвердження",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Warning) == DialogResult.Yes)
			{
				try
				{
					await _quizService.DeleteQuizAsync(quiz.Id);
					_quizzes.Remove(quiz);
					RefreshQuizList();

					MessageBox.Show("Квіз успішно видалено");
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "Помилка");
				}
			}
		}
	}
}