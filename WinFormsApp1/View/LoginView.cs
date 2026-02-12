using System;
using System.Drawing;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.RegularExpressions;
using System.Text.Json;
using System.Windows.Forms;

namespace CodeTrainerApp.View
{
	public partial class LoginView : Form
	{
		private readonly HttpClient _httpClient;
		private bool _isRegisterMode = false;

		public bool IsLoggedIn { get; private set; }
		public string LoggedEmail { get; private set; }
		public string UserRole { get; private set; }

		public LoginView()
		{
			InitializeComponent();
			InitializeDesign();

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

		private void SetMode(bool register)
		{
			_isRegisterMode = register;

			LoginLabel.Visible = register;
			LoginTextBox.Visible = register;

			ConfirmPasswordLabel.Visible = register;
			ConfirmPasswordTextBox.Visible = register;

			BirthDateLabel.Visible = register;
			BirthDatePicker.Visible = register;

			MentorCheckBox.Visible = register;
			MentorCodeLabel.Visible = register && MentorCheckBox.Checked;
			MentorCodeTextBox.Visible = register && MentorCheckBox.Checked;

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

		private async void MainAction_Click(object sender, EventArgs e)
		{
			string login = LoginTextBox.Text.Trim();
			string identifier = EmailTextBox.Text.Trim();
			string password = PasswordTextBox.Text;
			string repeatPassword = ConfirmPasswordTextBox.Text;

			if (_isRegisterMode)
			{
				// ----- Реєстрація (без змін) -----

				if (string.IsNullOrWhiteSpace(login))
				{
					MessageBox.Show("Введіть логін");
					return;
				}

				if (string.IsNullOrWhiteSpace(identifier))
				{
					MessageBox.Show("Введіть email");
					return;
				}

				if (!IsValidEmail(identifier))
				{
					MessageBox.Show("Некоректний формат email");
					return;
				}

				if (password.Length < 6)
				{
					MessageBox.Show("Пароль має містити мінімум 6 символів");
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
					$"email={Uri.EscapeDataString(identifier)}" +
					$"&password={Uri.EscapeDataString(password)}" +
					$"&login={Uri.EscapeDataString(login)}" +
					$"&birthDate={birthDate:yyyy-MM-dd}" +
					$"&mentorCode={Uri.EscapeDataString(mentorCode)}";

				try
				{
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
				catch (Exception ex)
				{
					MessageBox.Show("Помилка підключення до API: " + ex.Message);
				}
			}
			else
			{
				// ===== НОВА ЛОГІКА ВАЛІДАЦІЇ =====

				if (string.IsNullOrWhiteSpace(identifier) &&
					string.IsNullOrWhiteSpace(password))
				{
					MessageBox.Show("Введіть логін/email і пароль");
					return;
				}

				if (!string.IsNullOrWhiteSpace(identifier) &&
					string.IsNullOrWhiteSpace(password))
				{
					MessageBox.Show("Введіть пароль");
					return;
				}

				if (string.IsNullOrWhiteSpace(identifier) &&
					!string.IsNullOrWhiteSpace(password))
				{
					MessageBox.Show("Введіть логін або email");
					return;
				}

				try
				{
					var response = await _httpClient.PostAsync(
						$"api/auth/login?identifier={Uri.EscapeDataString(identifier)}&password={Uri.EscapeDataString(password)}",
						null);

					if (response.IsSuccessStatusCode)
					{
						var json = await response.Content.ReadFromJsonAsync<JsonElement>();

						LoggedEmail = json.GetProperty("email").GetString();
						UserRole = json.GetProperty("role").GetString();

						IsLoggedIn = true;
						DialogResult = DialogResult.OK;
						Close();
					}
					else
					{
						MessageBox.Show("Невірний логін/email або пароль");
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("Помилка підключення до API: " + ex.Message);
				}
			}
		}

		private bool IsValidEmail(string email)
		{
			return Regex.IsMatch(email,
				@"^[^@\s]+@[^@\s]+\.[^@\s]+$",
				RegexOptions.IgnoreCase);
		}
	}
}
