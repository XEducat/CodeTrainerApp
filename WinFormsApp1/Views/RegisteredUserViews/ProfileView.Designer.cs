using System;
using System.Drawing;
using System.Windows.Forms;
using CodeTrainerApp.Model;

namespace CodeTrainerApp.Views.RegisteredUserViews
{
	public partial class ProfileView : Form
	{
		// Controls
		private TableLayoutPanel tableLayout;
		private Label lblTitle;

		private Label lblEmail;
		private Label lblLogin;
		private Label lblBirthDate;
		private Label lblRole;

		private Label lblEmailValue;
		private Label lblLoginValue;
		private Label lblBirthDateValue;
		private Label lblRoleValue;

		private Button btnLogout;

		private void InitializeComponent()
		{
			this.SuspendLayout();

			// ================= FORM =================
			this.Text = "Профіль";
			this.BackColor = Color.White;
			this.ClientSize = new Size(400, 475);      // компактний розмір
			this.FormBorderStyle = FormBorderStyle.FixedDialog; // забороняє змінювати розмір
			this.StartPosition = FormStartPosition.CenterScreen;

			// ================= TABLE =================
			tableLayout = new TableLayoutPanel
			{
				Dock = DockStyle.Fill,
				ColumnCount = 2,
				RowCount = 6,
				Padding = new Padding(20),
				GrowStyle = TableLayoutPanelGrowStyle.FixedSize
			};

			tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
			tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));

			// Рядки
			tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));   // Title
			tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));    // Login
			tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));    // Email
			tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));    // Role
			tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));    // BirthDate
			tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));   // Button

			// ================= TITLE =================
			lblTitle = new Label
			{
				Text = "Ваш профіль",
				Font = new Font("Segoe UI", 20F, FontStyle.Bold),
				Dock = DockStyle.Fill,
				TextAlign = ContentAlignment.MiddleCenter
			};
			tableLayout.Controls.Add(lblTitle, 0, 0);
			tableLayout.SetColumnSpan(lblTitle, 2);

			// ================= LABELS =================
			lblLogin = SetupLabel("Логін:");
			lblEmail = SetupLabel("Пошта:");
			lblRole = SetupLabel("Роль:");
			lblBirthDate = SetupLabel("Дата народження:");

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
				Font = new Font("Segoe UI", 12F, FontStyle.Bold),
				Dock = DockStyle.Fill,
				FlatStyle = FlatStyle.Flat,
				BackColor = Color.FromArgb(220, 53, 69),
				ForeColor = Color.White
			};
			btnLogout.FlatAppearance.BorderSize = 0;
			btnLogout.Click += btnLogout_Click;

			tableLayout.Controls.Add(btnLogout, 0, 5);
			tableLayout.SetColumnSpan(btnLogout, 2);

			this.Controls.Add(tableLayout);
			this.ResumeLayout(false);
		}

		private Label SetupLabel(string text)
		{
			return new Label
			{
				Text = text,
				Font = new Font("Segoe UI", 14F, FontStyle.Bold),
				Dock = DockStyle.Fill,
				TextAlign = ContentAlignment.MiddleCenter
			};
		}

		private Label SetupValueLabel()
		{
			return new Label
			{
				Font = new Font("Segoe UI", 14F),
				Dock = DockStyle.Fill,
				TextAlign = ContentAlignment.MiddleLeft
			};
		}
	}
}