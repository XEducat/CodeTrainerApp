using CodeTrainerApp.Model;
using CodeTrainerApp.Services;

namespace CodeTrainerApp.View
{
	public partial class QuizzesView : Form
	{
		private readonly QuizService _quizService = new QuizService();
		private List<Quiz> _quizzes = new List<Quiz>();
		private bool _isLoggedIn = false;
		private string _userEmail = "";
		private string _userRole = "";

		public QuizzesView()
		{
			InitializeComponent();
		}

		private async void QuizzesView_Load(object? sender, EventArgs e)
		{
			await LoadQuizzesAsync();
		}

		private async Task LoadQuizzesAsync()
		{
			try
			{
				LoginButton.Enabled = false;
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
				LoginButton.Enabled = true;
				QuizListBox.Enabled = true;
			}
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
