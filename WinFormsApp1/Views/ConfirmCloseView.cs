namespace CodeTrainerApp.Views
{
	public partial class ConfirmCloseView : Form
	{
		public ConfirmCloseView()
		{
			InitializeComponent();

			// Кнопки "Так" та "Ні" як стандартні діалогові результати
			this.AcceptButton = btnYes;
			this.CancelButton = btnNo;
		}
	}
}
