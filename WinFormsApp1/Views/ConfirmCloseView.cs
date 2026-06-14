using CodeTrainerApp.UI;

namespace CodeTrainerApp.Views
{
	public partial class ConfirmCloseView : Form
	{
		public ConfirmCloseView(string title = "Завершити тест?", string message = "Ваш прогрес у цьому тесті не буде збережено. Ви дійсно бажаєте вийти?")
		{
			InitializeComponent();

			labelTitle.Text = title;
			labelMessage.Text = message;

			Theme.ThemeChanged += OnThemeChanged;
			this.Disposed += (s, e) => Theme.ThemeChanged -= OnThemeChanged;
			OnThemeChanged();

			this.AcceptButton = btnNo; // За замовчуванням краще залишитися
			this.CancelButton = btnNo;
		}

		private void OnThemeChanged()
		{
			StyleHelper.ApplyFormStyle(this);
			ApplyModernStyles();
		}
	}
}
