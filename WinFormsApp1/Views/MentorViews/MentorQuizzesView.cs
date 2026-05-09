using CodeTrainerApp.Model;
using CodeTrainerApp.Services;
using CodeTrainerApp.UI;

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
			_quizPanel.BackColor = Theme.Background;

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
				Width = _quizPanel.ClientSize.Width - 40,
				Height = 110,
				BackColor = Color.White,
				BorderStyle = BorderStyle.None,
				Padding = new Padding(15),
				Margin = new Padding(10),
			};

			panel.Paint += (s, e) =>
			{
				var rect = panel.ClientRectangle;
				rect.Width -= 1;
				rect.Height -= 1;
				using (var pen = new Pen(Theme.Border, 1))
				{
					e.Graphics.DrawRectangle(pen, rect);
				}
			};

			// Заголовок
			var lblTitle = new Label()
			{
				Text = quiz.Title,
				Font = new Font("Segoe UI", 12, FontStyle.Bold),
				ForeColor = Theme.TextPrimary,
				Location = new Point(15, 15),
				AutoSize = true
			};
			panel.Controls.Add(lblTitle);

			// Опис
			var lblDesc = new Label()
			{
				Text = quiz.Description,
				Font = new Font("Segoe UI", 9, FontStyle.Regular),
				ForeColor = Theme.TextSecondary,
				Location = new Point(15, 45),
				MaximumSize = new Size(panel.Width - 250, 40),
				AutoSize = true
			};
			panel.Controls.Add(lblDesc);

			// Бейдж
			var lblTasksCount = new Label()
			{
				Text = $"📑 {quiz.Tasks?.Count ?? 0} задач",
				Font = new Font("Segoe UI", 8, FontStyle.Bold),
				BackColor = Color.FromArgb(230, 240, 255),
				ForeColor = Theme.Primary,
				AutoSize = true,
				Padding = new Padding(5),
				Location = new Point(15, 75)
			};
			panel.Controls.Add(lblTasksCount);

			// Кнопка редагувати
			var btnEdit = new Button()
			{
				Text = "✏ Редагувати",
				Size = new Size(110, 32),
				Location = new Point(panel.Width - 240, 35)
			};
			StyleHelper.ApplyPrimaryButton(btnEdit);
			btnEdit.BackColor = Theme.Warning;
			btnEdit.Click += (s, e) => EditQuiz(quiz);
			panel.Controls.Add(btnEdit);

			// Кнопка видалити
			var btnDelete = new Button()
			{
				Text = "🗑 Видалити",
				Size = new Size(110, 32),
				Location = new Point(panel.Width - 120, 35)
			};
			StyleHelper.ApplyDangerButton(btnDelete);
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