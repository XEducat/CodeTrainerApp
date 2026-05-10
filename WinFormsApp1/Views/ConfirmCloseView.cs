using CodeTrainerApp.UI;

namespace CodeTrainerApp.Views
{
	public partial class ConfirmCloseView : Form
	{
		public ConfirmCloseView()
		{
			InitializeComponent();

			Theme.ThemeChanged += OnThemeChanged;
			this.FormClosed += (s, e) => Theme.ThemeChanged -= OnThemeChanged;
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
