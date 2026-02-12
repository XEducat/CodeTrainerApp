namespace CodeTrainerApp.View
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

		private System.Windows.Forms.Label ConfirmPasswordLabel;
		private System.Windows.Forms.TextBox ConfirmPasswordTextBox;

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
			LoginLabel = new Label();
			LoginTextBox = new TextBox();
			EmailLabel = new Label();
			EmailTextBox = new TextBox();
			PasswordLabel = new Label();
			PasswordTextBox = new TextBox();
			ConfirmPasswordLabel = new Label();
			ConfirmPasswordTextBox = new TextBox();
			BirthDateLabel = new Label();
			BirthDatePicker = new DateTimePicker();
			MentorCheckBox = new CheckBox();
			MentorCodeLabel = new Label();
			MentorCodeTextBox = new TextBox();
			MainButton = new Button();
			SwitchModeButton = new Button();

			SuspendLayout();

			// ===== LOGIN =====
			LoginLabel.Text = "Логін";
			LoginLabel.Location = new Point(30, 20);
			LoginLabel.Size = new Size(270, 20);

			LoginTextBox.Location = new Point(30, 40);
			LoginTextBox.Size = new Size(270, 23);
			LoginTextBox.TabIndex = 0;

			// ===== EMAIL =====
			EmailLabel.Text = "Email";
			EmailLabel.Location = new Point(30, 75);
			EmailLabel.Size = new Size(270, 20);

			EmailTextBox.Location = new Point(30, 95);
			EmailTextBox.Size = new Size(270, 23);
			EmailTextBox.TabIndex = 1;

			// ===== PASSWORD =====
			PasswordLabel.Text = "Пароль";
			PasswordLabel.Location = new Point(30, 130);
			PasswordLabel.Size = new Size(270, 20);

			PasswordTextBox.Location = new Point(30, 150);
			PasswordTextBox.Size = new Size(270, 23);
			PasswordTextBox.PasswordChar = '*';
			PasswordTextBox.TabIndex = 2;

			// ===== CONFIRM PASSWORD =====
			ConfirmPasswordLabel.Text = "Повторіть пароль";
			ConfirmPasswordLabel.Location = new Point(30, 185);
			ConfirmPasswordLabel.Size = new Size(270, 20);

			ConfirmPasswordTextBox.Location = new Point(30, 205);
			ConfirmPasswordTextBox.Size = new Size(270, 23);
			ConfirmPasswordTextBox.PasswordChar = '*';
			ConfirmPasswordTextBox.TabIndex = 3;

			// ===== BIRTH DATE =====
			BirthDateLabel.Text = "Дата народження";
			BirthDateLabel.Location = new Point(30, 240);
			BirthDateLabel.Size = new Size(270, 20);

			BirthDatePicker.Location = new Point(30, 260);
			BirthDatePicker.Size = new Size(270, 23);
			BirthDatePicker.TabIndex = 4;

			// ===== MENTOR =====
			MentorCheckBox.Text = "Я Mentor";
			MentorCheckBox.Location = new Point(30, 295);
			MentorCheckBox.Size = new Size(120, 24);
			MentorCheckBox.TabIndex = 5;

			MentorCodeLabel.Text = "Код ментора";
			MentorCodeLabel.Location = new Point(30, 320);
			MentorCodeLabel.Size = new Size(270, 20);

			MentorCodeTextBox.Location = new Point(30, 340);
			MentorCodeTextBox.Size = new Size(270, 23);
			MentorCodeTextBox.TabIndex = 6;

			// ===== BUTTONS =====
			MainButton.Location = new Point(30, 380);
			MainButton.Size = new Size(270, 40);
			MainButton.TabIndex = 7;
			MainButton.Text = "Увійти";

			SwitchModeButton.Location = new Point(30, 430);
			SwitchModeButton.Size = new Size(270, 30);
			SwitchModeButton.TabIndex = 8;
			SwitchModeButton.Text = "Зареєструватися";

			// ===== FORM =====
			ClientSize = new Size(340, 480);
			Controls.AddRange(new Control[]
			{
				LoginLabel, LoginTextBox,
				EmailLabel, EmailTextBox,
				PasswordLabel, PasswordTextBox,
				ConfirmPasswordLabel, ConfirmPasswordTextBox,
				BirthDateLabel, BirthDatePicker,
				MentorCheckBox,
				MentorCodeLabel, MentorCodeTextBox,
				MainButton, SwitchModeButton
			});

			FormBorderStyle = FormBorderStyle.FixedDialog;
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Вхід";

			ResumeLayout(false);
			PerformLayout();
		}
	}
}
