using CodeTrainerApp.Model;
using CodeTrainerApp.Services;
using CodeTrainerApp.UI;

namespace CodeTrainerApp.Views
{
	public partial class LoginView : Form
	{
		private bool _isRegisterMode = false;
		private bool _passwordVisible = false;
		private bool _confirmPasswordVisible = false;

		public bool IsLoggedIn { get; private set; }
		public User LoggedUser { get; private set; }

		public LoginView()
		{
			InitializeComponent();
			Theme.ThemeChanged += OnThemeChanged;
			this.FormClosed += (s, e) => Theme.ThemeChanged -= OnThemeChanged;
			OnThemeChanged();

			TogglePasswordButton.Click += TogglePasswordButton_Click;
			ToggleConfirmPasswordButton.Click += ToggleConfirmPasswordButton_Click;

			MentorCheckBox.CheckedChanged += MentorCheckBox_CheckedChanged; // ← ОЦЕ ДОДАТИ
			SwitchModeButton.Click += SwitchModeButton_Click;

			// Спочатку поле коду приховане
			MentorCodeLabel.Visible = false;
			MentorCodeTextBox.Visible = false;

			SetMode(false);
		}

		private void OnThemeChanged()
		{
			StyleHelper.ApplyFormStyle(this);
			ApplyModernStyles();
		}

		// ================= PASSWORD TOGGLE =================

		private void TogglePasswordButton_Click(object sender, EventArgs e)
		{
			_passwordVisible = !_passwordVisible;
			PasswordTextBox.UseSystemPasswordChar = !_passwordVisible;
			TogglePasswordButton.Text = _passwordVisible ? "🙈" : "👁";
		}

		private void ToggleConfirmPasswordButton_Click(object sender, EventArgs e)
		{
			_confirmPasswordVisible = !_confirmPasswordVisible;
			ConfirmPasswordTextBox.UseSystemPasswordChar = !_confirmPasswordVisible;
			ToggleConfirmPasswordButton.Text = _confirmPasswordVisible ? "🙈" : "👁";
		}

		// ================= MODE SWITCH =================

		private void SetMode(bool register)
		{
			_isRegisterMode = register;
			UpdateModeLayout(register);

			TogglePasswordButton.Visible = true;
			PasswordTextBox.Visible = true;
			PasswordLabel.Visible = true;
		}

		private void SwitchModeButton_Click(object sender, EventArgs e)
		{
			SetMode(!_isRegisterMode);
		}

		private void MentorCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			bool visible = MentorCheckBox.Checked;
			MentorCodeLabel.Visible = visible;
			MentorCodeTextBox.Visible = visible;
		}

		// ================= MAIN ACTION =================
		private async void MainAction_Click(object sender, EventArgs e)
		{
			string login = LoginTextBox.Text.Trim();
			string email = EmailTextBox.Text.Trim();
			string password = PasswordTextBox.Text;
			string repeatPassword = ConfirmPasswordTextBox.Text;
			DateTime birthDate = BirthDatePicker.Value.Date;
			string mentorCode = MentorCheckBox.Checked ? MentorCodeTextBox.Text.Trim() : "";

			try
			{
				if (_isRegisterMode)
				{
					var (success, message) = await UserService.Instance.RegisterAsync(
						login,
						email,
						password,
						repeatPassword,
						birthDate,
						mentorCode
					);

					MessageBox.Show(message);

					if (success) SetMode(false);
				}
				else
				{
					var (success, message) = await UserService.Instance.LoginAsync(
						email,
						password
					);

					if (success)
					{
						DialogResult = DialogResult.OK;
						Close();
					}
					else
					{
						MessageBox.Show(message, success ? "Успіх" : "Помилка");
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Помилка");
			}
		}
	}
}