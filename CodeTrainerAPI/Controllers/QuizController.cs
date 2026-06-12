using CodeTrainerAPI.Data;
using CodeTrainerAPI.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeTrainerAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public partial class QuizController : ControllerBase
	{
		private readonly AppDbContext _context;

		public QuizController(AppDbContext context)
		{
			_context = context;
		}

		// ================= GET: api/quiz/my =================
		[HttpGet("my")]
		[Authorize(Roles = "Mentor")]
		public async Task<ActionResult<IEnumerable<Quiz>>> GetMyQuizzes()
		{
			var mentorId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

			if (mentorId == null)
				return Unauthorized();

			var quizzes = await _context.Quizzes
				.Where(q => q.MentorId == mentorId)
				.Include(q => q.Tasks)
					.ThenInclude(t => t.Tests)
				.ToListAsync();

			return quizzes;
		}

		// ================= GET: api/quiz =================
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Quiz>>> GetQuizzes()
		{
			return await _context.Quizzes
				.Include(q => q.Tasks)
					.ThenInclude(t => t.Tests)
				.ToListAsync();
		}

		// ================= GET: api/quiz/5 =================
		[HttpGet("{id}")]
		public async Task<ActionResult<Quiz>> GetQuiz(int id)
		{
			var quiz = await _context.Quizzes
				.Include(q => q.Tasks)
					.ThenInclude(t => t.Tests)
				.FirstOrDefaultAsync(q => q.Id == id);

			if (quiz == null) return NotFound();

			return quiz;
		}

		//// ================= POST: api/quiz =================
		[HttpPost]
		[Authorize(Roles = "Mentor")]
		public async Task<ActionResult<Quiz>> CreateQuiz([FromBody] Quiz quiz)
		{
			if (quiz == null)
				return BadRequest();

			_context.Quizzes.Add(quiz);
			await _context.SaveChangesAsync();

			return CreatedAtAction(nameof(GetQuiz), new { id = quiz.Id }, quiz);
		}

		[HttpGet("mentor/{mentorId}")]
		public async Task<ActionResult<IEnumerable<Quiz>>> GetMentorQuizzes(string mentorId)
		{
			var quizzes = await _context.Quizzes
				.Where(q => q.MentorId == mentorId)
				.Include(q => q.Tasks)
					.ThenInclude(t => t.Tests)
				.ToListAsync();

			return quizzes;
		}

		// ================= PUT: api/quiz/5 =================
		[HttpPut("{id}")]
		[Authorize(Roles = "Mentor")]
		public async Task<IActionResult> UpdateQuiz(int id, [FromBody] Quiz updatedQuiz)
		{
			if (id != updatedQuiz.Id) return BadRequest();

			var existingQuiz = await _context.Quizzes
				.Include(q => q.Tasks)
					.ThenInclude(t => t.Tests)
				.FirstOrDefaultAsync(q => q.Id == id);

			if (existingQuiz == null) return NotFound();

			// Оновлюємо основні властивості
			existingQuiz.Title = updatedQuiz.Title;
			existingQuiz.Description = updatedQuiz.Description;

			// СИНХРОНІЗАЦІЯ ЗАДАЧ (Tasks)
			// 1. Видаляємо задачі, яких немає в новому списку
			foreach (var existingTask in existingQuiz.Tasks.ToList())
			{
				if (!updatedQuiz.Tasks.Any(t => t.Id == existingTask.Id))
					_context.ProgrammingTasks.Remove(existingTask);
			}

			// 2. Оновлюємо існуючі або додаємо нові задачі
			foreach (var taskDto in updatedQuiz.Tasks)
			{
				var existingTask = existingQuiz.Tasks.FirstOrDefault(t => t.Id == taskDto.Id && t.Id != 0);

				if (existingTask != null)
				{
					// Оновлюємо поля задачі
					existingTask.Title = taskDto.Title;
					existingTask.Description = taskDto.Description;
					existingTask.CodeTemplate = taskDto.CodeTemplate;

					// СИНХРОНІЗАЦІЯ ТЕСТІВ для задачі
					// 1. Видаляємо старі тести
					foreach (var existingTest in existingTask.Tests.ToList())
					{
						if (!taskDto.Tests.Any(t => t.Id == existingTest.Id))
							_context.TestCases.Remove(existingTest);
					}

					// 2. Оновлюємо/додаємо тести
					foreach (var testDto in taskDto.Tests)
					{
						var existingTest = existingTask.Tests.FirstOrDefault(t => t.Id == testDto.Id && t.Id != 0);
						if (existingTest != null)
						{
							existingTest.Call = testDto.Call;
							existingTest.Expected = testDto.Expected;
						}
						else
						{
							existingTask.Tests.Add(new TestCase
							{
								Call = testDto.Call,
								Expected = testDto.Expected
							});
						}
					}
				}
				else
				{
					// Додаємо абсолютно нову задачу
					existingQuiz.Tasks.Add(taskDto);
				}
			}

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!QuizExists(id)) return NotFound();
				throw;
			}

			return NoContent();
		}

		// ================= DELETE: api/quiz/5 =================
		[HttpDelete("{id}")]
		[Authorize(Roles = "Mentor")]
		public async Task<IActionResult> DeleteQuiz(int id)
		{
			var quiz = await _context.Quizzes.FindAsync(id);
			if (quiz == null) return NotFound();

			_context.Quizzes.Remove(quiz);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		// ================= ATTEMPTS LOGIC =================
		[HttpGet("{id}/attempts-left")]
		[Authorize]
		public async Task<ActionResult<int>> GetRemainingAttempts(int id)
		{
			var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
			if (userId == null) return Unauthorized();

			// Використані спроби: всі записи, де є реальні відповіді
			int usedAttempts = await _context.UserHistories
				.CountAsync(h => h.UserId == userId && h.QuizId == id && h.UserAnswersJson != null);
			
			// Надані бонуси: IsGrant=true та відповіді порожні
			int extraAttempts = await _context.UserHistories
				.CountAsync(h => h.UserId == userId && h.QuizId == id && h.IsGrant && h.UserAnswersJson == null);

			int remaining = 2 + extraAttempts - usedAttempts;
			return Ok(remaining < 0 ? 0 : remaining);
		}

		[HttpGet("{id}/allowed-attempts/{userEmail}")]
		[Authorize(Roles = "Mentor")]
		public async Task<ActionResult<int>> GetAllowedAttempts(int id, string userEmail)
		{
			var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);
			if (user == null) return NotFound("User not found");

			// Тільки чисті бонуси (без відповідей)
			int extraAttempts = await _context.UserHistories
				.CountAsync(h => h.UserId == user.Id && h.QuizId == id && h.IsGrant && h.UserAnswersJson == null);
			
			return Ok(2 + extraAttempts);
		}

		[HttpPost("grant-attempts")]
		[Authorize(Roles = "Mentor")]
		public async Task<IActionResult> GrantExtraAttempts([FromBody] GrantAttemptsRequest dto)
		{
			var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.UserEmail);
			if (user == null) return NotFound("User not found");

			int targetTotal = dto.Count;
			if (targetTotal < 2) targetTotal = 2;

			// Рахуємо чисті бонуси
			int currentExtra = await _context.UserHistories
				.CountAsync(h => h.UserId == user.Id && h.QuizId == dto.QuizId && h.IsGrant && h.UserAnswersJson == null);
			
			int targetExtra = targetTotal - 2;

			if (targetExtra > currentExtra)
			{
				for (int i = 0; i < (targetExtra - currentExtra); i++)
				{
					_context.UserHistories.Add(new UserHistory
					{
						UserId = user.Id,
						QuizId = dto.QuizId,
						IsGrant = true,
						UserAnswersJson = null, // Це маркер бонусу
						CompletedAt = DateTime.UtcNow
					});
				}
			}
			else if (targetExtra < currentExtra)
			{
				var bonusesToRemove = await _context.UserHistories
					.Where(h => h.UserId == user.Id && h.QuizId == dto.QuizId && h.IsGrant && h.UserAnswersJson == null)
					.OrderByDescending(h => h.CompletedAt)
					.Take(currentExtra - targetExtra)
					.ToListAsync();

				_context.UserHistories.RemoveRange(bonusesToRemove);
			}

			await _context.SaveChangesAsync();
			return Ok(new { message = $"Total allowed attempts set to {targetTotal} for {dto.UserEmail}" });
		}

		// ================= HELPERS =================
		private bool QuizExists(int id)
		{
			return _context.Quizzes.Any(q => q.Id == id);
		}
	}
}
