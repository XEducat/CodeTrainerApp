using System.Net.Http.Json;
using CodeTrainerApp.Model;

namespace CodeTrainerApp.Services
{
	public class QuizService
	{
		private readonly HttpClient _httpClient;
		private readonly string baseUrl = "http://localhost:5181/";

		public QuizService(bool ignoreSslErrors = true)
		{
			if (ignoreSslErrors)
			{
				var handler = new HttpClientHandler //TODO: remove in production
				{
					ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
				};

				_httpClient = new HttpClient(handler)
				{
					BaseAddress = new Uri("http://localhost:5181/")
				};
			}
			else
			{
				_httpClient = new HttpClient
				{
					BaseAddress = new Uri(baseUrl)
				};
			}
		}

		// ================= GET: всі квізи =================
		public async Task<List<Quiz>> GetAllQuizzesAsync()
		{
			try
			{
				var quizzes = await _httpClient.GetFromJsonAsync<List<Quiz>>("api/quiz");
				return quizzes ?? new List<Quiz>();
			}
			catch (Exception ex)
			{
				throw new Exception("Помилка при отриманні квізів: " + ex.Message);
			}
		}

		// ================= GET: всі квізи з таймаутом і повторними спробами =================
		public async Task<List<Quiz>> GetAllQuizzesAsync(int timeoutSeconds)
		{
			int delayBetweenRetriesMs = 500;
			int retryCount = 5;

			for (int attempt = 1; attempt <= retryCount; attempt++)
			{
				using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(timeoutSeconds));

				try
				{
					var response = await _httpClient.GetAsync("api/quiz", cts.Token);
					response.EnsureSuccessStatusCode();

					var quizzes = await response.Content
						.ReadFromJsonAsync<List<Quiz>>(cancellationToken: cts.Token);

					return quizzes ?? new List<Quiz>();
				}
				catch (TaskCanceledException)
				{
					if (attempt == retryCount)
						throw new Exception($"Сервер не відповідає. Таймаут {timeoutSeconds} сек.");

					await Task.Delay(delayBetweenRetriesMs);
				}
				catch (HttpRequestException)
				{
					if (attempt == retryCount)
						throw new Exception("Неможливо підключитися до сервера.");

					await Task.Delay(delayBetweenRetriesMs);
				}
				catch (Exception ex)
				{
					throw new Exception("Помилка при отриманні квізів: " + ex.Message);
				}
			}

			return new List<Quiz>();
		}

		// ================= GET: один квіз =================
		public async Task<Quiz?> GetQuizAsync(int id)
		{
			try
			{
				return await _httpClient.GetFromJsonAsync<Quiz>($"api/quiz/{id}");
			}
			catch (Exception ex)
			{
				throw new Exception("Помилка при отриманні квізу: " + ex.Message);
			}
		}

		// ================= POST: створити квіз =================
		public async Task<Quiz> CreateQuizAsync(Quiz quiz)
		{
			try
			{
				var response = await _httpClient.PostAsJsonAsync("api/quiz", quiz);
				response.EnsureSuccessStatusCode();
				var createdQuiz = await response.Content.ReadFromJsonAsync<Quiz>();
				return createdQuiz!;
			}
			catch (Exception ex)
			{
				throw new Exception("Помилка при створенні квізу: " + ex.Message);
			}
		}

		// ================= PUT: оновити квіз =================
		public async Task UpdateQuizAsync(int id, Quiz quiz)
		{
			try
			{
				var response = await _httpClient.PutAsJsonAsync($"api/quiz/{id}", quiz);
				response.EnsureSuccessStatusCode();
			}
			catch (Exception ex)
			{
				throw new Exception("Помилка при оновленні квізу: " + ex.Message);
			}
		}

		// ================= DELETE: видалити квіз =================
		public async Task DeleteQuizAsync(int id)
		{
			try
			{
				var response = await _httpClient.DeleteAsync($"api/quiz/{id}");
				response.EnsureSuccessStatusCode();
			}
			catch (Exception ex)
			{
				throw new Exception("Помилка при видаленні квізу: " + ex.Message);
			}
		}
	}
}
