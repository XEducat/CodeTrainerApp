using CodeTrainerApp.Views.MentorViews;
using CodeTrainerApp.Views.RegisteredUserViews;
using CodeTrainerApp.UI;

namespace CodeTrainerApp.Views.MentorViews
{
	public partial class CabinetContainerView : Form
	{
		private Form currentForm;
		private List<Button> menuButtons;

		public CabinetContainerView()
		{
			InitializeComponent();

			// Збираємо кнопки меню в список для зручності
			menuButtons = new List<Button> { btnCabinet, btnSecondView /*, додай інші кнопки */ };

			// Підписуємося на події
			btnCabinet.Click += (s, e) => { ActivateButton(btnCabinet); ShowForm(new UserHistoryView()); };
			btnSecondView.Click += (s, e) => { ActivateButton(btnSecondView); ShowForm(new MentorQuizzesView()); };
		}

		private void CabinetContainerView_Load(object sender, EventArgs e)
		{
			ActivateButton(btnCabinet); // підсвітимо першу кнопку
			ShowForm(new UserHistoryView());
		}

		// ================= ЛОГІКА ПЕРЕМИКАННЯ ФОРМ =================
		private void ShowForm(Form form)
		{
			if (currentForm != null)
				currentForm.Close();

			currentForm = form;
			form.TopLevel = false;
			form.FormBorderStyle = FormBorderStyle.None;
			form.Dock = DockStyle.Fill;

			contentPanel.Controls.Clear();
			contentPanel.Controls.Add(form);

			form.Show();
		}

		// ================= ЛОГІКА ПІДСВІТКИ КНОПКИ =================
		private void ActivateButton(Button button)
		{
			foreach (var btn in menuButtons)
			{
				if (btn == button)
				{
					btn.BackColor = Theme.Primary; 
					btn.ForeColor = Color.White;
					btn.FlatAppearance.MouseOverBackColor = Theme.PrimaryHover;
				}
				else
				{
					btn.BackColor = Color.Transparent;
					btn.ForeColor = Theme.TextSecondary;
					btn.FlatAppearance.MouseOverBackColor = Theme.MenuHover;
				}
			}
		}
	}
}