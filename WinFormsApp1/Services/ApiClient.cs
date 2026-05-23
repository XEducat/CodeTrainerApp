using System.Net;
using System.Text.Json;
using CodeTrainerApp.Model;

namespace CodeTrainerApp.Services
{
	public static class ApiClient
	{
		private static readonly CookieContainer _cookieContainer;
		private static readonly HttpClient _httpClient;

		// Папка для збереження файлів (LocalApplicationData\CodeTrainerApp)
		private static readonly string _storageDir =
			Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CodeTrainerApp");

		private static readonly string _cookiesFile = Path.Combine(_storageDir, "cookies.json");

		static ApiClient()
		{
			_cookieContainer = new CookieContainer();

			var handler = new HttpClientHandler
			{
				UseCookies = true,
				CookieContainer = _cookieContainer,
				ServerCertificateCustomValidationCallback =
					HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
			};

			_httpClient = new HttpClient(handler)
			{
				BaseAddress = new Uri("https://localhost:7205/")
			};

			// Спроба завантажити збережені cookie при ініціалізації
			try
			{
				LoadCookiesFromDisk();
			}
			catch
			{
				// Ігноруємо помилки читання/десеріалізації
			}
		}

		public static HttpClient Instance => _httpClient;

		public static void ClearCookies()
		{
			// Очищаємо cookies для нашого домену
			if (_httpClient.BaseAddress != null)
			{
				var cookies = _cookieContainer.GetCookies(_httpClient.BaseAddress);
				foreach (Cookie cookie in cookies)
				{
					cookie.Expired = true;
				}
			}

			// Видаляємо файл з cookie при очищенні
			try
			{
				if (File.Exists(_cookiesFile))
					File.Delete(_cookiesFile);
			}
			catch { }
		}

		// Серіалізуємо cookie у файл для відновлення між запусками
		public static void SaveCookiesToDisk()
		{
			try
			{
				Directory.CreateDirectory(_storageDir);

				if (_httpClient.BaseAddress == null) return;

				var cookies = _cookieContainer.GetCookies(_httpClient.BaseAddress);
				var list = new List<SerializableCookie>();

				foreach (Cookie c in cookies)
				{
					list.Add(new SerializableCookie
					{
						Name = c.Name,
						Value = c.Value,
						Domain = c.Domain,
						Path = c.Path,
						Expires = c.Expires == DateTime.MinValue ? (DateTime?)null : c.Expires,
						Secure = c.Secure,
						HttpOnly = c.HttpOnly
					});
				}

				var json = JsonSerializer.Serialize(list);
				File.WriteAllText(_cookiesFile, json);
			}
			catch
			{
				// При неуспіху нічого не кидаємо
			}
		}

		public static void LoadCookiesFromDisk()
		{
			if (!File.Exists(_cookiesFile))
				return;

			try
			{
				var json = File.ReadAllText(_cookiesFile);
				var list = JsonSerializer.Deserialize<List<SerializableCookie>>(json);

				if (list == null) return;

				foreach (var sc in list)
				{
					try
					{
						var cookie = new Cookie(sc.Name, sc.Value, sc.Path ?? "/", sc.Domain)
						{
							Secure = sc.Secure,
							HttpOnly = sc.HttpOnly
						};

						if (sc.Expires.HasValue)
							cookie.Expires = sc.Expires.Value;

						// Додаємо cookie для базової адреси
						_cookieContainer.Add(_httpClient.BaseAddress!, cookie);
					}
					catch
					{
						// Пропускаємо проблемні cookie
					}
				}
			}
			catch
			{
				// Ігноруємо помилки читання/десеріалізації
			}
		}
	}
}
