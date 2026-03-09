using CodeTrainerAPI.Data;
using CodeTrainerAPI.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeTrainerAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class QuizController : ControllerBase
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

			_context.Entry(updatedQuiz).State = EntityState.Modified;

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

		// ================= HELPERS =================
		private bool QuizExists(int id)
		{
			return _context.Quizzes.Any(q => q.Id == id);
		}
	}
}
