using CodeTrainerApp.UI;

namespace CodeTrainerApp.Views
{
	public partial class QuizResultView : Form
	{
		public QuizResultView(int score, int maxScore)
		{
			InitializeComponent();
			
			ScoreLabel.Text = $"{score} / {maxScore}";
			
			double percentage = (double)score / maxScore;
			if (percentage >= 0.8)
			{
				ResultIconLabel.Text = "🏆";
				TitleLabel.Text = "Вітаємо!";
				MessageLabel.Text = "Чудовий результат! Ви справжній майстер! 🚀";
			}
			else if (percentage >= 0.5)
			{
				ResultIconLabel.Text = "🌟";
				TitleLabel.Text = "Гарна робота!";
				MessageLabel.Text = "Ви добре впоралися! Продовжуйте в тому ж дусі! 👍";
			}
			else
			{
				ResultIconLabel.Text = "📚";
				TitleLabel.Text = "Квіз завершено";
				MessageLabel.Text = "Непогано, але є куди рости. Тренуйтеся більше! 💪";
			}

			Theme.ThemeChanged += OnThemeChanged;
			this.Disposed += (s, e) => Theme.ThemeChanged -= OnThemeChanged;
			OnThemeChanged();
		}

		private void OnThemeChanged()
		{
			StyleHelper.ApplyFormStyle(this);
			ApplyModernStyles();
		}

		private void FinishButton_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
		}
	}
}