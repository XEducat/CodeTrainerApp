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
			// 
			// LoginLabel
			// 
			LoginLabel.Location = new Point(30, 20);
			LoginLabel.Name = "LoginLabel";
			LoginLabel.Size = new Size(270, 20);
			LoginLabel.TabIndex = 0;
			LoginLabel.Text = "Логін";
			// 
			// LoginTextBox
			// 
			LoginTextBox.Location = new Point(30, 40);
			LoginTextBox.Name = "LoginTextBox";
			LoginTextBox.Size = new Size(270, 23);
			LoginTextBox.TabIndex = 1;
			// 
			// EmailLabel
			// 
			EmailLabel.Location = new Point(30, 75);
			EmailLabel.Name = "EmailLabel";
			EmailLabel.Size = new Size(270, 20);
			EmailLabel.TabIndex = 2;
			EmailLabel.Text = "Email";
			// 
			// EmailTextBox
			// 
			EmailTextBox.Location = new Point(30, 95);
			EmailTextBox.Name = "EmailTextBox";
			EmailTextBox.Size = new Size(270, 23);
			EmailTextBox.TabIndex = 3;
			// 
			// PasswordLabel
			// 
			PasswordLabel.Location = new Point(30, 130);
			PasswordLabel.Name = "PasswordLabel";
			PasswordLabel.Size = new Size(270, 20);
			PasswordLabel.TabIndex = 4;
			PasswordLabel.Text = "Пароль";
			// 
			// PasswordTextBox
			// 
			PasswordTextBox.Location = new Point(30, 150);
			PasswordTextBox.Name = "PasswordTextBox";
			PasswordTextBox.Size = new Size(240, 23);
			PasswordTextBox.TabIndex = 5;
			PasswordTextBox.UseSystemPasswordChar = true;
			// 
			// TogglePasswordButton
			// 
			TogglePasswordButton.Cursor = Cursors.Hand;
			TogglePasswordButton.FlatAppearance.BorderSize = 0;
			TogglePasswordButton.FlatStyle = FlatStyle.Flat;
			TogglePasswordButton.Location = new Point(275, 150);
			TogglePasswordButton.Name = "TogglePasswordButton";
			TogglePasswordButton.Size = new Size(25, 23);
			TogglePasswordButton.TabIndex = 6;
			TogglePasswordButton.Text = "👁";
			// 
			// ConfirmPasswordLabel
			// 
			ConfirmPasswordLabel.Location = new Point(30, 185);
			ConfirmPasswordLabel.Name = "ConfirmPasswordLabel";
			ConfirmPasswordLabel.Size = new Size(270, 20);
			ConfirmPasswordLabel.TabIndex = 7;
			ConfirmPasswordLabel.Text = "Повторіть пароль";
			// 
			// ConfirmPasswordTextBox
			// 
			ConfirmPasswordTextBox.Location = new Point(30, 205);
			ConfirmPasswordTextBox.Name = "ConfirmPasswordTextBox";
			ConfirmPasswordTextBox.Size = new Size(240, 23);
			ConfirmPasswordTextBox.TabIndex = 8;
			ConfirmPasswordTextBox.UseSystemPasswordChar = true;
			// 
			// ToggleConfirmPasswordButton
			// 
			ToggleConfirmPasswordButton.Cursor = Cursors.Hand;
			ToggleConfirmPasswordButton.FlatAppearance.BorderSize = 0;
			ToggleConfirmPasswordButton.FlatStyle = FlatStyle.Flat;
			ToggleConfirmPasswordButton.Location = new Point(275, 205);
			ToggleConfirmPasswordButton.Name = "ToggleConfirmPasswordButton";
			ToggleConfirmPasswordButton.Size = new Size(25, 23);
			ToggleConfirmPasswordButton.TabIndex = 9;
			ToggleConfirmPasswordButton.Text = "👁";
			// 
			// BirthDateLabel
			// 
			BirthDateLabel.Location = new Point(30, 240);
			BirthDateLabel.Name = "BirthDateLabel";
			BirthDateLabel.Size = new Size(270, 20);
			BirthDateLabel.TabIndex = 10;
			BirthDateLabel.Text = "Дата народження";
			// 
			// BirthDatePicker
			// 
			BirthDatePicker.Location = new Point(30, 260);
			BirthDatePicker.Name = "BirthDatePicker";
			BirthDatePicker.Size = new Size(270, 23);
			BirthDatePicker.TabIndex = 11;
			// 
			// MentorCheckBox
			// 
			MentorCheckBox.Location = new Point(30, 295);
			MentorCheckBox.Name = "MentorCheckBox";
			MentorCheckBox.Size = new Size(104, 24);
			MentorCheckBox.TabIndex = 12;
			MentorCheckBox.Text = "Я Mentor";
			// 
			// MentorCodeLabel
			// 
			MentorCodeLabel.Location = new Point(30, 320);
			MentorCodeLabel.Name = "MentorCodeLabel";
			MentorCodeLabel.Size = new Size(270, 20);
			MentorCodeLabel.TabIndex = 13;
			MentorCodeLabel.Text = "Код ментора";
			// 
			// MentorCodeTextBox
			// 
			MentorCodeTextBox.Location = new Point(30, 340);
			MentorCodeTextBox.Name = "MentorCodeTextBox";
			MentorCodeTextBox.Size = new Size(270, 23);
			MentorCodeTextBox.TabIndex = 14;
			// 
			// MainButton
			// 
			MainButton.Location = new Point(30, 380);
			MainButton.Name = "MainButton";
			MainButton.Size = new Size(270, 40);
			MainButton.TabIndex = 15;
			MainButton.Text = "Увійти";
			MainButton.Click += MainAction_Click;
			// 
			// SwitchModeButton
			// 
			SwitchModeButton.Location = new Point(30, 430);
			SwitchModeButton.Name = "SwitchModeButton";
			SwitchModeButton.Size = new Size(270, 30);
			SwitchModeButton.TabIndex = 16;
			SwitchModeButton.Text = "Зареєструватися";
			// 
			// LoginView
			// 
			ClientSize = new Size(340, 480);
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
			Name = "LoginView";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Вхід";
			ResumeLayout(false);
			PerformLayout();
		}
	}
}