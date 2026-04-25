using System;
using System.Drawing;
using System.Windows.Forms;
using CodeTrainerApp.UI;

namespace CodeTrainerApp.Views.RegisteredUserViews
{
	public partial class ProfileView : Form
	{
		private Panel cardPanel;
		private TableLayoutPanel tableLayout;

		private Label lblTitle;

		private Label lblLogin;
		private Label lblEmail;
		private Label lblRole;
		private Label lblBirthDate;

		private Label lblLoginValue;
		private Label lblEmailValue;
		private Label lblRoleValue;
		private Label lblBirthDateValue;

		private Button btnLogout;

		private void InitializeComponent()
		{
			this.SuspendLayout();

			// ================= FORM =================
			this.Text = "Профіль";
			this.BackColor = Theme.Background;
			this.ClientSize = new Size(540, 520);
			this.StartPosition = FormStartPosition.CenterScreen;
			this.FormBorderStyle = FormBorderStyle.FixedDialog;

			// ================= CARD =================
			cardPanel = new Panel
			{
				Size = new Size(460, 540),
				BackColor = Theme.Surface,
				Padding = new Padding(35),
				Location = new Point(40, 0)
			};

			// ================= TABLE =================
			tableLayout = new TableLayoutPanel
			{
				Dock = DockStyle.Fill,
				ColumnCount = 2,
				RowCount = 6
			};

			// Фіксована колонка для назв
			tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150));
			tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

			// Більше простору між рядками
			tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 80));
			tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 65));
			tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 65));
			tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 65));
			tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 65));
			tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 100));

			// ================= TITLE =================
			lblTitle = new Label
			{
				Text = "Профіль",
				Font = new Font("Segoe UI Semibold", 20F),
				ForeColor = Theme.TextPrimary,
				Dock = DockStyle.Fill,
				TextAlign = ContentAlignment.MiddleCenter
			};

			tableLayout.Controls.Add(lblTitle, 0, 0);
			tableLayout.SetColumnSpan(lblTitle, 2);

			// ================= LABELS =================
			lblLogin = SetupLabel("Логін");
			lblEmail = SetupLabel("Пошта");
			lblRole = SetupLabel("Роль");
			lblBirthDate = SetupLabel("Дата народж.");

			lblLoginValue = SetupValueLabel();
			lblEmailValue = SetupValueLabel();
			lblRoleValue = SetupValueLabel();
			lblBirthDateValue = SetupValueLabel();

			tableLayout.Controls.Add(lblLogin, 0, 1);
			tableLayout.Controls.Add(lblLoginValue, 1, 1);

			tableLayout.Controls.Add(lblEmail, 0, 2);
			tableLayout.Controls.Add(lblEmailValue, 1, 2);

			tableLayout.Controls.Add(lblRole, 0, 3);
			tableLayout.Controls.Add(lblRoleValue, 1, 3);

			tableLayout.Controls.Add(lblBirthDate, 0, 4);
			tableLayout.Controls.Add(lblBirthDateValue, 1, 4);

			// ================= BUTTON =================
			btnLogout = new Button
			{
				Text = "Вийти з акаунту",
				Height = 45,
				Dock = DockStyle.None,
				Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top,
				Margin = new Padding(0, 20, 0, 0)
			};

			StyleHelper.ApplyPrimaryButton(btnLogout);

			btnLogout.Click += btnLogout_Click;

			tableLayout.Controls.Add(btnLogout, 0, 5);
			tableLayout.SetColumnSpan(btnLogout, 2);

			// ================= BUILD =================
			cardPanel.Controls.Add(tableLayout);
			this.Controls.Add(cardPanel);

			this.ResumeLayout(false);
		}

		private Label SetupLabel(string text)
		{
			return new Label
			{
				Text = text,
				Font = new Font("Segoe UI Semibold", 12F),
				ForeColor = Theme.TextSecondary,
				Dock = DockStyle.Fill,
				TextAlign = ContentAlignment.MiddleLeft
			};
		}

		private Label SetupValueLabel()
		{
			return new Label
			{
				Font = new Font("Segoe UI", 12F),
				ForeColor = Theme.TextPrimary,
				Dock = DockStyle.Fill,
				TextAlign = ContentAlignment.MiddleLeft
			};
		}
	}
}