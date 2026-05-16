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

			Theme.ThemeChanged += OnThemeChanged;
			this.Disposed += (s, e) => Theme.ThemeChanged -= OnThemeChanged;
			OnThemeChanged();

			this.Load += async (s, e) =>
			{
				await LoadQuizzes();
			};
		}

		private void OnThemeChanged()
		{
			StyleHelper.ApplyFormStyle(this);
			ApplyModernStyles();
			RefreshQuizList();
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
				BackColor = Theme.Surface,
				BorderStyle = BorderStyle.None,
				Padding = new Padding(20),
				Margin = new Padding(0, 0, 0, 15),
			};

			panel.Paint += (s, e) =>
			{
				// Малюємо сучасну акцентну лінію зліва
				using (var stripeBrush = new SolidBrush(Theme.Primary))
				{
					e.Graphics.FillRectangle(stripeBrush, 0, 0, 6, panel.Height);
				}
				
				// Тонка роздільна лінія знизу для чистого Flat-дизайну
				using (var pen = new Pen(Theme.Border, 1))
				{
					e.Graphics.DrawLine(pen, 0, panel.Height - 1, panel.Width, panel.Height - 1);
				}
			};

			// Заголовок
			var lblTitle = new Label()
			{
				Text = quiz.Title,
				Font = new Font("Segoe UI Semibold", 13, FontStyle.Bold),
				ForeColor = Theme.TextPrimary,
				Location = new Point(25, 15),
				AutoSize = true
			};
			panel.Controls.Add(lblTitle);

			// Опис
			var lblDesc = new Label()
			{
				Text = quiz.Description,
				Font = new Font("Segoe UI", 9, FontStyle.Regular),
				ForeColor = Theme.TextSecondary,
				Location = new Point(25, 45),
				MaximumSize = new Size(panel.Width - 260, 40),
				AutoSize = true
			};
			panel.Controls.Add(lblDesc);

			// Бейдж
			var lblTasksCount = new Label()
			{
				Text = $"📑 {quiz.Tasks?.Count ?? 0} задач",
				Font = new Font("Segoe UI", 9, FontStyle.Bold),
				BackColor = Theme.BadgeBackground,
				ForeColor = Theme.Primary,
				AutoSize = true,
				Padding = new Padding(6),
				Location = new Point(25, 75)
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