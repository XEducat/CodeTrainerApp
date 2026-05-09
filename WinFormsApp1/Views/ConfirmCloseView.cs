using CodeTrainerApp.UI;

namespace CodeTrainerApp.Views
{
	public partial class ConfirmCloseView : Form
	{
		public ConfirmCloseView()
		{
			InitializeComponent();

			this.AcceptButton = btnNo; // За замовчуванням краще залишитися
			this.CancelButton = btnNo;
		}
	}
}
