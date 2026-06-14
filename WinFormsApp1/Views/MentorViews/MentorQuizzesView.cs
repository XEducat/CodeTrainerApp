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

			_quizPanel.Resize += (s, e) => 
			{
				foreach (Control ctrl in _quizPanel.Controls)
				{
					ctrl.Width = _quizPanel.ClientSize.Width - 25;
				}
			};

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
				Width = _quizPanel.ClientSize.Width - 25,
				Height = 100,
				BackColor = Theme.Surface,
				BorderStyle = BorderStyle.None,
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

			// Контейнер для кнопок (праворуч)
			var actionsPanel = new Panel()
			{
				Dock = DockStyle.Right,
				Width = 375,
				BackColor = Color.Transparent
			};

			// Контейнер для тексту (ліворуч)
			var infoPanel = new FlowLayoutPanel()
			{
				Dock = DockStyle.Fill,
				FlowDirection = FlowDirection.TopDown,
				WrapContents = false,
				Padding = new Padding(25, 12, 10, 12),
				BackColor = Color.Transparent,
				AutoScroll = false
			};

			// Заголовок
			var lblTitle = new Label()
			{
				Text = quiz.Title,
				Font = new Font("Segoe UI Semibold", 13, FontStyle.Bold),
				ForeColor = Theme.TextPrimary,
				AutoSize = true,
				Margin = new Padding(0, 0, 0, 4)
			};
			infoPanel.Controls.Add(lblTitle);

			// Опис
			var lblDesc = new Label()
			{
				Text = quiz.Description,
				Font = new Font("Segoe UI", 9, FontStyle.Regular),
				ForeColor = Theme.TextSecondary,
				AutoSize = true,
				Margin = new Padding(0, 0, 0, 8)
			};
			infoPanel.Controls.Add(lblDesc);

			// Бейдж
			var lblTasksCount = new Label()
			{
				Text = $"📑 {quiz.Tasks?.Count ?? 0} задач",
				Font = new Font("Segoe UI", 9, FontStyle.Bold),
				BackColor = Theme.BadgeBackground,
				ForeColor = Theme.Primary,
				AutoSize = true,
				Padding = new Padding(6),
				Margin = new Padding(0)
			};
			infoPanel.Controls.Add(lblTasksCount);

			// Кнопки
			var btnStats = new Button() { Text = "📊 Статистика", Size = new Size(110, 40) };
			var btnEdit = new Button() { Text = "✏ Редагувати", Size = new Size(110, 40) };
			var btnDelete = new Button() { Text = "🗑 Видалити", Size = new Size(110, 40) };

			StyleHelper.ApplyPrimaryButton(btnStats);
			StyleHelper.ApplyWarningButton(btnEdit);
			StyleHelper.ApplyDangerButton(btnDelete);

			btnStats.Click += (s, e) => ShowStats(quiz);
			btnEdit.Click += (s, e) => EditQuiz(quiz);
			btnDelete.Click += (s, e) => DeleteQuiz(quiz);

			actionsPanel.Controls.Add(btnStats);
			actionsPanel.Controls.Add(btnEdit);
			actionsPanel.Controls.Add(btnDelete);

			// Додаємо в правильному порядку для Docking
			panel.Controls.Add(infoPanel);
			panel.Controls.Add(actionsPanel);

			// Логіка адаптивності
			void PerformLayout()
			{
				if (panel.IsDisposed) return;
				int maxWidth = panel.Width - actionsPanel.Width - 25;
				lblTitle.MaximumSize = new Size(maxWidth, 0);
				lblDesc.MaximumSize = new Size(maxWidth, 0);

				// Розрахунок висоти панелі на основі контенту
				int preferredHeight = infoPanel.GetPreferredSize(new Size(maxWidth, 0)).Height + 20;
				panel.Height = Math.Max(100, preferredHeight);

				// Центрування кнопок (висота кнопок тепер 40)
				int btnY = (panel.Height - 40) / 2;
				btnStats.Location = new Point(0, btnY);
				btnEdit.Location = new Point(120, btnY);
				btnDelete.Location = new Point(240, btnY);
			}

			panel.Resize += (s, e) => PerformLayout();
			panel.HandleCreated += (s, e) => PerformLayout();

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
			if (IsQuizActive(quiz.Id))
			{
				MessageBox.Show("Неможливо редагувати квіз, який зараз проходить користувач!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

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
			if (IsQuizActive(quiz.Id))
			{
				MessageBox.Show("Неможливо видалити квіз, який зараз проходить користувач!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

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

		private void ShowStats(Quiz quiz)
		{
			// Знаходимо батьківський контейнер (CabinetContainerView), щоб відкрити форму в ньому
			var parent = this.ParentForm as CabinetContainerView;
			if (parent != null)
			{
				parent.OpenStatsForQuiz(quiz.Id);
			}
		}

		private bool IsQuizActive(int quizId)
		{
			// Перевіряємо всі відкриті форми у застосунку
			foreach (Form form in Application.OpenForms)
			{
				if (form is QuizView qv && qv.CurrentQuizId == quizId)
				{
					return true;
				}
			}
			return false;
		}
	}
}