using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CodeTrainerApp.Model;

namespace CodeTrainerApp.Services
{
	public sealed class UserService
	{
		private static readonly Lazy<UserService> _instance =
			new Lazy<UserService>(() => new UserService());

		public static UserService Instance => _instance.Value;

		private readonly HttpClient _httpClient;

		public User CurrentUser { get; private set; }
		public bool IsLoggedIn => CurrentUser != null;

		private UserService()
		{
			_httpClient = ApiClient.Instance;
		}

		// ================= LOGIN =================
		public async Task<(bool success, string message)> LoginAsync(string identifier, string password)
		{
			if (string.IsNullOrWhiteSpace(identifier) || string.IsNullOrWhiteSpace(password))
				return (false, "Введіть логін/email і пароль");

			var content = new FormUrlEncodedContent(new[]
			{
				new KeyValuePair<string, string>("loginOrEmail", identifier),
				new KeyValuePair<string, string>("password", password)
			});

			var response = await _httpClient.PostAsync("api/auth/login", content);

			if (!response.IsSuccessStatusCode)
				return (false, await response.Content.ReadAsStringAsync());

			var json = await response.Content.ReadFromJsonAsync<JsonElement>();

			string id = json.GetProperty("id").GetString() ?? "";
			string email = json.GetProperty("email").GetString() ?? "";
			string login = json.GetProperty("login").GetString() ?? "";
			DateTime birthDate = json.GetProperty("birthDate").GetDateTime();
			string role = json.GetProperty("role").GetString() ?? "";

			CurrentUser = new User(id, email, login, birthDate, role);

			return (true, "Успішний вхід");
		}

		// ================= REGISTER =================
		public async Task<(bool success, string message)> RegisterAsync(
			string login,
			string email,
			string password,
			string repeatPassword,
			DateTime birthDate,
			string mentorCode = "")
		{
			if (string.IsNullOrWhiteSpace(login))
				return (false, "Введіть логін");

			if (!IsValidEmail(email))
				return (false, "Некоректний email");

			if (password.Length < 6)
				return (false, "Пароль мінімум 6 символів");

			if (password != repeatPassword)
				return (false, "Паролі не співпадають");

			var content = new FormUrlEncodedContent(new[]
			{
				new KeyValuePair<string, string>("email", email),
				new KeyValuePair<string, string>("password", password),
				new KeyValuePair<string, string>("name", login), // ВАЖЛИВО: name а не login
                new KeyValuePair<string, string>("birthDate", birthDate.ToString("yyyy-MM-dd")),
				new KeyValuePair<string, string>("mentorCode", mentorCode ?? "")
			});

			var response = await _httpClient.PostAsync("api/auth/register", content);

			if (!response.IsSuccessStatusCode)
				return (false, await response.Content.ReadAsStringAsync());

			return (true, "Успішна реєстрація");
		}

		// ================= LOGOUT =================
		public async Task LogoutAsync()
		{
			await _httpClient.PostAsync("api/auth/logout", null);

			ApiClient.ClearCookies();
			CurrentUser = null;
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