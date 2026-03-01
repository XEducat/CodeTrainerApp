using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using CodeTrainerApp.Model;

namespace CodeTrainerApp.Services
{
	public class UserHistoryService
	{
		private readonly HttpClient _httpClient;

		public UserHistoryService()
		{
			_httpClient = ApiClient.Instance;
		}

		public async Task<List<QuizAttempt>> GetUserHistoryAsync()
		{
			var response = await _httpClient.GetAsync("api/UserHistory/my");

			if (!response.IsSuccessStatusCode)
			{
				var body = await response.Content.ReadAsStringAsync();
				throw new Exception($"Помилка: {(int)response.StatusCode} {response.StatusCode}. Body: {body}");
			}

			return await response.Content.ReadFromJsonAsync<List<QuizAttempt>>()
				   ?? new List<QuizAttempt>();
		}

		// Accept userId and include it in DTO sent to API.
		public async Task<QuizAttempt?> CreateHistoryAsync(QuizAttempt attempt, string userId)
		{
			// Prepare DTO with fields server expects from client (including UserId)
			var dto = new
			{
				UserId = userId,
				QuizId = attempt.QuizId,
				QuizTitle = attempt.QuizTitle,
				Score = attempt.Score,
				// Date is optional — server will set CompletedAt; include if you want
				Date = attempt.Date
			};

			var response = await _httpClient.PostAsJsonAsync("api/UserHistory/create", dto);

			if (!response.IsSuccessStatusCode)
			{
				var body = await response.Content.ReadAsStringAsync();
				throw new Exception($"Помилка: {(int)response.StatusCode} {response.StatusCode}. Body: {body}");
			}

			// Server returns saved UserHistory (Id, UserId, QuizId, MaxScore, Score, CompletedAt)
			var json = await response.Content.ReadFromJsonAsync<JsonElement>();

			try
			{
				var created = new QuizAttempt
				{
					Id = json.TryGetProperty("id", out var jId) && jId.ValueKind != JsonValueKind.Null ? jId.GetInt32() : 0,
					UserId = json.TryGetProperty("userId", out var jUser) && jUser.ValueKind != JsonValueKind.Null ? jUser.GetString() ?? "" : userId,
					QuizId = json.TryGetProperty("quizId", out var jQuiz) && jQuiz.ValueKind != JsonValueKind.Null ? jQuiz.GetInt32() : attempt.QuizId,
					QuizTitle = json.TryGetProperty("quizTitle", out var jTitle) && jTitle.ValueKind != JsonValueKind.Null ? jTitle.GetString() ?? attempt.QuizTitle : attempt.QuizTitle,
					Score = json.TryGetProperty("score", out var jScore) && jScore.ValueKind != JsonValueKind.Null ? jScore.GetInt32() : attempt.Score,
					Date = json.TryGetProperty("completedAt", out var jDate) && jDate.ValueKind != JsonValueKind.Null ? jDate.GetDateTime() : (json.TryGetProperty("date", out var jDate2) && jDate2.ValueKind != JsonValueKind.Null ? jDate2.GetDateTime() : attempt.Date)
				};

				return created;
			}
			catch (Exception ex)
			{
				var raw = await response.Content.ReadAsStringAsync();
				throw new Exception("Не вдалося розібрати відповідь сервера: " + ex.Message + ". Body: " + raw);
			}
		}

		public async Task<bool> DeleteHistoryAsync(int id)
		{
			var response = await _httpClient.DeleteAsync($"api/UserHistory/delete/{id}");
			return response.IsSuccessStatusCode;
		}
	}
}