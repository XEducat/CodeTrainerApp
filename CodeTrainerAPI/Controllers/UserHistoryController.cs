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
				.Where(x => x.UserId == userId)
				.Include(x => x.Quiz)   // Підтягуємо тест
				.OrderByDescending(x => x.CompletedAt)
				.Select(x => new UserHistoryDto
				{
					Id = x.Id,
					QuizId = x.QuizId,
					QuizTitle = x.Quiz != null ? x.Quiz.Title : "Unknown",
					MaxScore = x.MaxScore,
					Score = x.Score,
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

			_context.UserHistories.Remove(record);
			_context.SaveChanges();

			return Ok(new { message = "Record deleted successfully." });
		}

		[HttpDelete("clear")]
		public async Task<IActionResult> ClearHistory()
		{
			var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			if (string.IsNullOrEmpty(userId))
				return Unauthorized();

			var history = _context.UserHistories
				.Where(x => x.UserId == userId);

			_context.UserHistories.RemoveRange(history);
			await _context.SaveChangesAsync();

			return Ok();
		}
	}
}