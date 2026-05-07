using System.Drawing.Drawing2D;

namespace CodeTrainerApp.Views
{
	partial class LoginView
	{
		private System.ComponentModel.IContainer components = null;

		private System.Windows.Forms.Label LoginLabel;
		private System.Windows.Forms.TextBox LoginTextBox;

		private System.Windows.Forms.Label EmailLabel;
		private System.Windows.Forms.TextBox EmailTextBox;

		private System.Windows.Forms.Label PasswordLabel;
		private System.Windows.Forms.TextBox PasswordTextBox;
		private System.Windows.Forms.Button TogglePasswordButton;

		private System.Windows.Forms.Label ConfirmPasswordLabel;
		private System.Windows.Forms.TextBox ConfirmPasswordTextBox;
		private System.Windows.Forms.Button ToggleConfirmPasswordButton;

		private System.Windows.Forms.Label BirthDateLabel;
		private System.Windows.Forms.DateTimePicker BirthDatePicker;

		private System.Windows.Forms.CheckBox MentorCheckBox;
		private System.Windows.Forms.Label MentorCodeLabel;
		private System.Windows.Forms.TextBox MentorCodeTextBox;

		private System.Windows.Forms.Button MainButton;
		private System.Windows.Forms.Button SwitchModeButton;

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
				components.Dispose();

			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			Font labelFont = new Font("Segoe UI", 10F);
			Font inputFont = new Font("Segoe UI", 10F);
			Font buttonFont = new Font("Segoe UI", 10.5F);

			LoginLabel = new Label();
			LoginTextBox = new TextBox();

			EmailLabel = new Label();
			EmailTextBox = new TextBox();

			PasswordLabel = new Label();
			PasswordTextBox = new TextBox();
			TogglePasswordButton = new Button();

			ConfirmPasswordLabel = new Label();
			ConfirmPasswordTextBox = new TextBox();
			ToggleConfirmPasswordButton = new Button();

			BirthDateLabel = new Label();
			BirthDatePicker = new DateTimePicker();

			MentorCheckBox = new CheckBox();
			MentorCodeLabel = new Label();
			MentorCodeTextBox = new TextBox();

			MainButton = new Button();
			SwitchModeButton = new Button();

			SuspendLayout();

			// LoginLabel
			LoginLabel.Location = new Point(30, 20);
			LoginLabel.Size = new Size(270, 22);
			LoginLabel.Text = "Логін";
			LoginLabel.Font = labelFont;

			// LoginTextBox
			LoginTextBox.Location = new Point(30, 45);
			LoginTextBox.Size = new Size(270, 25);
			LoginTextBox.Font = inputFont;

			// EmailLabel
			EmailLabel.Location = new Point(30, 80);
			EmailLabel.Size = new Size(270, 22);
			EmailLabel.Text = "Email";
			EmailLabel.Font = labelFont;

			// EmailTextBox
			EmailTextBox.Location = new Point(30, 105);
			EmailTextBox.Size = new Size(270, 25);
			EmailTextBox.Font = inputFont;

			// PasswordLabel
			PasswordLabel.Location = new Point(30, 140);
			PasswordLabel.Size = new Size(270, 22);
			PasswordLabel.Text = "Пароль";
			PasswordLabel.Font = labelFont;

			// PasswordTextBox
			PasswordTextBox.Location = new Point(30, 165);
			PasswordTextBox.Size = new Size(240, 25);
			PasswordTextBox.UseSystemPasswordChar = true;
			PasswordTextBox.Font = inputFont;

			// TogglePasswordButton
			TogglePasswordButton.Location = new Point(275, 165);
			TogglePasswordButton.Size = new Size(25, 25);
			TogglePasswordButton.Text = "👁";
			TogglePasswordButton.FlatStyle = FlatStyle.Flat;
			TogglePasswordButton.FlatAppearance.BorderSize = 0;

			// ConfirmPasswordLabel
			ConfirmPasswordLabel.Location = new Point(30, 200);
			ConfirmPasswordLabel.Size = new Size(270, 22);
			ConfirmPasswordLabel.Text = "Повторіть пароль";
			ConfirmPasswordLabel.Font = labelFont;

