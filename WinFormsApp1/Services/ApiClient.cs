using System.Net;

namespace CodeTrainerApp.Services
{
	public static class ApiClient
	{
		private static readonly CookieContainer _cookieContainer;
		private static readonly HttpClient _httpClient;

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
		}

		public static HttpClient Instance => _httpClient;

		public static void ClearCookies()
		{
			_cookieContainer?.GetAllCookies()?.Clear();
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