using CodeTrainerApp.Model;
using CodeTrainerApp.Services;

namespace CodeTrainerApp.View
{
	public partial class QuizzesView : Form
	{
		private readonly QuizService _quizService = new QuizService();
		private List<Quiz> _quizzes = new List<Quiz>();

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
				using var profile = new ProfileView(UserService.Instance.CurrentUser);

				// Підписуємося на подію закриття форми ProfileView
				profile.FormClosed += (s, args) =>
				{
					// Перевіряємо, чи користувач ще залогінений
					UpdateAuthUI();
				};

				profile.ShowDialog();
			}
		}

		private void UpdateAuthUI()
		{
			bool loggedIn = UserService.Instance.IsLoggedIn;
			ProfileButton.Text = loggedIn ? "👤 Профіль" : "🔐 Увійти";
			CabinetButton.Visible = loggedIn; // ← додаємо показ кнопки
			// CabinetButton.Enabled будет управляться после проверки доступности API в Load
		}

		// ================= CABINET BUTTON =================
		private void CabinetButton_Click(object sender, EventArgs e)
		{
			if (UserService.Instance.CurrentUser != null)
			{
				// Открываем CabinetView как немодальное окно и скрываем основную форму,
				// при закрытии кабинета показываем основную форму снова.
				var cabinet = new CabinetView(UserService.Instance.CurrentUser);
				cabinet.FormClosed += (s, args) =>
				{
					UpdateAuthUI(); // обновляем UI (например, после логаута)
					this.Show();
				};

				this.Hide();
				cabinet.Show();
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