			// ConfirmPasswordTextBox
			ConfirmPasswordTextBox.Location = new Point(30, 225);
			ConfirmPasswordTextBox.Size = new Size(240, 25);
			ConfirmPasswordTextBox.UseSystemPasswordChar = true;
			ConfirmPasswordTextBox.Font = inputFont;

			// ToggleConfirmPasswordButton
			ToggleConfirmPasswordButton.Location = new Point(275, 225);
			ToggleConfirmPasswordButton.Size = new Size(25, 25);
			ToggleConfirmPasswordButton.Text = "👁";
			ToggleConfirmPasswordButton.FlatStyle = FlatStyle.Flat;
			ToggleConfirmPasswordButton.FlatAppearance.BorderSize = 0;

			// BirthDateLabel
			BirthDateLabel.Location = new Point(30, 260);
			BirthDateLabel.Size = new Size(270, 22);
			BirthDateLabel.Text = "Дата народження";
			BirthDateLabel.Font = labelFont;

			// BirthDatePicker
			BirthDatePicker.Location = new Point(30, 285);
			BirthDatePicker.Size = new Size(270, 25);
			BirthDatePicker.Font = inputFont;

			// MentorCheckBox
			MentorCheckBox.Location = new Point(30, 320);
			MentorCheckBox.Text = "Я Mentor";
			MentorCheckBox.Font = inputFont;

			// MentorCodeLabel
			MentorCodeLabel.Location = new Point(30, 350);
			MentorCodeLabel.Size = new Size(270, 22);
			MentorCodeLabel.Text = "Код ментора";
			MentorCodeLabel.Font = labelFont;

			// MentorCodeTextBox
			MentorCodeTextBox.Location = new Point(30, 375);
			MentorCodeTextBox.Size = new Size(270, 25);
			MentorCodeTextBox.Font = inputFont;

			// MainButton
			MainButton.Location = new Point(30, 420);
			MainButton.Size = new Size(270, 45);
			MainButton.Text = "Увійти";
			MainButton.Font = buttonFont;
			MainButton.ForeColor = Color.White;
			MainButton.BackColor = Color.FromArgb(0, 120, 215);
			MainButton.FlatStyle = FlatStyle.Flat;
			MainButton.FlatAppearance.BorderSize = 0;
			MainButton.Cursor = Cursors.Hand;
			MainButton.Click += MainAction_Click;

			// SwitchModeButton
			SwitchModeButton.Location = new Point(30, 480);
			SwitchModeButton.Size = new Size(270, 35);
			SwitchModeButton.Text = "Зареєструватися";
			SwitchModeButton.Font = new Font("Segoe UI", 10F);
			SwitchModeButton.FlatStyle = FlatStyle.Flat;
			SwitchModeButton.FlatAppearance.BorderSize = 0;
			SwitchModeButton.ForeColor = Color.FromArgb(0, 120, 215);
			SwitchModeButton.Cursor = Cursors.Hand;

			// LoginView
			ClientSize = new Size(340, 540);
			BackColor = Color.White;

			Controls.Add(LoginLabel);
			Controls.Add(LoginTextBox);

			Controls.Add(EmailLabel);
			Controls.Add(EmailTextBox);

			Controls.Add(PasswordLabel);
			Controls.Add(PasswordTextBox);
			Controls.Add(TogglePasswordButton);

			Controls.Add(ConfirmPasswordLabel);
			Controls.Add(ConfirmPasswordTextBox);
			Controls.Add(ToggleConfirmPasswordButton);

			Controls.Add(BirthDateLabel);
			Controls.Add(BirthDatePicker);

			Controls.Add(MentorCheckBox);
			Controls.Add(MentorCodeLabel);
			Controls.Add(MentorCodeTextBox);

			Controls.Add(MainButton);
			Controls.Add(SwitchModeButton);

			FormBorderStyle = FormBorderStyle.FixedDialog;
			StartPosition = FormStartPosition.CenterScreen;
			Text = "CodeTrainer — Вхід";

			ResumeLayout(false);
			PerformLayout();
		}
	}
}