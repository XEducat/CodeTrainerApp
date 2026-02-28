using System;
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
			var handler = new HttpClientHandler
			{
				ServerCertificateCustomValidationCallback =
					HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
			};

			_httpClient = new HttpClient(handler)
			{
				BaseAddress = new Uri("http://localhost:5181/") // встановлюємо базовий URL прямо тут
			};
		}

		// ================= LOGIN =================
		public async Task<bool> LoginAsync(string identifier, string password)
		{
			if (string.IsNullOrWhiteSpace(identifier) || string.IsNullOrWhiteSpace(password))
				throw new ArgumentException("Введіть логін/email і пароль");

			var response = await _httpClient.PostAsync(
				$"api/auth/login?loginOrEmail={Uri.EscapeDataString(identifier)}&password={Uri.EscapeDataString(password)}",
				null);

			if (!response.IsSuccessStatusCode)
				return false;

			var json = await response.Content.ReadFromJsonAsync<JsonElement>();

			string id = json.TryGetProperty("id", out var idProp) ? idProp.GetString() ?? "" : "";
			string email = json.TryGetProperty("email", out var emailProp) ? emailProp.GetString() ?? "" : "";
			string login = json.TryGetProperty("login", out var loginProp) ? loginProp.GetString() ?? "" : "";
			DateTime birthDate = json.TryGetProperty("birthDate", out var bdProp) && bdProp.TryGetDateTime(out var dt) ? dt : DateTime.MinValue;
			string role = json.TryGetProperty("role", out var roleProp) ? roleProp.GetString() ?? "" : "";

			CurrentUser = new User(id, email, login, birthDate, role);
			return true;
		}

		// ================= REGISTER =================
		public async Task<bool> RegisterAsync(string login, string email, string password, string repeatPassword,
			DateTime birthDate, string mentorCode = "")
		{
			if (string.IsNullOrWhiteSpace(login))
				throw new ArgumentException("Введіть логін");
			if (!IsValidEmail(email))
				throw new ArgumentException("Некоректний email");
			if (password.Length < 6)
				throw new ArgumentException("Пароль мінімум 6 символів");
			if (password != repeatPassword)
				throw new ArgumentException("Паролі не співпадають");

			string registerUrl =
				$"api/auth/register?" +
				$"email={Uri.EscapeDataString(email)}" +
				$"&password={Uri.EscapeDataString(password)}" +
				$"&login={Uri.EscapeDataString(login)}" +
				$"&birthDate={birthDate:yyyy-MM-dd}" +
				$"&mentorCode={Uri.EscapeDataString(mentorCode)}";

			var response = await _httpClient.PostAsync(registerUrl, null);
			return response.IsSuccessStatusCode;
		}

		// ================= LOGOUT =================
		public void Logout()
		{
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