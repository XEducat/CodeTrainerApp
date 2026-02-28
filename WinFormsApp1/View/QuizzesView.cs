using CodeTrainerApp.Model;
using CodeTrainerApp.Services;

namespace CodeTrainerApp.View
{
	public partial class QuizzesView : Form
	{
		private readonly QuizService _quizService = new QuizService();
		private List<Quiz> _quizzes = new List<Quiz>();

		private User _currentUser;   // ← тепер тільки один об'єкт
		private bool _isLoggedIn = false;

		public QuizzesView()
		{
			InitializeComponent();
		}

		private async void QuizzesView_Load(object? sender, EventArgs e)
		{
			UpdateAuthUI();
			await LoadQuizzesAsync();
		}

		// ================= LOAD QUIZZES =================
		private async Task LoadQuizzesAsync()
		{
			try
			{
				ProfileButton.Enabled = false;
				QuizListBox.Enabled = false;
				TitleLabel.Text = "Завантаження квізів...";

				_quizzes = await _quizService.GetAllQuizzesAsync(5);

				QuizListBox.Items.Clear();

				foreach (var quiz in _quizzes)
				{
					QuizListBox.Items.Add(quiz);
				}

				TitleLabel.Text = "Доступні квізи";
			}
			catch (Exception ex)
			{
				MessageBox.Show(
					ex.Message,
					"Помилка",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);

				TitleLabel.Text = "Помилка завантаження";
			}
			finally
			{
				ProfileButton.Enabled = true;
				QuizListBox.Enabled = true;
			}
		}

		// ================= PROFILE BUTTON =================
		private void ProfileButton_Click(object sender, EventArgs e)
		{
			if (!_isLoggedIn)
			{
				using (var login = new LoginView())
				{
					if (login.ShowDialog() == DialogResult.OK && login.LoggedUser != null)
					{
						var lr = login.LoggedUser; // LoginResponse
						_currentUser = new User(
							lr.Id,
							lr.Email,
							lr.Login,
							lr.BirthDate,
							lr.Role
						);

						_isLoggedIn = true;
						UpdateAuthUI();
					}
				}
			}
			else
			{
				using (var profile = new ProfileView(_currentUser))
				{
					profile.ShowDialog();
				}
			}
		}

		// ================= UPDATE UI =================
		private void UpdateAuthUI()
		{
			ProfileButton.Text = _isLoggedIn
				? $"👤 Профіль"
				: "🔐 Увійти";
		}

		// ================= QUIZ START =================
		private void QuizListBox_DoubleClick(object sender, EventArgs e)
		{
			StartQuiz();
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

			quizView.FormClosed += (s, args) => this.Show();

			this.Hide();
			quizView.Show();
		}
	}
}