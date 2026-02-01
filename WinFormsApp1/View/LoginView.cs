using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net.Http.Json;

namespace WinFormsApp1.View
{
	public partial class LoginView : Form
	{
		private readonly HttpClient _httpClient;

		public bool IsLoggedIn { get; private set; } = false;
		public string LoggedEmail { get; private set; }

		public LoginView()
		{
			InitializeComponent();

			var handler = new HttpClientHandler //TODO: remove in production
			{
				ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
			};

			_httpClient = new HttpClient(handler)
			{
				BaseAddress = new Uri("http://localhost:5181/")
			};

			LoginButton.Click += LoginButton_Click;
			RegisterButton.Click += RegisterButton_Click;
		}

		// ===== LOGIN =====
		private async void LoginButton_Click(object sender, EventArgs e)
		{
			string email = EmailTextBox.Text;
			string password = PasswordTextBox.Text;

			try
			{
				var response = await _httpClient.PostAsync(
					$"api/auth/login?email={Uri.EscapeDataString(email)}&password={Uri.EscapeDataString(password)}",
					null // тіло пусте
				);

				if (response.IsSuccessStatusCode)
				{
					IsLoggedIn = true;
					LoggedEmail = email;
					this.DialogResult = DialogResult.OK;
					this.Close();
				}
				else
				{
					MessageBox.Show("Невірний email або пароль", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Помилка з'єднання з сервером: " + ex.Message);
			}
		}

		// ===== REGISTER =====
		private async void RegisterButton_Click(object sender, EventArgs e)
		{
			string email = EmailTextBox.Text;
			string password = PasswordTextBox.Text;
			var role = RoleComboBox.SelectedItem?.ToString() ?? "Student";

			try
			{
				var response = await _httpClient.PostAsync(
					$"api/auth/register?email={Uri.EscapeDataString(email)}&password={Uri.EscapeDataString(password)}&role={role}",
					null // тіло пусте
				);

				if (response.IsSuccessStatusCode)
				{
					MessageBox.Show("Реєстрація успішна! Можете увійти.", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				else
				{
					var error = await response.Content.ReadAsStringAsync();
					MessageBox.Show("Не вдалося зареєструвати користувача:\n" + error, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Помилка з'єднання з сервером: " + ex.Message);
			}
		}
	}
}
