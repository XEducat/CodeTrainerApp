using CodeTrainerApp.Model;
using CodeTrainerApp.Services;
using CodeTrainerApp.Views.MentorViews;
using CodeTrainerApp.Views.RegisteredUserViews;

namespace CodeTrainerApp.Views
{
	public partial class MainView : Form
	{
		private readonly QuizService _quizService = new QuizService();
		private List<Quiz> _quizzes = new List<Quiz>();

		public MainView()
		{
			InitializeComponent();
		}

		private async void QuizzesView_Load(object? sender, EventArgs e)
		{
			UpdateAuthUI();

			await LoadQuizzesAsync();
		}

		// ================= PROFILE BUTTON =================
		private void ProfileButton_Click(object sender, EventArgs e)
		{
			if (!UserService.Instance.IsLoggedIn)
			{
				using var login = new LoginView();
				if (login.ShowDialog() == DialogResult.OK)
					UpdateAuthUI();
			}
			else
			{
				var profile = new ProfileView(UserService.Instance.CurrentUser);

				// Подписываемся на событие выхода — обновляем UI сразу при logout
				profile.LoggedOut += (s, args) =>
				{
					UpdateAuthUI();
				};

				profile.ShowDialog();
			}
		}

		private void UpdateAuthUI()
		{
			bool loggedIn = UserService.Instance.IsLoggedIn;
			ProfileButton.Text = loggedIn ? "👤 Профіль" : "🔐 Увійти";
			CabinetButton.Visible = loggedIn;
		}

		// ================= CABINET BUTTON =================
		private void CabinetButton_Click(object sender, EventArgs e)
		{
			var currentUser = UserService.Instance.CurrentUser;
			if (currentUser == null)
				return;

			Form cabinetForm;

			if (currentUser.Role == "Mentor")
				cabinetForm = new CabinetContainerView();
			else
				cabinetForm = new UserHistoryView();

			// Підписка на закриття форми
			cabinetForm.FormClosed += async (s, args) =>
			{
				this.Show();
				await LoadQuizzesAsync(); // Підвантажуємо список тестів після закриття
			};

			this.Hide();
			cabinetForm.Show();
		}

		// ================= LOAD QUIZZES =================
		private async Task LoadQuizzesAsync()
		{
			try
			{
				ProfileButton.Enabled = false;
				CabinetButton.Enabled = false;
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
				CabinetButton.Enabled = true;
				QuizListBox.Enabled = true;
			}
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