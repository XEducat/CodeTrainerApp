using System.Net;
using System.IO;
using System.Text.Json;

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

			// Попытка загрузить сохранённые cookie при инициализации
			try
			{
				LoadCookiesFromDisk();
			}
			catch
			{
				// Игнорируем ошибки чтения/десериализации
			}
		}

		public static HttpClient Instance => _httpClient;

		public static void ClearCookies()
		{
			_cookieContainer?.GetAllCookies()?.Clear();

			// удалить файл с cookie при очистке
			try
			{
				if (File.Exists(_cookiesFile))
					File.Delete(_cookiesFile);
			}
			catch { }
		}

		// Сериализуем cookie в файл для восстановления между запусками
		public static void SaveCookiesToDisk()
		{
			try
			{
				Directory.CreateDirectory(_storageDir);

				var cookies = _cookieContainer.GetAllCookies();
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
				// При неуспіху нічого не кидаємо — можна логувати при потребі
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

						// Добавляем cookie для базового адреса
						_cookieContainer.Add(_httpClient.BaseAddress!, cookie);
					}
					catch
					{
						// Пропускаем проблемные cookie
					}
				}
			}
			catch
			{
				// Игнорируем ошибки чтения/десериализации
			}
		}

		private class SerializableCookie
		{
			public string Name { get; set; } = string.Empty;
			public string Value { get; set; } = string.Empty;
			public string Domain { get; set; } = string.Empty;
			public string? Path { get; set; }
			public DateTime? Expires { get; set; }
			public bool Secure { get; set; }
			public bool HttpOnly { get; set; }
		}
	}

	// Додаємо helper для очищення cookie
	public static class CookieExtensions
	{
		public static CookieCollection GetAllCookies(this CookieContainer container)
		{
			var cookies = new CookieCollection();
			var table = (System.Collections.Hashtable)container.GetType()
				.InvokeMember("m_domainTable",
					System.Reflection.BindingFlags.NonPublic |
					System.Reflection.BindingFlags.GetField |
					System.Reflection.BindingFlags.Instance,
					null,
					container,
					new object[] { });

			foreach (var key in table.Keys)
			{
				var item = table[key];
				var items = (System.Collections.Hashtable)item.GetType()
					.InvokeMember("m_list",
						System.Reflection.BindingFlags.NonPublic |
						System.Reflection.BindingFlags.GetField |
						System.Reflection.BindingFlags.Instance,
						null,
						item,
						new object[] { });

				foreach (var col in items.Values)
					cookies.Add((CookieCollection)col);
			}

			return cookies;
		}
	}
}