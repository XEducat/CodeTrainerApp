using CodeTrainerApp.Model;
using CodeTrainerApp.Services;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace CodeTrainerApp.View
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
			InitializeDesign();

			TogglePasswordButton.Click += TogglePasswordButton_Click;
			ToggleConfirmPasswordButton.Click += ToggleConfirmPasswordButton_Click;

			MentorCheckBox.CheckedChanged += MentorCheckBox_CheckedChanged; // ← ОЦЕ ДОДАТИ

			MainButton.Click += MainAction_Click;
			SwitchModeButton.Click += SwitchModeButton_Click;

			// Спочатку поле коду приховане
			MentorCodeLabel.Visible = false;
			MentorCodeTextBox.Visible = false;

			SetMode(false);
		}

		// ================= UI DESIGN =================
		private void InitializeDesign()
		{
			BackColor = Color.FromArgb(245, 247, 250);

			MainButton.BackColor = Color.FromArgb(52, 120, 246);
			MainButton.ForeColor = Color.White;
			MainButton.FlatStyle = FlatStyle.Flat;
			MainButton.FlatAppearance.BorderSize = 0;

			SwitchModeButton.FlatStyle = FlatStyle.Flat;
			SwitchModeButton.FlatAppearance.BorderSize = 0;
			SwitchModeButton.ForeColor = Color.FromArgb(52, 120, 246);
			SwitchModeButton.BackColor = Color.Transparent;
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

			LoginLabel.Visible = register;
			LoginTextBox.Visible = register;

			ConfirmPasswordLabel.Visible = register;
			ConfirmPasswordTextBox.Visible = register;
			ToggleConfirmPasswordButton.Visible = register;

			BirthDateLabel.Visible = register;
			BirthDatePicker.Visible = register;

			MentorCheckBox.Visible = register;
			MentorCodeLabel.Visible = register && MentorCheckBox.Checked;
			MentorCodeTextBox.Visible = register && MentorCheckBox.Checked;

			TogglePasswordButton.Visible = true;
			PasswordTextBox.Visible = true;
			PasswordLabel.Visible = true;

			if (register)
			{
				Text = "Реєстрація";
				MainButton.Text = "Зареєструватися";
				SwitchModeButton.Text = "Назад до входу";
				EmailLabel.Text = "Email";

				ClientSize = new Size(340, 480);
				MainButton.Location = new Point(30, 380);
				SwitchModeButton.Location = new Point(30, 430);
			}
			else
			{
				Text = "Вхід";
				MainButton.Text = "Увійти";
				SwitchModeButton.Text = "Зареєструватися";
				EmailLabel.Text = "Логін або Email";

				ClientSize = new Size(340, 340);
				MainButton.Location = new Point(30, 200);
				SwitchModeButton.Location = new Point(30, 250);
			}
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
			string login = EmailTextBox.Text.Trim();
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
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Помилка");
			}
		}
	}
}