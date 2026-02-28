using System;
using System.Drawing;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using CodeTrainerApp.Model;

namespace CodeTrainerApp.View
{
	public partial class LoginView : Form
	{
		private readonly HttpClient _httpClient;

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

			var handler = new HttpClientHandler
			{
				ServerCertificateCustomValidationCallback =
					HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
			};

			_httpClient = new HttpClient(handler)
			{
				BaseAddress = new Uri("http://localhost:5181/")
			};

			MainButton.Click += MainAction_Click;
			SwitchModeButton.Click += SwitchModeButton_Click;
			MentorCheckBox.CheckedChanged += MentorCheckBox_CheckedChanged;

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
			string login = LoginTextBox.Text.Trim();
			string identifier = EmailTextBox.Text.Trim();
			string password = PasswordTextBox.Text;
			string repeatPassword = ConfirmPasswordTextBox.Text;

			if (_isRegisterMode)
			{
				await RegisterAsync(login, identifier, password, repeatPassword);
			}
			else
			{
				await LoginAsync(identifier, password);
			}
		}

		// ================= REGISTER =================

		private async Task RegisterAsync(string login, string email, string password, string repeatPassword)
		{
			if (string.IsNullOrWhiteSpace(login))
			{
				MessageBox.Show("Введіть логін");
				return;
			}

			if (!IsValidEmail(email))
			{
				MessageBox.Show("Некоректний email");
				return;
			}

			if (password.Length < 6)
			{
				MessageBox.Show("Пароль мінімум 6 символів");
				return;
			}

			if (password != repeatPassword)
			{
				MessageBox.Show("Паролі не співпадають");
				return;
			}

			DateTime birthDate = BirthDatePicker.Value.Date;
			string mentorCode = MentorCheckBox.Checked
				? MentorCodeTextBox.Text.Trim()
				: "";

			string registerUrl =
				$"api/auth/register?" +
				$"email={Uri.EscapeDataString(email)}" +
				$"&password={Uri.EscapeDataString(password)}" +
				$"&login={Uri.EscapeDataString(login)}" +
				$"&birthDate={birthDate:yyyy-MM-dd}" +
				$"&mentorCode={Uri.EscapeDataString(mentorCode)}";

			var response = await _httpClient.PostAsync(registerUrl, null);

			if (response.IsSuccessStatusCode)
			{
				MessageBox.Show("Реєстрація успішна");
				SetMode(false);
			}
			else
			{
				MessageBox.Show(await response.Content.ReadAsStringAsync());
			}
		}

		// ================= LOGIN =================

		private async Task LoginAsync(string identifier, string password)
		{
			if (string.IsNullOrWhiteSpace(identifier) || string.IsNullOrWhiteSpace(password))
			{
				MessageBox.Show("Введіть логін/email і пароль");
				return;
			}

			var response = await _httpClient.PostAsync(
				$"api/auth/login?loginOrEmail={Uri.EscapeDataString(identifier)}&password={Uri.EscapeDataString(password)}",
				null);

			if (!response.IsSuccessStatusCode)
			{
				MessageBox.Show("Невірний логін/email або пароль");
				return;
			}

			var json = await response.Content.ReadFromJsonAsync<JsonElement>();

			// Створюємо User через конструктор
			string id = json.TryGetProperty("id", out var idProp) ? idProp.GetString() ?? "" : "";
			string email = json.TryGetProperty("email", out var emailProp) ? emailProp.GetString() ?? "" : "";
			string login = json.TryGetProperty("login", out var loginProp) ? loginProp.GetString() ?? "" : "";
			DateTime birthDate = json.TryGetProperty("birthDate", out var bdProp) && bdProp.TryGetDateTime(out var dt) ? dt : DateTime.MinValue;
			string role = json.TryGetProperty("role", out var roleProp) ? roleProp.GetString() ?? "" : "";

			LoggedUser = new User(id, email, login, birthDate, role);

			IsLoggedIn = true;
			DialogResult = DialogResult.OK;
			Close();
		}

		// ================= HELPERS =================

		private bool IsValidEmail(string email)
		{
			return Regex.IsMatch(email,
				@"^[^@\s]+@[^@\s]+\.[^@\s]+$",
				RegexOptions.IgnoreCase);
		}
	}
}