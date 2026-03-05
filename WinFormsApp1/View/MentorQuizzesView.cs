using CodeTrainerApp.Model;
using CodeTrainerApp.Services;

namespace CodeTrainerApp.View
{
	public partial class MentorQuizzesView : Form
	{
		private readonly QuizService _quizService = new QuizService();
		private List<Quiz> _quizzes = new List<Quiz>();
		private FlowLayoutPanel _quizPanel;
		private Button _addQuizButton;

		public MentorQuizzesView()
		{
			InitializeComponent();
			InitializeUI();

			this.Load += async (s, e) =>
			{
				await LoadQuizzes();
			};
		}

		private void InitializeUI()
		{
			this.Text = "Квізи ментора";
			this.ClientSize = new Size(800, 600);

			_quizPanel = new FlowLayoutPanel()
			{
				Dock = DockStyle.Fill,
				AutoScroll = true,
				FlowDirection = FlowDirection.TopDown,
				WrapContents = false,
				Padding = new Padding(10)
			};
			this.Controls.Add(_quizPanel);

			_addQuizButton = new Button()
			{
				Text = "Додати квіз",
				Height = 40,
				Dock = DockStyle.Top,
				BackColor = Color.DodgerBlue,
				ForeColor = Color.White
			};
			_addQuizButton.Click += AddQuizButton_Click;
			this.Controls.Add(_addQuizButton);

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
				Height = 80,
				BorderStyle = BorderStyle.FixedSingle,
				Margin = new Padding(5)
			};

			var lblTitle = new Label()
			{
				Text = quiz.Title,
				Font = new Font("Segoe UI", 10, FontStyle.Bold),
				Location = new Point(10, 10),
				AutoSize = true
			};
			panel.Controls.Add(lblTitle);

			var lblDesc = new Label()
			{
				Text = quiz.Description,
				Font = new Font("Segoe UI", 9, FontStyle.Regular),
				Location = new Point(10, 35),
				AutoSize = true
			};
			panel.Controls.Add(lblDesc);

			var btnEdit = new Button()
			{
				Text = "Змінити",
				Size = new Size(80, 25),
				Location = new Point(panel.Width - 180, 25),
				BackColor = Color.Orange,
				ForeColor = Color.White
			};
			btnEdit.Click += (s, e) => EditQuiz(quiz);
			panel.Controls.Add(btnEdit);

			var btnDelete = new Button()
			{
				Text = "Видалити",
				Size = new Size(80, 25),
				Location = new Point(panel.Width - 90, 25),
				BackColor = Color.Red,
				ForeColor = Color.White
			};
			btnDelete.Click += (s, e) => DeleteQuiz(quiz);
			panel.Controls.Add(btnDelete);

			return panel;
		}

		// ================= Логіка CRUD =================
		private async void AddQuizButton_Click(object sender, EventArgs e)
		{
			try
			{
				string title = Microsoft.VisualBasic.Interaction.InputBox(
					"Введіть назву квізу:",
					"Новий квіз",
					"Новий квіз");

				if (string.IsNullOrWhiteSpace(title))
					return;

				string description = Microsoft.VisualBasic.Interaction.InputBox(
					"Введіть опис квізу:",
					"Опис квізу",
					"");

				var (success, message, quiz) = await _quizService.AddQuizAsync(title, description);

				MessageBox.Show(message);

				if (success && quiz != null)
				{
					_quizzes.Add(quiz);
					RefreshQuizList();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Помилка");
			}
		}

		private void EditQuiz(Quiz quiz)
		{
			// Можна відкрити модальне вікно для редагування
			var inputTitle = Microsoft.VisualBasic.Interaction.InputBox(
				"Назва квізу:", "Редагувати квіз", quiz.Title);
			if (!string.IsNullOrWhiteSpace(inputTitle))
			{
				quiz.Title = inputTitle;
				RefreshQuizList();
			}
		}

		private void DeleteQuiz(Quiz quiz)
		{
			if (MessageBox.Show($"Видалити квіз '{quiz.Title}'?", "Підтвердження",
				MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
			{
				_quizzes.Remove(quiz);
				RefreshQuizList();
			}
		}
	}
}