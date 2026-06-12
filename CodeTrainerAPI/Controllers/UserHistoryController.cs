using CodeTrainerAPI.Data;
using CodeTrainerAPI.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using CodeTrainerAPI.DTO;

namespace CodeTrainerAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[Authorize]
	public class UserHistoryController : ControllerBase
	{
		private readonly AppDbContext _context;
		private readonly ILogger<UserHistoryController> _logger;

		public UserHistoryController(AppDbContext context, ILogger<UserHistoryController> logger)
		{
			_context = context;
			_logger = logger;
		}

		// ================= GET MY HISTORY =================
		[HttpGet("my")]
		public IActionResult GetMyHistory()
		{
			var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			if (string.IsNullOrEmpty(userId))
				return Unauthorized();

			var history = _context.UserHistories
				.Where(x => x.UserId == userId && !x.IsGrant) // Студент бачить тільки не-гранти
				.Include(x => x.Quiz)
				.OrderByDescending(x => x.CompletedAt)
				.Select(x => new UserHistoryDto
				{
					Id = x.Id,
					QuizId = x.QuizId,
					QuizTitle = x.Quiz != null ? x.Quiz.Title : "Unknown",
					MaxScore = x.MaxScore,
					Score = x.Score,
					UserAnswersJson = x.UserAnswersJson,
					CompletedAt = x.CompletedAt
				})
				.ToList();

			return Ok(history);
		}

		// ================= GET ALL (FOR MENTORS) =================
		[HttpGet("all")]
		[Authorize(Roles = "Mentor")]
		public IActionResult GetAllHistory()
		{
			var history = _context.UserHistories
				.Where(x => x.UserAnswersJson != null) // Ментор бачить ВСІ проходження, навіть "видалені" студентом
				.Include(x => x.Quiz)
				.Include(x => x.User)
				.OrderByDescending(x => x.CompletedAt)
				.Select(x => new UserHistoryDto
				{
					Id = x.Id,
					QuizId = x.QuizId,
					QuizTitle = x.Quiz != null ? x.Quiz.Title : "Unknown",
					UserEmail = x.User != null ? x.User.Email : "Unknown",
					UserName = x.User != null ? x.User.Login : "Unknown",
					MaxScore = x.MaxScore,
					Score = x.Score,
					UserAnswersJson = x.UserAnswersJson,
					CompletedAt = x.CompletedAt
				})
				.ToList();

			return Ok(history);
		}

		// ================= CREATE =================
		[HttpPost("create")]
		public IActionResult CreateHistory([FromBody] UserHistory model)
		{
			var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			if (string.IsNullOrEmpty(userId))
				return Unauthorized();

			model.UserId = userId;
			model.CompletedAt = DateTime.UtcNow;
			model.IsGrant = false; // Нове проходження завжди видиме студенту

			_context.UserHistories.Add(model);
			_context.SaveChanges();

			return Ok(model);
		}

		// ================= DELETE =================
		[HttpDelete("delete/{id}")]
		public IActionResult DeleteHistory(int id)
		{
			var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			if (string.IsNullOrEmpty(userId))
				return Unauthorized();

			var record = _context.UserHistories
				.FirstOrDefault(x => x.Id == id && x.UserId == userId);

			if (record == null)
				return NotFound("Record not found or access denied.");

			// Логічне видалення для студента: просто позначаємо як Grant
			// Це приховає запис у GetMyHistory, але збереже для ментора та ліміту спроб
			record.IsGrant = true; 
			_context.SaveChanges();

			return Ok(new { message = "Record removed from history." });
		}

		[HttpDelete("clear")]
		public async Task<IActionResult> ClearHistory()
		{
			var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			if (string.IsNullOrEmpty(userId))
				return Unauthorized();

			var history = _context.UserHistories
				.Where(x => x.UserId == userId && !x.IsGrant);

			foreach (var record in history)
			{
				record.IsGrant = true; // Приховуємо всі записи від студента
			}

			await _context.SaveChangesAsync();

			return Ok();
		}
	}
}