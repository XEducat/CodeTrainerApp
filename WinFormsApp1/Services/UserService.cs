using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.IO;
using CodeTrainerApp.Model;

namespace CodeTrainerApp.Services
{
	public sealed class UserService
	{
		public static UserService Instance => _instance.Value;
		public User CurrentUser { get; private set; }
		public bool IsLoggedIn => CurrentUser != null;
 
		private readonly TimeSpan _localSessionLifetime = TimeSpan.FromDays(30); // Время жизни локально сохранённой сессии
		private readonly string _userFile;
		private readonly HttpClient _httpClient;
		private static readonly Lazy<UserService> _instance =
	new Lazy<UserService>(() => new UserService());
		private readonly string _storageDir = 
			Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CodeTrainerApp");


		private UserService()
		{
			_httpClient = ApiClient.Instance;
			_userFile = Path.Combine(_storageDir, "user.json");

			// Попытка відновлення сесії при створенні сінглтону
			try
			{
				LoadCurrentUserFromDisk();
				ApiClient.LoadCookiesFromDisk();
			}
			catch
			{
				// ігнорувати помилки відновлення
			}
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

			// Зберігаємо користувача та cookies для відновлення сесії
			SaveCurrentUserToDisk();
			ApiClient.SaveCookiesToDisk();

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

			// Видаляємо локально збереженого користувача
			try
			{
				if (File.Exists(_userFile))
					File.Delete(_userFile);
			}
			catch { }
		}

		// ================= HELPERS =================
		private bool IsValidEmail(string email)
		{
			return Regex.IsMatch(email,
				@"^[^@\s]+@[^@\s]+\.[^@\s]+$",
				RegexOptions.IgnoreCase);
		}

		// Wrapper для збереження разом з часовою міткою
		private class StoredUser
		{
			public User User { get; set; } = null!;
			public DateTime SavedAt { get; set; }
		}

		private void SaveCurrentUserToDisk()
		{
			try
			{
				Directory.CreateDirectory(_storageDir);

				var wrapper = new StoredUser
				{
					User = CurrentUser,
					SavedAt = DateTime.UtcNow
				};

				var json = JsonSerializer.Serialize(wrapper);
				File.WriteAllText(_userFile, json);
			}
			catch
			{
				// ігнорувати помилки запису
			}
		}

		private void LoadCurrentUserFromDisk()
		{
			try
			{
				if (!File.Exists(_userFile))
					return;

				var json = File.ReadAllText(_userFile);
				var wrapper = JsonSerializer.Deserialize<StoredUser>(json);

				if (wrapper == null || wrapper.User == null)
					return;

				// Перевіряємо вік збереженої сесії
				if (DateTime.UtcNow - wrapper.SavedAt > _localSessionLifetime)
				{
					// сесія застаріла — очищаємо дані з диска та cookie
					try { File.Delete(_userFile); } catch { }
					try { ApiClient.ClearCookies(); } catch { }
					CurrentUser = null;
					return;
				}

				// сесія ще дійсна — відновлюємо користувача
				CurrentUser = wrapper.User;
			}
			catch
			{
				// ігнорувати помилки читання/десеріалізації
			}
		}
	}
}