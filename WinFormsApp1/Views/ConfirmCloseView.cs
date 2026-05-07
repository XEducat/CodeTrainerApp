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

			ApplyStyles();
		}

		private void ApplyStyles()
		{
			mainPanel.BackColor = Theme.Surface;
			labelTitle.ForeColor = Theme.TextPrimary;
			labelMessage.ForeColor = Theme.TextSecondary;

			StyleHelper.ApplyPrimaryButton(btnNo);
			
			// Налаштовуємо кнопку "Вийти" (Danger style)
			btnYes.FlatStyle = FlatStyle.Flat;
			btnYes.FlatAppearance.BorderSize = 0;
			btnYes.BackColor = Color.FromArgb(254, 226, 226); // Light red
			btnYes.ForeColor = Color.FromArgb(220, 38, 38);   // Dark red
			btnYes.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			btnYes.Cursor = Cursors.Hand;

			// Малюємо рівномірну сіру рамку прямо на mainPanel
			mainPanel.Paint += (s, e) =>
			{
				// Виразна сіра рамка (2 пікселі) для кращого контрасту
				using (var pen = new Pen(Color.FromArgb(160, 160, 160), 2))
				{
					e.Graphics.DrawRectangle(pen, 1, 1, mainPanel.Width - 2, mainPanel.Height - 2);
				}
			};
		}
	}
}
